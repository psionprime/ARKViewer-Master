using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
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
        public event EventHandler<StructureMarker> HighlightStructure;
        ContentManager cm = null;

        public frmStructureLocations(ContentManager manager, List<ContentStructure> structures, string title, Image icon)
        {
            InitializeComponent();

            lblType.Text = title;
            picType.Image = icon;
            cm = manager;

            lvwMapMarkers.Items.Clear();

            
            foreach(var structure in structures)
            {
                ListViewItem newItem = lvwMapMarkers.Items.Add(structure.Latitude.Value.ToString("0.00"));
                newItem.SubItems.Add(structure.Longitude.Value.ToString("0.00"));
                newItem.Tag = structure;

                var inventory = cm.GetInventory(structure.InventoryId.GetValueOrDefault(0));
                if (inventory!=null && inventory.Items.Count > 0)
                {
                    newItem.BackColor = Color.LightGreen;    
                }

            }
        }

        private void lvwMapMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];

            StructureMarker selectedMarker = new StructureMarker();
            
            ContentStructure selectedStructure = (ContentStructure)selectedItem.Tag;
            selectedMarker.Colour = "White";
            selectedMarker.Lat = (double)selectedStructure.Latitude.GetValueOrDefault(0);
            selectedMarker.Lon = (double)selectedStructure.Longitude.GetValueOrDefault(0);
            selectedMarker.X = selectedStructure.X;
            selectedMarker.Y = selectedStructure.Y;
            selectedMarker.Z = selectedStructure.Z;

            StringBuilder inventString = new StringBuilder();
            var inventory = cm.GetInventory(selectedStructure.InventoryId.GetValueOrDefault(0));
            if(inventory!=null && inventory.Items.Count > 0)
            {
                foreach(var item in inventory.Items)
                {
                    string friendlyName = item.CustomName == null? "": item.CustomName;
                    if(friendlyName.Length == 0)
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
            txtContents.Text = inventString.ToString();
            
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
                MessageBox.Show($"Command copied to the clipboard:\n\n{commandText}", "Command Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }
    }
}
