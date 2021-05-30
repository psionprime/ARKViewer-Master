using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentItem
    {
        [DataMember] public string ClassName { get; set; }
        [DataMember] public int Quantity { get; set; }

    }
}
