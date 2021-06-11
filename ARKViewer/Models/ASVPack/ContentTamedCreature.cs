using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models.ASVPack
{
    [DataContract]
    public class ContentTamedCreature
    {
        [DataMember] public long Id { get; set; }  = 0;
        [DataMember] public string TamedOnServerName { get; set; } = "";
        [DataMember] public string ClassName { get; set; } = "";
        [DataMember] public string Name { get; set; } = "";
        [DataMember] public float? Latitude { get; set; }
        [DataMember] public float? Longitude { get; set; }
        [DataMember] public float? X { get; set; } = 0;
        [DataMember] public float? Y { get; set; } = 0;
        [DataMember] public float? Z { get; set; } = 0;
        [DataMember] public int BaseLevel { get; set; } = 0;
        [DataMember] public int Level { get; set; } = 0;
        [DataMember] public string Gender { get; set; } = "N/a";
        [DataMember] public byte[] BaseStats { get; set; }
        [DataMember] public byte[] TamedStats { get; set; }
        [DataMember] public byte[] Colors { get; set; }
        [DataMember] public bool IsCryo { get; set; } = false;
        [DataMember] public bool IsVivarium { get; set; } = false;
        [DataMember] public int TargetingTeam { get; set; } = 0;
        [DataMember] public string TribeName { get; set; } = "";
        [DataMember] public string TamerName { get; set; } = "";
        [DataMember] public string ImprinterName { get; set; } = "";
        [DataMember] public decimal ImprintQuality { get; set; } = 0;
        [DataMember] public int RandomMutationsFemale { get; set; } = 0;
        [DataMember] public int RandomMutationsMale { get; set; } = 0;
        [DataMember] public long? InventoryId { get; set; } = null;
        [DataMember] public int ImprintedPlayerId { get; set; } = 0;
        [DataMember] public long? MotherId { get; set; }
        [DataMember] public string MotherName { get; set; } = "";
        [DataMember] public long? FatherId { get; internal set; }
        [DataMember] public string FatherName { get; set; } = "";
        [DataMember] public int AbandonedTeam { get; set; } = 0;
        [DataMember] public string[] Resources { get; set; }


        public bool IsSpawned()
        {
            return Latitude.HasValue;
        }

    }
}
