using ArkSavegameToolkitNet.Domain;
using ARKViewer.Configuration;
using ARKViewer.Models;
using FluentFTP;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public class ContentManager
    {
        ContentPack pack = null;

        public DateTime ContentDate { 
            get
            {
                if (pack == null) return DateTime.MinValue;
                return pack.ContentDate;
            }
        }

        public string MapName {
            get
            {
                if (pack == null) return "";
                return pack.MapFilename;
            }
        }

        public Image MapImage { get; internal set; }
        public bool MapTerminals { get; set; } = true;
        public bool MapOilVeins { get; set; } = true;
        public bool MapGasVeins { get; set; } = true;
        public bool MapWaterVeins { get; set; } = true;
        public bool MapChargeNodes { get; set; } = true;
        public bool MapArtifacts { get; set; } = true;
        public bool MapWyvernNests { get; set; } = true;
        public bool MapDeinoNests { get; set; } = true;
        public bool MapDrakeNests { get; set; } = true;
        public bool MapMagmaNests { get; set; } = true;
        public bool MapBeaverDams { get; set; } = true;
        public bool MapGlitches { get; set; } = true;

        private void InitMapImage()
        {
            switch (MapName.ToLower())
            {
                case "thecenter":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_thecenter, new Size(1024, 1024));
                    break;
                case "theisland":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_theisland, new Size(1024, 1024));
                    break;
                case "scorchedearth_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_scorchedearth, new Size(1024, 1024));
                    break;
                case "aberration_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_aberration, new Size(1024, 1024));
                    break;
                case "ragnarok":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_ragnarok, new Size(1024, 1024));
                    break;
                case "extinction":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_extinction, new Size(1024, 1024));
                    break;
                case "valguero_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_valguero, new Size(1024, 1024));
                    break;
                case "crystalisles":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_crystalisles, new Size(1024, 1024));
                    break;
                case "tunguska_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_tunguska, new Size(1024, 1024));
                    break;
                case "caballus_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_caballus, new Size(1024, 1024));
                    break;
                case "genesis":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_genesis, new Size(1024, 1024));
                    break;
                case "astralark":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_astralark, new Size(1024, 1024));
                    break;
                case "hope":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_hope, new Size(1024, 1024));
                    break;
                case "viking_p":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_fjordur, new Size(1024, 1024));
                    break;
                case "tiamatprime":
                    MapImage = new Bitmap(ARKViewer.Properties.Resources.map_tiamat, new Size(1024, 1024));
                    break;
                default:
                    MapImage = new Bitmap(1024, 1024);
                    break;
            }
        }

        public ContentManager(ArkGameData data)
        {
            pack = new ContentPack(data,0,0,50,50,100);
            InitMapImage();
        }

        public ContentManager(ContentPack data)
        {
            pack = data;
            InitMapImage();
        }

        ~ContentManager()
        {
            pack = null;
        }


        public ContentInventory GetInventory(long inventoryId)
        {
            if (pack == null) return new ContentInventory();
            var inventory = pack.Inventories.FirstOrDefault<ContentInventory>(i => i.InventoryId == inventoryId);
            if (inventory == null) inventory = new ContentInventory();
            return inventory;
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


        public List<ContentTamedCreature> GetTamedCreatures(string selectedClass, long selectedTribeId, long selectedPlayerId, bool includeCryoVivarium)
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

        public List<ContentStructure> GetTerminals() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.TerminalMarkers;
        }
        public List<ContentStructure> GetGlitchMarkers() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.GlitchMarkers;
        }
        public List<ContentStructure> GetChargeNodes() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.ChargeNodes;
        }
        public List<ContentStructure> GetBeaverDams() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.BeaverDams;
        }
        public List<ContentStructure> GetWyvernNests() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.WyvernNests;
        }
        public List<ContentStructure> GetDrakeNests() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.DrakeNests;
        }

        public List<ContentStructure> GetDeinoNests()
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.DeinoNests;
        }
        public List<ContentStructure> GetMagmaNests() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.MagmaNests;
        }
        public List<ContentStructure> GetOilVeins() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.OilVeins;
        }
        public List<ContentStructure> GetWaterVeins() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.WaterVeins;
        }
        public List<ContentStructure> GetGasVeins() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.GasVeins;
        }
        public List<ContentStructure> GetArtifacts() 
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.Artifacts;
        }

        public List<ContentStructure> GetPlayerStructures(long selectedTribeId, long selectedPlayerId, string selectedClass)
        {
            var tribeStructures = pack.Tribes
                .Where(t =>
                    (t.TribeId == selectedTribeId || (selectedTribeId == 0 && selectedPlayerId==0))
                    || t.Players.Any(p=>p.Id == selectedPlayerId)
                ).SelectMany(s =>
                    s.Structures.Where(x =>
                        (selectedClass.Length == 0 || x.ClassName == selectedClass)
                        &&
                        (!Program.ProgramConfig.StructureExclusions.Contains(x.ClassName))

                    )
                ).ToList();

            return tribeStructures;
        }

        public List<ContentPlayer> GetPlayers(long selectedTribeId, long selectedPlayerId)
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

        public List<ContentTribe> GetTribes(long selectedTribeId)
        {
            return pack.Tribes.Where(t => selectedTribeId == 0 || t.TribeId == selectedTribeId).ToList();
        }

        public ContentTribe GetPlayerTribe(long playerId)
        {
            return pack.Tribes.FirstOrDefault<ContentTribe>(t => t.Players.Any(p => p.Id == playerId));
        }


        public List<ContentDroppedItem> GetDroppedItems(long playerId, string className)
        {
            var foundItems = pack.DroppedItems
                .Where(d =>
                    d.IsDeathCache == false
                    && (d.DroppedByPlayerId == playerId || playerId == 0)
                    && (d.ClassName == className || className == "")
                ).ToList();

            return foundItems;
        }

        public List<ContentDroppedItem> GetDeathCacheBags(long playerId)
        {
            return pack.DroppedItems
                .Where(d => 
                    d.IsDeathCache
                    && (d.DroppedByPlayerId == playerId || playerId ==0)
                ).ToList();
        }

        // Export options
        public void ExportContentPack(string exportFilename)
        {
            pack.ExportPack(exportFilename);
        }

        public void ExportAll(string exportPath)
        {
            ExportWild(Path.Combine(exportPath, "ASV_Wild.json"));
            ExportTamed(Path.Combine(exportPath, "ASV_Tamed.json"));
            ExportPlayerTribes(Path.Combine(exportPath, "ASV_Tribes.json"));
            ExportPlayers(Path.Combine(exportPath, "ASV_Players.json"));
            ExportPlayerStructures(Path.Combine(exportPath, "ASV_Structures.json"));
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
                            jw.WriteValue(creature.Latitude.GetValueOrDefault(0));

                            jw.WritePropertyName("lon");
                            jw.WriteValue(creature.Longitude.GetValueOrDefault(0));

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
                            jw.WriteValue(creature.Latitude.GetValueOrDefault(0));

                            jw.WritePropertyName("lon");
                            jw.WriteValue(creature.Longitude.GetValueOrDefault(0));

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
                                jw.WriteValue(structure.Latitude.GetValueOrDefault(0));

                                jw.WritePropertyName("lon");
                                jw.WriteValue(structure.Longitude.GetValueOrDefault(0));

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
                                jw.WriteValue(player.Latitude.GetValueOrDefault(0));

                                jw.WritePropertyName("lon");
                                jw.WriteValue(player.Longitude.GetValueOrDefault(0));



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

        public List<ContentStructure> GetPlantZ()
        {
            if (pack == null) return new List<ContentStructure>();
            return pack.PlantZ;


        }



        public Bitmap GetMapImageWild(string className, int minLevel, int maxLevel, decimal filterLat, decimal filterLon, decimal filterRadius, decimal? selectedLat, decimal? selectedLon, List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);



            
            if(customMarkers!=null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, selectedLat, selectedLon);

            return bitmap;
        }

        public Bitmap GetMapImageTamed(string className, bool includeStored, long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon, List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);




            if (customMarkers != null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, selectedLat, selectedLon);

            return bitmap;
        }

        public Bitmap GetMapImagePlayerStructures(string className, long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon, List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);




            if (customMarkers != null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, selectedLat, selectedLon);

            return bitmap;
        }

        public Bitmap GetMapImageTribes(List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);




            if (customMarkers != null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, null, null);

            return bitmap;
        }

        public Bitmap GetMapImagePlayers(long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon, List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);




            if (customMarkers != null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, selectedLat, selectedLon);

            return bitmap;
        }

        public Bitmap GetMapDroppedItems(long playerId, decimal? selectedLat, decimal? selectedLon, List<ContentMarker> customMarkers)
        {
            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(MapImage, new Rectangle(0, 0, 1024, 1024));
            graphics = AddMapStructures(graphics);




            if (customMarkers != null && customMarkers.Count > 0)
            {
                graphics = AddCustomMarkers(graphics, customMarkers);
            }

            graphics = AddCurrentMarker(graphics, selectedLat, selectedLon);

            return bitmap;
        }

        private Graphics AddMapStructures(Graphics g)
        {

            return g;
        }
        private Graphics AddCustomMarkers(Graphics g, List<ContentMarker> markers)
        {

            return g;
        }

        private Graphics AddCurrentMarker(Graphics g, decimal? lat, decimal? lon)
        {

            return g;
        }

        public string Download()
        {
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return "";

            switch (selectedServer.Mode)
            {
                case 0:
                    //ftp
                    return DownloadFtp();

                case 1:
                    //sftp
                    return DownloadSFtp();

            }

            return "";

        }


        private string DownloadSFtp()
        {
            string downloadFilename = "";
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return downloadFilename;

            string ftpServerUrl = $"{selectedServer.Address}";
            string serverUsername = selectedServer.Username;
            string serverPassword = selectedServer.Password;
            string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            if (Program.ProgramConfig.FtpDownloadMode == 1)
            {
                //clear any previous .arktribe, .arkprofile files
                var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                foreach (var profileFile in profileFiles)
                {
                    File.Delete(profileFile);
                }

                var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                foreach (var tribeFile in tribeFiles)
                {
                    File.Delete(tribeFile);
                }

            }

            string mapFilename = ARKViewer.Program.ProgramConfig.SelectedFile;

            try
            {
                using (var sftp = new SftpClient(selectedServer.Address, selectedServer.Port, selectedServer.Username, selectedServer.Password))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(selectedServer.SaveGamePath).Where(f => f.IsRegularFile);
                    foreach (var serverFile in files)
                    {


                        if (Path.GetExtension(serverFile.Name).StartsWith(".ark"))
                        {

                            string localFilename = Path.Combine(downloadPath, serverFile.Name);


                            if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 1)
                            {
                                File.Delete(localFilename);
                            }

                            bool shouldDownload = true;

                            if (serverFile.Name.EndsWith(".ark"))
                            {
                                downloadFilename = localFilename;

                                if (serverFile.Name.ToLower() != selectedServer.Map.ToLower())
                                {
                                    shouldDownload = false;
                                }
                                else
                                {
                                    if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                    {
                                        shouldDownload = false;
                                    }
                                }
                            }
                            else
                            {
                                if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                {
                                    shouldDownload = false;
                                }
                            }

                            if (shouldDownload)
                            {
                                //delete local if any
                                if (File.Exists(localFilename))
                                {
                                    File.Delete(localFilename);
                                }

                                using (FileStream outputStream = new FileStream(localFilename, FileMode.CreateNew))
                                {
                                    sftp.DownloadFile(serverFile.FullName, outputStream);
                                    outputStream.Flush();
                                }
                                DateTime saveTime = serverFile.LastWriteTimeUtc;
                                File.SetLastWriteTime(localFilename, saveTime);

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {


            }

            return downloadFilename;
        }

        private string DownloadFtp()
        {
            string downloadedFilename = "";
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return downloadedFilename;

            selectedServer.Address = selectedServer.Address.Trim();
            selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim();
            if (!selectedServer.SaveGamePath.EndsWith("/"))
            {
                selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim() + "/";
            }


            using (FtpClient ftpClient = new FtpClient(selectedServer.Address))
            {

                ftpClient.Credentials.UserName = selectedServer.Username;
                ftpClient.Credentials.Password = selectedServer.Password;
                ftpClient.Port = selectedServer.Port;

                string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }


                // try remove existing local copies                

                if (Program.ProgramConfig.FtpDownloadMode == 1)
                {
                    //clean download
                    // ... arkprofile(s)
                    var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                    foreach (var profileFilename in profileFiles)
                    {
                        try
                        {
                            File.Delete(profileFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }

                    // ... arktribe(s)
                    var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                    foreach (var tribeFilename in tribeFiles)
                    {
                        try
                        {
                            File.Delete(tribeFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }
                }

                ftpClient.Connect();
                var serverFiles = ftpClient.GetListing(selectedServer.SaveGamePath);
                string localFilename = "";

                //get correct casing for the selected map file
                var serverSaveFile = serverFiles.Where(f => f.Name.ToLower() == selectedServer.Map.ToLower()).FirstOrDefault();
                if (serverSaveFile != null)
                {
                    localFilename = Path.Combine(downloadPath, serverSaveFile.Name);
                    downloadedFilename = localFilename;
                    bool shouldDownload = true;


                    if (File.Exists(localFilename) && serverSaveFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                    {
                        if (Program.ProgramConfig.FtpDownloadMode == 0)
                        {
                            shouldDownload = false;
                        }

                    }

                    if (shouldDownload)
                    {
                        using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                        {
                            ftpClient.Download(outputStream, serverSaveFile.FullName);
                            outputStream.Flush();
                        }
                        File.SetLastWriteTimeUtc(localFilename, serverSaveFile.Modified.ToUniversalTime());
                    }



                    //get .arktribe files
                    var serverTribeFiles = serverFiles.Where(f => f.Name.EndsWith(".arktribe"));
                    if (serverTribeFiles != null && serverTribeFiles.Count() > 0)
                    {
                        foreach (var serverTribeFile in serverTribeFiles)
                        {
                            localFilename = Path.Combine(downloadPath, serverTribeFile.Name);
                            shouldDownload = true;
                            if (File.Exists(localFilename) && serverTribeFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                            {
                                if (Program.ProgramConfig.FtpDownloadMode == 0)
                                {
                                    shouldDownload = false;
                                }

                            }


                            if (shouldDownload)
                            {
                                using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                {
                                    ftpClient.Download(outputStream, serverTribeFile.FullName);
                                    outputStream.Flush();
                                }
                                File.SetLastWriteTimeUtc(localFilename, serverTribeFile.Modified.ToUniversalTime());
                            }

                        }

                    }


                    //get .arkprofile files
                    var serverProfileFiles = serverFiles.Where(f => f.Name.EndsWith(".arkprofile"));
                    if (serverProfileFiles != null && serverProfileFiles.Count() > 0)
                    {
                        foreach (var serverProfileFile in serverProfileFiles)
                        {

                            localFilename = Path.Combine(downloadPath, serverProfileFile.Name);
                            shouldDownload = true;
                            if (File.Exists(localFilename) && serverProfileFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                            {
                                if (Program.ProgramConfig.FtpDownloadMode == 0)
                                {
                                    shouldDownload = false;
                                }

                            }
                            if (shouldDownload)
                            {
                                using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                {
                                    ftpClient.Download(outputStream, serverProfileFile.FullName);
                                    outputStream.Flush();
                                }
                                File.SetLastWriteTimeUtc(localFilename, serverProfileFile.Modified.ToUniversalTime());
                            }
                        }

                    }
                }
                else
                {
                    //save file not found on server.


                }
            }

            return downloadedFilename;

        }



    }
}
