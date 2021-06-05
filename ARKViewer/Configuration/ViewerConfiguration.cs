﻿using ARKViewer.CustomNameMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Configuration
{


    public enum ViewerModes: int
    {
        Mode_SinglePlayer = 0,
        Mode_Offline = 1,
        Mode_Ftp = 2,
        Mode_ContentPack = 3
    }

    [DataContract]
    public class ViewerConfiguration
    {
        private int BlockSize = 128;

        Dictionary<string, string> mapFilenameMap = new Dictionary<string, string>
        {
            { "theisland.ark", "The Island" },
            { "scorchedearth_p.ark","Scorched Earth"},
            { "aberration_p.ark", "Aberration"},
            { "extinction.ark", "Extinction"},
            { "ragnarok.ark", "Ragnarok"},
            { "valguero_p.ark", "Valguero" },
            {"crystalisles.ark", "Crystal Isles" },
            {"genesis.ark", "Genesis 1" },
            { "viking_p.ark", "Fjördur"},
            { "tiamatprime.ark", "Tiamat Prime"}
        };

        [DataMember] public string IV { get; set; }
        [DataMember] public string EncryptionPassword { get; set; } = "";
        [DataMember] public bool Artifacts { get; set; } = false;
        [DataMember] public bool BeaverDams { get; set; } = false;
        [DataMember] public bool WyvernNests { get; set; } = false;
        [DataMember(EmitDefaultValue = false, IsRequired = false)] public bool MagmaNests { get; set; } = false;
        [DataMember] public bool DeinoNests { get; set; } = false;
        [DataMember] public bool DrakeNests { get; set; } = false;
        [DataMember] public bool OilVeins { get; set; } = false;
        [DataMember] public bool WaterVeins { get; set; } = false;
        [DataMember] public bool GasVeins { get; set; } = false;
        [DataMember] public bool Obelisks { get; set; } = true;
        [DataMember(EmitDefaultValue = false, IsRequired = false)] public bool Glitches { get; set; } = true;
        [DataMember] public bool ChargeNodes { get; set; } = false;
        [DataMember] public bool PlantX { get; set; } = false;
        [DataMember] public bool PlantZ { get; set; } = false;
        [DataMember] public ViewerModes Mode { get; set; } = ViewerModes.Mode_SinglePlayer;
        [DataMember(IsRequired = false, EmitDefaultValue = false)] public bool UpdateNotificationSingle { get; set; } = true;
        [DataMember(IsRequired = false, EmitDefaultValue = false)] public bool UpdateNotificationFile { get; set; } = true;
        [DataMember] public string SelectedFile { get; set; }
        [DataMember] public string SelectedServer { get; set; } = "";
        [DataMember] public List<string> StructureExclusions { get; set; } = new List<string>();
        [DataMember] public List<ServerConfiguration> ServerList { get; set; } = new List<ServerConfiguration>();
        [DataMember] public List<ViewerWindow> Windows { get; set; } = new List<ViewerWindow>();
        [DataMember] public int Zoom { get; set; } = 50;
        [DataMember(IsRequired = false)] public int SplitterDistance { get; set; } = 840;
        [DataMember(IsRequired = false)] public bool HideNoStructures { get; set; } = true;
        [DataMember(IsRequired = false)] public bool HideNoTames { get; set; } = true;
        [DataMember(IsRequired = false)] public bool HideNoBody { get; set; } = true;
        [DataMember(IsRequired = false)] public int CommandPrefix { get; set; } = 0;
        [DataMember(IsRequired = false, EmitDefaultValue = false)] public int FtpDownloadMode { get; set; } = 0;
        [DataMember(IsRequired = false, EmitDefaultValue = false)] public bool SortCommandLineExport { get; set; } = false;
        [DataMember(IsRequired = false, EmitDefaultValue = false)] public bool ExportInventories { get; set; } = false;
        [DataMember(IsRequired = false, EmitDefaultValue = true)] public LogColourMap TribeLogColours { get; set; } = new LogColourMap();
        [DataMember(IsRequired = false)] public bool StoredTames { get; set; } = false;


        public List<DinoClassMap> DinoMap = new List<DinoClassMap>();
        public List<MapMarker> MapMarkerList { get; set; } = new List<MapMarker>();
        public List<ItemClassMap> ItemMap { get; set; } = new List<ItemClassMap>();
        public List<StructureClassMap> StructureMap { get; set; } = new List<StructureClassMap>();
        public List<ColourMap> ColourMap { get; set; } = new List<ColourMap>();
        public List<StructureMarker> TerminalMarkers { get; set; } = new List<StructureMarker>();
        public List<StructureMarker> GlitchMarkers { get; set; } = new List<StructureMarker>();

        

        public ViewerConfiguration()
        {
            Load();
        }

        public void Save()
        {
            var savePath = AppContext.BaseDirectory;
            var saveFilename = Path.Combine(savePath, "config.json");


            //encrypt server passwords prior to saving to disk
            if (ServerList.Count > 0)
            {
                foreach(var server in ServerList)
                {
                    
                    server.Username = EncryptString(server.Username, Convert.FromBase64String(this.IV), this.EncryptionPassword);
                    server.Password = EncryptString(server.Password, Convert.FromBase64String(this.IV), this.EncryptionPassword);
                }
            }


            //base64 the password to save
            this.EncryptionPassword = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(this.EncryptionPassword));
            string jsonFileContent = configToJson();
            File.WriteAllText(saveFilename, jsonFileContent);

            //save dinomap
            var dinoMapFilename = Path.Combine(savePath, "creaturemap.json");
           
            JArray dinoArray = new JArray();
            if(DinoMap.Count > 0)
            {
                foreach(var dino in DinoMap)
                {
                    JObject dinoObject = new JObject();
                    dinoObject.Add(new JProperty("ClassName", dino.ClassName));
                    dinoObject.Add(new JProperty("FriendlyName", dino.FriendlyName));
                    dinoArray.Add(dinoObject);
                }
            }
            JObject dinoSaves = new JObject();
            dinoSaves.Add(new JProperty("creatures", dinoArray));

            File.WriteAllText(dinoMapFilename, dinoSaves.ToString(Formatting.None));


            //save itemmap
            var itemMapFilename = Path.Combine(savePath, "itemmap.json");

            JArray itemArray = new JArray();
            if (ItemMap.Count > 0)
            {
                foreach (var item in ItemMap)
                {
                    JObject itemObject = new JObject();
                    itemObject.Add(new JProperty("ClassName", item.ClassName));
                    itemObject.Add(new JProperty("Category", item.Category));
                    itemObject.Add(new JProperty("FriendlyName", item.FriendlyName));
                    itemObject.Add(new JProperty("Image", item.Image));

                    itemArray.Add(itemObject);
                }
            }

            JObject itemSaves = new JObject();
            itemSaves.Add(new JProperty("items", itemArray));
            File.WriteAllText(itemMapFilename, itemSaves.ToString(Formatting.None));

            //save mapmarkers
            var mapMarkerFilename  = Path.Combine(savePath, "mapmarkers.json");
            JArray markerArray = new JArray();
            if (MapMarkerList.Count > 0)
            {
                foreach (var marker in MapMarkerList)
                {
                    JObject markerObject = new JObject();
                    markerObject.Add(new JProperty("Map", marker.Map));
                    markerObject.Add(new JProperty("Name", marker.Name));
                    markerObject.Add(new JProperty("Image", marker.Image));
                    markerObject.Add(new JProperty("Colour", marker.Colour));
                    markerObject.Add(new JProperty("BorderColour", marker.BorderColour));
                    markerObject.Add(new JProperty("BorderWidth", marker.BorderWidth));
                    markerObject.Add(new JProperty("Lat", marker.Lat));
                    markerObject.Add(new JProperty("Lon", marker.Lon));

                    markerArray.Add(markerObject);
                }
            }
            JObject markerFile = new JObject();
            markerFile.Add(new JProperty("markers", markerArray));
            File.WriteAllText(mapMarkerFilename, markerFile.ToString(Formatting.None));

            //re-load config now saved
            Load();
        }

        public void Load() 
        {
            var savePath = AppDomain.CurrentDomain.BaseDirectory;
            var saveFilename = Path.Combine(savePath, "config.json");

            Mode = ViewerModes.Mode_SinglePlayer;
            SelectedFile = "TheIsland.ark";
            SelectedServer = "";
            
            //load colours

            ColourMap = new List<ColourMap>();
            string colourMapFilename = Path.Combine(savePath, "colours.json");
            if (File.Exists(colourMapFilename))
            {
                string jsonFileContent = File.ReadAllText(colourMapFilename);

                JObject itemFile = JObject.Parse(jsonFileContent);
                JArray itemList = (JArray)itemFile.GetValue("colors");
                foreach (JObject itemObject in itemList)
                {
                    ColourMap item = new ColourMap();
                    item.Id = itemObject.Value<int>("id");
                    item.Hex = itemObject.Value<string>("hex");
                    ColourMap.Add(item);
                }
            }

            //load markers
            MapMarkerList = new List<MapMarker>();
            string markerFilename = Path.Combine(savePath, "mapmarkers.json");
            if (File.Exists(markerFilename))
            {
                string jsonFileContent = File.ReadAllText(markerFilename);

                JObject markerFile = JObject.Parse(jsonFileContent);
                JArray markerList = (JArray)markerFile.GetValue("markers");
                foreach(JObject markerObject in markerList)
                {
                    MapMarker mapMarker = new MapMarker();

                    mapMarker.Map = markerObject.Value<string>("Map");
                    mapMarker.Name = markerObject.Value<string>("Name");
                    mapMarker.Colour = markerObject.Value<int>("Colour");
                    mapMarker.BorderColour = markerObject.Value<int>("BorderColour");
                    mapMarker.BorderWidth = markerObject.Value<int>("BorderWidth");
                    mapMarker.Image = markerObject.Value<string>("Image");
                    mapMarker.Lat = markerObject.Value<double>("Lat");
                    mapMarker.Lon = markerObject.Value<double>("Lon");


                    MapMarkerList.Add(mapMarker);
                }
            }


            //load terminal markers
            string structureMarkerFilename = Path.Combine(savePath, "structuremarkers.json");
            if (File.Exists(structureMarkerFilename))
            {
                string jsonFileContent = File.ReadAllText(structureMarkerFilename);

                TerminalMarkers = new List<StructureMarker>();
                JObject markerFile = JObject.Parse(jsonFileContent);
                JArray terminalList = (JArray)markerFile.GetValue("terminals");

                foreach (JObject markerObject in terminalList)
                {
                    StructureMarker mapMarker = new StructureMarker();

                    mapMarker.Map = markerObject.Value<string>("Map");
                    mapMarker.Lat = markerObject.Value<double>("Lat");
                    mapMarker.Lon = markerObject.Value<double>("Lon");
                    mapMarker.X = (float)Math.Round(markerObject.Value<float>("X"),2);
                    mapMarker.Y = (float)Math.Round(markerObject.Value<float>("Y"),2);
                    mapMarker.Z = (float)Math.Round(markerObject.Value<float>("Z"),2);

                    mapMarker.Colour = markerObject.Value<string>("Colour");

                    TerminalMarkers.Add(mapMarker);
                }


                GlitchMarkers = new List<StructureMarker>();
                JArray glitchList = (JArray)markerFile.GetValue("glitches");

                foreach (JObject markerObject in glitchList)
                {
                    StructureMarker mapMarker = new StructureMarker();

                    mapMarker.Map = markerObject.Value<string>("Map");
                    mapMarker.Lat = markerObject.Value<double>("Lat");
                    mapMarker.Lon = markerObject.Value<double>("Lon");
                    mapMarker.X = markerObject.Value<float>("X");
                    mapMarker.Y = markerObject.Value<float>("Y");
                    mapMarker.Z = markerObject.Value<float>("Z");

                    mapMarker.Colour = markerObject.Value<string>("Colour");

                    GlitchMarkers.Add(mapMarker);
                }


            }


            //load item map
            ItemMap = new List<ItemClassMap>();
            string itemMapFilename = Path.Combine(savePath, "itemmap.json");
            if (File.Exists(itemMapFilename))
            {
                string jsonFileContent = File.ReadAllText(itemMapFilename);

                JObject itemFile = JObject.Parse(jsonFileContent);
                JArray itemList = (JArray)itemFile.GetValue("items");
                foreach (JObject itemObject in itemList)
                {
                    ItemClassMap item = new ItemClassMap();
                    item.ClassName = itemObject.Value<string>("ClassName");
                    item.FriendlyName = itemObject.Value<string>("FriendlyName");
                    item.Category = itemObject.Value<string>("Category");
                    item.Image = itemObject.Value<string>("Image") ;
                    ItemMap.Add(item);
                }
            }

            //load structure map
            StructureMap = new List<StructureClassMap>();
            string structureMapFilename = Path.Combine(savePath, "structuremap.json");
            if (File.Exists(structureMapFilename))
            {
                string jsonFileContent = File.ReadAllText(structureMapFilename);

                JObject itemFile = JObject.Parse(jsonFileContent);
                JArray itemList = (JArray)itemFile.GetValue("structures");
                foreach (JObject itemObject in itemList)
                {
                    StructureClassMap  item = new StructureClassMap();
                    item.ClassName = itemObject.Value<string>("ClassName");
                    item.FriendlyName = itemObject.Value<string>("FriendlyName");
                    StructureMap.Add(item);
                }
            }

            //load dino map
            DinoMap = new List<DinoClassMap>();
            string dinoMapFilename = Path.Combine(savePath, "creaturemap.json");
            if (File.Exists(dinoMapFilename))
            {
                string jsonFileContent = File.ReadAllText(dinoMapFilename);

                JObject dinoFile = JObject.Parse(jsonFileContent);
                JArray dinoList = (JArray)dinoFile.GetValue("creatures");
                foreach (JObject dinoObject in dinoList)
                {
                    DinoClassMap dino = new DinoClassMap();
                    dino.ClassName = dinoObject.Value<string>("ClassName");
                    dino.FriendlyName = dinoObject.Value<string>("FriendlyName");
                    DinoMap.Add(dino);
                }
            }

            ServerList = new List<ServerConfiguration>();
            if (File.Exists(saveFilename))
            {
                //found, load the saved state
                string jsonFileContent = File.ReadAllText(saveFilename);
                configFromJson(jsonFileContent);

                //decrypt server password after reading from disk
                if(EncryptionPassword !=null && EncryptionPassword.Length > 0)
                {
                    //decode from base64
                    byte[] passwordBytes = Convert.FromBase64String(EncryptionPassword);
                    this.EncryptionPassword = ASCIIEncoding.ASCII.GetString(passwordBytes);

                    if (ServerList.Count > 0)
                    {
                        foreach (var server in ServerList)
                        {

                            string mapFilename = "theisland.ark";
                            if (server.SaveGamePath.ToLower().EndsWith(".ark"))
                            {

                                if (server.SaveGamePath.Contains("/"))
                                {
                                    mapFilename = server.SaveGamePath.Substring(server.SaveGamePath.LastIndexOf("/")+1).ToLower();
                                    server.SaveGamePath = server.SaveGamePath.Substring(0, server.SaveGamePath.LastIndexOf("/")+1);
                                }

                            }
                            else
                            {
                                mapFilename = server.Map;
                            }
                            server.Map = mapFilename.ToLower();
                            if (server.Address.ToLower().Contains("ftp://"))
                            {
                                server.Address = server.Address.Substring(server.Address.IndexOf("ftp://") + 6);
                            }

                            server.Username = DecryptString(server.Username, Convert.FromBase64String(this.IV), this.EncryptionPassword);
                            server.Password = DecryptString(server.Password, Convert.FromBase64String(this.IV), this.EncryptionPassword);
                        }
                    }
                }
                else
                {
                    //create random initial password for this installation
                    Random rnd = new Random();

                    string randomPasswordString = "";
                    for (int charIndex = 0; charIndex < 16; charIndex++)
                    {
                        int nextRand = rnd.Next(32, 126);
                        randomPasswordString += (char)nextRand;
                    }

                    this.EncryptionPassword = randomPasswordString;

                }


            }
            else
            {

                Aes aes = Aes.Create();
                this.IV = Convert.ToBase64String(aes.IV);

                Save();

            }
        }

        private string configToJson()
        {
            JsonSerializer js = new JsonSerializer();
            TextWriter output = new StringWriter();


            using (JsonWriter writer = new JsonTextWriter(output))
            {
                js.Serialize(writer, this);

            }

            return output.ToString();
        }

        private void configFromJson(string jsonMessage)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ViewerConfiguration));

            using (MemoryStream m = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage)))
            {
                var fileState = ser.ReadObject(m);
                ViewerConfiguration savedState = (ViewerConfiguration)fileState;

                this.EncryptionPassword = savedState.EncryptionPassword;
                if (savedState.IV !=null)
                {
                    this.IV = savedState.IV;
                }
                else
                {
                    Aes aes = Aes.Create();
                    this.IV =  Convert.ToBase64String(aes.IV);
                }


                this.Mode = savedState.Mode;
                this.SelectedFile = savedState.SelectedFile;
                this.SelectedServer = savedState.SelectedServer;
                this.Artifacts = savedState.Artifacts;
                this.BeaverDams = savedState.BeaverDams;
                this.DeinoNests = savedState.DeinoNests;
                this.GasVeins = savedState.GasVeins;
                this.OilVeins = savedState.OilVeins;
                this.Obelisks = savedState.Obelisks;
                this.WaterVeins = savedState.WaterVeins;
                this.WyvernNests = savedState.WyvernNests;
                this.DrakeNests = savedState.DrakeNests;
                this.ChargeNodes = savedState.ChargeNodes;
                this.PlantZ = savedState.PlantZ;
                this.PlantX = savedState.PlantX;
                this.Glitches = savedState.Glitches;
                this.MagmaNests = savedState.MagmaNests;
                this.UpdateNotificationFile = savedState.UpdateNotificationFile;
                this.UpdateNotificationSingle = savedState.UpdateNotificationSingle;
                this.SortCommandLineExport = savedState.SortCommandLineExport;
                this.ExportInventories = savedState.ExportInventories;
                this.TribeLogColours = new LogColourMap();
                if(savedState.TribeLogColours!=null)  this.TribeLogColours = savedState.TribeLogColours;
                if (this.TribeLogColours.TextColourMap == null) this.TribeLogColours.TextColourMap = new List<LogTextColourMap>();
                if (savedState.StructureExclusions != null)  this.StructureExclusions = savedState.StructureExclusions;

                this.HideNoBody = savedState.HideNoBody;
                this.HideNoStructures = savedState.HideNoStructures;
                this.HideNoTames = savedState.HideNoTames;
                this.CommandPrefix = savedState.CommandPrefix;
                this.FtpDownloadMode = savedState.FtpDownloadMode;
                if(savedState.Windows!=null) this.Windows = savedState.Windows;
                this.StoredTames = savedState.StoredTames;
                this.Zoom = savedState.Zoom;
                this.SplitterDistance = savedState.SplitterDistance;
                this.ServerList = savedState.ServerList;


                savedState = null;
            }
            
        }

        private string EncryptString(string plainText, byte[] currentIV, string password)
        {
            string returnString = "";

            // Binary representation of plain text string
            byte[] plaintextBytes = (new UnicodeEncoding()).GetBytes(plainText);

            //Encrypt
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.BlockSize = BlockSize;
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(password));
            crypt.IV =  currentIV;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
                }

                returnString = Convert.ToBase64String(memoryStream.ToArray());
            }

            return returnString;
        }

        private string DecryptString(string encryptedText, byte[] currentIV, string password)
        {
            string plainText = "";

            byte[] bytes = Convert.FromBase64String(encryptedText);
            SymmetricAlgorithm crypt = Aes.Create();
            HashAlgorithm hash = MD5.Create();
            crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(password));
            crypt.IV = currentIV;

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (CryptoStream cryptoStream =
                   new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    byte[] decryptedBytes = new byte[bytes.Length];
                    cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    plainText = Encoding.Unicode.GetString(decryptedBytes);
                }
            }

            if (plainText.Contains("\0"))
            {
                plainText = plainText.Replace("\0", "");
            }

            return plainText;
        }

    }
}
