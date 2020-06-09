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
    public partial class frmMarkerEditor : Form
    {
        private int selectedMarkerIndex = 0;
        private string selectedMap = "TheIsland.ark";
        private List<Image> markerIcons = new List<Image>();
        public MapMarker EditingMarker { get; set; } = new MapMarker();
        private List<MapMarker> markerList = new List<MapMarker>();

        public frmMarkerEditor(string currentMapFile, List<MapMarker> currentMarkers, string selectedMarkerName)
        {
            InitializeComponent();

            markerList = currentMarkers;
            selectedMap = currentMapFile;

            for(int markerIndex = 0; markerIndex <= 32; markerIndex++)
            {

                Image markerImage = (Image)ARKViewer.Properties.Resources.ResourceManager.GetObject($"marker_{markerIndex}");
                if (markerImage != null)
                {
                    markerIcons.Add(markerImage);
                }
            }

            if(selectedMarkerName.Length > 0)
            {
                //attempt to find and load it
                MapMarker selectedMarker = currentMarkers.Where(m => m.Map == currentMapFile && m.Name == selectedMarkerName).FirstOrDefault();
                EditingMarker = selectedMarker;
            }

            txtName.Enabled = selectedMarkerName.Length == 0;

            UpdateDisplay();

        }

        private void UpdateDisplay()
        {
            txtName.Text = "";
            pnlBackgroundColour.BackColor = Color.White;
            pnlBorderColour.BackColor = Color.Black;
            udBorderSize.Value = 0;
            udLat.Value = 0;
            udLon.Value = 0;
            selectedMarkerIndex = 0;
            picIcon.Image = ARKViewer.Properties.Resources.marker_0;

            if (EditingMarker != null)
            {
                txtName.Text = EditingMarker.Name;
                pnlBackgroundColour.BackColor = Color.FromArgb(EditingMarker.Colour);
                pnlBorderColour.BackColor = Color.FromArgb(EditingMarker.BorderColour);
                udBorderSize.Value = EditingMarker.BorderWidth;
                udLat.Value = (decimal)EditingMarker.Lat;
                udLon.Value = (decimal)EditingMarker.Lon;
                selectedMarkerIndex = EditingMarker.Marker;
                UpdateImage();
            }

        }

        private void UpdateImage()
        {
            Image markerImage = (Image)ARKViewer.Properties.Resources.ResourceManager.GetObject($"marker_{selectedMarkerIndex}");
            picIcon.Image = markerImage;
        }




        private void btnNextIcon_Click(object sender, EventArgs e)
        {
            if(selectedMarkerIndex < markerIcons.Count-1)
            {
                selectedMarkerIndex += 1;

            }
            else
            {
                selectedMarkerIndex = 0;
            }

            UpdateImage();

        }

        private void btnPrevIcon_Click(object sender, EventArgs e)
        {
            if (selectedMarkerIndex > 0)
            {
                selectedMarkerIndex -= 1;
            }
            else
            {
                selectedMarkerIndex = markerIcons.Count-1;

            }
            UpdateImage();

        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            bool nameExists = markerList.Count(m => m.Map == selectedMap && m.Name.ToLower() == txtName.Text.ToLower()) > 0;

            if (nameExists)
            {
                if (EditingMarker != null)
                {
                    nameExists = EditingMarker?.Name.ToLower() != txtName.Text;

                }

            }

            if (nameExists)
            {
                MessageBox.Show("Marker name is already in use for this map.\n\nPlease use a different marker name.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }       

            e.Cancel = nameExists;

        }

        private void pnlBorderColour_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pnlBorderColour.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pnlBorderColour.BackColor = colorDialog1.Color;
            }
        }

        private void pnlBackgroundColour_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pnlBackgroundColour.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pnlBackgroundColour.BackColor = colorDialog1.Color;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EditingMarker.Map = selectedMap;
            EditingMarker.Name = txtName.Text;
            EditingMarker.Colour = pnlBackgroundColour.BackColor.ToArgb();
            EditingMarker.BorderColour = pnlBorderColour.BackColor.ToArgb();
            EditingMarker.BorderWidth = (int)udBorderSize.Value;
            EditingMarker.Lat = (double)udLat.Value;
            EditingMarker.Lon = (double)udLon.Value;
            EditingMarker.Marker = selectedMarkerIndex;
        }
    }
}
