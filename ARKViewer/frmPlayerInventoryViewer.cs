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
    public delegate EventHandler InventoryHighlightEvent(float x, float y);


    public partial class frmPlayerInventoryViewer : Form
    {
        public event InventoryHighlightEvent HighlightInventoryEvent;
        
        private ColumnHeader SortingColumn_Player = null;
        private ColumnHeader SortingColumn_Creature = null;
        private ColumnHeader SortingColumn_Storage = null;

        private ColumnHeader SortingColumn_Scores = null;


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



        ContentManager cm = null;
        ContentPlayer currentPlayer = null;
        List<ContentItem> playerInventory = new List<ContentItem>();
        ContentTribe playerTribe = new ContentTribe();

        private void PopulatePersonalInventory()
        {
            //populate personal inventory list
            lvwPlayerInventory.Items.Clear();

            if (playerInventory != null)
            {
                //var playerItems = selectedPlayer.Creatures;

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

                Parallel.ForEach(playerInventory, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.DisplayName;
                        categoryName = itemMap.Category;
                        itemIcon = Program.GetItemImageIndex(itemMap.Image);
                    }

                    if (invItem.IsBlueprint) itemName += " (Blueprint)";
                    
                    if (itemName.ToLower().Contains(txtPlayerFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtPlayerFilter.Text.ToLower()))
                    {
                        if (!invItem.IsEngram)
                        {
                            string qualityName = "";
                            Color backColor = SystemColors.Window;
                            Color foreColor = SystemColors.WindowText;

                            string craftedBy = "";
                            if (invItem.CraftedByPlayer != null && invItem.CraftedByPlayer.Length > 0)
                            {
                                craftedBy = $"{invItem.CraftedByPlayer} ({invItem.CraftedByTribe})";
                            }

                            ListViewItem newItem = new ListViewItem(itemName);
                            newItem.ForeColor = foreColor;
                            newItem.BackColor = backColor;
                            newItem.SubItems.Add(categoryName);
                            newItem.SubItems.Add(qualityName);
                            newItem.SubItems.Add(craftedBy);
                            newItem.SubItems.Add(invItem.Quantity.ToString());
                            newItem.ImageIndex = itemIcon - 1;

                            listItems.Add(newItem);
                        }
                    }
                });

                lvwPlayerInventory.Items.AddRange(listItems.ToArray());
            }
        }

        private void PopulateMissionScores()
        {

        }

        private void PopulateCreatureInventory()
        {

            //item, category, name, lat, lon, qty
            lvwCreatureInventory.Items.Clear();
            ComboValuePair selectedItem = (ComboValuePair)cboCreatureType.SelectedItem;
            string selectedClass = selectedItem.Key;
            var selectedCreatures = playerTribe.Tames.Where(t => t.ClassName == selectedClass || selectedClass.Length == 0);

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

            Parallel.ForEach(selectedCreatures, creature =>
            {
                if (creature.InventoryId.GetValueOrDefault(0) != 0)
                {
                    var inventory = cm.GetInventory(creature.InventoryId.GetValueOrDefault(0));
                    foreach (var invItem in inventory.Items)
                    {
                        string itemName = invItem.ClassName;
                        string categoryName = "Misc.";
                        string creatureName = creature.ClassName;

                        if (creature.Name == null)
                        {
                            var classMap = ARKViewer.Program.ProgramConfig.DinoMap.FirstOrDefault<DinoClassMap>(d => d.ClassName == creature.ClassName);
                            if (classMap != null)
                            {
                                creatureName = classMap.FriendlyName;
                            }
                        }
                        else
                        {
                            creatureName = creature.Name;
                        }

                        int itemIcon = 0;

                        if (ARKViewer.Program.ProgramConfig.ItemMap != null)
                        {
                            var itemMap = ARKViewer.Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                            if (itemMap != null && itemMap.DisplayName != null)
                            {
                                itemName = itemMap.DisplayName;
                                categoryName = itemMap.Category;
                            }
                        }

                        if (itemName.ToLower().Contains(txtCreatureFilter.Text.ToLower()) || creatureName.ToLower().Contains(txtCreatureFilter.Text.ToLower()))
                        {
                            if (!invItem.IsEngram)
                            {

                                string qualityName = "";
                                Color backColor = SystemColors.Window;
                                Color foreColor = SystemColors.WindowText;

                                string craftedBy = "";
                                if (invItem.CraftedByPlayer != null && invItem.CraftedByPlayer.Length > 0)
                                {
                                    craftedBy = $"{invItem.CraftedByPlayer} ({invItem.CraftedByTribe})";
                                }

                                ListViewItem newItem = new ListViewItem(itemName);
                                newItem.BackColor = backColor;
                                newItem.ForeColor = foreColor;
                                newItem.SubItems.Add(categoryName);
                                newItem.SubItems.Add(qualityName);
                                newItem.SubItems.Add(craftedBy);

                                newItem.SubItems.Add(creatureName);
                                newItem.SubItems.Add(creature.Latitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(creature.Longitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(invItem.Quantity.ToString());
                                newItem.ImageIndex = itemIcon - 1;
                                newItem.Tag = invItem;

                                listItems.Add(newItem);
                            }
                        }


                    }
                }
            });

            lvwCreatureInventory.Items.AddRange(listItems.ToArray());
        }

        private void PopulateStructureInventory()
        {
            //item, category, container, lat, lon, qty
            lvwStorageInventory.Items.Clear();
            ComboValuePair selectedItem = (ComboValuePair)cboStorageType.SelectedItem;
            string selectedClass = selectedItem.Key;
            var selectedContainers = playerTribe.Structures.Where(t => t.ClassName == selectedClass || selectedClass.Length == 0).Distinct();

            List<string> unmappedItemClassList = new List<string>();

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
            Parallel.ForEach(selectedContainers, container =>
            {
                if (container.InventoryId.GetValueOrDefault(0) != 0)
                {
                    var inventory = cm.GetInventory(container.InventoryId.GetValueOrDefault(0));

                    foreach (var invItem in inventory.Items)
                    {
                        string itemName = invItem.ClassName;
                        string categoryName = "Misc.";
                        string containerName = container.ClassName;

                        var classMap = ARKViewer.Program.ProgramConfig.StructureMap.FirstOrDefault<StructureClassMap>(d => d.ClassName == container.ClassName);
                        if (classMap != null)
                        {
                            containerName = classMap.FriendlyName;
                        }

                        int itemIcon = 0;

                        if (ARKViewer.Program.ProgramConfig.ItemMap != null)
                        {
                            var itemMap = ARKViewer.Program.ProgramConfig.ItemMap.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                            if (itemMap != null && itemMap.DisplayName != null)
                            {
                                itemName = itemMap.DisplayName;
                                categoryName = itemMap.Category;
                            }
                        }


                        if (categoryName.ToLower().Contains(txtStorageFilter.Text.ToLower()) || itemName.ToLower().Contains(txtStorageFilter.Text.ToLower()))
                        {
                            if (!invItem.IsEngram)
                            {

                                string qualityName = "";
                                Color backColor = SystemColors.Window;
                                Color foreColor = SystemColors.WindowText;

                                string craftedBy = "";
                                if (invItem.CraftedByPlayer != null && invItem.CraftedByPlayer.Length > 0)
                                {
                                    craftedBy = $"{invItem.CraftedByPlayer} ({invItem.CraftedByTribe})";
                                }


                                ListViewItem newItem = new ListViewItem(itemName);
                                newItem.BackColor = backColor;
                                newItem.ForeColor = foreColor;

                                newItem.SubItems.Add(categoryName);
                                newItem.SubItems.Add(qualityName);
                                newItem.SubItems.Add(craftedBy);
                                newItem.SubItems.Add(containerName);
                                newItem.SubItems.Add(container.Latitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(container.Longitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(invItem.Quantity.ToString());
                                newItem.ImageIndex = itemIcon - 1;
                                newItem.Tag = invItem;

                                listItems.Add(newItem);
                            }
                        }

                    }
                }
            });

            lvwStorageInventory.Items.AddRange(listItems.ToArray());

        }


        public frmPlayerInventoryViewer(ContentManager manager, ContentPlayer selectedPlayer)
        {
            InitializeComponent();
            LoadWindowSettings();

            cm = manager;
            currentPlayer = selectedPlayer;
            playerTribe = cm.GetPlayerTribe(currentPlayer.Id);
            playerInventory = new List<ContentItem>();
            
            var inventCheck = cm.GetInventory(currentPlayer.InventoryId.GetValueOrDefault(0));
            if (inventCheck != null && inventCheck.Items != null) playerInventory = inventCheck.Items;

            var inventory = cm.GetInventory(currentPlayer.InventoryId.GetValueOrDefault(0));
            if (inventory != null)
            {
                playerInventory = inventory.Items;
            }

            lvwCreatureInventory.SmallImageList = Program.ItemImageList;
            lvwCreatureInventory.LargeImageList = Program.ItemImageList;
            lvwPlayerInventory.SmallImageList = Program.ItemImageList;
            lvwPlayerInventory.LargeImageList = Program.ItemImageList;
            lvwStorageInventory.LargeImageList = Program.ItemImageList;
            lvwStorageInventory.SmallImageList = Program.ItemImageList;

            currentPlayer = selectedPlayer;
            lblPlayerName.Text = selectedPlayer.CharacterName;
            lblPlayerLevel.Text = selectedPlayer.Level.ToString();

            var tribe = cm.GetPlayerTribe(currentPlayer.Id);
            lblTribeName.Text = tribe.TribeName;
            picPlayerGender.Image = selectedPlayer.Gender == "Male" ? ARKViewer.Properties.Resources.marker_28 : ARKViewer.Properties.Resources.marker_29;

            lblPlayerId.Text = $"Player Id: {selectedPlayer.Id.ToString()}";
            
            PopulateMissionScores();

            PopulatePersonalInventory();



            //get list of tamed dino types
            cboCreatureType.Items.Clear();
            cboCreatureType.Items.Add(new ComboValuePair("", "[All]"));
            var tamedCreatureTypes = playerTribe.Tames.GroupBy(t => t.ClassName)
                                                        .Select(g => new ComboValuePair() { Key = g.Key, Value = (ARKViewer.Program.ProgramConfig.DinoMap.FirstOrDefault(d => d.ClassName == g.Key)==null?g.Key: ARKViewer.Program.ProgramConfig.DinoMap.First(d => d.ClassName == g.Key).FriendlyName) })
                                                        .OrderBy(o => o.Value);
                        
            cboCreatureType.Items.AddRange(tamedCreatureTypes.ToArray());

            cboCreatureType.SelectedIndex = 0;


            //get list of inventory containers
            List<ContentStructure> tribeStructures = new List<ContentStructure>();
            var playerStructures = playerTribe.Structures.Where(s => s.InventoryId.GetValueOrDefault(0)!=0);
            if (playerStructures != null && playerStructures.Count() > 0)
            {
                tribeStructures.AddRange(playerStructures);
            }


            var containerTypes = tribeStructures.GroupBy(s => s.ClassName)
                                    .Select(g => new ComboValuePair() { Key = g.Key, Value = (ARKViewer.Program.ProgramConfig.StructureMap.FirstOrDefault(d => d.ClassName == g.Key)==null ? g.Key: ARKViewer.Program.ProgramConfig.StructureMap.First(d => d.ClassName == g.Key).FriendlyName) })
                                    .OrderBy(o=>o.Value);

            cboStorageType.Items.Clear();
            cboStorageType.Items.Add(new ComboValuePair("", "[All]"));
            cboStorageType.Items.AddRange(containerTypes.ToArray());
            cboStorageType.SelectedIndex = 0;

        }

        private void lvwPlayerInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lvwCreatureInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwCreatureInventory.SelectedItems.Count == 0)
            {
                return;
            }


            float lat = 0f;
            float lon = 0f;
            
            ListViewItem item = lvwCreatureInventory.SelectedItems[0];
            float.TryParse(item.SubItems[3].Text, out lat);
            float.TryParse(item.SubItems[4].Text, out lon);

            HighlightInventoryEvent?.Invoke(lat, lon);

            
        }

        private void lvwStorageInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwStorageInventory.SelectedItems.Count == 0)
            {
                return;
            }

            float lat = 0f;
            float lon = 0f;

            ListViewItem item = lvwStorageInventory.SelectedItems[0];
            float.TryParse(item.SubItems[3].Text, out lat);
            float.TryParse(item.SubItems[4].Text, out lon);

            HighlightInventoryEvent?.Invoke(lat, lon);


        }

        

        private void cboCreatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCreatureFilter.Text = string.Empty;
            PopulateCreatureInventory();
        }

       

        private void cboStorageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStorageFilter.Text = string.Empty;
            PopulateStructureInventory();
        }

        private void lvwPlayerInventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwPlayerInventory.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Player == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Player)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Player.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Player.Text = SortingColumn_Player.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Player = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Player.Text = "> " + SortingColumn_Player.Text;
            }
            else
            {
                SortingColumn_Player.Text = "< " + SortingColumn_Player.Text;
            }

            // Create a comparer.
            lvwPlayerInventory.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwPlayerInventory.Sort();
        }

        private void lvwCreatureInventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwCreatureInventory.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Creature == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Creature)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Creature.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Creature.Text = SortingColumn_Creature.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Creature = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Creature.Text = "> " + SortingColumn_Creature.Text;
            }
            else
            {
                SortingColumn_Creature.Text = "< " + SortingColumn_Creature.Text;
            }

            // Create a comparer.
            lvwCreatureInventory.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwCreatureInventory.Sort();
        }

        private void lvwStorageInventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwStorageInventory.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Storage == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Storage)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Storage.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Storage.Text = SortingColumn_Storage.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Storage = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Storage.Text = "> " + SortingColumn_Storage.Text;
            }
            else
            {
                SortingColumn_Storage.Text = "< " + SortingColumn_Storage.Text;
            }

            // Create a comparer.
            lvwStorageInventory.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwStorageInventory.Sort();
        }

        private void txtPlayerFilter_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCreatureFilter_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtStorageFilter_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void chkFilterStorage_CheckedChanged(object sender, EventArgs e)
        {

            txtStorageFilter.Enabled = !chkApplyFilterStorage.Checked;
            if (chkApplyFilterStorage.Checked)
            {
                txtStorageFilter.Enabled = false;
            }
            else
            {
                txtStorageFilter.Enabled = true;
                txtStorageFilter.Text = string.Empty;
                txtStorageFilter.Focus();

            }
            PopulateStructureInventory();
        }

        private void chkApplyFilterPlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplyFilterPlayer.Checked)
            {
                txtPlayerFilter.Enabled = false;
            }
            else
            {
                txtPlayerFilter.Text = string.Empty;
                txtPlayerFilter.Enabled = true;
                txtPlayerFilter.Focus();
            }

            PopulatePersonalInventory();
        }

        private void chkApplyFilterCreature_CheckedChanged(object sender, EventArgs e)
        {
            txtCreatureFilter.Enabled = !chkApplyFilterCreature.Checked;
            if (chkApplyFilterCreature.Checked)
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

        private void lvwPlayerScores_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwPlayerScores.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Scores == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Scores)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Scores.Text.StartsWith("> "))
                    {
                        sort_order = SortOrder.Descending;
                    }
                    else
                    {
                        sort_order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // New column. Sort ascending.
                    sort_order = SortOrder.Ascending;
                }

                // Remove the old sort indicator.
                SortingColumn_Scores.Text = SortingColumn_Scores.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Scores = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Scores.Text = "> " + SortingColumn_Scores.Text;
            }
            else
            {
                SortingColumn_Scores.Text = "< " + SortingColumn_Scores.Text;
            }

            // Create a comparer.
            lvwPlayerScores.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwPlayerScores.Sort();
        }

        private void frmPlayerInventoryViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateWindowSettings();
        }
    }
}
