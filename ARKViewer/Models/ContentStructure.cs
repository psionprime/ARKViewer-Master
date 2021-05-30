using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentStructure
    {
        [DataMember] public string ClassName { get; set; }
        [DataMember] public float? Latitude { get; set; }
        [DataMember] public float? Longitude { get; set; }
        [DataMember] public int? InventoryId { get; set; }


    }
}
