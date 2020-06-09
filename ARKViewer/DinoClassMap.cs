using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer
{
    [DataContract]
    public class DinoClassMap
    {
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string FriendlyName { get; set; }

    }
}
