using ArkSavegameToolkitNet.Domain;
using ARKViewer.Configuration;
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
        public frmDinoInventoryViewer(ContentTamedCreature tame, List<ContentItem> items)
        {
            InitializeComponent();
            LoadWindowSettings();

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

        private void frmDinoInventoryViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
