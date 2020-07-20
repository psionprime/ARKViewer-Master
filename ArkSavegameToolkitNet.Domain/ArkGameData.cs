using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArkName = ArkSavegameToolkitNet.Types.ArkName;
using ObjectReference = ArkSavegameToolkitNet.Types.ObjectReference;
using StructPropertyList = ArkSavegameToolkitNet.Structs.StructPropertyList;

namespace ArkSavegameToolkitNet.Domain
{
    public class ArkGameData : ArkGameDataBase
    {
        private readonly string _saveFilePath;
        private readonly ArkClusterDataBase _clusterData;
        private readonly int? _savegameMaxDegreeOfParallelism;
        private readonly bool _loadOnlyPropertiesInDomain;
        private dynamic[] _newData;

        private static readonly ArkName _myCharacterStatusComponent = ArkName.Create("MyCharacterStatusComponent");
        private static readonly ArkName _myData = ArkName.Create("MyData");
        private static readonly ArkName _playerDataID = ArkName.Create("PlayerDataID");
        private static readonly ArkName _linkedPlayerDataID = ArkName.Create("LinkedPlayerDataID");
        private static readonly ArkName _uniqueID = ArkName.Create("UniqueID");

        internal static readonly ArkNameTree _dependencies = new ArkNameTree
        {
            { _myCharacterStatusComponent, null },
            {
                _myData,
                new ArkNameTree
                {
                    { _playerDataID, null },
                    { _uniqueID, null }
                }
            },
            { _linkedPlayerDataID, null }
        };

        public ArkGameData(string saveFilePath, string clusterSavePath = null, int? savegameMaxDegreeOfParallelism = null, bool loadOnlyPropertiesInDomain = false)
        {
            _saveFilePath = saveFilePath;
            _clusterData = !string.IsNullOrEmpty(clusterSavePath) ? new ArkClusterData(clusterSavePath) : null;
            _savegameMaxDegreeOfParallelism = savegameMaxDegreeOfParallelism;
            _loadOnlyPropertiesInDomain = loadOnlyPropertiesInDomain;
        }

        public ArkGameData(string saveFilePath, ArkClusterDataBase clusterData, int? savegameMaxDegreeOfParallelism = null, bool loadOnlyPropertiesInDomain = false)
        {
            _saveFilePath = saveFilePath;
            _clusterData = clusterData;
            _savegameMaxDegreeOfParallelism = savegameMaxDegreeOfParallelism;
            _loadOnlyPropertiesInDomain = loadOnlyPropertiesInDomain;
        }

        /// <param name="externalPlayerData">Supply the update operation with external player data. This is a way to make sure transfered players are not lost inbetween saves.</param>
        public ArkGameDataUpdateResult Update(CancellationToken ct, ArkPlayerExternal[] externalPlayerData = null, bool deferApplyNewData = false, ArkAnonymizeData anonymize = null)
        {
            var success = false;
            var cancelled = false;
            ArkSavegame save = null;
            var st = Stopwatch.StartNew();

            try
            {
                var directoryPath = Path.GetDirectoryName(_saveFilePath);
                var exclusivePropertyNameTree = _loadOnlyPropertiesInDomain ? ArkGameDataContainerBase._alldependencies : null;

                // Extract all game data
                save = new ArkSavegame(_saveFilePath, null, _savegameMaxDegreeOfParallelism, exclusivePropertyNameTree);
                save.LoadEverything();

                ct.ThrowIfCancellationRequested();

                var arktribes = Directory.GetFiles(directoryPath, "*.arktribe", SearchOption.TopDirectoryOnly).Select(x =>
                {
                    try
                    {
                        return new ArkSavegameToolkitNet.ArkTribe(x, exclusivePropertyNameTree: exclusivePropertyNameTree);
                    }
                    catch (Exception ex) { Debug.WriteLine($"Failed to extract tribe profile '{x}': {ex.Message}"); }

                    return null;
                    
                }).Where(x => x != null).ToArray();
                ct.ThrowIfCancellationRequested();


                var _myData = ArkName.Create("MyData");
                var _playerDataID = ArkName.Create("PlayerDataID");
                var _linkedPlayerDataID = ArkName.Create("LinkedPlayerDataID");
                var _uniqueID = ArkName.Create("UniqueID");

                var arkprofiles = Directory.GetFiles(directoryPath, "*.arkprofile", SearchOption.TopDirectoryOnly).Select(x =>
                {
                    try
                    {
                        return new ArkProfile(x, exclusivePropertyNameTree: exclusivePropertyNameTree); 
                    }
                    catch(Exception ex) { Debug.WriteLine($"Failed to extract player profile '{x}': {ex.Message}"); }

                    return null;
                }).Where(x => x != null).ToArray();
                ct.ThrowIfCancellationRequested();

                
                // Remove duplicates from object collection (objects are sometimes duplicated for structures, creatures etc.)
                var objects = save.Objects.GroupBy(x => x.Names, new ArkNameCollectionComparer()).Select(x => x.OrderBy(y => y.ObjectId).First()).ToArray();

                // Map all game data into domain model
                // Note: objects.GroupBy(x => x.Names.Last().Token) would also get creature, status- and inventory component together
                var statusComponents = objects.Where(x => x.IsDinoStatusComponent).ToDictionary(x => x.ObjectId, x => x);
                var tamed = objects.Where(x => x.IsTamedCreature).Select(x =>
                {
                    GameObject status = null;
                    statusComponents.TryGetValue(x.GetPropertyValue<ObjectReference>(_myCharacterStatusComponent).ObjectId, out status);
                    ArkTamedCreature returnValue = x.AsTamedCreature(status, save.SaveState);

                    return returnValue;
                }).ToArray();


                var wild = objects.Where(x => x.IsWildCreature).Select(x =>
                {
                    GameObject status = null;
                    statusComponents.TryGetValue(x.GetPropertyValue<ObjectReference>(_myCharacterStatusComponent).ObjectId, out status);
                    ArkWildCreature returnValue = x.AsWildCreature(status, save.SaveState);

                    return returnValue;
                }).ToArray();

                
                var playerdict = objects.Where(x => x.IsPlayerCharacter).ToLookup(x => x.GetPropertyValue<long>(_linkedPlayerDataID), x => x);

                //var duplicates = playerdict.Where(x => x.Count() > 1).ToArray();

                //mydata.GetPropertyValue<StructUniqueNetIdRepl>(_uniqueID)?.NetId
                //private static readonly ArkName _uniqueID = ArkName.Create("UniqueID"); 


                List<Tuple<string, long, ArkProfile>> distinctProfiles = new List<Tuple<string, long, ArkProfile>>();
                foreach(var x in arkprofiles)
                {
                    try
                    {

                        var mydata = x.GetPropertyValue<StructPropertyList>(_myData);

                        Tuple<string, long, ArkProfile> newTuple = new Tuple<string, long, ArkProfile>(mydata.GetPropertyValue<Structs.StructUniqueNetIdRepl>(_uniqueID)?.NetId, mydata.GetPropertyValue<long>(_playerDataID), x);
                        if (distinctProfiles.Count(p => p.Item1 == newTuple.Item1) == 0)
                        {
                            distinctProfiles.Add(newTuple);
                        }

                    }
                    catch (Exception ex) { Debug.WriteLine($"Failed to project player profile as domain object '{x._fileName}': {ex.Message}"); }
                }

                var keyedProfiles = distinctProfiles.Select(x =>
                {
                    return new
                    {
                        steamId = x.Item1,
                        playerId = x.Item2,
                        profile = x.Item3,
                    };

                }).ToArray();



                var profileSteamIds = keyedProfiles.Select(x => x.steamId).Where(x => x != null).Distinct();

                var players = keyedProfiles.Select(x =>
                {
                    var player = playerdict[x.playerId]?.FirstOrDefault();
                    try
                    {
                        return x.profile.Profile.AsPlayer(player, x.profile.SaveTime, save.SaveState);
                    }
                    catch (Exception ex) { Debug.WriteLine($"Failed to project player profile as domain object '{x.profile._fileName}': {ex.Message}"); }

                    return null;
                }).Where(x => x != null).ToArray();
                var externalPlayers = externalPlayerData != null ?
                    externalPlayerData.Where(x => !profileSteamIds.Contains(x.SteamId, StringComparer.OrdinalIgnoreCase)).Select(x => x.AsPlayer()).ToArray() 
                    : new ArkPlayer[] { };

                var allplayers = players.Concat(externalPlayers).ToArray();

                var tribes = arktribes.Select(x => x.Tribe.AsTribe(x.SaveTime)).Where(t=>t.Id!=0).ToArray();
                var items = objects.Where(x => x.IsItem).Select(x => x.AsItem(save.SaveState)).ToArray();
                var droppedItems = objects.Where(x => x.IsDroppedItem).Select(x =>
                {
                    GameObject linkedItem = null;

                    int linkedItemId = x.GetPropertyValue<ObjectReference>(ArkName.Create("MyItem")).ObjectId;
                    linkedItem = objects.Where(o => o.ObjectId == linkedItemId).FirstOrDefault();
                    ArkDroppedItem returnValue = x.AsDroppedItem(linkedItem, save.SaveState);

                    return returnValue;
                }).ToArray();

                var playerDeathCache = objects.Where(x => x.IsDeathItemCache).Select(x => x.AsDeathCache(save.SaveState)).ToArray();

                var structures = objects.Where(x => x.IsStructure).Select(x => x.AsStructure(save.SaveState)).ToArray();

                
                ApplyOrSaveNewData(deferApplyNewData, save, tamed, wild, allplayers, tribes, items, droppedItems, structures, playerDeathCache, anonymize);

                
                
                success = true;
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
            }
            finally
            {
                save?.Dispose();
            }

            return new ArkGameDataUpdateResult { Success = success, Cancelled = cancelled, Elapsed = st.Elapsed };
        }

        public bool ApplyPreviousUpdate(bool decouple = true)
        {
            if (_newData != null)
            {
                ApplyNewData(_newData[0], _newData[1], _newData[2], _newData[3], _newData[4], _newData[5], _newData[6], _newData[7], decouple, _newData[8]);
                _newData = null;
                return true;
            }
            else return false;
        }

        private void ApplyOrSaveNewData(bool deferApplyNewData, ArkSavegame save, ArkTamedCreature[] tamed, ArkWildCreature[] wild, ArkPlayer[] players, ArkTribe[] tribes, ArkItem[] items, ArkDroppedItem[] droppedItems, ArkStructure[] structures, ArkDeathCache[] deathCache, ArkAnonymizeData anonymize = null)
        {
            if (deferApplyNewData)
            {
                _newData = new dynamic[] { save, tamed, wild, players, tribes, items, droppedItems, structures, deathCache, anonymize };
            }
            else
            {
                ApplyNewData(save, tamed, wild, players, tribes, items, droppedItems, structures, true, deathCache, anonymize);
            }
        }

        private void ApplyNewData(ArkSavegame save, ArkTamedCreature[] tamed, ArkWildCreature[] wild, ArkPlayer[] players, ArkTribe[] tribes, ArkItem[] items, ArkDroppedItem[] droppedItems, ArkStructure[] structures, bool decouple = true, ArkDeathCache[] deathCache = null, ArkAnonymizeData anonymize = null)
        {
            // Anonymize data if requested
            if (anonymize != null)
            {
                foreach (var i in players) anonymize.Do(i);
                foreach (var i in tribes) anonymize.Do(i);
                foreach (var i in tamed) anonymize.Do(i);
                foreach (var i in structures) anonymize.Do(i);
            }

            // Setup relations in the domain model between entities
            var newGameData = new ArkGameDataBase(save.SaveState, tamed, wild, players, tribes, items, droppedItems, structures, deathCache);
            newGameData.Initialize(_clusterData);
            foreach (var i in tamed) i.Initialize(newGameData, _clusterData);
            foreach (var i in wild) i.Initialize(newGameData, _clusterData);
            foreach (var i in players) i.Initialize(newGameData, _clusterData);
            foreach (var i in tribes) i.Initialize(newGameData, _clusterData);
            foreach (var i in items) i.Initialize(newGameData, _clusterData);
            foreach (var i in droppedItems) i.Initialize(newGameData, _clusterData);
            foreach (var i in structures) i.Initialize(newGameData, _clusterData);
            foreach (var i in deathCache) i.Initialize(newGameData, _clusterData);

            if (decouple) //should always be true except for debugging
            {
                // Unset all references to raw extracted game objects and properties to free memory
                foreach (var i in tamed) i.Decouple();
                foreach (var i in wild) i.Decouple();
                foreach (var i in players) i.Decouple();
                foreach (var i in tribes) i.Decouple();
                foreach (var i in items) i.Decouple();
                foreach (var i in droppedItems) i.Decouple();
                foreach (var i in structures) i.Decouple();
                foreach (var i in deathCache) i.Decouple();

            }

            // Assign updated data to public properties
            newGameData.CopyTo(this);

            // Force an immediate garbage collection because it seems more effective (extraction process requires a great deal of memory)
            //GC.Collect();
        }
    }
}
