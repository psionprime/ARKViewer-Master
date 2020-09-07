using ArkSavegameToolkitNet.Arrays;
using ArkSavegameToolkitNet.Property;
using ArkSavegameToolkitNet.Structs;
using ArkSavegameToolkitNet.Types;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArkSavegameToolkitNet
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ArkSavegame : IGameObjectContainer, IDisposable
    {
        private static ILog _logger = LogManager.GetLogger(typeof(ArkSavegame));
        private static readonly ArkName _customItemData = ArkName.Create("CustomItemDatas");
        private static readonly ArkName _myCharacterStatusComponent = ArkName.Create("MyCharacterStatusComponent");
        private static readonly ArkName _ownerInventory = ArkName.Create("OwnerInventory");
        private static readonly ArkName _myInventoryComponent = ArkName.Create("MyInventoryComponent");

        private static readonly ArkName _customDataBytesIdentifier = ArkName.Create("CustomDataBytes");
        private static readonly ArkName _byteArraysIdentifier = ArkName.Create("ByteArrays");
        private static readonly ArkName _bytesIdentifier = ArkName.Create("Bytes");

        private static readonly ArkName _dinoDataList = ArkName.Create("DinoDataList");
        private static readonly ArkName _dinoData = ArkName.Create("DinoData");


        [JsonProperty]
        public IList<GameObject> Objects
        {
            get { return _objects; }
            private set
            {
                _objects = value ?? throw new NullReferenceException("Value cannot be null");
            }
        }
        private IList<GameObject> _objects = new List<GameObject>();

        [JsonProperty]
        public IList<string> DataFiles
        {
            get { return _dataFiles; }
            set
            {
                _dataFiles = value ?? throw new NullReferenceException("Value cannot be null");
            }
        }
        private IList<string> _dataFiles = new List<string>();

        [JsonProperty]
        public IList<EmbeddedData> EmbeddedData
        {
            get { return _embeddedData; }
            set
            {
                _embeddedData = value ?? throw new NullReferenceException("Value cannot be null");
            }
        }
        private IList<EmbeddedData> _embeddedData = new List<EmbeddedData>();

        [JsonProperty]
        public short SaveVersion { get; set; }
        [JsonProperty]
        public float GameTime { get; set; }
        //the only way to get this is by looking at the last modified date of the savegame file. this may not be correct if not read from an active save on the server
        [JsonProperty]
        public DateTime SaveTime { get; set; }
        public SaveState SaveState { get; set; }

        protected internal int binaryDataOffset;
        protected internal int nameTableOffset;
        protected internal int propertiesBlockOffset;
        private string _fileName;
        private bool _baseRead;
        private long _gameObjectsOffset;
        private ArkStringCache _arkStringCache;
        private ArkNameCache _arkNameCache;
        private readonly int? _savegameMaxDegreeOfParallelism;
        private ArkNameTree _exclusivePropertyNameTree;

        private MemoryMappedFile _mmf;
        private MemoryMappedViewAccessor _va;
        private ArkArchive _archive;

        public ArkSavegame(int? savegameMaxDegreeOfParallelism = null, ArkNameTree exclusivePropertyNameTree = null)
        {
            Objects = new List<GameObject>();
            _arkNameCache = new ArkNameCache();
            _arkStringCache = new ArkStringCache();
            _savegameMaxDegreeOfParallelism = savegameMaxDegreeOfParallelism;
            _exclusivePropertyNameTree = exclusivePropertyNameTree;
        }

        public ArkSavegame(string fileName, ArkNameCache arkNameCache = null, int? savegameMaxDegreeOfParallelism = null, ArkNameTree exclusivePropertyNameTree = null)
        {
            _fileName = fileName;
            _arkNameCache = arkNameCache ?? new ArkNameCache();
            _arkStringCache = new ArkStringCache();
            _savegameMaxDegreeOfParallelism = savegameMaxDegreeOfParallelism;
            _exclusivePropertyNameTree = exclusivePropertyNameTree;

            var fi = new FileInfo(_fileName);
            var size = fi.Length;
            SaveTime = fi.LastWriteTimeUtc;
            if (size == 0) return;

            _mmf = MemoryMappedFile.CreateFromFile(_fileName, FileMode.Open, null, 0L, MemoryMappedFileAccess.Read);
            _va = _mmf.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.Read);
            _archive = new ArkArchive(_va, size, _arkNameCache, _arkStringCache, _exclusivePropertyNameTree);
        }

        /// <summary>
        /// Load all gameobjects, properties and other data in this savegame.
        /// </summary>
        public bool LoadEverything()
        {
            readBinary(_archive, _mmf);
            bool returnValue  = LoadCryopodEntries();
            return returnValue;
        }

        private bool LoadCryopodEntries()
        {

            int nextObjectId = 0;

            //parse any vivarium creatures
            var vivariumList = Objects.Where(o => o.ClassName.Name == "BP_Vivarium_C" && o.Location!=null).ToList();
            Guid currentId = Guid.Empty;

            foreach(var vivarium in vivariumList)
            {

                if (!currentId.Equals(vivarium.Uuid))
                {
                    currentId = vivarium.Uuid;

                    //get dino data (PropertyArray)
                    if (vivarium.Properties.ContainsKey(_dinoDataList))
                    {


                        PropertyArray dinoArray = vivarium.GetProperty<PropertyArray>(_dinoDataList);
                        foreach(StructPropertyList dinoData in dinoArray.Value)
                        {
                            if (dinoData.Properties.ContainsKey(_dinoData))
                            {
                                PropertyArray dinoDetails = dinoData.GetProperty<PropertyArray>(_dinoData);

                                nextObjectId = Objects.Count();
                                sbyte[] sbyteData = dinoDetails.Value.Cast<sbyte>().ToArray();
                                byte[] byteData = (byte[])(Array)sbyteData;


                                using (MemoryMappedFile cryoMmf = MemoryMappedFile.CreateNew(null, byteData.Length))
                                {
                                    using (MemoryMappedViewStream stream = cryoMmf.CreateViewStream())
                                    {
                                        BinaryWriter writer = new BinaryWriter(stream);
                                        writer.Write(byteData);
                                    }

                                    using (MemoryMappedViewAccessor cryoVa = cryoMmf.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.Read))
                                    {
                                        ArkArchive cryoArchive = new ArkArchive(cryoVa, byteData.Length, _arkNameCache, _arkStringCache, _exclusivePropertyNameTree);

                                        var result = UpdateVivariumCreatureStatus(cryoArchive);
                                        result.Item1.Location = vivarium.Location;

                                        
                                        Objects.Add(result.Item1);
                                        Objects.Add(result.Item2);

                                    }
                                }
                            }
                        }
                    }
                }
            };


            //Now parse out cryo creature data
            var cryoPodEntries = Objects.Where(WhereEmptyCryopodHasCustomItemDataBytesArrayBytes).ToList();
            
            nextObjectId = Objects.Count();

            if (cryoPodEntries != null && cryoPodEntries.Count() > 0)
            {
                
                foreach(var cryoPod in cryoPodEntries)
                {

                    var byteList = SelectCustomDataBytesArrayBytes(cryoPod);
                    sbyte[] sbyteValues = byteList.Value.Cast<sbyte>().ToArray();
                    byte[] cryoDataBytes = (byte[])(Array)sbyteValues;


                    using (MemoryMappedFile cryoMmf = MemoryMappedFile.CreateNew(null, cryoDataBytes.Length))
                    {
                        using (MemoryMappedViewStream stream = cryoMmf.CreateViewStream())
                        {
                            BinaryWriter writer = new BinaryWriter(stream);
                            writer.Write(cryoDataBytes);
                        }

                        using (MemoryMappedViewAccessor cryoVa = cryoMmf.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.Read))
                        {
                            ArkArchive cryoArchive = new ArkArchive(cryoVa, cryoDataBytes.Length, _arkNameCache, _arkStringCache, _exclusivePropertyNameTree);

                            var result = UpdateCryoCreatureStatus(cryoArchive);
                            var ownerInventoryRef = cryoPod.GetProperty<PropertyObject>(_ownerInventory);

                            if (ownerInventoryRef != null && ownerInventoryRef.Value?.ObjectId != null)
                            {
                                var ownerContainerInventory = Objects.FirstOrDefault(o => o.ObjectId == ownerInventoryRef.Value.ObjectId);
                                if (ownerContainerInventory != null)
                                {
                                    var ownerContainer = Objects.FirstOrDefault(o => o.Properties.ContainsKey(_myInventoryComponent) && o.GetProperty<PropertyObject>(_myInventoryComponent).Value.ObjectId == ownerContainerInventory.ObjectId);
                                    if (ownerContainer != null && ownerContainer.Location != null)
                                    {
                                        result.Item1.Location = ownerContainer.Location;
                                    }
                                }
                            }

                            Objects.Add(result.Item1);
                            Objects.Add(result.Item2);
                        }



                    }
                };

            }

            return true;

            bool WhereEmptyCryopodHasCustomItemDataBytesArrayBytes(GameObject o) => (o.ClassName.Name == "PrimalItem_WeaponEmptyCryopod_C" || o.ClassName.Name == "PrimalItem_WeaponEmptyCryopod_PE_C")
                && o.GetProperty<PropertyArray>(_customItemData)?.Value[0] is StructPropertyList customProperties
                && customProperties.GetProperty<PropertyStruct>(_customDataBytesIdentifier) is PropertyStruct customDataBytes
                && customDataBytes.Value is StructPropertyList byteArrays
                && byteArrays.GetProperty<PropertyArray>(_byteArraysIdentifier)?.Value.Count > 0 && byteArrays.GetProperty<PropertyArray>(_byteArraysIdentifier)?.Value[0] is StructPropertyList bytes
                && bytes != null && bytes.GetProperty<PropertyArray>(_bytesIdentifier) is PropertyArray byteList
                && byteList != null && byteList.Value.Count > 0;


            PropertyArray SelectCustomDataBytesArrayBytes(GameObject o) => ((StructPropertyList)((StructPropertyList)((StructPropertyList)o.GetProperty<PropertyArray>(_customItemData).Value[0]).GetProperty<PropertyStruct>(_customDataBytesIdentifier).Value).GetProperty<PropertyArray>(_byteArraysIdentifier).Value[0]).GetProperty<PropertyArray>(_bytesIdentifier);

            Tuple<GameObject, GameObject> UpdateCryoCreatureStatus(ArkArchive cryoArchive)
            {
                cryoArchive.GetBytes(4);

                nextObjectId++;
                var dino = new GameObject(cryoArchive)
                {
                    ObjectId = nextObjectId,
                    IsCryo = true
                };

                nextObjectId++;
                var statusobject = new GameObject(cryoArchive)
                {
                    ObjectId = nextObjectId
                };

                dino.loadProperties(cryoArchive, new GameObject(), 0, null);
                

                
                
                var statusComponentRef = dino.GetProperty<PropertyObject>(_myCharacterStatusComponent);
                statusComponentRef.Value.ObjectId = statusobject.ObjectId;
                
                statusobject.loadProperties(cryoArchive, new GameObject(), 0, null);
                return new Tuple<GameObject, GameObject>(dino, statusobject);
            }

            Tuple<GameObject, GameObject> UpdateVivariumCreatureStatus(ArkArchive vivariumArchive)
            {
                vivariumArchive.GetBytes(4);

                nextObjectId++;
                var dino = new GameObject(vivariumArchive)
                {
                    ObjectId = nextObjectId,
                    IsVivarium = true
                };

                nextObjectId++;
                var statusobject = new GameObject(vivariumArchive)
                {
                    ObjectId = nextObjectId
                };

                dino.loadProperties(vivariumArchive, new GameObject(), 0, null);

                var statusComponentRef = dino.GetProperty<PropertyObject>(_myCharacterStatusComponent);
                statusComponentRef.Value.ObjectId = statusobject.ObjectId;

                statusobject.loadProperties(vivariumArchive, new GameObject(), 0, null);
                return new Tuple<GameObject, GameObject>(dino, statusobject);
            }


        }

        public GameObject GetObjectAtOffset(long offset, int nextPropertiesOffset)
        {
            var oldposition = _archive.Position;
            _archive.Position = offset;
            var gameObject = new GameObject(_archive, _arkNameCache);
            gameObject.loadProperties(_archive, null, propertiesBlockOffset, nextPropertiesOffset);
            _archive.Position = oldposition;

            return gameObject;
        }

        public IEnumerable<Tuple<GameObjectReader, GameObjectReader>> GetObjectsEnumerable()
        {
            var fi = new FileInfo(_fileName);
            var size = fi.Length;
            SaveTime = fi.LastWriteTimeUtc;
            if (size == 0) yield break;

            //using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(_fileName, FileMode.Open, null, 0L, MemoryMappedFileAccess.Read))
            //{
            //    using (MemoryMappedViewAccessor va = mmf.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.Read))
            //    {
            //        ArkArchive archive = new ArkArchive(va, size, _arkNameCache);
            //        if (!_baseRead) readBinaryBase(archive);
            //        else archive.Position = _gameObjectsOffset;

            //        var count = archive.GetInt();
            //        for (var n = 0; n < count; n++)
            //        {
            //            var gameObject = new GameObjectReader(archive, _arkNameCache) { Index = n };
            //            archive.Position += gameObject.Size;
            //            yield return gameObject;
            //        }
            //    }
            //}

            if (!_baseRead) readBinaryBase(_archive);
            else _archive.Position = _gameObjectsOffset;

            var count = _archive.GetInt();
            GameObjectReader prev = null;
            for (var n = 0; n <= count; n++)
            {
                GameObjectReader gameObject = null;
                if (n < count)
                {
                    gameObject = new GameObjectReader(_archive, _arkNameCache) { Index = n };
                    _archive.Position += gameObject.Size;
                }

                if (n > 0) yield return Tuple.Create(prev, gameObject);
                prev = gameObject;
            }
        }

        public void readBinary(ArkArchive archive, MemoryMappedFile mmf)
        {
            if (!_baseRead) readBinaryBase(archive);
            else archive.Position = _gameObjectsOffset;
            readBinaryObjects(archive);
            readBinaryObjectProperties(archive, mmf);


        }

        private void readBinaryBase(ArkArchive archive)
        {
            readBinaryHeader(archive);

            if (SaveVersion > 5)
            {
                // Name table is located after the objects block, but will be needed to read the objects block
                readBinaryNameTable(archive);
            }

            readBinaryDataFiles(archive);

            SaveState = new SaveState { GameTime = GameTime, SaveTime = SaveTime, MapName = DataFiles.FirstOrDefault() };

            readBinaryEmbeddedData(archive);

            var unknownValue = archive.GetInt();
            if (unknownValue != 0)
            {
                //if (unknownValue > 2)
                //{
                //    var msg = $"Found unexpected Value {unknownValue} at {archive.Position - 4:X}";
                //    _logger.Error(msg);
                //    throw new System.NotSupportedException(msg);
                //}

                for (int n = 0; n < unknownValue; n++)
                {
                    archive.GetInt(); //unknownFlags
                    archive.GetInt(); //objectCount
                    archive.GetString(); //name
                }
            }
            _baseRead = true;
            _gameObjectsOffset = archive.Position;
        }

        protected void readBinaryHeader(ArkArchive archive)
        {


            SaveVersion = archive.GetShort();

            if (SaveVersion == 5)
            {
                GameTime = archive.GetFloat();

                propertiesBlockOffset = 0;
            }
            else if (SaveVersion == 6)
            {
                nameTableOffset = archive.GetInt();
                propertiesBlockOffset = archive.GetInt();
                GameTime = archive.GetFloat();
            }
            else if (SaveVersion == 7 || SaveVersion == 8)
            {
                binaryDataOffset = archive.GetInt();
                var unknownValue = archive.GetInt();
                if (unknownValue != 0)
                {
                    var msg = $"Found unexpected Value {unknownValue} at {archive.Position - 4:X}";
                    _logger.Error(msg);
                    throw new System.NotSupportedException(msg);
                }

                nameTableOffset = archive.GetInt();
                propertiesBlockOffset = archive.GetInt();
                GameTime = archive.GetFloat();
            }
            else if (SaveVersion == 9)
            {
                binaryDataOffset = archive.GetInt();
                var unknownValue = archive.GetInt();
                if (unknownValue != 0)
                {
                    var msg = $"Found unexpected Value {unknownValue} at {archive.Position - 4:X}";
                    _logger.Error(msg);
                    throw new System.NotSupportedException(msg);
                }

                nameTableOffset = archive.GetInt();
                propertiesBlockOffset = archive.GetInt();
                GameTime = archive.GetFloat();

                //note: unknown integer value was added in v268.22 with SaveVersion=9 [0x25 (37) on The Island, 0x26 (38) on ragnarok/center/scorched]
                archive.GetInt(); //unknownValue2
            }
            else
            {
                var msg = $"Found unknown Version {SaveVersion}";
                _logger.Error(msg);
                throw new System.NotSupportedException(msg);
            }
        }

        protected void readBinaryNameTable(ArkArchive archive)
        {
            var position = archive.Position;

            archive.Position = nameTableOffset;

            var nameCount = archive.GetInt();
            var nameTable = new List<string>(nameCount);
            for (var n = 0; n < nameCount; n++)
            {
                nameTable.Add(archive.GetString());
            }

            archive.NameTable = nameTable;

            archive.Position = position;
        }

        protected void readBinaryDataFiles(ArkArchive archive, bool dataFiles = true)
        {
            var count = archive.GetInt();
            if (dataFiles)
            {
                DataFiles.Clear();
                for (var n = 0; n < count; n++)
                {
                    DataFiles.Add(archive.GetString());
                }
            }
            else
            {
                for (var n = 0; n < count; n++)
                {
                    archive.SkipString();
                }
            }
        }

        protected void readBinaryEmbeddedData(ArkArchive archive, bool embeddedData = true)
        {
            var count = archive.GetInt();
            if (embeddedData)
            {
                EmbeddedData.Clear();
                for (var n = 0; n < count; n++)
                {
                    EmbeddedData.Add(new EmbeddedData(archive));
                }
            }
            else
            {
                for (int n = 0; n < count; n++)
                {
                    Types.EmbeddedData.Skip(archive);
                }
            }
        }

        protected void readBinaryObjects(ArkArchive archive, bool gameObjects = true)
        {
            if (gameObjects)
            {
                var count = archive.GetInt();

                Objects.Clear();
                for (var n = 0; n < count; n++)
                {
                    var gameObject = new GameObject(archive, _arkNameCache);

                    gameObject.ObjectId = n;



                    Objects.Add(gameObject);
                }

            }
        }

        protected void readBinaryObjectProperties(ArkArchive archive, MemoryMappedFile mmf, Func<GameObject, bool> objectFilter = null, bool gameObjects = true, bool gameObjectProperties = true)
        {
            if (gameObjects && gameObjectProperties)
            {
                var success = true;
                try
                {
                    var cb = new ConcurrentBag<ArkArchive>();
                    cb.Add(archive);




                    var indices = Enumerable.Range(0, Objects.Count);
                    if (objectFilter != null) indices = indices.Where(x => objectFilter(Objects[x]));


                    Parallel.ForEach(indices, _savegameMaxDegreeOfParallelism.HasValue ? new ParallelOptions { MaxDegreeOfParallelism = _savegameMaxDegreeOfParallelism.Value } : new ParallelOptions { },
                        () => 
                        
                        
                        
                        { ArkArchive arch = null; var va = cb.TryTake(out arch) ? null : mmf.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.Read); return new { va = va, a = arch ?? new ArkArchive(archive, va) }; },
                        (item, loopState, taskLocals) =>
                        {
                            readBinaryObjectPropertiesImpl(item, taskLocals.a);
                            return taskLocals;
                        },
                        (taskLocals) => { if (taskLocals?.va != null) taskLocals.va.Dispose(); }
                        );
                }
                catch (AggregateException ae)
                {
                    success = false;
                    ae.Handle(ex => {
                        if (ex is IOException 
                            && ex.Message.IndexOf("Not enough storage is available to process this command.", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            _logger.Error($"Not enough memory available to load properties with this degree of parallelism.");
                            return true;
                        }

                        return false;
                    });
                }

     
                if (!success) throw new ApplicationException("Failed to load properties for all gameobjects.");
            }




        }

        protected void readBinaryObjectPropertiesImpl(int n, ArkArchive archive)
        {
            Objects[n].loadProperties(archive, (n < Objects.Count - 1) ? Objects[n + 1] : null, propertiesBlockOffset);
                
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _va?.Dispose();
                    _mmf?.Dispose();
                    _va = null;
                    _mmf = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
