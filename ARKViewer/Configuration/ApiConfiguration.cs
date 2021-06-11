using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Configuration
{
    [DataContract]
    public class ApiConfiguration
    {
        [DataMember] public List<ApiUserConfiguration> Users { get; set; } = new List<ApiUserConfiguration>();
        public string Address { get; set; } = "http://localhost";
        public int Port { get; set; } = 8081;
    }
}
