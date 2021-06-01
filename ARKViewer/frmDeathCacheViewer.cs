using ArkSavegameToolkitNet.Domain;
using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using System;
using System.Collections.Concurrent;
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
    public partial class frmDeathCacheViewer : Form
    {
        ContentDroppedItem droppedItem = null;
        List<ContentItem> droppedInventory = new List<ContentItem>();

        public frmDeathCacheViewer(ContentDroppedItem dropItem, List<ContentItem> contentItems)
        {
            InitializeComponent();

            droppedItem = dropItem;
            droppedInventory = contentItems;

            string droppedBy = dropItem.DroppedByName;
            lblPlayerName.Text = droppedBy;
            
            PopulateDeathCacheInventory();


        }

        private void PopulateDeathCacheInventory()
        {
            
            lvwInventory.Items.Clear();


            if (droppedInventory!= null && droppedInventory.Count > 0)
            {
                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(droppedInventory, invItem =>
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

        private void chkApplyFilterDinos_CheckedChanged(object sender, EventArgs e)
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
            PopulateDeathCacheInventory();
        }
    }
}
