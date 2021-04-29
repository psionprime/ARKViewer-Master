using ArkSavegameToolkitNet.Domain;
using System;
using System.Collections.Concurrent;
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
    public partial class frmDeathCacheViewer : Form
    {
        ArkItem[] inventoryItems;

        public frmDeathCacheViewer(string playerName, string playerTribe, ArkItem[] inventory)
        {
            InitializeComponent();
            lblPlayerName.Text = playerName;
            lblTribeName.Text = playerTribe;
            inventoryItems = inventory;

            //inventory images
            imageList1.Images.Clear();
            int x = 1;
            while (true)
            {
                Image itemImage = (Image)ARKViewer.Properties.Resources.ResourceManager.GetObject($"item_{x}");
                if (itemImage == null)
                {
                    break;
                }

                imageList1.Images.Add(itemImage);
                x++;
            }

            PopulateDeathCacheInventory();


        }

        private void PopulateDeathCacheInventory()
        {
            
            lvwInventory.Items.Clear();
            if (inventoryItems != null && inventoryItems.Length > 0)
            {
                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(inventoryItems, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.FriendlyName;
                        itemIcon = itemMap.Icon;
                        categoryName = itemMap.Category;
                    }

                    if (invItem.IsBlueprint) itemName += " (Blueprint)";
                    float currentDurability = invItem.SavedDurability.GetValueOrDefault(0f);
                    float currentRating = invItem.Rating.GetValueOrDefault(0f);


                    if (invItem.StatValues != null)
                    {
                        /*
                            0 = Effectiveness
                            1 = Armor
                            2 = Max Durability
                            3 = Weapon Damage
                            4 = Weapon Clip Ammo
                            5 = Hypothermic Insulation
                            6 = Weight
                            7 = Hyperthermic Insulation
                        */

                    }

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
