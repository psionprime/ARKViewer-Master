﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models.ASVPack
{
    [DataContract]
    public class ContentItem
    {
        [DataMember] public string ClassName { get; set; }
        [DataMember] public string CustomName { get; set; }
        [DataMember] public string CraftedByPlayer { get; set; }
        [DataMember] public string CraftedByTribe { get; set; }
        [DataMember] public int Quantity { get; set; }
        [DataMember] public bool IsBlueprint { get; set; }
        [DataMember] public bool IsEngram { get; set; }

    }
}
