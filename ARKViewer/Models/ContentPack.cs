using ArkSavegameToolkitNet.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer.Models
{
    [DataContract]
    public class ContentPack
    {
        [DataMember] public string MapFilename { get; set; } = "TheIsland.ark";
        [DataMember] public int ExportedForTribe { get; set; } = 0;
        [DataMember] public int ExportedForPlayer { get; set; } = 0;
        [DataMember] public List<ContentStructure> TerminalMarkers { get; set; }
        [DataMember] public List<ContentStructure> GlitchMarkers { get; set; }
        [DataMember] public List<ContentStructure> ChargeNodes { get; set; }
        [DataMember] public List<ContentStructure> BeaverDams { get; set; }
        [DataMember] public List<ContentStructure> WyvernNests { get; set; }
        [DataMember] public List<ContentStructure> DrakeNests { get; set; }
        [DataMember] public List<ContentStructure> MagmaNests { get; set; }
        [DataMember] public List<ContentStructure> OilVeins { get; set; }
        [DataMember] public List<ContentStructure> WaterVeins { get; set; }
        [DataMember] public List<ContentStructure> GasVeins { get; set; }
        [DataMember] public List<ContentStructure> Artifacts { get; set; }
        [DataMember] public List<ContentInventory> Inventories { get; set; }
        [DataMember] public List<ContentWildCreature> WildCreatures { get; set; }
        [DataMember] public List<ContentTribe> Tribes { get; set; }

        public ContentPack()
        {
            //defaults for new content pack


        }

        public ContentPack(ArkGameData gd, int selectedTribeId, int selectedPlayerId, decimal lat, decimal lon, decimal rad):this()
        {
            //load content pack from Ark savegame 
            MapFilename = gd.SaveState.MapName;

            ExportedForTribe = selectedTribeId;
            ExportedForPlayer = selectedPlayerId;


        }


        public ContentPack(string jsonPack): this()
        {
            //load content from json

        }

        public async Task<bool> ExportPack(string fileName)
        {

            return true;
        }



    }
}
