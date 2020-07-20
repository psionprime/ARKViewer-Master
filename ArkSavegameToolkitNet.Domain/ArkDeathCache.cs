using ArkSavegameToolkitNet.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkSavegameToolkitNet.Domain
{
    public class ArkDeathCache: ArkGameDataContainerBase
    {
        // properties
        private static readonly ArkName _targetingTeam = ArkName.Create("TargetingTeam");
        private static readonly ArkName _ownerName = ArkName.Create("OwnerName");
        private static readonly ArkName _owningPlayerID = ArkName.Create("DeathCacheCharacterID");
        private static readonly ArkName _myInventoryComponent = ArkName.Create("MyInventoryComponent");
        
        public string ClassName { get; set; }
        public ArkLocation Location { get; set; }
        public string OwnerName { get; set; }
        public int? TargetingTeam { get; set; }
        public int? OwningPlayerId { get; set; }
        public int? InventoryId { get; set; }

        [JsonIgnore]
        public ArkItem[] Inventory => _inventory.Value;
        private Lazy<ArkItem[]> _inventory;
        private IGameObject _deathCache = null;

        public ArkDeathCache()
        {
            // Relations
            _inventory = new Lazy<ArkItem[]>(() => {
                if (!InventoryId.HasValue) return new ArkItem[] { };

                ArkItem[] items = null;
                return _gameData?._inventoryItems.TryGetValue(InventoryId.Value, out items) == true ? items.Where(ArkItem.Filter_RealItems).ToArray() : new ArkItem[] { };
            });
        }

        public ArkDeathCache(IGameObject deathCache, ISaveState saveState) : this()
        {
            _deathCache = deathCache;

            ClassName = deathCache.ClassName.Name;
            OwnerName = deathCache.GetPropertyValue<string>(_ownerName);
            TargetingTeam = deathCache.GetPropertyValue<int?>(_targetingTeam);
            OwningPlayerId = deathCache.GetPropertyValue<int?>(_owningPlayerID);
            InventoryId = deathCache.GetPropertyValue<ObjectReference>(_myInventoryComponent)?.ObjectId;

            if (deathCache?.Location != null) Location = new ArkLocation(deathCache.Location, saveState);
        }


        internal void Decouple()
        {
            _deathCache = null;
        }
    }
}
