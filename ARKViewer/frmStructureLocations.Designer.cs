namespace ARKViewer
{
    partial class frmStructureLocations
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
            this.picType = new System.Windows.Forms.PictureBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lvwMapMarkers = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopyCommand = new System.Windows.Forms.Button();
            this.cboConsoleCommands = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picType)).BeginInit();
            this.SuspendLayout();
            // 
            // picType
            // 
            this.picType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picType.Cursor = System.Windows.Forms.Cursors.Default;
            this.picType.Image = global::ARKViewer.Properties.Resources.structure_marker_beaver;
            this.picType.Location = new System.Drawing.Point(81, 4);
            this.picType.Name = "picType";
            this.picType.Size = new System.Drawing.Size(102, 50);
            this.picType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picType.TabIndex = 2;
            this.picType.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.Black;
            this.lblType.Location = new System.Drawing.Point(12, 58);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(241, 18);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Beaver Dams";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvwMapMarkers
            // 
            this.lvwMapMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMapMarkers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lvwMapMarkers.FullRowSelect = true;
            this.lvwMapMarkers.HideSelection = false;
            this.lvwMapMarkers.Location = new System.Drawing.Point(15, 79);
            this.lvwMapMarkers.Name = "lvwMapMarkers";
            this.lvwMapMarkers.ShowItemToolTips = true;
            this.lvwMapMarkers.Size = new System.Drawing.Size(238, 293);
            this.lvwMapMarkers.TabIndex = 21;
            this.lvwMapMarkers.UseCompatibleStateImageBehavior = false;
            this.lvwMapMarkers.View = System.Windows.Forms.View.Details;
            this.lvwMapMarkers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMapMarkers_ColumnClick);
            this.lvwMapMarkers.SelectedIndexChanged += new System.EventHandler(this.lvwMapMarkers_SelectedIndexChanged);
            this.lvwMapMarkers.Click += new System.EventHandler(this.lvwMapMarkers_Click);
            this.lvwMapMarkers.DoubleClick += new System.EventHandler(this.lvwMapMarkers_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Lat";
            this.columnHeader2.Width = 113;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Lon";
            this.columnHeader3.Width = 119;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(178, 411);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopyCommand
            // 
            this.btnCopyCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyCommand.Image = global::ARKViewer.Properties.Resources.button_document;
            this.btnCopyCommand.Location = new System.Drawing.Point(223, 378);
            this.btnCopyCommand.Name = "btnCopyCommand";
            this.btnCopyCommand.Size = new System.Drawing.Size(30, 30);
            this.btnCopyCommand.TabIndex = 38;
            this.btnCopyCommand.UseVisualStyleBackColor = true;
            this.btnCopyCommand.Click += new System.EventHandler(this.btnCopyCommand_Click);
            // 
            // cboConsoleCommands
            // 
            this.cboConsoleCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboConsoleCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoleCommands.FormattingEnabled = true;
            this.cboConsoleCommands.Items.AddRange(new object[] {
            "SetPlayerPos  <x> <y> <z>"});
            this.cboConsoleCommands.Location = new System.Drawing.Point(14, 382);
            this.cboConsoleCommands.Name = "cboConsoleCommands";
            this.cboConsoleCommands.Size = new System.Drawing.Size(201, 21);
            this.cboConsoleCommands.TabIndex = 36;
            // 
            // frmStructureLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(265, 442);
            this.Controls.Add(this.btnCopyCommand);
            this.Controls.Add(this.cboConsoleCommands);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvwMapMarkers);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.picType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmStructureLocations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Structure Locations";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStructureLocations_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ListView lvwMapMarkers;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopyCommand;
        private System.Windows.Forms.ComboBox cboConsoleCommands;
    }
}