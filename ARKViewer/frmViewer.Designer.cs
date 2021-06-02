namespace ARKViewer
{
    partial class frmViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewer));
            this.lvwWildDetail = new System.Windows.Forms.ListView();
            this.lvwWildDetail_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Base = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_HP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Craft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Colour6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwWildDetail_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContext_PlayerId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_SteamId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_TribeId = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWildTotal = new System.Windows.Forms.Label();
            this.lblMapDate = new System.Windows.Forms.Label();
            this.cboWildClass = new System.Windows.Forms.ComboBox();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.trackZoom = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnZoomPlus = new System.Windows.Forms.Button();
            this.btnZoomMinus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvwMapMarkers = new System.Windows.Forms.ListView();
            this.lvwMapMarkers_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwMapMarkers_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwMapMarkers_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMapMarkerHeader = new System.Windows.Forms.Label();
            this.btnRemoveMarker = new System.Windows.Forms.Button();
            this.btnAddMarker = new System.Windows.Forms.Button();
            this.btnEditMarker = new System.Windows.Forms.Button();
            this.picArtifacts = new System.Windows.Forms.PictureBox();
            this.chkArtifacts = new System.Windows.Forms.CheckBox();
            this.picWyvernNests = new System.Windows.Forms.PictureBox();
            this.chkWyvernNests = new System.Windows.Forms.CheckBox();
            this.picObelisks = new System.Windows.Forms.PictureBox();
            this.chkObelisks = new System.Windows.Forms.CheckBox();
            this.picWaterVeins = new System.Windows.Forms.PictureBox();
            this.chkWaterVeins = new System.Windows.Forms.CheckBox();
            this.picOilVeins = new System.Windows.Forms.PictureBox();
            this.chkOilVeins = new System.Windows.Forms.CheckBox();
            this.picGasVeins = new System.Windows.Forms.PictureBox();
            this.chkGasVeins = new System.Windows.Forms.CheckBox();
            this.picDeinoNests = new System.Windows.Forms.PictureBox();
            this.chkDeinoNests = new System.Windows.Forms.CheckBox();
            this.picBeaverDams = new System.Windows.Forms.PictureBox();
            this.chkBeaverDams = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnPlayerInventory = new System.Windows.Forms.Button();
            this.picChargeNodes = new System.Windows.Forms.PictureBox();
            this.picDrakeNests = new System.Windows.Forms.PictureBox();
            this.btnPlayerTribeLog = new System.Windows.Forms.Button();
            this.btnStructureExclusionFilter = new System.Windows.Forms.Button();
            this.btnCopyCommandPlayer = new System.Windows.Forms.Button();
            this.btnCopyCommandStructure = new System.Windows.Forms.Button();
            this.btnDinoAncestors = new System.Windows.Forms.Button();
            this.btnDinoInventory = new System.Windows.Forms.Button();
            this.btnCopyCommandWild = new System.Windows.Forms.Button();
            this.btnCopyCommandTamed = new System.Windows.Forms.Button();
            this.chkCryo = new System.Windows.Forms.CheckBox();
            this.btnCopyCommandDropped = new System.Windows.Forms.Button();
            this.btnTribeCopyCommand = new System.Windows.Forms.Button();
            this.btnTribeLog = new System.Windows.Forms.Button();
            this.picMagmasaurNests = new System.Windows.Forms.PictureBox();
            this.picGlitches = new System.Windows.Forms.PictureBox();
            this.btnStructureInventory = new System.Windows.Forms.Button();
            this.btnDeletePlayer = new System.Windows.Forms.Button();
            this.btnDropInventory = new System.Windows.Forms.Button();
            this.txtMarkerFilter = new System.Windows.Forms.TextBox();
            this.tabFeatures = new System.Windows.Forms.TabControl();
            this.tpgWild = new System.Windows.Forms.TabPage();
            this.lblWildRadius = new System.Windows.Forms.Label();
            this.udWildRadius = new System.Windows.Forms.NumericUpDown();
            this.lblWildLon = new System.Windows.Forms.Label();
            this.udWildLon = new System.Windows.Forms.NumericUpDown();
            this.lblWildLat = new System.Windows.Forms.Label();
            this.udWildLat = new System.Windows.Forms.NumericUpDown();
            this.lblWildMin = new System.Windows.Forms.Label();
            this.lblWildMax = new System.Windows.Forms.Label();
            this.udWildMin = new System.Windows.Forms.NumericUpDown();
            this.udWildMax = new System.Windows.Forms.NumericUpDown();
            this.lblWildCommand = new System.Windows.Forms.Label();
            this.cboConsoleCommandsWild = new System.Windows.Forms.ComboBox();
            this.lblSelectedWildTotal = new System.Windows.Forms.Label();
            this.lblWildClass = new System.Windows.Forms.Label();
            this.tpgTamed = new System.Windows.Forms.TabPage();
            this.lblTamedCommand = new System.Windows.Forms.Label();
            this.cboConsoleCommandsTamed = new System.Windows.Forms.ComboBox();
            this.cboTameTribes = new System.Windows.Forms.ComboBox();
            this.cboTamePlayers = new System.Windows.Forms.ComboBox();
            this.lblTameCreature = new System.Windows.Forms.Label();
            this.lblTamePlayer = new System.Windows.Forms.Label();
            this.lblTameTribe = new System.Windows.Forms.Label();
            this.lvwTameDetail = new System.Windows.Forms.ListView();
            this.lvwTameDetail_Creature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Base = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_HP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Craft = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Server = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Tamer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Imprinter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Imprint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Cryo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Colour6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_MutationsFemale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_MutationsMale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwTameDetail_Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTameTotal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.optStatsTamed = new System.Windows.Forms.RadioButton();
            this.optStatsBase = new System.Windows.Forms.RadioButton();
            this.cboTameClass = new System.Windows.Forms.ComboBox();
            this.tpgStructures = new System.Windows.Forms.TabPage();
            this.lblStructureTotal = new System.Windows.Forms.Label();
            this.lblCommandStructure = new System.Windows.Forms.Label();
            this.cboConsoleCommandsStructure = new System.Windows.Forms.ComboBox();
            this.lblStructureStructure = new System.Windows.Forms.Label();
            this.cboStructureStructure = new System.Windows.Forms.ComboBox();
            this.lblStructurePlayer = new System.Windows.Forms.Label();
            this.lblStructureTribe = new System.Windows.Forms.Label();
            this.cboStructureTribe = new System.Windows.Forms.ComboBox();
            this.cboStructurePlayer = new System.Windows.Forms.ComboBox();
            this.lvwStructureLocations = new System.Windows.Forms.ListView();
            this.lvwStructureLocations_Player = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Structure = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwStructureLocations_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgTribes = new System.Windows.Forms.TabPage();
            this.chkTribeStructures = new System.Windows.Forms.CheckBox();
            this.chkTribeTames = new System.Windows.Forms.CheckBox();
            this.chkTribePlayers = new System.Windows.Forms.CheckBox();
            this.lblTribeCopyCommand = new System.Windows.Forms.Label();
            this.cboTribeCopyCommand = new System.Windows.Forms.ComboBox();
            this.lvwTribes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgPlayers = new System.Windows.Forms.TabPage();
            this.lblPlayerTotal = new System.Windows.Forms.Label();
            this.lblCommandPlayer = new System.Windows.Forms.Label();
            this.cboConsoleCommandsPlayerTribe = new System.Windows.Forms.ComboBox();
            this.lblPlayersPlayer = new System.Windows.Forms.Label();
            this.lblPlayersTribe = new System.Windows.Forms.Label();
            this.cboTribes = new System.Windows.Forms.ComboBox();
            this.cboPlayers = new System.Windows.Forms.ComboBox();
            this.lvwPlayers = new System.Windows.Forms.ListView();
            this.lvwPlayers_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Sex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Hp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Stam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Melee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Weight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Speed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Food = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Water = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Oxygen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Crafting = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_Fortitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_LastOnline = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_SteamName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwPlayers_SteamId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgDroppedItems = new System.Windows.Forms.TabPage();
            this.cboDroppedItem = new System.Windows.Forms.ComboBox();
            this.lblDroppedPlayer = new System.Windows.Forms.Label();
            this.cboDroppedPlayer = new System.Windows.Forms.ComboBox();
            this.lblCopyCommandDropped = new System.Windows.Forms.Label();
            this.cboCopyCommandDropped = new System.Windows.Forms.ComboBox();
            this.lblCountDropped = new System.Windows.Forms.Label();
            this.lblDroppedItem = new System.Windows.Forms.Label();
            this.lvwDroppedItems = new System.Windows.Forms.ListView();
            this.lvwDroppedItems_Item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_DroppedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Lat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Lon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Tribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDroppedItems_Player = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlStructures = new System.Windows.Forms.Panel();
            this.chkGlitches = new System.Windows.Forms.CheckBox();
            this.chkMagmasaurNests = new System.Windows.Forms.CheckBox();
            this.chkDrakeNests = new System.Windows.Forms.CheckBox();
            this.chkChargeNodes = new System.Windows.Forms.CheckBox();
            this.lblStructureMarkers = new System.Windows.Forms.Label();
            this.chkApplyFilterMarkers = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mnuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.pnlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picArtifacts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWyvernNests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObelisks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterVeins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOilVeins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGasVeins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeinoNests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBeaverDams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChargeNodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDrakeNests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMagmasaurNests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGlitches)).BeginInit();
            this.tabFeatures.SuspendLayout();
            this.tpgWild.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWildRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMax)).BeginInit();
            this.tpgTamed.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tpgStructures.SuspendLayout();
            this.tpgTribes.SuspendLayout();
            this.tpgPlayers.SuspendLayout();
            this.tpgDroppedItems.SuspendLayout();
            this.pnlStructures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwWildDetail
            // 
            this.lvwWildDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwWildDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwWildDetail_Name,
            this.lvwWildDetail_Sex,
            this.lvwWildDetail_Base,
            this.lvwWildDetail_Level,
            this.lvwWildDetail_Lat,
            this.lvwWildDetail_Lon,
            this.lvwWildDetail_HP,
            this.lvwWildDetail_Stam,
            this.lvwWildDetail_Melee,
            this.lvwWildDetail_Weight,
            this.lvwWildDetail_Speed,
            this.lvwWildDetail_Food,
            this.lvwWildDetail_Oxygen,
            this.lvwWildDetail_Craft,
            this.lvwWildDetail_Colour1,
            this.lvwWildDetail_Colour2,
            this.lvwWildDetail_Colour3,
            this.lvwWildDetail_Colour4,
            this.lvwWildDetail_Colour5,
            this.lvwWildDetail_Colour6,
            this.lvwWildDetail_Id});
            this.lvwWildDetail.ContextMenuStrip = this.mnuContext;
            this.lvwWildDetail.FullRowSelect = true;
            this.lvwWildDetail.HideSelection = false;
            this.lvwWildDetail.Location = new System.Drawing.Point(11, 77);
            this.lvwWildDetail.MultiSelect = false;
            this.lvwWildDetail.Name = "lvwWildDetail";
            this.lvwWildDetail.Size = new System.Drawing.Size(773, 115);
            this.lvwWildDetail.TabIndex = 11;
            this.lvwWildDetail.UseCompatibleStateImageBehavior = false;
            this.lvwWildDetail.View = System.Windows.Forms.View.Details;
            this.lvwWildDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwWildDetail_ColumnClick);
            this.lvwWildDetail.SelectedIndexChanged += new System.EventHandler(this.LvwWildDetail_SelectedIndexChanged);
            this.lvwWildDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwWildDetail_MouseClick);
            // 
            // lvwWildDetail_Name
            // 
            this.lvwWildDetail_Name.Text = "Creature";
            this.lvwWildDetail_Name.Width = 142;
            // 
            // lvwWildDetail_Sex
            // 
            this.lvwWildDetail_Sex.Text = "Sex";
            this.lvwWildDetail_Sex.Width = 52;
            // 
            // lvwWildDetail_Base
            // 
            this.lvwWildDetail_Base.Text = "Base";
            this.lvwWildDetail_Base.Width = 0;
            // 
            // lvwWildDetail_Level
            // 
            this.lvwWildDetail_Level.Text = "Lvl";
            this.lvwWildDetail_Level.Width = 40;
            // 
            // lvwWildDetail_Lat
            // 
            this.lvwWildDetail_Lat.Text = "Lat";
            this.lvwWildDetail_Lat.Width = 51;
            // 
            // lvwWildDetail_Lon
            // 
            this.lvwWildDetail_Lon.Text = "Lon";
            this.lvwWildDetail_Lon.Width = 40;
            // 
            // lvwWildDetail_HP
            // 
            this.lvwWildDetail_HP.Text = "HP";
            this.lvwWildDetail_HP.Width = 45;
            // 
            // lvwWildDetail_Stam
            // 
            this.lvwWildDetail_Stam.Text = "Stam";
            this.lvwWildDetail_Stam.Width = 45;
            // 
            // lvwWildDetail_Melee
            // 
            this.lvwWildDetail_Melee.Text = "Melee";
            this.lvwWildDetail_Melee.Width = 48;
            // 
            // lvwWildDetail_Weight
            // 
            this.lvwWildDetail_Weight.Text = "Weight";
            this.lvwWildDetail_Weight.Width = 55;
            // 
            // lvwWildDetail_Speed
            // 
            this.lvwWildDetail_Speed.Text = "Speed";
            this.lvwWildDetail_Speed.Width = 50;
            // 
            // lvwWildDetail_Food
            // 
            this.lvwWildDetail_Food.Text = "Food";
            this.lvwWildDetail_Food.Width = 47;
            // 
            // lvwWildDetail_Oxygen
            // 
            this.lvwWildDetail_Oxygen.Text = "Oxygen";
            this.lvwWildDetail_Oxygen.Width = 53;
            // 
            // lvwWildDetail_Craft
            // 
            this.lvwWildDetail_Craft.Text = "Craft";
            this.lvwWildDetail_Craft.Width = 50;
            // 
            // lvwWildDetail_Colour1
            // 
            this.lvwWildDetail_Colour1.Text = "C0";
            this.lvwWildDetail_Colour1.Width = 35;
            // 
            // lvwWildDetail_Colour2
            // 
            this.lvwWildDetail_Colour2.Text = "C1";
            this.lvwWildDetail_Colour2.Width = 35;
            // 
            // lvwWildDetail_Colour3
            // 
            this.lvwWildDetail_Colour3.Text = "C2";
            this.lvwWildDetail_Colour3.Width = 35;
            // 
            // lvwWildDetail_Colour4
            // 
            this.lvwWildDetail_Colour4.Text = "C3";
            this.lvwWildDetail_Colour4.Width = 35;
            // 
            // lvwWildDetail_Colour5
            // 
            this.lvwWildDetail_Colour5.Text = "C4";
            this.lvwWildDetail_Colour5.Width = 35;
            // 
            // lvwWildDetail_Colour6
            // 
            this.lvwWildDetail_Colour6.Text = "C5";
            this.lvwWildDetail_Colour6.Width = 35;
            // 
            // lvwWildDetail_Id
            // 
            this.lvwWildDetail_Id.Text = "Id";
            this.lvwWildDetail_Id.Width = 0;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContext_PlayerId,
            this.mnuContext_SteamId,
            this.mnuContext_TribeId,
            this.mnuContext_Export});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(153, 92);
            // 
            // mnuContext_PlayerId
            // 
            this.mnuContext_PlayerId.Name = "mnuContext_PlayerId";
            this.mnuContext_PlayerId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_PlayerId.Text = "Copy Player ID";
            this.mnuContext_PlayerId.Click += new System.EventHandler(this.mnuContext_PlayerId_Click);
            // 
            // mnuContext_SteamId
            // 
            this.mnuContext_SteamId.Name = "mnuContext_SteamId";
            this.mnuContext_SteamId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_SteamId.Text = "Copy Steam ID";
            this.mnuContext_SteamId.Click += new System.EventHandler(this.mnuContext_SteamId_Click);
            // 
            // mnuContext_TribeId
            // 
            this.mnuContext_TribeId.Name = "mnuContext_TribeId";
            this.mnuContext_TribeId.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_TribeId.Text = "Copy Tribe ID";
            this.mnuContext_TribeId.Click += new System.EventHandler(this.mnuContext_TribeId_Click);
            // 
            // mnuContext_Export
            // 
            this.mnuContext_Export.Name = "mnuContext_Export";
            this.mnuContext_Export.Size = new System.Drawing.Size(152, 22);
            this.mnuContext_Export.Text = "Export Data";
            this.mnuContext_Export.Click += new System.EventHandler(this.mnuContext_ExportData_Click);
            // 
            // lblWildTotal
            // 
            this.lblWildTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWildTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblWildTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildTotal.Location = new System.Drawing.Point(655, 197);
            this.lblWildTotal.Name = "lblWildTotal";
            this.lblWildTotal.Size = new System.Drawing.Size(130, 30);
            this.lblWildTotal.TabIndex = 9;
            this.lblWildTotal.Text = "Total: 0";
            this.lblWildTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMapDate
            // 
            this.lblMapDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMapDate.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblMapDate.Location = new System.Drawing.Point(592, 557);
            this.lblMapDate.Name = "lblMapDate";
            this.lblMapDate.Size = new System.Drawing.Size(226, 21);
            this.lblMapDate.TabIndex = 1;
            this.lblMapDate.Text = "n/a";
            this.lblMapDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboWildClass
            // 
            this.cboWildClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboWildClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboWildClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWildClass.FormattingEnabled = true;
            this.cboWildClass.Location = new System.Drawing.Point(82, 45);
            this.cboWildClass.Name = "cboWildClass";
            this.cboWildClass.Size = new System.Drawing.Size(701, 21);
            this.cboWildClass.TabIndex = 10;
            this.cboWildClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboSelected_DrawItem);
            this.cboWildClass.SelectedIndexChanged += new System.EventHandler(this.CboWildClass_SelectedIndexChanged);
            // 
            // picMap
            // 
            this.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMap.Location = new System.Drawing.Point(4, 5);
            this.picMap.Margin = new System.Windows.Forms.Padding(0);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(100, 100);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            this.picMap.Click += new System.EventHandler(this.picMap_Click);
            this.picMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            // 
            // pnlMap
            // 
            this.pnlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMap.AutoScroll = true;
            this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMap.Controls.Add(this.picMap);
            this.pnlMap.Location = new System.Drawing.Point(3, 34);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(542, 512);
            this.pnlMap.TabIndex = 14;
            this.pnlMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMap_MouseMove);
            // 
            // trackZoom
            // 
            this.trackZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackZoom.AutoSize = false;
            this.trackZoom.LargeChange = 10;
            this.trackZoom.Location = new System.Drawing.Point(34, 4);
            this.trackZoom.Maximum = 200;
            this.trackZoom.Minimum = 25;
            this.trackZoom.Name = "trackZoom";
            this.trackZoom.Size = new System.Drawing.Size(479, 23);
            this.trackZoom.TabIndex = 3;
            this.trackZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackZoom.Value = 50;
            this.trackZoom.Scroll += new System.EventHandler(this.UpdateZoomLevel);
            this.trackZoom.ValueChanged += new System.EventHandler(this.UpdateZoomLevel);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnZoomPlus);
            this.panel2.Controls.Add(this.btnZoomMinus);
            this.panel2.Controls.Add(this.trackZoom);
            this.panel2.Location = new System.Drawing.Point(3, 549);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 31);
            this.panel2.TabIndex = 15;
            // 
            // btnZoomPlus
            // 
            this.btnZoomPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomPlus.Image")));
            this.btnZoomPlus.Location = new System.Drawing.Point(514, 2);
            this.btnZoomPlus.Name = "btnZoomPlus";
            this.btnZoomPlus.Size = new System.Drawing.Size(33, 27);
            this.btnZoomPlus.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnZoomPlus, "Zoom In");
            this.btnZoomPlus.UseVisualStyleBackColor = true;
            this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
            // 
            // btnZoomMinus
            // 
            this.btnZoomMinus.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomMinus.Image")));
            this.btnZoomMinus.Location = new System.Drawing.Point(0, 2);
            this.btnZoomMinus.Name = "btnZoomMinus";
            this.btnZoomMinus.Size = new System.Drawing.Size(33, 27);
            this.btnZoomMinus.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnZoomMinus, "Zoom Out");
            this.btnZoomMinus.UseVisualStyleBackColor = true;
            this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(1346, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(33, 27);
            this.btnRefresh.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnRefresh, "Load latest save");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lvwMapMarkers
            // 
            this.lvwMapMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMapMarkers.CheckBoxes = true;
            this.lvwMapMarkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwMapMarkers_Name,
            this.lvwMapMarkers_Lat,
            this.lvwMapMarkers_Lon});
            this.lvwMapMarkers.FullRowSelect = true;
            this.lvwMapMarkers.HideSelection = false;
            this.lvwMapMarkers.Location = new System.Drawing.Point(455, 34);
            this.lvwMapMarkers.Name = "lvwMapMarkers";
            this.lvwMapMarkers.Size = new System.Drawing.Size(359, 221);
            this.lvwMapMarkers.TabIndex = 2;
            this.lvwMapMarkers.UseCompatibleStateImageBehavior = false;
            this.lvwMapMarkers.View = System.Windows.Forms.View.Details;
            this.lvwMapMarkers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMapMarkers_ColumnClick);
            this.lvwMapMarkers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwMapMarkers_ItemChecked);
            this.lvwMapMarkers.SelectedIndexChanged += new System.EventHandler(this.lvwMapMarkers_SelectedIndexChanged);
            this.lvwMapMarkers.Click += new System.EventHandler(this.lvwMapMarkers_Click);
            // 
            // lvwMapMarkers_Name
            // 
            this.lvwMapMarkers_Name.Text = "Name";
            this.lvwMapMarkers_Name.Width = 214;
            // 
            // lvwMapMarkers_Lat
            // 
            this.lvwMapMarkers_Lat.Text = "Lat";
            // 
            // lvwMapMarkers_Lon
            // 
            this.lvwMapMarkers_Lon.Text = "Lon";
            // 
            // lblMapMarkerHeader
            // 
            this.lblMapMarkerHeader.AutoSize = true;
            this.lblMapMarkerHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapMarkerHeader.Location = new System.Drawing.Point(455, 16);
            this.lblMapMarkerHeader.Name = "lblMapMarkerHeader";
            this.lblMapMarkerHeader.Size = new System.Drawing.Size(80, 13);
            this.lblMapMarkerHeader.TabIndex = 1;
            this.lblMapMarkerHeader.Text = "Map Markers";
            // 
            // btnRemoveMarker
            // 
            this.btnRemoveMarker.Enabled = false;
            this.btnRemoveMarker.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveMarker.Location = new System.Drawing.Point(491, 258);
            this.btnRemoveMarker.Name = "btnRemoveMarker";
            this.btnRemoveMarker.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveMarker.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnRemoveMarker, "Remove Marker");
            this.btnRemoveMarker.UseVisualStyleBackColor = true;
            this.btnRemoveMarker.Click += new System.EventHandler(this.btnRemoveMarker_Click);
            // 
            // btnAddMarker
            // 
            this.btnAddMarker.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddMarker.Location = new System.Drawing.Point(455, 258);
            this.btnAddMarker.Name = "btnAddMarker";
            this.btnAddMarker.Size = new System.Drawing.Size(30, 30);
            this.btnAddMarker.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAddMarker, "New Marker");
            this.btnAddMarker.UseVisualStyleBackColor = true;
            this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // btnEditMarker
            // 
            this.btnEditMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditMarker.Enabled = false;
            this.btnEditMarker.Image = ((System.Drawing.Image)(resources.GetObject("btnEditMarker.Image")));
            this.btnEditMarker.Location = new System.Drawing.Point(784, 258);
            this.btnEditMarker.Name = "btnEditMarker";
            this.btnEditMarker.Size = new System.Drawing.Size(30, 30);
            this.btnEditMarker.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnEditMarker, "Edit Marker");
            this.btnEditMarker.UseVisualStyleBackColor = true;
            this.btnEditMarker.Click += new System.EventHandler(this.btnEditMarker_Click);
            // 
            // picArtifacts
            // 
            this.picArtifacts.BackColor = System.Drawing.Color.Cornsilk;
            this.picArtifacts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picArtifacts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picArtifacts.Image = ((System.Drawing.Image)(resources.GetObject("picArtifacts.Image")));
            this.picArtifacts.Location = new System.Drawing.Point(34, 209);
            this.picArtifacts.Name = "picArtifacts";
            this.picArtifacts.Size = new System.Drawing.Size(32, 32);
            this.picArtifacts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picArtifacts.TabIndex = 15;
            this.picArtifacts.TabStop = false;
            this.toolTip1.SetToolTip(this.picArtifacts, "Artifact Locations");
            this.picArtifacts.Click += new System.EventHandler(this.picArtifacts_Click);
            // 
            // chkArtifacts
            // 
            this.chkArtifacts.AutoSize = true;
            this.chkArtifacts.Location = new System.Drawing.Point(85, 217);
            this.chkArtifacts.Name = "chkArtifacts";
            this.chkArtifacts.Size = new System.Drawing.Size(64, 17);
            this.chkArtifacts.TabIndex = 7;
            this.chkArtifacts.Text = "Artifacts";
            this.chkArtifacts.UseVisualStyleBackColor = true;
            this.chkArtifacts.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picWyvernNests
            // 
            this.picWyvernNests.BackColor = System.Drawing.Color.White;
            this.picWyvernNests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWyvernNests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWyvernNests.Image = global::ARKViewer.Properties.Resources.structure_marker_wyvern;
            this.picWyvernNests.Location = new System.Drawing.Point(364, 12);
            this.picWyvernNests.Name = "picWyvernNests";
            this.picWyvernNests.Size = new System.Drawing.Size(32, 32);
            this.picWyvernNests.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picWyvernNests.TabIndex = 13;
            this.picWyvernNests.TabStop = false;
            this.toolTip1.SetToolTip(this.picWyvernNests, "Wyvern Nest Locations");
            this.picWyvernNests.Click += new System.EventHandler(this.picWyvernNests_Click);
            // 
            // chkWyvernNests
            // 
            this.chkWyvernNests.AutoSize = true;
            this.chkWyvernNests.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkWyvernNests.Location = new System.Drawing.Point(254, 18);
            this.chkWyvernNests.Name = "chkWyvernNests";
            this.chkWyvernNests.Size = new System.Drawing.Size(93, 17);
            this.chkWyvernNests.TabIndex = 2;
            this.chkWyvernNests.Text = "Wyvern Nests";
            this.chkWyvernNests.UseVisualStyleBackColor = true;
            this.chkWyvernNests.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picObelisks
            // 
            this.picObelisks.BackColor = System.Drawing.Color.Green;
            this.picObelisks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picObelisks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picObelisks.Image = ((System.Drawing.Image)(resources.GetObject("picObelisks.Image")));
            this.picObelisks.Location = new System.Drawing.Point(34, 10);
            this.picObelisks.Name = "picObelisks";
            this.picObelisks.Size = new System.Drawing.Size(32, 32);
            this.picObelisks.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picObelisks.TabIndex = 11;
            this.picObelisks.TabStop = false;
            this.toolTip1.SetToolTip(this.picObelisks, "Obelisk Locations");
            this.picObelisks.Click += new System.EventHandler(this.picObelisks_Click);
            // 
            // chkObelisks
            // 
            this.chkObelisks.AutoSize = true;
            this.chkObelisks.Location = new System.Drawing.Point(85, 18);
            this.chkObelisks.Name = "chkObelisks";
            this.chkObelisks.Size = new System.Drawing.Size(122, 17);
            this.chkObelisks.TabIndex = 6;
            this.chkObelisks.Text = "Obelisks / Terminals";
            this.chkObelisks.UseVisualStyleBackColor = true;
            this.chkObelisks.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picWaterVeins
            // 
            this.picWaterVeins.BackColor = System.Drawing.Color.White;
            this.picWaterVeins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWaterVeins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWaterVeins.Image = global::ARKViewer.Properties.Resources.structure_marker_water;
            this.picWaterVeins.Location = new System.Drawing.Point(34, 128);
            this.picWaterVeins.Name = "picWaterVeins";
            this.picWaterVeins.Size = new System.Drawing.Size(32, 32);
            this.picWaterVeins.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picWaterVeins.TabIndex = 9;
            this.picWaterVeins.TabStop = false;
            this.toolTip1.SetToolTip(this.picWaterVeins, "Water Vein Locations");
            this.picWaterVeins.Click += new System.EventHandler(this.picWaterVeins_Click);
            // 
            // chkWaterVeins
            // 
            this.chkWaterVeins.AutoSize = true;
            this.chkWaterVeins.Location = new System.Drawing.Point(85, 136);
            this.chkWaterVeins.Name = "chkWaterVeins";
            this.chkWaterVeins.Size = new System.Drawing.Size(84, 17);
            this.chkWaterVeins.TabIndex = 4;
            this.chkWaterVeins.Text = "Water Veins";
            this.chkWaterVeins.UseVisualStyleBackColor = true;
            this.chkWaterVeins.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picOilVeins
            // 
            this.picOilVeins.BackColor = System.Drawing.Color.White;
            this.picOilVeins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOilVeins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOilVeins.Image = global::ARKViewer.Properties.Resources.structure_marker_oil;
            this.picOilVeins.Location = new System.Drawing.Point(34, 50);
            this.picOilVeins.Name = "picOilVeins";
            this.picOilVeins.Size = new System.Drawing.Size(32, 32);
            this.picOilVeins.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOilVeins.TabIndex = 7;
            this.picOilVeins.TabStop = false;
            this.toolTip1.SetToolTip(this.picOilVeins, "Oil Vein Locations");
            this.picOilVeins.Click += new System.EventHandler(this.picOilVeins_Click);
            // 
            // chkOilVeins
            // 
            this.chkOilVeins.AutoSize = true;
            this.chkOilVeins.Location = new System.Drawing.Point(85, 56);
            this.chkOilVeins.Name = "chkOilVeins";
            this.chkOilVeins.Size = new System.Drawing.Size(67, 17);
            this.chkOilVeins.TabIndex = 5;
            this.chkOilVeins.Text = "Oil Veins";
            this.chkOilVeins.UseVisualStyleBackColor = true;
            this.chkOilVeins.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picGasVeins
            // 
            this.picGasVeins.BackColor = System.Drawing.Color.White;
            this.picGasVeins.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picGasVeins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGasVeins.Image = global::ARKViewer.Properties.Resources.structure_marker_gas;
            this.picGasVeins.Location = new System.Drawing.Point(34, 88);
            this.picGasVeins.Name = "picGasVeins";
            this.picGasVeins.Size = new System.Drawing.Size(32, 32);
            this.picGasVeins.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGasVeins.TabIndex = 5;
            this.picGasVeins.TabStop = false;
            this.toolTip1.SetToolTip(this.picGasVeins, "Gas Vein Locations");
            this.picGasVeins.Click += new System.EventHandler(this.picGasVeins_Click);
            // 
            // chkGasVeins
            // 
            this.chkGasVeins.AutoSize = true;
            this.chkGasVeins.Location = new System.Drawing.Point(85, 97);
            this.chkGasVeins.Name = "chkGasVeins";
            this.chkGasVeins.Size = new System.Drawing.Size(74, 17);
            this.chkGasVeins.TabIndex = 3;
            this.chkGasVeins.Text = "Gas Veins";
            this.chkGasVeins.UseVisualStyleBackColor = true;
            this.chkGasVeins.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picDeinoNests
            // 
            this.picDeinoNests.BackColor = System.Drawing.Color.White;
            this.picDeinoNests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDeinoNests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDeinoNests.Image = global::ARKViewer.Properties.Resources.structure_marker_deino;
            this.picDeinoNests.Location = new System.Drawing.Point(364, 50);
            this.picDeinoNests.Name = "picDeinoNests";
            this.picDeinoNests.Size = new System.Drawing.Size(32, 32);
            this.picDeinoNests.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDeinoNests.TabIndex = 3;
            this.picDeinoNests.TabStop = false;
            this.toolTip1.SetToolTip(this.picDeinoNests, "Deinonychus Nest Locations");
            this.picDeinoNests.Click += new System.EventHandler(this.picDeinoNests_Click);
            // 
            // chkDeinoNests
            // 
            this.chkDeinoNests.AutoSize = true;
            this.chkDeinoNests.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDeinoNests.Location = new System.Drawing.Point(229, 59);
            this.chkDeinoNests.Name = "chkDeinoNests";
            this.chkDeinoNests.Size = new System.Drawing.Size(118, 17);
            this.chkDeinoNests.TabIndex = 1;
            this.chkDeinoNests.Text = "Deinonychus Nests";
            this.chkDeinoNests.UseVisualStyleBackColor = true;
            this.chkDeinoNests.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // picBeaverDams
            // 
            this.picBeaverDams.BackColor = System.Drawing.Color.White;
            this.picBeaverDams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBeaverDams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBeaverDams.Image = global::ARKViewer.Properties.Resources.structure_marker_beaver;
            this.picBeaverDams.Location = new System.Drawing.Point(364, 170);
            this.picBeaverDams.Name = "picBeaverDams";
            this.picBeaverDams.Size = new System.Drawing.Size(32, 32);
            this.picBeaverDams.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBeaverDams.TabIndex = 1;
            this.picBeaverDams.TabStop = false;
            this.toolTip1.SetToolTip(this.picBeaverDams, "Beaver Dam Locations");
            this.picBeaverDams.Click += new System.EventHandler(this.picBeaverDams_Click);
            // 
            // chkBeaverDams
            // 
            this.chkBeaverDams.AutoSize = true;
            this.chkBeaverDams.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBeaverDams.Location = new System.Drawing.Point(257, 178);
            this.chkBeaverDams.Name = "chkBeaverDams";
            this.chkBeaverDams.Size = new System.Drawing.Size(90, 17);
            this.chkBeaverDams.TabIndex = 0;
            this.chkBeaverDams.Text = "Beaver Dams";
            this.chkBeaverDams.UseVisualStyleBackColor = true;
            this.chkBeaverDams.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 10;
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 10;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 2000;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Information";
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Image = global::ARKViewer.Properties.Resources.button_settings;
            this.btnSettings.Location = new System.Drawing.Point(1307, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(33, 27);
            this.btnSettings.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnSettings, "View and edit settings");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnPlayerInventory
            // 
            this.btnPlayerInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayerInventory.Enabled = false;
            this.btnPlayerInventory.Image = global::ARKViewer.Properties.Resources.button_family;
            this.btnPlayerInventory.Location = new System.Drawing.Point(426, 196);
            this.btnPlayerInventory.Name = "btnPlayerInventory";
            this.btnPlayerInventory.Size = new System.Drawing.Size(30, 30);
            this.btnPlayerInventory.TabIndex = 21;
            this.toolTip1.SetToolTip(this.btnPlayerInventory, "Player View");
            this.btnPlayerInventory.UseVisualStyleBackColor = true;
            this.btnPlayerInventory.Click += new System.EventHandler(this.btnPlayerInventory_Click);
            // 
            // picChargeNodes
            // 
            this.picChargeNodes.BackColor = System.Drawing.Color.White;
            this.picChargeNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picChargeNodes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picChargeNodes.Image = global::ARKViewer.Properties.Resources.structure_marker_battery;
            this.picChargeNodes.Location = new System.Drawing.Point(34, 170);
            this.picChargeNodes.Name = "picChargeNodes";
            this.picChargeNodes.Size = new System.Drawing.Size(32, 32);
            this.picChargeNodes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picChargeNodes.TabIndex = 17;
            this.picChargeNodes.TabStop = false;
            this.toolTip1.SetToolTip(this.picChargeNodes, "Charge Node Locations");
            this.picChargeNodes.Click += new System.EventHandler(this.picChargeNodes_Click);
            // 
            // picDrakeNests
            // 
            this.picDrakeNests.BackColor = System.Drawing.Color.White;
            this.picDrakeNests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDrakeNests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDrakeNests.Image = global::ARKViewer.Properties.Resources.structure_marker_rockdrake;
            this.picDrakeNests.Location = new System.Drawing.Point(364, 90);
            this.picDrakeNests.Name = "picDrakeNests";
            this.picDrakeNests.Size = new System.Drawing.Size(32, 32);
            this.picDrakeNests.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDrakeNests.TabIndex = 23;
            this.picDrakeNests.TabStop = false;
            this.toolTip1.SetToolTip(this.picDrakeNests, "Rock Drake Nest Locations");
            this.picDrakeNests.Click += new System.EventHandler(this.picDrakeNests_Click);
            // 
            // btnPlayerTribeLog
            // 
            this.btnPlayerTribeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayerTribeLog.Enabled = false;
            this.btnPlayerTribeLog.Image = global::ARKViewer.Properties.Resources.button_notepad;
            this.btnPlayerTribeLog.Location = new System.Drawing.Point(388, 196);
            this.btnPlayerTribeLog.Name = "btnPlayerTribeLog";
            this.btnPlayerTribeLog.Size = new System.Drawing.Size(30, 30);
            this.btnPlayerTribeLog.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnPlayerTribeLog, "Tribe Log");
            this.btnPlayerTribeLog.UseVisualStyleBackColor = true;
            this.btnPlayerTribeLog.Click += new System.EventHandler(this.btnPlayerTribeLog_Click);
            // 
            // btnStructureExclusionFilter
            // 
            this.btnStructureExclusionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStructureExclusionFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnStructureExclusionFilter.Image")));
            this.btnStructureExclusionFilter.Location = new System.Drawing.Point(754, 15);
            this.btnStructureExclusionFilter.Name = "btnStructureExclusionFilter";
            this.btnStructureExclusionFilter.Size = new System.Drawing.Size(31, 25);
            this.btnStructureExclusionFilter.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btnStructureExclusionFilter, "Edit Structure Exclusion List");
            this.btnStructureExclusionFilter.UseVisualStyleBackColor = true;
            this.btnStructureExclusionFilter.Click += new System.EventHandler(this.btnStructureExclusionFilter_Click);
            // 
            // btnCopyCommandPlayer
            // 
            this.btnCopyCommandPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandPlayer.Enabled = false;
            this.btnCopyCommandPlayer.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandPlayer.Location = new System.Drawing.Point(350, 196);
            this.btnCopyCommandPlayer.Name = "btnCopyCommandPlayer";
            this.btnCopyCommandPlayer.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommandPlayer.TabIndex = 27;
            this.toolTip1.SetToolTip(this.btnCopyCommandPlayer, "Copy to clipboard");
            this.btnCopyCommandPlayer.UseVisualStyleBackColor = true;
            this.btnCopyCommandPlayer.Click += new System.EventHandler(this.btnCopyCommandPlayer_Click);
            // 
            // btnCopyCommandStructure
            // 
            this.btnCopyCommandStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandStructure.Enabled = false;
            this.btnCopyCommandStructure.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandStructure.Location = new System.Drawing.Point(350, 197);
            this.btnCopyCommandStructure.Name = "btnCopyCommandStructure";
            this.btnCopyCommandStructure.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommandStructure.TabIndex = 32;
            this.toolTip1.SetToolTip(this.btnCopyCommandStructure, "Copy to clipboard");
            this.btnCopyCommandStructure.UseVisualStyleBackColor = true;
            this.btnCopyCommandStructure.Click += new System.EventHandler(this.btnCopyCommandStructure_Click);
            // 
            // btnDinoAncestors
            // 
            this.btnDinoAncestors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDinoAncestors.Image = global::ARKViewer.Properties.Resources.button_family;
            this.btnDinoAncestors.Location = new System.Drawing.Point(553, 194);
            this.btnDinoAncestors.Name = "btnDinoAncestors";
            this.btnDinoAncestors.Size = new System.Drawing.Size(30, 30);
            this.btnDinoAncestors.TabIndex = 26;
            this.toolTip1.SetToolTip(this.btnDinoAncestors, "Ancester Line");
            this.btnDinoAncestors.UseVisualStyleBackColor = true;
            this.btnDinoAncestors.Click += new System.EventHandler(this.btnDinoAncestors_Click);
            // 
            // btnDinoInventory
            // 
            this.btnDinoInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDinoInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnDinoInventory.Image")));
            this.btnDinoInventory.Location = new System.Drawing.Point(587, 194);
            this.btnDinoInventory.Name = "btnDinoInventory";
            this.btnDinoInventory.Size = new System.Drawing.Size(30, 30);
            this.btnDinoInventory.TabIndex = 25;
            this.toolTip1.SetToolTip(this.btnDinoInventory, "Inventory Data");
            this.btnDinoInventory.UseVisualStyleBackColor = true;
            this.btnDinoInventory.Click += new System.EventHandler(this.btnDinoInventory_Click);
            // 
            // btnCopyCommandWild
            // 
            this.btnCopyCommandWild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandWild.Enabled = false;
            this.btnCopyCommandWild.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandWild.Location = new System.Drawing.Point(350, 197);
            this.btnCopyCommandWild.Name = "btnCopyCommandWild";
            this.btnCopyCommandWild.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommandWild.TabIndex = 35;
            this.toolTip1.SetToolTip(this.btnCopyCommandWild, "Copy to clipboard");
            this.btnCopyCommandWild.UseVisualStyleBackColor = true;
            this.btnCopyCommandWild.Click += new System.EventHandler(this.btnCopyCommandWild_Click);
            // 
            // btnCopyCommandTamed
            // 
            this.btnCopyCommandTamed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandTamed.Enabled = false;
            this.btnCopyCommandTamed.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandTamed.Location = new System.Drawing.Point(518, 195);
            this.btnCopyCommandTamed.Name = "btnCopyCommandTamed";
            this.btnCopyCommandTamed.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommandTamed.TabIndex = 35;
            this.toolTip1.SetToolTip(this.btnCopyCommandTamed, "Copy to clipboard");
            this.btnCopyCommandTamed.UseVisualStyleBackColor = true;
            this.btnCopyCommandTamed.Click += new System.EventHandler(this.btnCopyCommandTamed_Click);
            // 
            // chkCryo
            // 
            this.chkCryo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCryo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCryo.BackgroundImage = global::ARKViewer.Properties.Resources.button_cryooff;
            this.chkCryo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkCryo.Location = new System.Drawing.Point(762, 15);
            this.chkCryo.Name = "chkCryo";
            this.chkCryo.Size = new System.Drawing.Size(23, 22);
            this.chkCryo.TabIndex = 36;
            this.toolTip1.SetToolTip(this.chkCryo, "Show/Hide Stored Creatures");
            this.chkCryo.UseVisualStyleBackColor = true;
            this.chkCryo.CheckedChanged += new System.EventHandler(this.chkCryo_CheckedChanged);
            // 
            // btnCopyCommandDropped
            // 
            this.btnCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyCommandDropped.Enabled = false;
            this.btnCopyCommandDropped.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommandDropped.Location = new System.Drawing.Point(350, 197);
            this.btnCopyCommandDropped.Name = "btnCopyCommandDropped";
            this.btnCopyCommandDropped.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommandDropped.TabIndex = 43;
            this.toolTip1.SetToolTip(this.btnCopyCommandDropped, "Copy to clipboard");
            this.btnCopyCommandDropped.UseVisualStyleBackColor = true;
            this.btnCopyCommandDropped.Click += new System.EventHandler(this.btnCopyCommandDropped_Click);
            // 
            // btnTribeCopyCommand
            // 
            this.btnTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTribeCopyCommand.Enabled = false;
            this.btnTribeCopyCommand.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnTribeCopyCommand.Location = new System.Drawing.Point(350, 196);
            this.btnTribeCopyCommand.Name = "btnTribeCopyCommand";
            this.btnTribeCopyCommand.Size = new System.Drawing.Size(30, 30);
            this.btnTribeCopyCommand.TabIndex = 32;
            this.toolTip1.SetToolTip(this.btnTribeCopyCommand, "Copy to clipboard");
            this.btnTribeCopyCommand.UseVisualStyleBackColor = true;
            this.btnTribeCopyCommand.Click += new System.EventHandler(this.btnTribeCopyCommand_Click);
            // 
            // btnTribeLog
            // 
            this.btnTribeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTribeLog.Enabled = false;
            this.btnTribeLog.Image = global::ARKViewer.Properties.Resources.button_notepad;
            this.btnTribeLog.Location = new System.Drawing.Point(388, 196);
            this.btnTribeLog.Name = "btnTribeLog";
            this.btnTribeLog.Size = new System.Drawing.Size(30, 30);
            this.btnTribeLog.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btnTribeLog, "Tribe Log");
            this.btnTribeLog.UseVisualStyleBackColor = true;
            this.btnTribeLog.Click += new System.EventHandler(this.btnTribeLog_Click);
            // 
            // picMagmasaurNests
            // 
            this.picMagmasaurNests.BackColor = System.Drawing.Color.White;
            this.picMagmasaurNests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMagmasaurNests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMagmasaurNests.Image = global::ARKViewer.Properties.Resources.structure_marker_magmasaur;
            this.picMagmasaurNests.Location = new System.Drawing.Point(364, 130);
            this.picMagmasaurNests.Name = "picMagmasaurNests";
            this.picMagmasaurNests.Size = new System.Drawing.Size(32, 32);
            this.picMagmasaurNests.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMagmasaurNests.TabIndex = 25;
            this.picMagmasaurNests.TabStop = false;
            this.toolTip1.SetToolTip(this.picMagmasaurNests, "Magmasaur Nest Locations");
            this.picMagmasaurNests.Click += new System.EventHandler(this.picMagmasaurNests_Click);
            // 
            // picGlitches
            // 
            this.picGlitches.BackColor = System.Drawing.Color.White;
            this.picGlitches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picGlitches.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGlitches.Image = global::ARKViewer.Properties.Resources.structure_marker_glitch;
            this.picGlitches.Location = new System.Drawing.Point(364, 209);
            this.picGlitches.Name = "picGlitches";
            this.picGlitches.Size = new System.Drawing.Size(32, 32);
            this.picGlitches.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGlitches.TabIndex = 27;
            this.picGlitches.TabStop = false;
            this.toolTip1.SetToolTip(this.picGlitches, "Glitch Locations");
            this.picGlitches.Click += new System.EventHandler(this.picGlitches_Click);
            // 
            // btnStructureInventory
            // 
            this.btnStructureInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStructureInventory.Enabled = false;
            this.btnStructureInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnStructureInventory.Image")));
            this.btnStructureInventory.Location = new System.Drawing.Point(386, 197);
            this.btnStructureInventory.Name = "btnStructureInventory";
            this.btnStructureInventory.Size = new System.Drawing.Size(30, 30);
            this.btnStructureInventory.TabIndex = 34;
            this.toolTip1.SetToolTip(this.btnStructureInventory, "Inventory Data");
            this.btnStructureInventory.UseVisualStyleBackColor = true;
            this.btnStructureInventory.Click += new System.EventHandler(this.btnStructureInventory_Click);
            // 
            // btnDeletePlayer
            // 
            this.btnDeletePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeletePlayer.Enabled = false;
            this.btnDeletePlayer.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnDeletePlayer.Location = new System.Drawing.Point(464, 196);
            this.btnDeletePlayer.Name = "btnDeletePlayer";
            this.btnDeletePlayer.Size = new System.Drawing.Size(30, 30);
            this.btnDeletePlayer.TabIndex = 29;
            this.toolTip1.SetToolTip(this.btnDeletePlayer, "Remove Player");
            this.btnDeletePlayer.UseVisualStyleBackColor = true;
            this.btnDeletePlayer.Click += new System.EventHandler(this.btnDeletePlayer_Click);
            // 
            // btnDropInventory
            // 
            this.btnDropInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDropInventory.Enabled = false;
            this.btnDropInventory.Image = ((System.Drawing.Image)(resources.GetObject("btnDropInventory.Image")));
            this.btnDropInventory.Location = new System.Drawing.Point(386, 197);
            this.btnDropInventory.Name = "btnDropInventory";
            this.btnDropInventory.Size = new System.Drawing.Size(30, 30);
            this.btnDropInventory.TabIndex = 49;
            this.toolTip1.SetToolTip(this.btnDropInventory, "Inventory Data");
            this.btnDropInventory.UseVisualStyleBackColor = true;
            this.btnDropInventory.Click += new System.EventHandler(this.btnDropInventory_Click);
            // 
            // txtMarkerFilter
            // 
            this.txtMarkerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarkerFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarkerFilter.Location = new System.Drawing.Point(530, 261);
            this.txtMarkerFilter.Name = "txtMarkerFilter";
            this.txtMarkerFilter.Size = new System.Drawing.Size(212, 22);
            this.txtMarkerFilter.TabIndex = 5;
            this.txtMarkerFilter.TextChanged += new System.EventHandler(this.txtMarkerFilter_TextChanged);
            // 
            // tabFeatures
            // 
            this.tabFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFeatures.Controls.Add(this.tpgWild);
            this.tabFeatures.Controls.Add(this.tpgTamed);
            this.tabFeatures.Controls.Add(this.tpgStructures);
            this.tabFeatures.Controls.Add(this.tpgTribes);
            this.tabFeatures.Controls.Add(this.tpgPlayers);
            this.tabFeatures.Controls.Add(this.tpgDroppedItems);
            this.tabFeatures.Location = new System.Drawing.Point(12, 291);
            this.tabFeatures.Name = "tabFeatures";
            this.tabFeatures.SelectedIndex = 0;
            this.tabFeatures.Size = new System.Drawing.Size(806, 258);
            this.tabFeatures.TabIndex = 16;
            this.tabFeatures.SelectedIndexChanged += new System.EventHandler(this.tabFeatures_SelectedIndexChanged);
            // 
            // tpgWild
            // 
            this.tpgWild.Controls.Add(this.lblWildRadius);
            this.tpgWild.Controls.Add(this.udWildRadius);
            this.tpgWild.Controls.Add(this.lblWildLon);
            this.tpgWild.Controls.Add(this.udWildLon);
            this.tpgWild.Controls.Add(this.lblWildLat);
            this.tpgWild.Controls.Add(this.udWildLat);
            this.tpgWild.Controls.Add(this.lblWildMin);
            this.tpgWild.Controls.Add(this.lblWildMax);
            this.tpgWild.Controls.Add(this.udWildMin);
            this.tpgWild.Controls.Add(this.udWildMax);
            this.tpgWild.Controls.Add(this.btnCopyCommandWild);
            this.tpgWild.Controls.Add(this.lblWildCommand);
            this.tpgWild.Controls.Add(this.cboConsoleCommandsWild);
            this.tpgWild.Controls.Add(this.lblSelectedWildTotal);
            this.tpgWild.Controls.Add(this.lblWildClass);
            this.tpgWild.Controls.Add(this.lvwWildDetail);
            this.tpgWild.Controls.Add(this.lblWildTotal);
            this.tpgWild.Controls.Add(this.cboWildClass);
            this.tpgWild.Location = new System.Drawing.Point(4, 22);
            this.tpgWild.Name = "tpgWild";
            this.tpgWild.Padding = new System.Windows.Forms.Padding(3);
            this.tpgWild.Size = new System.Drawing.Size(798, 232);
            this.tpgWild.TabIndex = 0;
            this.tpgWild.Text = "Wild Creatures";
            this.tpgWild.UseVisualStyleBackColor = true;
            // 
            // lblWildRadius
            // 
            this.lblWildRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWildRadius.AutoSize = true;
            this.lblWildRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildRadius.Location = new System.Drawing.Point(665, 18);
            this.lblWildRadius.Name = "lblWildRadius";
            this.lblWildRadius.Size = new System.Drawing.Size(50, 13);
            this.lblWildRadius.TabIndex = 45;
            this.lblWildRadius.Text = "Radius:";
            // 
            // udWildRadius
            // 
            this.udWildRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udWildRadius.DecimalPlaces = 2;
            this.udWildRadius.Location = new System.Drawing.Point(719, 15);
            this.udWildRadius.Name = "udWildRadius";
            this.udWildRadius.Size = new System.Drawing.Size(64, 20);
            this.udWildRadius.TabIndex = 44;
            this.udWildRadius.Value = new decimal(new int[] {
            10000,
            0,
            0,
            131072});
            this.udWildRadius.ValueChanged += new System.EventHandler(this.udWildRadius_ValueChanged);
            // 
            // lblWildLon
            // 
            this.lblWildLon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWildLon.AutoSize = true;
            this.lblWildLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildLon.Location = new System.Drawing.Point(550, 18);
            this.lblWildLon.Name = "lblWildLon";
            this.lblWildLon.Size = new System.Drawing.Size(32, 13);
            this.lblWildLon.TabIndex = 43;
            this.lblWildLon.Text = "Lon:";
            // 
            // udWildLon
            // 
            this.udWildLon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udWildLon.DecimalPlaces = 2;
            this.udWildLon.Location = new System.Drawing.Point(586, 15);
            this.udWildLon.Name = "udWildLon";
            this.udWildLon.Size = new System.Drawing.Size(64, 20);
            this.udWildLon.TabIndex = 42;
            this.udWildLon.Value = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.udWildLon.ValueChanged += new System.EventHandler(this.udWildLon_ValueChanged);
            // 
            // lblWildLat
            // 
            this.lblWildLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWildLat.AutoSize = true;
            this.lblWildLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildLat.Location = new System.Drawing.Point(428, 18);
            this.lblWildLat.Name = "lblWildLat";
            this.lblWildLat.Size = new System.Drawing.Size(29, 13);
            this.lblWildLat.TabIndex = 41;
            this.lblWildLat.Text = "Lat:";
            // 
            // udWildLat
            // 
            this.udWildLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udWildLat.DecimalPlaces = 2;
            this.udWildLat.Location = new System.Drawing.Point(464, 15);
            this.udWildLat.Name = "udWildLat";
            this.udWildLat.Size = new System.Drawing.Size(64, 20);
            this.udWildLat.TabIndex = 40;
            this.udWildLat.Value = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.udWildLat.ValueChanged += new System.EventHandler(this.udWildLat_ValueChanged);
            // 
            // lblWildMin
            // 
            this.lblWildMin.AutoSize = true;
            this.lblWildMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildMin.Location = new System.Drawing.Point(42, 18);
            this.lblWildMin.Name = "lblWildMin";
            this.lblWildMin.Size = new System.Drawing.Size(31, 13);
            this.lblWildMin.TabIndex = 39;
            this.lblWildMin.Text = "Min:";
            // 
            // lblWildMax
            // 
            this.lblWildMax.AutoSize = true;
            this.lblWildMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildMax.Location = new System.Drawing.Point(142, 18);
            this.lblWildMax.Name = "lblWildMax";
            this.lblWildMax.Size = new System.Drawing.Size(34, 13);
            this.lblWildMax.TabIndex = 38;
            this.lblWildMax.Text = "Max:";
            // 
            // udWildMin
            // 
            this.udWildMin.Location = new System.Drawing.Point(82, 15);
            this.udWildMin.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udWildMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWildMin.Name = "udWildMin";
            this.udWildMin.Size = new System.Drawing.Size(47, 20);
            this.udWildMin.TabIndex = 37;
            this.udWildMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWildMin.ValueChanged += new System.EventHandler(this.udWildMin_ValueChanged);
            // 
            // udWildMax
            // 
            this.udWildMax.Location = new System.Drawing.Point(182, 15);
            this.udWildMax.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.udWildMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udWildMax.Name = "udWildMax";
            this.udWildMax.Size = new System.Drawing.Size(48, 20);
            this.udWildMax.TabIndex = 36;
            this.udWildMax.Value = new decimal(new int[] {
            190,
            0,
            0,
            0});
            this.udWildMax.ValueChanged += new System.EventHandler(this.udWildMax_ValueChanged);
            // 
            // lblWildCommand
            // 
            this.lblWildCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWildCommand.AutoSize = true;
            this.lblWildCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildCommand.Location = new System.Drawing.Point(14, 204);
            this.lblWildCommand.Name = "lblWildCommand";
            this.lblWildCommand.Size = new System.Drawing.Size(65, 13);
            this.lblWildCommand.TabIndex = 34;
            this.lblWildCommand.Text = "Command:";
            // 
            // cboConsoleCommandsWild
            // 
            this.cboConsoleCommandsWild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsWild.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsWild.FormattingEnabled = true;
            this.cboConsoleCommandsWild.Items.AddRange(new object[] {
            "DestroyAll <ClassName>",
            "GMSummon \"<ClassName>\" <Level> ",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsWild.Location = new System.Drawing.Point(82, 201);
            this.cboConsoleCommandsWild.Name = "cboConsoleCommandsWild";
            this.cboConsoleCommandsWild.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsWild.TabIndex = 33;
            // 
            // lblSelectedWildTotal
            // 
            this.lblSelectedWildTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedWildTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblSelectedWildTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedWildTotal.Location = new System.Drawing.Point(528, 197);
            this.lblSelectedWildTotal.Name = "lblSelectedWildTotal";
            this.lblSelectedWildTotal.Size = new System.Drawing.Size(123, 30);
            this.lblSelectedWildTotal.TabIndex = 32;
            this.lblSelectedWildTotal.Text = "Count: 0";
            this.lblSelectedWildTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWildClass
            // 
            this.lblWildClass.AutoSize = true;
            this.lblWildClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWildClass.Location = new System.Drawing.Point(14, 48);
            this.lblWildClass.Name = "lblWildClass";
            this.lblWildClass.Size = new System.Drawing.Size(59, 13);
            this.lblWildClass.TabIndex = 31;
            this.lblWildClass.Text = "Creature:";
            // 
            // tpgTamed
            // 
            this.tpgTamed.Controls.Add(this.chkCryo);
            this.tpgTamed.Controls.Add(this.btnCopyCommandTamed);
            this.tpgTamed.Controls.Add(this.lblTamedCommand);
            this.tpgTamed.Controls.Add(this.cboConsoleCommandsTamed);
            this.tpgTamed.Controls.Add(this.cboTameTribes);
            this.tpgTamed.Controls.Add(this.cboTamePlayers);
            this.tpgTamed.Controls.Add(this.lblTameCreature);
            this.tpgTamed.Controls.Add(this.lblTamePlayer);
            this.tpgTamed.Controls.Add(this.lblTameTribe);
            this.tpgTamed.Controls.Add(this.btnDinoAncestors);
            this.tpgTamed.Controls.Add(this.btnDinoInventory);
            this.tpgTamed.Controls.Add(this.lvwTameDetail);
            this.tpgTamed.Controls.Add(this.lblTameTotal);
            this.tpgTamed.Controls.Add(this.panel3);
            this.tpgTamed.Controls.Add(this.cboTameClass);
            this.tpgTamed.Location = new System.Drawing.Point(4, 22);
            this.tpgTamed.Name = "tpgTamed";
            this.tpgTamed.Size = new System.Drawing.Size(798, 232);
            this.tpgTamed.TabIndex = 3;
            this.tpgTamed.Text = "Tamed Creatures";
            this.tpgTamed.UseVisualStyleBackColor = true;
            // 
            // lblTamedCommand
            // 
            this.lblTamedCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTamedCommand.AutoSize = true;
            this.lblTamedCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamedCommand.Location = new System.Drawing.Point(192, 202);
            this.lblTamedCommand.Name = "lblTamedCommand";
            this.lblTamedCommand.Size = new System.Drawing.Size(65, 13);
            this.lblTamedCommand.TabIndex = 34;
            this.lblTamedCommand.Text = "Command:";
            // 
            // cboConsoleCommandsTamed
            // 
            this.cboConsoleCommandsTamed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsTamed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsTamed.FormattingEnabled = true;
            this.cboConsoleCommandsTamed.Items.AddRange(new object[] {
            "DestroyTribeIdDinos <TribeID>",
            "GMSummon \"<ClassName>\"  <Level> | <DoTame>",
            "GMSummon \"<ClassName>\"  <Level>",
            "TakeTribe <TribeID>",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsTamed.Location = new System.Drawing.Point(260, 199);
            this.cboConsoleCommandsTamed.Name = "cboConsoleCommandsTamed";
            this.cboConsoleCommandsTamed.Size = new System.Drawing.Size(248, 21);
            this.cboConsoleCommandsTamed.TabIndex = 33;
            // 
            // cboTameTribes
            // 
            this.cboTameTribes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTameTribes.FormattingEnabled = true;
            this.cboTameTribes.Location = new System.Drawing.Point(57, 16);
            this.cboTameTribes.Name = "cboTameTribes";
            this.cboTameTribes.Size = new System.Drawing.Size(166, 21);
            this.cboTameTribes.TabIndex = 32;
            this.cboTameTribes.SelectedIndexChanged += new System.EventHandler(this.cboTameTribes_SelectedIndexChanged);
            // 
            // cboTamePlayers
            // 
            this.cboTamePlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTamePlayers.FormattingEnabled = true;
            this.cboTamePlayers.Location = new System.Drawing.Point(281, 16);
            this.cboTamePlayers.Name = "cboTamePlayers";
            this.cboTamePlayers.Size = new System.Drawing.Size(178, 21);
            this.cboTamePlayers.TabIndex = 31;
            this.cboTamePlayers.SelectedIndexChanged += new System.EventHandler(this.cboTamePlayers_SelectedIndexChanged);
            // 
            // lblTameCreature
            // 
            this.lblTameCreature.AutoSize = true;
            this.lblTameCreature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameCreature.Location = new System.Drawing.Point(467, 18);
            this.lblTameCreature.Name = "lblTameCreature";
            this.lblTameCreature.Size = new System.Drawing.Size(59, 13);
            this.lblTameCreature.TabIndex = 30;
            this.lblTameCreature.Text = "Creature:";
            // 
            // lblTamePlayer
            // 
            this.lblTamePlayer.AutoSize = true;
            this.lblTamePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamePlayer.Location = new System.Drawing.Point(229, 18);
            this.lblTamePlayer.Name = "lblTamePlayer";
            this.lblTamePlayer.Size = new System.Drawing.Size(46, 13);
            this.lblTamePlayer.TabIndex = 29;
            this.lblTamePlayer.Text = "Player:";
            // 
            // lblTameTribe
            // 
            this.lblTameTribe.AutoSize = true;
            this.lblTameTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameTribe.Location = new System.Drawing.Point(14, 18);
            this.lblTameTribe.Name = "lblTameTribe";
            this.lblTameTribe.Size = new System.Drawing.Size(40, 13);
            this.lblTameTribe.TabIndex = 28;
            this.lblTameTribe.Text = "Tribe:";
            // 
            // lvwTameDetail
            // 
            this.lvwTameDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTameDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwTameDetail_Creature,
            this.lvwTameDetail_Name,
            this.lvwTameDetail_Sex,
            this.lvwTameDetail_Base,
            this.lvwTameDetail_Level,
            this.lvwTameDetail_Lat,
            this.lvwTameDetail_Lon,
            this.lvwTameDetail_HP,
            this.lvwTameDetail_Stam,
            this.lvwTameDetail_Melee,
            this.lvwTameDetail_Weight,
            this.lvwTameDetail_Speed,
            this.lvwTameDetail_Food,
            this.lvwTameDetail_Oxygen,
            this.lvwTameDetail_Craft,
            this.lvwTameDetail_Server,
            this.lvwTameDetail_Tamer,
            this.lvwTameDetail_Imprinter,
            this.lvwTameDetail_Imprint,
            this.lvwTameDetail_Cryo,
            this.lvwTameDetail_Colour1,
            this.lvwTameDetail_Colour2,
            this.lvwTameDetail_Colour3,
            this.lvwTameDetail_Colour4,
            this.lvwTameDetail_Colour5,
            this.lvwTameDetail_Colour6,
            this.lvwTameDetail_MutationsFemale,
            this.lvwTameDetail_MutationsMale,
            this.lvwTameDetail_Id});
            this.lvwTameDetail.ContextMenuStrip = this.mnuContext;
            this.lvwTameDetail.FullRowSelect = true;
            this.lvwTameDetail.HideSelection = false;
            this.lvwTameDetail.Location = new System.Drawing.Point(11, 51);
            this.lvwTameDetail.MultiSelect = false;
            this.lvwTameDetail.Name = "lvwTameDetail";
            this.lvwTameDetail.Size = new System.Drawing.Size(774, 138);
            this.lvwTameDetail.TabIndex = 15;
            this.lvwTameDetail.UseCompatibleStateImageBehavior = false;
            this.lvwTameDetail.View = System.Windows.Forms.View.Details;
            this.lvwTameDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTameDetail_ColumnClick);
            this.lvwTameDetail.SelectedIndexChanged += new System.EventHandler(this.lvwTameDetail_SelectedIndexChanged);
            this.lvwTameDetail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwTameDetail_MouseClick);
            // 
            // lvwTameDetail_Creature
            // 
            this.lvwTameDetail_Creature.Text = "Creature";
            this.lvwTameDetail_Creature.Width = 168;
            // 
            // lvwTameDetail_Name
            // 
            this.lvwTameDetail_Name.Text = "Name";
            this.lvwTameDetail_Name.Width = 150;
            // 
            // lvwTameDetail_Sex
            // 
            this.lvwTameDetail_Sex.Text = "Sex";
            // 
            // lvwTameDetail_Base
            // 
            this.lvwTameDetail_Base.Text = "Base";
            this.lvwTameDetail_Base.Width = 50;
            // 
            // lvwTameDetail_Level
            // 
            this.lvwTameDetail_Level.Text = "Lvl";
            this.lvwTameDetail_Level.Width = 41;
            // 
            // lvwTameDetail_Lat
            // 
            this.lvwTameDetail_Lat.Text = "Lat";
            this.lvwTameDetail_Lat.Width = 58;
            // 
            // lvwTameDetail_Lon
            // 
            this.lvwTameDetail_Lon.Text = "Lon";
            this.lvwTameDetail_Lon.Width = 57;
            // 
            // lvwTameDetail_HP
            // 
            this.lvwTameDetail_HP.Text = "HP";
            this.lvwTameDetail_HP.Width = 45;
            // 
            // lvwTameDetail_Stam
            // 
            this.lvwTameDetail_Stam.Text = "Stam";
            this.lvwTameDetail_Stam.Width = 45;
            // 
            // lvwTameDetail_Melee
            // 
            this.lvwTameDetail_Melee.Text = "Melee";
            this.lvwTameDetail_Melee.Width = 48;
            // 
            // lvwTameDetail_Weight
            // 
            this.lvwTameDetail_Weight.Text = "Weight";
            this.lvwTameDetail_Weight.Width = 55;
            // 
            // lvwTameDetail_Speed
            // 
            this.lvwTameDetail_Speed.Text = "Speed";
            this.lvwTameDetail_Speed.Width = 50;
            // 
            // lvwTameDetail_Food
            // 
            this.lvwTameDetail_Food.Text = "Food";
            this.lvwTameDetail_Food.Width = 47;
            // 
            // lvwTameDetail_Oxygen
            // 
            this.lvwTameDetail_Oxygen.Text = "Oxygen";
            this.lvwTameDetail_Oxygen.Width = 53;
            // 
            // lvwTameDetail_Craft
            // 
            this.lvwTameDetail_Craft.Text = "Craft";
            this.lvwTameDetail_Craft.Width = 50;
            // 
            // lvwTameDetail_Server
            // 
            this.lvwTameDetail_Server.Text = "Server";
            this.lvwTameDetail_Server.Width = 150;
            // 
            // lvwTameDetail_Tamer
            // 
            this.lvwTameDetail_Tamer.Text = "Tamer";
            this.lvwTameDetail_Tamer.Width = 105;
            // 
            // lvwTameDetail_Imprinter
            // 
            this.lvwTameDetail_Imprinter.Text = "Imprinter";
            this.lvwTameDetail_Imprinter.Width = 105;
            // 
            // lvwTameDetail_Imprint
            // 
            this.lvwTameDetail_Imprint.Text = "Imprint";
            // 
            // lvwTameDetail_Cryo
            // 
            this.lvwTameDetail_Cryo.Text = "Stored";
            // 
            // lvwTameDetail_Colour1
            // 
            this.lvwTameDetail_Colour1.Text = "C0";
            this.lvwTameDetail_Colour1.Width = 35;
            // 
            // lvwTameDetail_Colour2
            // 
            this.lvwTameDetail_Colour2.Text = "C1";
            this.lvwTameDetail_Colour2.Width = 35;
            // 
            // lvwTameDetail_Colour3
            // 
            this.lvwTameDetail_Colour3.Text = "C2";
            this.lvwTameDetail_Colour3.Width = 35;
            // 
            // lvwTameDetail_Colour4
            // 
            this.lvwTameDetail_Colour4.Text = "C3";
            this.lvwTameDetail_Colour4.Width = 35;
            // 
            // lvwTameDetail_Colour5
            // 
            this.lvwTameDetail_Colour5.Text = "C4";
            this.lvwTameDetail_Colour5.Width = 35;
            // 
            // lvwTameDetail_Colour6
            // 
            this.lvwTameDetail_Colour6.Text = "C5";
            this.lvwTameDetail_Colour6.Width = 35;
            // 
            // lvwTameDetail_MutationsFemale
            // 
            this.lvwTameDetail_MutationsFemale.Text = "Mut (F)";
            // 
            // lvwTameDetail_MutationsMale
            // 
            this.lvwTameDetail_MutationsMale.Text = "Mut (M)";
            // 
            // lvwTameDetail_Id
            // 
            this.lvwTameDetail_Id.Text = "Id";
            this.lvwTameDetail_Id.Width = 0;
            // 
            // lblTameTotal
            // 
            this.lblTameTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTameTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblTameTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTameTotal.Location = new System.Drawing.Point(662, 195);
            this.lblTameTotal.Name = "lblTameTotal";
            this.lblTameTotal.Size = new System.Drawing.Size(123, 30);
            this.lblTameTotal.TabIndex = 13;
            this.lblTameTotal.Text = "Count: 0";
            this.lblTameTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblStats);
            this.panel3.Controls.Add(this.optStatsTamed);
            this.panel3.Controls.Add(this.optStatsBase);
            this.panel3.Location = new System.Drawing.Point(11, 193);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 34);
            this.panel3.TabIndex = 16;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(2, 10);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(34, 13);
            this.lblStats.TabIndex = 19;
            this.lblStats.Text = "Stats:";
            // 
            // optStatsTamed
            // 
            this.optStatsTamed.AutoSize = true;
            this.optStatsTamed.Location = new System.Drawing.Point(97, 8);
            this.optStatsTamed.Name = "optStatsTamed";
            this.optStatsTamed.Size = new System.Drawing.Size(58, 17);
            this.optStatsTamed.TabIndex = 1;
            this.optStatsTamed.Text = "Tamed";
            this.optStatsTamed.UseVisualStyleBackColor = true;
            // 
            // optStatsBase
            // 
            this.optStatsBase.AutoSize = true;
            this.optStatsBase.Checked = true;
            this.optStatsBase.Location = new System.Drawing.Point(42, 8);
            this.optStatsBase.Name = "optStatsBase";
            this.optStatsBase.Size = new System.Drawing.Size(49, 17);
            this.optStatsBase.TabIndex = 0;
            this.optStatsBase.TabStop = true;
            this.optStatsBase.Text = "Base";
            this.optStatsBase.UseVisualStyleBackColor = true;
            this.optStatsBase.CheckedChanged += new System.EventHandler(this.optStatsBase_CheckedChanged);
            // 
            // cboTameClass
            // 
            this.cboTameClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTameClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTameClass.FormattingEnabled = true;
            this.cboTameClass.Location = new System.Drawing.Point(532, 16);
            this.cboTameClass.Name = "cboTameClass";
            this.cboTameClass.Size = new System.Drawing.Size(224, 21);
            this.cboTameClass.TabIndex = 14;
            this.cboTameClass.SelectedIndexChanged += new System.EventHandler(this.cboTameClass_SelectedIndexChanged);
            // 
            // tpgStructures
            // 
            this.tpgStructures.Controls.Add(this.btnStructureInventory);
            this.tpgStructures.Controls.Add(this.lblStructureTotal);
            this.tpgStructures.Controls.Add(this.btnCopyCommandStructure);
            this.tpgStructures.Controls.Add(this.lblCommandStructure);
            this.tpgStructures.Controls.Add(this.cboConsoleCommandsStructure);
            this.tpgStructures.Controls.Add(this.btnStructureExclusionFilter);
            this.tpgStructures.Controls.Add(this.lblStructureStructure);
            this.tpgStructures.Controls.Add(this.cboStructureStructure);
            this.tpgStructures.Controls.Add(this.lblStructurePlayer);
            this.tpgStructures.Controls.Add(this.lblStructureTribe);
            this.tpgStructures.Controls.Add(this.cboStructureTribe);
            this.tpgStructures.Controls.Add(this.cboStructurePlayer);
            this.tpgStructures.Controls.Add(this.lvwStructureLocations);
            this.tpgStructures.Location = new System.Drawing.Point(4, 22);
            this.tpgStructures.Name = "tpgStructures";
            this.tpgStructures.Size = new System.Drawing.Size(798, 232);
            this.tpgStructures.TabIndex = 2;
            this.tpgStructures.Text = "Player Structures";
            this.tpgStructures.UseVisualStyleBackColor = true;
            // 
            // lblStructureTotal
            // 
            this.lblStructureTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStructureTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblStructureTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureTotal.Location = new System.Drawing.Point(662, 196);
            this.lblStructureTotal.Name = "lblStructureTotal";
            this.lblStructureTotal.Size = new System.Drawing.Size(123, 30);
            this.lblStructureTotal.TabIndex = 33;
            this.lblStructureTotal.Text = "Total: 0";
            this.lblStructureTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommandStructure
            // 
            this.lblCommandStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommandStructure.AutoSize = true;
            this.lblCommandStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandStructure.Location = new System.Drawing.Point(14, 204);
            this.lblCommandStructure.Name = "lblCommandStructure";
            this.lblCommandStructure.Size = new System.Drawing.Size(65, 13);
            this.lblCommandStructure.TabIndex = 31;
            this.lblCommandStructure.Text = "Command:";
            // 
            // cboConsoleCommandsStructure
            // 
            this.cboConsoleCommandsStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsStructure.FormattingEnabled = true;
            this.cboConsoleCommandsStructure.Items.AddRange(new object[] {
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "TakeTribe <TribeID>",
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommandsStructure.Location = new System.Drawing.Point(82, 201);
            this.cboConsoleCommandsStructure.Name = "cboConsoleCommandsStructure";
            this.cboConsoleCommandsStructure.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsStructure.TabIndex = 30;
            this.cboConsoleCommandsStructure.SelectedIndexChanged += new System.EventHandler(this.cboConsoleCommandsStructure_SelectedIndexChanged);
            // 
            // lblStructureStructure
            // 
            this.lblStructureStructure.AutoSize = true;
            this.lblStructureStructure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureStructure.Location = new System.Drawing.Point(483, 18);
            this.lblStructureStructure.Name = "lblStructureStructure";
            this.lblStructureStructure.Size = new System.Drawing.Size(63, 13);
            this.lblStructureStructure.TabIndex = 27;
            this.lblStructureStructure.Text = "Structure:";
            // 
            // cboStructureStructure
            // 
            this.cboStructureStructure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStructureStructure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructureStructure.FormattingEnabled = true;
            this.cboStructureStructure.Location = new System.Drawing.Point(553, 16);
            this.cboStructureStructure.Name = "cboStructureStructure";
            this.cboStructureStructure.Size = new System.Drawing.Size(196, 21);
            this.cboStructureStructure.TabIndex = 26;
            this.cboStructureStructure.SelectedIndexChanged += new System.EventHandler(this.cboStructureStructure_SelectedIndexChanged);
            // 
            // lblStructurePlayer
            // 
            this.lblStructurePlayer.AutoSize = true;
            this.lblStructurePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructurePlayer.Location = new System.Drawing.Point(229, 18);
            this.lblStructurePlayer.Name = "lblStructurePlayer";
            this.lblStructurePlayer.Size = new System.Drawing.Size(46, 13);
            this.lblStructurePlayer.TabIndex = 25;
            this.lblStructurePlayer.Text = "Player:";
            // 
            // lblStructureTribe
            // 
            this.lblStructureTribe.AutoSize = true;
            this.lblStructureTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureTribe.Location = new System.Drawing.Point(14, 18);
            this.lblStructureTribe.Name = "lblStructureTribe";
            this.lblStructureTribe.Size = new System.Drawing.Size(40, 13);
            this.lblStructureTribe.TabIndex = 24;
            this.lblStructureTribe.Text = "Tribe:";
            // 
            // cboStructureTribe
            // 
            this.cboStructureTribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructureTribe.FormattingEnabled = true;
            this.cboStructureTribe.Location = new System.Drawing.Point(57, 16);
            this.cboStructureTribe.Name = "cboStructureTribe";
            this.cboStructureTribe.Size = new System.Drawing.Size(166, 21);
            this.cboStructureTribe.TabIndex = 23;
            this.cboStructureTribe.SelectedIndexChanged += new System.EventHandler(this.cboStructureTribe_SelectedIndexChanged);
            // 
            // cboStructurePlayer
            // 
            this.cboStructurePlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStructurePlayer.FormattingEnabled = true;
            this.cboStructurePlayer.Location = new System.Drawing.Point(281, 16);
            this.cboStructurePlayer.Name = "cboStructurePlayer";
            this.cboStructurePlayer.Size = new System.Drawing.Size(193, 21);
            this.cboStructurePlayer.TabIndex = 22;
            this.cboStructurePlayer.SelectedIndexChanged += new System.EventHandler(this.cboStructurePlayer_SelectedIndexChanged);
            // 
            // lvwStructureLocations
            // 
            this.lvwStructureLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwStructureLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwStructureLocations_Player,
            this.lvwStructureLocations_Tribe,
            this.lvwStructureLocations_Structure,
            this.lvwStructureLocations_Lat,
            this.lvwStructureLocations_Lon});
            this.lvwStructureLocations.ContextMenuStrip = this.mnuContext;
            this.lvwStructureLocations.FullRowSelect = true;
            this.lvwStructureLocations.HideSelection = false;
            this.lvwStructureLocations.Location = new System.Drawing.Point(12, 51);
            this.lvwStructureLocations.MultiSelect = false;
            this.lvwStructureLocations.Name = "lvwStructureLocations";
            this.lvwStructureLocations.Size = new System.Drawing.Size(773, 137);
            this.lvwStructureLocations.TabIndex = 21;
            this.lvwStructureLocations.UseCompatibleStateImageBehavior = false;
            this.lvwStructureLocations.View = System.Windows.Forms.View.Details;
            this.lvwStructureLocations.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwStructureLocations_ColumnClick);
            this.lvwStructureLocations.SelectedIndexChanged += new System.EventHandler(this.lvwStructureLocations_SelectedIndexChanged);
            this.lvwStructureLocations.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwStructureLocations_MouseClick);
            // 
            // lvwStructureLocations_Player
            // 
            this.lvwStructureLocations_Player.Text = "Player";
            this.lvwStructureLocations_Player.Width = 177;
            // 
            // lvwStructureLocations_Tribe
            // 
            this.lvwStructureLocations_Tribe.Text = "Tribe";
            this.lvwStructureLocations_Tribe.Width = 172;
            // 
            // lvwStructureLocations_Structure
            // 
            this.lvwStructureLocations_Structure.Text = "Structure";
            this.lvwStructureLocations_Structure.Width = 206;
            // 
            // lvwStructureLocations_Lat
            // 
            this.lvwStructureLocations_Lat.Text = "Lat";
            this.lvwStructureLocations_Lat.Width = 65;
            // 
            // lvwStructureLocations_Lon
            // 
            this.lvwStructureLocations_Lon.Text = "Lon";
            this.lvwStructureLocations_Lon.Width = 62;
            // 
            // tpgTribes
            // 
            this.tpgTribes.Controls.Add(this.chkTribeStructures);
            this.tpgTribes.Controls.Add(this.chkTribeTames);
            this.tpgTribes.Controls.Add(this.chkTribePlayers);
            this.tpgTribes.Controls.Add(this.btnTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.lblTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.cboTribeCopyCommand);
            this.tpgTribes.Controls.Add(this.btnTribeLog);
            this.tpgTribes.Controls.Add(this.lvwTribes);
            this.tpgTribes.Location = new System.Drawing.Point(4, 22);
            this.tpgTribes.Name = "tpgTribes";
            this.tpgTribes.Size = new System.Drawing.Size(798, 232);
            this.tpgTribes.TabIndex = 5;
            this.tpgTribes.Text = "Tribes";
            this.tpgTribes.UseVisualStyleBackColor = true;
            // 
            // chkTribeStructures
            // 
            this.chkTribeStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribeStructures.BackColor = System.Drawing.Color.PaleGreen;
            this.chkTribeStructures.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribeStructures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribeStructures.ForeColor = System.Drawing.Color.ForestGreen;
            this.chkTribeStructures.Location = new System.Drawing.Point(664, 197);
            this.chkTribeStructures.Name = "chkTribeStructures";
            this.chkTribeStructures.Size = new System.Drawing.Size(121, 29);
            this.chkTribeStructures.TabIndex = 35;
            this.chkTribeStructures.Text = "Structure Markers";
            this.chkTribeStructures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTribeStructures.UseVisualStyleBackColor = false;
            this.chkTribeStructures.CheckedChanged += new System.EventHandler(this.chkTribeStructures_CheckedChanged);
            // 
            // chkTribeTames
            // 
            this.chkTribeTames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribeTames.BackColor = System.Drawing.Color.Gold;
            this.chkTribeTames.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribeTames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribeTames.ForeColor = System.Drawing.Color.Chocolate;
            this.chkTribeTames.Location = new System.Drawing.Point(553, 198);
            this.chkTribeTames.Name = "chkTribeTames";
            this.chkTribeTames.Size = new System.Drawing.Size(105, 29);
            this.chkTribeTames.TabIndex = 34;
            this.chkTribeTames.Text = "Tame Markers";
            this.chkTribeTames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTribeTames.UseVisualStyleBackColor = false;
            this.chkTribeTames.CheckedChanged += new System.EventHandler(this.chkTribeTames_CheckedChanged);
            // 
            // chkTribePlayers
            // 
            this.chkTribePlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTribePlayers.BackColor = System.Drawing.Color.CornflowerBlue;
            this.chkTribePlayers.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.chkTribePlayers.Checked = true;
            this.chkTribePlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTribePlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTribePlayers.ForeColor = System.Drawing.Color.LightCyan;
            this.chkTribePlayers.Location = new System.Drawing.Point(433, 197);
            this.chkTribePlayers.Name = "chkTribePlayers";
            this.chkTribePlayers.Size = new System.Drawing.Size(114, 31);
            this.chkTribePlayers.TabIndex = 33;
            this.chkTribePlayers.Text = "Player Markers";
            this.chkTribePlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkTribePlayers.UseVisualStyleBackColor = false;
            this.chkTribePlayers.CheckedChanged += new System.EventHandler(this.chkTribePlayers_CheckedChanged);
            // 
            // lblTribeCopyCommand
            // 
            this.lblTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTribeCopyCommand.AutoSize = true;
            this.lblTribeCopyCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTribeCopyCommand.Location = new System.Drawing.Point(14, 203);
            this.lblTribeCopyCommand.Name = "lblTribeCopyCommand";
            this.lblTribeCopyCommand.Size = new System.Drawing.Size(65, 13);
            this.lblTribeCopyCommand.TabIndex = 31;
            this.lblTribeCopyCommand.Text = "Command:";
            // 
            // cboTribeCopyCommand
            // 
            this.cboTribeCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboTribeCopyCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTribeCopyCommand.FormattingEnabled = true;
            this.cboTribeCopyCommand.Items.AddRange(new object[] {
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "RenameTribe \"<TribeName>\" ",
            "TakeTribe <TribeID>",
            "TribeStructureAudit <TribeID>",
            "TribeDinoAudit  <TribeID>",
            "RM <FileCsvList>",
            "DEL <FileCsvList>"});
            this.cboTribeCopyCommand.Location = new System.Drawing.Point(82, 200);
            this.cboTribeCopyCommand.Name = "cboTribeCopyCommand";
            this.cboTribeCopyCommand.Size = new System.Drawing.Size(262, 21);
            this.cboTribeCopyCommand.TabIndex = 30;
            // 
            // lvwTribes
            // 
            this.lvwTribes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTribes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lvwTribes.ContextMenuStrip = this.mnuContext;
            this.lvwTribes.FullRowSelect = true;
            this.lvwTribes.HideSelection = false;
            this.lvwTribes.Location = new System.Drawing.Point(12, 19);
            this.lvwTribes.Name = "lvwTribes";
            this.lvwTribes.Size = new System.Drawing.Size(773, 170);
            this.lvwTribes.TabIndex = 28;
            this.lvwTribes.UseCompatibleStateImageBehavior = false;
            this.lvwTribes.View = System.Windows.Forms.View.Details;
            this.lvwTribes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTribes_ColumnClick);
            this.lvwTribes.SelectedIndexChanged += new System.EventHandler(this.lvwTribes_SelectedIndexChanged);
            this.lvwTribes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwTribes_MouseClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Id";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Name";
            this.columnHeader11.Width = 228;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Players";
            this.columnHeader12.Width = 79;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Tames";
            this.columnHeader13.Width = 81;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Structures";
            this.columnHeader14.Width = 91;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Last Active";
            this.columnHeader15.Width = 127;
            // 
            // tpgPlayers
            // 
            this.tpgPlayers.Controls.Add(this.btnDeletePlayer);
            this.tpgPlayers.Controls.Add(this.lblPlayerTotal);
            this.tpgPlayers.Controls.Add(this.btnCopyCommandPlayer);
            this.tpgPlayers.Controls.Add(this.lblCommandPlayer);
            this.tpgPlayers.Controls.Add(this.cboConsoleCommandsPlayerTribe);
            this.tpgPlayers.Controls.Add(this.btnPlayerTribeLog);
            this.tpgPlayers.Controls.Add(this.btnPlayerInventory);
            this.tpgPlayers.Controls.Add(this.lblPlayersPlayer);
            this.tpgPlayers.Controls.Add(this.lblPlayersTribe);
            this.tpgPlayers.Controls.Add(this.cboTribes);
            this.tpgPlayers.Controls.Add(this.cboPlayers);
            this.tpgPlayers.Controls.Add(this.lvwPlayers);
            this.tpgPlayers.Location = new System.Drawing.Point(4, 22);
            this.tpgPlayers.Name = "tpgPlayers";
            this.tpgPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlayers.Size = new System.Drawing.Size(798, 232);
            this.tpgPlayers.TabIndex = 1;
            this.tpgPlayers.Text = "Players";
            this.tpgPlayers.UseVisualStyleBackColor = true;
            // 
            // lblPlayerTotal
            // 
            this.lblPlayerTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerTotal.BackColor = System.Drawing.Color.PowderBlue;
            this.lblPlayerTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTotal.Location = new System.Drawing.Point(662, 196);
            this.lblPlayerTotal.Name = "lblPlayerTotal";
            this.lblPlayerTotal.Size = new System.Drawing.Size(123, 30);
            this.lblPlayerTotal.TabIndex = 28;
            this.lblPlayerTotal.Text = "Total: 0";
            this.lblPlayerTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommandPlayer
            // 
            this.lblCommandPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCommandPlayer.AutoSize = true;
            this.lblCommandPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommandPlayer.Location = new System.Drawing.Point(14, 203);
            this.lblCommandPlayer.Name = "lblCommandPlayer";
            this.lblCommandPlayer.Size = new System.Drawing.Size(65, 13);
            this.lblCommandPlayer.TabIndex = 26;
            this.lblCommandPlayer.Text = "Command:";
            // 
            // cboConsoleCommandsPlayerTribe
            // 
            this.cboConsoleCommandsPlayerTribe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboConsoleCommandsPlayerTribe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommandsPlayerTribe.FormattingEnabled = true;
            this.cboConsoleCommandsPlayerTribe.Items.AddRange(new object[] {
            "AllowPlayerToJoinNoCheck <SteamID>",
            "BanPlayer <SteamID>",
            "ClearPlayerInventory <PlayerID> true true true",
            "DefeatAllBosses <PlayerID> ",
            "DestroyTribeId <TribeID> ",
            "DestroyTribeIdDinos <TribeID>",
            "DestroyTribeIdPlayers <TribeID>",
            "DestroyTribeIdStructures <TribeID>",
            "DisallowPlayerToJoinNoCheck <SteamID> ",
            "GetPlayerIDForSteamID <SteamID> ",
            "GetSteamIDForPlayerID <PlayerID> ",
            "GiveCreativeModeToPlayer <PlayerID> ",
            "GiveTekengramsTo <PlayerID>",
            "KickPlayer <SteamID> ",
            "KillPlayer <PlayerID>",
            "MaxAscend <PlayerID>  ",
            "RenamePlayer \"<CharacterName>\" ",
            "RenameTribe \"<TribeName>\" ",
            "ServerChatToPlayer <PlayerName>",
            "SetImprintedPlayer \"<CharacterName>\" <PlayerID>",
            "SetPlayerPos  <x> <y> <z>",
            "TakeTribe <TribeID>",
            "TeleportPlayerIDToMe <PlayerID>",
            "TeleportPlayerNameToMe <CharacterName>",
            "TeleportToPlayer <PlayerID>",
            "TeleportToPlayerName <CharacterName",
            "TribeStructureAudit <TribeID>",
            "TribeDinoAudit  <TribeID>",
            "UnbanPlayer <SteamID>",
            "RM <FileCsvList>",
            "DEL <FileCsvList>"});
            this.cboConsoleCommandsPlayerTribe.Location = new System.Drawing.Point(82, 200);
            this.cboConsoleCommandsPlayerTribe.Name = "cboConsoleCommandsPlayerTribe";
            this.cboConsoleCommandsPlayerTribe.Size = new System.Drawing.Size(262, 21);
            this.cboConsoleCommandsPlayerTribe.TabIndex = 25;
            this.cboConsoleCommandsPlayerTribe.SelectedIndexChanged += new System.EventHandler(this.cboConsoleCommandsPlayerTribe_SelectedIndexChanged);
            // 
            // lblPlayersPlayer
            // 
            this.lblPlayersPlayer.AutoSize = true;
            this.lblPlayersPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersPlayer.Location = new System.Drawing.Point(414, 18);
            this.lblPlayersPlayer.Name = "lblPlayersPlayer";
            this.lblPlayersPlayer.Size = new System.Drawing.Size(46, 13);
            this.lblPlayersPlayer.TabIndex = 20;
            this.lblPlayersPlayer.Text = "Player:";
            // 
            // lblPlayersTribe
            // 
            this.lblPlayersTribe.AutoSize = true;
            this.lblPlayersTribe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayersTribe.Location = new System.Drawing.Point(14, 18);
            this.lblPlayersTribe.Name = "lblPlayersTribe";
            this.lblPlayersTribe.Size = new System.Drawing.Size(40, 13);
            this.lblPlayersTribe.TabIndex = 19;
            this.lblPlayersTribe.Text = "Tribe:";
            // 
            // cboTribes
            // 
            this.cboTribes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTribes.FormattingEnabled = true;
            this.cboTribes.Location = new System.Drawing.Point(60, 15);
            this.cboTribes.Name = "cboTribes";
            this.cboTribes.Size = new System.Drawing.Size(287, 21);
            this.cboTribes.TabIndex = 14;
            this.cboTribes.SelectedIndexChanged += new System.EventHandler(this.cboTribes_SelectedIndexChanged);
            // 
            // cboPlayers
            // 
            this.cboPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlayers.FormattingEnabled = true;
            this.cboPlayers.Location = new System.Drawing.Point(466, 15);
            this.cboPlayers.Name = "cboPlayers";
            this.cboPlayers.Size = new System.Drawing.Size(317, 21);
            this.cboPlayers.TabIndex = 13;
            this.cboPlayers.SelectedIndexChanged += new System.EventHandler(this.cboPlayers_SelectedIndexChanged);
            // 
            // lvwPlayers
            // 
            this.lvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwPlayers_Name,
            this.lvwPlayers_Tribe,
            this.lvwPlayers_Sex,
            this.lvwPlayers_Level,
            this.lvwPlayers_Lat,
            this.lvwPlayers_Lon,
            this.lvwPlayers_Hp,
            this.lvwPlayers_Stam,
            this.lvwPlayers_Melee,
            this.lvwPlayers_Weight,
            this.lvwPlayers_Speed,
            this.lvwPlayers_Food,
            this.lvwPlayers_Water,
            this.lvwPlayers_Oxygen,
            this.lvwPlayers_Crafting,
            this.lvwPlayers_Fortitude,
            this.lvwPlayers_LastOnline,
            this.lvwPlayers_SteamName,
            this.lvwPlayers_SteamId});
            this.lvwPlayers.ContextMenuStrip = this.mnuContext;
            this.lvwPlayers.FullRowSelect = true;
            this.lvwPlayers.HideSelection = false;
            this.lvwPlayers.Location = new System.Drawing.Point(12, 52);
            this.lvwPlayers.Name = "lvwPlayers";
            this.lvwPlayers.Size = new System.Drawing.Size(773, 137);
            this.lvwPlayers.TabIndex = 12;
            this.lvwPlayers.UseCompatibleStateImageBehavior = false;
            this.lvwPlayers.View = System.Windows.Forms.View.Details;
            this.lvwPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPlayers_ColumnClick);
            this.lvwPlayers.SelectedIndexChanged += new System.EventHandler(this.lvwPlayers_SelectedIndexChanged);
            this.lvwPlayers.Click += new System.EventHandler(this.lvwPlayers_Click);
            this.lvwPlayers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwPlayers_MouseClick);
            // 
            // lvwPlayers_Name
            // 
            this.lvwPlayers_Name.Text = "Name";
            this.lvwPlayers_Name.Width = 90;
            // 
            // lvwPlayers_Tribe
            // 
            this.lvwPlayers_Tribe.Text = "Tribe";
            this.lvwPlayers_Tribe.Width = 90;
            // 
            // lvwPlayers_Sex
            // 
            this.lvwPlayers_Sex.Text = "Sex";
            this.lvwPlayers_Sex.Width = 55;
            // 
            // lvwPlayers_Level
            // 
            this.lvwPlayers_Level.Text = "Lvl";
            this.lvwPlayers_Level.Width = 35;
            // 
            // lvwPlayers_Lat
            // 
            this.lvwPlayers_Lat.Text = "Lat";
            this.lvwPlayers_Lat.Width = 45;
            // 
            // lvwPlayers_Lon
            // 
            this.lvwPlayers_Lon.Text = "Lon";
            this.lvwPlayers_Lon.Width = 45;
            // 
            // lvwPlayers_Hp
            // 
            this.lvwPlayers_Hp.Text = "HP";
            this.lvwPlayers_Hp.Width = 45;
            // 
            // lvwPlayers_Stam
            // 
            this.lvwPlayers_Stam.Text = "Stam";
            this.lvwPlayers_Stam.Width = 45;
            // 
            // lvwPlayers_Melee
            // 
            this.lvwPlayers_Melee.Text = "Melee";
            this.lvwPlayers_Melee.Width = 48;
            // 
            // lvwPlayers_Weight
            // 
            this.lvwPlayers_Weight.Text = "Weight";
            this.lvwPlayers_Weight.Width = 55;
            // 
            // lvwPlayers_Speed
            // 
            this.lvwPlayers_Speed.Text = "Speed";
            this.lvwPlayers_Speed.Width = 50;
            // 
            // lvwPlayers_Food
            // 
            this.lvwPlayers_Food.Text = "Food";
            this.lvwPlayers_Food.Width = 47;
            // 
            // lvwPlayers_Water
            // 
            this.lvwPlayers_Water.Text = "Water";
            // 
            // lvwPlayers_Oxygen
            // 
            this.lvwPlayers_Oxygen.Text = "Oxygen";
            this.lvwPlayers_Oxygen.Width = 53;
            // 
            // lvwPlayers_Crafting
            // 
            this.lvwPlayers_Crafting.Text = "Crafting";
            // 
            // lvwPlayers_Fortitude
            // 
            this.lvwPlayers_Fortitude.Text = "Fortitude";
            // 
            // lvwPlayers_LastOnline
            // 
            this.lvwPlayers_LastOnline.Text = "Last Online";
            this.lvwPlayers_LastOnline.Width = 140;
            // 
            // lvwPlayers_SteamName
            // 
            this.lvwPlayers_SteamName.Text = "Steam Name";
            this.lvwPlayers_SteamName.Width = 150;
            // 
            // lvwPlayers_SteamId
            // 
            this.lvwPlayers_SteamId.Text = "SteamId";
            this.lvwPlayers_SteamId.Width = 0;
            // 
            // tpgDroppedItems
            // 
            this.tpgDroppedItems.Controls.Add(this.btnDropInventory);
            this.tpgDroppedItems.Controls.Add(this.cboDroppedItem);
            this.tpgDroppedItems.Controls.Add(this.lblDroppedPlayer);
            this.tpgDroppedItems.Controls.Add(this.cboDroppedPlayer);
            this.tpgDroppedItems.Controls.Add(this.btnCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.lblCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.cboCopyCommandDropped);
            this.tpgDroppedItems.Controls.Add(this.lblCountDropped);
            this.tpgDroppedItems.Controls.Add(this.lblDroppedItem);
            this.tpgDroppedItems.Controls.Add(this.lvwDroppedItems);
            this.tpgDroppedItems.Location = new System.Drawing.Point(4, 22);
            this.tpgDroppedItems.Name = "tpgDroppedItems";
            this.tpgDroppedItems.Size = new System.Drawing.Size(798, 232);
            this.tpgDroppedItems.TabIndex = 4;
            this.tpgDroppedItems.Text = "Dropped Items";
            this.tpgDroppedItems.UseVisualStyleBackColor = true;
            // 
            // cboDroppedItem
            // 
            this.cboDroppedItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDroppedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDroppedItem.FormattingEnabled = true;
            this.cboDroppedItem.Location = new System.Drawing.Point(480, 15);
            this.cboDroppedItem.Name = "cboDroppedItem";
            this.cboDroppedItem.Size = new System.Drawing.Size(304, 21);
            this.cboDroppedItem.TabIndex = 48;
            this.cboDroppedItem.SelectedIndexChanged += new System.EventHandler(this.cboDroppedItem_SelectedIndexChanged);
            // 
            // lblDroppedPlayer
            // 
            this.lblDroppedPlayer.AutoSize = true;
            this.lblDroppedPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroppedPlayer.Location = new System.Drawing.Point(17, 18);
            this.lblDroppedPlayer.Name = "lblDroppedPlayer";
            this.lblDroppedPlayer.Size = new System.Drawing.Size(46, 13);
            this.lblDroppedPlayer.TabIndex = 47;
            this.lblDroppedPlayer.Text = "Player:";
            // 
            // cboDroppedPlayer
            // 
            this.cboDroppedPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDroppedPlayer.FormattingEnabled = true;
            this.cboDroppedPlayer.Location = new System.Drawing.Point(69, 16);
            this.cboDroppedPlayer.Name = "cboDroppedPlayer";
            this.cboDroppedPlayer.Size = new System.Drawing.Size(278, 21);
            this.cboDroppedPlayer.TabIndex = 44;
            this.cboDroppedPlayer.SelectedIndexChanged += new System.EventHandler(this.cboDroppedPlayer_SelectedIndexChanged);
            // 
            // lblCopyCommandDropped
            // 
            this.lblCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyCommandDropped.AutoSize = true;
            this.lblCopyCommandDropped.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyCommandDropped.Location = new System.Drawing.Point(14, 204);
            this.lblCopyCommandDropped.Name = "lblCopyCommandDropped";
            this.lblCopyCommandDropped.Size = new System.Drawing.Size(65, 13);
            this.lblCopyCommandDropped.TabIndex = 42;
            this.lblCopyCommandDropped.Text = "Command:";
            // 
            // cboCopyCommandDropped
            // 
            this.cboCopyCommandDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboCopyCommandDropped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCopyCommandDropped.FormattingEnabled = true;
            this.cboCopyCommandDropped.Items.AddRange(new object[] {
            "SetPlayerPos  <x> <y> <z>"});
            this.cboCopyCommandDropped.Location = new System.Drawing.Point(82, 201);
            this.cboCopyCommandDropped.Name = "cboCopyCommandDropped";
            this.cboCopyCommandDropped.Size = new System.Drawing.Size(262, 21);
            this.cboCopyCommandDropped.TabIndex = 41;
            // 
            // lblCountDropped
            // 
            this.lblCountDropped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountDropped.BackColor = System.Drawing.Color.PowderBlue;
            this.lblCountDropped.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDropped.Location = new System.Drawing.Point(661, 197);
            this.lblCountDropped.Name = "lblCountDropped";
            this.lblCountDropped.Size = new System.Drawing.Size(123, 30);
            this.lblCountDropped.TabIndex = 40;
            this.lblCountDropped.Text = "Count: 0";
            this.lblCountDropped.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDroppedItem
            // 
            this.lblDroppedItem.AutoSize = true;
            this.lblDroppedItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroppedItem.Location = new System.Drawing.Point(439, 19);
            this.lblDroppedItem.Name = "lblDroppedItem";
            this.lblDroppedItem.Size = new System.Drawing.Size(35, 13);
            this.lblDroppedItem.TabIndex = 39;
            this.lblDroppedItem.Text = "Item:";
            // 
            // lvwDroppedItems
            // 
            this.lvwDroppedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDroppedItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwDroppedItems_Item,
            this.lvwDroppedItems_DroppedBy,
            this.lvwDroppedItems_Lat,
            this.lvwDroppedItems_Lon,
            this.lvwDroppedItems_Tribe,
            this.lvwDroppedItems_Player});
            this.lvwDroppedItems.FullRowSelect = true;
            this.lvwDroppedItems.HideSelection = false;
            this.lvwDroppedItems.Location = new System.Drawing.Point(12, 52);
            this.lvwDroppedItems.MultiSelect = false;
            this.lvwDroppedItems.Name = "lvwDroppedItems";
            this.lvwDroppedItems.Size = new System.Drawing.Size(773, 140);
            this.lvwDroppedItems.TabIndex = 38;
            this.lvwDroppedItems.UseCompatibleStateImageBehavior = false;
            this.lvwDroppedItems.View = System.Windows.Forms.View.Details;
            this.lvwDroppedItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwDroppedItems_ColumnClick);
            this.lvwDroppedItems.SelectedIndexChanged += new System.EventHandler(this.lvwDroppedItems_SelectedIndexChanged);
            // 
            // lvwDroppedItems_Item
            // 
            this.lvwDroppedItems_Item.Text = "Item";
            this.lvwDroppedItems_Item.Width = 247;
            // 
            // lvwDroppedItems_DroppedBy
            // 
            this.lvwDroppedItems_DroppedBy.Text = "Dropped By";
            this.lvwDroppedItems_DroppedBy.Width = 169;
            // 
            // lvwDroppedItems_Lat
            // 
            this.lvwDroppedItems_Lat.Text = "Lat";
            // 
            // lvwDroppedItems_Lon
            // 
            this.lvwDroppedItems_Lon.Text = "Lon";
            // 
            // lvwDroppedItems_Tribe
            // 
            this.lvwDroppedItems_Tribe.Text = "Tribe";
            this.lvwDroppedItems_Tribe.Width = 105;
            // 
            // lvwDroppedItems_Player
            // 
            this.lvwDroppedItems_Player.Text = "Player";
            this.lvwDroppedItems_Player.Width = 109;
            // 
            // pnlStructures
            // 
            this.pnlStructures.BackColor = System.Drawing.Color.CadetBlue;
            this.pnlStructures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStructures.Controls.Add(this.picGlitches);
            this.pnlStructures.Controls.Add(this.chkGlitches);
            this.pnlStructures.Controls.Add(this.chkMagmasaurNests);
            this.pnlStructures.Controls.Add(this.picMagmasaurNests);
            this.pnlStructures.Controls.Add(this.chkDrakeNests);
            this.pnlStructures.Controls.Add(this.picDrakeNests);
            this.pnlStructures.Controls.Add(this.picChargeNodes);
            this.pnlStructures.Controls.Add(this.chkChargeNodes);
            this.pnlStructures.Controls.Add(this.picArtifacts);
            this.pnlStructures.Controls.Add(this.picBeaverDams);
            this.pnlStructures.Controls.Add(this.chkArtifacts);
            this.pnlStructures.Controls.Add(this.chkBeaverDams);
            this.pnlStructures.Controls.Add(this.picWyvernNests);
            this.pnlStructures.Controls.Add(this.chkDeinoNests);
            this.pnlStructures.Controls.Add(this.chkWyvernNests);
            this.pnlStructures.Controls.Add(this.picDeinoNests);
            this.pnlStructures.Controls.Add(this.picObelisks);
            this.pnlStructures.Controls.Add(this.chkGasVeins);
            this.pnlStructures.Controls.Add(this.chkObelisks);
            this.pnlStructures.Controls.Add(this.picGasVeins);
            this.pnlStructures.Controls.Add(this.picWaterVeins);
            this.pnlStructures.Controls.Add(this.chkOilVeins);
            this.pnlStructures.Controls.Add(this.chkWaterVeins);
            this.pnlStructures.Controls.Add(this.picOilVeins);
            this.pnlStructures.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStructures.Location = new System.Drawing.Point(12, 34);
            this.pnlStructures.Name = "pnlStructures";
            this.pnlStructures.Size = new System.Drawing.Size(437, 251);
            this.pnlStructures.TabIndex = 17;
            // 
            // chkGlitches
            // 
            this.chkGlitches.AutoSize = true;
            this.chkGlitches.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGlitches.Location = new System.Drawing.Point(283, 217);
            this.chkGlitches.Name = "chkGlitches";
            this.chkGlitches.Size = new System.Drawing.Size(64, 17);
            this.chkGlitches.TabIndex = 26;
            this.chkGlitches.Text = "Glitches";
            this.chkGlitches.UseVisualStyleBackColor = true;
            this.chkGlitches.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // chkMagmasaurNests
            // 
            this.chkMagmasaurNests.AutoSize = true;
            this.chkMagmasaurNests.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMagmasaurNests.Location = new System.Drawing.Point(236, 139);
            this.chkMagmasaurNests.Name = "chkMagmasaurNests";
            this.chkMagmasaurNests.Size = new System.Drawing.Size(111, 17);
            this.chkMagmasaurNests.TabIndex = 24;
            this.chkMagmasaurNests.Text = "Magmasaur Nests";
            this.chkMagmasaurNests.UseVisualStyleBackColor = true;
            this.chkMagmasaurNests.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // chkDrakeNests
            // 
            this.chkDrakeNests.AutoSize = true;
            this.chkDrakeNests.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDrakeNests.Location = new System.Drawing.Point(233, 99);
            this.chkDrakeNests.Name = "chkDrakeNests";
            this.chkDrakeNests.Size = new System.Drawing.Size(114, 17);
            this.chkDrakeNests.TabIndex = 22;
            this.chkDrakeNests.Text = "Rock Drake Nests";
            this.chkDrakeNests.UseVisualStyleBackColor = true;
            this.chkDrakeNests.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // chkChargeNodes
            // 
            this.chkChargeNodes.AutoSize = true;
            this.chkChargeNodes.Location = new System.Drawing.Point(85, 178);
            this.chkChargeNodes.Name = "chkChargeNodes";
            this.chkChargeNodes.Size = new System.Drawing.Size(94, 17);
            this.chkChargeNodes.TabIndex = 16;
            this.chkChargeNodes.Text = "Charge Nodes";
            this.chkChargeNodes.UseVisualStyleBackColor = true;
            this.chkChargeNodes.CheckedChanged += new System.EventHandler(this.Structure_CheckedChanged);
            // 
            // lblStructureMarkers
            // 
            this.lblStructureMarkers.AutoSize = true;
            this.lblStructureMarkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStructureMarkers.Location = new System.Drawing.Point(9, 16);
            this.lblStructureMarkers.Name = "lblStructureMarkers";
            this.lblStructureMarkers.Size = new System.Drawing.Size(93, 13);
            this.lblStructureMarkers.TabIndex = 18;
            this.lblStructureMarkers.Text = "Map Structures";
            // 
            // chkApplyFilterMarkers
            // 
            this.chkApplyFilterMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilterMarkers.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilterMarkers.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilterMarkers.Location = new System.Drawing.Point(748, 258);
            this.chkApplyFilterMarkers.Name = "chkApplyFilterMarkers";
            this.chkApplyFilterMarkers.Size = new System.Drawing.Size(30, 30);
            this.chkApplyFilterMarkers.TabIndex = 21;
            this.chkApplyFilterMarkers.UseVisualStyleBackColor = true;
            this.chkApplyFilterMarkers.CheckedChanged += new System.EventHandler(this.chkApplyFilterMarkers_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.BackColor = System.Drawing.Color.PowderBlue;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblStatus.Location = new System.Drawing.Point(15, 561);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(571, 22);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "Loading...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.splitContainer1.Panel1.Controls.Add(this.lblMapDate);
            this.splitContainer1.Panel1.Controls.Add(this.pnlStructures);
            this.splitContainer1.Panel1.Controls.Add(this.lvwMapMarkers);
            this.splitContainer1.Panel1.Controls.Add(this.lblStatus);
            this.splitContainer1.Panel1.Controls.Add(this.lblMapMarkerHeader);
            this.splitContainer1.Panel1.Controls.Add(this.btnRemoveMarker);
            this.splitContainer1.Panel1.Controls.Add(this.chkApplyFilterMarkers);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddMarker);
            this.splitContainer1.Panel1.Controls.Add(this.btnEditMarker);
            this.splitContainer1.Panel1.Controls.Add(this.lblStructureMarkers);
            this.splitContainer1.Panel1.Controls.Add(this.txtMarkerFilter);
            this.splitContainer1.Panel1.Controls.Add(this.tabFeatures);
            this.splitContainer1.Panel1MinSize = 830;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.splitContainer1.Panel2.Controls.Add(this.pnlMap);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1387, 587);
            this.splitContainer1.SplitterDistance = 830;
            this.splitContainer1.TabIndex = 23;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // frmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(1391, 587);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ARK Save Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmViewer_FormClosed);
            this.mnuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.pnlMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picArtifacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWyvernNests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObelisks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaterVeins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOilVeins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGasVeins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeinoNests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBeaverDams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picChargeNodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDrakeNests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMagmasaurNests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGlitches)).EndInit();
            this.tabFeatures.ResumeLayout(false);
            this.tpgWild.ResumeLayout(false);
            this.tpgWild.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWildRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udWildMax)).EndInit();
            this.tpgTamed.ResumeLayout(false);
            this.tpgTamed.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tpgStructures.ResumeLayout(false);
            this.tpgStructures.PerformLayout();
            this.tpgTribes.ResumeLayout(false);
            this.tpgTribes.PerformLayout();
            this.tpgPlayers.ResumeLayout(false);
            this.tpgPlayers.PerformLayout();
            this.tpgDroppedItems.ResumeLayout(false);
            this.tpgDroppedItems.PerformLayout();
            this.pnlStructures.ResumeLayout(false);
            this.pnlStructures.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lvwWildDetail;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Name;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Sex;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Base;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_HP;
        private System.Windows.Forms.Label lblWildTotal;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Stam;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Melee;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Weight;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Speed;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Food;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Craft;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Lat;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Lon;
        private System.Windows.Forms.Label lblMapDate;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Level;
        private System.Windows.Forms.ComboBox cboWildClass;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.TrackBar trackZoom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvwMapMarkers;
        private System.Windows.Forms.Label lblMapMarkerHeader;
        private System.Windows.Forms.Button btnRemoveMarker;
        private System.Windows.Forms.Button btnAddMarker;
        private System.Windows.Forms.Button btnEditMarker;
        private System.Windows.Forms.PictureBox picWyvernNests;
        private System.Windows.Forms.CheckBox chkWyvernNests;
        private System.Windows.Forms.PictureBox picObelisks;
        private System.Windows.Forms.CheckBox chkObelisks;
        private System.Windows.Forms.PictureBox picWaterVeins;
        private System.Windows.Forms.CheckBox chkWaterVeins;
        private System.Windows.Forms.PictureBox picOilVeins;
        private System.Windows.Forms.CheckBox chkOilVeins;
        private System.Windows.Forms.PictureBox picGasVeins;
        private System.Windows.Forms.CheckBox chkGasVeins;
        private System.Windows.Forms.PictureBox picDeinoNests;
        private System.Windows.Forms.CheckBox chkDeinoNests;
        private System.Windows.Forms.PictureBox picBeaverDams;
        private System.Windows.Forms.CheckBox chkBeaverDams;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Name;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Lat;
        private System.Windows.Forms.ColumnHeader lvwMapMarkers_Lon;
        private System.Windows.Forms.PictureBox picArtifacts;
        private System.Windows.Forms.CheckBox chkArtifacts;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtMarkerFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnZoomMinus;
        private System.Windows.Forms.TabControl tabFeatures;
        private System.Windows.Forms.TabPage tpgWild;
        private System.Windows.Forms.TabPage tpgPlayers;
        private System.Windows.Forms.Panel pnlStructures;
        private System.Windows.Forms.Label lblStructureMarkers;
        private System.Windows.Forms.Label lblPlayersPlayer;
        private System.Windows.Forms.Label lblPlayersTribe;
        private System.Windows.Forms.ComboBox cboTribes;
        private System.Windows.Forms.ComboBox cboPlayers;
        private System.Windows.Forms.ListView lvwPlayers;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Name;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Sex;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Level;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Lat;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Lon;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Hp;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Stam;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Melee;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Weight;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Speed;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Food;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwPlayers_LastOnline;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Water;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Tribe;
        private System.Windows.Forms.Button btnPlayerInventory;
        private System.Windows.Forms.PictureBox picChargeNodes;
        private System.Windows.Forms.CheckBox chkChargeNodes;
        private System.Windows.Forms.CheckBox chkApplyFilterMarkers;
        private System.Windows.Forms.CheckBox chkDrakeNests;
        private System.Windows.Forms.PictureBox picDrakeNests;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Crafting;
        private System.Windows.Forms.ColumnHeader lvwPlayers_Fortitude;
        private System.Windows.Forms.Button btnPlayerTribeLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabPage tpgStructures;
        private System.Windows.Forms.Label lblStructurePlayer;
        private System.Windows.Forms.Label lblStructureTribe;
        private System.Windows.Forms.ComboBox cboStructureTribe;
        private System.Windows.Forms.ComboBox cboStructurePlayer;
        private System.Windows.Forms.ListView lvwStructureLocations;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Player;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Tribe;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Structure;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Lat;
        private System.Windows.Forms.ColumnHeader lvwStructureLocations_Lon;
        private System.Windows.Forms.Label lblStructureStructure;
        private System.Windows.Forms.ComboBox cboStructureStructure;
        private System.Windows.Forms.Button btnZoomPlus;
        private System.Windows.Forms.Button btnStructureExclusionFilter;
        private System.Windows.Forms.Label lblCommandPlayer;
        private System.Windows.Forms.ComboBox cboConsoleCommandsPlayerTribe;
        private System.Windows.Forms.Button btnCopyCommandPlayer;
        private System.Windows.Forms.Button btnCopyCommandStructure;
        private System.Windows.Forms.Label lblCommandStructure;
        private System.Windows.Forms.ComboBox cboConsoleCommandsStructure;
        private System.Windows.Forms.TabPage tpgTamed;
        private System.Windows.Forms.Button btnDinoAncestors;
        private System.Windows.Forms.Button btnDinoInventory;
        private System.Windows.Forms.ListView lvwTameDetail;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Name;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Sex;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Base;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Level;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Lat;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Lon;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_HP;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Stam;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Melee;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Weight;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Speed;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Food;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Oxygen;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Craft;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Tamer;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Imprinter;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Imprint;
        private System.Windows.Forms.Label lblTameTotal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.RadioButton optStatsTamed;
        private System.Windows.Forms.RadioButton optStatsBase;
        private System.Windows.Forms.ComboBox cboTameClass;
        private System.Windows.Forms.ComboBox cboTameTribes;
        private System.Windows.Forms.ComboBox cboTamePlayers;
        private System.Windows.Forms.Label lblTameCreature;
        private System.Windows.Forms.Label lblTamePlayer;
        private System.Windows.Forms.Label lblTameTribe;
        private System.Windows.Forms.Label lblStructureTotal;
        private System.Windows.Forms.Label lblPlayerTotal;
        private System.Windows.Forms.Label lblWildClass;
        private System.Windows.Forms.Label lblSelectedWildTotal;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Creature;
        private System.Windows.Forms.Button btnCopyCommandWild;
        private System.Windows.Forms.Label lblWildCommand;
        private System.Windows.Forms.ComboBox cboConsoleCommandsWild;
        private System.Windows.Forms.Button btnCopyCommandTamed;
        private System.Windows.Forms.Label lblTamedCommand;
        private System.Windows.Forms.ComboBox cboConsoleCommandsTamed;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_PlayerId;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_SteamId;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_TribeId;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Cryo;
        private System.Windows.Forms.CheckBox chkCryo;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour1;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour2;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour3;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour4;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour5;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Colour6;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour1;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour2;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour3;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour4;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour5;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Colour6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tpgDroppedItems;
        private System.Windows.Forms.Button btnCopyCommandDropped;
        private System.Windows.Forms.Label lblCopyCommandDropped;
        private System.Windows.Forms.ComboBox cboCopyCommandDropped;
        private System.Windows.Forms.Label lblCountDropped;
        private System.Windows.Forms.Label lblDroppedItem;
        private System.Windows.Forms.ListView lvwDroppedItems;
        private System.Windows.Forms.ComboBox cboDroppedItem;
        private System.Windows.Forms.Label lblDroppedPlayer;
        private System.Windows.Forms.ComboBox cboDroppedPlayer;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Item;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_DroppedBy;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Lat;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Lon;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Tribe;
        private System.Windows.Forms.ColumnHeader lvwDroppedItems_Player;
        private System.Windows.Forms.Label lblWildMin;
        private System.Windows.Forms.Label lblWildMax;
        private System.Windows.Forms.NumericUpDown udWildMin;
        private System.Windows.Forms.NumericUpDown udWildMax;
        private System.Windows.Forms.TabPage tpgTribes;
        private System.Windows.Forms.Button btnTribeCopyCommand;
        private System.Windows.Forms.Label lblTribeCopyCommand;
        private System.Windows.Forms.ComboBox cboTribeCopyCommand;
        private System.Windows.Forms.Button btnTribeLog;
        private System.Windows.Forms.ListView lvwTribes;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.CheckBox chkTribeStructures;
        private System.Windows.Forms.CheckBox chkTribeTames;
        private System.Windows.Forms.CheckBox chkTribePlayers;
        private System.Windows.Forms.CheckBox chkMagmasaurNests;
        private System.Windows.Forms.PictureBox picMagmasaurNests;
        private System.Windows.Forms.Label lblWildRadius;
        private System.Windows.Forms.NumericUpDown udWildRadius;
        private System.Windows.Forms.Label lblWildLon;
        private System.Windows.Forms.NumericUpDown udWildLon;
        private System.Windows.Forms.Label lblWildLat;
        private System.Windows.Forms.NumericUpDown udWildLat;
        private System.Windows.Forms.PictureBox picGlitches;
        private System.Windows.Forms.CheckBox chkGlitches;
        private System.Windows.Forms.Button btnStructureInventory;
        private System.Windows.Forms.Button btnDeletePlayer;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Export;
        private System.Windows.Forms.ColumnHeader lvwPlayers_SteamName;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_MutationsFemale;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_MutationsMale;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Server;
        private System.Windows.Forms.ColumnHeader lvwWildDetail_Id;
        private System.Windows.Forms.ColumnHeader lvwTameDetail_Id;
        private System.Windows.Forms.ColumnHeader lvwPlayers_SteamId;
        private System.Windows.Forms.Button btnDropInventory;
    }
}

