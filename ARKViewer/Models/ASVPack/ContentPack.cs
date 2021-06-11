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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ARKViewer.Models.ASVPack
{
    [DataContract]
    public class ContentPack
    {
        [DataMember] public string MapFilename { get; set; } = "TheIsland.ark";
        [DataMember] public DateTime ContentDate { get; internal set; } = DateTime.Now;
        [DataMember] public long ExportedForTribe { get; set; } = 0;
        [DataMember] public long ExportedForPlayer { get; set; } = 0;
        [DataMember] public DateTime ExportedTimestamp { get; set; } = DateTime.Now;
        [DataMember] public List<ContentStructure> TerminalMarkers { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> GlitchMarkers { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> ChargeNodes { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> BeaverDams { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> WyvernNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> DrakeNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> MagmaNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> DeinoNests { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> OilVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> WaterVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> GasVeins { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> Artifacts { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentStructure> PlantZ { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentInventory> Inventories { get; set; } = new List<ContentInventory>();
        [DataMember] public List<ContentDroppedItem> DroppedItems { get; set; } = new List<ContentDroppedItem>();
        [DataMember] public List<ContentWildCreature> WildCreatures { get; set; } = new List<ContentWildCreature>();
        [DataMember] public List<ContentTribe> Tribes { get; set; } = new List<ContentTribe>();

        public List<ContentStructure> OrphanedStructures { get; set; } = new List<ContentStructure>();
        public List<ContentTamedCreature> OrphanedTames { get; set; } = new List<ContentTamedCreature>();

        bool IncludeGameStructures { get; set; } = true;
        bool IncludeGameStructureContent { get; set; } = true;
        bool IncludeTribesPlayers { get; set; } = true;
        bool IncludeTamed { get; set; } = true;
        bool IncludeWild { get; set; } = true;
        bool IncludePlayerStructures { get; set; } = true;
        bool IncludeDroppedItems { get; set; } = true;
        decimal FilterLatitude { get; set; } = 50;
        decimal FilterLongitude { get; set; } = 50;
        decimal FilterRadius { get; set; } = 100;

        ConcurrentBag<ContentInventory> inventoryBag = new ConcurrentBag<ContentInventory>();

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

        public ContentPack(ContentPack sourcePack, long selectedTribeId, long selectedPlayerId, decimal lat, decimal lon, decimal rad, bool includeGameStructures, bool includeGameStructureContent, bool includeTribesPlayers, bool includeTamed, bool includeWild, bool includePlayerStructures, bool includeDropped) : this()
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

            LoadPackData(sourcePack);

        }

        public ContentPack(ArkGameData gd, long selectedTribeId, long selectedPlayerId, decimal lat, decimal lon, decimal rad, bool includeGameStructures, bool includeGameStructureContent, bool includeTribesPlayers, bool includeTamed, bool includeWild, bool includePlayerStructures, bool includeDropped): this()
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

        public ContentPack(ArkGameData gd, int selectedTribeId, int selectedPlayerId, decimal lat, decimal lon, decimal rad): this(gd, selectedTribeId, selectedPlayerId, lat, lon, rad,true,true,true,true,true,true,true)
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

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void ExportPack(string fileName)
        {
           
            string filePath = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

            string jsonContent = JsonConvert.SerializeObject(this);
            var compressedContent = Zip(jsonContent);
            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);

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
                var mapDetectedTerminals = gd.Structures.Where(s => (s.ClassName.StartsWith("TributeTerminal_") || s.ClassName.Contains("CityTerminal_")) && s.Location!=null);
                if (mapDetectedTerminals != null)
                {
                    Parallel.ForEach(mapDetectedTerminals, terminal =>
                    {
                        var inventoryId = addInventory(terminal.Inventory);

                        loadedStructures.Add(new ContentStructure()
                        {
                            ClassName = "ASV_Terminal",
                            Latitude = terminal.Location!= null ? (float)terminal.Location.Latitude.GetValueOrDefault(0): 0,
                            Longitude = terminal.Location != null ? (float)terminal.Location.Longitude.GetValueOrDefault(0) : 0,
                            X = terminal.Location.X,
                            Y = terminal.Location.Y,
                            Z = terminal.Location.Z,
                            InventoryId = IncludeGameStructureContent ? inventoryId : 0
                        });

                    });
                }               


                //user defined terminals
                if (Program.ProgramConfig.TerminalMarkers != null)
                {
                    var mapTerminals = loadedStructures.ToList();
                    var terminals = Program.ProgramConfig.TerminalMarkers
                        .Where(m =>
                            m.Map.ToLower().StartsWith(MapFilename.ToLower())
                            //exclude any that match map detected terminal location
                            & !mapTerminals.Any(t => t.Latitude.ToString().StartsWith(m.Lat.ToString()) && t.Longitude.ToString().StartsWith(m.Lon.ToString()))
                        ).ToList();

                    if (terminals != null)
                    {
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
                    }
                    
                }
                
                
                if (!loadedStructures.IsEmpty) TerminalMarkers.AddRange(loadedStructures.ToList());


                //Charge nodes
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var chargeNodes = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("PrimalItem_PowerNodeCharge")
                        && s.Location!=null
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(chargeNodes!=null && chargeNodes.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) ChargeNodes.AddRange(loadedStructures.ToList());

                //GlitchMarkers
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var glitches = Program.ProgramConfig.GlitchMarkers
                    .Where(
                        m => m.Map.ToLower().StartsWith(MapFilename.ToLower())
                        && (Math.Abs((decimal)m.Lat - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)m.Lon - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(glitches!=null && glitches.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) GlitchMarkers.AddRange(loadedStructures.ToList());


                //BeaverDams 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var beaverHouses = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("BeaverDam_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(beaverHouses!=null && beaverHouses.Count > 0)
                {
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
                            InventoryId = IncludeGameStructureContent ? addInventory(house.Inventory) : 0
                        };


                        loadedStructures.Add(loadedStructure);

                    });
                }                 
                if (!loadedStructures.IsEmpty) BeaverDams.AddRange(loadedStructures.ToList());


                //WyvernNests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var wyvernNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("WyvernNest_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();
                if(wyvernNests!=null && wyvernNests.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) WyvernNests.AddRange(loadedStructures.ToList());


                //DrakeNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var drakeNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("RockDrakeNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();
                
                if(drakeNests!=null && drakeNests.Count > 0)
                {
                    Parallel.ForEach(drakeNests, nest =>
                    {
                        var loadedStructure = new ContentStructure()
                        {
                            ClassName = "ASV_DrakeNest",
                            Latitude = (float)nest.Location.Latitude,
                            Longitude = (float)nest.Location.Longitude,
                            X = nest.Location.X,
                            Y = nest.Location.Y,
                            Z = nest.Location.Z,
                            InventoryId = 0
                        };

                        //check for egg and create inventory if found
                        ArkItem fertileEgg = gd.Items.FirstOrDefault<ArkItem>(
                            i => i.ClassName.ToLower().Contains("egg")
                                    && i.OwnerInventoryId == null
                                    && i.OwnerContainerId == null
                                    && i.Location != null
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
                }             
                if (!loadedStructures.IsEmpty) DrakeNests.AddRange(loadedStructures.ToList());


                //Deino nests
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var deinoNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("DeinonychusNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(deinoNests!=null && deinoNests.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) DeinoNests.AddRange(loadedStructures.ToList());

                //MagmaNests 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var magmaNests = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("CherufeNest_C")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(magmaNests!=null && magmaNests.Count > 0)
                {
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
                }
                if (!loadedStructures.IsEmpty) MagmaNests.AddRange(loadedStructures.ToList());


                //OilVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var oilVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("OilVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(oilVeins!=null && oilVeins.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) OilVeins.AddRange(loadedStructures.ToList());

                //WaterVeins 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var waterVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("WaterVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(waterVeins!=null && waterVeins.Count > 0)
                {
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
                }
                if (!loadedStructures.IsEmpty) WaterVeins.AddRange(loadedStructures.ToList());

                //GasVeins  
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var gasVeins = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("GasVein_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(gasVeins!=null && gasVeins.Count > 0)
                {
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
                }                
                if (!loadedStructures.IsEmpty) GasVeins.AddRange(loadedStructures.ToList());

                //Artifacts 
                loadedStructures = new ConcurrentBag<ContentStructure>();
                var artifacts = gd.Structures
                    .Where(s =>
                        s.ClassName.StartsWith("ArtifactCrate_")
                        && (Math.Abs((decimal)s.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)s.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(artifacts!=null && artifacts.Count > 0)
                {
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
                }
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

                if(plants!=null && plants.Count > 0)
                {
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
                }                
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

                if(wilds!=null && wilds.Count > 0)
                {
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
                            BaseStats = wild.BaseStats,
                            Resources = wild.ProductionResources
                        });


                    });
                }
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

                if(tribes!=null && tribes.Count > 0)
                {
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
                                && (p.Location != null
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
                }
                
                var soloPlayers = gd.Players
                    .Where(p =>
                        (p.TribeId == null || (p.TribeId.HasValue && (p.TribeId == 0 || p.TribeId == p.Id)))
                        && (ExportedForPlayer == 0 || p.Id == ExportedForPlayer)
                        &! loadedTribes.Any(t=>t.Players.Any(tp => tp.Id == p.Id))
                        &&
                        (p.Location== null
                        || (
                                p.Location != null 
                                && (Math.Abs((decimal)p.Location.Latitude - FilterLatitude) <= FilterRadius)
                                && (Math.Abs((decimal)p.Location.Longitude - FilterLongitude) <= FilterRadius)
                           )
                        )
                    ).ToList();

                if(soloPlayers!=null && soloPlayers.Count > 0)
                {
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
                            Latitude = player.Location == null ? 0 : player.Location.Latitude,
                            Longitude = player.Location == null ? 0 : player.Location.Longitude,
                            Level = player.CharacterLevel,
                            Name = player.Name,
                            Gender = player.Gender.ToString(),
                            SteamId = player.SteamId,
                            Stats = player.Stats,
                            X = player.Location == null ? 0 : player.Location.X,
                            Y = player.Location == null ? 0 : player.Location.Y,
                            Z = player.Location == null ? 0 : player.Location.Z,
                            InventoryId = addInventory(player.Inventory)
                        };
                        soloTribe.Players.Add(soloPlayer);

                        loadedTribes.Add(soloTribe);

                    });
                }
                
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

                var orphanedTribe = new ContentTribe()
                {
                    TribeId = int.MinValue,
                    TribeName = $"[Abandoned]",
                    LastActive = DateTime.Now,
                    Logs = new string[0],
                    Players = new List<ContentPlayer>(),
                    Structures = new List<ContentStructure>(),
                    Tames = new List<ContentTamedCreature>()
                };
                loadedTribes.Add(orphanedTribe);

                if (!loadedTribes.IsEmpty) Tribes.AddRange(loadedTribes.ToList());
            }

            
            //player structures
            if(Tribes!=null && Tribes.Count > 0)
            {
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
                        if(tribeStructures!=null && tribeStructures.Count > 0)
                        {
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

                        }

                        //add rafts which for some reason are under "Tamed" in the toolkit.
                        var rafts = gd.Rafts
                            .Where(t =>
                                (t.TargetingTeam == tribe.TribeId)
                                && (Math.Abs((decimal)t.Location.Latitude - FilterLatitude) <= FilterRadius)
                                && (Math.Abs((decimal)t.Location.Longitude - FilterLongitude) <= FilterRadius)

                            ).ToList();

                        if(rafts!=null && rafts.Count > 0)
                        {
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
                        }

                        if (!loadedStructures.IsEmpty) tribe.Structures.AddRange(loadedStructures.ToList());
                    }

                    if (IncludeTamed)
                    {


                        //tamed
                        ConcurrentBag<ContentTamedCreature> loadedTames = new ConcurrentBag<ContentTamedCreature>();
                        var tamed = gd.NoRafts
                            .Where(t =>
                                (t.TargetingTeam == tribe.TribeId)
                                && (Math.Abs((decimal)t.Location.Latitude - FilterLatitude) <= FilterRadius)
                                && (Math.Abs((decimal)t.Location.Longitude - FilterLongitude) <= FilterRadius)
                            ).ToList();



                        //Tamed
                        if(tamed!=null && tamed.Count > 0)
                        {
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
                                    TribeName = tribe.TribeName,
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
                                    Resources = tame.ProductionResources,
                                    InventoryId = addInventory(tame.Inventory)
                                };

                                if ((tame.DinoAncestors != null && tame.DinoAncestors.Length > 0))
                                {
                                    var parents = tame.DinoAncestors.First();
                                    loadedTame.MotherId = (long)parents.FemaleId;
                                    loadedTame.MotherName = parents.FemaleName;
                                    loadedTame.FatherId = (long)parents.MaleId;
                                    loadedTame.FatherName = parents.MaleName;
                                }


                                loadedTames.Add(loadedTame);

                            });
                        }

                        if (!loadedTames.IsEmpty) tribe.Tames.AddRange(loadedTames.ToList());


                        

                    }

                });
            }


            //hold new abandoned tribes to create fake ones until they return to server.
            ConcurrentBag<ContentTribe> abandonedTribes = new ConcurrentBag<ContentTribe>();

            //check for abandoned tames

            ConcurrentBag<ContentTamedCreature> abandonedTames = new ConcurrentBag<ContentTamedCreature>();
            var abandoned = gd.TamedCreatures.Where(x => x.TargetingTeam!=0 &!Tribes.Any(t => t.TribeId == x.TargetingTeam)).ToList();
            if (abandoned != null && abandoned.Count > 0)
            {
                //try and create ContentTribe from abandoned tame data
                var missingSoloTribes = abandoned.Where(a=>a.TribeName == null && a.TargetingTeam!=0).GroupBy(x => x.TargetingTeam).Where(g=>g.Key!=0).Select(s=> new { TribeId = s.Key, TribeName = $"Tribe of {s.First().TamerName}"}).Distinct().ToList();
                if(missingSoloTribes!=null && missingSoloTribes.Count > 0)
                {
                    missingSoloTribes.ForEach(x =>
                    {
                        var newTribe = new ContentTribe()
                        {
                            TribeId = x.TribeId,
                            TribeName = x.TribeName,
                            LastActive = DateTime.MinValue,
                            Players = new List<ContentPlayer>(),
                            Structures = new List<ContentStructure>(),
                            Tames = new List<ContentTamedCreature>(),
                            Logs = Array.Empty<string>()
                        };


                        //add the solo player
                        abandonedTribes.Add(newTribe);
                    });

                }
                
                var missingTribes = abandoned.Where(a=>a.TribeName!=null && a.TargetingTeam !=0  &!abandonedTribes.Any(t => t.TribeId == a.TargetingTeam)).GroupBy(x => x.TargetingTeam).Select(s => new { TribeId = s.Key, TribeName = s.First().TribeName }).Distinct().ToList();
                if (missingTribes != null && missingTribes.Count > 0)
                {
                    missingTribes.ForEach(x =>
                    {
                        var newTribe = new ContentTribe()
                        {
                            TribeId = x.TribeId,
                            TribeName = x.TribeName,
                            LastActive = DateTime.MinValue,
                            Players = new List<ContentPlayer>(),
                            Structures = new List<ContentStructure>(),
                            Tames = new List<ContentTamedCreature>(),
                            Logs = Array.Empty<string>()
                        };


                        //add the solo player
                        abandonedTribes.Add(newTribe);
                    });
                }


                //populate ASV content for tame as abandoned
                Parallel.ForEach(abandoned, tame =>
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
                        TargetingTeam = int.MinValue,
                        AbandonedTeam = tame.TargetingTeam,
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

                    if ((tame.DinoAncestors != null && tame.DinoAncestors.Length > 0))
                    {
                        var parents = tame.DinoAncestors.First();
                        loadedTame.MotherId = (long)parents.FemaleId;
                        loadedTame.MotherName = parents.FemaleName;
                        loadedTame.FatherId = (long)parents.MaleId;
                        loadedTame.FatherName = parents.MaleName;
                    }
                    
                    abandonedTames.Add(loadedTame);
                });



                if (!abandonedTames.IsEmpty) Tribes.First(t=>t.TribeId == int.MinValue).Tames.AddRange(abandonedTames.ToList());
            }

            //check for abandoned
            ConcurrentBag<ContentStructure> abandonedStructures = new ConcurrentBag<ContentStructure>();
            var derelics = gd.Structures.Where(x => x.TargetingTeam !=null && x.TargetingTeam != 0 &!Tribes.Any(t => t.TribeId == x.TargetingTeam &! abandonedTribes.Any(a => t.TribeId == a.TribeId))).ToList();
            if (derelics != null && derelics.Count > 0)
            {

                //try and create ContentTribe from abandoned tame data
                var missingSoloTribes = derelics.Where(a => a.OwnerName == null && a.TargetingTeam.GetValueOrDefault(0) !=0 &!abandonedTribes.Any(t => t.TribeId == a.TargetingTeam)).GroupBy(x => x.TargetingTeam).Select(s => new { TribeId = (long)s.Key.GetValueOrDefault(0), TribeName = $"Tribe of {s.First().OwningPlayerName}" }).Distinct().ToList();
                if (missingSoloTribes != null && missingSoloTribes.Count > 0)
                {
                    missingSoloTribes.ForEach(x =>
                    {
                        var newTribe = new ContentTribe()
                        {
                            TribeId = x.TribeId,
                            TribeName = x.TribeName,
                            LastActive = DateTime.MinValue,
                            Players = new List<ContentPlayer>(),
                            Structures = new List<ContentStructure>(),
                            Tames = new List<ContentTamedCreature>(),
                            Logs = Array.Empty<string>()
                        };

                        //add the solo player
                        abandonedTribes.Add(newTribe);
                    });

                }

                var missingTribes = derelics.Where(a => a.OwnerName != null && a.TargetingTeam.GetValueOrDefault(0)!=0  &! abandonedTribes.Any(t=>t.TribeId == a.TargetingTeam)).GroupBy(x => x.TargetingTeam).Select(s => new { TribeId = (long)s.Key.GetValueOrDefault(0), TribeName = s.First().OwnerName}).Distinct().ToList();
                if (missingTribes != null && missingTribes.Count > 0)
                {
                    missingTribes.ForEach(x =>
                    {
                        var newTribe = new ContentTribe()
                        {
                            TribeId = x.TribeId,
                            TribeName = x.TribeName,
                            LastActive = DateTime.MinValue,
                            Players = new List<ContentPlayer>(),
                            Structures = new List<ContentStructure>(),
                            Tames = new List<ContentTamedCreature>(),
                            Logs = Array.Empty<string>()
                        };


                        //add the solo player
                        abandonedTribes.Add(newTribe);
                    });
                }


                Parallel.ForEach(derelics, structure =>
                {
                    ContentStructure loadedStructure = new ContentStructure()
                    {
                        ClassName = structure.ClassName,
                        Latitude = structure.Location.Latitude,
                        Longitude = structure.Location.Longitude,
                        X = structure.Location.X,
                        Y = structure.Location.Y,
                        Z = structure.Location.Z,
                        TargetingTeam = int.MinValue,
                        AbandonedTeam = structure.TargetingTeam.GetValueOrDefault(0),
                        InventoryId = addInventory(structure.Inventory)
                    };

                    abandonedStructures.Add(loadedStructure);
                });

                if (!abandonedStructures.IsEmpty) Tribes.First(t => t.TribeId == int.MinValue).Structures.AddRange(abandonedStructures.ToList());
            }


            if(abandonedTribes!=null && abandonedTribes.Count > 0)
            {
                var abandonedTribeList = abandonedTribes.ToList();
                abandonedTribeList.ForEach(x =>
                {
                    //find matching abandoned tames
                    var tribeTames = abandonedTames.Where(t => t.AbandonedTeam == x.TribeId).ToList();
                    if(tribeTames!=null && tribeTames.Count > 0)
                    {
                        tribeTames.ForEach(t => {
                            t.TargetingTeam = t.AbandonedTeam;
                            t.AbandonedTeam = 0;
                        });

                        //add to this tribe tames
                        x.Tames.AddRange(tribeTames.ToArray());

                        //remove from abandoned tribe tames
                        Tribes.First(t => t.TribeId == int.MinValue).Tames.RemoveAll(a => a.TargetingTeam == x.TribeId);
                    }

                    //now matching structures
                    var tribeStructures = abandonedStructures.Where(t => t.AbandonedTeam == x.TribeId).ToList();
                    if (tribeStructures != null && tribeStructures.Count > 0)
                    {
                        tribeStructures.ForEach(t => {
                            t.TargetingTeam = t.AbandonedTeam;
                            t.AbandonedTeam = 0;
                        });

                        //add to this tribe tames
                        x.Structures.AddRange(tribeStructures.ToArray());

                        //remove from abandoned tribe tames
                        Tribes.First(t => t.TribeId == int.MinValue).Structures.RemoveAll(a => a.AbandonedTeam == x.TribeId);
                    }
                });

                Tribes.AddRange(abandonedTribeList);

                var abandonedTribe = Tribes.First(t => t.TribeId == int.MinValue);
                if (abandonedTribe.Tames.Count == 0 && abandonedTribe.Structures.Count == 0) Tribes.Remove(abandonedTribe);
            }

            if (IncludeDroppedItems)
            {
                //Dropped items
                ConcurrentBag<ContentDroppedItem> loadedDroppedItems = new ConcurrentBag<ContentDroppedItem>();
                var droppedItems = gd.DroppedItems
                    .Where(i =>
                        (i.DroppedByPlayerId.GetValueOrDefault(0) == 0 || i.DroppedByPlayerId.GetValueOrDefault(0) == ExportedForPlayer || ExportedForPlayer == 0)
                        && (Math.Abs((decimal)i.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)i.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();

                if(droppedItems!=null && droppedItems.Count > 0)
                {
                    Parallel.ForEach(droppedItems, droppedItem =>
                    {
                        loadedDroppedItems.Add(new ContentDroppedItem()
                        {
                            ClassName = droppedItem.ClassName,
                            DroppedByName = droppedItem.DroppedByName,
                            DroppedByPlayerId = droppedItem.DroppedByPlayerId.GetValueOrDefault(0),
                            IsBlueprint = droppedItem.IsBlueprint,
                            IsEngram = droppedItem.IsEngram,
                            IsDeathCache = false,
                            Latitude = droppedItem.Location.Latitude.GetValueOrDefault(0),
                            Longitude = droppedItem.Location.Longitude.GetValueOrDefault(0),
                            X = droppedItem.Location.X,
                            Y = droppedItem.Location.Y,
                            Z = droppedItem.Location.Z,
                            InventoryId = null
                        });

                    });
                }
                

                //Death cache bags
                var dropBags = gd.PlayerDeathCache
                    .Where(i =>
                        (i.TargetingTeam == 0 || (i.TargetingTeam == ExportedForTribe || ExportedForTribe == 0))
                        && (Math.Abs((decimal)i.Location.Latitude - FilterLatitude) <= FilterRadius)
                        && (Math.Abs((decimal)i.Location.Longitude - FilterLongitude) <= FilterRadius)
                    ).ToList();


                if(dropBags!=null && dropBags.Count > 0)
                {
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
                }
                if (!loadedDroppedItems.IsEmpty) DroppedItems.AddRange(loadedDroppedItems.ToList());
                if (inventoryBag != null && inventoryBag.Count > 0) Inventories.AddRange(inventoryBag.ToList());
            }
        }

        private void LoadPackData(ContentPack pack)
        {
            //load content pack from Ark savegame 
            MapFilename = pack.MapFilename;
            ContentDate = pack.ContentDate;

            if (IncludeGameStructures)
            {
                //all locations
                TerminalMarkers = pack.TerminalMarkers;
                                
                
                
                //possibly location restricted
                ChargeNodes = pack.ChargeNodes.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                GlitchMarkers = pack.GlitchMarkers.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                BeaverDams = pack.BeaverDams.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                WyvernNests = pack.WyvernNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                DrakeNests = pack.DrakeNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                DeinoNests = pack.DeinoNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                MagmaNests = pack.MagmaNests.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                OilVeins = pack.OilVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                WaterVeins = pack.WaterVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                GasVeins = pack.GasVeins.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 
                Artifacts = pack.Artifacts.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
                PlantZ = pack.PlantZ.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList(); 

                if (!IncludeGameStructureContent)
                {
                    //remove linked inventory, and then unassign
                    BeaverDams.ForEach(x =>
                    {
                        if(x.InventoryId > 0)
                        {
                            if(Inventories!=null && Inventories.Count > 0)
                            {
                                Inventories.RemoveAll(i => i.InventoryId == x.InventoryId);
                            }
                            x.InventoryId = 0;
                        }
                        
                    });
                    WyvernNests.ForEach(x =>
                    {
                        if (x.InventoryId > 0)
                        {
                            if (Inventories != null && Inventories.Count > 0)
                            {
                                Inventories.RemoveAll(i => i.InventoryId == x.InventoryId);
                            }
                            x.InventoryId = 0;
                        }

                    });
                    DrakeNests.ForEach(x =>
                    {
                        if (x.InventoryId > 0)
                        {
                            if (Inventories != null && Inventories.Count > 0)
                            {
                                Inventories.RemoveAll(i => i.InventoryId == x.InventoryId);
                            }
                            x.InventoryId = 0;
                        }

                    });
                    DeinoNests.ForEach(x =>
                    {
                        if (x.InventoryId > 0)
                        {
                            if (Inventories != null && Inventories.Count > 0)
                            {
                                Inventories.RemoveAll(i => i.InventoryId == x.InventoryId);
                            }
                            x.InventoryId = 0;
                        }

                    });
                    MagmaNests.ForEach(x =>
                    {
                        if (x.InventoryId > 0)
                        {
                            if (Inventories != null && Inventories.Count > 0)
                            {
                                Inventories.RemoveAll(i => i.InventoryId == x.InventoryId);
                            }
                            x.InventoryId = 0;
                        }

                    });


                }

            }


            if (IncludeWild)
            {
                //WildCreatures
                WildCreatures = pack.WildCreatures.Where(w =>
                                                            (Math.Abs((decimal)w.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)w.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                        ).ToList();
            }

            if (IncludeTribesPlayers)
            {
                //Tribes 
                Tribes = pack.Tribes.Where(t => (t.TribeId == ExportedForTribe || ExportedForTribe == 0) || (t.Players.Any(p=>p.Id == ExportedForPlayer && ExportedForPlayer!=0))).ToList();
            }

            //player structures
            if (Tribes != null && Tribes.Count > 0)
            {
                if (!IncludePlayerStructures)
                {
                    //remove structures, not included in the filter
                    Tribes.ForEach(t => {
                        //clear down inventories
                        Inventories.RemoveAll(i => t.Structures.Any(s => s.InventoryId == i.InventoryId));

                        //then structures
                        t.Structures.Clear();
                    });
                }
                if (!IncludeTamed)
                {
                    Tribes.ForEach(t => {
                        //clear down inventories
                        Inventories.RemoveAll(i => t.Tames.Any(s => s.InventoryId == i.InventoryId));

                        //then tames
                        t.Tames.Clear();

                    });
                }

                if (ExportedForPlayer != 0)
                {
                    //specific player, dont give data for all tribe members
                    Tribes.ForEach(t => {
                            Inventories.RemoveAll(i=> t.Players.Any(p=>p.Id != ExportedForPlayer && p.InventoryId == i.InventoryId));
                            t.Players.RemoveAll(p => p.Id != ExportedForPlayer);
                        
                        });
                }
            }

            if (IncludeDroppedItems)
            {
                DroppedItems = pack.DroppedItems.Where(i =>
                                                            (i.DroppedByPlayerId == ExportedForPlayer || ExportedForPlayer == 0)
                                                            &&  (Math.Abs((decimal)i.Latitude.GetValueOrDefault(0) - FilterLatitude) <= FilterRadius)
                                                            && (Math.Abs((decimal)i.Longitude.GetValueOrDefault(0) - FilterLongitude) <= FilterRadius)
                                                      ).ToList();
            }
        }


        private long addInventory(ArkItem[] items)
        {
            if (items == null || items.Length == 0) return 0; //no point creating one if no content
            long nextId = inventoryBag.Count + 1;

            List<ContentItem> invItems = new List<ContentItem>();
            foreach(var item in items)
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

            };

            if (invItems == null || invItems.Count == 0) return 0;

            var invent = new ContentInventory()
            {
                InventoryId = nextId,
                Items = invItems
            };
            if (invent == null || invent.Items == null) return 0;

            inventoryBag.Add(invent);

            return nextId;
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
