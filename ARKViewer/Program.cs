using ArkSavegameToolkitNet.Domain;
using ArkSavegameToolkitNet.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ARKViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            ProgramConfig = new ViewerConfiguration();

            string logFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ARKViewer.log");
            TextWriter logWriter = null;

            //support quoted command line arguments which doesn't seem to be supported with Environment.GetCommandLineArgs() 
            string[] commandArguments = Regex.Split(Environment.CommandLine.Trim(), " (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            
            //remove empty argument entries
            commandArguments = commandArguments.Where(a => string.IsNullOrEmpty(a) == false).ToArray();
            if(commandArguments.Length > 0)
            {
                logWriter = new StreamWriter(logFilename);
                logWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - ArkViewer Command Line Started: {commandArguments.Length}");

                int argIndex = 0;
                foreach (string arg in commandArguments)
                {
                    logWriter.WriteLine($"\tArg-{argIndex} = {arg}");
                    argIndex++;
                }
            }
            if (commandArguments != null && commandArguments.Length > 1)
            {
                //command line, load save game data for export
                string savePath = "";
                string saveFilename = "";
                bool shouldSort = ProgramConfig.SortCommandLineExport;


                if(commandArguments.Length > 3)
                {
                    //ark save game specified
                    saveFilename = commandArguments[3].ToString().Trim().Replace("\"", "");
                    savePath = Path.GetDirectoryName(saveFilename);
                }
                else
                {
                    //use last configured save
                    switch (ARKViewer.Program.ProgramConfig.Mode)
                    {
                        case ViewerModes.Mode_SinglePlayer:
                            if (ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                            {
                                savePath = Path.GetFullPath(ARKViewer.Program.ProgramConfig.SelectedFile);
                                saveFilename = ARKViewer.Program.ProgramConfig.SelectedFile;
                            }

                            break;
                        case ViewerModes.Mode_Offline:
                            if (ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                            {
                                savePath = Path.GetFullPath(ARKViewer.Program.ProgramConfig.SelectedFile);
                                saveFilename = ARKViewer.Program.ProgramConfig.SelectedFile;
                            }
                            break;
                        case ViewerModes.Mode_Ftp:
                            savePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ARKViewer.Program.ProgramConfig.SelectedServer);

                            if (ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                            {
                                saveFilename = Path.Combine(savePath, ARKViewer.Program.ProgramConfig.SelectedFile);
                            }
                            break;

                        default:

                            break;
                    }

                }


                if (File.Exists(saveFilename))
                {
                    try
                    {


                        ArkGameData gd = new ArkGameData(saveFilename, loadOnlyPropertiesInDomain: false);
                        List<TribeMap> allTribes = new List<TribeMap>();
                        List<PlayerMap> allPlayers = new List<PlayerMap>();

                        if (gd.Update(CancellationToken.None, null, true)?.Success == true)
                        {

                            gd.ApplyPreviousUpdate();

                            //get structures
                            if (commandArguments[1].ToString().Trim().ToLower() == "structures" || commandArguments[1].ToString().Trim().ToLower() == "tribes" || commandArguments[1].ToString().Trim().ToString() == "all")
                            {
                                if (gd.Structures != null && gd.Structures.Count() > 0)
                                {
                                    var structureTribes = gd.Structures.Where(s => s.OwnerName != null && s.TargetingTeam.GetValueOrDefault(0) != 0).Select(s => new TribeMap() { TribeId = s.TargetingTeam.GetValueOrDefault(0), TribeName = s.OwnerName }).Distinct().ToList();

                                    if (structureTribes != null)
                                    {
                                        if (gd.Tribes != null && gd.Tribes.Count() > 0)
                                        {
                                            var serverTribes = gd.Tribes.Where(t => structureTribes.LongCount(s => s.TribeId == t.Id) == 0 && t.Id != 0)
                                                                        .Select(i => new TribeMap() { TribeId = i.Id, TribeName = i.Name, ContainsLog = i.Logs.Count() > 0 });

                                            if (serverTribes != null)
                                            {
                                                structureTribes.AddRange(serverTribes.ToArray());
                                            }

                                        }
                                        allTribes = structureTribes.Distinct().ToList();
                                    }
                                    else
                                    {
                                        if (gd.Tribes != null && gd.Tribes.Count() > 0)
                                        {
                                            var serverTribes = gd.Tribes.Where(t => structureTribes.LongCount(s => s.TribeId == t.Id) == 0)
                                                                        .Select(i => new TribeMap() { TribeId = i.Id, TribeName = i.Name });

                                            allTribes = serverTribes.Distinct().ToList();
                                        }


                                    }

                                    if (gd.Players != null && gd.Players.Count() > 0)
                                    {

                                        var serverPlayers = gd.Players.Select(i => new PlayerMap() { TribeId = (long)i.TribeId.GetValueOrDefault(0), PlayerId = (long)i.Id, PlayerName = i.CharacterName != null ? i.CharacterName : i.Name });
                                        if (serverPlayers != null)
                                        {
                                            foreach (var serverPlayer in serverPlayers.Where(p => p.TribeId == 0))
                                            {
                                                var testTribe = gd.Tribes.Where(t => t.MemberIds.Contains((int)serverPlayer.PlayerId)).FirstOrDefault();
                                                if (testTribe != null)
                                                {
                                                    serverPlayer.TribeId = testTribe.Id;
                                                }
                                            }

                                            allPlayers = serverPlayers.Where(p => p.TribeId != 0).ToList();
                                        }


                                    }

                                    var structurePlayers = gd.Structures.Where(s => s.TargetingTeam.GetValueOrDefault(0) != 0 && s.OwningPlayerName != null && allPlayers.LongCount(c => c.PlayerId == s.OwningPlayerId) == 0).Select(s => new PlayerMap() { TribeId = s.TargetingTeam.Value, PlayerId = (long)s.OwningPlayerId, PlayerName = s.OwningPlayerName }).Distinct().OrderBy(o => o.PlayerName).ToList();
                                    if (structurePlayers != null && structurePlayers.Count() > 0)
                                    {
                                        allPlayers.AddRange(structurePlayers.ToArray());
                                    }


                                    foreach (var tribe in allTribes)
                                    {
                                        int playerCount = allPlayers.Count(p => p.TribeId == tribe.TribeId);
                                        int tribePlayerCount = gd.Tribes.Where(t => t.Id == tribe.TribeId).Select(m => m.Members).Count();

                                        tribe.PlayerCount = playerCount > tribePlayerCount ? playerCount : tribePlayerCount;
                                        tribe.StructureCount = gd.Structures.LongCount(s => s.TargetingTeam.GetValueOrDefault(0) == tribe.TribeId);
                                        tribe.TameCount = gd.TamedCreatures.LongCount(t => t.TargetingTeam == tribe.TribeId);

                                        var tribeTames = gd.NoRafts.Where(t => t.TargetingTeam == tribe.TribeId);
                                        if (tribeTames != null && tribeTames.Count() > 0)
                                        {
                                            TimeSpan noTime = new TimeSpan();

                                            var lastTamedCreature = tribeTames.Where(t => t.TamedAtTime != null).OrderBy(o => o?.TamedForApprox.GetValueOrDefault(noTime).TotalSeconds).FirstOrDefault();
                                            if (lastTamedCreature != null)
                                            {
                                                var tamedTime = lastTamedCreature.TamedForApprox.GetValueOrDefault(noTime);
                                                tribe.LastActive = DateTime.Now.Subtract(tamedTime);
                                            }
                                        }
                                        if (gd.Players != null && gd.Players.Count() > 0)
                                        {
                                            var tribePlayers = gd.Players.Where(p => p.TribeId == tribe.TribeId);
                                            if (tribePlayers != null && tribePlayers.Count() > 0)
                                            {
                                                var lastActivePlayer = tribePlayers.OrderBy(p => p.LastActiveTime).First();
                                                if (lastActivePlayer.LastActiveTime > tribe.LastActive)
                                                {
                                                    tribe.LastActive = lastActivePlayer.LastActiveTime;
                                                }
                                            }

                                        }

                                        if (gd.Tribes != null && gd.Tribes.Count() > 0)
                                        {
                                            var actualTribe = gd.Tribes.FirstOrDefault(t => t.Id == tribe.TribeId);
                                            if (actualTribe != null)
                                            {
                                                if (actualTribe.LastActiveTime > tribe.LastActive)
                                                {
                                                    tribe.LastActive = actualTribe.LastActiveTime;
                                                    tribe.TribeName = actualTribe.Name;
                                                }
                                                //error reported by @mjfro2 - crash when tribe has no log data
                                                long logCount = actualTribe.Logs == null ? 0 : actualTribe.Logs.Count();
                                                tribe.ContainsLog = logCount > 0;
                                            }
                                        }


                                    }

                                }
                            }

                        
                        }
                        else
                        {
                            gd = null;
                        }

                        string exportFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        string exportFilename = "";

                        string commandOptionCheck = commandArguments[1].ToString().Trim().ToLower();

                        if (commandArguments.Length > 2)
                        {
                            //export filename
                            exportFilename = commandArguments[2].ToString().Trim().Replace("\"", "");

                            //use path only if "all"
                            if(commandOptionCheck == "all")
                            {
                                exportFilePath = Path.GetDirectoryName(exportFilename);
                                exportFilename = "";

                            }
                        }

                        if(commandOptionCheck ==  "wild" || commandOptionCheck == "all")
                        { 
                            //Wild 
                            exportFilename = exportFilename.Length > 0 ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Wild.json");

                            using (FileStream fs = File.Create(exportFilename))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                                    {
                                        var creatureList = shouldSort ? gd.WildCreatures.OrderBy(o => o.ClassName).Cast<ArkWildCreature>() : gd.WildCreatures;

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
                                            if (creature.Location != null && creature.Location.Latitude != null)
                                            {
                                                jw.WriteValue(creature.Location.Latitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

                                            jw.WritePropertyName("lon");
                                            if (creature.Location != null && creature.Location.Longitude != null)
                                            {
                                                jw.WriteValue(creature.Location.Longitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

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
                                            if (creature.Location != null)
                                            {
                                                jw.WriteValue($"{creature.Location.X} {creature.Location.Y} {creature.Location.Z}");

                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }

                                            jw.WriteEnd();
                                        }

                                        jw.WriteEndArray();
                                    }

                                }

                            }


                        }
                        if(commandOptionCheck == "tamed" || commandOptionCheck == "all")
                            { 
                            //Tamed
                            exportFilename = exportFilename.Length > 0 && commandOptionCheck != "all" ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Tamed.json");

                            using (FileStream fs = File.Create(exportFilename))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                                    {

                                        var creatureList = shouldSort ? gd.TamedCreatures.OrderBy(o => o.ClassName).Cast<ArkTamedCreature>() : gd.TamedCreatures;

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
                                            jw.WriteValue(creature.DinoImprintingQuality);

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
                                            if (creature.Location != null && creature.Location.Latitude != null)
                                            {
                                                jw.WriteValue(creature.Location.Latitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

                                            jw.WritePropertyName("lon");
                                            if (creature.Location != null && creature.Location.Longitude != null)
                                            {
                                                jw.WriteValue(creature.Location.Longitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

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
                                            if (creature.Location != null)
                                            {
                                                jw.WriteValue($"{creature.Location.X} {creature.Location.Y} {creature.Location.Z}");
                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }

                                            jw.WriteEnd();
                                        }

                                        jw.WriteEndArray();
                                    }

                                }

                            }
                        }

                        if(commandOptionCheck ==  "structures" || commandOptionCheck == "all")
                        { 
                            //Structures
                            exportFilename = exportFilename.Length > 0 && commandOptionCheck != "all" ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Structures.json");

                            //Player, Tribe, Structure, Lat, Lon
                            using (FileStream fs = File.Create(exportFilename))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                                    {
                                        jw.WriteStartArray();

                                        foreach (var structure in gd.Structures)
                                        {
                                            jw.WriteStartObject();

                                            jw.WritePropertyName("tribeid");
                                            jw.WriteValue(structure.TargetingTeam.GetValueOrDefault(0));

                                            jw.WritePropertyName("playerid");
                                            if (structure.OwningPlayerId == structure.TargetingTeam.GetValueOrDefault(0))
                                            {
                                                jw.WriteValue(0);
                                            }
                                            else
                                            {
                                                jw.WriteValue(structure.OwningPlayerId.GetValueOrDefault(0));
                                            }


                                            jw.WritePropertyName("tribe");
                                            if (allTribes.Count(t => t.TribeId == structure.TargetingTeam.GetValueOrDefault(0)) > 0)
                                            {
                                                jw.WriteValue(allTribes.First(t => t.TribeId == structure.TargetingTeam.GetValueOrDefault(0)).TribeName);
                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }


                                            jw.WritePropertyName("player");
                                            jw.WriteValue(structure.OwningPlayerName == "null" ? "" : structure.OwningPlayerName);

                                            jw.WritePropertyName("struct");
                                            jw.WriteValue(structure.ClassName);

                                            jw.WritePropertyName("lat");
                                            if (structure.Location != null && structure.Location.Latitude != null)
                                            {
                                                jw.WriteValue(structure.Location.Latitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }


                                            jw.WritePropertyName("lon");
                                            if (structure.Location != null && structure.Location.Longitude != null)
                                            {
                                                jw.WriteValue(structure.Location.Longitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

                                            jw.WritePropertyName("ccc");
                                            if (structure.Location != null)
                                            {
                                                jw.WriteValue($"{structure.Location.X} {structure.Location.Y} {structure.Location.Z}");
                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }


                                            jw.WriteEnd();
                                        }

                                        jw.WriteEndArray();
                                    }

                                }

                            }
                        }

                                
                        if(commandOptionCheck ==  "tribes" || commandOptionCheck == "all")
                        { 
                            //Tribes
                            exportFilename = exportFilename.Length > 0 && commandOptionCheck != "all" ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Tribes.json");

                            //Id, Name, Players, Tames, Structures, Last Active
                            using (FileStream fs = File.Create(exportFilename))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                                    {
                                        jw.WriteStartArray();

                                        foreach (var playerTribe in allTribes)
                                        {
                                            jw.WriteStartObject();

                                            jw.WritePropertyName("tribeid");
                                            jw.WriteValue(playerTribe.TribeId);

                                            jw.WritePropertyName("tribe");
                                            jw.WriteValue(playerTribe.TribeName);

                                            jw.WritePropertyName("players");
                                            jw.WriteValue(playerTribe.PlayerCount);

                                            jw.WritePropertyName("tames");
                                            jw.WriteValue(playerTribe.TameCount);

                                            jw.WritePropertyName("structures");
                                            jw.WriteValue(playerTribe.StructureCount);

                                            jw.WritePropertyName("active");
                                            jw.WriteValue(playerTribe.LastActive);


                                            jw.WriteEnd();
                                        }

                                        jw.WriteEndArray();
                                    }

                                }

                            }
                        }


                        if (commandOptionCheck == "all" || commandOptionCheck == "players")
                        {
                            //Players
                            exportFilename = exportFilename.Length > 0 && commandOptionCheck != "all" ? exportFilename : Path.Combine(exportFilePath, "ARKViewer_Export_Players.json");

                            //Id, Name, Tribe, Sex, Lvl, Lat, Lon, HP, Stam, Melee, Weight, Speed, Food, Water, Oxygen, Crafting, Fortitude, Steam, Last Online
                            using (FileStream fs = File.Create(exportFilename))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                                    {
                                        jw.WriteStartArray();

                                        foreach (var player in gd.Players)
                                        {
                                            jw.WriteStartObject();

                                            jw.WritePropertyName("playerid");
                                            jw.WriteValue(player.Id);

                                            jw.WritePropertyName("steam");
                                            jw.WriteValue(player.Name);

                                            jw.WritePropertyName("name");
                                            jw.WriteValue(player.CharacterName);

                                            jw.WritePropertyName("tribeid");
                                            jw.WriteValue(player.TribeId);

                                            jw.WritePropertyName("tribe");
                                            if (player.Tribe != null && player.Tribe.Name != null)
                                            {
                                                jw.WriteValue(player.Tribe.Name);
                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }

                                            jw.WritePropertyName("sex");
                                            jw.WriteValue(player.Gender);

                                            jw.WritePropertyName("lvl");
                                            jw.WriteValue(player.CharacterLevel);

                                            jw.WritePropertyName("lat");
                                            if (player.Location != null)
                                            {
                                                jw.WriteValue(player.Location.Latitude);
                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }

                                            jw.WritePropertyName("lon");
                                            if (player.Location != null)
                                            {
                                                jw.WriteValue(player.Location.Longitude);

                                            }
                                            else
                                            {
                                                jw.WriteValue(0);
                                            }


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
                                            jw.WriteValue(player.LastActiveTime);

                                            jw.WritePropertyName("ccc");
                                            if (player.Location != null)
                                            {
                                                jw.WriteValue($"{player.Location.X} {player.Location.Y} {player.Location.Z}");
                                            }
                                            else
                                            {
                                                jw.WriteValue("");
                                            }


                                            jw.WriteEnd();
                                        }

                                        jw.WriteEndArray();
                                    }

                                }

                            }

                        }

                        //success
                        Environment.ExitCode = 0;
                    }
                    catch (Exception ex)
                    {
                        string traceLog = ex.StackTrace;
                        string errorMessage = ex.Message;

                        try
                        {


                            StringBuilder errorData = new StringBuilder();

                            errorData.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            errorData.AppendLine($"Error: {errorMessage}");
                            errorData.AppendLine($"Trace: {traceLog}");
                            errorData.AppendLine("");
                            if (logWriter == null) logWriter = new StreamWriter(logFilename);
                            logWriter.Write(errorData.ToString());
                        }
                        catch
                        {
                            //couldn't write to error.log
                        }

                        Console.Out.WriteLine($"Error : {ex.Message.ToString()}");

                        //error
                        Environment.ExitCode = -1;

                    }

                }
                else
                {
                    //no save file found or inaccessible
                    if (logWriter == null) logWriter = new StreamWriter(logFilename);
                    logWriter.Write($"Unable to find or access save game file: {saveFilename}");
                    Environment.ExitCode = -2;
                }

                if (logWriter != null)
                {
                    logWriter.Close();
                    logWriter.Dispose();
                }

                Application.Exit();
            }
            else
            {
                //no command line options, run viewer


                Application.Run(new frmViewer());
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            string traceLog = ex.StackTrace;
            string errorMessage = ex.Message;

            frmErrorReport report = new frmErrorReport(errorMessage, traceLog);
            report.ShowDialog();


        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            string traceLog = ex.StackTrace;
            string errorMessage = ex.Message;

            frmErrorReport report = new frmErrorReport(errorMessage, traceLog);
            report.ShowDialog();

        }

        public static ViewerConfiguration ProgramConfig { get; set; }
    }
}
