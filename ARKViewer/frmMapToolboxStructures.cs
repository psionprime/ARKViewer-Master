﻿using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmMapToolboxStructures : Form
    {


        frmMapView MapViewer = null;
        bool isLoading = false;
        bool isUpdating = false;
        ContentManager cm = null;


        private static frmMapToolboxStructures inst;
        public static frmMapToolboxStructures GetForm(frmMapView mapViewer, ContentManager manager)
        {
            if (inst == null || inst.IsDisposed)
            {
                inst = new frmMapToolboxStructures(mapViewer,manager);
                inst.Owner = mapViewer;
            }

            return inst;
        }


        private void LoadWindowSettings()
        {
            var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);

            if (savedWindow != null)
            {
                var targetScreen = Screen.FromPoint(new Point(savedWindow.Left, savedWindow.Top));
                if (targetScreen == null) return;

                if (targetScreen.DeviceName == null || targetScreen.DeviceName == savedWindow.Monitor)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Left = savedWindow.Left;
                    this.Top = savedWindow.Top;
                    this.Width = savedWindow.Width;
                    this.Height = savedWindow.Height;
                }
            }
        }

        private void UpdateWindowSettings()
        {
            //only save location if normal window, do not save location/size if minimized/maximized
            if (this.WindowState == FormWindowState.Normal)
            {
                var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);
                if (savedWindow == null)
                {
                    savedWindow = new ViewerWindow();
                    savedWindow.Name = this.Name;
                    ARKViewer.Program.ProgramConfig.Windows.Add(savedWindow);
                }

                if (savedWindow != null)
                {
                    var restoreScreen = Screen.FromHandle(this.Handle);

                    savedWindow.Left = this.Left;
                    savedWindow.Top = this.Top;
                    savedWindow.Width = this.Width;
                    savedWindow.Height = this.Height;
                    savedWindow.Monitor = restoreScreen.DeviceName;

                }
            }
        }


        private frmMapToolboxStructures(frmMapView mapViewer, ContentManager manager)
        {
            
            InitializeComponent();
            LoadWindowSettings();

            MapViewer =  mapViewer;
            cm = manager;

            isLoading = true;

            var c = Program.ProgramConfig;
            chkMapTerminals.Checked = c.Obelisks;
            chkMapOilVeins.Checked = c.OilVeins;
            chkMapGasVeins.Checked = c.GasVeins;
            chkMapWaterVeins.Checked = c.WaterVeins;
            chkMapChargeNodes.Checked = c.ChargeNodes;
            chkMapArtifacts.Checked = c.Artifacts;
            chkMapWyvernNests.Checked = c.WyvernNests;
            chkMapDeinoNests.Checked = c.DeinoNests;
            chkMapDrakeNests.Checked = c.DrakeNests;
            chkMapMagmaNests.Checked = c.MagmaNests;
            chkMapBeaverDams.Checked = c.BeaverDams;
            chkMapGlitches.Checked = c.Glitches;

            isLoading = false;

            PopulateMapStructures();

        }

        private void btnCopyCommand_Click(object sender, EventArgs e)
        {

            if (cboConsoleCommands.SelectedItem == null) return;
            if (lvwStructureLocations.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];
            StructureMarker selectedMarker = new StructureMarker();


            ContentStructure selectedStructure = (ContentStructure)selectedItem.Tag;
            selectedMarker.Colour = "White";
            selectedMarker.Lat = (double)selectedStructure.Latitude.GetValueOrDefault(0);
            selectedMarker.Lon = (double)selectedStructure.Latitude.GetValueOrDefault(0);
            selectedMarker.X = selectedStructure.X;
            selectedMarker.Y = selectedStructure.Y;
            selectedMarker.Z = selectedStructure.Z;


            var commandText = cboConsoleCommands.SelectedItem.ToString();
            if (commandText != null)
            {

                commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedMarker.X:0.00}"));
                commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedMarker.Y:0.00}"));
                commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedMarker.Z + 500:0.00}")); //+500

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);
                lblStatus.Text = $"Command copied: {commandText}";
                lblStatus.Refresh();

            }

        }

        private void PopulateMapStructures()
        {
            lblStatus.Text = "Populating selected map structures...";
            lblStatus.Refresh();

            //update program config
            var c = Program.ProgramConfig;


            lvwStructureLocations.Items.Clear();

            ConcurrentBag<ListViewItem> structureBag = new ConcurrentBag<ListViewItem>();

            if (c.Obelisks)
            {
                var structures = cm.GetTerminals();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m=>m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });

            }
            if (c.OilVeins)
            {
                var structures = cm.GetOilVeins();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.GasVeins)
            {
                var structures = cm.GetGasVeins();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.WaterVeins)
            {
                var structures = cm.GetWyvernNests();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.ChargeNodes)
            {
                var structures = cm.GetChargeNodes();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.Artifacts)
            {
                var structures = cm.GetArtifacts();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.WyvernNests) 
            {
                var structures = cm.GetWyvernNests();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.DeinoNests)
            {
                var structures = cm.GetDeinoNests();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.DrakeNests)
            {
                var structures = cm.GetDrakeNests();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.MagmaNests)
            {
                var structures = cm.GetMagmaNests();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.BeaverDams)
            {
                var structures = cm.GetBeaverDams();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }
            if (c.Glitches)
            {
                var structures = cm.GetGlitchMarkers();
                Parallel.ForEach(structures, structure =>
                {
                    string friendlyName = structure.ClassName;
                    var mapStructure = Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(m => m.ClassName == structure.ClassName);
                    if (mapStructure != null)
                    {
                        friendlyName = mapStructure.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(friendlyName);
                    newItem.SubItems.Add(structure.Latitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.SubItems.Add(structure.Longitude.GetValueOrDefault(0).ToString("f2"));
                    newItem.Tag = structure;

                    structureBag.Add(newItem);

                });
            }

            if (!structureBag.IsEmpty) lvwStructureLocations.Items.AddRange(structureBag.ToArray());

            MapViewer.DrawTestMap(0,0);

            lblStatus.Text = "";
            lblStatus.Refresh();
        }

        private void UpdateSelection()
        {
            txtContents.Text = "";
            if (lvwStructureLocations.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];
            ContentStructure selectedStructure = (ContentStructure)selectedItem.Tag;

            selectedStructure.Latitude = selectedStructure.Latitude.GetValueOrDefault(0);
            selectedStructure.Longitude = selectedStructure.Longitude.GetValueOrDefault(0);
            selectedStructure.X = selectedStructure.X;
            selectedStructure.Y = selectedStructure.Y;
            selectedStructure.Z = selectedStructure.Z;

            MapViewer.DrawTestMap((decimal)selectedStructure.Longitude.GetValueOrDefault(0), (decimal)selectedStructure.Latitude.GetValueOrDefault(0));


            StringBuilder inventString = new StringBuilder();
            var inventory = cm.GetInventory(selectedStructure.InventoryId.GetValueOrDefault(0));
            if (inventory != null && inventory.Items.Count > 0)
            {
                var invItems = inventory.Items;
                var groupedItems = invItems.GroupBy(x => x.ClassName).Select(g => new { ClassName = g.Key, TotalQuantity = g.Sum(i => i.Quantity), Items = g.ToList() });
                foreach (var groupItem in groupedItems)
                {
                    if (chkGroup.Checked)
                    {
                        string friendlyName = "";
                        var map = Program.ProgramConfig.ItemMap.FirstOrDefault<ItemClassMap>(m => m.ClassName == groupItem.ClassName);
                        if (map != null) friendlyName = map.FriendlyName;

                        if (friendlyName.Contains(Environment.NewLine))
                        {
                            inventString.AppendLine($"{friendlyName}");
                        }
                        else
                        {
                            inventString.AppendLine($"{friendlyName} x {groupItem.TotalQuantity}");
                        }
                    }
                    else
                    {
                        foreach (var item in invItems)
                        {
                            string friendlyName = item.CustomName == null ? "" : item.CustomName;
                            if (friendlyName.Length == 0)
                            {
                                var map = Program.ProgramConfig.ItemMap.FirstOrDefault<ItemClassMap>(m => m.ClassName == item.ClassName);
                                if (map != null) friendlyName = map.FriendlyName;
                            }

                            if (friendlyName.Contains(Environment.NewLine))
                            {
                                inventString.AppendLine($"{friendlyName}");
                            }
                            else
                            {
                                inventString.AppendLine($"{friendlyName} x {item.Quantity}");
                            }

                        }

                    }



                }

            };

            
            txtContents.Text = inventString.ToString();
        }

        private void UpdateConfig()
        {
            if (isLoading) return;

            //update program config
            var c = Program.ProgramConfig;

            c.Obelisks = chkMapTerminals.Checked;
            c.OilVeins = chkMapOilVeins.Checked;
            c.GasVeins = chkMapGasVeins.Checked;
            c.WaterVeins = chkMapWaterVeins.Checked;
            c.ChargeNodes = chkMapChargeNodes.Checked;
            c.Artifacts = chkMapArtifacts.Checked;
            c.WyvernNests = chkMapWyvernNests.Checked;
            c.DeinoNests = chkMapDeinoNests.Checked;
            c.DrakeNests = chkMapDrakeNests.Checked;
            c.MagmaNests = chkMapMagmaNests.Checked;
            c.BeaverDams = chkMapBeaverDams.Checked;
            c.Glitches = chkMapGlitches.Checked;

            PopulateMapStructures();

        }

        private void chkMapTerminals_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapOilVeins_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapGasVeins_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapWaterVeins_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapChargeNodes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapArtifacts_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapWyvernNests_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapDeinoNests_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapDrakeNests_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapMagmaNests_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapBeaverDams_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void chkMapGlitches_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void lvwStructureLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelection();
        }

        private void frmMapToolboxStructures_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
