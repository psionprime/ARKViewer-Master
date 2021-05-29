using ArkSavegameToolkitNet.Domain;
using System;
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
    public partial class frmStructureLocations : Form
    {
        private ColumnHeader SortingColumn_Markers = null;
        private List<ArkItem> usedEggs = new List<ArkItem>();

        public event EventHandler<StructureMarker> HighlightStructure;

        public frmStructureLocations(List<StructureMarker> structureList, string windowTitle, Image structureIcon)
        {
            InitializeComponent();

            lblType.Text = windowTitle;
            if (structureIcon != null)
            {
                picType.Image = structureIcon;
            }
            
            lvwMapMarkers.Items.Clear();

            foreach (var structure in structureList)
            {
                ListViewItem newItem = lvwMapMarkers.Items.Add(structure.Lat.ToString("0.00"));
                newItem.SubItems.Add(structure.Lon.ToString("0.00"));
                newItem.Tag = structure;
            }


        }

        public frmStructureLocations(ArkGameData gameData, string className, string title, Image icon)
        {
            InitializeComponent();

            lblType.Text = title;
            picType.Image = icon;

            lvwMapMarkers.Items.Clear();

            var structureList = gameData.Structures.Where(s => s.ClassName.StartsWith(className));
            foreach(var structure in structureList)
            {
                ListViewItem newItem = lvwMapMarkers.Items.Add(structure.Location.Latitude.Value.ToString("0.00"));
                newItem.SubItems.Add(structure.Location.Longitude.Value.ToString("0.00"));
                newItem.Tag = structure;


                if (structure.ClassName == "RockDrakeNest_C")
                {
                    ArkItem fertileEgg = gameData.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_RockDrake_Fertilized_C" && i.Location != null && i.Location.Latitude.Value.ToString("0.00").Equals(structure.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(structure.Location.Longitude.Value.ToString("0.00")) && i.OwnerInventoryId == null).FirstOrDefault();
                    if (fertileEgg != null)
                    {
                        newItem.BackColor = Color.LightGreen;
                        newItem.ToolTipText = $"{fertileEgg.CustomDescription}";
                    }
                }
                else if (structure.ClassName == "DeinonychusNest_C")
                {
                    ArkItem fertileEgg = gameData.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_Deinonychus_Fertilized_C" && i.Location != null && i.Location.Latitude.Value.ToString("0.00").Equals(structure.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(structure.Location.Longitude.Value.ToString("0.00")) && i.OwnerInventoryId == null).FirstOrDefault();
                    if (fertileEgg != null)
                    {
                        newItem.BackColor = Color.LightGreen;
                        newItem.ToolTipText = $"{fertileEgg.CustomDescription}";
                    }
                }
                else if (structure.ClassName == "CherufeNest_C")
                {
                    ArkItem fertileEgg = gameData.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_Cherufe_Fertilized_C" && i.Location != null && (Math.Abs(Math.Round(i.Location.Latitude.Value,1) - Math.Round(structure.Location.Latitude.Value,1)) <= 0.2) && (Math.Abs(Math.Round(i.Location.Longitude.Value, 1) - Math.Round(structure.Location.Longitude.Value, 1)) <= 0.2) && i.OwnerInventoryId == null && usedEggs.LongCount(e=>e.Id == i.Id) == 0).FirstOrDefault();
                    if (fertileEgg != null)
                    {
                        usedEggs.Add(fertileEgg);
                        newItem.BackColor = Color.LightGreen;
                        newItem.ToolTipText = $"{fertileEgg.CustomDescription}";
                    }
                }
                else if (structure.ClassName.StartsWith("WyvernNest_"))
                {
                    var fertileEggs= gameData.Items.Where(i =>  i.ClassName.ToLower().Contains("egg") && i.Location != null && i.Location.Latitude.Value.ToString("0.00").Equals(structure.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(structure.Location.Longitude.Value.ToString("0.00")) && i.OwnerInventoryId == null);
                    if (fertileEggs != null && fertileEggs.Count() > 0)
                    {
                        newItem.BackColor = Color.LightGreen;
                        newItem.ToolTipText = $"{fertileEggs.First().CustomDescription}";
                    }
                }
                else
                {

                    if (structure.Inventory != null)
                    {
                        //tooltip the inventory contents
                        var itemSummary = structure.Inventory.Where(s => s.ClassName != "PrimalItem_PowerNodeCharge_C").GroupBy(i => i.ClassName).Select(g => new { ClassName = g.Key, FriendlyName = Program.ProgramConfig.ItemMap.Count(im => im.ClassName == g.Key) > 0 ? Program.ProgramConfig.ItemMap.FirstOrDefault(im => im.ClassName == g.Key).FriendlyName : g.Key, Quantity = g.Sum(s => s.Quantity) });
                        if(itemSummary!=null && itemSummary.Count() > 0)
                        {
                            newItem.BackColor = Color.LightGreen;
                            
                            string tooltipText = "";
                            foreach (var summary in itemSummary)
                            {
                                if (tooltipText.Length != 0)
                                {
                                    tooltipText = $"{tooltipText}\n";
                                }

                                string summaryText = $"{summary.FriendlyName} x {summary.Quantity.ToString()}";
                                tooltipText = $"{tooltipText}{summaryText}";

                            }

                            newItem.ToolTipText = tooltipText;

                        }


                    }
                }

            }
        }

        private void lvwMapMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];

            StructureMarker selectedMarker = new StructureMarker();
            if(selectedItem.Tag is ArkStructure)
            {
                ArkStructure selectedStructure = (ArkStructure)selectedItem.Tag;
                selectedMarker.Colour = "White";
                selectedMarker.Lat = (double)selectedStructure.Location?.Latitude.GetValueOrDefault(0);
                selectedMarker.Lon = (double)selectedStructure.Location?.Longitude.GetValueOrDefault(0);
                selectedMarker.X = selectedStructure.Location.X;
                selectedMarker.Y = selectedStructure.Location.Y;
                selectedMarker.Z = selectedStructure.Location.Z;

            }
            else if(selectedItem.Tag is StructureMarker)
            {
                selectedMarker = (StructureMarker)selectedItem.Tag;
            }

            txtContents.Text = selectedItem.ToolTipText.Replace("\n", Environment.NewLine);
            
            HighlightStructure?.Invoke(this, selectedMarker);
        }

        private void lvwMapMarkers_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStructureLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            HighlightStructure?.Invoke(this, null);
        }

        private void lvwMapMarkers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwMapMarkers.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Markers == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Markers)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Markers.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Markers.Text = SortingColumn_Markers.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Markers = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Markers.Text = "> " + SortingColumn_Markers.Text;
            }
            else
            {
                SortingColumn_Markers.Text = "< " + SortingColumn_Markers.Text;
            }

            // Create a comparer.
            lvwMapMarkers.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwMapMarkers.Sort();
        }

        private void lvwMapMarkers_DoubleClick(object sender, EventArgs e)
        {

            
        }

        private void btnCopyCommand_Click(object sender, EventArgs e)
        {

            if (cboConsoleCommands.SelectedItem == null) return;
            if (lvwMapMarkers.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];
            StructureMarker selectedMarker = new StructureMarker();

            if(selectedItem.Tag is ArkStructure)
            {
                ArkStructure selectedStructure = (ArkStructure)selectedItem.Tag;
                selectedMarker.Colour = "White";
                selectedMarker.Lat = (double)selectedStructure.Location?.Latitude.GetValueOrDefault(0);
                selectedMarker.Lon = (double)selectedStructure.Location?.Latitude.GetValueOrDefault(0);
                selectedMarker.X = selectedStructure.Location.X;
                selectedMarker.Y = selectedStructure.Location.Y;
                selectedMarker.Z = selectedStructure.Location.Z;
            }
            else if(selectedItem.Tag is StructureMarker)
            {
                selectedMarker = (StructureMarker)selectedItem.Tag;
            }
            
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
                MessageBox.Show($"Command copied to the clipboard:\n\n{commandText}", "Command Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }
    }
}
