using ArkSavegameToolkitNet.Domain;
using ARKViewer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer
{
    public class ContentManager
    {
        ContentPack pack = null;
        
        public ContentManager(ArkGameData data)
        {
            pack = new ContentPack(data,0,0,50,50,100);
        }

        public ContentManager(ContentPack data)
        {
            pack = data;
        }

        ~ContentManager()
        {
            pack = null;
        }


        //Query options
        public List<ContentWildCreature> GetWildCreatures(int minLevel, int maxLevel, float fromLat, float fromLon, float fromRadius, string selectedClass)
        {
            return pack.WildCreatures.Where(w =>
                                            ((w.ClassName == selectedClass || selectedClass == "") && ((w.BaseLevel >= minLevel && w.BaseLevel <= maxLevel) || w.BaseLevel == 0))
                                            && (Math.Abs(w.Latitude.GetValueOrDefault(0) - fromLat) <= fromRadius)
                                            && (Math.Abs(w.Longitude.GetValueOrDefault(0) - fromLon) <= fromRadius)
                                ).OrderByDescending(c => c.BaseLevel).ToList();
        }


        public List<ContentTamedCreature> GetTamedCreatures(string selectedClass, int selectedTribeId, int selectedPlayerId, bool includeCryoVivarium)
        {
            return pack.Tribes
                .Where(t => (t.TribeId == selectedTribeId || selectedTribeId == 0) || t.Players.Any(p => p.Id == selectedPlayerId))
                .SelectMany(c =>
                                c.Tames.Where(w =>
                                    (w.ClassName == selectedClass || selectedClass == "")
                                    & !(w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                    && (includeCryoVivarium || w.IsCryo == false)
                                    && (includeCryoVivarium || w.IsVivarium == false)
                                )
                            ).ToList();

        }

        public List<ContentStructure> GetPlayerStructures(int selectedTribeId, int selectedPlayerId, string selectedClass)
        {
            var tribeStructures = pack.Tribes
                .Where(t =>
                    t.TribeId == selectedTribeId || selectedTribeId == 0
                ).SelectMany(s =>
                    s.Structures.Where(x =>
                        (selectedClass.Length == 0 || x.ClassName == selectedClass)
                        &&
                        (!Program.ProgramConfig.StructureExclusions.Contains(x.ClassName))

                    )
                ).ToList();

            return tribeStructures;
        }

        public List<ContentPlayer> GetPlayers(int selectedTribeId, int selectedPlayerId)
        {
            var tribePlayers = pack.Tribes
                .Where(t =>
                    t.TribeId == selectedTribeId || selectedTribeId == 0
                ).SelectMany(s =>
                    s.Players.Where(p =>
                        (selectedPlayerId == 0 || p.Id == selectedPlayerId)
                    )
                ).ToList();

            return tribePlayers;
        }

        public List<ContentTribe> GetTribes(int selectedTribeId)
        {
            return pack.Tribes.Where(t => selectedTribeId == 0 || t.TribeId == selectedTribeId).ToList();
        }

        public List<ContentDroppedItem> GetDroppedItems(int playerId, string className)
        {
            return new List<ContentDroppedItem>();
        }

        public List<ContentDroppedItem> GetDeathCacheBags(int playerId)
        {
            return new List<ContentDroppedItem>();
        }








        // Export options
        public void ExportAll(string exportPath)
        {

        }

        public void ExportWild(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        //var creatureList = Program.ProgramConfig.SortCommandLineExport ? gd.WildCreatures.OrderBy(o => o.ClassName).Cast<ArkWildCreature>() : gd.WildCreatures;
                        var creatureList = Program.ProgramConfig.SortCommandLineExport ? pack.WildCreatures.OrderBy(o => o.ClassName).ToList(): pack.WildCreatures;
                        jw.WriteStartArray();

                        //Creature, Sex, Lvl, Lat, Lon, HP, Stam, Weight, Speed, Food, Oxygen, Craft, C0, C1, C2, C3, C4, C5              
                        foreach (var creature in creatureList)
                        {
                            jw.WriteStartObject();

                            jw.WritePropertyName("id");
                            jw.WriteValue(creature.Id);

                            jw.WritePropertyName("creature");
                            jw.WriteValue(creature.ClassName);

                            jw.WritePropertyName("sex");
                            jw.WriteValue(creature.Gender);

                            jw.WritePropertyName("lvl");
                            jw.WriteValue(creature.BaseLevel);

                            jw.WritePropertyName("lat");
                            jw.WriteValue(creature.Latitude);

                            jw.WritePropertyName("lon");
                            jw.WriteValue(creature.Longitude);

                            jw.WritePropertyName("hp");
                            jw.WriteValue(creature.BaseStats[0]);

                            jw.WritePropertyName("stam");
                            jw.WriteValue(creature.BaseStats[1]);

                            jw.WritePropertyName("melee");
                            jw.WriteValue(creature.BaseStats[8]);

                            jw.WritePropertyName("weight");
                            jw.WriteValue(creature.BaseStats[7]);

                            jw.WritePropertyName("speed");
                            jw.WriteValue(creature.BaseStats[9]);

                            jw.WritePropertyName("food");
                            jw.WriteValue(creature.BaseStats[4]);

                            jw.WritePropertyName("oxy");
                            jw.WriteValue(creature.BaseStats[3]);

                            jw.WritePropertyName("craft");
                            jw.WriteValue(creature.BaseStats[11]);

                            jw.WritePropertyName("c0");
                            jw.WriteValue(creature.Colors[0]);

                            jw.WritePropertyName("c1");
                            jw.WriteValue(creature.Colors[1]);

                            jw.WritePropertyName("c2");
                            jw.WriteValue(creature.Colors[2]);

                            jw.WritePropertyName("c3");
                            jw.WriteValue(creature.Colors[3]);

                            jw.WritePropertyName("c4");
                            jw.WriteValue(creature.Colors[4]);

                            jw.WritePropertyName("c5");
                            jw.WriteValue(creature.Colors[5]);

                            jw.WritePropertyName("ccc");
                            jw.WriteValue($"{creature.X} {creature.Y} {creature.Z}");

                            jw.WriteEnd();
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

        public void ExportTamed(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        var allTames = pack.Tribes.SelectMany(t => t.Tames).ToList();

                        //var creatureList = shouldSort ? gd.TamedCreatures.OrderBy(o => o.ClassName).Cast<ArkTamedCreature>() : gd.TamedCreatures;
                        var creatureList = Program.ProgramConfig.SortCommandLineExport ? allTames.OrderBy(o => o.ClassName).ToList() : allTames;
                        jw.WriteStartArray();

                        foreach (var creature in creatureList)
                        {

                            jw.WriteStartObject();
                            jw.WritePropertyName("id");
                            jw.WriteValue(creature.Id);

                            jw.WritePropertyName("tribeid");
                            jw.WriteValue(creature.TargetingTeam);

                            jw.WritePropertyName("tribe");
                            jw.WriteValue(creature.TribeName);

                            jw.WritePropertyName("tamer");
                            jw.WriteValue(creature.TamerName);

                            jw.WritePropertyName("imprinter");
                            jw.WriteValue(creature.ImprinterName);


                            jw.WritePropertyName("imprint");
                            jw.WriteValue(creature.ImprintQuality);

                            jw.WritePropertyName("creature");
                            jw.WriteValue(creature.ClassName);

                            jw.WritePropertyName("name");
                            jw.WriteValue(creature.Name != null ? creature.Name : "");


                            jw.WritePropertyName("sex");
                            jw.WriteValue(creature.Gender);

                            jw.WritePropertyName("base");
                            jw.WriteValue(creature.BaseLevel);


                            jw.WritePropertyName("lvl");
                            jw.WriteValue(creature.Level);

                            jw.WritePropertyName("lat");
                            jw.WriteValue(creature.Latitude);

                            jw.WritePropertyName("lon");
                            jw.WriteValue(creature.Longitude);

                            jw.WritePropertyName("hp-w");
                            jw.WriteValue(creature.BaseStats[0]);

                            jw.WritePropertyName("stam-w");
                            jw.WriteValue(creature.BaseStats[1]);

                            jw.WritePropertyName("melee-w");
                            jw.WriteValue(creature.BaseStats[8]);

                            jw.WritePropertyName("weight-w");
                            jw.WriteValue(creature.BaseStats[7]);

                            jw.WritePropertyName("speed-w");
                            jw.WriteValue(creature.BaseStats[9]);

                            jw.WritePropertyName("food-w");
                            jw.WriteValue(creature.BaseStats[4]);

                            jw.WritePropertyName("oxy-w");
                            jw.WriteValue(creature.BaseStats[3]);

                            jw.WritePropertyName("craft-w");
                            jw.WriteValue(creature.BaseStats[11]);

                            jw.WritePropertyName("hp-t");
                            jw.WriteValue(creature.TamedStats[0]);

                            jw.WritePropertyName("stam-t");
                            jw.WriteValue(creature.TamedStats[1]);

                            jw.WritePropertyName("melee-t");
                            jw.WriteValue(creature.TamedStats[8]);

                            jw.WritePropertyName("weight-t");
                            jw.WriteValue(creature.TamedStats[7]);

                            jw.WritePropertyName("speed-t");
                            jw.WriteValue(creature.TamedStats[9]);

                            jw.WritePropertyName("food-t");
                            jw.WriteValue(creature.TamedStats[4]);

                            jw.WritePropertyName("oxy-t");
                            jw.WriteValue(creature.TamedStats[3]);

                            jw.WritePropertyName("craft-t");
                            jw.WriteValue(creature.TamedStats[11]);


                            jw.WritePropertyName("c0");
                            jw.WriteValue(creature.Colors[0]);

                            jw.WritePropertyName("c1");
                            jw.WriteValue(creature.Colors[1]);

                            jw.WritePropertyName("c2");
                            jw.WriteValue(creature.Colors[2]);

                            jw.WritePropertyName("c3");
                            jw.WriteValue(creature.Colors[3]);

                            jw.WritePropertyName("c4");
                            jw.WriteValue(creature.Colors[4]);

                            jw.WritePropertyName("c5");
                            jw.WriteValue(creature.Colors[5]);

                            jw.WritePropertyName("mut-f");
                            jw.WriteValue(creature.RandomMutationsFemale);

                            jw.WritePropertyName("mut-m");
                            jw.WriteValue(creature.RandomMutationsMale);

                            jw.WritePropertyName("cryo");
                            jw.WriteValue(creature.IsCryo);

                            jw.WritePropertyName("viv");
                            jw.WriteValue(creature.IsVivarium);

                            jw.WritePropertyName("ccc");
                            jw.WriteValue($"{creature.X} {creature.Y} {creature.Z}");


                            if (Program.ProgramConfig.ExportInventories)
                            {
                                jw.WritePropertyName("inventory");
                                jw.WriteStartArray();
                                var inventory = pack.Inventories.FirstOrDefault<ContentInventory>(i => i.InventoryId == creature.InventoryId);
                                if(inventory!=null && inventory.Items.Count > 0)
                                {
                                    foreach (var invItem in inventory.Items)
                                    {
                                        if (!invItem.IsEngram)
                                        {
                                            jw.WriteStartObject();

                                            jw.WritePropertyName("itemId");
                                            jw.WriteValue(invItem.ClassName);

                                            jw.WritePropertyName("qty");
                                            jw.WriteValue(invItem.Quantity);


                                            jw.WritePropertyName("blueprint");
                                            jw.WriteValue(invItem.IsBlueprint);

                                            var itemMap = Program.ProgramConfig.ItemMap.FirstOrDefault(m => m.ClassName == invItem.ClassName);
                                            if (itemMap != null)
                                            {
                                                jw.WritePropertyName("category");
                                                jw.WriteValue(itemMap.Category);
                                            }

                                            jw.WriteEndObject();
                                        }

                                    }
                                }
                                


                                jw.WriteEndArray();
                            }

                            jw.WriteEnd();
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

        public void ExportPlayerStructures(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.WriteStartArray();
                        
                        foreach(var tribe in pack.Tribes)
                        {
                            var playerStructures = tribe.Structures;
                            foreach (var structure in playerStructures)
                            {
                                jw.WriteStartObject();

                                jw.WritePropertyName("tribeid");
                                jw.WriteValue(tribe.TribeId);


                                jw.WritePropertyName("tribe");
                                jw.WriteValue(tribe.TribeName);


                                jw.WritePropertyName("struct");
                                jw.WriteValue(structure.ClassName);

                                jw.WritePropertyName("lat");
                                jw.WriteValue(structure.Latitude);

                                jw.WritePropertyName("lon");
                                jw.WriteValue(structure.Longitude);

                                jw.WritePropertyName("ccc");
                                jw.WriteValue($"{structure.X} {structure.Y} {structure.Z}");

                                if (Program.ProgramConfig.ExportInventories)
                                {
                                    jw.WritePropertyName("inventory");
                                    jw.WriteStartArray();
                                    ContentInventory inventory = pack.Inventories.FirstOrDefault<ContentInventory>(i => i.InventoryId == structure.InventoryId);
                                    if(inventory!=null && inventory.Items.Count > 0)
                                    {
                                        foreach (var invItem in inventory.Items)
                                        {
                                            if (!invItem.IsEngram)
                                            {
                                                jw.WriteStartObject();

                                                jw.WritePropertyName("itemId");
                                                jw.WriteValue(invItem.ClassName);

                                                jw.WritePropertyName("qty");
                                                jw.WriteValue(invItem.Quantity);


                                                jw.WritePropertyName("blueprint");
                                                jw.WriteValue(invItem.IsBlueprint);

                                                var itemMap = Program.ProgramConfig.ItemMap.FirstOrDefault(m => m.ClassName == invItem.ClassName);
                                                if (itemMap != null)
                                                {
                                                    jw.WritePropertyName("category");
                                                    jw.WriteValue(itemMap.Category);
                                                }


                                                jw.WriteEndObject();
                                            }

                                        }
                                    }
                                    
                                    jw.WriteEndArray();
                                }

                                jw.WriteEnd();
                            }
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

        public void ExportPlayerTribes(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.WriteStartArray();

                        foreach (var playerTribe in pack.Tribes)
                        {
                            jw.WriteStartObject();

                            jw.WritePropertyName("tribeid");
                            jw.WriteValue(playerTribe.TribeId);

                            jw.WritePropertyName("tribe");
                            jw.WriteValue(playerTribe.TribeName);

                            jw.WritePropertyName("players");
                            jw.WriteValue(playerTribe.Players.Count);

                            if (playerTribe.Players != null && playerTribe.Players.Count > 0)
                            {
                                jw.WritePropertyName("members");
                                jw.WriteStartArray();
                                foreach (var tribePlayer in playerTribe.Players)
                                {
                                    jw.WriteStartObject();

                                    jw.WritePropertyName("ign");
                                    jw.WriteValue(tribePlayer.CharacterName);

                                    jw.WritePropertyName("lvl");
                                    jw.WriteValue(tribePlayer.Level.ToString());

                                    jw.WritePropertyName("playerid");
                                    jw.WriteValue(tribePlayer.Id.ToString());

                                    jw.WritePropertyName("playername");
                                    jw.WriteValue(tribePlayer.Name);

                                    jw.WritePropertyName("steamid");
                                    jw.WriteValue(tribePlayer.SteamId);

                                    jw.WriteEndObject();
                                }

                                jw.WriteEndArray();
                            }



                            jw.WritePropertyName("tames");
                            jw.WriteValue(playerTribe.Tames.Count);

                            jw.WritePropertyName("structures");
                            jw.WriteValue(playerTribe.Structures.Count);

                            jw.WritePropertyName("active");
                            jw.WriteValue(playerTribe.LastActive);


                            jw.WriteEnd();
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

        public void ExportPlayers(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.WriteStartArray();

                        foreach(var tribe in pack.Tribes)
                        {
                            foreach (var player in tribe.Players)
                            {
                                jw.WriteStartObject();

                                jw.WritePropertyName("playerid");
                                jw.WriteValue(player.Id);

                                jw.WritePropertyName("steam");
                                jw.WriteValue(player.Name);

                                jw.WritePropertyName("name");
                                jw.WriteValue(player.CharacterName);

                                jw.WritePropertyName("tribeid");
                                jw.WriteValue(tribe.TribeId);

                                jw.WritePropertyName("tribe");
                                jw.WriteValue(tribe.TribeName);

                                jw.WritePropertyName("sex");
                                jw.WriteValue(player.Gender);

                                jw.WritePropertyName("lvl");
                                jw.WriteValue(player.Level);

                                jw.WritePropertyName("lat");
                                jw.WriteValue(player.Latitude);

                                jw.WritePropertyName("lon");
                                jw.WriteValue(player.Longitude);



                                //0=health
                                //1=stamina
                                //2=torpor
                                //3=oxygen
                                //4=food
                                //5=water
                                //6=temperature
                                //7=weight
                                //8=melee damage
                                //9=movement speed
                                //10=fortitude
                                //11=crafting speed
                                jw.WritePropertyName("hp");
                                jw.WriteValue(player.Stats[0]);

                                jw.WritePropertyName("stam");
                                jw.WriteValue(player.Stats[1]);

                                jw.WritePropertyName("melee");
                                jw.WriteValue(player.Stats[8]);

                                jw.WritePropertyName("weight");
                                jw.WriteValue(player.Stats[7]);

                                jw.WritePropertyName("speed");
                                jw.WriteValue(player.Stats[9]);

                                jw.WritePropertyName("food");
                                jw.WriteValue(player.Stats[4]);

                                jw.WritePropertyName("water");
                                jw.WriteValue(player.Stats[5]);

                                jw.WritePropertyName("oxy");
                                jw.WriteValue(player.Stats[3]);

                                jw.WritePropertyName("craft");
                                jw.WriteValue(player.Stats[11]);

                                jw.WritePropertyName("fort");
                                jw.WriteValue(player.Stats[10]);

                                jw.WritePropertyName("active");
                                jw.WriteValue(player.LastActive);

                                jw.WritePropertyName("ccc");
                                jw.WriteValue($"{player.X} {player.Y} {player.Z}");

                                jw.WritePropertyName("steamid");
                                jw.WriteValue(player.SteamId);

                                if (Program.ProgramConfig.ExportInventories)
                                {
                                    jw.WritePropertyName("inventory");
                                    jw.WriteStartArray();

                                    var playerInventory = pack.Inventories.FirstOrDefault<ContentInventory>(i => i.InventoryId == player.InventoryId);
                                    if(playerInventory!=null && playerInventory.Items.Count > 0)
                                    {
                                        foreach (var invItem in playerInventory.Items)
                                        {
                                            if (!invItem.IsEngram && invItem.ClassName != "PrimalItem_StartingNote_C")
                                            {
                                                jw.WriteStartObject();

                                                jw.WritePropertyName("itemId");
                                                jw.WriteValue(invItem.ClassName);

                                                jw.WritePropertyName("qty");
                                                jw.WriteValue(invItem.Quantity);

                                                jw.WritePropertyName("blueprint");
                                                jw.WriteValue(invItem.IsBlueprint);

                                                var itemMap = Program.ProgramConfig.ItemMap.FirstOrDefault(m => m.ClassName == invItem.ClassName);
                                                if (itemMap != null)
                                                {
                                                    jw.WritePropertyName("category");
                                                    jw.WriteValue(itemMap.Category);
                                                }

                                                jw.WriteEndObject();
                                            }

                                        }
                                    }
                                    


                                    jw.WriteEndArray();
                                }


                                jw.WriteEnd();
                            }
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

    }
}
