using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmStructureInventoryViewer : Form
    {
        ContentStructure loadedStructure = null;
        List<ContentItem> loadedInventory = new List<ContentItem>();

        public frmStructureInventoryViewer(ContentStructure structure, List<ContentItem> inventory)
        {
            InitializeComponent();

            loadedStructure = structure;
            loadedInventory = inventory;

            string structureName = structure.ClassName;
            StructureClassMap classMap = Program.ProgramConfig.StructureMap.Where(d => d.ClassName == structure.ClassName).FirstOrDefault();
            if (classMap != null && classMap.FriendlyName.Length > 0)
            {
                structureName = classMap.FriendlyName;
            }
            lblStructureName.Text = structureName;


            PopulateStructureInventory();
        }
    
        private void PopulateStructureInventory()
        {
            lvwInventory.Items.Clear();
            if (loadedInventory != null)
            {
                //var playerItems = selectedPlayer.Creatures;
                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(loadedInventory, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.FriendlyName;
                        itemIcon = Program.GetItemImageIndex(itemMap.Image);
                        categoryName = itemMap.Category;
                    }

                    if (invItem.IsBlueprint) itemName += " (Blueprint)";
                    

                    if (itemName.ToLower().Contains(txtFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtFilter.Text.ToLower()))
                    {
                        if (!invItem.IsEngram)
                        {
                            ListViewItem newItem = new ListViewItem(itemName);
                            newItem.SubItems.Add(categoryName);
                            newItem.SubItems.Add(invItem.Quantity.ToString());
                            newItem.ImageIndex = itemIcon - 1;

                            listItems.Add(newItem);
                        }
                    }
                });

                lvwInventory.Items.AddRange(listItems.ToArray());
            }
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

            PopulateStructureInventory();
        }
    }
}
