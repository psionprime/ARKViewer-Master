using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
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
    public partial class frmItemClassMap : Form
    {
        ColumnHeader SortingColumn_ClassMap = null;
        string imageFolder = "";
        string loadedClassName = "";
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

        public ItemClassMap ClassMap { get; set; } = new ItemClassMap();

        public frmItemClassMap()
        {
            InitializeComponent();
            LoadWindowSettings();

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            txtClassName.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            picIcon.Image = ARKViewer.Properties.Resources.marker_0;

            txtClassName.ReadOnly = false;
        }

        public frmItemClassMap(ItemClassMap selectedMap)
        {
            InitializeComponent();

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

            txtClassName.ReadOnly = true;

            loadedClassName = selectedMap.ClassName;
            
            ClassMap = selectedMap;
            txtClassName.Text = selectedMap.ClassName;
            txtDisplayName.Text = selectedMap.FriendlyName;
            txtCategory.Text = selectedMap.Category;

            picIcon.Image = ARKViewer.Properties.Resources.marker_0;
            if(selectedMap.Image.Length > 0)
            {
                string imageFilename = Path.Combine(imageFolder, selectedMap.Image);
                if (File.Exists(imageFilename))
                {
                    picIcon.Image = Image.FromFile(imageFilename);
                }
            }
        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "All Supported Images|*.ico;*.png;*.jpg;*.bmp";
                dialog.Title = "Select Marker Icon";
                dialog.InitialDirectory = imageFolder;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Image img = Image.FromFile(dialog.FileName))
                    {
                        string fileName = dialog.FileName;
                        var mapIcon = img.GetThumbnailImage(30, 30, () => { return true; }, IntPtr.Zero);
                        picIcon.Image = mapIcon;

                        if (Path.GetDirectoryName(fileName) != Path.GetDirectoryName(imageFolder))
                        {
                            //not already in image folder, save for future use
                            var newFilename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png";
                            fileName = Path.Combine(imageFolder, newFilename);
                            mapIcon.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        var fileNameOnly = Path.GetFileName(fileName);
                        ClassMap.Image = fileNameOnly;
                    }
                }

            }
        }

        private void txtClassName_Validating(object sender, CancelEventArgs e)
        {
            bool canUse = txtClassName.Text.Trim().ToLower() == loadedClassName.ToLower() ||  !Program.ProgramConfig.ItemMap.Any(m => m.ClassName.ToLower() == txtClassName.Text.Trim().ToLower());

            if (!canUse)
            {
                MessageBox.Show("Class name already in use.\n\nPlease edit the class name or close and edit the existing class map.", "Already Used", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ClassMap.ClassName = txtClassName.Text.Trim();
        }

        private void txtDisplayName_Validating(object sender, CancelEventArgs e)
        {
            ClassMap.FriendlyName = txtDisplayName.Text.Trim();
        }

        private void txtCategory_Validating(object sender, CancelEventArgs e)
        {
            ClassMap.Category = txtClassName.Text.Trim();
        }

        private void frmItemClassMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
