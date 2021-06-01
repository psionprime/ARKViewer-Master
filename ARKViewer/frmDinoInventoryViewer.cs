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
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmDinoInventoryViewer : Form
    {
        List<ContentItem> loadedItems = new List<ContentItem>();

        public frmDinoInventoryViewer(ContentTamedCreature tame, List<ContentItem> items)
        {
            InitializeComponent();

            lvwCreatureInventory.LargeImageList = Program.ItemImageList;
            lvwCreatureInventory.SmallImageList = Program.ItemImageList;

            loadedItems = items;

            string dinoName = tame.Name;

            if(dinoName.Length == 0)
            {
                dinoName = tame.ClassName;
                DinoClassMap classMap = Program.ProgramConfig.DinoMap.Where(d => d.ClassName == tame.ClassName).FirstOrDefault();
                if (classMap != null && classMap.FriendlyName.Length > 0)
                {
                    dinoName = classMap.FriendlyName;
                }
            }

            lblPlayerName.Text = dinoName;
            lblPlayerLevel.Text = tame.Level.ToString();
            lblTribeName.Text = tame.TribeName;


            PopulateCreatureInventory();

        }

        private void PopulateCreatureInventory()
        {
            lvwCreatureInventory.Items.Clear();
            if (loadedItems != null && loadedItems.Count > 0)
            {
                //var playerItems = selectedPlayer.Creatures;

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(loadedItems, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.FriendlyName;
                        categoryName = itemMap.Category;
                        itemIcon = Program.GetItemImageIndex(itemMap.Image);
                        
                    }

                    if (invItem.IsBlueprint) itemName += " (Blueprint)";
                    
                    if (itemName.ToLower().Contains(txtCreatureFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtCreatureFilter.Text.ToLower()))
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

                lvwCreatureInventory.Items.AddRange(listItems.ToArray());

            }
        }

        private void chkApplyFilterDinos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplyFilterDinos.Checked)
            {
                txtCreatureFilter.Enabled = false;
            }
            else
            {
                txtCreatureFilter.Enabled = true;
                txtCreatureFilter.Text = string.Empty;
                txtCreatureFilter.Focus();
            }
            PopulateCreatureInventory();
        }

        private void txtCreatureFilter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
