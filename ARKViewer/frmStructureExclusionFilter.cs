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
    public partial class frmStructureExclusionFilter : Form
    {
        private ArkGameData gd = null;
        private List<string> currentExclusions = new List<string>();

        public frmStructureExclusionFilter(ArkGameData gameData)
        {
            InitializeComponent();
            
            gd = gameData;
            currentExclusions = Program.ProgramConfig.StructureExclusions;

            PopulateStructures();
        }

        private void PopulateStructures()
        {
            if (gd == null) return;
            if (gd.Structures == null) return;

            var playerStructureTypes = gd.Structures.Where(s => (s.OwningPlayerId!=null || s.TargetingTeam!=null))
                                            .GroupBy(g => g.ClassName)
                                            .Select(s => s.Key);


            lstStructureFilter.Items.Clear();
            foreach (var className in playerStructureTypes)
            {
                var structureName = className;
                StructureClassMap itemMap = Program.ProgramConfig.StructureMap.Where(i => i.ClassName == className).FirstOrDefault();
                if (itemMap != null && itemMap.FriendlyName.Length > 0)
                {
                    structureName = itemMap.FriendlyName;
                }

                if(className.ToLower().Contains(txtFilter.Text.ToLower()) || structureName.ToLower().Contains(txtFilter.Text.ToLower()))
                {
                    int newIndex = lstStructureFilter.Items.Add(new ComboValuePair() { Key = className, Value = structureName });
                    lstStructureFilter.SetItemChecked(newIndex, Program.ProgramConfig.StructureExclusions.Contains(className));
                }
            }

            //add rafts
            int itemIndex = lstStructureFilter.Items.Add(new ComboValuePair() { Key = "Raft_BP_C", Value = "Raft"});
            lstStructureFilter.SetItemChecked(itemIndex, Program.ProgramConfig.StructureExclusions.Contains("Raft_BP_C"));

            itemIndex = lstStructureFilter.Items.Add(new ComboValuePair() { Key = "MotorRaft_BP_C", Value = "Motorboat" });
            lstStructureFilter.SetItemChecked(itemIndex, Program.ProgramConfig.StructureExclusions.Contains("MotorRaft_BP_C"));

            lstStructureFilter.Sorted = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Program.ProgramConfig.StructureExclusions = currentExclusions;
        }

        private void chkApplyFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplyFilter.Checked)
            {
                txtFilter.Enabled = false;
            }
            else
            {
                txtFilter.Enabled = true;
                txtFilter.Text = string.Empty;
                txtFilter.Focus();
            }

            PopulateStructures();
            

        }

        private void UpdateCheckState()
        {
            if (lstStructureFilter.SelectedIndex >= 0)
            {
                int selectedIndex = lstStructureFilter.SelectedIndex;
                bool currentCheck = lstStructureFilter.GetItemChecked(selectedIndex);
                currentCheck = !currentCheck;
                lstStructureFilter.SetItemChecked(selectedIndex, currentCheck);
                
            }
        }

        private void lstStructureFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(lstStructureFilter.SelectedIndex >= 0)
            {

                ComboValuePair comboValue = (ComboValuePair)lstStructureFilter.Items[e.Index];
                if(e.NewValue == CheckState.Checked)
                {
                    if (!currentExclusions.Contains(comboValue.Key))
                    {
                        currentExclusions.Add(comboValue.Key);
                    }
                }
                else
                {
                    if (currentExclusions.Contains(comboValue.Key))
                    {
                        currentExclusions.Remove(comboValue.Key);
                    }
                }


            }
        }
    }
}
