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
    public partial class frmDinoInventoryViewer : Form
    {
        private ArkTamedCreature currentCreature = null;

        public frmDinoInventoryViewer(ArkGameData gameData, ArkTamedCreature selectedCreature)
        {
            InitializeComponent();

            currentCreature = selectedCreature;

            string dinoName = selectedCreature.Name != null ? selectedCreature.Name : string.Empty;

            if(dinoName.Length == 0)
            {
                dinoName = selectedCreature.ClassName;
                DinoClassMap classMap = Program.ProgramConfig.DinoMap.Where(d => d.ClassName == selectedCreature.ClassName).FirstOrDefault();
                if (classMap != null && classMap.FriendlyName.Length > 0)
                {
                    dinoName = classMap.FriendlyName;
                }
            }

            lblPlayerName.Text = dinoName;
            lblPlayerLevel.Text = selectedCreature.Level.ToString();
            lblTribeName.Text = selectedCreature.TribeName;

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

            PopulateCreatureInventory();

        }

        private void PopulateCreatureInventory()
        {
            lvwCreatureInventory.Items.Clear();
            if (currentCreature.Inventory != null)
            {
                //var playerItems = selectedPlayer.Creatures;

                foreach (var invItem in currentCreature.Inventory)
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

                    if (itemName.ToLower().Contains(txtCreatureFilter.Text.ToLower()) || categoryName.ToLower().Contains(txtCreatureFilter.Text.ToLower()))
                    {
                        if (!invItem.IsEngram)
                        {
                            ListViewItem newItem = lvwCreatureInventory.Items.Add(itemName);
                            newItem.SubItems.Add(categoryName);
                            newItem.SubItems.Add(invItem.Quantity.ToString());
                            newItem.ImageIndex = itemIcon - 1;

                        }
                    }
                }
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
    }
}
