using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentTribe
    {
        [DataMember] public long TribeId { get; set; }
        [DataMember] public string TribeName { get; set; }
        [DataMember] public DateTime? LastActive { get; set; }
        [DataMember] public List<ContentPlayer> Players { get; set; } = new List<ContentPlayer>();
        [DataMember] public List<ContentStructure> Structures { get; set; } = new List<ContentStructure>();
        [DataMember] public List<ContentTamedCreature> Tames { get; set; } = new List<ContentTamedCreature>();
        [DataMember] public string[] Logs { get; set; }



    }
}
