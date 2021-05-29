using ArkSavegameToolkitNet.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer
{
    public class ExportManager
    {
        ArkGameData gd = null;

        public ExportManager(ArkGameData data)
        {
            gd = data;
        }
        ~ExportManager()
        {
            gd = null;
        }

        public void ExportAll(string exportPath)
        {

        }

        public void ExportWild(string exportFilename)
        {
            using (FileStream fs = File.Create(exportFilename))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        var creatureList = Program.ProgramConfig.SortCommandLineExport ? gd.WildCreatures.OrderBy(o => o.ClassName).Cast<ArkWildCreature>() : gd.WildCreatures;

                        jw.WriteStartArray();

                        //Creature, Sex, Lvl, Lat, Lon, HP, Stam, Weight, Speed, Food, Oxygen, Craft, C0, C1, C2, C3, C4, C5              
                        foreach (var creature in creatureList)
                        {
                            jw.WriteStartObject();

                            jw.WritePropertyName("id");
                            jw.WriteValue(creature.Id);

                            jw.WritePropertyName("creature");
                            jw.WriteValue(creature.ClassName);

                            jw.WritePropertyName("sex");
                            jw.WriteValue(creature.Gender);

                            jw.WritePropertyName("lvl");
                            jw.WriteValue(creature.BaseLevel);

                            jw.WritePropertyName("lat");
                            if (creature.Location != null && creature.Location.Latitude != null)
                            {
                                jw.WriteValue(creature.Location.Latitude);
                            }
                            else
                            {
                                jw.WriteValue(0);
                            }

                            jw.WritePropertyName("lon");
                            if (creature.Location != null && creature.Location.Longitude != null)
                            {
                                jw.WriteValue(creature.Location.Longitude);
                            }
                            else
                            {
                                jw.WriteValue(0);
                            }

                            jw.WritePropertyName("hp");
                            jw.WriteValue(creature.BaseStats[0]);

                            jw.WritePropertyName("stam");
                            jw.WriteValue(creature.BaseStats[1]);

                            jw.WritePropertyName("melee");
                            jw.WriteValue(creature.BaseStats[8]);

                            jw.WritePropertyName("weight");
                            jw.WriteValue(creature.BaseStats[7]);

                            jw.WritePropertyName("speed");
                            jw.WriteValue(creature.BaseStats[9]);

                            jw.WritePropertyName("food");
                            jw.WriteValue(creature.BaseStats[4]);

                            jw.WritePropertyName("oxy");
                            jw.WriteValue(creature.BaseStats[3]);

                            jw.WritePropertyName("craft");
                            jw.WriteValue(creature.BaseStats[11]);

                            jw.WritePropertyName("c0");
                            jw.WriteValue(creature.Colors[0]);

                            jw.WritePropertyName("c1");
                            jw.WriteValue(creature.Colors[1]);

                            jw.WritePropertyName("c2");
                            jw.WriteValue(creature.Colors[2]);

                            jw.WritePropertyName("c3");
                            jw.WriteValue(creature.Colors[3]);

                            jw.WritePropertyName("c4");
                            jw.WriteValue(creature.Colors[4]);

                            jw.WritePropertyName("c5");
                            jw.WriteValue(creature.Colors[5]);

                            jw.WritePropertyName("ccc");
                            if (creature.Location != null)
                            {
                                jw.WriteValue($"{creature.Location.X} {creature.Location.Y} {creature.Location.Z}");

                            }
                            else
                            {
                                jw.WriteValue("");
                            }

                            jw.WriteEnd();
                        }

                        jw.WriteEndArray();
                    }

                }

            }
        }

        public void ExportTamed(string exportFilename)
        {

        }

        public void ExportPlayerStructures(string exportFilename)
        {

        }

        public void ExportPlayerTribes(string exportFilename)
        {

        }

        public void ExportPlayers(string exportFilename)
        {

        }

    }
}
