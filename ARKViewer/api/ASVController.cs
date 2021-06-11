using ARKViewer.Configuration;
using ARKViewer.Models.ASVPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Api
{
    public class ASVController
    {

        public string Get(string accessKey)
        {
            ApiUserConfiguration userConfig = Program.ApiConfig.Users.First(u=>u.AccessKey == accessKey);
            if (userConfig == null)
            {
                //return accessdenied
            }

            
            //filter the loaded game pack for selected user
            ContentPack pack = Program.GetFilteredPackCurrent(userConfig.TribeId,userConfig.PlayerId,userConfig.Latitude,userConfig.Longitude,userConfig.Radius, userConfig.AllowGameStructures, userConfig.AllowGameInventories,true,userConfig.AllowTamedCreatures, userConfig.AllowWildCreatures,userConfig.AllowTribeStructures, userConfig.AllowDroppedContents);
            
            //return pack
            return pack.ToJson();
        }
    }
}
