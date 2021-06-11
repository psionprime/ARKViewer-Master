using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models.ASVPack
{
    [DataContract]
    public class ContentStructure
    {
        [DataMember] public string ClassName { get; set; } = "";
        [DataMember] public float? Latitude { get; set; } = 0;
        [DataMember] public float? Longitude { get; set; } = 0;
        [DataMember] public float X { get; set; } = 0;
        [DataMember] public float Y { get; set; } = 0;
        [DataMember] public float Z { get; set; } = 0;
        [DataMember] public long? InventoryId { get; set; } = null;
        [DataMember] public long TargetingTeam { get; set; } = 0;
        [DataMember] public long AbandonedTeam { get; set; } = 0;
    }
}
