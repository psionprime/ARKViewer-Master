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


        public frmGenericClassMap(IGenericClassMap selectedClassMap) 
        {
            InitializeComponent();

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
    }
}
