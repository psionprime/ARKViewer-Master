using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models.ASVPack
{
    [DataContract]
    public class ContentWildCreature
    {
        [DataMember] public long Id { get; set; }
        [DataMember] public string ClassName { get; set; }
        [DataMember] public float? Latitude { get; set; }
        [DataMember] public float? Longitude { get; set; }
        [DataMember] public float? X { get; set; }
        [DataMember] public float? Y { get; set; }
        [DataMember] public float? Z { get; set; }

        [DataMember] public int BaseLevel { get; set; }
        [DataMember] public  string Gender { get; set; }
        [DataMember] public byte[] BaseStats { get; set; }
        [DataMember] public byte[] Colors { get; set; }
        [DataMember] public string[] Resources { get; set; } 
        
        public bool IsSpawned()
        {
            return Latitude.HasValue;
        }
    }
}
