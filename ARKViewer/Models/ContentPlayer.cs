using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentPlayer
    {
        [DataMember] public long Id { get; set; }
        [DataMember] public string CharacterName { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Gender { get; set; }
        [DataMember] public string SteamId { get; set; }
        [DataMember] public float? Latitude { get; set; }
        [DataMember] public float? Longitude { get; set; }
        [DataMember] public float? X { get; set; }
        [DataMember] public float? Y { get; set; }
        [DataMember] public float? Z { get; set; }
        [DataMember] public long? InventoryId { get; set; }
        [DataMember] public int Level { get; set; }
        [DataMember] public byte[] Stats { get; set; }
        [DataMember] public DateTime? LastActive { get; set; }
        

        public bool IsSpawned()
        {
            return X.HasValue;
        }


    }
}
