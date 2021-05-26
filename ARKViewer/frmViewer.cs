using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using ArkSavegameToolkitNet.Domain;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using ArkSavegameToolkitNet.Types;
using Renci.SshNet;
using Timer = System.Windows.Forms.Timer;
using FluentFTP;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.CodeDom;

namespace ARKViewer
{
    public partial class frmViewer : Form
    {


        Timer saveCheckTimer = new Timer();
        frmStructureLocations structureLocations = null;

        private bool isLoading = false;
        private decimal lastSelectedX = 0.0m;
        private decimal lastSelectedY = 0.0m;

        ArkGameData gd = null;
        private ColumnHeader SortingColumn_DetailTame = null;
        private ColumnHeader SortingColumn_DetailWild = null;

        private ColumnHeader SortingColumn_Markers = null;
        private ColumnHeader SortingColumn_Players = null;
        private ColumnHeader SortingColumn_Structures = null;
        private ColumnHeader SortingColumn_Tribes = null;

        private string savePath = Path.GetDirectoryName(Application.ExecutablePath);
        private string saveFilename = "TheIsland.ark";

        private List<TribeMap> allTribes = new List<TribeMap>();
        private List<PlayerMap> allPlayers = new List<PlayerMap>();

        private int mapMouseDownX = 0;
        private int mapMouseDownY = 0;
        private int mapMouseDownZoom = 0;
        
        
        public frmViewer()
        {
            InitializeComponent();

            isLoading = true;

            for (int x = 0; x <= 32; x++)
            {
                Image image = (Image)ARKViewer.Properties.Resources.ResourceManager.GetObject($"marker_{x}");
                if (image != null)
                {
                    imageList1.Images.Add($"marker_{x}", image);
                }
            }

            
            if(ARKViewer.Program.ProgramConfig.WindowHeight != 0)
            {
                this.Left = ARKViewer.Program.ProgramConfig.WindowLeft;
                this.Top = ARKViewer.Program.ProgramConfig.WindowTop;
                this.Width = ARKViewer.Program.ProgramConfig.WindowWidth;
                this.Height = ARKViewer.Program.ProgramConfig.WindowHeight;
            }else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            if (ARKViewer.Program.ProgramConfig.SplitterDistance > 0)
            {
                splitContainer1.SplitterDistance = ARKViewer.Program.ProgramConfig.SplitterDistance;
            }
            UpdateZoomLevel(this, new EventArgs());

            this.Show();
            Application.DoEvents();

            LoadData();


            if (ARKViewer.Program.ProgramConfig.Zoom > 0)
            {
                trackZoom.Value = ARKViewer.Program.ProgramConfig.Zoom;
            }

            isLoading = false;            
        }

        private void ReloadCheck(object sender, EventArgs args)
        {



            switch (Program.ProgramConfig.Mode)
            {
                case ViewerModes.Mode_SinglePlayer:
                    if (Program.ProgramConfig.UpdateNotificationSingle)
                    {
                        var lastWriteTime = File.GetLastWriteTimeUtc(saveFilename);

                        if (gd.SaveState.SaveTime < lastWriteTime)
                        {
                            saveCheckTimer.Enabled = false;

                            if (MessageBox.Show("Changes detected in selected save game data.\n\nDo you wish to reload now?", "Reload Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                LoadData();
                            }

                            saveCheckTimer.Enabled = true;
                        }
                    }
                    break;
                case ViewerModes.Mode_Offline:
                    if (Program.ProgramConfig.UpdateNotificationFile)
                    {
                        var lastWriteTime = File.GetLastWriteTimeUtc(saveFilename);

                        if (gd.SaveState.SaveTime < lastWriteTime)
                        {
                            saveCheckTimer.Enabled = false;
                            if (MessageBox.Show("Changes detected in selected save game data.\n\nDo you wish to reload now?", "Reload Data?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                LoadData();
                            }
                            saveCheckTimer.Enabled = true;
                        }
                    }

                    break;
                default:

                    break;
            }


        }

        private void LoadData()
        {

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Loading Save File";
            lblStatus.Refresh();

            if (saveCheckTimer != null)
            {
                saveCheckTimer.Tick -= ReloadCheck;
                saveCheckTimer.Stop();
                saveCheckTimer.Enabled = false;
            }

            switch (ARKViewer.Program.ProgramConfig.Mode)
            {
                case ViewerModes.Mode_SinglePlayer:
                    if(ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                    {
                        savePath = Path.GetFullPath(ARKViewer.Program.ProgramConfig.SelectedFile);
                        saveFilename = ARKViewer.Program.ProgramConfig.SelectedFile;
                    }

                    break;
                case ViewerModes.Mode_Offline:
                    if (ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                    {
                        savePath = Path.GetFullPath(ARKViewer.Program.ProgramConfig.SelectedFile);
                        saveFilename = ARKViewer.Program.ProgramConfig.SelectedFile;
                    }
                    break;
                case ViewerModes.Mode_Ftp:
                    savePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ARKViewer.Program.ProgramConfig.SelectedServer);

                    if (ARKViewer.Program.ProgramConfig.SelectedFile.Length > 0)
                    {
                        saveFilename = Path.Combine(savePath, ARKViewer.Program.ProgramConfig.SelectedFile);
                        if (!File.Exists(saveFilename))
                        {
                            //not yet downloaded, start new download
                            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
                            if (selectedServer != null)
                            {
                                if (selectedServer.Mode == 0)
                                {
                                    DownloadFtp();

                                }
                                else
                                {
                                    DownloadSFtp();
                                }

                            }
                        }
                    
                    }
                    break;

                default:

                    break;
            }

            if (File.Exists(saveFilename)){
                if (gd != null)
                {
                    gd = null;
                    GC.Collect();
                }

                gd = new ArkGameData(saveFilename, loadOnlyPropertiesInDomain: false);

                if (gd.Update(CancellationToken.None, null, true)?.Success == true)
                {

                    gd.ApplyPreviousUpdate();

                    lblMapDate.Text = File.GetLastWriteTime(saveFilename).ToString("dd MMM yyyy - HH:mm") ;

                    //var tamerTribes = gd.TamedCreatures.Where(s=>s.TribeName!=null).Select(s => new TribeMap() { TribeId = s.TargetingTeam,  TribeName = s.TribeName}).ToList();
                    if (gd.Structures != null && gd.Structures.Count() > 0)
                    {
                        var structureTribes = gd.Structures.Where(s => s.OwnerName != null && s.TargetingTeam.GetValueOrDefault(0) != 0).Select(s => new TribeMap() { TribeId = s.TargetingTeam.GetValueOrDefault(0), TribeName = s.OwnerName }).Distinct().ToList();

                        if (structureTribes != null)
                        {
                            if (gd.Tribes != null && gd.Tribes.Count() > 0)
                            {
                                var serverTribesQuery = gd.Tribes.Where(t => structureTribes.LongCount(s => s.TribeId == t.Id) == 0 && t.Id != 0);
                                if(serverTribesQuery!=null && serverTribesQuery.Count() > 0)
                                {
                                    var serverTribes = serverTribesQuery.Select(i => new TribeMap() { TribeId = i.Id, TribeName = i.Name, ContainsLog = (i.Logs !=null && i.Logs.Count() > 0) });


                                    if (serverTribes != null && serverTribes.Count() > 0)
                                    {
                                        structureTribes.AddRange(serverTribes.ToArray());
                                    }

                                }

                            }
                            allTribes = structureTribes.Distinct().ToList();
                        }
                        else
                        {
                            if (gd.Tribes != null && gd.Tribes.Count() > 0)
                            {
                                var serverTribes = gd.Tribes.Where(t => structureTribes.LongCount(s => s.TribeId == t.Id) == 0)
                                                            .Select(i => new TribeMap() { TribeId = i.Id, TribeName = i.Name });

                                allTribes = serverTribes.Distinct().ToList();
                            }


                        }


                        if (gd.Players != null && gd.Players.Count() > 0)
                        {

                            var serverPlayers = gd.Players.Select(i => new PlayerMap() { TribeId = (long)i.TribeId.GetValueOrDefault(0), PlayerId = (long)i.Id, PlayerName = i.CharacterName != null ? i.CharacterName : i.Name });
                            if (serverPlayers != null)
                            {
                                Parallel.ForEach(serverPlayers.Where(p => p.TribeId == 0), serverPlayer =>
                                {
                                    var testTribe = gd.Tribes.Where(t => t.MemberIds.Contains((int)serverPlayer.PlayerId)).FirstOrDefault();
                                    if (testTribe != null)
                                    {
                                        serverPlayer.TribeId = testTribe.Id;
                                    }
                                });


                                allPlayers = serverPlayers.Where(p => p.TribeId != 0).ToList();
                            }


                        }

                        var structurePlayers = gd.Structures.Where(s => s.TargetingTeam.GetValueOrDefault(0) != 0 && s.OwningPlayerName != null && allPlayers.LongCount(c => c.PlayerId == s.OwningPlayerId) == 0).Select(s => new PlayerMap() { TribeId = s.TargetingTeam.Value, PlayerId = (long)s.OwningPlayerId, PlayerName = s.OwningPlayerName }).Distinct().OrderBy(o => o.PlayerName).ToList();
                        if (structurePlayers != null && structurePlayers.Count() > 0)
                        {
                            allPlayers.AddRange(structurePlayers.ToArray());
                        }

                        Parallel.ForEach(allTribes, tribe =>
                        {
                            int playerCount = allPlayers.Count(p => p.TribeId == tribe.TribeId);
                            int tribePlayerCount = gd.Tribes.Where(t => t.Id == tribe.TribeId).Select(m => m.Members).Count();

                            tribe.PlayerCount = playerCount > tribePlayerCount ? playerCount : tribePlayerCount;
                            tribe.StructureCount = gd.Structures.LongCount(s => s.TargetingTeam.GetValueOrDefault(0) == tribe.TribeId);
                            tribe.TameCount = gd.TamedCreatures.LongCount(t => t.TargetingTeam == tribe.TribeId);

                            var tribeTames = gd.NoRafts.Where(t => t.TargetingTeam == tribe.TribeId);
                            if (tribeTames != null && tribeTames.Count() > 0)
                            {
                                TimeSpan noTime = new TimeSpan();

                                var lastTamedCreature = tribeTames.Where(t => t.TamedAtTime != null).OrderBy(o => o?.TamedForApprox.GetValueOrDefault(noTime).TotalSeconds).FirstOrDefault();
                                if (lastTamedCreature != null)
                                {
                                    var tamedTime = lastTamedCreature.TamedForApprox.GetValueOrDefault(noTime);
                                    tribe.LastActive = DateTime.Now.Subtract(tamedTime);
                                }
                            }
                            if (gd.Players != null && gd.Players.Count() > 0)
                            {
                                var tribePlayers = gd.Players.Where(p => p.TribeId == tribe.TribeId);
                                if (tribePlayers != null && tribePlayers.Count() > 0)
                                {
                                    var lastActivePlayer = tribePlayers.OrderBy(p => p.LastActiveTime).First();
                                    if (lastActivePlayer.LastActiveTime > tribe.LastActive)
                                    {
                                        tribe.LastActive = lastActivePlayer.LastActiveTime;
                                    }
                                }

                            }

                            if (gd.Tribes != null && gd.Tribes.Count() > 0)
                            {
                                var actualTribe = gd.Tribes.FirstOrDefault(t => t.Id == tribe.TribeId);
                                if (actualTribe != null)
                                {
                                    if (actualTribe.LastActiveTime > tribe.LastActive)
                                    {
                                        tribe.LastActive = actualTribe.LastActiveTime;
                                        tribe.TribeName = actualTribe.Name;
                                    }
                                    //error reported by @mjfro2 - crash when tribe has no log data
                                    long logCount = actualTribe.Logs == null ? 0 : actualTribe.Logs.Count();
                                    tribe.ContainsLog = logCount > 0;
                                }
                            }
                        });

                        if (gd.WildCreatures != null && gd.WildCreatures.Count() > 0)
                        {
                            int maxLevel = gd.WildCreatures.Max(w => w.BaseLevel);
                            udWildMax.Maximum = maxLevel;
                            udWildMin.Maximum = maxLevel;

                            udWildMax.Value = maxLevel;

                        }

                        saveCheckTimer.Interval = 60000;
                        saveCheckTimer.Tick += ReloadCheck;
                        saveCheckTimer.Enabled = true;
                        saveCheckTimer.Start();
                    }




                    RefreshWildSummary();
                    RefreshTamedSummary();
                    RefreshTribeSummary();
                }
                else
                {
                    MessageBox.Show("Unable to load the seleted save file as it appears to be incomplete or corrupted.\n\nPlease check your settings and try again.", "Failed to load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    gd = null;
                }
            }
            else
            {
                gd = null;
            }



            chkArtifacts.CheckedChanged -= Structure_CheckedChanged;
            chkBeaverDams.CheckedChanged -= Structure_CheckedChanged;
            chkDeinoNests.CheckedChanged -= Structure_CheckedChanged;
            chkGasVeins.CheckedChanged -= Structure_CheckedChanged;
            chkObelisks.CheckedChanged -= Structure_CheckedChanged;
            chkOilVeins.CheckedChanged -= Structure_CheckedChanged;
            chkWaterVeins.CheckedChanged -= Structure_CheckedChanged;
            chkWyvernNests.CheckedChanged -= Structure_CheckedChanged;
            chkChargeNodes.CheckedChanged -= Structure_CheckedChanged;
            chkGlitches.CheckedChanged -= Structure_CheckedChanged;
            chkMagmasaurNests.CheckedChanged -= Structure_CheckedChanged;


            chkArtifacts.Checked = ARKViewer.Program.ProgramConfig.Artifacts;
            chkBeaverDams.Checked = ARKViewer.Program.ProgramConfig.BeaverDams;
            chkDeinoNests.Checked = ARKViewer.Program.ProgramConfig.DeinoNests;
            chkDrakeNests.Checked = ARKViewer.Program.ProgramConfig.DrakeNests;
            chkGasVeins.Checked = ARKViewer.Program.ProgramConfig.GasVeins;
            chkObelisks.Checked = ARKViewer.Program.ProgramConfig.Obelisks;
            chkOilVeins.Checked = ARKViewer.Program.ProgramConfig.OilVeins;
            chkWaterVeins.Checked = ARKViewer.Program.ProgramConfig.WaterVeins;
            chkWyvernNests.Checked = ARKViewer.Program.ProgramConfig.WyvernNests;
            chkChargeNodes.Checked = ARKViewer.Program.ProgramConfig.ChargeNodes;
            chkGlitches.Checked = ARKViewer.Program.ProgramConfig.Glitches;
            chkMagmasaurNests.Checked = ARKViewer.Program.ProgramConfig.MagmaNests;

            chkArtifacts.CheckedChanged += Structure_CheckedChanged;
            chkBeaverDams.CheckedChanged += Structure_CheckedChanged;
            chkDeinoNests.CheckedChanged += Structure_CheckedChanged;
            chkGasVeins.CheckedChanged += Structure_CheckedChanged;
            chkObelisks.CheckedChanged += Structure_CheckedChanged;
            chkOilVeins.CheckedChanged += Structure_CheckedChanged;
            chkWaterVeins.CheckedChanged += Structure_CheckedChanged;
            chkWyvernNests.CheckedChanged += Structure_CheckedChanged; 
            chkChargeNodes.CheckedChanged += Structure_CheckedChanged; 
            chkGlitches.CheckedChanged += Structure_CheckedChanged;
            chkMagmasaurNests.CheckedChanged += Structure_CheckedChanged;


            txtMarkerFilter.Text = "";


            RefreshMapMarkers();
            RefreshPlayerTribes();
            RefreshTamedTribes();
            RefreshCryoTribes();
            RefreshStructureTribes();
            RefreshDroppedPlayers();


            if (gd != null)
            {
                lblStatus.Text = "Save file loaded successfully.";
                lblStatus.Refresh();

            }
            else
            {
                lblStatus.Text = "No save game selected. Please select a map from the settings page.";
                lblStatus.Refresh();

            }


            picMap.Image = DrawMap(0,0);
            this.Cursor = Cursors.Default;
        }

        private void RefreshPlayerTribes()
        {
            if (gd == null) return;

            cboTribes.Items.Clear();
            cboTribes.Items.Add(new ComboValuePair("0", "[All Tribes]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            if (gd.Tribes.Count() > 0)
            {
                foreach (var tribe in allTribes.Where(t=>t.TribeName !=null && t.TribeName.Length > 0))
                {
                    bool addTribe = true;
                    if (Program.ProgramConfig.HideNoBody)
                    {

                        addTribe = gd.Players != null && gd.Players.Count(p => p.TribeId != null && p.TribeId.Value == tribe.TribeId && p.Location != null) > 0;
                    }

                    if (addTribe)
                    {
                        ComboValuePair valuePair = new ComboValuePair(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }
            if(newItems.Count > 0)
            {
                cboTribes.BeginUpdate();
                foreach(var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTribes.Items.Add(newItem);
                }

                cboTribes.EndUpdate();
            }

            cboTribes.SelectedIndex = 0;
        }

        private void RefreshTamedTribes()
        {
            if (gd == null) return;

            cboTameTribes.Items.Clear();
            cboTameTribes.Items.Add(new ComboValuePair("0", "[All Tribes]"));
            cboTameTribes.Items.Add(new ComboValuePair("2000000000", "[Unclaimed Creatures]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            if (allTribes.Count() > 0)
            {
                foreach (var tribe in allTribes)
                {
                    bool addItem = true;

                    if (Program.ProgramConfig.HideNoTames)
                    {
                        addItem = (
                                    gd.TamedCreatures != null
                                    &&
                                    (
                                        gd.TamedCreatures.LongCount(t => t.TargetingTeam == tribe.TribeId & !(t.ClassName == "MotorRaft_BP_C" || t.ClassName == "Raft_BP_C")) > 0
                                    )
                                  );
                    }

                    if (addItem)
                    {
                        if (tribe.TribeName == null || tribe.TribeName.Length == 0) tribe.TribeName = "[N/A]";
                        ComboValuePair valuePair = new ComboValuePair(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }
            if (newItems.Count > 0)
            {
                cboTameTribes.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTameTribes.Items.Add(newItem);
                }

                cboTameTribes.EndUpdate();
            }
            cboTameTribes.SelectedIndex = 0;
        }


        private void RefreshCryoTribes()
        {
            
        }


        private void RefreshStructureTribes()
        {
            if (gd == null) return;

            cboStructureTribe.Items.Clear();
            cboStructureTribe.Items.Add(new ComboValuePair("0", "[All Tribes]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            if (allTribes.Count() > 0)
            {
                foreach (var tribe in allTribes)
                {
                    bool addItem = true;
                    if (Program.ProgramConfig.HideNoStructures)
                    {

                        addItem = (
                                    gd.Structures != null
                                    &&

                                    (
                                    gd.Structures.LongCount(s =>
                                                                (
                                                                    (tribe.TribeId == 0)
                                                                    ||
                                                                    (s.TargetingTeam != null && s.TargetingTeam == tribe.TribeId)
                                                                )
                                        ) > 0
                                    )
                                ) || (

                                    gd.TamedCreatures != null
                                    && gd.TamedCreatures.LongCount(w =>
                                                 (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                                && (w.TargetingTeam == tribe.TribeId)) > 0


                                );

                    }

                    if (addItem)
                    {
                        if (tribe.TribeName == null || tribe.TribeName.Length == 0) tribe.TribeName = "[N/A]";
                        ComboValuePair valuePair = new ComboValuePair(tribe.TribeId.ToString(), tribe.TribeName);
                        newItems.Add(valuePair);
                    }
                }
            }
            if(newItems.Count > 0)
            {
                cboStructureStructure.BeginUpdate();
                
                foreach(var newItem in newItems.OrderBy(o=>o.Value))
                {
                    cboStructureTribe.Items.Add(newItem);
                }
                
                cboStructureStructure.EndUpdate();
            }
            cboStructureTribe.SelectedIndex = 0;
        }


        private void RefreshDroppedPlayers()
        {
            if (gd == null) return;

            cboDroppedPlayer.Items.Clear();
            cboDroppedPlayer.Items.Add(new ComboValuePair("0", "[All Players]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            if (allPlayers.Count() > 0)
            {
                foreach (var player in allPlayers)
                {
                    ComboValuePair valuePair = new ComboValuePair(player.PlayerId.ToString(), player.PlayerName);
                    newItems.Add(valuePair);
                }
            }
            if (newItems.Count > 0)
            {
                cboDroppedPlayer.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboDroppedPlayer.Items.Add(newItem);
                }

                cboDroppedPlayer.EndUpdate();
            }
            cboDroppedPlayer.SelectedIndex = 0;
        }

        public void RefreshDroppedItems()
        {
            if (gd == null) return;

            cboDroppedItem.Items.Clear();

            List<ComboValuePair> newItems = new List<ComboValuePair>();
            cboDroppedItem.Items.Add(new ComboValuePair() { Key = "", Value = "[Dropped Items]" });
            cboDroppedItem.Items.Add(new ComboValuePair() { Key = "DeathItemCache_PlayerDeath_C", Value = "[Death Cache]" });


            if (gd.DroppedItems != null && gd.DroppedItems.Count() > 0)
            {
                //player
                ComboValuePair comboValue = (ComboValuePair)cboDroppedPlayer.SelectedItem;
                int.TryParse(comboValue.Key, out int selectedPlayerId);


                var playerStructureTypes = gd.DroppedItems.Where(s => (s.DroppedByPlayerId == selectedPlayerId || selectedPlayerId == 0))
                                                            .GroupBy(g => g.ClassName)
                                                            .Select(s => s.Key);


                if (playerStructureTypes != null && playerStructureTypes.Count() > 0)
                {

                    foreach (var className in playerStructureTypes)
                    {
                        var itemName = className;
                        var itemMap = Program.ProgramConfig.ItemMap.Where(i => i.ClassName == className).FirstOrDefault();

                        ComboValuePair classNameItem = new ComboValuePair(className, "");

                        if (itemMap != null && itemMap.FriendlyName.Length > 0)
                        {
                            itemName = itemMap.FriendlyName;
                            classNameItem.Value = itemName;

                        }

                        if (itemName == null || itemName.Length == 0) itemName = className;

                        newItems.Add(new ComboValuePair() { Key = className, Value = itemName });
                    }


                }



            }

       
            if (newItems.Count > 0)
            {
                cboDroppedItem.BeginUpdate();

                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboDroppedItem.Items.Add(newItem);
                }

                cboDroppedItem.EndUpdate();
            }
            cboDroppedItem.SelectedIndex = 0;
        }


        private void RefreshPlayerList()
        {
            if (gd == null) return;
            if (cboTribes.SelectedItem == null) return;

            btnCopyCommandPlayer.Enabled = false;

            ComboValuePair comboValue = (ComboValuePair)cboTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboPlayers.Items.Clear();
            cboPlayers.Items.Add(new ComboValuePair("0", "[All Players]"));


            List<ComboValuePair> newItems = new List<ComboValuePair>();

            foreach (var player in gd.Players.Where(p => p.TribeId == selectedTribeId || selectedTribeId == 0))
            {
                bool addPlayer = true;
                if (Program.ProgramConfig.HideNoBody)
                {
                    addPlayer = player.Location != null;
                }

                if (addPlayer)
                {
                    ComboValuePair valuePair = new ComboValuePair(player.Id.ToString(), player.CharacterName != null && player.CharacterName.Length > 0 ? player.CharacterName : player.Name);
                    newItems.Add(valuePair);
                }
            }

            if(newItems.Count > 0)
            {
                cboPlayers.BeginUpdate();
                foreach(var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboPlayers.Items.Add(newItem);
                }
                cboPlayers.EndUpdate();


            }
            cboPlayers.SelectedIndex = 0;
        }


        private void RefreshCryoPlayerList()
        {
            


        }



        private void RefreshTamePlayerList()
        {
            if (gd == null) return;
            if (cboTameTribes.SelectedItem == null) return;

            ComboValuePair comboValue = (ComboValuePair)cboTameTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboTamePlayers.Items.Clear();
            cboTamePlayers.Items.Add(new ComboValuePair("0", "[All Players]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            var filteredPlayers = allPlayers.Where(p => p.TribeId == selectedTribeId);

            foreach (var player in filteredPlayers)
            {

                bool addItem = true;

                if (Program.ProgramConfig.HideNoTames)
                {
                    addItem = (
                                gd.TamedCreatures != null
                                &&
                                (
                                    gd.TamedCreatures.LongCount(t => t.ImprinterPlayerDataId.GetValueOrDefault(0) == player.PlayerId || t.OwningPlayerId.GetValueOrDefault(0) == player.PlayerId &! (t.ClassName == "MotorRaft_BP_C" || t.ClassName == "Raft_BP_C")) > 0
                                )
                              );
                }

                if (addItem)
                {
                    if (player.PlayerName == null || player.PlayerName.Length == 0) player.PlayerName = "[N/A]";

                    ComboValuePair valuePair = new ComboValuePair(player.PlayerId.ToString(), player.PlayerName);
                    newItems.Add(valuePair);

                }
            }

            if (newItems.Count > 0)
            {
                cboTamePlayers.BeginUpdate();
                foreach (var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboTamePlayers.Items.Add(newItem);
                }
                cboTamePlayers.EndUpdate();

            }

            if(cboTamePlayers.Items.Count > 0)
            {
                cboTamePlayers.SelectedIndex = 0;
            }


        }

        private void RefreshStructurePlayerList()
        {
            if (gd == null) return;
            if (cboStructureTribe.SelectedItem == null) return;

            ComboValuePair comboValue = (ComboValuePair)cboStructureTribe.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            cboStructurePlayer.Items.Clear();
            cboStructurePlayer.Items.Add(new ComboValuePair("0", "[All Players]"));

            List<ComboValuePair> newItems = new List<ComboValuePair>();

            var filteredPlayers = allPlayers.Where(p => p.TribeId == selectedTribeId || selectedTribeId == 0 && p.TribeId != 0);

            foreach (var player in filteredPlayers)
            {
                bool addItem = true;

                if (Program.ProgramConfig.HideNoStructures)
                {
                    addItem = (
                                gd.Structures != null 
                                && 
                                gd.Structures.LongCount(s=>
                                                                s.InventoryId    == null
                                                                &&
                                                                (
                                                                    (s.OwningPlayerId != null && s.OwningPlayerId == player.PlayerId)
                                                                    ||
                                                                    (s.TargetingTeam != null && (s.TargetingTeam == player.TribeId )) 
                                                                )
                                                        ) > 0) || (

                                    gd.TamedCreatures != null
                                    && gd.TamedCreatures.LongCount(w =>
                                                 (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                                && (w.TargetingTeam == player.TribeId && player.TribeId !=0)) > 0

                                );
                }

                if (addItem)
                {
                    if (player.PlayerName == null || player.PlayerName.Length == 0) player.PlayerName = "[N/A]";

                    ComboValuePair valuePair = new ComboValuePair(player.PlayerId.ToString(), player.PlayerName);
                    newItems.Add(valuePair);

                }
            }

            if(newItems.Count > 0)
            {
                cboStructurePlayer.BeginUpdate();
                foreach(var newItem in newItems.OrderBy(o => o.Value))
                {
                    cboStructurePlayer.Items.Add(newItem);
                }
                cboStructurePlayer.EndUpdate();

            }

            cboStructurePlayer.SelectedIndex = 0;
            
        }

        private void LoadPlayerDetail()
        {
            if (gd == null) return;
            if (cboTribes.SelectedItem == null) return;
            if (cboPlayers.SelectedItem == null) return;

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            btnPlayerInventory.Enabled = false;

            //tribe
            ComboValuePair comboValue = (ComboValuePair)cboTribes.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            //player
            comboValue = (ComboValuePair)cboPlayers.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);

            var tribePlayerList = gd.Tribes.Where(t => selectedTribeId == 0 || t.Id == selectedTribeId).SelectMany(p => p.Members.Where(m=>selectedPlayerId==0 || m.Id == selectedPlayerId));
            var soloPlayerList = gd.Players.Where(p => p.Tribe == null && ((selectedPlayerId ==0 && selectedTribeId==0) || p.Id == selectedPlayerId));

            lvwPlayers.Items.Clear();
            lvwPlayers.Refresh();
            lvwPlayers.BeginUpdate();

            //Name, sex, lvl, lat, lon, hp, stam, melee, weight, speed, food,water, oxy, last on
            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

            Parallel.ForEach(tribePlayerList, player =>
            {
                bool addPlayer = true;
                if(Program.ProgramConfig.HideNoBody)
                {
                    addPlayer = player.Location != null;
                }

                if (addPlayer)
                {
                    ListViewItem newItem = new ListViewItem(player.CharacterName);
                    newItem.SubItems.Add(player.Tribe.Name);

                    newItem.SubItems.Add(player.Gender.ToString());
                    newItem.SubItems.Add(player.CharacterLevel.ToString());

                    if (player.Location != null)
                    {
                        newItem.SubItems.Add(player.Location.Latitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(player.Location.Longitude.Value.ToString("0.00"));

                    }
                    else
                    {
                        newItem.SubItems.Add("n/a");
                        newItem.SubItems.Add("n/a");
                    }

                    //0=health
                    //1=stamina
                    //2=torpor
                    //3=oxygen
                    //4=food
                    //5=water
                    //6=temperature
                    //7=weight
                    //8=melee damage
                    //9=movement speed
                    //10=fortitude
                    //11=crafting speed

                    newItem.SubItems.Add(player.Stats.GetValue(0).ToString()); //hp
                    newItem.SubItems.Add(player.Stats.GetValue(1).ToString()); //stam
                    newItem.SubItems.Add(player.Stats.GetValue(8).ToString()); //melee
                    newItem.SubItems.Add(player.Stats.GetValue(7).ToString()); //weight
                    newItem.SubItems.Add(player.Stats.GetValue(9).ToString()); //speed
                    newItem.SubItems.Add(player.Stats.GetValue(4).ToString()); //food
                    newItem.SubItems.Add(player.Stats.GetValue(5).ToString()); //water
                    newItem.SubItems.Add(player.Stats.GetValue(3).ToString()); //oxygen
                    newItem.SubItems.Add(player.Stats.GetValue(11).ToString());//crafting
                    newItem.SubItems.Add(player.Stats.GetValue(10).ToString());//fortitude


                    newItem.SubItems.Add(player.LastActiveTime.ToString("dd MMM yy HH:mm:ss"));
                    newItem.SubItems.Add(player.Name);
                    newItem.SubItems.Add(player.SteamId);
                    newItem.Tag = player;


                    listItems.Add(newItem);
                }


            });

            //Name, sex, lvl, lat, lon, hp, stam, melee, weight, speed, food,water, oxy, last on

            Parallel.ForEach(soloPlayerList, player =>
            {

                bool addPlayer = true;
                if (Program.ProgramConfig.HideNoBody)
                {
                    addPlayer = player.Location != null;
                }

                if (addPlayer)
                {

                    ListViewItem newItem = new ListViewItem(player.CharacterName);

                    string tribeName = "";
                    if (player.Tribe != null)
                    {
                        tribeName = player.Tribe.Name;
                    }

                    newItem.SubItems.Add(tribeName);

                    newItem.SubItems.Add(player.Gender.ToString());
                    newItem.SubItems.Add(player.CharacterLevel.ToString());


                    if (player.Location != null)
                    {
                        newItem.SubItems.Add(player.Location.Latitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(player.Location.Longitude.Value.ToString("0.00"));

                    }
                    else
                    {
                        newItem.SubItems.Add("n/a");
                        newItem.SubItems.Add("n/a");
                    }

                    newItem.SubItems.Add(player.Stats.GetValue(0).ToString()); //hp
                    newItem.SubItems.Add(player.Stats.GetValue(1).ToString()); //stam
                    newItem.SubItems.Add(player.Stats.GetValue(8).ToString()); //melee
                    newItem.SubItems.Add(player.Stats.GetValue(7).ToString()); //weight
                    newItem.SubItems.Add(player.Stats.GetValue(9).ToString()); //speed
                    newItem.SubItems.Add(player.Stats.GetValue(4).ToString()); //food
                    newItem.SubItems.Add(player.Stats.GetValue(5).ToString()); //water

                    newItem.SubItems.Add(player.Stats.GetValue(3).ToString()); //oxygen
                    newItem.SubItems.Add(player.Stats.GetValue(11).ToString()); //crafting
                    newItem.SubItems.Add(player.Stats.GetValue(10).ToString());//fortitude

                    newItem.SubItems.Add(player.LastActiveTime.ToString("dd MMM yy HH:mm:ss"));
                    newItem.SubItems.Add(player.Name);
                    newItem.SubItems.Add(player.SteamId);

                    newItem.Tag = player;

                    listItems.Add(newItem);
                }
            });

            lvwPlayers.Items.AddRange(listItems.ToArray());

            if (SortingColumn_Players != null)
            {
                lvwPlayers.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_Players.Index, SortingColumn_Players.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwPlayers.Sort();
            }

            lvwPlayers.EndUpdate();
            lblPlayerTotal.Text = $"Count: {lvwPlayers.Items.Count}";
            if(tabFeatures.SelectedTab.Name == "tpgPlayers")
            {
                picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
            }
        }



        private void RefreshMapMarkers()
        {
            lvwMapMarkers.Items.Clear();
            lvwMapMarkers.Refresh();
            lvwMapMarkers.BeginUpdate();
            if (ARKViewer.Program.ProgramConfig.MapMarkerList.Count(m => m.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower()) > 0)
            {
                foreach (var marker in ARKViewer.Program.ProgramConfig.MapMarkerList.Where(m => m.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower()))
                {
                    if (marker.Name.ToLower().Contains(txtMarkerFilter.Text.ToLower()))
                    {
                        ListViewItem newItem = lvwMapMarkers.Items.Add(marker.Name);
                        newItem.ImageKey = $"marker_{marker.Marker}";
                        newItem.SubItems.Add(marker.Lat.ToString("0.00"));
                        newItem.SubItems.Add(marker.Lon.ToString("0.00"));
                        newItem.Tag = marker;
                    }
                }
            }

            lvwMapMarkers.EndUpdate();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            DownloadFtp();
        }


        private bool DeletePlayerFtp(ArkPlayer player)
        {

            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return false;

            this.Cursor = Cursors.WaitCursor;
            bool returnVal = true;


            string profilePath = selectedServer.SaveGamePath.Substring(0, selectedServer.SaveGamePath.LastIndexOf("/"));
            string playerProfileFilename = $"{player.SteamId}.arkprofile";
            string ftpFilePath = $"{profilePath}/{playerProfileFilename}";
            string serverUsername = selectedServer.Username;
            string serverPassword = selectedServer.Password;

            switch (selectedServer.Mode)
            {
                case 0:
                    //ftp
                    FtpClient ftpClient = new FtpClient(selectedServer.Address);

                    try
                    {
                        ftpClient.Credentials.UserName = selectedServer.Username;
                        ftpClient.Credentials.Password = selectedServer.Password;
                        ftpClient.Port = selectedServer.Port;

                        ftpClient.DeleteFile(ftpFilePath);

                    }
                    catch
                    {
                        returnVal = false;
                    }
                    finally
                    {
                        ftpClient = null;
                    }


                    break;
                case 1:
                    //sftp
                    SftpClient sftpClient = new SftpClient(selectedServer.Address, selectedServer.Port, serverUsername, serverPassword);
                    try
                    {
                        sftpClient.Connect();

                        sftpClient.DeleteFile(ftpFilePath);

                    }
                    catch
                    {
                        returnVal = false;
                    }
                    finally
                    {
                        sftpClient.Dispose();
                    }

                    break;
            }
            


            return returnVal;
        }


        private void DownloadSFtp()
        {

            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return;

            btnRefresh.Enabled = false;

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Downloading save file from SFTP server.";
            lblStatus.Refresh();


            string ftpServerUrl = $"{selectedServer.Address}";
            string serverUsername = selectedServer.Username;
            string serverPassword = selectedServer.Password;
            string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            if(Program.ProgramConfig.FtpDownloadMode == 1)
            {
                //clear any previous .arktribe, .arkprofile files
                var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                foreach (var profileFile in profileFiles)
                {
                    File.Delete(profileFile);
                }

                var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                foreach (var tribeFile in tribeFiles)
                {
                    File.Delete(tribeFile);
                }

            }

            string mapFilename = ARKViewer.Program.ProgramConfig.SelectedFile;

            try
            {
                using (var sftp = new SftpClient(selectedServer.Address, selectedServer.Port, selectedServer.Username, selectedServer.Password))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory(selectedServer.SaveGamePath).Where(f => f.IsRegularFile);
                    foreach (var serverFile in files)
                    {


                        if (Path.GetExtension(serverFile.Name).StartsWith(".ark"))
                        {

                            string localFilename = Path.Combine(downloadPath, serverFile.Name);
                            
                            
                            if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 1)
                            {
                                File.Delete(localFilename);
                            }

                            bool shouldDownload = true;

                            if (serverFile.Name.EndsWith(".ark"))
                            {
                                if(serverFile.Name.ToLower() != selectedServer.Map.ToLower())
                                {
                                    shouldDownload = false;
                                }
                                else
                                {
                                    if(File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                    {
                                        shouldDownload = false;
                                    }
                                }
                            }
                            else
                            {
                                if (File.Exists(localFilename) && Program.ProgramConfig.FtpDownloadMode == 0 && File.GetLastWriteTimeUtc(localFilename) >= serverFile.LastAccessTimeUtc)
                                {
                                    shouldDownload = false;
                                }
                            }
                            
                            if (shouldDownload)
                            {
                                //delete local if any
                                if (File.Exists(localFilename))
                                {
                                    File.Delete(localFilename);
                                }

                                using (FileStream outputStream = new FileStream(localFilename, FileMode.CreateNew))
                                {
                                    sftp.DownloadFile(serverFile.FullName, outputStream);
                                    outputStream.Flush();
                                }
                                DateTime saveTime = serverFile.LastWriteTimeUtc;
                                File.SetLastWriteTime(localFilename, saveTime);

                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                frmErrorReport report = new frmErrorReport($"Download failed.\n\n{ex.Message.ToString()}", "");
                report.ShowDialog();
                lblStatus.Text = "Download failed.";
                lblStatus.Refresh();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnRefresh.Enabled = true;
            }


            lblStatus.Text = "Save file downloaded.";
            lblStatus.Refresh();
        }

        private void DownloadFtp()
        {
            ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
            if (selectedServer == null) return;

            btnRefresh.Enabled = false;

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Downloading save file from FTP server.";
            lblStatus.Refresh();


            selectedServer.Address = selectedServer.Address.Trim();
            selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim();
            if (!selectedServer.SaveGamePath.EndsWith("/"))
            {
                selectedServer.SaveGamePath = selectedServer.SaveGamePath.Trim() + "/"; 
            }


            using (FtpClient ftpClient = new FtpClient(selectedServer.Address))
            {

                ftpClient.Credentials.UserName = selectedServer.Username;
                ftpClient.Credentials.Password = selectedServer.Password;
                ftpClient.Port = selectedServer.Port;

                string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), selectedServer.Name);
                if (!Directory.Exists(downloadPath))
                {
                    Directory.CreateDirectory(downloadPath);
                }


                // try remove existing local copies                
                
                if(Program.ProgramConfig.FtpDownloadMode == 1)
                {
                    //clean download
                    // ... arkprofile(s)
                    var profileFiles = Directory.GetFiles(downloadPath, "*.arkprofile");
                    foreach (var profileFilename in profileFiles)
                    {
                        try
                        {
                            File.Delete(profileFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }

                    // ... arktribe(s)
                    var tribeFiles = Directory.GetFiles(downloadPath, "*.arktribe");
                    foreach (var tribeFilename in tribeFiles)
                    {
                        try
                        {
                            File.Delete(tribeFilename);
                        }
                        finally
                        {
                            //ignore, issue deleting the file but not concerned.
                        }
                    }
                }

                ftpClient.Connect();
                var serverFiles = ftpClient.GetListing(selectedServer.SaveGamePath);
                string localFilename = "";

                //get correct casing for the selected map file
                var serverSaveFile = serverFiles.Where(f => f.Name.ToLower() == selectedServer.Map.ToLower()).FirstOrDefault();
                if (serverSaveFile != null)
                {
                    localFilename = Path.Combine(downloadPath, serverSaveFile.Name);

                    bool shouldDownload = true;


                    if(File.Exists(localFilename) && serverSaveFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                    {
                        if(Program.ProgramConfig.FtpDownloadMode == 0)
                        {
                            shouldDownload = false;
                        } 
                        
                    }

                    if (shouldDownload)
                    {
                        using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                        {
                            ftpClient.Download(outputStream, serverSaveFile.FullName);
                            outputStream.Flush();
                        }
                        File.SetLastWriteTimeUtc(localFilename, serverSaveFile.Modified.ToUniversalTime());
                    }


                    
                    //get .arktribe files
                    var serverTribeFiles = serverFiles.Where(f => f.Name.EndsWith(".arktribe"));
                    if (serverTribeFiles != null && serverTribeFiles.Count() > 0)
                    {
                        foreach(var serverTribeFile in serverTribeFiles)
                        {
                            localFilename = Path.Combine(downloadPath, serverTribeFile.Name);
                            shouldDownload = true;
                            if (File.Exists(localFilename) && serverTribeFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                            {
                                if (Program.ProgramConfig.FtpDownloadMode == 0)
                                {
                                    shouldDownload = false;
                                }

                            }


                            if (shouldDownload)
                            {
                                using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                {
                                    ftpClient.Download(outputStream, serverTribeFile.FullName);
                                    outputStream.Flush();
                                }
                                File.SetLastWriteTimeUtc(localFilename, serverTribeFile.Modified.ToUniversalTime());
                            }

                        }

                    }


                    //get .arkprofile files
                    var serverProfileFiles = serverFiles.Where(f => f.Name.EndsWith(".arkprofile"));
                    if (serverProfileFiles != null && serverProfileFiles.Count() > 0)
                    {
                        foreach (var serverProfileFile in serverProfileFiles)
                        {

                            localFilename = Path.Combine(downloadPath, serverProfileFile.Name);
                            shouldDownload = true;
                            if (File.Exists(localFilename) && serverProfileFile.Modified.ToUniversalTime() <= File.GetLastWriteTimeUtc(localFilename))
                            {
                                if (Program.ProgramConfig.FtpDownloadMode == 0)
                                {
                                    shouldDownload = false;
                                }

                            }
                            if (shouldDownload)
                            {
                                using (FileStream outputStream = new FileStream(localFilename, FileMode.Create))
                                {
                                    ftpClient.Download(outputStream, serverProfileFile.FullName);
                                    outputStream.Flush();
                                }
                                File.SetLastWriteTimeUtc(localFilename, serverProfileFile.Modified.ToUniversalTime());
                            }
                        }

                    }
                }
                else
                {
                    //save file not found on server.
                    MessageBox.Show($"Unable to find the selected game save on the specified server: {selectedServer.Map}", "Save Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            
            this.Cursor = Cursors.Default;
            btnRefresh.Enabled = true;
            lblStatus.Text = "Save file downloaded.";
            lblStatus.Refresh();

        }
        
        private List<Tuple<string,DateTime>> GetFtpFileList(Uri address, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(address);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(username, password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            List<Tuple<string,DateTime>> fileList = new List<Tuple<string,DateTime>>();
            string[] list = null;


            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                list = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach(var line in list)
            {
                if (!line.Contains("<DIR>")){
                    //file
                    string fileLine = line.Substring(0, line.LastIndexOf(" "));

                    int.TryParse(fileLine.Substring(0, 2), out int dateMonth);
                    int.TryParse(fileLine.Substring(3, 2), out int dateDay);
                    int.TryParse(fileLine.Substring(6, 2), out int dateYear);

                    int.TryParse(fileLine.Substring(10, 2), out int dateHour);
                    int.TryParse(fileLine.Substring(13, 2), out int dateMin);
                    if (fileLine.ToLower().Contains("pm"))
                    {
                        dateHour += 12;
                        if (dateHour > 23)
                        {
                            dateHour = 0;
                        }
                    }

                    if (dateYear.ToString().Length == 2) dateYear = dateYear + 2000;


                    DateTime fileDateTime = DateTime.Now;
                    try
                    {

                        string[] FtpDateFormats = { "yyyyMMddHHmmss", "yyyyMMddHHmmss'.'f", "yyyyMMddHHmmss'.'ff", "yyyyMMddHHmmss'.'fff", "MMM dd  yyyy", "MMM  d  yyyy", "MMM dd HH:mm", "MMM  d HH:mm" };

                        DateTime.TryParseExact(fileLine, FtpDateFormats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime ftpFileTime);


                        fileDateTime = new DateTime(dateYear, dateMonth, dateDay, dateHour, dateMin, 0);
                    }
                    catch
                    {
                        //try other formats
                        fileDateTime = DateTime.Now;
                    }
                    

                    string fileName = line.Substring(line.LastIndexOf(" ")+1);
                    fileList.Add(new Tuple<string,DateTime>(fileName, fileDateTime));
                }
            }

            return fileList;
        }

        private Bitmap DrawMap(decimal selectedX, decimal selectedY) {
            if (gd == null)
            {
                return new Bitmap(1024, 1024);
            }

            lblStatus.Text = "Updating map display.";
            lblStatus.Refresh();

            lastSelectedX = selectedX;
            lastSelectedY = selectedY;

            Bitmap originalImage = null;
            switch (gd.SaveState.MapName.ToLower())
            {
                case "thecenter":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_thecenter, new Size(1024, 1024));
                    break;
                case "theisland":
                    //originalImage = new Bitmap(ArkSavegameToolkitNet.Domain.MapResources.topo_map_TheIsland, new Size(1024, 1024));
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_theisland, new Size(1024, 1024));
                    break;
                case "scorchedearth_p":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_scorchedearth, new Size(1024, 1024));
                    break;
                case "aberration_p":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_aberration, new Size(1024, 1024));
                    break;
                case "ragnarok":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_ragnarok, new Size(1024, 1024));
                    break;

                case "extinction":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_extinction, new Size(1024, 1024));
                    break;
                case "valguero_p":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_valguero, new Size(1024, 1024));
                    break;

                case "crystalisles":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_crystalisles, new Size(1024, 1024));
                    break;
                case "tunguska_p":

                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_tunguska, new Size(1024, 1024));
                    break;

                case "caballus_p":

                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_caballus, new Size(1024, 1024));
                    break;

                case "genesis":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_genesis, new Size(1024, 1024));
                    break;
                case "astralark":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_astralark, new Size(1024, 1024));
                    break;
                case "hope":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_hope, new Size(1024, 1024));
                    break;

                case "viking_p":

                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_fjordur, new Size(1024, 1024));
                    break;
                case "tiamatprime":
                    originalImage = new Bitmap(ARKViewer.Properties.Resources.map_tiamat, new Size(1024, 1024));
                    break;
                default:
                    originalImage = new Bitmap(1024,1024);
                    break;
            }


            Bitmap bitmap = new Bitmap(1024, 1024);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(originalImage, new Rectangle(0, 0, 1024, 1024));

            decimal markerX = 0m;
            decimal markerY = 0m;

            Tuple<int, int, decimal, decimal, decimal, decimal> mapvals = Tuple.Create(1024, 1024, 0.0m, 0.0m, 100.0m, 100.0m);

            //obelisks/tribute terminals

            if (ARKViewer.Program.ProgramConfig.Obelisks)
            {
                var terminalMarkers = ARKViewer.Program.ProgramConfig.TerminalMarkers.Where(t => t.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower());
                foreach (var terminal in terminalMarkers)
                {

                    //attempt to determine colour from class name
                    Color brushColor = Color.Silver;
                    string colourName = terminal.Colour;

                    try
                    {
                        brushColor = Color.FromName(colourName);
                    }
                    catch
                    {
                        brushColor = Color.WhiteSmoke;
                    }
                    

                    markerX = ((decimal)terminal.Lon - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)terminal.Lat - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);


                    graphics.FillEllipse(new SolidBrush(brushColor), (float)markerX - 25f, (float)markerY - 25f, 50, 50);
                    graphics.DrawEllipse(new Pen(Color.White, 2), (float)markerX - 25f, (float)markerY - 25f, 50, 50);

                    Bitmap mapMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_obelisk, new Size(40, 40));
                    graphics.DrawImage(mapMarker, (float)markerX - 20f, (float)markerY - 20f);


                }
            }

            if (ARKViewer.Program.ProgramConfig.Glitches)
            {
                foreach (var glitch in ARKViewer.Program.ProgramConfig.GlitchMarkers.Where(g => g.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower()))
                {
                    //attempt to determine colour from class name
                    Color brushColor = Color.Silver;
                    string colourName = glitch.Colour;

                    try
                    {
                        brushColor = Color.FromName(colourName);
                    }
                    catch
                    {
                        brushColor = Color.WhiteSmoke;
                    }
                    

                    markerX = ((decimal)glitch.Lon - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)glitch.Lat - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    float markerSize = 25;

                    graphics.FillEllipse(new SolidBrush(brushColor), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                    graphics.DrawEllipse(new Pen(Color.White, 2), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);

                    float iconSize = 18.75f;
                    Bitmap mapMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_glitch, new Size((int)iconSize, (int)iconSize));
                    graphics.DrawImage(mapMarker, (float)markerX - (iconSize / 2), (float)markerY - (iconSize / 2));
                }
            }
            



            //map markers
            if (lvwMapMarkers.SelectedItems.Count > 0)
            {
                MapMarker selectedMarker = (MapMarker)lvwMapMarkers.SelectedItems[0].Tag;

                markerX = ((decimal)selectedMarker.Lon - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                markerY = ((decimal)selectedMarker.Lat - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                Color markerBackGround = Color.FromArgb(selectedMarker.Colour);
                graphics.FillEllipse(new SolidBrush(markerBackGround), (float)markerX - 17.5f, (float)markerY - 17.5f, 35, 35);

                if (selectedMarker.BorderWidth > 0)
                {
                    Color markerBorder = Color.FromArgb(selectedMarker.BorderColour);
                    graphics.DrawEllipse(new Pen(markerBackGround, selectedMarker.BorderWidth), (float)markerX - 17.5f, (float)markerY - 17.5f, 35, 35);

                }

                if (selectedMarker.Marker > 0)
                {
                    Image markerImage = (Image)ARKViewer.Properties.Resources.ResourceManager.GetObject($"marker_{selectedMarker.Marker}");
                    graphics.DrawImage(new Bitmap(markerImage, 28, 28), (float)markerX - 14.0f, (float)markerY - 14.0f, 28.0f, 28.0f);
                }

            }


            //charge nodes?
            if (ARKViewer.Program.ProgramConfig.ChargeNodes) { 
                var chargeNodeList = gd.Structures.Where(s => s.ClassName.StartsWith("PrimalStructurePowerNode"));

                foreach (var chargeNode in chargeNodeList)
                {
                    markerX = ((decimal)Math.Round(chargeNode.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(chargeNode.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Color borderColor = Color.Red;
                    if(chargeNode.Inventory!=null & chargeNode.Inventory.Count(i=>i.ClassName!= "PrimalItem_PowerNodeCharge_C") > 0)
                    {
                        borderColor = Color.Green;
                    }
                    
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    Bitmap chargeMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_battery, new Size(20, 20));
                    graphics.DrawImage(chargeMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);
                }


            }


            //beaver dams
            if (ARKViewer.Program.ProgramConfig.BeaverDams)
            {
                var beaverDamList = gd.Structures.Where(f => f.ClassName == "BeaverDam_C");
                foreach (var beaverDam in beaverDamList)
                {
                    markerX = ((decimal)Math.Round(beaverDam.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(beaverDam.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Color borderColor = Color.Red;
                    if(beaverDam.Inventory!=null && beaverDam.Inventory.Count() > 0)
                    {
                        borderColor = Color.Green;
                    }
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    Bitmap beaverMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_beaver, new Size(20, 20));
                    graphics.DrawImage(beaverMarker, (float)markerX -10.0f, (float)markerY -10.0f);
                }
            }

            //deino nests
            if (ARKViewer.Program.ProgramConfig.DeinoNests)
            {
                var deinoNestList = gd.Structures.Where(f => f.ClassName == "DeinonychusNest_C");
                foreach (var deinoNest in deinoNestList)
                {
                    markerX = ((decimal)Math.Round(deinoNest.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(deinoNest.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    
                    Color borderColor = Color.Red;
                    ArkItem fertileEgg = gd.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_Deinonychus_Fertilized_C" && i.Location != null && i.Location.Latitude.Value.ToString("0.00").Equals(deinoNest.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(deinoNest.Location.Longitude.Value.ToString("0.00"))).FirstOrDefault();
                    if (fertileEgg != null)
                    {

                        int eggLevel = fertileEgg.EggBaseStats.Sum(s => s) + 1;


                        borderColor = Color.Green;
                    }
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap deinoMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_deino, new Size(20, 20));
                    graphics.DrawImage(deinoMarker, (float)markerX -10.0f, (float)markerY -10.0f);

                }
            }

            //wyvern nests
            if (ARKViewer.Program.ProgramConfig.WyvernNests)
            {

                var wyvernNestList = gd.Structures.Where(f => f.ClassName.StartsWith("WyvernNest_"));
                //var allNests = gd.Structures.Where(f => f.ClassName.Contains("heir"));
                foreach (var wyvernNest in wyvernNestList)
                {
                    markerX = ((decimal)Math.Round(wyvernNest.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(wyvernNest.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Color borderColor = Color.Red;
                    ArkItem fertileEgg = gd.Items.Where(i => i.ClassName.ToLower().Contains("egg") && i.Location != null && i.Location.Latitude.Value.ToString("0.00").Equals(wyvernNest.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(wyvernNest.Location.Longitude.Value.ToString("0.00")) && i.OwnerInventoryId == null).FirstOrDefault();
                    if (fertileEgg != null)
                    {

                        int eggLevel = fertileEgg.EggBaseStats.Sum(s => s) + 1;


                        borderColor = Color.Green;
                    }


                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap wyvernMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_wyvern, new Size(20, 20));
                    graphics.DrawImage(wyvernMarker, (float)markerX -10.0f, (float)markerY -10.0f);

                }
            }

            //rock drakes (RockDrakeNest_C)
            if (ARKViewer.Program.ProgramConfig.DrakeNests)
            {

                var drakeNestList = gd.Structures.Where(f => f.ClassName == "RockDrakeNest_C");
                foreach (var drakeNest in drakeNestList)
                {



                    markerX = ((decimal)Math.Round(drakeNest.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(drakeNest.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    Color markerColor = Color.White;
                    graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - 15f, (float)markerY - 15f, 30, 30);


                    Color borderColor = Color.Red;

                    ArkItem fertileEgg = gd.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_RockDrake_Fertilized_C" && i.OwnerInventoryId == null && i.OwnerContainerId == null && i.Location.Latitude.Value.ToString("0.00").Equals(drakeNest.Location.Latitude.Value.ToString("0.00")) && i.Location.Longitude.Value.ToString("0.00").Equals(drakeNest.Location.Longitude.Value.ToString("0.00"))).FirstOrDefault();
                    if (fertileEgg != null)
                    {

                        int eggLevel = fertileEgg.EggBaseStats.Sum(s => s) + 1;


                        borderColor = Color.Green;
                    }
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap wyvernMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_rockdrake, new Size(20, 20));
                    graphics.DrawImage(wyvernMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);

                }
            }

            //magmasaur nests
            if (ARKViewer.Program.ProgramConfig.MagmaNests)
            {
                var magmaNests = gd.Structures.Where(f => f.ClassName == "CherufeNest_C");
                foreach (var magmaNest in magmaNests)
                {

                    markerX = ((decimal)Math.Round(magmaNest.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(magmaNest.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    Color markerColor = Color.White;
                    graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Color borderColor = Color.Red;

                    ArkItem fertileEgg = gd.Items.Where(i => i.ClassName == "PrimalItemConsumable_Egg_Cherufe_Fertilized_C" && i.Location != null && (i.Location.Latitude.Value - magmaNest.Location.Latitude.Value < 10) && (i.Location.Longitude.Value - magmaNest.Location.Longitude.Value) < 10 && i.OwnerInventoryId == null).FirstOrDefault();
                    if (fertileEgg != null)
                    {

                        int eggLevel = fertileEgg.EggBaseStats.Sum(s => s) + 1;


                        borderColor = Color.Green;
                    }
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap magmaMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_magmasaur, new Size(20, 20));
                    graphics.DrawImage(magmaMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);

                }
            }
            


            //oil veins (OilVein_)
            if (ARKViewer.Program.ProgramConfig.OilVeins)
            {
                var oilVeinList = gd.Structures.Where(f => f.ClassName.StartsWith("OilVein_"));
                foreach (var oilVein in oilVeinList)
                {
                    markerX = ((decimal)Math.Round(oilVein.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(oilVein.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    
                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    graphics.DrawEllipse(new Pen(Color.Black, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap oilMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_oil, new Size(20, 20));
                    graphics.DrawImage(oilMarker, (float)markerX -10.0f, (float)markerY -10.0f);

                }

            }

            //water veins (WaterVein_)
            if (ARKViewer.Program.ProgramConfig.WaterVeins)
            {
                var waterVeinList = gd.Structures.Where(f => f.ClassName.StartsWith("WaterVein_"));
                foreach (var waterVein in waterVeinList)
                {
                    markerX = ((decimal)Math.Round(waterVein.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(waterVein.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    graphics.DrawEllipse(new Pen(Color.Black, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap waterMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_water, new Size(20, 20));
                    graphics.DrawImage(waterMarker, (float)markerX -10.0f, (float)markerY - 10.0f);

                }

            }

            //gas veins (GasVein_)
            if (ARKViewer.Program.ProgramConfig.GasVeins)
            {
                var gasVeinList = gd.Structures.Where(f => f.ClassName.StartsWith("GasVein_"));
                foreach (var gasVein in gasVeinList)
                {
                    markerX = ((decimal)Math.Round(gasVein.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(gasVein.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.White), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Color borderColor = Color.Red;
                    if(gasVein.Inventory!=null && gasVein.Inventory.Count() > 0)
                    {
                        borderColor = Color.Green;
                    }
                    graphics.DrawEllipse(new Pen(borderColor, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                    Bitmap gasMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_gas, new Size(20, 20));
                    graphics.DrawImage(gasMarker, (float)markerX -10.0f, (float)markerY - 10.0f);

                }
            }

            //artifacts
            if (ARKViewer.Program.ProgramConfig.Artifacts)
            {
                var artifactList = gd.Structures.Where(f => f.ClassName.StartsWith("ArtifactCrate_"));
                foreach (var artifact in artifactList)
                {
                    markerX = ((decimal)Math.Round(artifact.Location.Longitude.Value,2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(artifact.Location.Latitude.Value,2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                    graphics.FillEllipse(new SolidBrush(Color.FloralWhite), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);
                    graphics.DrawEllipse(new Pen(Color.Yellow, 1), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);

                    Bitmap gasMarker = new Bitmap(ARKViewer.Properties.Resources.structure_marker_trophy, new Size(20, 20));
                    graphics.DrawImage(gasMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);

                }

            }

            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":
                    if (cboWildClass.SelectedItem != null)
                    {

                        DinoSummary selectedSummary = (DinoSummary)cboWildClass.SelectedItem;
                        string className = selectedSummary.ClassName;
                        float markerSize = 10f;

                        int minLevel = (int)udWildMin.Value;
                        int maxLevel = (int)udWildMax.Value;
                        float selectedLat = (float)udWildLat.Value;
                        float selectedLon = (float)udWildLon.Value;
                        float selectedRad = (float)udWildRadius.Value;

                        foreach(ListViewItem item in lvwWildDetail.Items)
                        {
                            
                            decimal.TryParse(item.SubItems[4].Text, out decimal latWild);
                            decimal.TryParse(item.SubItems[5].Text, out decimal lonWild);

                            markerX = ((decimal)Math.Round(lonWild, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                            markerY = ((decimal)Math.Round(latWild, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                            Color markerColor = Color.Blue;

                            graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);

                        }
                    }

                    break;
                case "tpgTamed":

                    

                    if(lvwTameDetail.Items.Count > 0)
                    {
                        float markerSize = 10f;

                        foreach (ListViewItem item in lvwTameDetail.Items)
                        {

                            decimal.TryParse(item.SubItems[6].Text, out decimal longtitude);
                            decimal.TryParse(item.SubItems[5].Text, out decimal latitude);

                            markerX = ((decimal)Math.Round(longtitude, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                            markerY = ((decimal)Math.Round(latitude, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                            Color markerColor = Color.Blue;

                            graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                        }

                    }


                    break;
                case "tpgStructures":
                    //map out player structures
                    if(lvwStructureLocations.Items.Count > 0)
                    {

                        foreach (ListViewItem item in lvwStructureLocations.Items)
                        {
                            if(item.Tag is ArkStructure){
                                ArkStructure structure = (ArkStructure)item.Tag;
                                if (structure.Location != null)
                                {
                                    float markerSize = 10f;

                                    markerX = ((decimal)Math.Round(structure.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                    markerY = ((decimal)Math.Round(structure.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                    graphics.FillEllipse(new SolidBrush(Color.AliceBlue), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                    Color borderColour = Color.Blue;
                                    int borderSize = 1;
                                    if (item.Selected)
                                    {
                                        borderColour = Color.Red;
                                        borderSize = 1;
                                    }
                                    graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                }
                            }
                            else
                            {
                                ArkTamedCreature raft = (ArkTamedCreature)item.Tag;
                                if (raft.Location != null)
                                {
                                    float markerSize = 10f;

                                    markerX = ((decimal)Math.Round(raft.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                    markerY = ((decimal)Math.Round(raft.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                    graphics.FillEllipse(new SolidBrush(Color.AliceBlue), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                    Color borderColour = Color.Blue;
                                    int borderSize = 1;
                                    if (item.Selected)
                                    {
                                        borderColour = Color.Red;
                                        borderSize = 1;
                                    }
                                    graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                }
                            }
                            
                        }

                        if (lvwStructureLocations.SelectedItems.Count > 0)
                        {
                            ListViewItem item = lvwStructureLocations.SelectedItems[0];
                            if (item.Tag is ArkStructure)
                            {
                                ArkStructure structure = (ArkStructure)lvwStructureLocations.SelectedItems[0].Tag;

                                if (structure.Location != null)
                                {
                                    float markerSize = 10f;

                                    markerX = ((decimal)Math.Round(structure.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                    markerY = ((decimal)Math.Round(structure.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                    graphics.FillEllipse(new SolidBrush(Color.IndianRed), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                    Color borderColour = Color.Red;
                                    int borderSize = 1;
                                    graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                }
                            }
                            else
                            {
                                ArkTamedCreature raft = (ArkTamedCreature)lvwStructureLocations.SelectedItems[0].Tag;

                                if (raft.Location != null)
                                {
                                    float markerSize = 10f;

                                    markerX = ((decimal)Math.Round(raft.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                    markerY = ((decimal)Math.Round(raft.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                    graphics.FillEllipse(new SolidBrush(Color.IndianRed), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                    Color borderColour = Color.Red;
                                    int borderSize = 1;
                                    graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                }
                            }
                        }

                    }

                    break;
                case "tpgPlayers":
                    //players
                    if (lvwPlayers.Items.Count > 0)
                    {
                        foreach (ListViewItem playerItem in lvwPlayers.Items)
                        {
                            ArkPlayer player = (ArkPlayer)playerItem.Tag;

                            if (player.Location != null)
                            {
                                markerX = ((decimal)Math.Round(player.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                markerY = ((decimal)Math.Round(player.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                graphics.FillEllipse(new SolidBrush(Color.FloralWhite), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);


                                Color borderColour = Color.Blue;
                                int borderSize = 1;
                                if (playerItem.Selected)
                                {
                                    borderColour = Color.Red;
                                    borderSize = 2;
                                }
                                graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);

                                Bitmap playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_28, new Size(20, 20));
                                if (player.Gender == ArkPlayerGender.Female)
                                {
                                    playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_29, new Size(20, 20));
                                }
                                graphics.DrawImage(playerMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);
                            }


                        }


                        //re-draw selected in-case it's been drawn over by somebody in same area
                        if (lvwPlayers.SelectedItems.Count > 0)
                        {
                            ListViewItem playerItem = lvwPlayers.SelectedItems[0];
                            ArkPlayer player = (ArkPlayer)playerItem.Tag;

                            if (player.Location != null)
                            {
                                markerX = ((decimal)Math.Round(player.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                markerY = ((decimal)Math.Round(player.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                graphics.FillEllipse(new SolidBrush(Color.FloralWhite), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);


                                Color borderColour = Color.Blue;
                                int borderSize = 1;
                                if (playerItem.Selected)
                                {
                                    borderColour = Color.Red;
                                    borderSize = 2;
                                }
                                graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);

                                Bitmap playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_28, new Size(20, 20));
                                if (player.Gender == ArkPlayerGender.Female)
                                {
                                    playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_29, new Size(20, 20));
                                }
                                graphics.DrawImage(playerMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);

                            }


                        }




                    }


                    break;
                case "tpgDroppedItems":

                    
                    if (lvwDroppedItems.Items.Count > 0)
                    {
                        float markerSize = 10f;

                        foreach (ListViewItem item in lvwDroppedItems.Items)
                        {

                            decimal.TryParse(item.SubItems[2].Text, out decimal latitude);
                            decimal.TryParse(item.SubItems[3].Text, out decimal longtitude);

                            markerX = ((decimal)Math.Round(longtitude, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                            markerY = ((decimal)Math.Round(latitude, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                            Color markerColor = Color.Blue;

                            graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                        }

                    }


                    break;
                case "tpgTribes":
                    if(lvwTribes.SelectedItems.Count > 0)
                    {
                        

                        if (chkTribeTames.Checked)
                        {
                            foreach(ListViewItem selectedItem in lvwTribes.SelectedItems)
                            {
                                TribeMap selectedTribe = (TribeMap)selectedItem.Tag;
                                //gold
                                var tribeTames = gd.NoRafts.Where(s => s.TargetingTeam == selectedTribe.TribeId);
                                if (tribeTames.Count() > 0)
                                {
                                    foreach (var structure in tribeTames)
                                    {
                                        if (structure.Location != null)
                                        {
                                            float markerSize = 10f;

                                            markerX = ((decimal)Math.Round(structure.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                            markerY = ((decimal)Math.Round(structure.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                            graphics.FillEllipse(new SolidBrush(Color.Gold), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                            Color borderColour = Color.Black;
                                            int borderSize = 1;
                                            graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                        }


                                    }
                                }
                            }
                            

                        }

                        if (chkTribeStructures.Checked)
                        {
                            //pale green
                            foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                            {
                                TribeMap selectedTribe = (TribeMap)selectedItem.Tag;
                                
                                var tribeStructures = gd.Structures.Where(s => s.TargetingTeam.GetValueOrDefault(0) == selectedTribe.TribeId);
                                if (tribeStructures.Count() > 0)
                                {
                                    foreach (var structure in tribeStructures)
                                    {
                                        if (structure.Location != null)
                                        {
                                            float markerSize = 10f;

                                            markerX = ((decimal)Math.Round(structure.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                            markerY = ((decimal)Math.Round(structure.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                            graphics.FillEllipse(new SolidBrush(Color.PaleGreen), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);


                                            Color borderColour = Color.Black;
                                            int borderSize = 1;
                                            graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - (markerSize / 2), (float)markerY - (markerSize / 2), markerSize, markerSize);
                                        }


                                    }
                                }
                            }
                            

                        }

                        if (chkTribePlayers.Checked)
                        {
                            foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                            {
                                TribeMap selectedTribe = (TribeMap)selectedItem.Tag;

                                var tribePlayers = gd.Players.Where(p => p.TribeId.GetValueOrDefault(0) == selectedTribe.TribeId);
                                if (tribePlayers != null && tribePlayers.Count() > 0)
                                {
                                    foreach (var player in tribePlayers)
                                    {
                                        if (player.Location != null)
                                        {
                                            markerX = ((decimal)Math.Round(player.Location.Longitude.Value, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                                            markerY = ((decimal)Math.Round(player.Location.Latitude.Value, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);
                                            graphics.FillEllipse(new SolidBrush(Color.FloralWhite), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);

                                            Color borderColour = Color.Blue;
                                            int borderSize = 1;
                                            graphics.DrawEllipse(new Pen(borderColour, borderSize), (float)markerX - 15.0f, (float)markerY - 15.0f, 30, 30);

                                            Bitmap playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_28, new Size(20, 20));
                                            if (player.Gender == ArkPlayerGender.Female)
                                            {
                                                playerMarker = new Bitmap(ARKViewer.Properties.Resources.marker_29, new Size(20, 20));
                                            }
                                            graphics.DrawImage(playerMarker, (float)markerX - 10.0f, (float)markerY - 10.0f);
                                        }
                                    }
                                }
                            }
                            
                            
                        }

                    }

                    break;
                default:

                    break;
            }


            
            if (selectedX != 0 && selectedY != 0)
            {
                
                markerX = ((decimal)Math.Round(selectedX, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                markerY = ((decimal)Math.Round(selectedY, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                Color markerColor = Color.Red;

                graphics.FillEllipse(new SolidBrush(markerColor), (float)markerX - 5.0f, (float)markerY - 5.0f, 10, 10);

            }

            lblStatus.Text = "Map display updated.";
            lblStatus.Refresh();


            return bitmap;
        }

        private void LvwSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWildDetail();
        }


        private void LoadDroppedItemDetail()
        {
            if (gd == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating dropped item data.";
            lblStatus.Refresh();

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            lvwDroppedItems.BeginUpdate();
            lvwDroppedItems.Items.Clear();

            //player
            ComboValuePair comboValue = (ComboValuePair)cboDroppedPlayer.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);

            string selectedClass = "NONE";
            comboValue = (ComboValuePair)cboDroppedItem.SelectedItem;
            selectedClass = comboValue.Key;

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
            if (gd.DroppedItems != null && gd.DroppedItems.Count() > 0)
            {



                var droppedItems = gd.DroppedItems.Where(s =>
                                                                (s.ClassName == selectedClass || selectedClass.Length == 0)
                                                                &&
                                                                (s.DroppedByPlayerId == selectedPlayerId || selectedPlayerId == 0)
                                                                && s.DroppedByPlayerId != null

                                                           );

                //item, dropped by, lat, lon, tribe, player

                if(droppedItems!=null && droppedItems.LongCount(d=>d.DroppedByPlayerId !=0) > 0)
                {
                    
                    Parallel.ForEach(droppedItems,  droppedItem =>
                    {
                        string itemName = droppedItem.ClassName;
                        ItemClassMap itemMap = Program.ProgramConfig.ItemMap.Where(m => m.ClassName == droppedItem.ClassName).FirstOrDefault();
                        if (itemMap != null)
                        {
                            itemName = itemMap.FriendlyName;
                        }
                        //tribe name
                        string tribeName = "";


                        //player name
                        string playerName = "";
                        PlayerMap playerMap = allPlayers.Where(p => p.PlayerId == droppedItem.DroppedByPlayerId).FirstOrDefault();
                        if (playerMap != null)
                        {
                            playerName = playerMap.PlayerName;
                            TribeMap tribeMap = allTribes.Where(t => t.TribeId == playerMap.TribeId).FirstOrDefault();
                            if (tribeMap != null)
                            {
                                tribeName = tribeMap.TribeName;
                            }
                        }
                        else
                        {
                            //check tamed dinos

                        }

                        ListViewItem newItem = new ListViewItem(itemName);
                        newItem.Tag = droppedItem;
                        newItem.SubItems.Add(droppedItem.DroppedByName);
                        newItem.SubItems.Add(droppedItem.Location == null ? "N/A" : droppedItem.Location.Latitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(droppedItem.Location == null ? "N/A" : droppedItem.Location.Longitude.Value.ToString("0.00"));
                        newItem.SubItems.Add(tribeName);
                        newItem.SubItems.Add(playerName);

                        listItems.Add(newItem);

                    });

                }

            }

            if(selectedClass == "DeathItemCache_PlayerDeath_C")
            {
                Parallel.ForEach(gd.PlayerDeathCache, playerCache =>
                {
                    string itemName = "Player Cache";

                    //tribe name
                    string tribeName = "";

                    //player name
                    string playerName = "";
                    PlayerMap playerMap = allPlayers.Where(p => p.PlayerId == playerCache.OwningPlayerId).FirstOrDefault();
                    if (playerMap != null)
                    {
                        playerName = playerMap.PlayerName;
                        TribeMap tribeMap = allTribes.Where(t => t.TribeId == playerMap.TribeId).FirstOrDefault();
                        if (tribeMap != null)
                        {
                            tribeName = tribeMap.TribeName;
                        }
                    }
                    else
                    {
                        //check tamed dinos

                    }

                    ListViewItem newItem = new ListViewItem(itemName);
                    newItem.Tag = playerCache;
                    newItem.SubItems.Add(playerCache.OwnerName);
                    newItem.SubItems.Add(playerCache.Location == null ? "N/A" : playerCache.Location.Latitude.Value.ToString("0.00"));
                    newItem.SubItems.Add(playerCache.Location == null ? "N/A" : playerCache.Location.Longitude.Value.ToString("0.00"));
                    newItem.SubItems.Add(tribeName);
                    newItem.SubItems.Add(playerName);

                    listItems.Add(newItem);

                });

            }


            lvwDroppedItems.Items.AddRange(listItems.ToArray());
            lvwDroppedItems.EndUpdate();
            lblStatus.Text = "Dropped item data populated.";
            lblStatus.Refresh();

            lblCountDropped.Text = $"Count: {lvwDroppedItems.Items.Count}";

            if (tabFeatures.SelectedTab.Name == "tpgDroppedItems")
            {
                picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
                picMap.Refresh();
            }


            this.Cursor = Cursors.Default;
        }


        private void LoadTameDetail()
        {
            if (gd == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating tame data.";
            lblStatus.Refresh();

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            decimal selectedX = 0.0m;
            decimal selectedY = 0.0m;

            if (cboTameClass.SelectedItem != null)
            {
                DinoSummary selectedSummary = (DinoSummary)cboTameClass.SelectedItem;

                ulong selectedId = 0;
                if (lvwTameDetail.SelectedItems.Count > 0)
                {
                    ulong.TryParse(lvwTameDetail.SelectedItems[0].Tag.ToString(), out selectedId);
                }
                lvwTameDetail.BeginUpdate();
                lvwTameDetail.Items.Clear();

                string className = selectedSummary.ClassName;

                //tribe
                int selectedTribeId = 0;
                int selectedPlayerId = 0;

                if(cboTameTribes.SelectedItem != null)
                {
                    ComboValuePair comboValue = (ComboValuePair)cboTameTribes.SelectedItem;
                    int.TryParse(comboValue.Key, out selectedTribeId);

                }

                //player
                if (cboTamePlayers.SelectedItem != null)
                {
                    ComboValuePair comboValue = (ComboValuePair)cboTamePlayers.SelectedItem;
                    int.TryParse(comboValue.Key, out selectedPlayerId);
                }

                var detailList = gd.TamedCreatures
                                    .Where(w => 
                                                (w.ClassName == className || className == "")
                                                & !(w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                                && (selectedTribeId == 0 || w.TargetingTeam == selectedTribeId)
                                                && (selectedPlayerId == 0 || (w.OwningPlayerId.GetValueOrDefault(0) == selectedPlayerId || w.ImprinterPlayerDataId.GetValueOrDefault(0) == selectedPlayerId ))
                                                && (chkCryo.Checked || w.IsCryo == false)
                                                && (chkCryo.Checked || w.IsVivarium == false)

                                           );

                //change into a strongly typed list for use in parallel
                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();
                Parallel.ForEach(detailList, detail =>
                {
                    var dinoMap = ARKViewer.Program.ProgramConfig.DinoMap.Where(dino => dino.ClassName == detail.ClassName).FirstOrDefault();

                    string creatureClassName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;
                    string creatureName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;

                    if (detail.Name != null)
                    {
                        creatureName = detail.Name;
                    }

                    if (creatureName.ToLower().Contains("queen"))
                    {
                        detail.Gender = ArkCreatureGender.Female;
                    }

                    ListViewItem item = new ListViewItem(creatureClassName);
                    item.Tag = detail;
                    item.UseItemStyleForSubItems = false;

                    item.SubItems.Add(creatureName);
                    item.SubItems.Add(detail.Gender.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(detail.Level.ToString());
                    item.SubItems.Add(((decimal)detail.Location.Latitude).ToString("0.00"));
                    item.SubItems.Add(((decimal)detail.Location.Longitude).ToString("0.00"));
                    if (optStatsTamed.Checked)
                    {
                        item.SubItems.Add(detail.TamedStats[0].ToString());
                        item.SubItems.Add(detail.TamedStats[1].ToString());
                        item.SubItems.Add(detail.TamedStats[8].ToString());
                        item.SubItems.Add(detail.TamedStats[7].ToString());
                        item.SubItems.Add(detail.TamedStats[9].ToString());
                        item.SubItems.Add(detail.TamedStats[4].ToString());
                        item.SubItems.Add(detail.TamedStats[3].ToString());
                        item.SubItems.Add(detail.TamedStats[11].ToString());

                    }
                    else
                    {
                        item.SubItems.Add(detail.BaseStats[0].ToString());
                        item.SubItems.Add(detail.BaseStats[1].ToString());
                        item.SubItems.Add(detail.BaseStats[8].ToString());
                        item.SubItems.Add(detail.BaseStats[7].ToString());
                        item.SubItems.Add(detail.BaseStats[9].ToString());
                        item.SubItems.Add(detail.BaseStats[4].ToString());
                        item.SubItems.Add(detail.BaseStats[3].ToString());
                        item.SubItems.Add(detail.BaseStats[11].ToString());

                    }

                    item.SubItems.Add(detail.TamedOnServerName);

                    string tamerName = detail.TamerName != null ? detail.TamerName : "";
                    string imprinterName = detail.ImprinterName;

                    if (detail.TargetingTeam > 0 && detail.ImprinterPlayerDataId == null && tamerName.Length == 0)
                    {
                        tamerName = allTribes.FirstOrDefault(t => t.TribeId == detail.TargetingTeam)?.TribeName;
                    }

                    if (detail.ImprinterPlayerDataId != null)
                    {
                        tamerName = "";
                    }

                    item.SubItems.Add(tamerName);
                    item.SubItems.Add(detail.ImprinterName);
                    item.SubItems.Add((detail.DinoImprintingQuality * 100).ToString());

                    bool isStored = detail.IsCryo | detail.IsVivarium;

                    item.SubItems.Add(isStored.ToString());

                    if (detail.IsCryo)
                    {
                        item.BackColor = Color.LightSkyBlue;
                        item.SubItems[1].BackColor = Color.LightSkyBlue;
                        item.SubItems[2].BackColor = Color.LightSkyBlue;
                        item.SubItems[3].BackColor = Color.LightSkyBlue;
                        item.SubItems[4].BackColor = Color.LightSkyBlue;
                        item.SubItems[5].BackColor = Color.LightSkyBlue;
                        item.SubItems[6].BackColor = Color.LightSkyBlue;
                        item.SubItems[7].BackColor = Color.LightSkyBlue;
                        item.SubItems[8].BackColor = Color.LightSkyBlue;
                        item.SubItems[9].BackColor = Color.LightSkyBlue;
                        item.SubItems[10].BackColor = Color.LightSkyBlue;
                        item.SubItems[11].BackColor = Color.LightSkyBlue;
                        item.SubItems[12].BackColor = Color.LightSkyBlue;
                        item.SubItems[13].BackColor = Color.LightSkyBlue;
                        item.SubItems[14].BackColor = Color.LightSkyBlue;
                        item.SubItems[15].BackColor = Color.LightSkyBlue;
                        item.SubItems[16].BackColor = Color.LightSkyBlue;
                        item.SubItems[17].BackColor = Color.LightSkyBlue;
                        item.SubItems[18].BackColor = Color.LightSkyBlue;
                    }
                    else if (detail.IsVivarium)
                    {
                        item.BackColor = Color.LightPink;
                        item.SubItems[1].BackColor = Color.LightPink;
                        item.SubItems[2].BackColor = Color.LightPink;
                        item.SubItems[3].BackColor = Color.LightPink;
                        item.SubItems[4].BackColor = Color.LightPink;
                        item.SubItems[5].BackColor = Color.LightPink;
                        item.SubItems[6].BackColor = Color.LightPink;
                        item.SubItems[7].BackColor = Color.LightPink;
                        item.SubItems[8].BackColor = Color.LightPink;
                        item.SubItems[9].BackColor = Color.LightPink;
                        item.SubItems[10].BackColor = Color.LightPink;
                        item.SubItems[11].BackColor = Color.LightPink;
                        item.SubItems[12].BackColor = Color.LightPink;
                        item.SubItems[13].BackColor = Color.LightPink;
                        item.SubItems[14].BackColor = Color.LightPink;
                        item.SubItems[15].BackColor = Color.LightPink;
                        item.SubItems[16].BackColor = Color.LightPink;
                        item.SubItems[17].BackColor = Color.LightPink;
                        item.SubItems[18].BackColor = Color.LightPink;
                    }


                    //Colours
                    int colourCheck = (int)detail.Colors[0];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[0].ToString()); //14
                    ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[0]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[20].BackColor = selectedColor.Color;
                        item.SubItems[20].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[1];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[1].ToString()); //15
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[1]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[21].BackColor = selectedColor.Color;
                        item.SubItems[21].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[2];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[2].ToString()); //16
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[2]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[22].BackColor = selectedColor.Color;
                        item.SubItems[22].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[3];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[3].ToString()); //17
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[3]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[23].BackColor = selectedColor.Color;
                        item.SubItems[23].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[4];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[4].ToString()); //18
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[4]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[24].BackColor = selectedColor.Color;
                        item.SubItems[24].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[5];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[5].ToString()); //19
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[5]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[25].BackColor = selectedColor.Color;
                        item.SubItems[25].ForeColor = selectedColor.Color;
                    }


                    //mutations
                    item.SubItems.Add(detail.RandomMutationsFemale.ToString());
                    item.SubItems.Add(detail.RandomMutationsMale.ToString());

                    item.SubItems.Add(detail.Id.ToString());


                    if (detail.Id == selectedId)
                    {

                        item.Selected = true;
                        selectedX = (decimal)detail.Location.Longitude;
                        selectedY = (decimal)detail.Location.Latitude;
                    }

                    listItems.Add(item);
                });

                lvwTameDetail.Items.AddRange(listItems.ToArray());

                if (SortingColumn_DetailTame != null)
                {
                    lvwTameDetail.ListViewItemSorter =
                        new ListViewComparer(SortingColumn_DetailTame.Index, SortingColumn_DetailTame.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                    // Sort.
                    lvwTameDetail.Sort();
                }else
                {

                    SortingColumn_DetailTame = lvwTameDetail.Columns[0]; ;
                    SortingColumn_DetailTame.Text = "> " + SortingColumn_DetailTame.Text;

                    lvwTameDetail.ListViewItemSorter =
                        new ListViewComparer(0, SortOrder.Ascending);

                    // Sort.
                    lvwTameDetail.Sort();
                }


                lvwTameDetail.EndUpdate();

                lblStatus.Text = "Tame data populated.";
                lblStatus.Refresh();
                lblTameTotal.Text = $"Count: {lvwTameDetail.Items.Count}";


                /*
                Bitmap originalImage = DrawMap(selectedX, selectedY);

                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                */


                if(tabFeatures.SelectedTab.Name == "tpgTamed")
                {
                    picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
                    picMap.Refresh();
                }


            }



            this.Cursor = Cursors.Default;

        }

        private void LoadWildDetail()
        {
            if (gd == null)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            lblStatus.Text = "Populating creature data.";
            lblStatus.Refresh();

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            decimal selectedX = 0.0m;
            decimal selectedY = 0.0m;

            if (cboWildClass.SelectedItem != null)
            {
                DinoSummary selectedSummary = (DinoSummary)cboWildClass.SelectedItem;

                ulong selectedId = 0;
                if(lvwWildDetail.SelectedItems.Count > 0)
                {
                    ulong.TryParse(lvwWildDetail.SelectedItems[0].Tag.ToString(), out selectedId);
                }
                lvwWildDetail.BeginUpdate();
                lvwWildDetail.Items.Clear();
                

                string className = selectedSummary.ClassName;

                int minLevel = (int)udWildMin.Value;
                int maxLevel = (int)udWildMax.Value;
                float selectedLat = (float)udWildLat.Value;
                float selectedLon = (float)udWildLon.Value;
                float selectedRad = (float)udWildRadius.Value;

                var detailList = gd.WildCreatures.Where(w => 
                                            ((w.ClassName == className || className == "") && ((w.BaseLevel >= minLevel && w.BaseLevel <= maxLevel) || w.BaseLevel == 0))
                                            && (Math.Abs(w.Location.Latitude.GetValueOrDefault(0) - selectedLat) <= selectedRad)
                                            && (Math.Abs(w.Location.Longitude.GetValueOrDefault(0) - selectedLon) <= selectedRad)

                                ).OrderByDescending(c => c.BaseLevel);

                ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

                Parallel.ForEach(detailList, detail =>
                {
                    var dinoMap = ARKViewer.Program.ProgramConfig.DinoMap.Where(dino => dino.ClassName == detail.ClassName).FirstOrDefault();
                    string creatureName = dinoMap == null ? detail.ClassName : dinoMap.FriendlyName;
                    ListViewItem item = new ListViewItem(creatureName);//lvwWildDetail.Items.Add(creatureName);
                    item.Tag = detail;
                    item.UseItemStyleForSubItems = false;

                    if (creatureName.ToLower().Contains("queen"))
                    {
                        detail.Gender = ArkCreatureGender.Female;
                    }


                    item.SubItems.Add(detail.Gender.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(detail.BaseLevel.ToString());
                    item.SubItems.Add(((decimal)detail.Location.Latitude).ToString("0.00"));
                    item.SubItems.Add(((decimal)detail.Location.Longitude).ToString("0.00"));

                    item.SubItems.Add(detail.BaseStats[0].ToString());
                    item.SubItems.Add(detail.BaseStats[1].ToString());
                    item.SubItems.Add(detail.BaseStats[8].ToString());
                    item.SubItems.Add(detail.BaseStats[7].ToString());
                    item.SubItems.Add(detail.BaseStats[9].ToString());
                    item.SubItems.Add(detail.BaseStats[4].ToString());
                    item.SubItems.Add(detail.BaseStats[3].ToString());
                    item.SubItems.Add(detail.BaseStats[11].ToString());



                    int colourCheck = (int)detail.Colors[0];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[0].ToString()); //14
                    ColourMap selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[0]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[14].BackColor = selectedColor.Color;
                        item.SubItems[14].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[1];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[1].ToString()); //15
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[1]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[15].BackColor = selectedColor.Color;
                        item.SubItems[15].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[2];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[2].ToString()); //16
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[2]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[16].BackColor = selectedColor.Color;
                        item.SubItems[16].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[3];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[3].ToString()); //17
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[3]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[17].BackColor = selectedColor.Color;
                        item.SubItems[17].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[4];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[4].ToString()); //18
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[4]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[18].BackColor = selectedColor.Color;
                        item.SubItems[18].ForeColor = selectedColor.Color;
                    }

                    colourCheck = (int)detail.Colors[5];
                    item.SubItems.Add(colourCheck == 0 ? "N/A" : detail.Colors[5].ToString()); //19
                    selectedColor = Program.ProgramConfig.ColourMap.Where(c => c.Id == (int)detail.Colors[5]).FirstOrDefault();
                    if (selectedColor != null && selectedColor.Hex.Length > 0)
                    {
                        item.SubItems[19].BackColor = selectedColor.Color;
                        item.SubItems[19].ForeColor = selectedColor.Color;
                    }

                    item.SubItems.Add(detail.Id.ToString());

                    if (detail.Id == selectedId)
                    {

                        item.Selected = true;
                        selectedX = (decimal)Math.Round(detail.Location.Longitude.Value, 2);
                        selectedY = (decimal)Math.Round(detail.Location.Latitude.Value, 2);
                    }

                    listItems.Add(item);
                });

                lvwWildDetail.Items.AddRange(listItems.ToArray());

                // Create a comparer.
                if (SortingColumn_DetailWild != null)
                {
                    lvwWildDetail.ListViewItemSorter =
                        new ListViewComparer(SortingColumn_DetailWild.Index, SortingColumn_DetailWild.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                    // Sort.
                    lvwWildDetail.Sort();
                }
                else
                {

                    SortingColumn_DetailWild = lvwWildDetail.Columns[3]; ;
                    SortingColumn_DetailWild.Text = "< " + SortingColumn_DetailWild.Text;

                    lvwWildDetail.ListViewItemSorter =
                        new ListViewComparer(3, SortOrder.Descending);

                    // Sort.
                    lvwWildDetail.Sort();

                }

                lvwWildDetail.EndUpdate();

                lblSelectedWildTotal.Text = "Count: " + lvwWildDetail.Items.Count.ToString();

                lblStatus.Text = "Creature data populated.";
                lblStatus.Refresh();



                /*
                Bitmap originalImage = DrawMap(selectedX, selectedY);

                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                */

                if(tabFeatures.SelectedTab.Name == "tpgWild")
                {
                    picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
                    picMap.Refresh();

                }


            }

            this.Cursor = Cursors.Default;

        }

        private void LvwWildDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            btnCopyCommandWild.Enabled = lvwWildDetail.SelectedItems.Count > 0;

            if (lvwWildDetail.SelectedItems.Count > 0)
            {

                var selectedItem = lvwWildDetail.SelectedItems[0];
                decimal.TryParse(selectedItem.SubItems[5].Text, out decimal selectedX);
                decimal.TryParse(selectedItem.SubItems[4].Text, out decimal selectedY);


                Bitmap bitmap = DrawMap(selectedX, selectedY);

                picMap.Image = bitmap;
                picMap.Refresh();


            }
            this.Cursor = Cursors.Default;
        }



        private void RefreshTamedSummary()
        {

            if (gd == null)
            {
                return;
            }

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            lblStatus.Text = "Populating tamed creature summary...";
            lblStatus.Refresh();



            int classIndex = 0;
            string selectedClass = "";
            if (cboTameClass.SelectedItem != null)
            {
                DinoSummary selectedDino = (DinoSummary)cboTameClass.SelectedItem;
                selectedClass = selectedDino.ClassName;
            }


            List<int> playerRestrictions = new List<int>();
            List<string> tribeRestrictions = new List<string>();

            if(ARKViewer.Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp)
            {
                //check for server restritions
                ServerConfiguration currentConfig = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault<ServerConfiguration>();
                if (currentConfig != null)
                {
                    if (currentConfig.RestrictedTribes != null)
                    {
                        tribeRestrictions.AddRange(currentConfig.RestrictedTribes);
                    }

                    if (currentConfig.RestrictedPlayers != null)
                    {
                        playerRestrictions.AddRange(currentConfig.RestrictedPlayers);
                    }
                }
            }


            //MessageBox.Show("Listing tamed creatures.");
            var tamedSummary = gd.TamedCreatures
                                .Where(t => !(t.ClassName == "MotorRaft_BP_C" || t.ClassName == "Raft_BP_C") && t.TargetingTeam!=0)
                                .GroupBy(c => c.ClassName)
                                .Select(g => new { ClassName = g.Key, Name = ARKViewer.Program.ProgramConfig.DinoMap.Count(d=>d.ClassName == g.Key)==0? g.Key : ARKViewer.Program.ProgramConfig.DinoMap.Where(d => d.ClassName == g.Key).FirstOrDefault().FriendlyName, Count = g.Count(), Min = g.Min(l => l.Level), Max = g.Max(l => l.Level) })
                                .OrderBy(o => o.Name);

            cboTameClass.Items.Clear();
            cboTameClass.Items.Add(new DinoSummary() { ClassName = "", Name = "[All Creatures]", Count = tamedSummary.Sum(s => s.Count) });

            foreach (var summary in tamedSummary)
            {
                DinoSummary newSummary = new DinoSummary()
                {
                    ClassName = summary.ClassName,
                    Name = summary.Name,
                    Count = summary.Count,
                    MinLevel = summary.Min,
                    MaxLevel = summary.Max,
                    MaxLength = 100
                };
                int newIndex = cboTameClass.Items.Add(newSummary);
                if(selectedClass == summary.ClassName)
                {
                    classIndex = newIndex;
                }
            }

            lblTameTotal.Text = "Count: 0";

            if (cboTameClass.Items.Count > 0) cboTameClass.SelectedIndex = classIndex;

            lblStatus.Text = "Tamed creatures populated.";
            lblStatus.Refresh();

        }


        private void RefreshCryoSummary()
        {

        }


        private void RefreshWildSummary()
        {

            if (gd == null)
            {
                return;
            }

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;

            lblStatus.Text = "Populating wild creature summary...";
            lblStatus.Refresh();


            int classIndex = 0;
            string selectedClass = "";
            if (cboWildClass.SelectedItem!=null)
            {
                DinoSummary selectedDino = (DinoSummary)cboWildClass.SelectedItem;
                selectedClass = selectedDino.ClassName;
            }


            //wild side
            int minLevel = (int)udWildMin.Value;
            int maxLevel = (int)udWildMax.Value;
            float selectedLat = (float)udWildLat.Value;
            float selectedLon = (float)udWildLon.Value;
            float selectedRad = (float)udWildRadius.Value;
            
            var wildSummary = gd.WildCreatures.Where
                                                (w => 
                                                    ((w.BaseLevel >= minLevel && w.BaseLevel <= maxLevel) || w.BaseLevel == 0)
                                                    && (Math.Abs(w.Location.Latitude.GetValueOrDefault(0) - selectedLat) <= selectedRad)
                                                    && (Math.Abs(w.Location.Longitude.GetValueOrDefault(0) - selectedLon) <= selectedRad)
                                                )
                                                .GroupBy(c => c.ClassName)
                                                .Select(g => new { ClassName = g.Key, Name = ARKViewer.Program.ProgramConfig.DinoMap.Count(d => d.ClassName == g.Key) == 0 ? g.Key : ARKViewer.Program.ProgramConfig.DinoMap.Where(d => d.ClassName == g.Key).FirstOrDefault().FriendlyName, Count = g.Count(), Min = g.Min(l => l.BaseLevel), Max = g.Max(l => l.BaseLevel) })
                                                .OrderBy(o => o.Name);

            
            cboWildClass.Items.Clear();

            //add "All" summary
            int newIndex = 0;

            foreach (var summary in wildSummary.OrderBy(o=>o.Name))
            {

                DinoSummary newSummary = new DinoSummary()
                {
                    ClassName = summary.ClassName,
                    Name = summary.Name,
                    Count = summary.Count,
                    MinLevel = summary.Min,
                    MaxLevel = summary.Max,
                    MaxLength = 100
                };


                newIndex = cboWildClass.Items.Add(newSummary);
                if (selectedClass == summary.ClassName)
                {
                    classIndex = newIndex;
                }
            }


            lblWildTotal.Text = "TOTAL: " + wildSummary.Sum(w => w.Count).ToString(); ;
            lblStatus.Text = "Wild creatures populated.";
            lblStatus.Refresh();


            if (cboWildClass.Items.Count > 0) cboWildClass.SelectedIndex = classIndex;

        }

        private void CboWildClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWildDetail();
        }

        private void LvwDetail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwWildDetail.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_DetailWild == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_DetailWild)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_DetailWild.Text.StartsWith("> "))
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
                SortingColumn_DetailWild.Text = SortingColumn_DetailWild.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_DetailWild = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_DetailWild.Text = "> " + SortingColumn_DetailWild.Text;
            }
            else
            {
                SortingColumn_DetailWild.Text = "< " + SortingColumn_DetailWild.Text;
            }

            // Create a comparer.
            lvwWildDetail.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwWildDetail.Sort();
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings(ARKViewer.Program.ProgramConfig);
            if(settings.ShowDialog() == DialogResult.OK)
            {
                ARKViewer.Program.ProgramConfig = settings.SavedConfig;
                LoadData();
            }

        }
        


        private void UpdateZoomLevel(object sender, EventArgs e)
        {
            var newSize = 1024 * ((double)trackZoom.Value / 100.0);
            picMap.Width = (int)newSize;
            picMap.Height = (int)newSize;

        }

        private void cboSelected_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            DinoSummary dinoSummary = (DinoSummary)cboWildClass.Items[e.Index];
            string dinoName = dinoSummary.Name;
            string dinoCount = $"Count: {dinoSummary.Count}";
            string minLevel = $"Min: {dinoSummary.MinLevel}";
            string maxLevel = $"Max: {dinoSummary.MaxLevel}";

            Rectangle r1 = e.Bounds;
            r1.Width = r1.Width;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(dinoName, e.Font, sb, r1);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom)
            // End Using

            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width -200;
            r2.Width = r2.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(dinoCount, e.Font, sb, r2);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r2.Right, 0, r2.Right, r2.Bottom)
            // End Using

            Rectangle r3 = e.Bounds;
            r3.X = e.Bounds.Width -120;
            r3.Width = r3.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(minLevel, e.Font, sb, r3);
            }

            // Using p As New Pen(Color.AliceBlue)
            // e.Graphics.DrawLine(p, r3.Right, 0, r3.Right, r3.Bottom)
            // End Using

            Rectangle r4 = e.Bounds;
            r4.X = (int)(e.Bounds.Width - 65);
            r4.Width = r4.Width / 4;

            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(maxLevel, e.Font, sb, r4);
            }
        }

        private void RefreshMap()
        {
            if(ARKViewer.Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp)
            {
                ServerConfiguration selectedServer = ARKViewer.Program.ProgramConfig.ServerList.Where(s => s.Name == ARKViewer.Program.ProgramConfig.SelectedServer).FirstOrDefault();
                if (selectedServer == null) return;
                if (selectedServer.Mode == 0)
                {
                    DownloadFtp();

                }
                else
                {
                    DownloadSFtp();
                }
            }

            LoadData();
        }

        private void ZoomIn()
        {
            if (trackZoom.Value + trackZoom.LargeChange < trackZoom.Maximum)
            {
                trackZoom.Value += trackZoom.LargeChange;
            }
            else
            {
                trackZoom.Value = trackZoom.Maximum;
            }
        }

        private void ZoomOut()
        {
            if (trackZoom.Value - trackZoom.LargeChange >= 0)
            {
                trackZoom.Value -= trackZoom.LargeChange;
            }
            else
            {
                trackZoom.Value = 0;
            }
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            frmMarkerEditor markerEditor = new frmMarkerEditor(Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile), ARKViewer.Program.ProgramConfig.MapMarkerList, "");
            if (markerEditor.ShowDialog() == DialogResult.OK)
            {
                ListViewItem newItem = lvwMapMarkers.Items.Add(markerEditor.EditingMarker.Name);
                newItem.ImageKey = $"marker_{markerEditor.EditingMarker.Marker}";
                newItem.SubItems.Add(markerEditor.EditingMarker.Lat.ToString("0.00"));
                newItem.SubItems.Add(markerEditor.EditingMarker.Lon.ToString("0.00"));
                newItem.Tag = markerEditor.EditingMarker;

                ARKViewer.Program.ProgramConfig.MapMarkerList.Add(markerEditor.EditingMarker);
            }
        }


        private void Structure_CheckedChanged(object sender, EventArgs e)
        {


            ARKViewer.Program.ProgramConfig.WaterVeins = chkWaterVeins.Checked;
            ARKViewer.Program.ProgramConfig.WyvernNests = chkWyvernNests.Checked;
            ARKViewer.Program.ProgramConfig.BeaverDams = chkBeaverDams.Checked;
            ARKViewer.Program.ProgramConfig.DeinoNests = chkDeinoNests.Checked;
            ARKViewer.Program.ProgramConfig.DrakeNests = chkDrakeNests.Checked;
            ARKViewer.Program.ProgramConfig.Obelisks = chkObelisks.Checked;
            ARKViewer.Program.ProgramConfig.GasVeins = chkGasVeins.Checked;
            ARKViewer.Program.ProgramConfig.OilVeins = chkOilVeins.Checked;
            ARKViewer.Program.ProgramConfig.Artifacts = chkArtifacts.Checked;
            ARKViewer.Program.ProgramConfig.ChargeNodes = chkChargeNodes.Checked;
            ARKViewer.Program.ProgramConfig.PlantZ = false;
            ARKViewer.Program.ProgramConfig.PlantX = false;
            ARKViewer.Program.ProgramConfig.MagmaNests = chkMagmasaurNests.Checked;
            ARKViewer.Program.ProgramConfig.Glitches = chkGlitches.Checked;

            picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
        }

        private void ShowStructureMarkers(List<StructureMarker> structureList, string windowTitle, Image structureIcon)
        {
            if (structureLocations != null)
            {
                structureLocations.Close();
                structureLocations.Dispose();
            }
            structureLocations = new frmStructureLocations(structureList, windowTitle, structureIcon);
            structureLocations.Top = this.Top;
            structureLocations.Left = this.Left;
            structureLocations.Height = this.Height;


            structureLocations.HighlightStructure += (s, structure) =>
            {
                Bitmap originalImage = DrawMap(lastSelectedX, lastSelectedY);
                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                decimal markerX = 0m;
                decimal markerY = 0m;

                Tuple<int, int, decimal, decimal, decimal, decimal> mapvals = Tuple.Create(1024, 1024, 0.0m, 0.0m, 100.0m, 100.0m);
                
                if (structure != null)
                {
                    markerX = ((decimal)Math.Round(structure.Lon, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(structure.Lat, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.Red), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    graphics.DrawEllipse(new Pen(Color.Green, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                }

                picMap.Image = bitmap;

            };


            structureLocations.Show();

        }

        private void ShowStructureLocations(string className)
        {
            Image structureIcon = new Bitmap(32, 32);
            string structureName = "None";

            if (className == "BeaverDam_C")
            {
                structureName = "Beaver Dams";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_beaver;
            }
            if (className == "DeinonychusNest_C")
            {
                structureName = "Deinonychus Nests";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_deino;
            }
            if(className == "RockDrakeNest_C")
            {
                structureName = "Rock Drake Nests";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_rockdrake;
            }
            if (className.StartsWith("WyvernNest_"))
            {
                structureName = "Wyvern Nests";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_wyvern;
            }
            if (className.StartsWith("OilVein_"))
            {
                structureName = "Oil Veins";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_oil;
            }
            if (className.StartsWith("WaterVein_"))
            {
                structureName = "Water Veins";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_water;
            }
            if (className.StartsWith("GasVein_"))
            {
                structureName = "Gas Veins";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_gas;
            }
            if (className.StartsWith("ArtifactCrate_"))
            {
                structureName = "Artifacts";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_trophy;
            }
            if (className.StartsWith("TributeTerminal_"))
            {
                structureName = "Obelisk / Terminals";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_obelisk;
            }
            if (className.StartsWith("PrimalStructurePowerNode"))
            {
                structureName = "Charge Nodes";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_battery;
            }
            if (className == "Structure_PlantSpeciesZ_Wild_C")
            {
                structureName = "Plant-Z";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_flower;
            }
            if (className == "CherufeNest_C")
            {
                structureName = "Magmasaur Nests";
                structureIcon = ARKViewer.Properties.Resources.structure_marker_magmasaur;
            }


            if (structureLocations != null)
            {
                structureLocations.Close();
                structureLocations.Dispose();
            }
            structureLocations = new frmStructureLocations(gd, className, structureName, structureIcon);
            structureLocations.Top = this.Top;
            structureLocations.Left = this.Left;
            structureLocations.Height = this.Height;


            structureLocations.HighlightStructure += (s, structure) =>
            {
                Bitmap originalImage = DrawMap(lastSelectedX, lastSelectedY);
                Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                decimal markerX = 0m;
                decimal markerY = 0m;

                Tuple<int, int, decimal, decimal, decimal, decimal> mapvals = Tuple.Create(1024, 1024, 0.0m, 0.0m, 100.0m, 100.0m);
                
                if (structure != null)
                {
                    markerX = ((decimal)Math.Round(structure.Lon, 2) - mapvals.Item4) * mapvals.Item1 / (mapvals.Item5 - mapvals.Item4);
                    markerY = ((decimal)Math.Round(structure.Lat, 2) - mapvals.Item3) * mapvals.Item2 / (mapvals.Item6 - mapvals.Item3);

                    graphics.FillEllipse(new SolidBrush(Color.Red), (float)markerX - 15f, (float)markerY - 15f, 30, 30);
                    graphics.DrawEllipse(new Pen(Color.Green, 2), (float)markerX - 15f, (float)markerY - 15f, 30, 30);

                }

                picMap.Image = bitmap;

            };


            structureLocations.Show();
        }

        private void picBeaverDams_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("BeaverDam_C");
        }

        private void picWyvernNests_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("WyvernNest_");

        }

        private void picWaterVeins_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("WaterVein_");
        }

        private void picObelisks_Click(object sender, EventArgs e)
        {
            var terminalList = Program.ProgramConfig.TerminalMarkers.Where(m => m.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower()).ToList();
            Image markerIcon = ARKViewer.Properties.Resources.structure_marker_obelisk;
            ShowStructureMarkers(terminalList, "Obelisks / Terminals", markerIcon);
        }

        private void picDeinoNests_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("DeinonychusNest_C");
        }

        private void picGasVeins_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("GasVein_");
        }

        private void picOilVeins_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("OilVein");
        }

        private void picArtifacts_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("ArtifactCrate_");
        }

        private void frmViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            //only save location if normal window, do not save location/size if minimized/maximized
            if(this.WindowState == FormWindowState.Normal)
            {
                ARKViewer.Program.ProgramConfig.WindowTop = this.Top;
                ARKViewer.Program.ProgramConfig.WindowLeft = this.Left;
                ARKViewer.Program.ProgramConfig.WindowHeight = this.Height;
                ARKViewer.Program.ProgramConfig.WindowWidth = this.Width;
            }

            ARKViewer.Program.ProgramConfig.Zoom = trackZoom.Value;
            ARKViewer.Program.ProgramConfig.SplitterDistance = splitContainer1.SplitterDistance;
            ARKViewer.Program.ProgramConfig.Save();
        }

        private void btnEditMarker_Click(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];
            MapMarker selectedMarker = (MapMarker)selectedItem.Tag;
            
            frmMarkerEditor markerEditor = new frmMarkerEditor(Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile), ARKViewer.Program.ProgramConfig.MapMarkerList, selectedMarker.Name);
            if (markerEditor.ShowDialog() == DialogResult.OK)
            {
                selectedItem.Text = markerEditor.EditingMarker.Name;
                selectedItem.ImageKey = $"marker_{markerEditor.EditingMarker.Marker}";
                selectedItem.SubItems[1].Text = markerEditor.EditingMarker.Lat.ToString("0.00");
                selectedItem.SubItems[2].Text = markerEditor.EditingMarker.Lon.ToString("0.00");
                selectedItem.Tag = markerEditor.EditingMarker;

                if (ARKViewer.Program.ProgramConfig.MapMarkerList.Contains(selectedMarker))
                {
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Remove(selectedMarker);
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Add(markerEditor.EditingMarker);
                }
                
            }
        }

        private void lvwMapMarkers_Click(object sender, EventArgs e)
        {

        }

        private void lvwMapMarkers_SelectedIndexChanged(object sender, EventArgs e)
        {

            picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
            btnEditMarker.Enabled = lvwMapMarkers.SelectedItems.Count > 0;
            btnRemoveMarker.Enabled = lvwMapMarkers.SelectedItems.Count > 0;
        }

        private void btnRemoveMarker_Click(object sender, EventArgs e)
        {
            if (lvwMapMarkers.SelectedItems.Count == 0) return;

            ListViewItem selectedItem = lvwMapMarkers.SelectedItems[0];
            MapMarker selectedMarker = (MapMarker)selectedItem.Tag;
            if(MessageBox.Show($"Are you sure you want to remove your marker for '{selectedMarker.Name}'?", "Remove Marker?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lvwMapMarkers.Items.Remove(selectedItem);
                if (ARKViewer.Program.ProgramConfig.MapMarkerList.Contains(selectedMarker))
                {
                    ARKViewer.Program.ProgramConfig.MapMarkerList.Remove(selectedMarker);
                }
            }

        }

        private void txtMarkerFilter_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void lvwMapMarkers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwMapMarkers.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Markers == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Markers)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Markers.Text.StartsWith("> "))
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
                SortingColumn_Markers.Text = SortingColumn_Markers.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Markers = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Markers.Text = "> " + SortingColumn_Markers.Text;
            }
            else
            {
                SortingColumn_Markers.Text = "< " + SortingColumn_Markers.Text;
            }

            // Create a comparer.
            lvwMapMarkers.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwMapMarkers.Sort();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            RefreshMap();
            btnRefresh.Enabled = true;
        }

        private void btnZoomMinus_Click(object sender, EventArgs e)
        {
            btnZoomMinus.Enabled = false;
            ZoomOut();
            btnZoomMinus.Enabled = true;
        }

        private void cboTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPlayerList();
        }

        private void cboPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayerDetail();
        }

        private void lvwPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void lvwPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwPlayers.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Players == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Players)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Players.Text.StartsWith("> "))
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
                SortingColumn_Players.Text = SortingColumn_Players.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Players = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Players.Text = "> " + SortingColumn_Players.Text;
            }
            else
            {
                SortingColumn_Players.Text = "< " + SortingColumn_Players.Text;
            }

            // Create a comparer.
            lvwPlayers.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwPlayers.Sort();
        }

        private void btnPlayerTames_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Player tame explorer coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (lvwPlayers.SelectedItems.Count == 0) return;

            ArkPlayer selectedPlayer = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;

            var playerTames = gd.TamedCreatures.Where(t => t.OwningPlayerId == selectedPlayer.Id || t.ImprinterPlayerDataId.GetValueOrDefault(-1) == selectedPlayer.Id);



        }

        private void btnPlayerInventory_Click(object sender, EventArgs e)
        {
            if (lvwPlayers.SelectedItems.Count == 0) return;

            
            ArkPlayer selectedPlayer = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;

            frmPlayerInventoryViewer playerViewer = new frmPlayerInventoryViewer(gd, ARKViewer.Program.ProgramConfig.ItemMap, selectedPlayer);
            playerViewer.ShowDialog();

            /*
             * 
            //player personal inventory
            var playerInv = selectedPlayer.Inventory;

            
            //tribe and player owned structures inventory
            List<ArkItem> tribeStructureInv = new List<ArkItem>();
            if (selectedPlayer.Tribe != null)
            {
                foreach(var player in selectedPlayer.Tribe.Members)
                {
                    var playerStructures = gd.Structures.Where(s => s.Inventory != null && s.OwningPlayerId == player.Id);
                    if(playerStructures!=null && playerStructures.Count() > 0)
                    {
                        foreach(var structureInv in playerStructures)
                        {
                            tribeStructureInv.AddRange(structureInv.Inventory);
                        }
                    }
                }
            }
            else
            {
                //going solo
                if(selectedPlayer.Inventory!=null && selectedPlayer.Inventory.Count() > 0)
                {
                    var playerStructures = gd.Structures.Where(s => s.Inventory != null && s.OwningPlayerId == selectedPlayer.Id);
                    if (playerStructures != null && playerStructures.Count() > 0)
                    {
                        foreach (var structureInv in playerStructures)
                        {
                            tribeStructureInv.AddRange(structureInv.Inventory);
                        }
                    }
                }
            }

            //tribe and player owned tame inventories
            List<ArkItem> creatureStrucutreInv = new List<ArkItem>();
            if (selectedPlayer.Tribe != null)
            {
                foreach (var player in selectedPlayer.Tribe.Members)
                {
                    var playerTames = gd.TamedCreatures.Where(s => s.OwningPlayerId == player.Id);
                    if (playerTames != null && playerTames.Count() > 0)
                    {
                        foreach (var tameInv in playerTames)
                        {
                            creatureStrucutreInv.AddRange(tameInv.Inventory);
                        }
                    }
                }
            }
            else
            {
                //going solo
                if (selectedPlayer.Inventory != null && selectedPlayer.Inventory.Count() > 0)
                {
                    var playerTames = gd.TamedCreatures.Where(s => s.OwningPlayerId == selectedPlayer.Id);
                    if (playerTames != null && playerTames.Count() > 0)
                    {
                        foreach (var tameInv in playerTames)
                        {
                            creatureStrucutreInv.AddRange(tameInv.Inventory);
                        }
                    }
                }
            }

            */

            //MessageBox.Show("Player tame explorer coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDinoAncestors_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dino ancestor explorer coming soon.", "Coming Soon!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDinoInventory_Click(object sender, EventArgs e)
        {
            if (lvwTameDetail.SelectedItems.Count == 0) return;


            ArkTamedCreature selectedCreature = (ArkTamedCreature)lvwTameDetail.SelectedItems[0].Tag;

            frmDinoInventoryViewer inventoryViewer = new frmDinoInventoryViewer(gd, selectedCreature);
            inventoryViewer.ShowDialog();
        }

        private void picChargeNodes_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("PrimalStructurePowerNode");

        }

        private void picPlantZ_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("Structure_PlantSpeciesZ_Wild_C");
        }

        private void chkApplyFilterMarkers_CheckedChanged(object sender, EventArgs e)
        {
            txtMarkerFilter.Enabled = !chkApplyFilterMarkers.Checked;
            if (!chkApplyFilterMarkers.Checked)
            {
                txtMarkerFilter.Text = string.Empty;
                txtMarkerFilter.Focus();
            }

            RefreshMapMarkers();
        }

        private void picDrakeNests_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("RockDrakeNest_C");
        }


        private void btnPlayerTribeLog_Click(object sender, EventArgs e)
        {

            if (lvwPlayers.SelectedItems.Count == 0) return;

            ArkPlayer selectedPlayer = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;
            frmTribeLog logViewer = new frmTribeLog(selectedPlayer);
            logViewer.ShowDialog();
        }

        private void cboStructureTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStructurePlayerList();
        }

        private void cboStructurePlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStructureSummary();
        }

        private void RefreshStructureSummary()
        {
            if (gd == null) return;
            if (cboStructureTribe.SelectedItem == null) return;
            if (cboStructurePlayer.SelectedItem == null) return;

            lastSelectedX = 0.0m;
            lastSelectedY = 0.0m;


            string selectedClass = "NONE";
            if(cboStructureStructure.SelectedItem != null)
            {
                ComboValuePair selectedValue = (ComboValuePair)cboStructureStructure.SelectedItem;
                selectedClass = selectedValue.Key;
            }

            cboStructureStructure.Items.Clear();
            cboStructureStructure.Items.Add(new ComboValuePair() { Key = "NONE", Value = "[None]" });
            cboStructureStructure.Items.Add(new ComboValuePair() { Key = "", Value = "[All Structures]" });

            //tribe
            ComboValuePair comboValue = (ComboValuePair)cboStructureTribe.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            //player
            comboValue = (ComboValuePair)cboStructurePlayer.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);


            var playerStructureTypes = gd.Structures.Where(s =>
                                                            (s.OwningPlayerId == selectedPlayerId || selectedPlayerId == 0)
                                                            &&
                                                            (s.TargetingTeam.GetValueOrDefault(0) == selectedTribeId || selectedTribeId == 0)
                                                            && (s.TargetingTeam!=null)
                                                            &&
                                                            (
                                                                Program.ProgramConfig.StructureExclusions == null || Program.ProgramConfig.StructureExclusions != null & !Program.ProgramConfig.StructureExclusions.Contains(s.ClassName)
                                                            )
                                                       ).GroupBy(g=>g.ClassName)
                                                       .Select(s=>s.Key);

            List<ComboValuePair> newItems = new List<ComboValuePair>();


            if (playerStructureTypes != null && playerStructureTypes.Count() > 0)
            {

                foreach(var className in playerStructureTypes)
                {
                    var structureName = className;
                    var itemMap = Program.ProgramConfig.StructureMap.Where(i => i.ClassName == className).FirstOrDefault();

                    ComboValuePair classNameItem = new ComboValuePair(className, "");

                    if(itemMap!=null && itemMap.FriendlyName.Length > 0)
                    {
                        structureName = itemMap.FriendlyName;
                        classNameItem.Value = structureName;

                    }


                    if (structureName == null || structureName.Length == 0) structureName = className;

                    newItems.Add(new ComboValuePair() { Key = className, Value = structureName });
                }


            }

            var rafts = gd.TamedCreatures
                                        .Where(w => (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                                    && (selectedTribeId == 0 || w.TargetingTeam == selectedTribeId)
                                                    && (selectedPlayerId == 0 || (w.OwningPlayerId.GetValueOrDefault(0) == selectedPlayerId || w.ImprinterPlayerDataId.GetValueOrDefault(0) == selectedPlayerId))
                                               )
                                                .GroupBy(g => g.ClassName)
                                                .Select(s => s.Key); 

            if(rafts !=null && rafts.Count() > 0)
            {
                foreach(var raftClass in rafts)
                {
                    string raftName = raftClass;
                    DinoClassMap classMap = Program.ProgramConfig.DinoMap.Where(d => d.ClassName == raftClass).FirstOrDefault();
                    if(classMap!=null && classMap.FriendlyName != null)
                    {
                        raftName = classMap.FriendlyName;
                    }


                    newItems.Add(new ComboValuePair() { Key = raftClass, Value = raftName});
                }
            }

            int selectedIndex = 1;
            if(newItems.Count > 0)
            {
                cboStructureStructure.BeginUpdate();
                foreach(var newItem in newItems.OrderBy(o => o.Value))
                {
                    int newIndex = cboStructureStructure.Items.Add(newItem);
                    if(newItem.Key == selectedClass)
                    {
                        selectedIndex = newIndex;
                    }
                }
                cboStructureStructure.EndUpdate();
                
            }

            if(tabFeatures.SelectedTab.Name == "tpgStructures")
            {
                cboStructureStructure.SelectedIndex = selectedIndex;
            }
            else
            {
                cboStructureStructure.SelectedIndex = 0;
            }
            

        }

        private void RefreshTribeSummary()
        {
            if (gd == null) return;

            lvwTribes.Items.Clear();
            if(allTribes!=null && allTribes.Count() > 0)
            {
                //tribe id, tribe name, players, tames, structures, last active
                foreach(var tribe in allTribes)
                {
                    ListViewItem newItem = lvwTribes.Items.Add(tribe.TribeId.ToString());
                    newItem.Tag = tribe;
                    newItem.SubItems.Add(tribe.TribeName);
                    newItem.SubItems.Add(tribe.PlayerCount.ToString());
                    newItem.SubItems.Add(tribe.TameCount.ToString());
                    newItem.SubItems.Add(tribe.StructureCount.ToString());
                    newItem.SubItems.Add(tribe.LastActive.Equals(new DateTime())?"":tribe.LastActive.ToString());
                }
            }
        }

        private void LoadPlayerStructureDetail()
        {

            if (gd == null) return;
            if (cboStructureTribe.SelectedItem == null) return;
            if (cboStructurePlayer.SelectedItem == null) return;

            this.Cursor = Cursors.WaitCursor;

            btnCopyCommandStructure.Enabled = false;
            lblStatus.Text = "Updating player structure selection.";
            lblStatus.Refresh();

            //tribe
            ComboValuePair comboValue = (ComboValuePair)cboStructureTribe.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedTribeId);

            //player
            comboValue = (ComboValuePair)cboStructurePlayer.SelectedItem;
            int.TryParse(comboValue.Key, out int selectedPlayerId);

            if (selectedPlayerId != 0)
            {
                PlayerMap playerMap = allPlayers.Where(p => p.PlayerId == selectedPlayerId).FirstOrDefault();
                if (playerMap != null)
                {
                    selectedTribeId = (int)playerMap.TribeId;
                }
            }


            //structure
            comboValue = (ComboValuePair)cboStructureStructure.SelectedItem;
            var selectedClass = comboValue.Key;


            var playerStructures = gd.Structures.Where(s => 
                                                            (
                                                                (selectedClass.Length ==0 || s.ClassName == selectedClass)
                                                                
                                                            ) 
                                                            && 
                                                            (
                                                                (s.OwningPlayerId !=null && s.OwningPlayerId == selectedPlayerId)                                                             
                                                                ||
                                                                (s.TargetingTeam != null && s.TargetingTeam == selectedTribeId || selectedTribeId == 0 && s.TargetingTeam != null)
                                                            )
                                                            && 
                                                            (
                                                                !Program.ProgramConfig.StructureExclusions.Contains(s.ClassName)
                                                                
                                                            )
                                                       );
            
            lblStructureTotal.Text = $"Count: {playerStructures.Count()}";
            lblStructureTotal.Refresh();
            
            lvwStructureLocations.Items.Clear();
            lvwStructureLocations.Refresh();
            lvwStructureLocations.BeginUpdate();

            ConcurrentBag<ListViewItem> listItems = new ConcurrentBag<ListViewItem>();

            Parallel.ForEach(playerStructures, playerStructure =>
            {

                if (playerStructure.Location != null && playerStructure.Location.Latitude.HasValue)
                {
                    var playerName = playerStructure.OwningPlayerName;
                    var tribeName = playerStructure.OwnerName;

                    if (playerStructure.OwningPlayerId != null)
                    {
                        ArkPlayer player = gd.Players.Where(p => p.Id == playerStructure.OwningPlayerId).FirstOrDefault();
                        if (player != null)
                        {
                            playerName = player.CharacterName != null ? player.CharacterName : player.Name;
                            if (player.Tribe != null && player.Tribe.Name != null && player.Tribe.Name.Length > 0)
                            {
                                tribeName = player.Tribe.Name;
                            }
                        }
                    }
                    else
                    {
                        if (playerStructure.TargetingTeam != null)
                        {
                            ArkTribe tribe = gd.Tribes.Where(t => t.Id == playerStructure.TargetingTeam.Value).FirstOrDefault();
                            if (tribe != null)
                            {
                                tribeName = tribe.Name;

                                ArkPlayer player = null;
                                if (selectedPlayerId > 0)
                                {
                                    player = gd.Players.Where(p => p.Id == selectedPlayerId).FirstOrDefault();
                                }
                                else
                                {
                                    player = gd.Players.Where(p => p.Id == tribe.OwnerPlayerId).FirstOrDefault();
                                }

                                if (player != null)
                                {
                                    playerName = player.CharacterName != null ? player.CharacterName : player.Name;
                                }
                            }
                        }
                    }


                    var itemName = playerStructure.ClassName;
                    var itemMap = ARKViewer.Program.ProgramConfig.StructureMap.Where(i => i.ClassName == playerStructure.ClassName).FirstOrDefault();
                    if (itemMap != null && itemMap.FriendlyName.Length > 0)
                    {
                        itemName = itemMap.FriendlyName;
                    }

                    ListViewItem newItem = new ListViewItem(playerName);
                    newItem.SubItems.Add(tribeName);
                    newItem.SubItems.Add(itemName);


                    newItem.SubItems.Add(playerStructure.Location.Latitude.Value.ToString("0.00"));
                    newItem.SubItems.Add(playerStructure.Location.Longitude.Value.ToString("0.00"));

                    newItem.SubItems.Add(playerStructure.OwningPlayerName);
                    newItem.SubItems.Add(playerStructure.OwnerName);

                    newItem.Tag = playerStructure;

                    listItems.Add(newItem);
                }


            });

            if(gd.TamedCreatures!=null && gd.TamedCreatures.Count() > 0)
            {
                var raftList = gd.TamedCreatures
                                    .Where(w =>
                                                (w.ClassName == "MotorRaft_BP_C" || w.ClassName == "Raft_BP_C")
                                                &&
                                                (
                                                    !Program.ProgramConfig.StructureExclusions.Contains(w.ClassName)

                                                )
                                                && (w.ClassName == selectedClass || selectedClass.Length == 0)
                                                && (selectedTribeId == 0 || w.TargetingTeam == selectedTribeId)
                                                && (selectedPlayerId == 0 || (w.OwningPlayerId.GetValueOrDefault(0) == selectedPlayerId || w.ImprinterPlayerDataId.GetValueOrDefault(0) == selectedPlayerId))
                                           ).OrderBy(c => c.Name);

                Parallel.ForEach(raftList, raft =>
                {

                    var playerName = raft.OwningPlayerName;
                    var tribeName = raft.TribeName;

                    if (raft.OwningPlayerId != null)
                    {
                        ArkPlayer player = gd.Players.Where(p => p.Id == raft.OwningPlayerId).FirstOrDefault();
                        if (player != null)
                        {
                            playerName = player.CharacterName != null ? player.CharacterName : player.Name;
                            if (player.Tribe != null && player.Tribe.Name != null && player.Tribe.Name.Length > 0)
                            {
                                tribeName = player.Tribe.Name;
                            }
                        }
                    }
                    else
                    {
                        ArkTribe tribe = gd.Tribes.Where(t => t.Id == raft.TargetingTeam).FirstOrDefault();
                        if (tribe != null)
                        {
                            tribeName = tribe.Name;

                            ArkPlayer player = null;
                            if (selectedPlayerId > 0)
                            {
                                player = gd.Players.Where(p => p.Id == selectedPlayerId).FirstOrDefault();
                            }
                            else
                            {
                                player = gd.Players.Where(p => p.Id == tribe.OwnerPlayerId).FirstOrDefault();
                            }

                            if (player != null)
                            {
                                playerName = player.CharacterName != null ? player.CharacterName : player.Name;
                            }
                        }
                    }

                    var itemName = raft.ClassName;
                    var itemMap = ARKViewer.Program.ProgramConfig.DinoMap.Where(i => i.ClassName == raft.ClassName).FirstOrDefault();
                    if (itemMap != null && itemMap.FriendlyName.Length > 0)
                    {
                        itemName = itemMap.FriendlyName;
                    }
                    ListViewItem newItem = new ListViewItem(playerName);
                    newItem.SubItems.Add(tribeName);
                    newItem.SubItems.Add(itemName);
                    newItem.SubItems.Add(raft.Location.Latitude.Value.ToString("0.00"));
                    newItem.SubItems.Add(raft.Location.Longitude.Value.ToString("0.00"));
                    newItem.Tag = raft;

                    listItems.Add(newItem);
                });

            }

            lvwStructureLocations.Items.AddRange(listItems.ToArray());

            if (SortingColumn_Structures != null)
            {
                lvwStructureLocations.ListViewItemSorter =
                    new ListViewComparer(SortingColumn_Structures.Index, SortingColumn_Structures.Text.Contains(">") ? SortOrder.Ascending : SortOrder.Descending);

                // Sort.
                lvwStructureLocations.Sort();
            }

            lvwStructureLocations.EndUpdate();
            lblStatus.Text = "Player structure selection updated.";
            lblStatus.Refresh();

            picMap.Image = DrawMap(lastSelectedX, lastSelectedY);

            this.Cursor = Cursors.Default;
        }

        private void lvwStructureLocations_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwStructureLocations.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Structures == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Structures)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Structures.Text.StartsWith("> "))
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
                SortingColumn_Structures.Text = SortingColumn_Structures.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Structures = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Structures.Text = "> " + SortingColumn_Structures.Text;
            }
            else
            {
                SortingColumn_Structures.Text = "< " + SortingColumn_Structures.Text;
            }

            // Create a comparer.
            lvwStructureLocations.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwStructureLocations.Sort();
        }

        private void tabFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            tabFeatures.Refresh();
            this.Cursor = Cursors.WaitCursor;
            picMap.Image = DrawMap(0, 0);
            this.Cursor = Cursors.Default;
        }

        private void lvwStructureLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            btnCopyCommandStructure.Enabled = lvwStructureLocations.SelectedItems.Count > 0;
            btnStructureInventory.Enabled = false;

            if(lvwStructureLocations.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];
                if(selectedItem.Tag is ArkStructure)
                {
                    ArkStructure selectedStructure = (ArkStructure)selectedItem.Tag;
                    btnStructureInventory.Enabled = selectedStructure.Inventory != null && selectedStructure.Inventory.Length > 0;
                }
            }

            picMap.Image = DrawMap(lastSelectedY, lastSelectedY);
        }

        private void cboStructureStructure_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadPlayerStructureDetail();
        }

        private void btnZoomPlus_Click(object sender, EventArgs e)
        {
            btnZoomPlus.Enabled = false;
            ZoomIn();
            btnZoomPlus.Enabled = true;
        }

        private void btnStructureExclusionFilter_Click(object sender, EventArgs e)
        {
            if (gd == null) return;
            if(gd.Structures == null)
            {
                MessageBox.Show("Structure exclusions can only be set when a map with structures has been loaded.", "No Structures", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmStructureExclusionFilter exclusionEditor = new frmStructureExclusionFilter(gd);
            if(exclusionEditor.ShowDialog() == DialogResult.OK)
            {
                RefreshStructureSummary();
            }
        
        }

        private void cboConsoleCommandsPlayerTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandPlayer.Enabled = cboConsoleCommandsPlayerTribe.SelectedIndex >= 0 && lvwPlayers.SelectedItems.Count > 0;
        }

        private void btnCopyCommandPlayer_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsPlayerTribe.SelectedItem == null) return;

            var commandText = cboConsoleCommandsPlayerTribe.SelectedItem.ToString();
            string commandList = "";
            

            if (commandText.Contains("<FileCsvList>"))
            {
                string fileList = "";
                commandList = commandText;

                foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                {
                    TribeMap selectedTribe = (TribeMap)selectedItem.Tag;
                    if (fileList.Length > 0)
                    {
                        fileList = fileList + " ";
                    }
                    fileList = fileList + selectedTribe.TribeId.ToString() + ".arktribe";
                }

                commandList = commandList.Replace("<FileCsvList>", fileList);
                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandList = $"admincheat {commandList}";

                        break;
                    case 2:
                        commandList = $"cheat {commandList}";
                        break;
                }

            }
            else
            {

                foreach(ListViewItem selectedItem in lvwPlayers.SelectedItems)
                {
                    ArkPlayer selectedPlayer = (ArkPlayer)selectedItem.Tag;

                    int selectedPlayerId = selectedPlayer.Id;
                    int selectedTribeId = selectedPlayer.TribeId.GetValueOrDefault(selectedPlayer.Id);
                    string selectedSteamId = selectedPlayer.SteamId;

                    commandText = cboConsoleCommandsPlayerTribe.SelectedItem.ToString();

                    commandText = commandText.Replace("<PlayerID>", selectedPlayerId.ToString("f0"));
                    commandText = commandText.Replace("<TribeID>", selectedTribeId.ToString("f0"));
                    commandText = commandText.Replace("<SteamID>", selectedSteamId);
                    commandText = commandText.Replace("<PlayerName>", selectedPlayer.Name);
                    commandText = commandText.Replace("<CharacterName>", selectedPlayer.CharacterName);
                    if (selectedPlayer.Tribe != null)
                    {
                        commandText = commandText.Replace("<TribeName>", selectedPlayer.Tribe.Name);
                    }

                    if (selectedPlayer.Location != null)
                    {
                        commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedPlayer.Location.X:0.00}"));
                        commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedPlayer.Location.Y:0.00}"));
                        commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedPlayer.Location.Z + 250:0.00}"));
                    }


                    switch (Program.ProgramConfig.CommandPrefix)
                    {
                        case 1:
                            commandText = $"admincheat {commandText}";

                            break;
                        case 2:
                            commandText = $"cheat {commandText}";
                            break;
                    }

                    commandText = commandText.Trim();

                    if (commandList.Length > 0)
                    {
                        commandList += $"|{commandText}";
                    }
                    else
                    {
                        commandList = commandText;
                    }
                }
                


            }

            Clipboard.SetText(commandList);

            lblStatus.Text = $"Command copied to the clipboard:\n\n{commandText}";
            lblStatus.Refresh();
        }

        private void btnCopyCommandStructure_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsStructure.SelectedItem == null) return;
            if (lvwStructureLocations.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwStructureLocations.SelectedItems[0];
            
            var commandText = cboConsoleCommandsStructure.SelectedItem.ToString();
            if (commandText != null)
            {
                if(selectedItem.Tag is ArkStructure)
                {
                    ArkStructure selectedStructure = (ArkStructure)selectedItem.Tag;

                    int selectedTribeId = selectedStructure.TargetingTeam.HasValue ? selectedStructure.TargetingTeam.Value : selectedStructure.OwningPlayerId.Value;

                    commandText = commandText.Replace("<TribeID>", selectedTribeId.ToString("f0"));
                    if (selectedStructure.Location != null)
                    {
                        commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedStructure.Location.X:0.00}"));
                        commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedStructure.Location.Y:0.00}"));
                        commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedStructure.Location.Z + 250:0.00}"));

                    }
                }else if(selectedItem.Tag is ArkTamedCreature)
                {
                    ArkTamedCreature selectedRaft = (ArkTamedCreature)selectedItem.Tag;

                    int selectedTribeId = selectedRaft.TargetingTeam;

                    commandText = commandText.Replace("<TribeID>", selectedTribeId.ToString());
                    if (selectedRaft.Location != null)
                    {
                        commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedRaft.Location.X:0.00}"));
                        commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedRaft.Location.Y:0.00}"));
                        commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedRaft.Location.Z + 250:0.00}"));

                    }
                }


                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);
                MessageBox.Show($"Command copied to the clipboard:\n\n{commandText}", "Command Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cboConsoleCommandsStructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandStructure.Enabled = cboConsoleCommandsStructure.SelectedIndex >= 0 && lvwStructureLocations.SelectedItems.Count > 0;
        }

        private void cboTameTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTamePlayerList();
        }

        private void cboTamePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void cboTameClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void lvwTameDetail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwTameDetail.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_DetailTame == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_DetailTame)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_DetailTame.Text.StartsWith("> "))
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
                SortingColumn_DetailTame.Text = SortingColumn_DetailTame.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_DetailTame = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_DetailTame.Text = "> " + SortingColumn_DetailTame.Text;
            }
            else
            {
                SortingColumn_DetailTame.Text = "< " + SortingColumn_DetailTame.Text;
            }

            // Create a comparer.
            lvwTameDetail.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwTameDetail.Sort();
        }

        private void optStatsBase_CheckedChanged(object sender, EventArgs e)
        {
            LoadTameDetail();
        }

        private void lvwTameDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            btnCopyCommandTamed.Enabled = lvwTameDetail.SelectedItems.Count > 0;

            if (lvwTameDetail.SelectedItems.Count > 0)
            {

                var selectedItem = lvwTameDetail.SelectedItems[0];
                decimal.TryParse(selectedItem.SubItems[6].Text, out decimal selectedX);
                decimal.TryParse(selectedItem.SubItems[5].Text, out decimal selectedY);


                Bitmap bitmap = DrawMap(selectedX, selectedY);

                picMap.Image = bitmap;
                picMap.Refresh();


            }
            this.Cursor = Cursors.Default;
        }

        private void picMap_MouseClick(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;           

            PictureBox clickedPic = (PictureBox)sender;

            double zoomLevel = (double)clickedPic.Height / (double)clickedPic.Image.Height;


            double clickY = e.Location.Y / (zoomLevel);
            double clickX = e.Location.X / (zoomLevel);
            
            double latitude = clickY / 10.25;
            double longtitude = clickX / 10.25;

            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    if (lvwWildDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach(ListViewItem item in lvwWildDetail.Items)
                        {
                            if(item.SubItems[4].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[4].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[5].Text);

                                double latDistance = Math.Abs(itemLat - latitude);
                                double lonDistance = Math.Abs(itemLon- longtitude);

                                
                                if(latDistance <= 0.5 && lonDistance <= 0.5){
                                    lvwWildDetail.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }


                    break;
                case "tpgTamed":

                    if (lvwTameDetail.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwTameDetail.Items)
                        {
                            if (item.SubItems[5].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[5].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[6].Text);

                                double latDistance = itemLat - latitude;
                                double lonDistance = itemLon - longtitude;


                                if ((latDistance >=0 && latDistance <= 0.5) && (lonDistance >=0 && lonDistance <= 0.5))
                                {
                                    lvwTameDetail.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }

                    break;
                case "tpgStructures":
                    if (lvwStructureLocations.Items.Count > 0)
                    {
                        //get nearest 
                        foreach (ListViewItem item in lvwStructureLocations.Items)
                        {
                            if (item.SubItems[3].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[3].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[4].Text);

                                double latDistance = itemLat - latitude;
                                double lonDistance = itemLon - longtitude;


                                if ((latDistance >= 0 && latDistance <= 0.5) && (lonDistance >= 0 && lonDistance <= 0.5))
                                {
                                    lvwStructureLocations.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }



                        }


                    }


                    break;
                case "tpgPlayers":
                    if(lvwPlayers.Items.Count > 0)
                    {

                        //get nearest 
                        foreach (ListViewItem item in lvwPlayers.Items)
                        {
                            if (item.SubItems[4].Text != "n/a")
                            {
                                double itemLat = Convert.ToDouble(item.SubItems[4].Text);
                                double itemLon = Convert.ToDouble(item.SubItems[5].Text);

                                double latDistance = Math.Abs(itemLat - latitude);
                                double lonDistance = Math.Abs(itemLon - longtitude);

                                if (latDistance <= 0.75 && lonDistance <= 0.75)
                                {
                                    lvwPlayers.SelectedItems.Clear();
                                    item.Selected = true;
                                    item.EnsureVisible();
                                    break;
                                }
                            }

                        }
                    }

                    break;

                default:
                    break;
            }


            this.Cursor = Cursors.Default;
        }

        private void picMap_Click(object sender, EventArgs e)
        {

        }

        private void cboCryoTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCryoPlayerList();
        }
        

        private void btnCopyCommandWild_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsWild.SelectedItem == null) return;
            if (lvwWildDetail.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwWildDetail.SelectedItems[0];
            ArkWildCreature selectedCreature = (ArkWildCreature)selectedItem.Tag;
            var commandText = cboConsoleCommandsWild.SelectedItem.ToString();
            if (commandText != null)
            {

                commandText = commandText.Replace("<ClassName>", selectedCreature.ClassName);
                commandText = commandText.Replace("<Level>", selectedCreature.BaseLevel.ToString("f0"));
               
                if (selectedCreature.Location != null)
                {


                    commandText = commandText.Replace("<x>",System.FormattableString.Invariant($"{selectedCreature.Location.X:0.00}"));
                    commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedCreature.Location.Y:0.00}"));
                    commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedCreature.Location.Z + 250:0.00}"));
                }

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied to the clipboard:\n\n{commandText}";
                lblStatus.Refresh();

            }
        }

        private void btnCopyCommandTamed_Click(object sender, EventArgs e)
        {
            if (cboConsoleCommandsTamed.SelectedItem == null) return;
            if (lvwTameDetail.SelectedItems.Count <= 0) return;

            ListViewItem selectedItem = lvwTameDetail.SelectedItems[0];

            var commandText = cboConsoleCommandsTamed.SelectedItem.ToString();
            if (commandText != null)
            {

                ArkTamedCreature selectedCreature = (ArkTamedCreature)selectedItem.Tag;
                commandText = commandText.Replace("<ClassName>", selectedCreature.ClassName);
                commandText = commandText.Replace("<Level>", (selectedCreature.BaseLevel / 1.5).ToString("f0"));
                commandText = commandText.Replace("<TribeID>", selectedCreature.TargetingTeam.ToString("f0"));

                if (selectedCreature.Location != null)
                {
                    commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{selectedCreature.Location.X:0.00}"));
                    commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{selectedCreature.Location.Y:0.00}"));
                    commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{selectedCreature.Location.Z + 250:0.00}"));
                }

                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = commandText.Replace("<DoTame>", "admincheat DoTame");
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = commandText.Replace("<DoTame>", "cheat DoTame");
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied to the clipboard:\n\n{commandText}";
                lblStatus.Refresh();

            }
        }

        
        private void lvwPlayers_Click(object sender, EventArgs e)
        {
           
        }

        private void lvwPlayers_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = true;
                mnuContext_SteamId.Visible = true;
                mnuContext_TribeId.Visible = false;
            }
            else
            {
                if (isLoading) return;

                picMap.Image = DrawMap(lastSelectedX, lastSelectedY);
                btnPlayerInventory.Enabled = lvwPlayers.SelectedItems.Count == 1;
                btnPlayerTribeLog.Enabled = lvwPlayers.SelectedItems.Count == 1;
                btnCopyCommandPlayer.Enabled = lvwPlayers.SelectedItems.Count > 0 && cboConsoleCommandsPlayerTribe.SelectedIndex >= 0;
                btnDeletePlayer.Enabled = lvwPlayers.SelectedItems.Count == 1;
            }
        }

        private void lvwStructureLocations_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }
        }

        private void mnuContext_PlayerId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":

                    break;

                case "tpgStructures":

                    break;
                case "tpgPlayers":
                    if(lvwPlayers.SelectedItems.Count > 0)
                    {
                        ArkPlayer player = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;
                        Clipboard.SetText(player.Id.ToString("f0"));
                        MessageBox.Show("Player ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
            }
        }

        private void mnuContext_SteamId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":

                    break;

                case "tpgStructures":

                    break;
                case "tpgPlayers":
                    if (lvwPlayers.SelectedItems.Count > 0)
                    {
                        ArkPlayer player = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;
                        Clipboard.SetText(player.SteamId.ToString());
                        MessageBox.Show("Steam ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
            }
        }

        private void mnuContext_TribeId_Click(object sender, EventArgs e)
        {
            switch (tabFeatures.SelectedTab.Name)
            {
                case "tpgWild":

                    break;
                case "tpgTamed":
                    if(lvwTameDetail.SelectedItems.Count > 0)
                    {
                        ArkTamedCreature creature = (ArkTamedCreature)lvwTameDetail.SelectedItems[0].Tag;
                        Clipboard.SetText(creature.TargetingTeam.ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;

                case "tpgStructures":
                    if (lvwStructureLocations.SelectedItems.Count > 0)
                    {
                        ArkStructure structure = (ArkStructure)lvwStructureLocations.SelectedItems[0].Tag;
                        Clipboard.SetText(structure.TargetingTeam.GetValueOrDefault(0).ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "tpgTribes":
                    if(lvwTribes.SelectedItems.Count > 0)
                    {
                        TribeMap selectedTribe = (TribeMap)lvwTribes.SelectedItems[0].Tag;

                        Clipboard.SetText(selectedTribe.TribeId.ToString("f0"));
                        MessageBox.Show("Tribe ID copied to the clipboard!", "Copy Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    break;
                case "tpgPlayers":

                    break;
            }
        }

        private void chkCryo_CheckedChanged(object sender, EventArgs e)
        {
            chkCryo.BackgroundImage = chkCryo.Checked ? ARKViewer.Properties.Resources.button_cryoon : ARKViewer.Properties.Resources.button_cryooff;
            LoadTameDetail();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //Program.ProgramConfig.SplitterDistance = splitContainer1.SplitterDistance;
        }

        private void cboDroppedTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDroppedPlayers();
        }

        private void cboDroppedPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDroppedItems();
        }

        private void cboDroppedItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDroppedItemDetail();
        }

        private void lvwDroppedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopyCommandDropped.Enabled = lvwDroppedItems.SelectedItems.Count > 0;

            if(lvwDroppedItems.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvwDroppedItems.SelectedItems[0];

                decimal selectedX = 0;
                decimal selectedY = 0;

                switch (selectedItem.Tag)
                {
                    case ArkDroppedItem droppedItem:
                        selectedX = (decimal)droppedItem.Location.Longitude.GetValueOrDefault(0);
                        selectedY = (decimal)droppedItem.Location.Latitude.GetValueOrDefault(0);

                        btnDropInventory.Enabled = false;

                        break;
                    case ArkDeathCache deathCache:
                        selectedX = (decimal)deathCache.Location.Longitude.GetValueOrDefault(0);
                        selectedY = (decimal)deathCache.Location.Latitude.GetValueOrDefault(0);

                        btnDropInventory.Enabled = deathCache.Inventory != null && deathCache.Inventory.Length > 0;
                        break;
                    default:

                        break;
                }

                picMap.Image = DrawMap(selectedX, selectedY);
            }
            

        }

        private void btnCopyCommandDropped_Click(object sender, EventArgs e)
        {
            if (cboCopyCommandDropped.SelectedItem == null) return;

            var commandText = cboCopyCommandDropped.SelectedItem.ToString();
            if (commandText != null)
            {

                ListViewItem selectedItem = lvwDroppedItems.SelectedItems[0];
                switch (selectedItem.Tag)
                {
                    case ArkDroppedItem droppedItem:
                        if (droppedItem.Location != null)
                        {
                            commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{droppedItem.Location.X:0.00}"));
                            commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{droppedItem.Location.Y:0.00}"));
                            commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{droppedItem.Location.Z + 100:0.00}"));
                        }
                        break;
                    case ArkDeathCache deathCache:
                        if (deathCache.Location != null)
                        {
                            commandText = commandText.Replace("<x>", System.FormattableString.Invariant($"{deathCache.Location.X:0.00}"));
                            commandText = commandText.Replace("<y>", System.FormattableString.Invariant($"{deathCache.Location.Y:0.00}"));
                            commandText = commandText.Replace("<z>", System.FormattableString.Invariant($"{deathCache.Location.Z + 100:0.00}"));
                        }
                        break;
                    default:
                        break;
                }


                switch (Program.ProgramConfig.CommandPrefix)
                {
                    case 1:
                        commandText = $"admincheat {commandText}";

                        break;
                    case 2:
                        commandText = $"cheat {commandText}";
                        break;
                }

                Clipboard.SetText(commandText);

                lblStatus.Text = $"Command copied to the clipboard:\n\n{commandText}";
                lblStatus.Refresh();

            }
        }

        private void udWildMax_ValueChanged(object sender, EventArgs e)
        {
            if (udWildMin.Value > udWildMax.Value) udWildMin.Value = udWildMax.Value;
            udWildMin.Maximum = udWildMax.Value;
            RefreshWildSummary();
        }

        private void udWildMin_ValueChanged(object sender, EventArgs e)
        {
            if (udWildMax.Value < udWildMin.Value) udWildMax.Value = udWildMin.Value;
            udWildMax.Minimum = udWildMin.Value;
            RefreshWildSummary();

        }

        private void lvwTribes_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void btnTribeLog_Click(object sender, EventArgs e)
        {
            if (gd == null) return;
            if (lvwTribes.SelectedItems.Count == 0) return;
            TribeMap selectedTribe = (TribeMap)lvwTribes.SelectedItems[0].Tag;
            ArkTribe tribe = gd.Tribes.First(t => t.Id == selectedTribe.TribeId);
            if (tribe != null)
            {
                frmTribeLog logViewer = new frmTribeLog(tribe);
                logViewer.ShowDialog();

            }
        }

        private void lvwTribes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Get the new sorting column.
            ColumnHeader new_sorting_column = lvwTribes.Columns[e.Column];

            // Figure out the new sorting order.
            System.Windows.Forms.SortOrder sort_order;
            if (SortingColumn_Tribes == null)
            {
                // New column. Sort ascending.
                sort_order = SortOrder.Ascending;
            }
            else
            {
                // See if this is the same column.
                if (new_sorting_column == SortingColumn_Tribes)
                {
                    // Same column. Switch the sort order.
                    if (SortingColumn_Tribes.Text.StartsWith("> "))
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
                SortingColumn_Tribes.Text = SortingColumn_Tribes.Text.Substring(2);
            }

            // Display the new sort order.
            SortingColumn_Tribes = new_sorting_column;
            if (sort_order == SortOrder.Ascending)
            {
                SortingColumn_Tribes.Text = "> " + SortingColumn_Tribes.Text;
            }
            else
            {
                SortingColumn_Tribes.Text = "< " + SortingColumn_Tribes.Text;
            }

            // Create a comparer.
            lvwTribes.ListViewItemSorter =
                new ListViewComparer(e.Column, sort_order);

            // Sort.
            lvwTribes.Sort();
        }

        private void chkTribePlayers_CheckedChanged(object sender, EventArgs e)
        {
            if (gd == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                picMap.Image = DrawMap(0, 0);
            }
        }

        private void chkTribeTames_CheckedChanged(object sender, EventArgs e)
        {
            if (gd == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                picMap.Image = DrawMap(0, 0);
            }
        }

        private void chkTribeStructures_CheckedChanged(object sender, EventArgs e)
        {
            if (gd == null) return;

            if (tabFeatures.SelectedTab.Name == "tpgTribes")
            {

                picMap.Image = DrawMap(0, 0);
            }
        }

        private void btnTribeCopyCommand_Click(object sender, EventArgs e)
        {
            if (cboTribeCopyCommand.SelectedItem == null) return;
            if (lvwTribes.SelectedItems.Count == 0) return;
            
            string commandList = "";
            var commandText = cboTribeCopyCommand.SelectedItem.ToString();

            if (commandText != null)
            {

                if (commandText.Contains("<FileCsvList>"))
                {
                    commandList = commandText;
                    string fileList = "";

                    foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                    {
                        TribeMap selectedTribe = (TribeMap)selectedItem.Tag;
                        if(fileList.Length > 0)
                        {
                            fileList = fileList + " ";
                        }
                        fileList = fileList + selectedTribe.TribeId.ToString() + ".arktribe";
                    }

                    commandList = commandList.Replace("<FileCsvList>", fileList);

                    switch (Program.ProgramConfig.CommandPrefix)
                    {
                        case 1:
                            commandList = $"admincheat {commandList}";

                            break;
                        case 2:
                            commandList = $"cheat {commandList}";
                            break;
                    }

                }
                else
                {
                    foreach (ListViewItem selectedItem in lvwTribes.SelectedItems)
                    {
                        TribeMap selectedTribe = (TribeMap)selectedItem.Tag;

                        commandText = cboTribeCopyCommand.SelectedItem.ToString();
                        commandText = commandText.Replace("<TribeID>", selectedTribe.TribeId.ToString("f0"));
                        commandText = commandText.Replace("<TribeName>", selectedTribe.TribeName);
                        switch (Program.ProgramConfig.CommandPrefix)
                        {
                            case 1:
                                commandText = $"admincheat {commandText}";

                                break;
                            case 2:
                                commandText = $"cheat {commandText}";
                                break;
                        }

                        commandText = commandText.Trim();

                        if (commandList.Length > 0)
                        {
                            commandList += $"|{commandText}";
                        }
                        else
                        {
                            commandList = commandText;
                        }

                    }

                }

                Clipboard.SetText(commandList);
                
                lblStatus.Text = $"Command copied to the clipboard:\n\n{commandText}";
                lblStatus.Refresh();
            }
        }

        private void lvwTribes_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }else
            {
                if (gd == null) return;

                btnTribeLog.Enabled = false;
                btnTribeCopyCommand.Enabled = lvwTribes.SelectedItems.Count > 0;

                if (tabFeatures.SelectedTab.Name == "tpgTribes")
                {

                    picMap.Image = DrawMap(0, 0);
                }

                if (lvwTribes.SelectedItems.Count != 1) return;

                TribeMap selectedTribe = (TribeMap)lvwTribes.SelectedItems[0].Tag;
                btnTribeLog.Enabled = selectedTribe.ContainsLog;


            }
        }

        private void picMagmasaurNests_Click(object sender, EventArgs e)
        {
            ShowStructureLocations("CherufeNest_C");
        }

        private void udWildLat_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void udWildLon_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void udWildRadius_ValueChanged(object sender, EventArgs e)
        {
            RefreshWildSummary();
        }

        private void pnlMap_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            int movementY = e.Y - mapMouseDownY;
            int movementX = e.X - mapMouseDownX;


            if (e.Button == MouseButtons.Left)
            {
                picMap.Left = picMap.Left + movementX;
                picMap.Top = picMap.Top + movementY;
            }
            else if(e.Button == MouseButtons.Right)
            {
                if(movementY > 0 )
                {
                    if ((mapMouseDownZoom + movementY) <= trackZoom.Maximum)
                    {
                        trackZoom.Value = mapMouseDownZoom + movementY;
                    }
                }
                else if(movementY < 0 )
                {
                    if ((mapMouseDownZoom + movementY) >= trackZoom.Minimum)
                    {
                        trackZoom.Value = mapMouseDownZoom + movementY;
                    }
                }
            }
        }

        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            mapMouseDownX = e.X;
            mapMouseDownY = e.Y;
            mapMouseDownZoom = trackZoom.Value;
        }

        private void picGlitches_Click(object sender, EventArgs e)
        {
            var terminalList = Program.ProgramConfig.GlitchMarkers.Where(m => m.Map.ToLower() == Path.GetFileName(ARKViewer.Program.ProgramConfig.SelectedFile).ToLower()).ToList();
            Image markerIcon = ARKViewer.Properties.Resources.structure_marker_glitch;
            ShowStructureMarkers(terminalList, "Glitches", markerIcon);

        }

        private void btnStructureInventory_Click(object sender, EventArgs e)
        {
            if (lvwStructureLocations.SelectedItems.Count == 0) return;


            ArkStructure selectedStructure = (ArkStructure)lvwStructureLocations.SelectedItems[0].Tag;
            
            frmStructureInventoryViewer inventoryViewer = new frmStructureInventoryViewer(selectedStructure);
            inventoryViewer.ShowDialog();
        }

        private void mnuContext_ExportData_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JavaScript Object Notation|*.json|Comma Seperated Values|*.csv";
            saveDialog.Title = "Export Data";
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFilename = saveDialog.FileName;

                switch (tabFeatures.SelectedTab.Name)
                {
                    case "tpgWild":

                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if(lvwWildDetail.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach(ListViewItem item in lvwWildDetail.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach(ColumnHeader header in lvwWildDetail.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }

                                            
                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;
                            
                            case 2:
                                //CSV
                                if (lvwWildDetail.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex =0; colIndex < lvwWildDetail.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwWildDetail.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if(colIndex < lvwWildDetail.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwWildDetail.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwWildDetail.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }
                                            
                                            if (colIndex < lvwWildDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpgTamed":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwWildDetail.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwWildDetail.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwWildDetail.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTameDetail.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwTameDetail.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwTameDetail.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwTameDetail.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwTameDetail.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwTameDetail.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;

                    case "tpgStructures":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwStructureLocations.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwStructureLocations.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwStructureLocations.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwStructureLocations.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwStructureLocations.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpgTribes":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwStructureLocations.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwStructureLocations.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwStructureLocations.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTribes.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwTribes.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwTribes.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwTribes.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwTribes.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwTribes.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;

                    case "tpgPlayers":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwPlayers.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwPlayers.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwPlayers.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    long.TryParse(item.SubItems[header.Index].Text, out long intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwTribes.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwPlayers.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwPlayers.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwPlayers.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwPlayers.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwPlayers.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                    case "tpdDroppedItems":
                        switch (saveDialog.FilterIndex)
                        {
                            case 1:
                                if (lvwDroppedItems.Items.Count > 0)
                                {
                                    JArray jsonItems = new JArray();
                                    foreach (ListViewItem item in lvwDroppedItems.Items)
                                    {
                                        //row > columns 
                                        JArray jsonFields = new JArray();
                                        foreach (ColumnHeader header in lvwDroppedItems.Columns)
                                        {

                                            string headerText = header.Text;
                                            headerText = headerText.Replace("< ", "");
                                            headerText = headerText.Replace("> ", "");


                                            JObject jsonField = new JObject();
                                            if (item.SubItems[header.Index].Text.IsNumeric())
                                            {
                                                if (item.SubItems[header.Index].Text.Contains("."))
                                                {
                                                    decimal.TryParse(item.SubItems[header.Index].Text, out decimal decValue);

                                                    jsonField.Add(new JProperty(headerText, decValue));
                                                }
                                                else
                                                {
                                                    int.TryParse(item.SubItems[header.Index].Text, out int intValue);

                                                    jsonField.Add(new JProperty(headerText, intValue));
                                                }
                                            }
                                            else
                                            {
                                                jsonField.Add(new JProperty(headerText, item.SubItems[header.Index].Text));
                                            }


                                            jsonFields.Add(jsonField);
                                        }
                                        jsonItems.Add(jsonFields);

                                    }
                                    File.WriteAllText(saveDialog.FileName, jsonItems.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                //JSON
                                break;

                            case 2:
                                //CSV
                                if (lvwDroppedItems.Items.Count > 0)
                                {
                                    StringBuilder csvBuilder = new StringBuilder();
                                    for (int colIndex = 0; colIndex < lvwDroppedItems.Columns.Count; colIndex++)
                                    {

                                        ColumnHeader header = lvwDroppedItems.Columns[colIndex];
                                        string headerText = header.Text;
                                        headerText = headerText.Replace("< ", "");
                                        headerText = headerText.Replace("> ", "");

                                        csvBuilder.Append("\"" + headerText + "\"");
                                        if (colIndex < lvwDroppedItems.Columns.Count - 1)
                                        {
                                            csvBuilder.Append(",");
                                        }

                                    }
                                    csvBuilder.Append("\n");

                                    foreach (ListViewItem item in lvwDroppedItems.Items)
                                    {
                                        //rows
                                        for (int colIndex = 0; colIndex < lvwDroppedItems.Columns.Count; colIndex++)
                                        {

                                            if (item.SubItems[colIndex].Text.IsNumeric())
                                            {
                                                csvBuilder.Append(item.SubItems[colIndex].Text);
                                            }
                                            else
                                            {
                                                csvBuilder.Append("\"" + item.SubItems[colIndex].Text + "\"");
                                            }

                                            if (colIndex < lvwTameDetail.Columns.Count - 1)
                                            {
                                                csvBuilder.Append(",");
                                            }

                                        }

                                        csvBuilder.Append("\n");
                                    }
                                    File.WriteAllText(saveDialog.FileName, csvBuilder.ToString());

                                    MessageBox.Show("Export complete.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                break;
                        }

                        break;
                }

            }


        }

      
        private void lvwWildDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = false;
            }
        }

        private void lvwTameDetail_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mnuContext_PlayerId.Visible = false;
                mnuContext_SteamId.Visible = false;
                mnuContext_TribeId.Visible = true;
            }
        }

        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            if (lvwPlayers.SelectedItems.Count == 0) return;

            ArkPlayer selectedPlayer = (ArkPlayer)lvwPlayers.SelectedItems[0].Tag;

            bool shouldRemove = true;

            if(!selectedPlayer.LastActiveTime.Equals(new DateTime()))
            {
                if ((DateTime.Today - selectedPlayer.LastActiveTime).TotalDays <= 14)
                {
                    if (MessageBox.Show("The selected player has been active in the last 14 days.\n\nAre you sure you want to remove them?", "Remove Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        shouldRemove = false;
                    }
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to remove the selected player?", "Remove Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        shouldRemove = false;
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to remove the selected player?", "Remove Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    shouldRemove = false;
                }
            }


            //remove local
            if (shouldRemove)
            {
                string profilePathLocal = Path.GetDirectoryName(Program.ProgramConfig.SelectedFile);
                
                if(Program.ProgramConfig.Mode == ViewerModes.Mode_Ftp)
                {
                    profilePathLocal = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ARKViewer.Program.ProgramConfig.SelectedServer);

                    //also remove from server if it still exists
                    DeletePlayerFtp(selectedPlayer);
                }

                string profileFileName = Directory.GetFiles(profilePathLocal, $"{selectedPlayer.SteamId}.arkprofile").FirstOrDefault();
                if (profileFileName != null)
                {
                    try
                    {
                        File.Delete(profileFileName);
                        if (MessageBox.Show("Player profile data removed.\n\nPress OK to reload save data.", "Player Removed", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            LoadData();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Failed to remove player data.", "Removal Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }

        }

        private void btnDropInventory_Click(object sender, EventArgs e)
        {
            if (lvwDroppedItems.SelectedItems.Count == 0) return;
            switch (lvwDroppedItems.SelectedItems[0].Tag)
            {
                case ArkDeathCache deathCache:
                    ArkTribe tribe = gd.Tribes.FirstOrDefault(t => t.Id == deathCache.TargetingTeam);
                    string tribeName = tribe != null ? tribe.Name : "";

                    frmDeathCacheViewer inventoryView = new frmDeathCacheViewer(deathCache.OwnerName, tribeName, deathCache.Inventory);
                    inventoryView.ShowDialog();


                    break;
                default:

                    break;
            }
        }
    }
}
