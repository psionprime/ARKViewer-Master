﻿using ArkSavegameToolkitNet.Arrays;
using ArkSavegameToolkitNet.Structs;
using ArkSavegameToolkitNet.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkSavegameToolkitNet.Domain
{
    public class ArkLocation
    {
        //latitude shift, latitude divisor, longitude shift, longitude divisor
        private static Dictionary<string, Tuple<float, float, float, float>> _latlonCalcs = new Dictionary<string, Tuple<float, float, float, float>>
        {
            { "TheIsland", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "Hope", Tuple.Create(50f, 6850f, 50f, 6850f) },
            { "TheCenter", Tuple.Create(30.34223747253418f, 9584.0f, 55.10416793823242f, 9600.0f) },
            { "ScorchedEarth_P", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "Aberration_P", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "Extinction", Tuple.Create(50.0f, 8000.0f, 50.0f, 8000.0f) },
            { "Valhalla", Tuple.Create(48.813560485839844f, 14750.0f, 48.813560485839844f, 14750.0f) },
            { "MortemTupiu", Tuple.Create(32.479148864746094f, 20000.0f, 40.59893798828125f, 16000.0f) },
            { "ShigoIslands", Tuple.Create(50.001777870738339260f, 9562.0f, 50.001777870738339260f, 9562.0f) },
            { "Ragnarok", Tuple.Create(50.009388f, 13100f, 50.009388f, 13100f) },
            { "TheVolcano", Tuple.Create(50.0f, 9181.0f, 50.0f, 9181.0f) },
            { "PGARK", Tuple.Create(0.0f, 6080.0f, 0.0f, 6080.0f) },
            { "CrystalIsles" , Tuple.Create(48.7f, 16000f, 50.0f, 17000.0f) },
            { "Valguero_P" , Tuple.Create(50.0f, 8161.0f, 50.0f, 8161.0f) },
            { "Genesis", Tuple.Create(50.0f, 10500.0f, 50.0f, 10500.0f)},
            { "AstralARK", Tuple.Create(50.0f, 2000.0f, 50.0f, 2000.0f)},
            { "Tunguska_p", Tuple.Create(46.8f, 14000.0f,49.29f, 13300.0f) },
            { "Caballus_p", Tuple.Create(50.0f, 8125.0f,50.0f, 8125.0f)},
            { "Viking_P", Tuple.Create(50.0f, 7140.0f,50.0f, 7140.0f)},
            { "TiamatPrime", Tuple.Create(50.0f, 8000.0f,50.0f, 8000.0f)}
        };

        //width, height, latitude-top, longitude-left, longitude-right, latitude-bottom
        // { "ShigoIslands", Tuple.Create(50.0f, 8128.0f, 50.0f, 8128.0f) },
        private static Dictionary<string, Tuple<int, int, float, float, float, float>> _topoMapCalcs;

        static ArkLocation()
        {
           
            try
            {

                //painted-maps are divided into a 10x10 grid, lacking precise offsets and should instead align with the grid (0.0f, 0.0f, 100.0f, 100.0f)
                //topo-maps offsets are calculated using two easily identifiable points on the map and reversing the formula for TopoMapX/TopoMapY val-2.95f, 0.0f, 86.3f, 89.0f
                _topoMapCalcs = new Dictionary<string, Tuple<int, int, float, float, float, float>>
                {
                    { "TheIsland", Tuple.Create(1024, 1024, 7.2f, 7.2f, 92.8f, 92.8f) },
                    { "Hope", Tuple.Create(1024, 1024, 0f, 0f, 100.0f, 100.0f) },
                    { "TheCenter", Tuple.Create(1024, 1024, -2.5f, 1f, 104.5f, 101f) },
                    { "ScorchedEarth_P", Tuple.Create(1024, 1024, 7.2f, 7.2f, 92.8f, 92.8f) },
                    { "Aberration_P", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "Extinction", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "Ragnarok", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "CrystalIsles", Tuple.Create(1024, 1024, 0f, 0f, 100f, 100.0f) },
                    { "ShigoIslands", Tuple.Create(1024, 1024, -2.0f, -1.6f, 99.8f, 101.0f) },
                    { "TheVolcano", Tuple.Create(1024, 1024, -1.95f, -1.3f, 99.5f, 100.7f) },
                    { "Valguero_P", Tuple.Create(1024, 1024, -10.0f, -10.0f, 110.0f, 110.0f) },
                    { "Genesis", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "AstralARK", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "Tunguska_p", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "Caballus_p", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "Viking_P", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) },
                    { "TiamatPrime", Tuple.Create(1024, 1024, 0.0f, 0.0f, 100.0f, 100.0f) }
                };
            }
            finally
            {

            }
        }

        public ArkLocation() { }

        public ArkLocation(LocationData locationData, ISaveState savedState)
        {
            X = locationData.X;
            Y = locationData.Y;
            Z = locationData.Z;

            if (savedState?.MapName != null)
            {
                MapName = savedState.MapName;

                Tuple<float, float, float, float> vals = null;
                if (_latlonCalcs.TryGetValue(savedState.MapName, out vals))
                {
                    Latitude = vals.Item1 + Y / vals.Item2;
                    Longitude = vals.Item3 + X / vals.Item4;

                    Tuple<int, int, float, float, float, float> mapvals = null;
                    if (_topoMapCalcs.TryGetValue(savedState.MapName, out mapvals))
                    {
                        TopoMapX = (Longitude - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                        TopoMapY = (Latitude - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    }
                }
                else
                {
                    _latlonCalcs.TryGetValue("Extinction", out vals);

                    Latitude = vals.Item1 + Y / vals.Item2;
                    Longitude = vals.Item3 + X / vals.Item4;

                    Tuple<int, int, float, float, float, float> mapvals = null;
                    if (_topoMapCalcs.TryGetValue(savedState.MapName, out mapvals))
                    {
                        TopoMapX = (Longitude - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                        TopoMapY = (Latitude - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    }
                }
            }
        }

        public string MapName { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public float? TopoMapX { get; set; }
        public float? TopoMapY { get; set; }
    }
}
