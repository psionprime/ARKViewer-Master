using ArkSavegameToolkitNet.Domain;
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
    public delegate EventHandler InventoryHighlightEvent(float x, float y);

    public partial class frmPlayerInventoryViewer : Form
    {
        public event InventoryHighlightEvent HighlightInventoryEvent;
        private List<ItemClassMap> itemMapList = new List<ItemClassMap>();
        private List<ArkTamedCreature> tamedCreatureList = new List<ArkTamedCreature>();
        private List<ArkStructure> tribeStructureList = new List<ArkStructure>();


        private ColumnHeader SortingColumn_Player = null;
        private ColumnHeader SortingColumn_Creature = null;
        private ColumnHeader SortingColumn_Storage = null;

        private ColumnHeader SortingColumn_Scores = null;

        private ArkPlayer currentPlayer = null;

        private void PopulatePersonalInventory()
        {
            //populate personal inventory list
            lvwPlayerInventory.Items.Clear();
            if (currentPlayer.Inventory != null)
            {
                //var playerItems = selectedPlayer.Creatures;

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

                Parallel.ForEach(currentPlayer.Inventory, invItem =>
                {
                    string itemName = invItem.ClassName;
                    string categoryName = "Misc.";
                    int itemIcon = 0;
                    var itemMap = itemMapList.Where(i => i.ClassName == invItem.ClassName).FirstOrDefault<ItemClassMap>();
                    if (itemMap != null && itemMap.ClassName != "")
                    {
                        itemName = itemMap.FriendlyName;
                        categoryName = itemMap.Category;
                        itemIcon = Program.GetItemImageIndex(itemMap.Image);
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

                    if (itemName.ToLower().Contains(txtPlayerFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtPlayerFilter.Text.ToLower()))
                    {
                        if (!invItem.IsEngram)
                        {
                            string qualityName = "";
                            Color backColor = SystemColors.Window;
                            Color foreColor = SystemColors.WindowText;

                            if (invItem.Rating > 0 && invItem.Rating < 1.25)
                            {
                                qualityName = "Primitive";
                                backColor = Color.FromArgb(90, ColorTranslator.FromHtml("#C0C0C0"));

                                foreColor = Color.Black;
                            }
                            if (invItem.Rating >= 1.25 && invItem.Rating < 2.5)
                            {
                                qualityName = "Ramshackle";
                                backColor = ColorTranslator.FromHtml("#93FFA0");
                                foreColor = Color.Black;
                            }
                            else if (invItem.Rating >= 2.5 && invItem.Rating < 4.5)
                            {
                                qualityName = "Apprentice";
                                backColor = ColorTranslator.FromHtml("#6088FF");
                                foreColor = Color.Black;
                            }
                            else if (invItem.Rating >= 4.5 && invItem.Rating < 7)
                            {
                                qualityName = "Journeyman";
                                backColor = ColorTranslator.FromHtml("#E2AAFF");

                                foreColor = Color.Black;
                            }
                            else if (invItem.Rating >= 7 && invItem.Rating < 10)
                            {
                                qualityName = "Mastercraft";
                                backColor = ColorTranslator.FromHtml("#FFF991");

                                foreColor = Color.Black;
                            }
                            else if (invItem.Rating >= 10)
                            {
                                qualityName = "Ascendant";
                                backColor = ColorTranslator.FromHtml("#8EFFFD");

                                foreColor = Color.Black;
                            }

                            string craftedBy = "";
                            if (invItem.CraftedPlayerName != null && invItem.CraftedPlayerName.Length > 0)
                            {
                                craftedBy = $"{invItem.CraftedPlayerName} ({invItem.CraftedTribeName})";
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

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();


            var missionScores = currentPlayer.MissionScores;
            Parallel.ForEach(missionScores, missionScore =>
            {
                ArkMissionData missionData = missionScore;
                ListViewItem newItem = new ListViewItem(missionData.MissionTag);
                newItem.SubItems.Add(missionData.LastScore.ToString("f2"));
                newItem.SubItems.Add(missionData.BestScore.ToString("f2"));
                listItems.Add(newItem);
            });
            
            
            lvwPlayerScores.Items.AddRange(listItems.ToArray());
        }

        private void PopulateCreatureInventory()
        {

            //item, category, name, lat, lon, qty
            lvwCreatureInventory.Items.Clear();
            ComboValuePair selectedItem = (ComboValuePair)cboCreatureType.SelectedItem;
            string selectedClass = selectedItem.Key;
            var selectedCreatures = tamedCreatureList.Where(t => t.ClassName == selectedClass || selectedClass.Length == 0);

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

            Parallel.ForEach(selectedCreatures, creature =>
            {
                if (creature.Inventory != null && creature.Inventory.Count() > 0)
                {
                    foreach (var invItem in creature.Inventory)
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
                            if (itemMap != null && itemMap.FriendlyName != null)
                            {
                                itemName = itemMap.FriendlyName;
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

                                if (invItem.Rating > 0 && invItem.Rating < 1.25)
                                {
                                    qualityName = "Primitive";
                                    backColor = Color.FromArgb(90, ColorTranslator.FromHtml("#C0C0C0"));

                                    foreColor = Color.Black;
                                }
                                if (invItem.Rating >= 1.25 && invItem.Rating < 2.5)
                                {
                                    qualityName = "Ramshackle";
                                    backColor = ColorTranslator.FromHtml("#93FFA0");
                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 2.5 && invItem.Rating < 4.5)
                                {
                                    qualityName = "Apprentice";
                                    backColor = ColorTranslator.FromHtml("#6088FF");
                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 4.5 && invItem.Rating < 7)
                                {
                                    qualityName = "Journeyman";
                                    backColor = ColorTranslator.FromHtml("#E2AAFF");

                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 7 && invItem.Rating < 10)
                                {
                                    qualityName = "Mastercraft";
                                    backColor = ColorTranslator.FromHtml("#FFF991");

                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 10)
                                {
                                    qualityName = "Ascendant";
                                    backColor = ColorTranslator.FromHtml("#8EFFFD");

                                    foreColor = Color.Black;
                                }

                                string craftedBy = "";
                                if (invItem.CraftedPlayerName != null && invItem.CraftedPlayerName.Length > 0)
                                {
                                    craftedBy = $"{invItem.CraftedPlayerName} ({invItem.CraftedTribeName})";
                                }

                                ListViewItem newItem = new ListViewItem(itemName);
                                newItem.BackColor = backColor;
                                newItem.ForeColor = foreColor;
                                newItem.SubItems.Add(categoryName);
                                newItem.SubItems.Add(qualityName);
                                newItem.SubItems.Add(craftedBy);

                                newItem.SubItems.Add(creatureName);
                                newItem.SubItems.Add(creature?.Location.Latitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(creature?.Location.Longitude.GetValueOrDefault(0).ToString("0.00"));
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
            var selectedContainers = tribeStructureList.Where(t => t.ClassName == selectedClass || selectedClass.Length == 0).Distinct();

            List<string> unmappedItemClassList = new List<string>();

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
            Parallel.ForEach(selectedContainers, container =>
            {
                if (container.Inventory != null && container.Inventory.Count() > 0)
                {
                    foreach (var invItem in container.Inventory)
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
                            if (itemMap != null && itemMap.FriendlyName != null)
                            {
                                itemName = itemMap.FriendlyName;
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

                                if (invItem.Rating > 0 && invItem.Rating < 1.25)
                                {
                                    qualityName = "Primitive";
                                    backColor = Color.FromArgb(90, ColorTranslator.FromHtml("#C0C0C0"));

                                    foreColor = Color.Black;
                                }
                                if (invItem.Rating >= 1.25 && invItem.Rating < 2.5)
                                {
                                    qualityName = "Ramshackle";
                                    backColor = ColorTranslator.FromHtml("#93FFA0");
                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 2.5 && invItem.Rating < 4.5)
                                {
                                    qualityName = "Apprentice";
                                    backColor = ColorTranslator.FromHtml("#6088FF");
                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 4.5 && invItem.Rating < 7)
                                {
                                    qualityName = "Journeyman";
                                    backColor = ColorTranslator.FromHtml("#E2AAFF");

                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 7 && invItem.Rating < 10)
                                {
                                    qualityName = "Mastercraft";
                                    backColor = ColorTranslator.FromHtml("#FFF991");

                                    foreColor = Color.Black;
                                }
                                else if (invItem.Rating >= 10)
                                {
                                    qualityName = "Ascendant";
                                    backColor = ColorTranslator.FromHtml("#8EFFFD");


                                    foreColor = Color.Black;
                                }

                                string craftedBy = "";
                                if (invItem.CraftedPlayerName != null && invItem.CraftedPlayerName.Length > 0)
                                {
                                    craftedBy = $"{invItem.CraftedPlayerName} ({invItem.CraftedTribeName})";
                                }


                                ListViewItem newItem = new ListViewItem(itemName);
                                newItem.BackColor = backColor;
                                newItem.ForeColor = foreColor;

                                newItem.SubItems.Add(categoryName);
                                newItem.SubItems.Add(qualityName);
                                newItem.SubItems.Add(craftedBy);
                                newItem.SubItems.Add(containerName);
                                newItem.SubItems.Add(container?.Location.Latitude.GetValueOrDefault(0).ToString("0.00"));
                                newItem.SubItems.Add(container?.Location.Longitude.GetValueOrDefault(0).ToString("0.00"));
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


        public frmPlayerInventoryViewer(ArkGameData gameData, List<ItemClassMap> availableItemList, ArkPlayer selectedPlayer)
        {
            InitializeComponent();

            lvwCreatureInventory.SmallImageList = Program.ItemImageList;
            lvwCreatureInventory.LargeImageList = Program.ItemImageList;
            lvwPlayerInventory.SmallImageList = Program.ItemImageList;
            lvwPlayerInventory.LargeImageList = Program.ItemImageList;
            lvwStorageInventory.LargeImageList = Program.ItemImageList;
            lvwStorageInventory.SmallImageList = Program.ItemImageList;

            currentPlayer = selectedPlayer;
            lblPlayerName.Text = selectedPlayer.CharacterName;
            lblPlayerLevel.Text = selectedPlayer.CharacterLevel.ToString();
            lblTribeName.Text = selectedPlayer.Tribe != null ? selectedPlayer.Tribe.Name : "";
            picPlayerGender.Image = selectedPlayer.Gender == ArkPlayerGender.Male ? ARKViewer.Properties.Resources.marker_28 : ARKViewer.Properties.Resources.marker_29;

            lblPlayerId.Text = $"Player Id: {selectedPlayer.Id.ToString()}";
            
            //inventory images
            itemMapList = availableItemList;

            PopulateMissionScores();

            PopulatePersonalInventory();


            List<ArkTamedCreature> tribeTameList = new List<ArkTamedCreature>();
            if (selectedPlayer.Tribe != null)
            {
                foreach (var player in selectedPlayer.Tribe.Members)
                {
                    var playerTames = gameData.TamedCreatures.Where(s => s.OwningPlayerId == player.Id || s.ImprinterPlayerDataId == player.Id  || s.TargetingTeam == player.TribeId).Distinct();
                    if (playerTames != null && playerTames.Count() > 0)
                    {
                        tribeTameList.AddRange(playerTames);
                    }
                }


            }
            else
            {
                //going solo
                var playerTames = gameData.TamedCreatures.Where(s => s.OwningPlayerId == selectedPlayer.Id || s.ImprinterPlayerDataId == selectedPlayer.Id).Distinct();
                if (playerTames != null && playerTames.Count() > 0)
                {
                    tribeTameList.AddRange(playerTames);
                }
            }

            tamedCreatureList = tribeTameList.Distinct().ToList();

            //get a list of tamed dinos

            //get list of tamed dino types
            cboCreatureType.Items.Clear();
            cboCreatureType.Items.Add(new ComboValuePair("", "[All]"));
            if(tamedCreatureList!=null && tamedCreatureList.Count > 0)
            {
                var tamedCreatureTypes = tamedCreatureList.GroupBy(t => t.ClassName)
                                                            .Select(g => new ComboValuePair() { Key = g.Key, Value = (ARKViewer.Program.ProgramConfig.DinoMap.FirstOrDefault(d => d.ClassName == g.Key)==null?g.Key: ARKViewer.Program.ProgramConfig.DinoMap.First(d => d.ClassName == g.Key).FriendlyName) })
                                                            .OrderBy(o => o.Value);
                        
                cboCreatureType.Items.AddRange(tamedCreatureTypes.ToArray());
            }
            cboCreatureType.SelectedIndex = 0;


            //get list of inventory containers
            tribeStructureList = new List<ArkStructure>();
            List<ArkItem> tribeStructures = new List<ArkItem>();
            if (selectedPlayer.Tribe != null)
            {
                foreach (var player in selectedPlayer.Tribe.Members)
                {
                    var playerStructures = gameData.Structures.Where(s => s.Inventory != null && s.Inventory.Count() >0 &&  (s.OwningPlayerId == player.Id || s.TargetingTeam == player.TribeId));
                    if (playerStructures != null && playerStructures.Count() > 0)
                    {
                        tribeStructureList.AddRange(playerStructures);
                    }
                }
            }
            else
            {
                //going solo
                if (selectedPlayer.Inventory != null && selectedPlayer.Inventory.Count() > 0)
                {
                    var playerStructures = gameData.Structures.Where(s => s.Inventory != null && s.Inventory.Count() > 0 && (s.OwningPlayerId == selectedPlayer.Id));
                    if (playerStructures != null && playerStructures.Count() > 0)
                    {
                        foreach (var structureInv in playerStructures)
                        {
                            tribeStructureList.AddRange(playerStructures);
                        }
                    }
                }
            }


            var containerTypes = tribeStructureList.GroupBy(s => s.ClassName)
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
    }
}
