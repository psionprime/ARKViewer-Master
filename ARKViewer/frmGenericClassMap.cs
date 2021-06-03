using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
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
    public partial class frmGenericClassMap : Form
    {

        private ColumnHeader SortingColumn_ClassMap = null;
        public IGenericClassMap ClassMap { get; set; } = null;

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
        public frmGenericClassMap(IGenericClassMap selectedClassMap) 
        {
            InitializeComponent();
            LoadWindowSettings();

            ClassMap = selectedClassMap;
            txtClassName.Text = selectedClassMap.ClassName;
            txtDisplayName.Text = selectedClassMap.FriendlyName;

        }

        private void txtClassName_Validating(object sender, CancelEventArgs e)
        {


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClassMap.ClassName = txtClassName.Text;
            ClassMap.FriendlyName = txtDisplayName.Text;
        }

        private void frmGenericClassMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
