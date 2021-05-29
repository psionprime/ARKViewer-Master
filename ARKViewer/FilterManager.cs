using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkSavegameToolkitNet.Domain;

namespace ARKViewer
{
    public class FilterManager
    {
        ArkGameData gd = null;

        public FilterManager(ArkGameData data)
        {
            gd = data;
        }
        ~FilterManager()
        {
            gd = null;
        }

        public List<ArkWildCreature> GetWildCreatures(int minLevel, int maxLevel, float fromLat, float fromLon, float fromRadius, string selectedClass)
        {
            return gd.WildCreatures.Where(w =>
                                            ((w.ClassName == selectedClass || selectedClass == "") && ((w.BaseLevel >= minLevel && w.BaseLevel <= maxLevel) || w.BaseLevel == 0))
                                            && (Math.Abs(w.Location.Latitude.GetValueOrDefault(0) - fromLat) <= fromRadius)
                                            && (Math.Abs(w.Location.Longitude.GetValueOrDefault(0) - fromLon) <= fromRadius)
                                ).OrderByDescending(c => c.BaseLevel).ToList();
        }

        public List<ArkTamedCreature> GetTamedCreatures(string selectedClass, int selectedTribeId, int selectedPlayerId, bool includeCryoVivarium)
        {
            return gd.TamedCreatures.Where(w =>
                                            (w.ClassName == selectedClass || selectedClass == "")
                                            & !(w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                            && (selectedTribeId == 0 || w.TargetingTeam == selectedTribeId)
                                            && (selectedPlayerId == 0 || (w.OwningPlayerId.GetValueOrDefault(0) == selectedPlayerId || w.ImprinterPlayerDataId.GetValueOrDefault(0) == selectedPlayerId))
                                            && (includeCryoVivarium || w.IsCryo == false)
                                            && (includeCryoVivarium || w.IsVivarium == false)

                                        ).ToList();

            
        }

        public List<ArkStructure> GetPlayerStructures(int selectedTribeId, int selectedPlayerId, string selectedClass)
        {
            return gd.Structures.Where(s =>
                                            (
                                                (selectedClass.Length == 0 || s.ClassName == selectedClass)

                                            )
                                            &&
                                            (
                                                (s.OwningPlayerId != null && s.OwningPlayerId == selectedPlayerId)
                                                ||
                                                (s.TargetingTeam != null && s.TargetingTeam == selectedTribeId || selectedTribeId == 0 && s.TargetingTeam != null)
                                            )
                                            &&
                                            (
                                                !Program.ProgramConfig.StructureExclusions.Contains(s.ClassName)

                                            )
                                        ).ToList();

        }

        public List<ArkPlayer> GetPlayers(int selectedTribeId, int selectedPlayerId)
        {
            return gd.Players.Where(p => (selectedTribeId == 0 || p.TribeId == selectedTribeId) && (selectedPlayerId == 0 || p.Id == selectedPlayerId)).ToList();
        }

        public List<ArkTribe> GetTribes(int selectedTribeId)
        {
            return gd.Tribes.Where(t => selectedTribeId == 0 || t.Id == selectedTribeId).ToList();
        }

    }
}
