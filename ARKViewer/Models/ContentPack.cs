using ArkSavegameToolkitNet.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Web.Routing;
using System.Diagnostics;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentPack
    {
        [DataMember] public string MapFilename { get; set; } = "TheIsland.ark";
        [DataMember] public long ExportedForTribe { get; set; } = 0;
        [DataMember] public long ExportedForPlayer { get; set; } = 0;
        [DataMember] public DateTime ExportedTimestamp { get; set; } = DateTime.Now;
        [DataMember] public List<ContentStructure> TerminalMarkers { get; set; }
        [DataMember] public List<ContentStructure> GlitchMarkers { get; set; }
        [DataMember] public List<ContentStructure> ChargeNodes { get; set; }
        [DataMember] public List<ContentStructure> BeaverDams { get; set; }
        [DataMember] public List<ContentStructure> WyvernNests { get; set; }
        [DataMember] public List<ContentStructure> DrakeNests { get; set; }
        [DataMember] public List<ContentStructure> MagmaNests { get; set; }
        [DataMember] public List<ContentStructure> DeinoNests { get; set; }
        [DataMember] public List<ContentStructure> OilVeins { get; set; }
        [DataMember] public List<ContentStructure> WaterVeins { get; set; }
        [DataMember] public List<ContentStructure> GasVeins { get; set; }
        [DataMember] public List<ContentStructure> Artifacts { get; set; }
        [DataMember] public List<ContentStructure> PlantZ { get; set; }
        [DataMember] public List<ContentInventory> Inventories { get; set; }
        [DataMember] public List<ContentDroppedItem> DroppedItems { get; set; }
        [DataMember] public List<ContentWildCreature> WildCreatures { get; set; }

        [DataMember] public List<ContentTribe> Tribes { get; set; }

        bool IncludeGameStructures { get; set; } = true;
        bool IncludeGameStructureContent { get; set; } = true;
        bool IncludeTribesPlayers { get; set; } = true;
        bool IncludeTamed { get; set; } = true;
        bool IncludeWild { get; set; } = true;
        bool IncludePlayerStructures { get; set; } = true;
        decimal FilterLatitude { get; set; } = 50;
        decimal FilterLongitude { get; set; } = 50;
        decimal FilterRadius { get; set; } = 100;
        public DateTime ContentDate { get; internal set; }


        public ContentPack()
        {
            //initialize defaults
            MapFilename = "TheIsland.ark";
            ExportedForTribe = 0;
            ExportedForPlayer = 0;
            FilterLatitude = 50;
            FilterLongitude = 50;
            FilterRadius = 100;

            TerminalMarkers = new List<ContentStructure>();
            GlitchMarkers = new List<ContentStructure>();
            ChargeNodes = new List<ContentStructure>();
            BeaverDams = new List<ContentStructure>();
            WyvernNests = new List<ContentStructure>();
            DrakeNests = new List<ContentStructure>();
            MagmaNests = new List<ContentStructure>();
            DeinoNests = new List<ContentStructure>();
            OilVeins = new List<ContentStructure>();
            WaterVeins = new List<ContentStructure>();
            GasVeins = new List<ContentStructure>();
            Artifacts = new List<ContentStructure>();
            PlantZ = new List<ContentStructure>();
            Inventories = new List<ContentInventory>();
            WildCreatures = new List<ContentWildCreature>();
            Tribes = new List<ContentTribe>();
            DroppedItems = new List<ContentDroppedItem>();
        }

        public ContentPack(ArkGameData gd, long selectedTribeId, long selectedPlayerId, decimal lat, decimal lon, decimal rad, bool includeGameStructures, bool includeGameStructureContent, bool includeTribesPlayers, bool includeTamed, bool includeWild, bool includePlayerStructures): this()
        {
            ExportedForTribe = selectedTribeId;
            ExportedForPlayer = selectedPlayerId;

            FilterLatitude = lat;
            FilterLongitude = lon;
            FilterRadius = rad;

            IncludeGameStructures = includeGameStructures;
            IncludeGameStructureContent = includeGameStructureContent;
            IncludeTribesPlayers = includeTribesPlayers;
            IncludeTamed = includeTamed;
            IncludeWild = includeWild;
            IncludePlayerStructures = includePlayerStructures;

            LoadGameData(gd);
        }

        public ContentPack(ArkGameData gd, int selectedTribeId, int selectedPlayerId, decimal lat, decimal lon, decimal rad): this(gd, selectedTribeId, selectedPlayerId, lat, lon, rad,true,true,true,true,true,true)
        {

        }


        public ContentPack(byte[] dataPack): this()
        {
            string jsonContent = Unzip(dataPack);
            LoadJson(jsonContent);
        }

        private void LoadJson(string jsonPack)
        {
            //load content from json
            try
            {
                ContentPack loaded = new ContentPack();
                loaded = JsonConvert.DeserializeObject<ContentPack>(jsonPack);

                MapFilename = loaded.MapFilename;
                ExportedTimestamp = loaded.ExportedTimestamp;
                ExportedForTribe = loaded.ExportedForTribe;
                ExportedForPlayer = loaded.ExportedForPlayer;
                TerminalMarkers = loaded.TerminalMarkers;
                GlitchMarkers = loaded.GlitchMarkers;
                ChargeNodes = loaded.ChargeNodes;
                BeaverDams = loaded.BeaverDams;
                WyvernNests = loaded.WyvernNests;
                DrakeNests = loaded.DrakeNests;
                MagmaNests = loaded.MagmaNests;
                OilVeins = loaded.OilVeins;
                WaterVeins = loaded.WaterVeins;
                GasVeins = loaded.GasVeins;
                Artifacts = loaded.Artifacts;
                PlantZ = loaded.PlantZ;
                Inventories = loaded.Inventories;
                WildCreatures = loaded.WildCreatures;
                Tribes = loaded.Tribes;
                DroppedItems = loaded.DroppedItems;

            }
            catch
            {

            }
            
        }

        public void ExportPack(string fileName)
        {
           
            string filePath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            string jsonContent = JsonConvert.SerializeObject(this);
            var compressedContent = Zip(jsonContent);
            try
            {
                using(var writer = new FileStream(fileName,FileMode.CreateNew))
                {
                    writer.Write(compressedContent, 0, compressedContent.Length);
                    writer.Flush();
                }
            }
            catch
            {
                throw;
            }
            
        }


        private void LoadGameData(ArkGameData gd)
        {
            //load content pack from Ark savegame 
            MapFilename = gd.SaveState.MapName;

            


            if (IncludeGameStructures)
            {


                //TerminalMarkers
                ConcurrentBag<ContentStructure> loadedStructures = new ConcurrentBag<ContentStructure>();

                var mapDetectedTerminals = gd.Structures.Where(s => s.ClassName.ToLower().Contains("terminal"));
                Parallel.ForEach(mapDetectedTerminals, terminal =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_Terminal",
                        Latitude = (float)terminal.Location.Latitude.GetValueOrDefault(0),
                        Longitude = (float)terminal.Location.Longitude.GetValueOrDefault(0),
                        X = terminal.Location.X,
                        Y = terminal.Location.Y,
                        Z = terminal.Location.Z,
                        InventoryId = IncludeGameStructureContent?addInventory(terminal.Inventory):0
                    });

                });


                //user defined terminals
                var mapTerminals = loadedStructures.ToList();                
                var terminals = Program.ProgramConfig.TerminalMarkers
                    .Where(m =>
                        m.Map.ToLower().StartsWith(MapFilename.ToLower())
                        //exclude any that match map detected terminal location
                        &! mapTerminals.Any(t=>t.Latitude.ToString().StartsWith(m.Lat.ToString()) && t.Longitude.ToString().StartsWith(m.Lon.ToString()))
                    ).ToList();


                Parallel.ForEach(terminals, terminal =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_Terminal",
                        Latitude = (float)terminal.Lat,
                        Longitude = (float)terminal.Lon,
                        X = terminal.X,
                        Y = terminal.Y,
                        Z = terminal.Z,
                        InventoryId = 0
                    });
                    
                });
                if (!loadedStructures.IsEmpty) TerminalMarkers.AddRange(loadedStructures.ToList());

                //Charge nodes
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var chargeNodes = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("PrimalItem_PowerNodeCharge")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(chargeNodes, chargeNode =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_ChargeNode",
                        Latitude = chargeNode.Location.Latitude.GetValueOrDefault(0),
                        Longitude = chargeNode.Location.Longitude.GetValueOrDefault(0),
                        X = chargeNode.Location.X,
                        Y = chargeNode.Location.Y,
                        Z = chargeNode.Location.Z,
                        InventoryId = addInventory(chargeNode.Inventory)
                    }); ;
                });
                if (!loadedStructures.IsEmpty) ChargeNodes.AddRange(loadedStructures.ToList());

                //GlitchMarkers
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var glitches = Program.ProgramConfig.GlitchMarkers
                    .Where(
                        m => m.Map.ToLower().StartsWith(MapFilename.ToLower())
                        && (Math.Abs((decimal)m.Lat - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)m.Lon - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(glitches, glitch =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_Glitch",
                        Latitude = (float)glitch.Lat,
                        Longitude = (float)glitch.Lon,
                        X = glitch.X,
                        Y = glitch.Y,
                        Z = glitch.Z,
                        InventoryId = null
                    });

                });
                if (!loadedStructures.IsEmpty) GlitchMarkers.AddRange(loadedStructures.ToList());


                //BeaverDams 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var beaverHouses = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("BeaverDam_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                 
                Parallel.ForEach(beaverHouses, house =>
                {
                    var loadedStructure = new ContentStructure()
                    {
                        ClassName = "ASV_BeaverDam",
                        Latitude = (float)house.Location.Latitude,
                        Longitude = (float)house.Location.Longitude,
                        X = house.Location.X,
                        Y = house.Location.Y,
                        Z = house.Location.Z,
                        InventoryId = IncludeGameStructureContent?addInventory(house.Inventory):0
                    };


                    loadedStructures.Add(loadedStructure);

                });
                if (!loadedStructures.IsEmpty) BeaverDams.AddRange(loadedStructures.ToList());


                //WyvernNests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var wyvernNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("WyvernNest_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();
                Parallel.ForEach(wyvernNests, nest =>
                {
                    var loadedStructure = new ContentStructure()
                    {
                        ClassName = "ASV_WyvernNest",
                        Latitude = (float)nest.Location.Latitude,
                        Longitude = (float)nest.Location.Longitude,
                        X = nest.Location.X,
                        Y = nest.Location.Y,
                        Z = nest.Location.Z,
                        InventoryId = 0
                    };

                    //check for egg and create inventory if found
                    ArkItem fertileEgg = gd.Items.FirstOrDefault(
                        i => i.ClassName.ToLower().Contains("egg") 
                                && i.OwnerInventoryId == null 
                                && i.OwnerContainerId == null 
                                &&i.Location!=null
                                && i.Location.Latitude.Value.ToString("0.00").Equals(nest.Location.Latitude.Value.ToString("0.00")) 
                                && i.Location.Longitude.Value.ToString("0.00").Equals(nest.Location.Longitude.Value.ToString("0.00")));
                    if (fertileEgg != null)
                    {
                        ArkItem[] items = new ArkItem[] { fertileEgg };
                        fertileEgg.CustomName = fertileEgg.CustomDescription.Replace("\n", Environment.NewLine);
                        loadedStructure.InventoryId = addInventory(items);
                    }


                    loadedStructures.Add(loadedStructure);

                });
                if (!loadedStructures.IsEmpty) WyvernNests.AddRange(loadedStructures.ToList());


                //DrakeNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var drakeNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("RockDrakeNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();
                
                Parallel.ForEach (drakeNests,  nest => 
                {
                    var loadedStructure = new ContentStructure()
                    {
                        ClassName = "ASV_DrakeNest",
                        Latitude = (float)nest.Location.Latitude,
                        Longitude = (float)nest.Location.Longitude,
                        X = nest.Location.X,
                        Y = nest.Location.Y,
                        Z = nest.Location.Z,
                        InventoryId =0
                    };

                    //check for egg and create inventory if found
                    ArkItem fertileEgg = gd.Items.FirstOrDefault<ArkItem>(
                        i => i.ClassName.ToLower().Contains("egg")
                                && i.OwnerInventoryId == null
                                && i.OwnerContainerId == null
                                && i.Location!=null
                                && i.Location.Latitude.GetValueOrDefault(0).ToString("0.00").Equals(nest.Location.Latitude.GetValueOrDefault(0).ToString("0.00"))
                                && i.Location.Longitude.GetValueOrDefault(0).ToString("0.00").Equals(nest.Location.Longitude.GetValueOrDefault(0).ToString("0.00")));
                    if (fertileEgg != null)
                    {
                        fertileEgg.CustomName = fertileEgg.CustomDescription.Replace("\n", Environment.NewLine);

                        ArkItem[] items = new ArkItem[] { fertileEgg };
                        loadedStructure.InventoryId = addInventory(items);
                    }

                    loadedStructures.Add(loadedStructure);

                });
                if (!loadedStructures.IsEmpty) DrakeNests.AddRange(loadedStructures.ToList());


                //Deino nests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var deinoNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("DeinonychusNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(deinoNests, nest =>
                {
                    var loadedStructure = new ContentStructure()
                    {
                        ClassName = "ASV_DeinoNest",
                        Latitude = (float)nest.Location.Latitude,
                        Longitude = (float)nest.Location.Longitude,
                        X = nest.Location.X,
                        Y = nest.Location.Y,
                        Z = nest.Location.Z,
                        InventoryId = 0
                    };

                    //check for egg and create inventory if found
                    ArkItem fertileEgg = gd.Items.FirstOrDefault(
                        i => i.ClassName.ToLower().Contains("egg")
                                && i.OwnerInventoryId == null
                                && i.OwnerContainerId == null
                                && i.Location != null
                                && i.Location.Latitude.Value.ToString("0.00").Equals(nest.Location.Latitude.Value.ToString("0.00"))
                                && i.Location.Longitude.Value.ToString("0.00").Equals(nest.Location.Longitude.Value.ToString("0.00")));
                    if (fertileEgg != null)
                    {
                        ArkItem[] items = new ArkItem[] { fertileEgg };
                        fertileEgg.CustomName = fertileEgg.CustomDescription.Replace("\n", Environment.NewLine);

                        loadedStructure.InventoryId = addInventory(items);
                    }

                    loadedStructures.Add(loadedStructure);

                });
                if (!loadedStructures.IsEmpty) DeinoNests.AddRange(loadedStructures.ToList());

                //MagmaNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var magmaNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("CherufeNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(magmaNests, nest =>
                {
                    
                    var loadedStructure = new ContentStructure()
                    {
                        ClassName = "ASV_MagmaNest",
                        Latitude = (float)nest.Location.Latitude,
                        Longitude = (float)nest.Location.Longitude,
                        X = nest.Location.X,
                        Y = nest.Location.Y,
                        Z = nest.Location.Z,
                        InventoryId =0
                    };

                    //check for egg and create inventory if found
                    ArkItem fertileEgg = gd.Items.FirstOrDefault(
                        i => i.ClassName.ToLower().Contains("egg")
                                && i.OwnerInventoryId == null
                                && i.OwnerContainerId == null
                                && i.Location != null
                                && i.Location.Latitude.Value.ToString("0.00").Equals(nest.Location.Latitude.Value.ToString("0.00"))
                                && i.Location.Longitude.Value.ToString("0.00").Equals(nest.Location.Longitude.Value.ToString("0.00")));
                    if (fertileEgg != null)
                    {
                        ArkItem[] items = new ArkItem[] { fertileEgg };
                        fertileEgg.CustomName = fertileEgg.CustomDescription.Replace("\n", Environment.NewLine);

                        loadedStructure.InventoryId = addInventory(items);
                    }

                    loadedStructures.Add(loadedStructure);

                });
                if (!loadedStructures.IsEmpty) MagmaNests.AddRange(loadedStructures.ToList());


                //OilVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var oilVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("OilVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(oilVeins, vein =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_OilVein",
                        Latitude = (float)vein.Location.Latitude,
                        Longitude = (float)vein.Location.Longitude,
                        X = vein.Location.X,
                        Y = vein.Location.Y,
                        Z = vein.Location.Z
                    });
                });
                if (!loadedStructures.IsEmpty) OilVeins.AddRange(loadedStructures.ToList());

                //WaterVeins 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var waterVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("WaterVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(waterVeins, vein =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_WaterVein",
                        Latitude = (float)vein.Location.Latitude,
                        Longitude = (float)vein.Location.Longitude,
                        X = vein.Location.X,
                        Y = vein.Location.Y,
                        Z = vein.Location.Z
                    });
                });
                if (!loadedStructures.IsEmpty) WaterVeins.AddRange(loadedStructures.ToList());

                //GasVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var gasVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("GasVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(gasVeins, vein =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_GasVein",
                        Latitude = (float)vein.Location.Latitude,
                        Longitude = (float)vein.Location.Longitude,
                        X = vein.Location.X,
                        Y = vein.Location.Y,
                        Z = vein.Location.Z
                    });
                });
                if (!loadedStructures.IsEmpty) GasVeins.AddRange(loadedStructures.ToList());

                //Artifacts 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var artifacts = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("ArtifactCrate_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(artifacts, artifact =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_Artifact",
                        Latitude = (float)artifact.Location.Latitude,
                        Longitude = (float)artifact.Location.Longitude,
                        X = artifact.Location.X,
                        Y = artifact.Location.Y,
                        Z = artifact.Location.Z
                    });
                });
                if (!loadedStructures.IsEmpty) Artifacts.AddRange(loadedStructures.ToList());

                //PlantZ
                //Artifacts 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var plants = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("Structure_PlantSpeciesZ")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(plants, plant =>
                {
                    loadedStructures.Add(new ContentStructure()
                    {
                        ClassName = "ASV_PlantZ",
                        Latitude = (float)plant.Location.Latitude,
                        Longitude = (float)plant.Location.Longitude,
                        X = plant.Location.X,
                        Y = plant.Location.Y,
                        Z = plant.Location.Z
                    });
                });
                if (!loadedStructures.IsEmpty) PlantZ.AddRange(loadedStructures.ToList());

            }


            if (IncludeWild)
            {
                //WildCreatures
                ConcurrentBag<ContentWildCreature> loadedWilds = new ConcurrentBag<ContentWildCreature>();

                var wilds = gd.WildCreatures
                .Where(w =>
                    (Math.Abs((decimal)w.Location.Latitude - FilterLatitude) <= FilterRadius)
                    && (Math.Abs((decimal)w.Location.Longitude - FilterLongitude) <= FilterRadius)
                ).ToList();

                Parallel.ForEach(wilds, wild =>
                {
                    loadedWilds.Add(new ContentWildCreature()
                    {
                        Id = (long)wild.Id,
                        ClassName = wild.ClassName,
                        BaseLevel = wild.BaseLevel,
                        Gender = wild.Gender.ToString(),
                        X = wild.Location.X,
                        Y = wild.Location.Y,
                        Z = wild.Location.Z,
                        Longitude = wild.Location.Longitude,
                        Latitude = wild.Location.Latitude,
                        Colors = wild.Colors,
                        BaseStats = wild.BaseStats
                    });


                });

                if (!loadedWilds.IsEmpty) WildCreatures.AddRange(loadedWilds.ToList());

            }



            if (IncludeTribesPlayers)
            {
                ConcurrentBag<ContentTribe> loadedTribes = new ConcurrentBag<ContentTribe>();

                //Tribes 
                var tribes = gd.Tribes
                    .Where(t =>
                        t.Id == ExportedForTribe || ExportedForTribe == 0
                    ).ToList();

                Parallel.ForEach(tribes, tribe =>
                {
                    ContentTribe loadedTribe = new ContentTribe()
                    {
                        TribeId = tribe.Id,
                        LastActive = tribe.LastActiveTime,
                        TribeName = tribe.Name,
                        Logs = tribe.Logs,
                        Players = new List<ContentPlayer>(),
                        Structures = new List<ContentStructure>(),
                        Tames = new List<ContentTamedCreature>()                        
                    };

                    var players = tribe.Members
                        .Where(p =>
                            (p.Id == ExportedForPlayer || ExportedForPlayer == 0)
                            &&  (p.Location!=null 
                                    && (Math.Abs((decimal)p.Location.Latitude - FilterLatitude) <= FilterRadius)
                                    && (Math.Abs((decimal)p.Location.Longitude - FilterLongitude) <= FilterRadius)
                                )
                        ).ToList();

                    ConcurrentBag<ContentPlayer> loadedPlayers = new ConcurrentBag<ContentPlayer>();
                    Parallel.ForEach(players, player =>
                    {
                        loadedPlayers.Add(new ContentPlayer()
                        {
                            Id = player.Id,
                            CharacterName = player.CharacterName,
                            LastActive = player.LastActiveTime,
                            Latitude = player.Location.Latitude.GetValueOrDefault(0),
                            Longitude = player.Location.Longitude.GetValueOrDefault(0),
                            Level = player.CharacterLevel,
                            Name = player.Name,
                            Gender = player.Gender.ToString(),
                            SteamId = player.SteamId,
                            Stats = player.Stats,
                            X = player.Location.X,
                            Y = player.Location.Y,
                            Z = player.Location.Z,
                            InventoryId = addInventory(player.Inventory)
                        });
                    
                    });
                    if (!loadedPlayers.IsEmpty) loadedTribe.Players.AddRange(loadedPlayers.ToList());

                    loadedTribes.Add(loadedTribe);
                });


                var soloPlayers = gd.Players
                    .Where(p =>
                        p.TribeId.HasValue
                        && (p.TribeId == 0 || p.TribeId == p.Id)
                        && (ExportedForPlayer == 0 || p.Id == ExportedForPlayer)
                        && (Math.Abs((decimal)p.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)p.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                Parallel.ForEach(soloPlayers, player =>
                {

                    //"solo" tribe, all players must be associated with a tribe for ASV.
                    var soloTribe = new ContentTribe()
                    {
                        TribeId = player.Id,
                        TribeName = $"Tribe of {player.CharacterName}",
                        LastActive = player.LastActiveTime,
                        Logs = new string[0],
                        Players = new List<ContentPlayer>(),
                        Structures = new List<ContentStructure>(),
                        Tames = new List<ContentTamedCreature>()
                    };

                    ContentPlayer soloPlayer = new ContentPlayer()
                    {
                        Id = player.Id,
                        CharacterName = player.CharacterName,
                        LastActive = player.LastActiveTime,
                        Latitude = player.Location.Latitude,
                        Longitude = player.Location.Longitude,
                        Level = player.CharacterLevel,
                        Name = player.Name,
                        Gender = player.Gender.ToString(),
                        SteamId = player.SteamId,
                        Stats = player.Stats,
                        X = player.Location.X,
                        Y = player.Location.Y,
                        Z = player.Location.Z,
                        InventoryId = addInventory(player.Inventory)
                    };
                    soloTribe.Players.Add(soloPlayer);

                    //structures


                    loadedTribes.Add(soloTribe);

                });

                //[Unclaimed Bucket]
                var unclaimedTribe = new ContentTribe()
                {
                    TribeId = 2000000000,
                    TribeName = $"[Unclaimed Creatures]",
                    LastActive = DateTime.Now,
                    Logs = new string[0],
                    Players = new List<ContentPlayer>(),
                    Structures = new List<ContentStructure>(),
                    Tames = new List<ContentTamedCreature>()
                };
                loadedTribes.Add(unclaimedTribe);

                if (!loadedTribes.IsEmpty) Tribes.AddRange(loadedTribes.ToList());
            }

            
            //player structures            
            Parallel.ForEach(Tribes, tribe =>
            {

                if (IncludePlayerStructures)
                {

                    ConcurrentBag<ContentStructure> loadedStructures = new ConcurrentBag<ContentStructure>();

                    var tribeStructures = gd.Structures
                    .Where(s =>
                            s.TargetingTeam.HasValue && (s.TargetingTeam == tribe.TribeId)
                            && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                            && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                        ).ToList();

                    //Tribe Structures
                    Parallel.ForEach(tribeStructures, structure =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = structure.ClassName,
                            Latitude = structure.Location.Latitude,
                            Longitude = structure.Location.Longitude,
                            X = structure.Location.X,
                            Y = structure.Location.Y,
                            Z = structure.Location.Z,
                            TargetingTeam = tribe.TribeId,
                            InventoryId = addInventory(structure.Inventory)
                        });
                    });

                    //add rafts which for some reason are under "Tamed" in the toolkit.
                    var rafts = gd.Rafts
                        .Where(t =>
                            (t.TargetingTeam == tribe.TribeId || tribe.Players.Any(p => p.Id == t.OwningPlayerId || p.Id == t.ImprinterPlayerDataId))
                            && (Math.Abs((decimal)t.Location.Latitude - FilterLatitude) <= FilterRadius)
                            && (Math.Abs((decimal)t.Location.Longitude - FilterLongitude) <= FilterRadius)
                            
                        ).ToList();
                    Parallel.ForEach(rafts, structure =>
                    {
                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = structure.ClassName,
                            Latitude = structure.Location.Latitude,
                            Longitude = structure.Location.Longitude,
                            X = structure.Location.X,
                            Y = structure.Location.Y,
                            Z = structure.Location.Z,
                            InventoryId = addInventory(structure.Inventory)
                        });
                    });


                    if (!loadedStructures.IsEmpty) tribe.Structures.AddRange(loadedStructures.ToList());
                }

                if (IncludeTamed)
                {
                    //tamed
                    ConcurrentBag<ContentTamedCreature> loadedTames = new ConcurrentBag<ContentTamedCreature>();
                    var tamed = gd.NoRafts
                        .Where(t =>
                            (t.TargetingTeam == tribe.TribeId || tribe.Players.Any(p=>p.Id == t.OwningPlayerId || p.Id == t.ImprinterPlayerDataId))
                            && (Math.Abs((decimal)t.Location.Latitude - FilterLatitude) <= FilterRadius)
                            && (Math.Abs((decimal)t.Location.Longitude - FilterLongitude) <= FilterRadius)
                        ).ToList();

                    //Tamed
                    Parallel.ForEach(tamed, tame => 
                    { 
                        var loadedTame = new ContentTamedCreature()
                        {
                            Id = (long)tame.Id,
                            ClassName = tame.ClassName,
                            BaseLevel = tame.BaseLevel,
                            Gender = tame.Gender.ToString(),
                            ImprintedPlayerId = (int)tame.ImprinterPlayerDataId.GetValueOrDefault(0),
                            ImprinterName = tame.ImprinterName,
                            ImprintQuality = (decimal)tame.DinoImprintingQuality.GetValueOrDefault(0),
                            Level = tame.Level,
                            Name = tame.Name,
                            TamedOnServerName = tame.TamedOnServerName,
                            TribeName = tame.TribeName,
                            TamerName = tame.TamerName,
                            TargetingTeam = tame.TargetingTeam,
                            IsCryo = tame.IsCryo,
                            IsVivarium = tame.IsVivarium,
                            Latitude = tame.Location.Latitude.GetValueOrDefault(0),
                            Longitude = tame.Location.Longitude.GetValueOrDefault(0),
                            X = tame.Location.X,
                            Y = tame.Location.Y,
                            Z = tame.Location.Z,
                            Colors = tame.Colors,
                            BaseStats = tame.BaseStats,
                            TamedStats = tame.TamedStats,
                            RandomMutationsFemale = tame.RandomMutationsFemale,
                            RandomMutationsMale = tame.RandomMutationsMale,
                            InventoryId = addInventory(tame.Inventory)
                        };

                        loadedTames.Add(loadedTame);

                    });

                    if (!loadedTames.IsEmpty) tribe.Tames.AddRange(loadedTames.ToList());
                }

            });

            //Dropped items
            ConcurrentBag<ContentDroppedItem> loadedDroppedItems = new ConcurrentBag<ContentDroppedItem>();
            var droppedItems = gd.DroppedItems
                .Where(i =>
                    (i.DroppedByPlayerId.GetValueOrDefault(0) == 0 || i.DroppedByPlayerId.GetValueOrDefault(0) == ExportedForPlayer || ExportedForPlayer == 0)
                    && (Math.Abs((decimal)i.Location.Latitude - FilterLatitude) <= FilterRadius)
                    && (Math.Abs((decimal)i.Location.Longitude - FilterLongitude) <= FilterRadius)
                ).ToList();

            Parallel.ForEach(droppedItems, droppedItem =>
            {
                loadedDroppedItems.Add(new ContentDroppedItem()
                {
                    ClassName = droppedItem.ClassName,
                    DroppedByName = droppedItem.DroppedByName,
                    DroppedByPlayerId = droppedItem.DroppedByPlayerId.GetValueOrDefault(0),
                    IsBlueprint = droppedItem.IsBlueprint,
                    IsEngram = droppedItem.IsEngram,
                    IsDeathCache=false,
                    Latitude = droppedItem.Location.Latitude.GetValueOrDefault(0),
                    Longitude = droppedItem.Location.Longitude.GetValueOrDefault(0),
                    X = droppedItem.Location.X,
                    Y = droppedItem.Location.Y,
                    Z = droppedItem.Location.Z,
                    InventoryId = null
                });

            });

            //Death cache bags
            var dropBags = gd.PlayerDeathCache
                .Where(i =>
                    (i.TargetingTeam == 0 || (i.TargetingTeam == ExportedForTribe || ExportedForTribe==0))
                    && (Math.Abs((decimal)i.Location.Latitude - FilterLatitude) <= FilterRadius)
                    && (Math.Abs((decimal)i.Location.Longitude - FilterLongitude) <= FilterRadius)
                ).ToList();

            Parallel.ForEach(dropBags, droppedItem =>
            {
                loadedDroppedItems.Add(new ContentDroppedItem()
                {
                    ClassName = droppedItem.ClassName,
                    DroppedByName = droppedItem.OwnerName,
                    DroppedByPlayerId = droppedItem.OwningPlayerId.GetValueOrDefault(0),
                    IsBlueprint = false,
                    IsEngram = false,
                    IsDeathCache = true,
                    Latitude = droppedItem.Location.Latitude.GetValueOrDefault(0),
                    Longitude = droppedItem.Location.Longitude.GetValueOrDefault(0),
                    X = droppedItem.Location.X,
                    Y = droppedItem.Location.Y,
                    Z = droppedItem.Location.Z,
                    InventoryId = addInventory(droppedItem.Inventory)
                });

            });


            if (!loadedDroppedItems.IsEmpty) DroppedItems.AddRange(loadedDroppedItems.ToList());

            //Inventories - should have been populated by the previous steps through structure/tribe/player/tame/dropped items loading

            
        }

        private int addInventory(ArkItem[] items)
        {
            if (items.Length == 0) return 0; //no point creating one if no content

            int nextId = Inventories.Count + 1;
            ConcurrentBag<ContentItem> invItems = new ConcurrentBag<ContentItem>();
            Parallel.ForEach(items, item =>
            {
                invItems.Add(new ContentItem()
                {
                    ClassName = item.ClassName,
                    CustomName = item.CustomName,
                    CraftedByPlayer = item.CraftedPlayerName,
                    CraftedByTribe = item.CraftedTribeName,
                    IsBlueprint = item.IsBlueprint,
                    Quantity = (int)item.Quantity
                });

            });

            Inventories.Add(new ContentInventory()
            {
                InventoryId = nextId,
                Items = invItems.ToList()
            });

            return nextId;
        }


        private string GetOrphansTribeName(ArkTamedCreature tame)
        {
            if(tame.TribeName != null && tame.TribeName.Length >0)
            {
                return tame.TribeName;
            }

            if (tame.ImprinterName != null && tame.ImprinterName.Length > 0)
            {
                return tame.ImprinterName;
            }

            if (tame.OwningPlayerName  != null && tame.OwningPlayerName.Length > 0)
            {
                return tame.OwningPlayerName;
            }
            return "";
        }

        private void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        private byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        private string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }



    }
}
