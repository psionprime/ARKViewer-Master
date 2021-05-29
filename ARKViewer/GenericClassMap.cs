using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer
{
    public interface IGenericClassMap
    {
        string ClassName { get; set; }
        string FriendlyName { get; set; }
    }
}
