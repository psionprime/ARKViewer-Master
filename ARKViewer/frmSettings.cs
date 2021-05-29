using System;
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

        public ViewerConfiguration SavedConfig { get; set; }
        public frmSettings(ViewerConfiguration config)
        {
            InitializeComponent();

            lvwItemMap.LargeImageList = Program.ItemImageList;
            lvwItemMap.SmallImageList = Program.ItemImageList;

            imageFolder = Path.Combine(AppContext.BaseDirectory, @"images\icons\");
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            SavedConfig = config;
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

        private void frmSettings_Load(object sender, EventArgs e)
        {
            PopulateColours();
            PopulateDinoClassMap("");
            PopulateStructureClassMap("");
            PopulateItemClassMap("");
            
            var mapFilenameMap = new Dictionary<string,string>
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
                { "astralark.ark", "AstralARK" },
                { "hope.ark", "Hope"},
                { "tunguska_p.ark", "Tunguska"},
                { "caballus_p.ark", "Caballus"},
                { "viking_p.ark", "Fjördur"},
                { "tiamatprime.ark", "Tiamat Prime"}

            };

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

            //get registry path for steam apps
            string steamRoot = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "").ToString();

            if (steamRoot!=null && steamRoot.Length > 0)
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
                                string directoryCheck = lineContent[3].ToString().Replace("\"", "").Replace(@"\\", @"\") + @"\SteamApps\Common\ARK\ShooterGame\Saved\";
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

                        }
                    }
                }
            }

            optSinglePlayer.Enabled = cboMapSinglePlayer.Items.Count > 0;

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

            DisplayServerSettings();

            lblSelectedMapSP.BackColor = optSinglePlayer.Checked?Color.Aqua:Color.LightGray;
            lblOfflineSave.BackColor = optOffline.Checked? Color.Aqua : Color.LightGray;
            lblFTPServer.BackColor = optServer.Checked ? Color.Aqua : Color.LightGray;

            chkUpdateNotificationSingle.Enabled = optSinglePlayer.Checked;
            chkUpdateNotificationFile.Enabled = optOffline.Checked;

            cboMapSinglePlayer.Enabled = optSinglePlayer.Checked;
            if (optSinglePlayer.Checked)
            {
                if (cboMapSinglePlayer.SelectedIndex < 0 && cboMapSinglePlayer.Items.Count > 0) cboMapSinglePlayer.SelectedIndex = 0;
            }
            
            txtFilename.Enabled = optOffline.Checked;
            btnSelectSaveGame.Enabled = optOffline.Checked;

            cboFTPServer.Enabled = optServer.Checked;
            txtFTPAddress.Enabled = optServer.Checked && cboFTPServer.Visible == false;
            txtFTPFilePath.Enabled = optServer.Checked && cboFTPServer.Visible == false;
            txtFTPPassword.Enabled = optServer.Checked && cboFTPServer.Visible == false;
            udFTPPort.Enabled = optServer.Checked && cboFTPServer.Visible == false;
            txtFTPUsername.Enabled = optServer.Checked && cboFTPServer.Visible == false;
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
            txtFTPAddress.Text = "";
            txtFTPAddress.Enabled = false;
            udFTPPort.Value = 8821;
            udFTPPort.Enabled = false;
            txtFTPFilePath.Text = "";
            txtFTPFilePath.Enabled = false;
            txtFTPUsername.Text = "";
            txtFTPUsername.Enabled = false;
            txtFTPPassword.Text = "";
            txtFTPPassword.Enabled = false;
            cboFtpMap.Enabled = false;
            optFtpModeFtp.Enabled = false;
            optFtpModeSftp.Enabled = false;

            chkPasswordVisibility.Visible = txtServerName.Visible;
            chkPasswordVisibility.Checked = false;

            if(cboFTPServer.SelectedItem==null || cboFTPServer.Visible == false)
            {
                return;
            }
            

            ServerConfiguration selectedServer = (ServerConfiguration)cboFTPServer.SelectedItem;
            txtServerName.Text = selectedServer.Name;

            txtFTPAddress.Text = selectedServer.Address;
            txtFTPAddress.Enabled = cboFTPServer.Visible == false;
            udFTPPort.Value = selectedServer.Port;
            udFTPPort.Enabled = cboFTPServer.Visible == false;
            txtFTPFilePath.Text = selectedServer.SaveGamePath;
            txtFTPFilePath.Enabled = cboFTPServer.Visible == false;
            txtFTPUsername.Text = selectedServer.Username;
            txtFTPUsername.Enabled = cboFTPServer.Visible == false;
            txtFTPPassword.Text = selectedServer.Password;
            txtFTPPassword.Enabled = cboFTPServer.Visible == false;
            optFtpModeFtp.Enabled = cboFTPServer.Visible == false;
            optFtpModeSftp.Enabled = cboFTPServer.Visible == false;

            if(selectedServer.Mode == 0)
            {
                optFtpModeFtp.Checked = true;
            }
            else
            {
                optFtpModeSftp.Checked = true;
            }

            ComboValuePair selectedMapItem = cboFtpMap.Items.Cast<ComboValuePair>().FirstOrDefault(i => i.Key.ToLower() == selectedServer.Map.ToLower());
            if (selectedMapItem != null)
            {
                cboFtpMap.SelectedItem = selectedMapItem;
            }
            cboFtpMap.Enabled = cboFTPServer.Visible == false;
            txtFTPUsername.PasswordChar = cboFTPServer.Visible? '●':'\0';

            if (cboFTPServer.Visible)
            {
                btnAddServer.Image = ARKViewer.Properties.Resources.button_add;
                btnSave.Enabled = true;
            }

            chkPasswordVisibility.Visible = false;
            chkPasswordVisibility.Checked = false;
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(optPlayerCommandsPrefixNone.Checked)
            {
                SavedConfig.CommandPrefix = 0;
            }else if (optPlayerCommandsPrefixAdmincheat.Checked)
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

            if (optServer.Checked)
            {

                SavedConfig.Mode = ViewerModes.Mode_Ftp;

                if (cboFTPServer.SelectedItem != null)
                {
                    ServerConfiguration selectedConfig = (ServerConfiguration)cboFTPServer.SelectedItem;
                    SavedConfig.SelectedServer = selectedConfig.Name;
                    SavedConfig.SelectedFile = selectedConfig.Map;

                    if (txtServerName.Visible)
                    {
                        selectedConfig.Name = txtServerName.Text;
                    }

                    if (txtFTPAddress.Text.Contains(@"\"))
                    {
                        txtFTPAddress.Text = txtFTPAddress.Text.Replace(@"\", @"/");
                    }
                    if (txtFTPAddress.Text.EndsWith(@"/"))
                    {
                        txtFTPAddress.Text = txtFTPAddress.Text.Substring(0, txtFTPAddress.TextLength - 1);
                    }
                    if (txtFTPFilePath.Text.Contains(@"\"))
                    {
                        txtFTPFilePath.Text = txtFTPFilePath.Text.Replace(@"\", @"/");
                    }
                    if (!txtFTPFilePath.Text.StartsWith(@"/"))
                    {
                        txtFTPFilePath.Text = "/" + txtFTPFilePath.Text;
                    }

                    selectedConfig.Address = txtFTPAddress.Text;
                    selectedConfig.SaveGamePath = txtFTPFilePath.Text;
                    selectedConfig.Port = (int)udFTPPort.Value;
                    selectedConfig.Username = txtFTPUsername.Text;
                    selectedConfig.Password = txtFTPPassword.Text;

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

            if (cboFTPServer.Visible && cboFTPServer.SelectedItem != null)
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
            else
            {
                txtServerName.Visible = false;
                cboFTPServer.Visible = true;

                if (cboFTPServer.Items.Count > 0)
                {
                    cboFTPServer.SelectedIndex = 0;
                }
            }

            DisplayServerSettings();

        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            if (cboFTPServer.Visible)
            {
                btnSave.Enabled = false;
                btnAddServer.Image = ARKViewer.Properties.Resources.button_save;

                //add new
                cboFTPServer.Visible = false;
                txtServerName.Visible = true;
                
                txtServerName.Text = "";
                
                txtFTPAddress.Text = "";
                txtFTPAddress.Enabled = true;
                txtFTPFilePath.Text = "";
                txtFTPFilePath.Enabled = true;
                txtFTPUsername.Text = "";
                txtFTPUsername.PasswordChar = '\0';
                txtFTPUsername.Enabled = true;
                txtFTPPassword.Text = "";
                txtFTPPassword.Enabled = true;
                udFTPPort.Value = 8821;
                udFTPPort.Enabled = true;
                cboFtpMap.Enabled = true;
                optFtpModeFtp.Enabled = true;
                optFtpModeSftp.Enabled = true;


                txtServerName.Focus();
                chkPasswordVisibility.Visible = true;


            }
            else
            {
                UpdateFtpServer();


            }

        }
        private void UpdateFtpServer()
        {
            if (txtServerName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a unique name for this server.", "Missing Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServerName.Focus();
                return;
            }

            if(txtFTPAddress.TextLength == 0)
            {
                MessageBox.Show("Please enter the FTP server address.", "Missing Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFTPAddress.Focus();
                return;
            }

            if(!(cboFtpMap.SelectedIndex >= 0))
            {
                MessageBox.Show("Please select a server map.", "Missing Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboFtpMap.Focus();
                return;
            }

            txtServerName.Visible = false;
            txtFTPAddress.Text = txtFTPAddress.Text.Replace("ftp://", "");
            txtFTPAddress.Text = txtFTPAddress.Text.Replace("sftp://", "");
            cboFTPServer.Visible = true;

            //add it, revert to combo selection, select new server
            ServerConfiguration newConfig = new ServerConfiguration()
            {
                Name = txtServerName.Text,
                Address = txtFTPAddress.Text,
                SaveGamePath = txtFTPFilePath.Text,
                Username = txtFTPUsername.Text,
                Password = txtFTPPassword.Text,
                Port = (int)udFTPPort.Value,
                Map = ((ComboValuePair)cboFtpMap.SelectedItem).Key.ToLower(),
                Mode = optFtpModeFtp.Checked ? 0 : 1
            };

            int newIndex = cboFTPServer.Items.Add(newConfig);
            cboFTPServer.SelectedIndex = newIndex;
            DisplayServerSettings();
            btnSave.Enabled = true;
        }

        private void txtServerName_Validating(object sender, CancelEventArgs e)
        {
            if (txtServerName.TextLength == 0) return;

            if(cboFTPServer.Items.Count > 0)
            {
                var serverExists = cboFTPServer.Items.Cast<ServerConfiguration>().Where(s=>s.Name.ToLower() == txtServerName.Text.ToLower()).Count() > 0;
                if (serverExists)
                {
                    MessageBox.Show("Server name already exists, please choose a different name.", "Server Name.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                e.Cancel = serverExists;
            }
        }

        private void chkPasswordVisibility_CheckedChanged(object sender, EventArgs e)
        {            
            txtFTPPassword.PasswordChar = chkPasswordVisibility.Checked? '\0' : '●';
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
    }
}
