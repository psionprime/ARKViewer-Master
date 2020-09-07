using ArkSavegameToolkitNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkSavegameToolkitNet.Domain
{
    public class ArkMissionData
    {
        public string MissionTag { get; set; } = "";
        public float LastScore { get; set; } = 0;
        public float BestScore { get; set; } = 0;
    }
}
