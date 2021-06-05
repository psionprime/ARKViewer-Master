﻿using ARKViewer.Configuration;
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
    public partial class frmSettings : Form
    {
        private ColumnHeader SortingColumn_DinoMap = null;
        private ColumnHeader SortingColumn_ItemMap = null;
        private ColumnHeader SortingColumn_StructureMap = null;
        string imageFolder = "";
        ContentManager cm = null;

        Dictionary<string, string> mapFilenameMap = new Dictionary<string, string>
            {
                { "theisland.ark", "The Island" },
                { "thecenter.ark", "The Center" },
                { "scorchedearth_p.ark","Scorched Earth"},
                { "aberration_p.ark", "Aberration"},
                { "extinction.ark", "Extinction"},
                { "ragnarok.ark", "Ragnarok"},
                { "valguero_p.ark", "Valguero" },
                { "crystalisles.ark", "Crystal Isles" },
                { "genesis.ark", "Genesis" },
                { "gen2.ark", "Genesis 2" },
                { "astralark.ark", "AstralARK" },
                { "hope.ark", "Hope"},
                { "tunguska_p.ark", "Tunguska"},
                { "caballus_p.ark", "Caballus"},
                { "viking_p.ark", "Fjördur"},
                { "tiamatprime.ark", "Tiamat Prime"}
            };

        public ViewerConfiguration SavedConfig { get; set; }
        public frmSettings(ContentManager manager)
        {
            InitializeComponent();

            cm = manager;

            lvwItemMap.LargeImageList = Program.ItemImageList;
            lvwItemMap.SmallImageList = Program.ItemImageList;

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            PopulateExportTribes();

            SavedConfig = Program.ProgramConfig;
        }

        private void PopulateExportTribes()
        {
            this.Cursor = Cursors.WaitCursor;

            cboExportTribe.Items.Clear();
            cboExportTribe.Items.Add(new ComboValuePair("0", "[All Tribes]"));

            ConcurrentBag<ComboValuePair> tribeBag = new ConcurrentBag<ComboValuePair>();
            var tribes = cm.GetTribes(0);
            Parallel.ForEach(tribes, tribe =>
            {
                bool shouldAdd = true;
                if (Program.ProgramConfig.HideNoStructures && tribe.Structures.Count == 0) shouldAdd = false;
                if (Program.ProgramConfig.HideNoBody && tribe.Players.All(p=>p.Longitude.GetValueOrDefault(0) ==0 && p.Latitude.GetValueOrDefault(0) == 0)) shouldAdd = false;
                    
                if(shouldAdd) tribeBag.Add(new ComboValuePair(tribe.TribeId.ToString(),tribe.TribeName));
            });

            if(tribeBag!=null &!tribeBag.IsEmpty)
            {
                cboExportTribe.Items.AddRange(tribeBag.ToArray());
            }
            
            this.Cursor = Cursors.Default;
            cboExportTribe.SelectedIndex = 0;
      
        }

        private void PopulateExportPlayers(long tribeId)
        {
            this.Cursor = Cursors.WaitCursor;
            cboExportPlayer.Items.Clear();
            cboExportPlayer.Items.Add(new ComboValuePair("0", "[All Players]"));

            ConcurrentBag<ComboValuePair> playerBag = new ConcurrentBag<ComboValuePair>();
            var tribes = cm.GetTribes(tribeId);
            Parallel.ForEach(tribes, tribe =>
            {
                bool shouldAdd = true;
                if (Program.ProgramConfig.HideNoStructures && tribe.Structures.Count == 0) shouldAdd = false;
                if (shouldAdd)
                {
                    var players = tribe.Players.Where(p => !(Program.ProgramConfig.HideNoBody && (p.Latitude.GetValueOrDefault(0) == 0 && p.Longitude.GetValueOrDefault(0) == 0))).ToList();
                    if(players!=null && players.Count > 0)
                    {
                        foreach(var player in players)
                        {
                            playerBag.Add(new ComboValuePair(player.Id.ToString(), player.CharacterName));
                        }                        
                    }
                }


                
            });

            if (playerBag != null & !playerBag.IsEmpty)
            {
                cboExportPlayer.Items.AddRange(playerBag.ToArray());
            }

            cboExportPlayer.SelectedIndex = 0;

            this.Cursor = Cursors.Default;
        }

        private void PopulateColours()
        {
            this.Cursor = Cursors.WaitCursor;

            //populate class map
            lvwColours.Items.Clear();
            lvwColours.Refresh();
            lvwColours.BeginUpdate();
            if (SavedConfig.DinoMap.Count > 0)
            {
                foreach (var colourMap in SavedConfig.ColourMap.OrderBy(d => d.Id))
                {
                    ListViewItem newItem = lvwColours.Items.Add(colourMap.Id.ToString());
                    newItem.UseItemStyleForSubItems = false;
                    newItem.SubItems.Add(colourMap.Hex);
                    newItem.SubItems.Add("");
                    newItem.SubItems[2].BackColor = colourMap.Color;
                    newItem.Tag = colourMap;
                }

            }

            lvwColours.EndUpdate();

            this.Cursor = Cursors.Default;
        }
        private void PopulateDinoClassMap(string selectedClass)
        {
            this.Cursor = Cursors.WaitCursor;

            btnEditDinoClass.Enabled = false;
            btnRemoveDinoClass.Enabled = false;

            //populate class map
            lvwDinoClasses.Items.Clear();
            lvwDinoClasses.Refresh();
            lvwDinoClasses.BeginUpdate();
            if (SavedConfig.DinoMap.Count > 0)
            {
                foreach (var dino in SavedConfig.DinoMap.OrderBy(d => d.FriendlyName))
                {
                    if(dino.ClassName.ToLower().Contains(txtCreatureFilter.Text.ToLower()) || dino.FriendlyName.ToLower().Contains(txtCreatureFilter.Text.ToLower()))
                    {
                        ListViewItem newItem = lvwDinoClasses.Items.Add(dino.ClassName);
                        newItem.SubItems.Add(dino.FriendlyName);
                        newItem.Tag = dino;


                        if (selectedClass.Length > 0)
                        {
                            if (dino.ClassName.ToLower() == selectedClass.ToLower())
                            {
                                newItem.Selected = true;
                                lvwDinoClasses.EnsureVisible(newItem.Index);
                            }
                        }

                    }

                }



            }

            lvwDinoClasses.EndUpdate();

            this.Cursor = Cursors.Default;
        }

        private void PopulateItemClassMap(string selectedClass)
        {
            this.Cursor = Cursors.WaitCursor;

            btnEditDinoClass.Enabled = false;
            btnRemoveDinoClass.Enabled = false;

            //populate class map
            lvwItemMap.Items.Clear();
            lvwItemMap.Refresh();
            lvwItemMap.BeginUpdate();


            if (SavedConfig.ItemMap.Count > 0)
            {
                string filterText = txtItemFilter.Text;

                foreach (var item in SavedConfig.ItemMap.OrderBy(d => d.FriendlyName))
                {

                    ListViewItem newItem = lvwItemMap.Items.Add(item.Category);
                    newItem.SubItems.Add(item.ClassName);
                    newItem.SubItems.Add(item.FriendlyName);

                    int imageIndex = Program.GetItemImageIndex(item.Image);
                    newItem.ImageIndex = imageIndex -1;

                    newItem.Tag = item;

                    if (selectedClass.Length > 0)
                    {
                        if (item.ClassName.ToLower() == selectedClass.ToLower())
                        {
                            newItem.Selected = true;
                            lvwItemMap.EnsureVisible(newItem.Index);
                        }
                    }

                }

            }
            lvwItemMap.EndUpdate();
            this.Cursor = Cursors.Default;
        }



        private void PopulateStructureClassMap(string selectedClass)
        {
            this.Cursor = Cursors.WaitCursor;

            btnEditStructure.Enabled = false;
            btnRemoveStructure.Enabled = false;

            //populate class map
            lvwStructureMap.Items.Clear();
            lvwStructureMap.Refresh();
            lvwStructureMap.BeginUpdate();


            if (SavedConfig.StructureMap.Count > 0)
            {
                string filterText = txtStructureFilter.Text;

                foreach (var item in SavedConfig.StructureMap.OrderBy(d => d.FriendlyName))
                {

                    ListViewItem newItem = lvwStructureMap.Items.Add(item.FriendlyName);
                    newItem.SubItems.Add(item.ClassName);
                    newItem.Tag = item;

                    if (selectedClass.Length > 0)
                    {
                        if (item.ClassName.ToLower() == selectedClass.ToLower())
                        {
                            newItem.Selected = true;
                            lvwStructureMap.EnsureVisible(newItem.Index);
                        }
                    }

                }

            }
            lvwStructureMap.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void PopulateSinglePlayerGames()
        {

            //get registry path for steam apps 
            cboMapSinglePlayer.Items.Clear();
            string directoryCheck = "";

            try
            {
                string steamRoot = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "").ToString();

                if (steamRoot != null && steamRoot.Length > 0)
                {
                    steamRoot = steamRoot.Replace(@"/", @"\");
                    steamRoot = Path.Combine(steamRoot, @"steamapps\libraryfolders.vdf");
                    if (File.Exists(steamRoot))
                    {
                        string fileText = File.ReadAllText(steamRoot).Replace("\"LibraryFolders\"", "");

                        foreach (string line in fileText.Split('\n'))
                        {
                            if (line.Contains("\t"))
                            {
                                string[] lineContent = line.Split('\t');
                                if (lineContent.Length == 4)
                                {
                                    //check 4th param as a path
                                    directoryCheck = lineContent[3].ToString().Replace("\"", "").Replace(@"\\", @"\") + @"\SteamApps\Common\ARK\ShooterGame\Saved\";
                                }

                            }
                        }
                    }
                }
            }
            catch
            {
                //permission access to registry or unavailable?

            }

            if(directoryCheck.Length == 0)
            {
                //no directory found for steam from registry, ask user.
                if(MessageBox.Show("Unable to determine Steam library folder.\n\nWould you like to select it yourself?", "Steam Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {





                }
            }

            if (Directory.Exists(directoryCheck))
            {

                var saveFiles = Directory.GetFiles(directoryCheck, "*.ark", SearchOption.AllDirectories);
                foreach (string saveFilename in saveFiles)
                {
                    string fileName = Path.GetFileName(saveFilename);
                    if (mapFilenameMap.ContainsKey(fileName.ToLower()))
                    {
                        string knownMapName = mapFilenameMap[fileName.ToLower()];
                        if (knownMapName.Length > 0)
                        {
                            ComboValuePair comboValue = new ComboValuePair(saveFilename, knownMapName);
                            int newIndex = cboMapSinglePlayer.Items.Add(comboValue);

                            if (SavedConfig.Mode == ViewerModes.Mode_SinglePlayer)
                            {
                                if (SavedConfig.SelectedFile == saveFilename)
                                {
                                    cboMapSinglePlayer.SelectedIndex = newIndex;
                                }
                            }

                        }

                    }

                }

            }

        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            PopulateColours();
            PopulateDinoClassMap("");
            PopulateStructureClassMap("");
            PopulateItemClassMap("");
            
            

            chkUpdateNotificationFile.Checked = SavedConfig.UpdateNotificationFile;
            chkUpdateNotificationSingle.Checked = SavedConfig.UpdateNotificationSingle;


            optPlayerStructureHide.Checked = SavedConfig.HideNoStructures;
            optPlayerStructureShow.Checked = !SavedConfig.HideNoStructures;

            optPlayerTameHide.Checked = SavedConfig.HideNoTames;
            optPlayerTameShow.Checked = !SavedConfig.HideNoTames;

            optPlayerBodyHide.Checked = SavedConfig.HideNoBody;
            optPlayerBodyShow.Checked = !SavedConfig.HideNoBody;
            optFTPSync.Checked = SavedConfig.FtpDownloadMode == 0;
            optFTPClean.Checked = SavedConfig.FtpDownloadMode == 1;
            optExportNoSort.Checked = !SavedConfig.SortCommandLineExport;
            optExportSort.Checked = SavedConfig.SortCommandLineExport;

            switch (SavedConfig.CommandPrefix)
            {
                case 0:
                    optPlayerCommandsPrefixNone.Checked = true;
                    break;
                case 1:
                    optPlayerCommandsPrefixAdmincheat.Checked = true;
                    break;
                case 2:
                    optPlayerCommandsPrefixCheat.Checked = true;
                    break;
                default:

                    break;
            }

            switch (SavedConfig.Mode)
            {
                case ViewerModes.Mode_SinglePlayer:

                    if(cboMapSinglePlayer.Items.Count > 0)
                    {
                        optSinglePlayer.Checked = true;
                    }

                    break;
                case ViewerModes.Mode_Offline:
                    optOffline.Checked = true;

                    break;
                case ViewerModes.Mode_ContentPack:
                    optContentPack.Checked = true;

                    break;
                case ViewerModes.Mode_Ftp:
                    optServer.Checked = true;

                    break;
                default:
                    break;

            }


            //offline mode?
            if (SavedConfig.Mode == ViewerModes.Mode_Offline)
            {
                txtFilename.Text = SavedConfig.SelectedFile;
            }
            if(SavedConfig.Mode == ViewerModes.Mode_ContentPack)
            {
                txtContentPackFilename.Text = SavedConfig.SelectedFile;
            }

            cboFtpMap.Items.Clear();
            var orderedMap = mapFilenameMap.OrderBy(o => o.Value);
            foreach (var knownMap in orderedMap)
            {
                ComboValuePair comboValue = new ComboValuePair(knownMap.Key, knownMap.Value);
                int newIndex = cboFtpMap.Items.Add(comboValue);
            }

            //ftp servers?
            if(SavedConfig.ServerList.Count() > 0)
            {
                cboFTPServer.Items.Clear();

                foreach(var serverConfig in SavedConfig.ServerList)
                {
                    int newIndex = cboFTPServer.Items.Add(serverConfig);

                    if(serverConfig.Name == SavedConfig.SelectedServer)
                    {
                        cboFTPServer.SelectedIndex = newIndex;
                    }
                }
            }

            UpdateDisplay();

        }

        private void optSinglePlayer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {

            if (optSinglePlayer.Checked && cboMapSinglePlayer.Items.Count ==0)
            {
                PopulateSinglePlayerGames();
            }

            DisplayServerSettings();

            lblSelectedMapSP.BackColor = optSinglePlayer.Checked?Color.Aqua:Color.LightGray;
            lblOfflineSave.BackColor = optOffline.Checked? Color.Aqua : Color.LightGray;
            lblFTPServer.BackColor = optServer.Checked ? Color.Aqua : Color.LightGray;
            lblSelectedMapContentPack.BackColor = optContentPack.Checked ? Color.Aqua : Color.LightGray;

            chkUpdateNotificationSingle.Enabled = optSinglePlayer.Checked;
            chkUpdateNotificationFile.Enabled = optOffline.Checked;

            cboMapSinglePlayer.Enabled = optSinglePlayer.Checked;
            if (optSinglePlayer.Checked)
            {
                if (cboMapSinglePlayer.SelectedIndex < 0 && cboMapSinglePlayer.Items.Count > 0) cboMapSinglePlayer.SelectedIndex = 0;
            }
            
            txtFilename.Enabled = optOffline.Checked;
            btnSelectSaveGame.Enabled = optOffline.Checked;

            txtContentPackFilename.Enabled = optContentPack.Checked;
            btnLoadContentPack.Enabled = optContentPack.Checked;


            cboFTPServer.Enabled = optServer.Checked;
            btnAddServer.Enabled = optServer.Checked;
            btnRemoveServer.Enabled = optServer.Checked;

        }

        private void optOffline_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void optServer_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnSelectSaveGame_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog1.FileName;
            }
        }

        private void lblFTPHost_Click(object sender, EventArgs e)
        {

        }

        private void cboFTPServer_SelectedIndexChanged(object sender, EventArgs e)
        {

            DisplayServerSettings();
        }

        private void DisplayServerSettings()
        {

            if(cboFTPServer.SelectedItem==null || cboFTPServer.Visible == false)
            {
                return;
            }
            

            ServerConfiguration selectedServer = (ServerConfiguration)cboFTPServer.SelectedItem;
            ComboValuePair selectedMapItem = cboFtpMap.Items.Cast<ComboValuePair>().FirstOrDefault(i => i.Key.ToLower() == selectedServer.Map.ToLower());
            if (selectedMapItem != null)
            {
                cboFtpMap.SelectedItem = selectedMapItem;
            }
            cboFtpMap.Enabled = cboFTPServer.Visible == false;
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(optPlayerCommandsPrefixNone.Checked)
            {
                SavedConfig.CommandPrefix = 0;
            }
            else if (optPlayerCommandsPrefixAdmincheat.Checked)
            {
                SavedConfig.CommandPrefix = 1;
            }
            else
            {
                SavedConfig.CommandPrefix = 2;
            }


            SavedConfig.HideNoTames = optPlayerTameHide.Checked;
            SavedConfig.HideNoStructures = optPlayerStructureHide.Checked;
            SavedConfig.HideNoBody = optPlayerBodyHide.Checked;
            SavedConfig.FtpDownloadMode = optFTPSync.Checked ? 0 : 1;
            SavedConfig.SortCommandLineExport = optExportSort.Checked;
            SavedConfig.UpdateNotificationFile = chkUpdateNotificationFile.Checked;
            SavedConfig.UpdateNotificationSingle = chkUpdateNotificationSingle.Checked;

            //update server list
            if (optSinglePlayer.Checked)
            {
                if (cboMapSinglePlayer.SelectedItem != null)
                {
                    SavedConfig.Mode = ViewerModes.Mode_SinglePlayer;
                    ComboValuePair selectedMapPair = (ComboValuePair)cboMapSinglePlayer.SelectedItem;
                    SavedConfig.SelectedFile = selectedMapPair.Key;

                }
            }

            if (optOffline.Checked)
            {
                if(txtFilename.TextLength ==0)
                {
                    MessageBox.Show("Please select a file for offline mode.", "Offline Mode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                if (!File.Exists(txtFilename.Text))
                {

                    MessageBox.Show("Unable to find the selected file.\n\nPlease check the file exists and try again.", "Offline Mode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }


                SavedConfig.Mode = ViewerModes.Mode_Offline;
                SavedConfig.SelectedFile = txtFilename.Text;
            }

            if (optContentPack.Checked)
            {
                SavedConfig.Mode = ViewerModes.Mode_ContentPack;
                SavedConfig.SelectedFile = txtContentPackFilename.Text.Trim();
            }

            if (optServer.Checked)
            {

                SavedConfig.Mode = ViewerModes.Mode_Ftp;

                if (cboFTPServer.SelectedItem != null)
                {
                    ServerConfiguration selectedConfig = (ServerConfiguration)cboFTPServer.SelectedItem;
                    SavedConfig.SelectedServer = selectedConfig.Name;

                    string localFilename = Path.Combine(AppContext.BaseDirectory, $@"{selectedConfig.Name}\{selectedConfig.Map}");
                    SavedConfig.SelectedFile = localFilename;

                    cboFTPServer.Items[cboFTPServer.SelectedIndex] = selectedConfig;

                }
            }

            SavedConfig.ServerList.Clear();
            foreach (var item in cboFTPServer.Items)
            {
                if (item is ServerConfiguration)
                {
                    ServerConfiguration serverConfig = (ServerConfiguration)item;
                    SavedConfig.ServerList.Add(serverConfig);
                }
            }



            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {

            if (cboFTPServer.SelectedItem != null)
            {
                var selectedItem = cboFTPServer.SelectedItem;
                if(MessageBox.Show("Are you sure you want to remove this server?", "Remove Server?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cboFTPServer.Items.Remove(selectedItem);
                    if(cboFTPServer.Items.Count > 0)
                    {
                        cboFTPServer.SelectedIndex = 0;
                    }
                }
            }

            DisplayServerSettings();

        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {

            using (frmFtpFileBrowser serverSetup = new frmFtpFileBrowser())
            {
                if (serverSetup.ShowDialog() == DialogResult.OK)
                {
                    //commit changes to combo tag
                    int newIndex = cboFTPServer.Items.Add(serverSetup.SelectedServer);
                    Program.ProgramConfig.SelectedServer = serverSetup.Name;
                    cboFTPServer.SelectedIndex = newIndex;
                }
            }

        }
        private void UpdateFtpServer()
        {
            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Server admin provided imports coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lvwDinoClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveDinoClass.Enabled = lvwDinoClasses.SelectedItems.Count > 0;
            btnEditDinoClass.Enabled = lvwDinoClasses.SelectedItems.Count > 0;
        }

        private void btnAddDinoClass_Click(object sender, EventArgs e)
        {
            frmGenericClassMap mapEditor = new frmGenericClassMap(new DinoClassMap());
            mapEditor.Owner = this;
            if(mapEditor.ShowDialog() == DialogResult.OK)
            {
                //if line already exist for this class update the friendly name.
                DinoClassMap existingMap = SavedConfig.DinoMap.Where(d => d.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<DinoClassMap>();
                if(existingMap!=null && existingMap.ClassName.Length != 0)
                {
                    //found it, update
                    existingMap.FriendlyName = mapEditor.ClassMap.FriendlyName;
                }
                else
                {
                    //not found, add new
                    SavedConfig.DinoMap.Add((DinoClassMap)mapEditor.ClassMap);
                }

                PopulateDinoClassMap(mapEditor.ClassMap.ClassName);

            }
        }

        private void btnEditDinoClass_Click(object sender, EventArgs e)
        {
            DinoClassMap selectedDinoMap = (DinoClassMap)lvwDinoClasses.SelectedItems[0].Tag;

            frmGenericClassMap mapEditor = new frmGenericClassMap(selectedDinoMap);
            mapEditor.Owner = this;
            if (mapEditor.ShowDialog() == DialogResult.OK)
            {
                //if line already exist for this class update the friendly name.
                DinoClassMap existingMap = SavedConfig.DinoMap.Where(d => d.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<DinoClassMap>();
                if (existingMap != null && existingMap.ClassName.Length != 0)
                {
                    //found it, update
                    existingMap.FriendlyName = mapEditor.ClassMap.FriendlyName;
                }
                else
                {
                    //not found, add new
                    SavedConfig.DinoMap.Add((DinoClassMap)mapEditor.ClassMap);
                }

                PopulateDinoClassMap(mapEditor.ClassMap.ClassName);

            }
        }

        private void lvwDinoClasses_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwDinoClasses.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_DinoMap == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_DinoMap)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_DinoMap.Text.StartsWith("> "))
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
                SortingColumn_DinoMap.Text = SortingColumn_DinoMap.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_DinoMap = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_DinoMap.Text = "> " + SortingColumn_DinoMap.Text;
            }
            else
            {
                SortingColumn_DinoMap.Text = "< " + SortingColumn_DinoMap.Text;
            }

            // Create a comparer.
            lvwDinoClasses.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwDinoClasses.Sort();
        }

        private void btnRemoveDinoClass_Click(object sender, EventArgs e)
        {
            if (lvwDinoClasses.SelectedItems.Count == 0) return;

            DinoClassMap selectedClassMap = (DinoClassMap)lvwDinoClasses.SelectedItems[0].Tag;
            if(MessageBox.Show($"Are you sure you want to remove the display name for '{selectedClassMap.ClassName}'?", "Remove Name?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SavedConfig.DinoMap.Remove(selectedClassMap);
                PopulateDinoClassMap("");
            }
        }

        private void btnServerExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Server admin exports coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lvwItemMap_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwItemMap.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_ItemMap == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_ItemMap)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_ItemMap.Text.StartsWith("> "))
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
                SortingColumn_ItemMap.Text = SortingColumn_ItemMap.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_ItemMap = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_ItemMap.Text = "> " + SortingColumn_ItemMap.Text;
            }
            else
            {
                SortingColumn_ItemMap.Text = "< " + SortingColumn_ItemMap.Text;
            }

            // Create a comparer.
            lvwItemMap.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwItemMap.Sort();
        }

        private void txtCreatureFilter_TextChanged(object sender, EventArgs e)
        {
            //PopulateDinoClassMap("");
        }

        private void txtItemFilter_TextChanged(object sender, EventArgs e)
        {
            //PopulateItemClassMap("");
        }

        private void txtCreatureFilter_Validating(object sender, CancelEventArgs e)
        {
            //PopulateDinoClassMap("");
        }

        private void txtItemFilter_Validating(object sender, CancelEventArgs e)
        {
            //PopulateItemClassMap("");
        }

        private void chkApplyFilterDinos_CheckedChanged(object sender, EventArgs e)
        {
            txtCreatureFilter.Enabled = !chkApplyFilterDinos.Checked;
            if (!chkApplyFilterDinos.Checked)
            {
                txtCreatureFilter.Text = string.Empty;
                PopulateDinoClassMap("");
                txtCreatureFilter.Focus();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                lvwDinoClasses.BeginUpdate();
                int lastIndex = lvwDinoClasses.Items.Count - 1;
                for (int currentIndex = lastIndex; currentIndex >= 0; currentIndex--)
                {
                    ListViewItem item = lvwDinoClasses.Items[currentIndex];
                    if (!(item.SubItems[0].Text.ToLower().Contains(txtCreatureFilter.Text.ToLower()) | item.SubItems[1].Text.ToLower().Contains(txtCreatureFilter.Text.ToLower())))
                    {
                        lvwDinoClasses.Items.Remove(item);
                    }
                }
                lvwDinoClasses.EndUpdate();

                this.Cursor = Cursors.Default;
            }

            
        }

        private void chkApplyFilterItems_CheckedChanged(object sender, EventArgs e)
        {
            txtItemFilter.Enabled = !chkApplyFilterItems.Checked;
            if (!chkApplyFilterItems.Checked)
            {
                txtItemFilter.Text = string.Empty;
                PopulateItemClassMap("");
                txtItemFilter.Focus();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                lvwItemMap.BeginUpdate();
                int lastIndex = lvwItemMap.Items.Count - 1;
                for(int currentIndex = lastIndex; currentIndex >= 0; currentIndex--)
                {
                    ListViewItem item = lvwItemMap.Items[currentIndex];
                    bool shouldKeep = false;

                    foreach (ListViewItem subItem in item.SubItems)
                    {
                        if (subItem.Text.ToLower().Contains(txtItemFilter.Text.ToLower()))
                        {
                            shouldKeep = true;
                            break;
                        }
                    }

                    if (!shouldKeep)
                    {
                        lvwItemMap.Items.Remove(item);
                    }
                }
                lvwItemMap.EndUpdate();

                this.Cursor = Cursors.Default;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            frmItemClassMap mapEditor = new frmItemClassMap();
            mapEditor.Owner = this;
            if (mapEditor.ShowDialog() == DialogResult.OK)
            {
                //if line already exist for this class update the friendly name.
                ItemClassMap existingMap = SavedConfig.ItemMap.Where(i => i.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<ItemClassMap>();
                SavedConfig.ItemMap.Add(mapEditor.ClassMap);

                PopulateItemClassMap(mapEditor.ClassMap.ClassName);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (lvwItemMap.SelectedItems.Count == 0) return;
            ListViewItem selectedItem = lvwItemMap.SelectedItems[0];
            ItemClassMap itemMap = (ItemClassMap)selectedItem.Tag;

            Program.ProgramConfig.ItemMap.Remove(itemMap);
            lvwItemMap.Items.Remove(selectedItem);
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvwItemMap.SelectedItems.Count == 0) return;
            ListViewItem selectedItem = lvwItemMap.SelectedItems[0];
            ItemClassMap itemMap = (ItemClassMap)selectedItem.Tag;

            frmItemClassMap mapEditor = new frmItemClassMap(itemMap);
            mapEditor.Owner = this;
            if(mapEditor.ShowDialog() == DialogResult.OK)
            {

                //if line already exist for this class update the friendly name.
                ItemClassMap existingMap = SavedConfig.ItemMap.Where(i => i.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<ItemClassMap>();
                if (existingMap != null && existingMap.ClassName.Length != 0)
                {
                    //found it, update
                    existingMap.FriendlyName = mapEditor.ClassMap.FriendlyName;
                    existingMap.Category = mapEditor.ClassMap.Category;
                    existingMap.Image = mapEditor.ClassMap.Image;
                }
                else
                {
                    //not found, add new
                    SavedConfig.ItemMap.Add(mapEditor.ClassMap);
                }

                PopulateItemClassMap(mapEditor.ClassMap.ClassName);

                /*
                selectedItem.Text = mapEditor.ClassMap.Category;
                selectedItem.SubItems[1].Text = mapEditor.ClassMap.FriendlyName;
                selectedItem.SubItems[2].Text = mapEditor.ClassMap.ClassName;
                
                if(mapEditor.ClassMap.Image.Length > 0)
                {
                    if (Program.GetItemImageIndex(mapEditor.ClassMap.Image) ==0)
                    {
                        Program.AddItemImageMap(mapEditor.ClassMap.Image);
                    }
                }

                int imageIndex = Program.GetItemImageIndex(mapEditor.ClassMap.Image);
                selectedItem.ImageIndex = imageIndex-1;

                lvwItemMap.Refresh();
                */

            }
        }

        private void lvwItemMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveItem.Enabled = lvwItemMap.SelectedItems.Count > 0;
            btnEditItem.Enabled = lvwItemMap.SelectedItems.Count > 0;
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tpgPlayers_Click(object sender, EventArgs e)
        {

        }

        private void optPlayerStructureHide_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerStructureShow_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerTameHide_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerTameShow_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerBodyHide_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerBodyShow_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void UpdatePlayerOptions()
        {

        }

        private void optPlayerCommandsPrefixAdmincheat_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerCommandsPrefixNone_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void optPlayerCommandsPrefixCheat_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePlayerOptions();
        }

        private void btnRemoveStructure_Click(object sender, EventArgs e)
        {
            if (lvwStructureMap.SelectedItems.Count == 0) return;

            StructureClassMap selectedClassMap = (StructureClassMap)lvwStructureMap.SelectedItems[0].Tag;
            if (MessageBox.Show($"Are you sure you want to remove the display name for '{selectedClassMap.ClassName}'?", "Remove Name?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SavedConfig.StructureMap.Remove(selectedClassMap);
                PopulateStructureClassMap("");
            }
        }

        private void lvwStructureMap_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwStructureMap.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_StructureMap == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_StructureMap)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_StructureMap.Text.StartsWith("> "))
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
                SortingColumn_StructureMap.Text = SortingColumn_StructureMap.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_StructureMap = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_StructureMap.Text = "> " + SortingColumn_StructureMap.Text;
            }
            else
            {
                SortingColumn_StructureMap.Text = "< " + SortingColumn_StructureMap.Text;
            }

            // Create a comparer.
            lvwStructureMap.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwStructureMap.Sort();
        }

        private void cboFtpMap_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void cboMapSinglePlayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEditColour_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = lvwColours.SelectedItems[0];
            ColourMap selectedMap = (ColourMap)selectedItem.Tag;
            using(frmColourEditor colourEditor = new frmColourEditor(selectedMap))
            {
                colourEditor.Owner = this;
                if(colourEditor.ShowDialog() == DialogResult.OK)
                {
                    ColourMap existingMap = Program.ProgramConfig.ColourMap.FirstOrDefault(m => m.Id == colourEditor.SelectedMap.Id);
                    if (existingMap != null)
                    {
                        //update existing
                        existingMap.Hex = colourEditor.SelectedMap.Hex;
                    }
                    else
                    {
                        //add new
                        Program.ProgramConfig.ColourMap.Add(colourEditor.SelectedMap);
                    }

                    PopulateColours();
                }
            }
        }

        private void btnEditStructure_Click(object sender, EventArgs e)
        {
            StructureClassMap selectedStructureMap = (StructureClassMap)lvwStructureMap.SelectedItems[0].Tag;

            frmGenericClassMap mapEditor = new frmGenericClassMap(selectedStructureMap);
            mapEditor.Owner = this;
            if (mapEditor.ShowDialog() == DialogResult.OK)
            {
                //if line already exist for this class update the friendly name.
                StructureClassMap existingMap = SavedConfig.StructureMap.Where(d => d.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<StructureClassMap>();
                if (existingMap != null && existingMap.ClassName.Length != 0)
                {
                    //found it, update
                    existingMap.FriendlyName = mapEditor.ClassMap.FriendlyName;
                }
                else
                {
                    //not found, add new
                    SavedConfig.StructureMap.Add((StructureClassMap)mapEditor.ClassMap);
                }

                PopulateStructureClassMap(mapEditor.ClassMap.ClassName);
            }
        }

        private void chkApplyFilterStructures_CheckedChanged(object sender, EventArgs e)
        {
            txtStructureFilter.Enabled = !chkApplyFilterStructures.Checked;
            if (!chkApplyFilterStructures.Checked)
            {
                txtStructureFilter.Text = string.Empty;
                PopulateStructureClassMap("");
                txtStructureFilter.Focus();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                string filterText = txtStructureFilter.Text.ToLower();
                ListView selectedListview = lvwStructureMap;

                selectedListview.BeginUpdate();
                int lastIndex = lvwStructureMap.Items.Count - 1;
                for (int currentIndex = lastIndex; currentIndex >= 0; currentIndex--)
                {
                    ListViewItem item = selectedListview.Items[currentIndex];
                    if (!(item.SubItems[1].Text.ToLower().Contains(filterText) | item.SubItems[2].Text.ToLower().Contains(filterText)))
                    {
                        selectedListview.Items.Remove(item);
                    }
                }
                selectedListview.EndUpdate();

                this.Cursor = Cursors.Default;
            }
        }

        private void btnAddStructure_Click(object sender, EventArgs e)
        {
            frmGenericClassMap mapEditor = new frmGenericClassMap(new StructureClassMap());
            mapEditor.Owner = this;
            if (mapEditor.ShowDialog() == DialogResult.OK)
            {
                //if line already exist for this class update the friendly name.
                StructureClassMap existingMap = SavedConfig.StructureMap.Where(d => d.ClassName.ToLower() == mapEditor.ClassMap.ClassName.ToLower()).FirstOrDefault<StructureClassMap>();
                if (existingMap != null && existingMap.ClassName.Length != 0)
                {
                    //found it, update
                    existingMap.FriendlyName = mapEditor.ClassMap.FriendlyName;
                }
                else
                {
                    //not found, add new
                    SavedConfig.DinoMap.Add((DinoClassMap)mapEditor.ClassMap);
                }

                PopulateStructureClassMap(mapEditor.ClassMap.ClassName);
            }
        }

        private void chkApplyFilterColours_CheckedChanged(object sender, EventArgs e)
        {
            ListView selectedListview = lvwColours;
            TextBox selectedTextbox = txtFilterColour;
            CheckBox selectedCheckbox = chkApplyFilterColours;

            selectedTextbox.Enabled = !selectedCheckbox.Checked;
            if (!selectedCheckbox.Checked)
            {
                selectedTextbox.Text = string.Empty;
                PopulateColours();
                selectedTextbox.Focus();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;

                string filterText = selectedTextbox.Text.ToLower();
                

                selectedListview.BeginUpdate();
                int lastIndex = lvwStructureMap.Items.Count - 1;
                for (int currentIndex = lastIndex; currentIndex >= 0; currentIndex--)
                {
                    ListViewItem item = selectedListview.Items[currentIndex];
                    bool shouldKeep = false;

                    foreach(ListViewItem subItem in item.SubItems)
                    {
                        if (subItem.Text.ToLower().Contains(filterText))
                        {
                            shouldKeep = true;
                            break;
                        }
                    }

                    if (!shouldKeep)
                    {
                        selectedListview.Items.Remove(item);
                    }

                }
                selectedListview.EndUpdate();

                this.Cursor = Cursors.Default;
            }
        }

        private void btnRemoveColour_Click(object sender, EventArgs e)
        {
            if (lvwColours.SelectedItems.Count == 0) return;

            ColourMap selectedClassMap = (ColourMap)lvwColours.SelectedItems[0].Tag;
            if (MessageBox.Show($"Are you sure you want to remove colour for index '{selectedClassMap.Id}'?", "Remove Colour?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SavedConfig.ColourMap.Remove(selectedClassMap);
                PopulateColours();
            }
        }

        private void btnNewColour_Click(object sender, EventArgs e)
        {
            using (frmColourEditor colourEditor = new frmColourEditor())
            {
                colourEditor.Owner = this;
                if (colourEditor.ShowDialog() == DialogResult.OK)
                {
                    ColourMap existingMap = Program.ProgramConfig.ColourMap.FirstOrDefault(m => m.Id == colourEditor.SelectedMap.Id);
                    if (existingMap != null)
                    {
                        //update existing
                        existingMap.Hex = colourEditor.SelectedMap.Hex;
                    }
                    else
                    {
                        //add new
                        Program.ProgramConfig.ColourMap.Add(colourEditor.SelectedMap);
                    }

                    PopulateColours();
                }
            }
        }

        private void lvwColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveColour.Enabled = lvwColours.SelectedItems.Count == 1;
            btnEditColour.Enabled = lvwColours.SelectedItems.Count == 1;
        }

        private void lvwStructureMap_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvwColours_Click(object sender, EventArgs e)
        {
            btnRemoveColour.Enabled = lvwColours.SelectedItems.Count == 1;
            btnEditColour.Enabled = lvwColours.SelectedItems.Count == 1;
        }

        private void btnLoadContentPack_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "ASV Content Packs (*.asv)|*.asv";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    txtContentPackFilename.Text = dialog.FileName;
                }
            }
        }

        private void tabSettings_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(e.TabPage.Name == "tpgExport")
            {
                if (!File.Exists(SavedConfig.SelectedFile))
                {
                    MessageBox.Show("Export options only available for .ARK save files.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    e.Cancel = true;
                    return;
                }
                if(Path.GetExtension(SavedConfig.SelectedFile).ToLower() != ".ark")
                {
                    MessageBox.Show("Export options only available for .ARK save files.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    e.Cancel = true;
                    return;
                }

            }

        }

        private void cboExportTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboExportTribe.SelectedItem == null) return;
            ComboValuePair selectedValue = (ComboValuePair)cboExportTribe.SelectedItem;
            int.TryParse(selectedValue.Key, out int tribeId);
            PopulateExportPlayers(tribeId);
        }

        private void btnExportContentPack_Click(object sender, EventArgs e)
        {
            btnExportContentPack.Enabled = false;
            
            using(SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Save ASV Content Pack";
                dialog.Filter = "ASV Content Pack (*.asv)|*.asv";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string fileFolder = Path.GetDirectoryName(dialog.FileName);
                    if (!Directory.Exists(fileFolder)) Directory.CreateDirectory(fileFolder);

                    //re-load game data save
                    var contentFile = Program.ProgramConfig.SelectedFile;
                    if (File.Exists(contentFile))
                    {
                        var gd = Program.LoadArkData(contentFile);
                        long tribeId = 0;
                        if (cboExportTribe.SelectedItem == null)
                        {
                            ComboValuePair selectedValue = (ComboValuePair)cboExportTribe.SelectedItem;
                            long.TryParse(selectedValue.Key, out tribeId);
                        }

                        long playerId = 0;
                        if (cboExportPlayer.SelectedItem == null)
                        {
                            ComboValuePair selectedValue = (ComboValuePair)cboExportPlayer.SelectedItem;
                            long.TryParse(selectedValue.Key, out playerId);
                        }


                        bool includeGameStructures = chkStructureLocations.Checked;
                        bool includeGameStructureContent = chkStructureContents.Checked;
                        bool includeTribesPlayers = chkTribesPlayers.Checked;
                        bool includeTamed = chkTamedCreatures.Checked;
                        bool includeWild = chkWildCreatures.Checked;
                        bool includePlayerStructures = chkPlayerStructures.Checked;

                        ContentPack contentPack = new ContentPack(gd, tribeId, playerId, udExportLat.Value, udExportLon.Value, udExportRadius.Value, includeGameStructures, includeGameStructureContent, includeTribesPlayers, includeTamed, includeWild, includePlayerStructures,includeGameStructureContent);
                        ContentManager exportManager = new ContentManager(contentPack);
                        try
                        {
                            exportManager.ExportContentPack(dialog.FileName);
                            MessageBox.Show("Content pack exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            contentPack = null;
                            exportManager = null;
                        }

                    }
                    
                    this.Cursor = Cursors.Default;

                }
            }
            btnExportContentPack.Enabled = true;
        }

        private void btnJsonExportAll_Click(object sender, EventArgs e)
        {
            btnJsonExportAll.Enabled = false;

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath= AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.SelectedPath)) Directory.CreateDirectory(dialog.SelectedPath);
                    cm.ExportAll(dialog.SelectedPath);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("All data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            btnJsonExportAll.Enabled = true;
        }

        private void btnJsonExportWild_Click(object sender, EventArgs e)
        {
            btnJsonExportWild.Enabled = false;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Wild";
                dialog.Filter = "JSON text file(*.json)|*.json";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.FileName)) Directory.CreateDirectory(dialog.FileName);
                    cm.ExportWild(dialog.FileName);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Wild data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            btnJsonExportWild.Enabled = false;
        }

        private void btnJsonExportTamed_Click(object sender, EventArgs e)
        {
            btnJsonExportTamed.Enabled = false;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Tamed";
                dialog.Filter = "JSON text file(*.json)|*.json";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.FileName)) Directory.CreateDirectory(dialog.FileName);
                    cm.ExportTamed(dialog.FileName);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Tamed data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            btnJsonExportTamed.Enabled = true;
        }

        private void btnJsonExportTribes_Click(object sender, EventArgs e)
        {
            btnJsonExportTribes.Enabled = false;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Tribes";
                dialog.Filter = "JSON text file(*.json)|*.json";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.FileName)) Directory.CreateDirectory(dialog.FileName);
                    cm.ExportPlayerTribes(dialog.FileName);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Tribe data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            btnJsonExportTribes.Enabled = false;
        }

        private void btnJsonExportPlayers_Click(object sender, EventArgs e)
        {
            btnJsonExportPlayers.Enabled = false;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Players";
                dialog.Filter = "JSON text file(*.json)|*.json";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.FileName)) Directory.CreateDirectory(dialog.FileName);
                    cm.ExportPlayers(dialog.FileName);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Player data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            btnJsonExportPlayers.Enabled = true;

        }

        private void btnJsonExportPlayerStructures_Click(object sender, EventArgs e)
        {
            btnJsonExportPlayerStructures.Enabled = false;

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Structures";
                dialog.Filter = "JSON text file(*.json)|*.json";
                dialog.InitialDirectory = AppContext.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (!Directory.Exists(dialog.FileName)) Directory.CreateDirectory(dialog.FileName);
                    cm.ExportPlayerStructures(dialog.FileName);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Structure data exported successfully.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            btnJsonExportPlayerStructures.Enabled = true;

        }

        private void optContentPack_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void btnEditServer_Click(object sender, EventArgs e)
        {
            if (cboFTPServer.SelectedItem == null) return;
            ServerConfiguration selectedServer = (ServerConfiguration)cboFTPServer.SelectedItem;
            using (frmFtpFileBrowser serverSetup = new frmFtpFileBrowser(selectedServer))
            {
                if(serverSetup.ShowDialog() == DialogResult.OK)
                {
                    //commit changes to combo tag
                    cboFTPServer.Items[cboFTPServer.SelectedIndex] = serverSetup.SelectedServer;
                    DisplayServerSettings();
                }

            }
        }
    }
}
