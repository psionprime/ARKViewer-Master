using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentInventory
    {
        [DataMember] public int InventoryId { get; set; }
        [DataMember] public List<ContentItem> Items { get; set; }
    }
}
