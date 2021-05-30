using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace ARKViewer.CustomNameMaps
{
    [DataContract]
    public class ColourMap
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Hex { get; set; }

        public Color Color
        {
            get
            {
                Color translatedColor = Color.White;
                try
                {
                    translatedColor = ColorTranslator.FromHtml(Hex);
                }
                finally
                {

                }

                return translatedColor;
            }
            
        }

    }
}
