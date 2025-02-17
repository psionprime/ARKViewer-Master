﻿using ARKViewer.Configuration;
using ARKViewer.CustomNameMaps;
using ARKViewer.Models;
using ARKViewer.Models.ASVPack;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARKViewer
{
    public partial class frmStructureInventoryViewer : Form
    {
        ContentStructure loadedStructure = null;
        List<ContentItem> loadedInventory = new List<ContentItem>();


        private void LoadWindowSettings()
        {
            var savedWindow = ARKViewer.Program.ProgramConfig.Windows.FirstOrDefault(w => w.Name == this.Name);

            if (savedWindow != null)
            {
                var targetScreen = Screen.FromPoint(new Point(savedWindow.Left, savedWindow.Top));
                if (targetScreen == null) return;

                if (targetScreen.DeviceName == null || targetScreen.DeviceName == savedWindow.Monitor)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Left = savedWindow.Left;
                    this.Top = savedWindow.Top;
                    this.Width = savedWindow.Width;
                    this.Height = savedWindow.Height;
                }
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
                    var restoreScreen = Screen.FromHandle(this.Handle);

                    savedWindow.Left = this.Left;
                    savedWindow.Top = this.Top;
                    savedWindow.Width = this.Width;
                    savedWindow.Height = this.Height;
                    savedWindow.Monitor = restoreScreen.DeviceName;

                }
            }
        }



        public frmStructureInventoryViewer(ContentStructure structure, List<ContentItem> inventory)
        {
            InitializeComponent();
            LoadWindowSettings();
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
                        itemName = itemMap.DisplayName;
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

        private void frmStructureInventoryViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
