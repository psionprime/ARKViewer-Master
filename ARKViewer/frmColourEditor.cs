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
    public partial class frmColourEditor : Form
    {
        public ColourMap SelectedMap { get; set; } = new ColourMap();
        bool isLoading = false;

        public frmColourEditor()
        {
            InitializeComponent();
        }

        public frmColourEditor(ColourMap selectedMap)
        {
            InitializeComponent();

            SelectedMap = selectedMap;

            isLoading = true;
            udId.Value = selectedMap.Id;
            udId.Enabled = false;

            udR.Value = selectedMap.Color.R;
            udG.Value = selectedMap.Color.G;
            udB.Value = selectedMap.Color.B;

            pnlColour.BackColor = selectedMap.Color;
            isLoading = false;

        }

        private void pnlColour_Click(object sender, EventArgs e)
        {
            using (ColorDialog picker = new ColorDialog())
            {
                int currentColor = ColorTranslator.ToOle(pnlColour.BackColor);

                picker.CustomColors = new int[] { currentColor };

                if(picker.ShowDialog() == DialogResult.OK)
                {
                    isLoading = true;

                    pnlColour.BackColor = picker.Color;
                    SelectedMap.Hex = ColorTranslator.ToHtml(picker.Color);
                    udR.Value = picker.Color.R;
                    udG.Value = picker.Color.G;
                    udB.Value = picker.Color.R;
                    isLoading = false;
                }

            }


        }

        private void udR_ValueChanged(object sender, EventArgs e)
        {
            UpdateColourPanel();
        }

        private void udG_ValueChanged(object sender, EventArgs e)
        {
            UpdateColourPanel();
        }

        private void udB_ValueChanged(object sender, EventArgs e)
        {
            UpdateColourPanel();
        }

        private void UpdateColourPanel()
        {
            if (isLoading) return;

            Color color = Color.FromArgb((int)udR.Value, (int)udG.Value, (int)udB.Value);
            pnlColour.BackColor = color;
            SelectedMap.Hex = System.Drawing.ColorTranslator.ToHtml(color); 
        }

        private void udId_ValueChanged(object sender, EventArgs e)
        {
            udId.ForeColor = Program.ProgramConfig.ColourMap.Any(c =>  SelectedMap.Id == 0 && c.Id == (int)udId.Value) ? Color.Red : SystemColors.Window;
            if (udId.ForeColor != Color.Red)
            {
                SelectedMap.Id = (int)udId.Value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(udId.ForeColor != Color.Red)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
