namespace ARKViewer
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tpgMap = new System.Windows.Forms.TabPage();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.optFtpModeSftp = new System.Windows.Forms.RadioButton();
            this.optFtpModeFtp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFtpMap = new System.Windows.Forms.Label();
            this.cboFtpMap = new System.Windows.Forms.ComboBox();
            this.btnServerExport = new System.Windows.Forms.Button();
            this.btnServerImport = new System.Windows.Forms.Button();
            this.chkPasswordVisibility = new System.Windows.Forms.CheckBox();
            this.udFTPPort = new System.Windows.Forms.NumericUpDown();
            this.pnlServerDetails = new System.Windows.Forms.Panel();
            this.txtFTPFilePath = new System.Windows.Forms.TextBox();
            this.lblFtpFilePath = new System.Windows.Forms.Label();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.lblFTPServer = new System.Windows.Forms.Label();
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.txtFTPUsername = new System.Windows.Forms.TextBox();
            this.lblFTPPassword = new System.Windows.Forms.Label();
            this.lblFTPUsername = new System.Windows.Forms.Label();
            this.txtFTPAddress = new System.Windows.Forms.TextBox();
            this.lblFTPPort = new System.Windows.Forms.Label();
            this.lblFTPHost = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.cboFTPServer = new System.Windows.Forms.ComboBox();
            this.grpOffline = new System.Windows.Forms.GroupBox();
            this.chkUpdateNotificationFile = new System.Windows.Forms.CheckBox();
            this.btnSelectSaveGame = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.lblOfflineSave = new System.Windows.Forms.Label();
            this.grpSinglePlayer = new System.Windows.Forms.GroupBox();
            this.chkUpdateNotificationSingle = new System.Windows.Forms.CheckBox();
            this.lblSelectedMapSP = new System.Windows.Forms.Label();
            this.cboMapSinglePlayer = new System.Windows.Forms.ComboBox();
            this.optOffline = new System.Windows.Forms.RadioButton();
            this.optSinglePlayer = new System.Windows.Forms.RadioButton();
            this.optServer = new System.Windows.Forms.RadioButton();
            this.tpgColours = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lvwColours = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgCreatures = new System.Windows.Forms.TabPage();
            this.chkApplyFilterDinos = new System.Windows.Forms.CheckBox();
            this.txtCreatureFilter = new System.Windows.Forms.TextBox();
            this.btnEditDinoClass = new System.Windows.Forms.Button();
            this.btnRemoveDinoClass = new System.Windows.Forms.Button();
            this.btnAddDinoClass = new System.Windows.Forms.Button();
            this.lvwDinoClasses = new System.Windows.Forms.ListView();
            this.lvwDinoClasses_ClassName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvwDinoClasses_DisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgStructures = new System.Windows.Forms.TabPage();
            this.chkApplyFilterStructures = new System.Windows.Forms.CheckBox();
            this.txtStructureFilter = new System.Windows.Forms.TextBox();
            this.btnEditStructure = new System.Windows.Forms.Button();
            this.btnRemoveStructure = new System.Windows.Forms.Button();
            this.btnAddStructure = new System.Windows.Forms.Button();
            this.lvwStructureMap = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tpgItems = new System.Windows.Forms.TabPage();
            this.chkApplyFilterItems = new System.Windows.Forms.CheckBox();
            this.txtItemFilter = new System.Windows.Forms.TextBox();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lvwItemMap = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpgPlayers = new System.Windows.Forms.TabPage();
            this.pnlPlayerSettingsCommands = new System.Windows.Forms.Panel();
            this.optPlayerCommandsPrefixAdmincheat = new System.Windows.Forms.RadioButton();
            this.optPlayerCommandsPrefixNone = new System.Windows.Forms.RadioButton();
            this.optPlayerCommandsPrefixCheat = new System.Windows.Forms.RadioButton();
            this.lblOptionHeaderCommand = new System.Windows.Forms.Label();
            this.lblOptionTextCommand = new System.Windows.Forms.Label();
            this.pnlPlayerSettingsBody = new System.Windows.Forms.Panel();
            this.optPlayerBodyHide = new System.Windows.Forms.RadioButton();
            this.optPlayerBodyShow = new System.Windows.Forms.RadioButton();
            this.lblOptionHeaderBody = new System.Windows.Forms.Label();
            this.lblOptionTextBody = new System.Windows.Forms.Label();
            this.pnlPlayerSettingsTames = new System.Windows.Forms.Panel();
            this.optPlayerTameHide = new System.Windows.Forms.RadioButton();
            this.optPlayerTameShow = new System.Windows.Forms.RadioButton();
            this.lblOptionHeaderTames = new System.Windows.Forms.Label();
            this.lblOptionTextTames = new System.Windows.Forms.Label();
            this.pnlPlayerSettingsStuctures = new System.Windows.Forms.Panel();
            this.optPlayerStructureHide = new System.Windows.Forms.RadioButton();
            this.optPlayerStructureShow = new System.Windows.Forms.RadioButton();
            this.lblOptionHeaderStructures = new System.Windows.Forms.Label();
            this.lblOptionTextStructures = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlFtpSettingsCommands = new System.Windows.Forms.Panel();
            this.optFTPSync = new System.Windows.Forms.RadioButton();
            this.optFTPClean = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabSettings.SuspendLayout();
            this.tpgMap.SuspendLayout();
            this.grpServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFTPPort)).BeginInit();
            this.grpOffline.SuspendLayout();
            this.grpSinglePlayer.SuspendLayout();
            this.tpgColours.SuspendLayout();
            this.tpgCreatures.SuspendLayout();
            this.tpgStructures.SuspendLayout();
            this.tpgItems.SuspendLayout();
            this.tpgPlayers.SuspendLayout();
            this.pnlPlayerSettingsCommands.SuspendLayout();
            this.pnlPlayerSettingsBody.SuspendLayout();
            this.pnlPlayerSettingsTames.SuspendLayout();
            this.pnlPlayerSettingsStuctures.SuspendLayout();
            this.pnlFtpSettingsCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(405, 672);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(486, 672);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.ark";
            this.openFileDialog1.Filter = "ARK SaveGame|*.ark";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Open ARK Save Game";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tpgMap);
            this.tabSettings.Controls.Add(this.tpgColours);
            this.tabSettings.Controls.Add(this.tpgCreatures);
            this.tabSettings.Controls.Add(this.tpgStructures);
            this.tabSettings.Controls.Add(this.tpgItems);
            this.tabSettings.Controls.Add(this.tpgPlayers);
            this.tabSettings.Location = new System.Drawing.Point(12, 12);
            this.tabSettings.Multiline = true;
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(553, 652);
            this.tabSettings.TabIndex = 8;
            // 
            // tpgMap
            // 
            this.tpgMap.Controls.Add(this.grpServer);
            this.tpgMap.Controls.Add(this.grpOffline);
            this.tpgMap.Controls.Add(this.grpSinglePlayer);
            this.tpgMap.Controls.Add(this.optOffline);
            this.tpgMap.Controls.Add(this.optSinglePlayer);
            this.tpgMap.Controls.Add(this.optServer);
            this.tpgMap.Location = new System.Drawing.Point(4, 22);
            this.tpgMap.Name = "tpgMap";
            this.tpgMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpgMap.Size = new System.Drawing.Size(545, 626);
            this.tpgMap.TabIndex = 0;
            this.tpgMap.Text = "Map Settings";
            this.tpgMap.UseVisualStyleBackColor = true;
            // 
            // grpServer
            // 
            this.grpServer.Controls.Add(this.optFtpModeSftp);
            this.grpServer.Controls.Add(this.optFtpModeFtp);
            this.grpServer.Controls.Add(this.label1);
            this.grpServer.Controls.Add(this.lblFtpMap);
            this.grpServer.Controls.Add(this.cboFtpMap);
            this.grpServer.Controls.Add(this.btnServerExport);
            this.grpServer.Controls.Add(this.btnServerImport);
            this.grpServer.Controls.Add(this.chkPasswordVisibility);
            this.grpServer.Controls.Add(this.udFTPPort);
            this.grpServer.Controls.Add(this.pnlServerDetails);
            this.grpServer.Controls.Add(this.txtFTPFilePath);
            this.grpServer.Controls.Add(this.lblFtpFilePath);
            this.grpServer.Controls.Add(this.btnRemoveServer);
            this.grpServer.Controls.Add(this.btnAddServer);
            this.grpServer.Controls.Add(this.lblFTPServer);
            this.grpServer.Controls.Add(this.txtFTPPassword);
            this.grpServer.Controls.Add(this.txtFTPUsername);
            this.grpServer.Controls.Add(this.lblFTPPassword);
            this.grpServer.Controls.Add(this.lblFTPUsername);
            this.grpServer.Controls.Add(this.txtFTPAddress);
            this.grpServer.Controls.Add(this.lblFTPPort);
            this.grpServer.Controls.Add(this.lblFTPHost);
            this.grpServer.Controls.Add(this.txtServerName);
            this.grpServer.Controls.Add(this.cboFTPServer);
            this.grpServer.Location = new System.Drawing.Point(38, 248);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(471, 358);
            this.grpServer.TabIndex = 11;
            this.grpServer.TabStop = false;
            // 
            // optFtpModeSftp
            // 
            this.optFtpModeSftp.AutoSize = true;
            this.optFtpModeSftp.Enabled = false;
            this.optFtpModeSftp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFtpModeSftp.Location = new System.Drawing.Point(89, 105);
            this.optFtpModeSftp.Name = "optFtpModeSftp";
            this.optFtpModeSftp.Size = new System.Drawing.Size(52, 17);
            this.optFtpModeSftp.TabIndex = 23;
            this.optFtpModeSftp.Text = "SFTP";
            this.optFtpModeSftp.UseVisualStyleBackColor = true;
            // 
            // optFtpModeFtp
            // 
            this.optFtpModeFtp.AutoSize = true;
            this.optFtpModeFtp.Checked = true;
            this.optFtpModeFtp.Enabled = false;
            this.optFtpModeFtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFtpModeFtp.Location = new System.Drawing.Point(25, 105);
            this.optFtpModeFtp.Name = "optFtpModeFtp";
            this.optFtpModeFtp.Size = new System.Drawing.Size(45, 17);
            this.optFtpModeFtp.TabIndex = 22;
            this.optFtpModeFtp.TabStop = true;
            this.optFtpModeFtp.Text = "FTP";
            this.optFtpModeFtp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 22);
            this.label1.TabIndex = 21;
            this.label1.Text = "Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFtpMap
            // 
            this.lblFtpMap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFtpMap.BackColor = System.Drawing.SystemColors.Control;
            this.lblFtpMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFtpMap.Location = new System.Drawing.Point(22, 291);
            this.lblFtpMap.Name = "lblFtpMap";
            this.lblFtpMap.Size = new System.Drawing.Size(115, 22);
            this.lblFtpMap.TabIndex = 20;
            this.lblFtpMap.Text = "Map";
            this.lblFtpMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboFtpMap
            // 
            this.cboFtpMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFtpMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpMap.Enabled = false;
            this.cboFtpMap.FormattingEnabled = true;
            this.cboFtpMap.Location = new System.Drawing.Point(25, 321);
            this.cboFtpMap.Name = "cboFtpMap";
            this.cboFtpMap.Size = new System.Drawing.Size(422, 21);
            this.cboFtpMap.TabIndex = 19;
            this.cboFtpMap.SelectedIndexChanged += new System.EventHandler(this.cboFtpMap_SelectedIndexChanged);
            // 
            // btnServerExport
            // 
            this.btnServerExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServerExport.Image = global::ARKViewer.Properties.Resources.button_export;
            this.btnServerExport.Location = new System.Drawing.Point(414, 38);
            this.btnServerExport.Name = "btnServerExport";
            this.btnServerExport.Size = new System.Drawing.Size(33, 27);
            this.btnServerExport.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnServerExport, "Export server");
            this.btnServerExport.UseVisualStyleBackColor = true;
            this.btnServerExport.Click += new System.EventHandler(this.btnServerExport_Click);
            // 
            // btnServerImport
            // 
            this.btnServerImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnServerImport.Image = global::ARKViewer.Properties.Resources.button_import;
            this.btnServerImport.Location = new System.Drawing.Point(378, 38);
            this.btnServerImport.Name = "btnServerImport";
            this.btnServerImport.Size = new System.Drawing.Size(33, 27);
            this.btnServerImport.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnServerImport, "Import server");
            this.btnServerImport.UseVisualStyleBackColor = true;
            this.btnServerImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // chkPasswordVisibility
            // 
            this.chkPasswordVisibility.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPasswordVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPasswordVisibility.Image = ((System.Drawing.Image)(resources.GetObject("chkPasswordVisibility.Image")));
            this.chkPasswordVisibility.Location = new System.Drawing.Point(427, 203);
            this.chkPasswordVisibility.Name = "chkPasswordVisibility";
            this.chkPasswordVisibility.Size = new System.Drawing.Size(20, 20);
            this.chkPasswordVisibility.TabIndex = 15;
            this.chkPasswordVisibility.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.chkPasswordVisibility, "Reveal Password");
            this.chkPasswordVisibility.UseVisualStyleBackColor = false;
            this.chkPasswordVisibility.CheckedChanged += new System.EventHandler(this.chkPasswordVisibility_CheckedChanged);
            // 
            // udFTPPort
            // 
            this.udFTPPort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.udFTPPort.Enabled = false;
            this.udFTPPort.Location = new System.Drawing.Point(372, 150);
            this.udFTPPort.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.udFTPPort.Name = "udFTPPort";
            this.udFTPPort.Size = new System.Drawing.Size(75, 20);
            this.udFTPPort.TabIndex = 10;
            this.udFTPPort.Value = new decimal(new int[] {
            8821,
            0,
            0,
            0});
            // 
            // pnlServerDetails
            // 
            this.pnlServerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlServerDetails.BackColor = System.Drawing.Color.Silver;
            this.pnlServerDetails.Location = new System.Drawing.Point(17, 70);
            this.pnlServerDetails.Name = "pnlServerDetails";
            this.pnlServerDetails.Size = new System.Drawing.Size(433, 3);
            this.pnlServerDetails.TabIndex = 6;
            // 
            // txtFTPFilePath
            // 
            this.txtFTPFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFTPFilePath.Enabled = false;
            this.txtFTPFilePath.Location = new System.Drawing.Point(25, 261);
            this.txtFTPFilePath.Name = "txtFTPFilePath";
            this.txtFTPFilePath.Size = new System.Drawing.Size(422, 20);
            this.txtFTPFilePath.TabIndex = 17;
            // 
            // lblFtpFilePath
            // 
            this.lblFtpFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFtpFilePath.BackColor = System.Drawing.SystemColors.Control;
            this.lblFtpFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFtpFilePath.Location = new System.Drawing.Point(22, 233);
            this.lblFtpFilePath.Name = "lblFtpFilePath";
            this.lblFtpFilePath.Size = new System.Drawing.Size(115, 22);
            this.lblFtpFilePath.TabIndex = 16;
            this.lblFtpFilePath.Text = "Save path";
            this.lblFtpFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveServer.Enabled = false;
            this.btnRemoveServer.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveServer.Location = new System.Drawing.Point(333, 38);
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Size = new System.Drawing.Size(34, 27);
            this.btnRemoveServer.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnRemoveServer, "Remove selected server");
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // btnAddServer
            // 
            this.btnAddServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddServer.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddServer.Location = new System.Drawing.Point(296, 38);
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.Size = new System.Drawing.Size(34, 27);
            this.btnAddServer.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAddServer, "Add new server");
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // lblFTPServer
            // 
            this.lblFTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFTPServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFTPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPServer.Location = new System.Drawing.Point(-1, 0);
            this.lblFTPServer.Name = "lblFTPServer";
            this.lblFTPServer.Size = new System.Drawing.Size(474, 22);
            this.lblFTPServer.TabIndex = 0;
            this.lblFTPServer.Text = "    Server          ";
            this.lblFTPServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFTPPassword.Enabled = false;
            this.txtFTPPassword.Location = new System.Drawing.Point(233, 203);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.PasswordChar = '●';
            this.txtFTPPassword.Size = new System.Drawing.Size(214, 20);
            this.txtFTPPassword.TabIndex = 14;
            // 
            // txtFTPUsername
            // 
            this.txtFTPUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFTPUsername.Enabled = false;
            this.txtFTPUsername.Location = new System.Drawing.Point(25, 203);
            this.txtFTPUsername.Name = "txtFTPUsername";
            this.txtFTPUsername.Size = new System.Drawing.Size(197, 20);
            this.txtFTPUsername.TabIndex = 12;
            // 
            // lblFTPPassword
            // 
            this.lblFTPPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFTPPassword.AutoSize = true;
            this.lblFTPPassword.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPPassword.Location = new System.Drawing.Point(230, 181);
            this.lblFTPPassword.Name = "lblFTPPassword";
            this.lblFTPPassword.Size = new System.Drawing.Size(61, 15);
            this.lblFTPPassword.TabIndex = 13;
            this.lblFTPPassword.Text = "Password";
            // 
            // lblFTPUsername
            // 
            this.lblFTPUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFTPUsername.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPUsername.Location = new System.Drawing.Point(22, 177);
            this.lblFTPUsername.Name = "lblFTPUsername";
            this.lblFTPUsername.Size = new System.Drawing.Size(74, 22);
            this.lblFTPUsername.TabIndex = 11;
            this.lblFTPUsername.Text = "Username";
            this.lblFTPUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFTPAddress
            // 
            this.txtFTPAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFTPAddress.Enabled = false;
            this.txtFTPAddress.Location = new System.Drawing.Point(25, 150);
            this.txtFTPAddress.Name = "txtFTPAddress";
            this.txtFTPAddress.Size = new System.Drawing.Size(333, 20);
            this.txtFTPAddress.TabIndex = 8;
            // 
            // lblFTPPort
            // 
            this.lblFTPPort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFTPPort.AutoSize = true;
            this.lblFTPPort.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPPort.Location = new System.Drawing.Point(369, 129);
            this.lblFTPPort.Name = "lblFTPPort";
            this.lblFTPPort.Size = new System.Drawing.Size(29, 15);
            this.lblFTPPort.TabIndex = 9;
            this.lblFTPPort.Text = "Port";
            // 
            // lblFTPHost
            // 
            this.lblFTPHost.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFTPHost.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPHost.Location = new System.Drawing.Point(22, 127);
            this.lblFTPHost.Name = "lblFTPHost";
            this.lblFTPHost.Size = new System.Drawing.Size(329, 19);
            this.lblFTPHost.TabIndex = 7;
            this.lblFTPHost.Text = "Server Address                        ";
            this.lblFTPHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerName.Location = new System.Drawing.Point(25, 42);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(264, 20);
            this.txtServerName.TabIndex = 2;
            this.txtServerName.Visible = false;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            this.txtServerName.Validating += new System.ComponentModel.CancelEventHandler(this.txtServerName_Validating);
            // 
            // cboFTPServer
            // 
            this.cboFTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFTPServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFTPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFTPServer.FormattingEnabled = true;
            this.cboFTPServer.Location = new System.Drawing.Point(25, 40);
            this.cboFTPServer.Name = "cboFTPServer";
            this.cboFTPServer.Size = new System.Drawing.Size(264, 24);
            this.cboFTPServer.TabIndex = 1;
            this.cboFTPServer.SelectedIndexChanged += new System.EventHandler(this.cboFTPServer_SelectedIndexChanged);
            // 
            // grpOffline
            // 
            this.grpOffline.Controls.Add(this.chkUpdateNotificationFile);
            this.grpOffline.Controls.Add(this.btnSelectSaveGame);
            this.grpOffline.Controls.Add(this.txtFilename);
            this.grpOffline.Controls.Add(this.lblOfflineSave);
            this.grpOffline.Location = new System.Drawing.Point(37, 136);
            this.grpOffline.Name = "grpOffline";
            this.grpOffline.Size = new System.Drawing.Size(471, 83);
            this.grpOffline.TabIndex = 9;
            this.grpOffline.TabStop = false;
            // 
            // chkUpdateNotificationFile
            // 
            this.chkUpdateNotificationFile.AutoSize = true;
            this.chkUpdateNotificationFile.Enabled = false;
            this.chkUpdateNotificationFile.Location = new System.Drawing.Point(18, 63);
            this.chkUpdateNotificationFile.Name = "chkUpdateNotificationFile";
            this.chkUpdateNotificationFile.Size = new System.Drawing.Size(120, 17);
            this.chkUpdateNotificationFile.TabIndex = 3;
            this.chkUpdateNotificationFile.Text = "Update notifications";
            this.chkUpdateNotificationFile.UseVisualStyleBackColor = true;
            // 
            // btnSelectSaveGame
            // 
            this.btnSelectSaveGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSaveGame.Image = global::ARKViewer.Properties.Resources.button_folder;
            this.btnSelectSaveGame.Location = new System.Drawing.Point(415, 34);
            this.btnSelectSaveGame.Name = "btnSelectSaveGame";
            this.btnSelectSaveGame.Size = new System.Drawing.Size(33, 27);
            this.btnSelectSaveGame.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnSelectSaveGame, "Open ARK save file");
            this.btnSelectSaveGame.UseVisualStyleBackColor = true;
            this.btnSelectSaveGame.Click += new System.EventHandler(this.btnSelectSaveGame_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(18, 36);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(391, 22);
            this.txtFilename.TabIndex = 1;
            // 
            // lblOfflineSave
            // 
            this.lblOfflineSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOfflineSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOfflineSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfflineSave.Location = new System.Drawing.Point(-2, 6);
            this.lblOfflineSave.Name = "lblOfflineSave";
            this.lblOfflineSave.Size = new System.Drawing.Size(474, 22);
            this.lblOfflineSave.TabIndex = 0;
            this.lblOfflineSave.Text = "    Selected Save";
            this.lblOfflineSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpSinglePlayer
            // 
            this.grpSinglePlayer.Controls.Add(this.chkUpdateNotificationSingle);
            this.grpSinglePlayer.Controls.Add(this.lblSelectedMapSP);
            this.grpSinglePlayer.Controls.Add(this.cboMapSinglePlayer);
            this.grpSinglePlayer.Location = new System.Drawing.Point(36, 30);
            this.grpSinglePlayer.Name = "grpSinglePlayer";
            this.grpSinglePlayer.Size = new System.Drawing.Size(472, 81);
            this.grpSinglePlayer.TabIndex = 7;
            this.grpSinglePlayer.TabStop = false;
            // 
            // chkUpdateNotificationSingle
            // 
            this.chkUpdateNotificationSingle.AutoSize = true;
            this.chkUpdateNotificationSingle.Enabled = false;
            this.chkUpdateNotificationSingle.Location = new System.Drawing.Point(19, 58);
            this.chkUpdateNotificationSingle.Name = "chkUpdateNotificationSingle";
            this.chkUpdateNotificationSingle.Size = new System.Drawing.Size(120, 17);
            this.chkUpdateNotificationSingle.TabIndex = 2;
            this.chkUpdateNotificationSingle.Text = "Update notifications";
            this.chkUpdateNotificationSingle.UseVisualStyleBackColor = true;
            // 
            // lblSelectedMapSP
            // 
            this.lblSelectedMapSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedMapSP.BackColor = System.Drawing.Color.Aqua;
            this.lblSelectedMapSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMapSP.Location = new System.Drawing.Point(-2, 6);
            this.lblSelectedMapSP.Name = "lblSelectedMapSP";
            this.lblSelectedMapSP.Size = new System.Drawing.Size(475, 22);
            this.lblSelectedMapSP.TabIndex = 0;
            this.lblSelectedMapSP.Text = "    Selected Map";
            this.lblSelectedMapSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboMapSinglePlayer
            // 
            this.cboMapSinglePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMapSinglePlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMapSinglePlayer.FormattingEnabled = true;
            this.cboMapSinglePlayer.Location = new System.Drawing.Point(19, 34);
            this.cboMapSinglePlayer.Name = "cboMapSinglePlayer";
            this.cboMapSinglePlayer.Size = new System.Drawing.Size(430, 21);
            this.cboMapSinglePlayer.TabIndex = 1;
            this.cboMapSinglePlayer.SelectedIndexChanged += new System.EventHandler(this.cboMapSinglePlayer_SelectedIndexChanged);
            // 
            // optOffline
            // 
            this.optOffline.AutoSize = true;
            this.optOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optOffline.Location = new System.Drawing.Point(37, 118);
            this.optOffline.Name = "optOffline";
            this.optOffline.Size = new System.Drawing.Size(102, 17);
            this.optOffline.TabIndex = 8;
            this.optOffline.Text = "File Selection";
            this.optOffline.UseVisualStyleBackColor = true;
            this.optOffline.CheckedChanged += new System.EventHandler(this.optOffline_CheckedChanged);
            // 
            // optSinglePlayer
            // 
            this.optSinglePlayer.AutoSize = true;
            this.optSinglePlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSinglePlayer.Location = new System.Drawing.Point(36, 15);
            this.optSinglePlayer.Name = "optSinglePlayer";
            this.optSinglePlayer.Size = new System.Drawing.Size(99, 17);
            this.optSinglePlayer.TabIndex = 6;
            this.optSinglePlayer.Text = "Single Player";
            this.optSinglePlayer.UseVisualStyleBackColor = true;
            this.optSinglePlayer.CheckedChanged += new System.EventHandler(this.optSinglePlayer_CheckedChanged);
            // 
            // optServer
            // 
            this.optServer.AutoSize = true;
            this.optServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optServer.Location = new System.Drawing.Point(37, 225);
            this.optServer.Name = "optServer";
            this.optServer.Size = new System.Drawing.Size(89, 17);
            this.optServer.TabIndex = 10;
            this.optServer.Text = "FTP Server";
            this.optServer.UseVisualStyleBackColor = true;
            this.optServer.CheckedChanged += new System.EventHandler(this.optServer_CheckedChanged);
            // 
            // tpgColours
            // 
            this.tpgColours.Controls.Add(this.checkBox1);
            this.tpgColours.Controls.Add(this.textBox1);
            this.tpgColours.Controls.Add(this.button1);
            this.tpgColours.Controls.Add(this.button2);
            this.tpgColours.Controls.Add(this.button3);
            this.tpgColours.Controls.Add(this.lvwColours);
            this.tpgColours.Location = new System.Drawing.Point(4, 22);
            this.tpgColours.Name = "tpgColours";
            this.tpgColours.Size = new System.Drawing.Size(545, 626);
            this.tpgColours.TabIndex = 5;
            this.tpgColours.Text = "Colours";
            this.tpgColours.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.Enabled = false;
            this.checkBox1.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.checkBox1.Location = new System.Drawing.Point(445, 514);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(33, 27);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(108, 517);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 20);
            this.textBox1.TabIndex = 26;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(481, 514);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 27);
            this.button1.TabIndex = 25;
            this.toolTip1.SetToolTip(this.button1, "Edit display name");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.button2.Location = new System.Drawing.Point(68, 514);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 27);
            this.button2.TabIndex = 24;
            this.toolTip1.SetToolTip(this.button2, "Remove display name");
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = global::ARKViewer.Properties.Resources.button_add;
            this.button3.Location = new System.Drawing.Point(29, 514);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 27);
            this.button3.TabIndex = 23;
            this.toolTip1.SetToolTip(this.button3, "Add new display name");
            this.button3.UseVisualStyleBackColor = true;
            // 
            // lvwColours
            // 
            this.lvwColours.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader7,
            this.columnHeader8});
            this.lvwColours.FullRowSelect = true;
            this.lvwColours.HideSelection = false;
            this.lvwColours.Location = new System.Drawing.Point(29, 26);
            this.lvwColours.Name = "lvwColours";
            this.lvwColours.Size = new System.Drawing.Size(485, 482);
            this.lvwColours.TabIndex = 22;
            this.lvwColours.UseCompatibleStateImageBehavior = false;
            this.lvwColours.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Id";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Hex";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Colour";
            this.columnHeader8.Width = 297;
            // 
            // tpgCreatures
            // 
            this.tpgCreatures.Controls.Add(this.chkApplyFilterDinos);
            this.tpgCreatures.Controls.Add(this.txtCreatureFilter);
            this.tpgCreatures.Controls.Add(this.btnEditDinoClass);
            this.tpgCreatures.Controls.Add(this.btnRemoveDinoClass);
            this.tpgCreatures.Controls.Add(this.btnAddDinoClass);
            this.tpgCreatures.Controls.Add(this.lvwDinoClasses);
            this.tpgCreatures.Location = new System.Drawing.Point(4, 22);
            this.tpgCreatures.Name = "tpgCreatures";
            this.tpgCreatures.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCreatures.Size = new System.Drawing.Size(545, 626);
            this.tpgCreatures.TabIndex = 1;
            this.tpgCreatures.Text = "Creature Names";
            this.tpgCreatures.UseVisualStyleBackColor = true;
            // 
            // chkApplyFilterDinos
            // 
            this.chkApplyFilterDinos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilterDinos.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilterDinos.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilterDinos.Location = new System.Drawing.Point(445, 514);
            this.chkApplyFilterDinos.Name = "chkApplyFilterDinos";
            this.chkApplyFilterDinos.Size = new System.Drawing.Size(33, 27);
            this.chkApplyFilterDinos.TabIndex = 21;
            this.chkApplyFilterDinos.UseVisualStyleBackColor = true;
            this.chkApplyFilterDinos.CheckedChanged += new System.EventHandler(this.chkApplyFilterDinos_CheckedChanged);
            // 
            // txtCreatureFilter
            // 
            this.txtCreatureFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtCreatureFilter.Location = new System.Drawing.Point(108, 517);
            this.txtCreatureFilter.Name = "txtCreatureFilter";
            this.txtCreatureFilter.Size = new System.Drawing.Size(331, 20);
            this.txtCreatureFilter.TabIndex = 18;
            this.txtCreatureFilter.TextChanged += new System.EventHandler(this.txtCreatureFilter_TextChanged);
            this.txtCreatureFilter.Validating += new System.ComponentModel.CancelEventHandler(this.txtCreatureFilter_Validating);
            // 
            // btnEditDinoClass
            // 
            this.btnEditDinoClass.Enabled = false;
            this.btnEditDinoClass.Image = ((System.Drawing.Image)(resources.GetObject("btnEditDinoClass.Image")));
            this.btnEditDinoClass.Location = new System.Drawing.Point(481, 514);
            this.btnEditDinoClass.Name = "btnEditDinoClass";
            this.btnEditDinoClass.Size = new System.Drawing.Size(34, 27);
            this.btnEditDinoClass.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnEditDinoClass, "Edit display name");
            this.btnEditDinoClass.UseVisualStyleBackColor = true;
            this.btnEditDinoClass.Click += new System.EventHandler(this.btnEditDinoClass_Click);
            // 
            // btnRemoveDinoClass
            // 
            this.btnRemoveDinoClass.Enabled = false;
            this.btnRemoveDinoClass.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveDinoClass.Location = new System.Drawing.Point(68, 514);
            this.btnRemoveDinoClass.Name = "btnRemoveDinoClass";
            this.btnRemoveDinoClass.Size = new System.Drawing.Size(34, 27);
            this.btnRemoveDinoClass.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnRemoveDinoClass, "Remove display name");
            this.btnRemoveDinoClass.UseVisualStyleBackColor = true;
            this.btnRemoveDinoClass.Click += new System.EventHandler(this.btnRemoveDinoClass_Click);
            // 
            // btnAddDinoClass
            // 
            this.btnAddDinoClass.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddDinoClass.Location = new System.Drawing.Point(29, 514);
            this.btnAddDinoClass.Name = "btnAddDinoClass";
            this.btnAddDinoClass.Size = new System.Drawing.Size(34, 27);
            this.btnAddDinoClass.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnAddDinoClass, "Add new display name");
            this.btnAddDinoClass.UseVisualStyleBackColor = true;
            this.btnAddDinoClass.Click += new System.EventHandler(this.btnAddDinoClass_Click);
            // 
            // lvwDinoClasses
            // 
            this.lvwDinoClasses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvwDinoClasses_ClassName,
            this.lvwDinoClasses_DisplayName});
            this.lvwDinoClasses.FullRowSelect = true;
            this.lvwDinoClasses.HideSelection = false;
            this.lvwDinoClasses.Location = new System.Drawing.Point(29, 26);
            this.lvwDinoClasses.Name = "lvwDinoClasses";
            this.lvwDinoClasses.Size = new System.Drawing.Size(485, 482);
            this.lvwDinoClasses.TabIndex = 3;
            this.lvwDinoClasses.UseCompatibleStateImageBehavior = false;
            this.lvwDinoClasses.View = System.Windows.Forms.View.Details;
            this.lvwDinoClasses.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwDinoClasses_ColumnClick);
            this.lvwDinoClasses.SelectedIndexChanged += new System.EventHandler(this.lvwDinoClasses_SelectedIndexChanged);
            // 
            // lvwDinoClasses_ClassName
            // 
            this.lvwDinoClasses_ClassName.DisplayIndex = 1;
            this.lvwDinoClasses_ClassName.Text = "Class Name";
            this.lvwDinoClasses_ClassName.Width = 244;
            // 
            // lvwDinoClasses_DisplayName
            // 
            this.lvwDinoClasses_DisplayName.DisplayIndex = 0;
            this.lvwDinoClasses_DisplayName.Text = "Display Name";
            this.lvwDinoClasses_DisplayName.Width = 205;
            // 
            // tpgStructures
            // 
            this.tpgStructures.Controls.Add(this.chkApplyFilterStructures);
            this.tpgStructures.Controls.Add(this.txtStructureFilter);
            this.tpgStructures.Controls.Add(this.btnEditStructure);
            this.tpgStructures.Controls.Add(this.btnRemoveStructure);
            this.tpgStructures.Controls.Add(this.btnAddStructure);
            this.tpgStructures.Controls.Add(this.lvwStructureMap);
            this.tpgStructures.Location = new System.Drawing.Point(4, 22);
            this.tpgStructures.Name = "tpgStructures";
            this.tpgStructures.Size = new System.Drawing.Size(545, 626);
            this.tpgStructures.TabIndex = 4;
            this.tpgStructures.Text = "Structures";
            this.tpgStructures.UseVisualStyleBackColor = true;
            // 
            // chkApplyFilterStructures
            // 
            this.chkApplyFilterStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilterStructures.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilterStructures.Enabled = false;
            this.chkApplyFilterStructures.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilterStructures.Location = new System.Drawing.Point(442, 513);
            this.chkApplyFilterStructures.Name = "chkApplyFilterStructures";
            this.chkApplyFilterStructures.Size = new System.Drawing.Size(33, 27);
            this.chkApplyFilterStructures.TabIndex = 26;
            this.toolTip1.SetToolTip(this.chkApplyFilterStructures, "Apply/Remove filter");
            this.chkApplyFilterStructures.UseVisualStyleBackColor = true;
            // 
            // txtStructureFilter
            // 
            this.txtStructureFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtStructureFilter.Enabled = false;
            this.txtStructureFilter.Location = new System.Drawing.Point(108, 516);
            this.txtStructureFilter.Name = "txtStructureFilter";
            this.txtStructureFilter.Size = new System.Drawing.Size(327, 20);
            this.txtStructureFilter.TabIndex = 25;
            // 
            // btnEditStructure
            // 
            this.btnEditStructure.Enabled = false;
            this.btnEditStructure.Image = ((System.Drawing.Image)(resources.GetObject("btnEditStructure.Image")));
            this.btnEditStructure.Location = new System.Drawing.Point(481, 513);
            this.btnEditStructure.Name = "btnEditStructure";
            this.btnEditStructure.Size = new System.Drawing.Size(34, 27);
            this.btnEditStructure.TabIndex = 24;
            this.toolTip1.SetToolTip(this.btnEditStructure, "Edit display name");
            this.btnEditStructure.UseVisualStyleBackColor = true;
            // 
            // btnRemoveStructure
            // 
            this.btnRemoveStructure.Enabled = false;
            this.btnRemoveStructure.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveStructure.Location = new System.Drawing.Point(68, 513);
            this.btnRemoveStructure.Name = "btnRemoveStructure";
            this.btnRemoveStructure.Size = new System.Drawing.Size(34, 27);
            this.btnRemoveStructure.TabIndex = 23;
            this.toolTip1.SetToolTip(this.btnRemoveStructure, "Remove display name");
            this.btnRemoveStructure.UseVisualStyleBackColor = true;
            this.btnRemoveStructure.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAddStructure
            // 
            this.btnAddStructure.Enabled = false;
            this.btnAddStructure.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddStructure.Location = new System.Drawing.Point(29, 513);
            this.btnAddStructure.Name = "btnAddStructure";
            this.btnAddStructure.Size = new System.Drawing.Size(34, 27);
            this.btnAddStructure.TabIndex = 22;
            this.toolTip1.SetToolTip(this.btnAddStructure, "Add new display name");
            this.btnAddStructure.UseVisualStyleBackColor = true;
            // 
            // lvwStructureMap
            // 
            this.lvwStructureMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvwStructureMap.FullRowSelect = true;
            this.lvwStructureMap.HideSelection = false;
            this.lvwStructureMap.LargeImageList = this.imageList1;
            this.lvwStructureMap.Location = new System.Drawing.Point(29, 25);
            this.lvwStructureMap.Name = "lvwStructureMap";
            this.lvwStructureMap.Size = new System.Drawing.Size(485, 482);
            this.lvwStructureMap.SmallImageList = this.imageList1;
            this.lvwStructureMap.TabIndex = 21;
            this.lvwStructureMap.UseCompatibleStateImageBehavior = false;
            this.lvwStructureMap.View = System.Windows.Forms.View.Details;
            this.lvwStructureMap.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwStructureMap_ColumnClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 1;
            this.columnHeader5.Text = "Class Name";
            this.columnHeader5.Width = 262;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 0;
            this.columnHeader6.Text = "Display Name";
            this.columnHeader6.Width = 193;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tpgItems
            // 
            this.tpgItems.Controls.Add(this.chkApplyFilterItems);
            this.tpgItems.Controls.Add(this.txtItemFilter);
            this.tpgItems.Controls.Add(this.btnEditItem);
            this.tpgItems.Controls.Add(this.btnRemoveItem);
            this.tpgItems.Controls.Add(this.btnAddItem);
            this.tpgItems.Controls.Add(this.lvwItemMap);
            this.tpgItems.Location = new System.Drawing.Point(4, 22);
            this.tpgItems.Name = "tpgItems";
            this.tpgItems.Size = new System.Drawing.Size(545, 626);
            this.tpgItems.TabIndex = 2;
            this.tpgItems.Text = "Items";
            this.tpgItems.UseVisualStyleBackColor = true;
            // 
            // chkApplyFilterItems
            // 
            this.chkApplyFilterItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilterItems.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilterItems.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilterItems.Location = new System.Drawing.Point(442, 513);
            this.chkApplyFilterItems.Name = "chkApplyFilterItems";
            this.chkApplyFilterItems.Size = new System.Drawing.Size(33, 27);
            this.chkApplyFilterItems.TabIndex = 20;
            this.chkApplyFilterItems.UseVisualStyleBackColor = true;
            this.chkApplyFilterItems.CheckedChanged += new System.EventHandler(this.chkApplyFilterItems_CheckedChanged);
            // 
            // txtItemFilter
            // 
            this.txtItemFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtItemFilter.Location = new System.Drawing.Point(108, 516);
            this.txtItemFilter.Name = "txtItemFilter";
            this.txtItemFilter.Size = new System.Drawing.Size(327, 20);
            this.txtItemFilter.TabIndex = 19;
            this.txtItemFilter.TextChanged += new System.EventHandler(this.txtItemFilter_TextChanged);
            this.txtItemFilter.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemFilter_Validating);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Enabled = false;
            this.btnEditItem.Image = ((System.Drawing.Image)(resources.GetObject("btnEditItem.Image")));
            this.btnEditItem.Location = new System.Drawing.Point(481, 513);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(34, 27);
            this.btnEditItem.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnEditItem, "Edit display name");
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Enabled = false;
            this.btnRemoveItem.Image = global::ARKViewer.Properties.Resources.button_remove;
            this.btnRemoveItem.Location = new System.Drawing.Point(68, 513);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(34, 27);
            this.btnRemoveItem.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnRemoveItem, "Remove display name");
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Image = global::ARKViewer.Properties.Resources.button_add;
            this.btnAddItem.Location = new System.Drawing.Point(29, 513);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(34, 27);
            this.btnAddItem.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnAddItem, "Add new display name");
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lvwItemMap
            // 
            this.lvwItemMap.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwItemMap.FullRowSelect = true;
            this.lvwItemMap.HideSelection = false;
            this.lvwItemMap.LargeImageList = this.imageList1;
            this.lvwItemMap.Location = new System.Drawing.Point(29, 25);
            this.lvwItemMap.Name = "lvwItemMap";
            this.lvwItemMap.Size = new System.Drawing.Size(485, 482);
            this.lvwItemMap.SmallImageList = this.imageList1;
            this.lvwItemMap.TabIndex = 7;
            this.lvwItemMap.UseCompatibleStateImageBehavior = false;
            this.lvwItemMap.View = System.Windows.Forms.View.Details;
            this.lvwItemMap.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwItemMap_ColumnClick);
            this.lvwItemMap.SelectedIndexChanged += new System.EventHandler(this.lvwItemMap_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Category";
            this.columnHeader3.Width = 95;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 2;
            this.columnHeader1.Text = "Class Name";
            this.columnHeader1.Width = 172;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 1;
            this.columnHeader2.Text = "Display Name";
            this.columnHeader2.Width = 179;
            // 
            // tpgPlayers
            // 
            this.tpgPlayers.Controls.Add(this.pnlFtpSettingsCommands);
            this.tpgPlayers.Controls.Add(this.pnlPlayerSettingsCommands);
            this.tpgPlayers.Controls.Add(this.pnlPlayerSettingsBody);
            this.tpgPlayers.Controls.Add(this.pnlPlayerSettingsTames);
            this.tpgPlayers.Controls.Add(this.pnlPlayerSettingsStuctures);
            this.tpgPlayers.Location = new System.Drawing.Point(4, 22);
            this.tpgPlayers.Name = "tpgPlayers";
            this.tpgPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlayers.Size = new System.Drawing.Size(545, 626);
            this.tpgPlayers.TabIndex = 3;
            this.tpgPlayers.Text = "Options";
            this.tpgPlayers.UseVisualStyleBackColor = true;
            this.tpgPlayers.Click += new System.EventHandler(this.tpgPlayers_Click);
            // 
            // pnlPlayerSettingsCommands
            // 
            this.pnlPlayerSettingsCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayerSettingsCommands.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlPlayerSettingsCommands.Controls.Add(this.optPlayerCommandsPrefixAdmincheat);
            this.pnlPlayerSettingsCommands.Controls.Add(this.optPlayerCommandsPrefixNone);
            this.pnlPlayerSettingsCommands.Controls.Add(this.optPlayerCommandsPrefixCheat);
            this.pnlPlayerSettingsCommands.Controls.Add(this.lblOptionHeaderCommand);
            this.pnlPlayerSettingsCommands.Controls.Add(this.lblOptionTextCommand);
            this.pnlPlayerSettingsCommands.Location = new System.Drawing.Point(19, 293);
            this.pnlPlayerSettingsCommands.Name = "pnlPlayerSettingsCommands";
            this.pnlPlayerSettingsCommands.Size = new System.Drawing.Size(507, 73);
            this.pnlPlayerSettingsCommands.TabIndex = 19;
            // 
            // optPlayerCommandsPrefixAdmincheat
            // 
            this.optPlayerCommandsPrefixAdmincheat.AutoSize = true;
            this.optPlayerCommandsPrefixAdmincheat.Location = new System.Drawing.Point(352, 37);
            this.optPlayerCommandsPrefixAdmincheat.Name = "optPlayerCommandsPrefixAdmincheat";
            this.optPlayerCommandsPrefixAdmincheat.Size = new System.Drawing.Size(83, 17);
            this.optPlayerCommandsPrefixAdmincheat.TabIndex = 5;
            this.optPlayerCommandsPrefixAdmincheat.Text = "admincheat ";
            this.optPlayerCommandsPrefixAdmincheat.UseVisualStyleBackColor = true;
            this.optPlayerCommandsPrefixAdmincheat.CheckedChanged += new System.EventHandler(this.optPlayerCommandsPrefixAdmincheat_CheckedChanged);
            // 
            // optPlayerCommandsPrefixNone
            // 
            this.optPlayerCommandsPrefixNone.AutoSize = true;
            this.optPlayerCommandsPrefixNone.Checked = true;
            this.optPlayerCommandsPrefixNone.Location = new System.Drawing.Point(274, 37);
            this.optPlayerCommandsPrefixNone.Name = "optPlayerCommandsPrefixNone";
            this.optPlayerCommandsPrefixNone.Size = new System.Drawing.Size(57, 17);
            this.optPlayerCommandsPrefixNone.TabIndex = 4;
            this.optPlayerCommandsPrefixNone.TabStop = true;
            this.optPlayerCommandsPrefixNone.Text = "[None]";
            this.optPlayerCommandsPrefixNone.UseVisualStyleBackColor = true;
            this.optPlayerCommandsPrefixNone.CheckedChanged += new System.EventHandler(this.optPlayerCommandsPrefixNone_CheckedChanged);
            // 
            // optPlayerCommandsPrefixCheat
            // 
            this.optPlayerCommandsPrefixCheat.AutoSize = true;
            this.optPlayerCommandsPrefixCheat.Location = new System.Drawing.Point(440, 37);
            this.optPlayerCommandsPrefixCheat.Name = "optPlayerCommandsPrefixCheat";
            this.optPlayerCommandsPrefixCheat.Size = new System.Drawing.Size(55, 17);
            this.optPlayerCommandsPrefixCheat.TabIndex = 3;
            this.optPlayerCommandsPrefixCheat.Text = "cheat ";
            this.optPlayerCommandsPrefixCheat.UseVisualStyleBackColor = true;
            this.optPlayerCommandsPrefixCheat.CheckedChanged += new System.EventHandler(this.optPlayerCommandsPrefixCheat_CheckedChanged);
            // 
            // lblOptionHeaderCommand
            // 
            this.lblOptionHeaderCommand.AutoSize = true;
            this.lblOptionHeaderCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptionHeaderCommand.Location = new System.Drawing.Point(10, 14);
            this.lblOptionHeaderCommand.Name = "lblOptionHeaderCommand";
            this.lblOptionHeaderCommand.Size = new System.Drawing.Size(133, 18);
            this.lblOptionHeaderCommand.TabIndex = 2;
            this.lblOptionHeaderCommand.Text = "Command Prefix";
            // 
            // lblOptionTextCommand
            // 
            this.lblOptionTextCommand.AutoSize = true;
            this.lblOptionTextCommand.Location = new System.Drawing.Point(10, 39);
            this.lblOptionTextCommand.Name = "lblOptionTextCommand";
            this.lblOptionTextCommand.Size = new System.Drawing.Size(149, 13);
            this.lblOptionTextCommand.TabIndex = 0;
            this.lblOptionTextCommand.Text = "Add prefix to copy commands:";
            // 
            // pnlPlayerSettingsBody
            // 
            this.pnlPlayerSettingsBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayerSettingsBody.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlPlayerSettingsBody.Controls.Add(this.optPlayerBodyHide);
            this.pnlPlayerSettingsBody.Controls.Add(this.optPlayerBodyShow);
            this.pnlPlayerSettingsBody.Controls.Add(this.lblOptionHeaderBody);
            this.pnlPlayerSettingsBody.Controls.Add(this.lblOptionTextBody);
            this.pnlPlayerSettingsBody.Location = new System.Drawing.Point(20, 201);
            this.pnlPlayerSettingsBody.Name = "pnlPlayerSettingsBody";
            this.pnlPlayerSettingsBody.Size = new System.Drawing.Size(507, 73);
            this.pnlPlayerSettingsBody.TabIndex = 18;
            // 
            // optPlayerBodyHide
            // 
            this.optPlayerBodyHide.AutoSize = true;
            this.optPlayerBodyHide.Checked = true;
            this.optPlayerBodyHide.Location = new System.Drawing.Point(382, 37);
            this.optPlayerBodyHide.Name = "optPlayerBodyHide";
            this.optPlayerBodyHide.Size = new System.Drawing.Size(47, 17);
            this.optPlayerBodyHide.TabIndex = 4;
            this.optPlayerBodyHide.TabStop = true;
            this.optPlayerBodyHide.Text = "Hide";
            this.optPlayerBodyHide.UseVisualStyleBackColor = true;
            this.optPlayerBodyHide.CheckedChanged += new System.EventHandler(this.optPlayerBodyHide_CheckedChanged);
            // 
            // optPlayerBodyShow
            // 
            this.optPlayerBodyShow.AutoSize = true;
            this.optPlayerBodyShow.Location = new System.Drawing.Point(440, 37);
            this.optPlayerBodyShow.Name = "optPlayerBodyShow";
            this.optPlayerBodyShow.Size = new System.Drawing.Size(52, 17);
            this.optPlayerBodyShow.TabIndex = 3;
            this.optPlayerBodyShow.Text = "Show";
            this.optPlayerBodyShow.UseVisualStyleBackColor = true;
            this.optPlayerBodyShow.CheckedChanged += new System.EventHandler(this.optPlayerBodyShow_CheckedChanged);
            // 
            // lblOptionHeaderBody
            // 
            this.lblOptionHeaderBody.AutoSize = true;
            this.lblOptionHeaderBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptionHeaderBody.Location = new System.Drawing.Point(10, 14);
            this.lblOptionHeaderBody.Name = "lblOptionHeaderBody";
            this.lblOptionHeaderBody.Size = new System.Drawing.Size(46, 18);
            this.lblOptionHeaderBody.TabIndex = 2;
            this.lblOptionHeaderBody.Text = "Body";
            // 
            // lblOptionTextBody
            // 
            this.lblOptionTextBody.AutoSize = true;
            this.lblOptionTextBody.Location = new System.Drawing.Point(10, 39);
            this.lblOptionTextBody.Name = "lblOptionTextBody";
            this.lblOptionTextBody.Size = new System.Drawing.Size(289, 13);
            this.lblOptionTextBody.TabIndex = 0;
            this.lblOptionTextBody.Text = "Display Tribes and Players with no body. (Dead / Uploaded)";
            // 
            // pnlPlayerSettingsTames
            // 
            this.pnlPlayerSettingsTames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayerSettingsTames.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlPlayerSettingsTames.Controls.Add(this.optPlayerTameHide);
            this.pnlPlayerSettingsTames.Controls.Add(this.optPlayerTameShow);
            this.pnlPlayerSettingsTames.Controls.Add(this.lblOptionHeaderTames);
            this.pnlPlayerSettingsTames.Controls.Add(this.lblOptionTextTames);
            this.pnlPlayerSettingsTames.Location = new System.Drawing.Point(20, 109);
            this.pnlPlayerSettingsTames.Name = "pnlPlayerSettingsTames";
            this.pnlPlayerSettingsTames.Size = new System.Drawing.Size(507, 73);
            this.pnlPlayerSettingsTames.TabIndex = 17;
            // 
            // optPlayerTameHide
            // 
            this.optPlayerTameHide.AutoSize = true;
            this.optPlayerTameHide.Checked = true;
            this.optPlayerTameHide.Location = new System.Drawing.Point(382, 37);
            this.optPlayerTameHide.Name = "optPlayerTameHide";
            this.optPlayerTameHide.Size = new System.Drawing.Size(47, 17);
            this.optPlayerTameHide.TabIndex = 4;
            this.optPlayerTameHide.TabStop = true;
            this.optPlayerTameHide.Text = "Hide";
            this.optPlayerTameHide.UseVisualStyleBackColor = true;
            this.optPlayerTameHide.CheckedChanged += new System.EventHandler(this.optPlayerTameHide_CheckedChanged);
            // 
            // optPlayerTameShow
            // 
            this.optPlayerTameShow.AutoSize = true;
            this.optPlayerTameShow.Location = new System.Drawing.Point(440, 37);
            this.optPlayerTameShow.Name = "optPlayerTameShow";
            this.optPlayerTameShow.Size = new System.Drawing.Size(52, 17);
            this.optPlayerTameShow.TabIndex = 3;
            this.optPlayerTameShow.Text = "Show";
            this.optPlayerTameShow.UseVisualStyleBackColor = true;
            this.optPlayerTameShow.CheckedChanged += new System.EventHandler(this.optPlayerTameShow_CheckedChanged);
            // 
            // lblOptionHeaderTames
            // 
            this.lblOptionHeaderTames.AutoSize = true;
            this.lblOptionHeaderTames.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptionHeaderTames.Location = new System.Drawing.Point(10, 14);
            this.lblOptionHeaderTames.Name = "lblOptionHeaderTames";
            this.lblOptionHeaderTames.Size = new System.Drawing.Size(59, 18);
            this.lblOptionHeaderTames.TabIndex = 2;
            this.lblOptionHeaderTames.Text = "Tames";
            // 
            // lblOptionTextTames
            // 
            this.lblOptionTextTames.AutoSize = true;
            this.lblOptionTextTames.Location = new System.Drawing.Point(10, 39);
            this.lblOptionTextTames.Name = "lblOptionTextTames";
            this.lblOptionTextTames.Size = new System.Drawing.Size(250, 13);
            this.lblOptionTextTames.TabIndex = 0;
            this.lblOptionTextTames.Text = "Display Tribes and Players with no tamed creatures.";
            // 
            // pnlPlayerSettingsStuctures
            // 
            this.pnlPlayerSettingsStuctures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayerSettingsStuctures.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlPlayerSettingsStuctures.Controls.Add(this.optPlayerStructureHide);
            this.pnlPlayerSettingsStuctures.Controls.Add(this.optPlayerStructureShow);
            this.pnlPlayerSettingsStuctures.Controls.Add(this.lblOptionHeaderStructures);
            this.pnlPlayerSettingsStuctures.Controls.Add(this.lblOptionTextStructures);
            this.pnlPlayerSettingsStuctures.Location = new System.Drawing.Point(20, 20);
            this.pnlPlayerSettingsStuctures.Name = "pnlPlayerSettingsStuctures";
            this.pnlPlayerSettingsStuctures.Size = new System.Drawing.Size(507, 73);
            this.pnlPlayerSettingsStuctures.TabIndex = 16;
            // 
            // optPlayerStructureHide
            // 
            this.optPlayerStructureHide.AutoSize = true;
            this.optPlayerStructureHide.Checked = true;
            this.optPlayerStructureHide.Location = new System.Drawing.Point(382, 37);
            this.optPlayerStructureHide.Name = "optPlayerStructureHide";
            this.optPlayerStructureHide.Size = new System.Drawing.Size(47, 17);
            this.optPlayerStructureHide.TabIndex = 4;
            this.optPlayerStructureHide.TabStop = true;
            this.optPlayerStructureHide.Text = "Hide";
            this.optPlayerStructureHide.UseVisualStyleBackColor = true;
            this.optPlayerStructureHide.CheckedChanged += new System.EventHandler(this.optPlayerStructureHide_CheckedChanged);
            // 
            // optPlayerStructureShow
            // 
            this.optPlayerStructureShow.AutoSize = true;
            this.optPlayerStructureShow.Location = new System.Drawing.Point(440, 37);
            this.optPlayerStructureShow.Name = "optPlayerStructureShow";
            this.optPlayerStructureShow.Size = new System.Drawing.Size(52, 17);
            this.optPlayerStructureShow.TabIndex = 3;
            this.optPlayerStructureShow.Text = "Show";
            this.optPlayerStructureShow.UseVisualStyleBackColor = true;
            this.optPlayerStructureShow.CheckedChanged += new System.EventHandler(this.optPlayerStructureShow_CheckedChanged);
            // 
            // lblOptionHeaderStructures
            // 
            this.lblOptionHeaderStructures.AutoSize = true;
            this.lblOptionHeaderStructures.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptionHeaderStructures.Location = new System.Drawing.Point(10, 14);
            this.lblOptionHeaderStructures.Name = "lblOptionHeaderStructures";
            this.lblOptionHeaderStructures.Size = new System.Drawing.Size(86, 18);
            this.lblOptionHeaderStructures.TabIndex = 2;
            this.lblOptionHeaderStructures.Text = "Structures";
            // 
            // lblOptionTextStructures
            // 
            this.lblOptionTextStructures.AutoSize = true;
            this.lblOptionTextStructures.Location = new System.Drawing.Point(10, 39);
            this.lblOptionTextStructures.Name = "lblOptionTextStructures";
            this.lblOptionTextStructures.Size = new System.Drawing.Size(261, 13);
            this.lblOptionTextStructures.TabIndex = 0;
            this.lblOptionTextStructures.Text = "Display Tribes and Players with no placed structure(s).";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 10;
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 10;
            this.toolTip1.ReshowDelay = 2000;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Information";
            // 
            // pnlFtpSettingsCommands
            // 
            this.pnlFtpSettingsCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFtpSettingsCommands.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlFtpSettingsCommands.Controls.Add(this.optFTPSync);
            this.pnlFtpSettingsCommands.Controls.Add(this.optFTPClean);
            this.pnlFtpSettingsCommands.Controls.Add(this.label2);
            this.pnlFtpSettingsCommands.Controls.Add(this.label3);
            this.pnlFtpSettingsCommands.Location = new System.Drawing.Point(19, 387);
            this.pnlFtpSettingsCommands.Name = "pnlFtpSettingsCommands";
            this.pnlFtpSettingsCommands.Size = new System.Drawing.Size(507, 73);
            this.pnlFtpSettingsCommands.TabIndex = 20;
            // 
            // optFTPSync
            // 
            this.optFTPSync.AutoSize = true;
            this.optFTPSync.Checked = true;
            this.optFTPSync.Location = new System.Drawing.Point(347, 37);
            this.optFTPSync.Name = "optFTPSync";
            this.optFTPSync.Size = new System.Drawing.Size(83, 17);
            this.optFTPSync.TabIndex = 5;
            this.optFTPSync.TabStop = true;
            this.optFTPSync.Text = "Synchronise";
            this.optFTPSync.UseVisualStyleBackColor = true;
            // 
            // optFTPClean
            // 
            this.optFTPClean.AutoSize = true;
            this.optFTPClean.Location = new System.Drawing.Point(440, 37);
            this.optFTPClean.Name = "optFTPClean";
            this.optFTPClean.Size = new System.Drawing.Size(52, 17);
            this.optFTPClean.TabIndex = 3;
            this.optFTPClean.Text = "Clean";
            this.optFTPClean.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "FTP Downloads";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "How to handle downloads";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 707);
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabSettings.ResumeLayout(false);
            this.tpgMap.ResumeLayout(false);
            this.tpgMap.PerformLayout();
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFTPPort)).EndInit();
            this.grpOffline.ResumeLayout(false);
            this.grpOffline.PerformLayout();
            this.grpSinglePlayer.ResumeLayout(false);
            this.grpSinglePlayer.PerformLayout();
            this.tpgColours.ResumeLayout(false);
            this.tpgColours.PerformLayout();
            this.tpgCreatures.ResumeLayout(false);
            this.tpgCreatures.PerformLayout();
            this.tpgStructures.ResumeLayout(false);
            this.tpgStructures.PerformLayout();
            this.tpgItems.ResumeLayout(false);
            this.tpgItems.PerformLayout();
            this.tpgPlayers.ResumeLayout(false);
            this.pnlPlayerSettingsCommands.ResumeLayout(false);
            this.pnlPlayerSettingsCommands.PerformLayout();
            this.pnlPlayerSettingsBody.ResumeLayout(false);
            this.pnlPlayerSettingsBody.PerformLayout();
            this.pnlPlayerSettingsTames.ResumeLayout(false);
            this.pnlPlayerSettingsTames.PerformLayout();
            this.pnlPlayerSettingsStuctures.ResumeLayout(false);
            this.pnlPlayerSettingsStuctures.PerformLayout();
            this.pnlFtpSettingsCommands.ResumeLayout(false);
            this.pnlFtpSettingsCommands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tpgMap;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.Button btnServerImport;
        private System.Windows.Forms.CheckBox chkPasswordVisibility;
        private System.Windows.Forms.NumericUpDown udFTPPort;
        private System.Windows.Forms.Panel pnlServerDetails;
        private System.Windows.Forms.TextBox txtFTPFilePath;
        private System.Windows.Forms.Label lblFtpFilePath;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.Label lblFTPServer;
        private System.Windows.Forms.TextBox txtFTPPassword;
        private System.Windows.Forms.TextBox txtFTPUsername;
        private System.Windows.Forms.Label lblFTPPassword;
        private System.Windows.Forms.Label lblFTPUsername;
        private System.Windows.Forms.TextBox txtFTPAddress;
        private System.Windows.Forms.Label lblFTPPort;
        private System.Windows.Forms.Label lblFTPHost;
        private System.Windows.Forms.ComboBox cboFTPServer;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.GroupBox grpOffline;
        private System.Windows.Forms.Button btnSelectSaveGame;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label lblOfflineSave;
        private System.Windows.Forms.GroupBox grpSinglePlayer;
        private System.Windows.Forms.Label lblSelectedMapSP;
        private System.Windows.Forms.ComboBox cboMapSinglePlayer;
        private System.Windows.Forms.RadioButton optOffline;
        private System.Windows.Forms.RadioButton optSinglePlayer;
        private System.Windows.Forms.RadioButton optServer;
        private System.Windows.Forms.TabPage tpgCreatures;
        private System.Windows.Forms.ListView lvwDinoClasses;
        private System.Windows.Forms.ColumnHeader lvwDinoClasses_ClassName;
        private System.Windows.Forms.ColumnHeader lvwDinoClasses_DisplayName;
        private System.Windows.Forms.Button btnEditDinoClass;
        private System.Windows.Forms.Button btnRemoveDinoClass;
        private System.Windows.Forms.Button btnAddDinoClass;
        private System.Windows.Forms.Button btnServerExport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tpgItems;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListView lvwItemMap;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox txtCreatureFilter;
        private System.Windows.Forms.TextBox txtItemFilter;
        private System.Windows.Forms.CheckBox chkApplyFilterDinos;
        private System.Windows.Forms.CheckBox chkApplyFilterItems;
        private System.Windows.Forms.TabPage tpgPlayers;
        private System.Windows.Forms.Panel pnlPlayerSettingsStuctures;
        private System.Windows.Forms.RadioButton optPlayerStructureHide;
        private System.Windows.Forms.RadioButton optPlayerStructureShow;
        private System.Windows.Forms.Label lblOptionHeaderStructures;
        private System.Windows.Forms.Label lblOptionTextStructures;
        private System.Windows.Forms.Panel pnlPlayerSettingsTames;
        private System.Windows.Forms.RadioButton optPlayerTameHide;
        private System.Windows.Forms.RadioButton optPlayerTameShow;
        private System.Windows.Forms.Label lblOptionHeaderTames;
        private System.Windows.Forms.Label lblOptionTextTames;
        private System.Windows.Forms.Panel pnlPlayerSettingsBody;
        private System.Windows.Forms.RadioButton optPlayerBodyHide;
        private System.Windows.Forms.RadioButton optPlayerBodyShow;
        private System.Windows.Forms.Label lblOptionHeaderBody;
        private System.Windows.Forms.Label lblOptionTextBody;
        private System.Windows.Forms.Panel pnlPlayerSettingsCommands;
        private System.Windows.Forms.RadioButton optPlayerCommandsPrefixAdmincheat;
        private System.Windows.Forms.RadioButton optPlayerCommandsPrefixNone;
        private System.Windows.Forms.RadioButton optPlayerCommandsPrefixCheat;
        private System.Windows.Forms.Label lblOptionHeaderCommand;
        private System.Windows.Forms.Label lblOptionTextCommand;
        private System.Windows.Forms.TabPage tpgStructures;
        private System.Windows.Forms.CheckBox chkApplyFilterStructures;
        private System.Windows.Forms.TextBox txtStructureFilter;
        private System.Windows.Forms.Button btnEditStructure;
        private System.Windows.Forms.Button btnRemoveStructure;
        private System.Windows.Forms.Button btnAddStructure;
        private System.Windows.Forms.ListView lvwStructureMap;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TabPage tpgColours;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView lvwColours;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label lblFtpMap;
        private System.Windows.Forms.ComboBox cboFtpMap;
        private System.Windows.Forms.RadioButton optFtpModeSftp;
        private System.Windows.Forms.RadioButton optFtpModeFtp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkUpdateNotificationFile;
        private System.Windows.Forms.CheckBox chkUpdateNotificationSingle;
        private System.Windows.Forms.Panel pnlFtpSettingsCommands;
        private System.Windows.Forms.RadioButton optFTPSync;
        private System.Windows.Forms.RadioButton optFTPClean;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}