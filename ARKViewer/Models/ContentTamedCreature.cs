using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentTamedCreature
    {
        [DataMember] public long Id { get; set; }
        [DataMember] public string TamedOnServerName { get; set; }
        [DataMember] public string ClassName { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public float? Latitude { get; set; }
        [DataMember] public float? Longitude { get; set; }
        [DataMember] public float? X { get; set; }
        [DataMember] public float? Y { get; set; }
        [DataMember] public float? Z { get; set; }
        [DataMember] public int BaseLevel { get; set; }
        [DataMember] public int Level { get; set; }
        [DataMember] public string Gender { get; set; }
        [DataMember] public byte[] BaseStats { get; set; }
        [DataMember] public byte[] TamedStats { get; set; }
        [DataMember] public byte[] Colors { get; set; }
        [DataMember] public bool IsCryo { get; set; }
        [DataMember] public bool IsVivarium { get; set; }
        [DataMember] public int TargetingTeam { get; set; }
        [DataMember] public string TribeName { get; set; }
        [DataMember] public string TamerName { get; set; }
        [DataMember] public string ImprinterName { get; set; }
        [DataMember] public decimal ImprintQuality { get; set; }
        [DataMember] public int RandomMutationsFemale { get; set; }
        [DataMember] public int RandomMutationsMale { get; set; }
        [DataMember] public int? InventoryId { get; set; } = null;
        [DataMember] public int ImprintedPlayerId { get; set; }
        [DataMember] public long? MotherId { get; set; }
        [DataMember] public string MotherName { get; set; }
        [DataMember] public long? FatherId { get; internal set; }
        [DataMember] public string FatherName { get; internal set; }

        public bool IsSpawned()
        {
            return X.HasValue;
        }

    }
}
