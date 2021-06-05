﻿using ARKViewer.Configuration;
using ARKViewer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmMapView : Form
    {

        private static frmMapView inst;
        public static frmMapView GetForm(ContentManager manager)
        {
            if (inst == null || inst.IsDisposed)
            {
                inst  = new frmMapView(manager);
            }
            else
            {
                cm = manager;
            }
                    
            return inst;
        }

        private static ContentManager cm;
        private ColumnHeader SortingColumn_Markers = null;
        private Image currentMapImage = null;
        private int mapMouseDownX;
        private int mapMouseDownY;
        private int mapMouseDownZoom;

        public delegate void MapClickedEventHandler(decimal latitutde, decimal longitude);
        public event MapClickedEventHandler OnMapClicked;

        private void LoadWindowSettings()
        {
            var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);


            if (savedWindow != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Left = savedWindow.Left;
                this.Top = savedWindow.Top;
                this.Width = savedWindow.Width;
                this.Height = savedWindow.Height;
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
                    savedWindow.Left = this.Left;
                    savedWindow.Top = this.Top;
                    savedWindow.Width = this.Width;
                    savedWindow.Height = this.Height;
                }
            }
        }



        public void DrawMapImageTribes(long tribeId, bool showStructures, bool showPlayers, bool showTames, decimal selectedLat, decimal selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImageTribes(tribeId,showStructures,showPlayers,showTames,selectedLat,selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));
        }

        public void DrawMapImageWild(string className, int minLevel, int maxLevel, float filterLat, float filterLon, float filterRadius, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImageWild(className, minLevel, maxLevel, filterLat, filterLon, filterRadius, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));
        }
        public void DrawMapImageTamed(string className, bool includeStored, long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImageTamed(className, includeStored,tribeId,playerId, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));

        }
        public void DrawMapImageDroppedItems(long droppedPlayerId, string droppedClass, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImageDroppedItems(droppedPlayerId,droppedClass, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));

        }
        public void DrawMapImageDropBags(long droppedPlayerId, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImageDropBags(droppedPlayerId, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));
        }
        public void DrawMapImagePlayerStructures(string className, long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImagePlayerStructures(className, tribeId,playerId, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));

        }
        public void DrawMapImagePlayers(long tribeId, long playerId, decimal? selectedLat, decimal? selectedLon)
        {
            var c = Program.ProgramConfig;
            DrawMapImage(cm.GetMapImagePlayers(tribeId,playerId, selectedLat, selectedLon, c.Obelisks, c.Glitches, c.ChargeNodes, c.BeaverDams, c.DeinoNests, c.WyvernNests, c.DrakeNests, c.MagmaNests, c.OilVeins, c.WaterVeins, c.GasVeins, c.Artifacts, CustomMarkers));
        }

        private void DrawMapImage(Image map)
        {
            picMap.Image = map;
            currentMapImage = map;
            btnSave.Enabled = true;
        }

        public List<ContentMarker> CustomMarkers { get; set; } = new List<ContentMarker>();

        private frmMapView(ContentManager manager)
        {
            InitializeComponent();
            LoadWindowSettings();

            cm = manager;

            if (ARKViewer.Program.ProgramConfig.Zoom > 0)
            {
                trackZoom.Value = ARKViewer.Program.ProgramConfig.Zoom;
            }
        }

        
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using(frmMapToolboxStructures mapSettings = frmMapToolboxStructures.GetForm(this,cm))
            {
                mapSettings.Owner = this;
                mapSettings.ShowDialog();
                DrawTestMap(0,0);

            }
        }

        private void UpdateZoomLevel()
        {
            var newSize = 1024 * ((double)trackZoom.Value / 100.0);
            picMap.Width = (int)newSize;
            picMap.Height = (int)newSize;

            Program.ProgramConfig.Zoom = trackZoom.Value;
        }

        public void DrawTestMap(decimal selectedX, decimal selectedY)
        {


            picMap.Image = cm.GetMapImageMapStructures(CustomMarkers, selectedY, selectedX);
        }

        private void trackZoom_Scroll(object sender, EventArgs e)
        {
            UpdateZoomLevel();
        }

        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            mapMouseDownX = e.X;
            mapMouseDownY = e.Y;
            mapMouseDownZoom = trackZoom.Value;
        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            int movementY = e.Y - mapMouseDownY;
            int movementX = e.X - mapMouseDownX;


            if (e.Button == MouseButtons.Left)
            {
                picMap.Left = picMap.Left + movementX;
                picMap.Top = picMap.Top + movementY;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (movementY > 0)
                {
                    if ((mapMouseDownZoom + movementY) <= trackZoom.Maximum)
                    {
                        trackZoom.Value = mapMouseDownZoom + movementY;
                    }
                }
                else if (movementY < 0)
                {
                    if ((mapMouseDownZoom + movementY) >= trackZoom.Minimum)
                    {
                        trackZoom.Value = mapMouseDownZoom + movementY;
                    }
                }
            }
        }

        private void picMap_MouseClick(object sender, MouseEventArgs e)
        {
            double zoomLevel = (double)picMap.Height / (double)picMap.Image.Height;
            double clickY = e.Location.Y / (zoomLevel);
            double clickX = e.Location.X / (zoomLevel);

            double latitude = clickY / 10.25;
            double longitude = clickX / 10.25;

            var t = Screen.PrimaryScreen.BitsPerPixel;

            OnMapClicked?.Invoke((decimal)latitude, (decimal)longitude);
        }

        private void trackZoom_ValueChanged(object sender, EventArgs e)
        {
            UpdateZoomLevel();
        }

        

        private void frmMapView_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }

        private void btnMapStructures_Click(object sender, EventArgs e)
        {
            ShowMapStructures();
        }

        private void btnMapMarkers_Click(object sender, EventArgs e)
        {
            ShowMapMarkers();
        }

        private void ShowMapStructures()
        {
            frmMapToolboxStructures mapSettings = frmMapToolboxStructures.GetForm(this, cm);
            {
                mapSettings.Show();
                mapSettings.BringToFront();
                DrawTestMap(0, 0);
            }
        }
        private void ShowMapMarkers()
        {
            frmMapToolboxMarkers mapSettings = frmMapToolboxMarkers.GetForm(this);
            {
                mapSettings.Show();
                mapSettings.BringToFront();
                DrawTestMap(0, 0);
            }
        }

        private void picMap_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentMapImage == null) return;

            using(SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = "png";
                dialog.Filter = "PNG (*.png)|*.png";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileFolder = Path.GetDirectoryName(dialog.FileName);
                    if (!Directory.Exists(fileFolder)) Directory.CreateDirectory(fileFolder);
                    currentMapImage.Save(dialog.FileName);
                    MessageBox.Show("Map image saved.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
