
namespace ARKViewer
{
    partial class frmFtpFileBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFtpFileBrowser));
            this.lvwFileBrowser = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imagesFileBrowser = new System.Windows.Forms.ImageList(this.components);
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.optFtpModeSftp = new System.Windows.Forms.RadioButton();
            this.optFtpModeFtp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPasswordVisibility = new System.Windows.Forms.CheckBox();
            this.udFTPPort = new System.Windows.Forms.NumericUpDown();
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.txtFTPUsername = new System.Windows.Forms.TextBox();
            this.lblFTPPassword = new System.Windows.Forms.Label();
            this.lblFTPUsername = new System.Windows.Forms.Label();
            this.txtFTPAddress = new System.Windows.Forms.TextBox();
            this.lblFTPPort = new System.Windows.Forms.Label();
            this.lblFTPHost = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udFTPPort)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwFileBrowser
            // 
            this.lvwFileBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFileBrowser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwFileBrowser.Enabled = false;
            this.lvwFileBrowser.FullRowSelect = true;
            this.lvwFileBrowser.HideSelection = false;
            this.lvwFileBrowser.LargeImageList = this.imagesFileBrowser;
            this.lvwFileBrowser.Location = new System.Drawing.Point(258, 12);
            this.lvwFileBrowser.Name = "lvwFileBrowser";
            this.lvwFileBrowser.Size = new System.Drawing.Size(546, 339);
            this.lvwFileBrowser.SmallImageList = this.imagesFileBrowser;
            this.lvwFileBrowser.TabIndex = 15;
            this.lvwFileBrowser.UseCompatibleStateImageBehavior = false;
            this.lvwFileBrowser.View = System.Windows.Forms.View.Details;
            this.lvwFileBrowser.SelectedIndexChanged += new System.EventHandler(this.lvwFileBrowser_SelectedIndexChanged);
            this.lvwFileBrowser.DoubleClick += new System.EventHandler(this.lvwFileBrowser_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date Modified";
            this.columnHeader2.Width = 175;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 100;
            // 
            // imagesFileBrowser
            // 
            this.imagesFileBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesFileBrowser.ImageStream")));
            this.imagesFileBrowser.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesFileBrowser.Images.SetKeyName(0, "shell32_46.ico");
            this.imagesFileBrowser.Images.SetKeyName(1, "shell32_235.ico");
            this.imagesFileBrowser.Images.SetKeyName(2, "shell32_243.ico");
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(718, 360);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(86, 23);
            this.btnSelect.TabIndex = 16;
            this.btnSelect.Text = "Select Game";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(21, 363);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(666, 20);
            this.lblStatus.TabIndex = 2;
            // 
            // optFtpModeSftp
            // 
            this.optFtpModeSftp.AutoSize = true;
            this.optFtpModeSftp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFtpModeSftp.Location = new System.Drawing.Point(72, 278);
            this.optFtpModeSftp.Name = "optFtpModeSftp";
            this.optFtpModeSftp.Size = new System.Drawing.Size(52, 17);
            this.optFtpModeSftp.TabIndex = 13;
            this.optFtpModeSftp.Text = "SFTP";
            this.optFtpModeSftp.UseVisualStyleBackColor = true;
            // 
            // optFtpModeFtp
            // 
            this.optFtpModeFtp.AutoSize = true;
            this.optFtpModeFtp.Checked = true;
            this.optFtpModeFtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFtpModeFtp.Location = new System.Drawing.Point(21, 278);
            this.optFtpModeFtp.Name = "optFtpModeFtp";
            this.optFtpModeFtp.Size = new System.Drawing.Size(45, 17);
            this.optFtpModeFtp.TabIndex = 12;
            this.optFtpModeFtp.TabStop = true;
            this.optFtpModeFtp.Text = "FTP";
            this.optFtpModeFtp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPasswordVisibility
            // 
            this.chkPasswordVisibility.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPasswordVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkPasswordVisibility.Image = ((System.Drawing.Image)(resources.GetObject("chkPasswordVisibility.Image")));
            this.chkPasswordVisibility.Location = new System.Drawing.Point(232, 225);
            this.chkPasswordVisibility.Name = "chkPasswordVisibility";
            this.chkPasswordVisibility.Size = new System.Drawing.Size(20, 20);
            this.chkPasswordVisibility.TabIndex = 10;
            this.chkPasswordVisibility.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPasswordVisibility.UseVisualStyleBackColor = false;
            this.chkPasswordVisibility.CheckedChanged += new System.EventHandler(this.chkPasswordVisibility_CheckedChanged);
            // 
            // udFTPPort
            // 
            this.udFTPPort.Location = new System.Drawing.Point(18, 128);
            this.udFTPPort.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.udFTPPort.Name = "udFTPPort";
            this.udFTPPort.Size = new System.Drawing.Size(60, 20);
            this.udFTPPort.TabIndex = 5;
            this.udFTPPort.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.Location = new System.Drawing.Point(18, 225);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.PasswordChar = '●';
            this.txtFTPPassword.Size = new System.Drawing.Size(214, 20);
            this.txtFTPPassword.TabIndex = 9;
            this.txtFTPPassword.TextChanged += new System.EventHandler(this.txtFTPPassword_TextChanged);
            // 
            // txtFTPUsername
            // 
            this.txtFTPUsername.Location = new System.Drawing.Point(16, 179);
            this.txtFTPUsername.Name = "txtFTPUsername";
            this.txtFTPUsername.Size = new System.Drawing.Size(216, 20);
            this.txtFTPUsername.TabIndex = 7;
            // 
            // lblFTPPassword
            // 
            this.lblFTPPassword.AutoSize = true;
            this.lblFTPPassword.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPPassword.Location = new System.Drawing.Point(15, 207);
            this.lblFTPPassword.Name = "lblFTPPassword";
            this.lblFTPPassword.Size = new System.Drawing.Size(69, 15);
            this.lblFTPPassword.TabIndex = 8;
            this.lblFTPPassword.Text = "Password";
            this.lblFTPPassword.Click += new System.EventHandler(this.lblFTPPassword_Click);
            // 
            // lblFTPUsername
            // 
            this.lblFTPUsername.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPUsername.Location = new System.Drawing.Point(15, 154);
            this.lblFTPUsername.Name = "lblFTPUsername";
            this.lblFTPUsername.Size = new System.Drawing.Size(74, 22);
            this.lblFTPUsername.TabIndex = 6;
            this.lblFTPUsername.Text = "Username";
            this.lblFTPUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFTPAddress
            // 
            this.txtFTPAddress.Location = new System.Drawing.Point(18, 84);
            this.txtFTPAddress.Name = "txtFTPAddress";
            this.txtFTPAddress.Size = new System.Drawing.Size(214, 20);
            this.txtFTPAddress.TabIndex = 3;
            this.txtFTPAddress.Validating += new System.ComponentModel.CancelEventHandler(this.txtFTPAddress_Validating);
            // 
            // lblFTPPort
            // 
            this.lblFTPPort.AutoSize = true;
            this.lblFTPPort.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPPort.Location = new System.Drawing.Point(15, 107);
            this.lblFTPPort.Name = "lblFTPPort";
            this.lblFTPPort.Size = new System.Drawing.Size(33, 15);
            this.lblFTPPort.TabIndex = 4;
            this.lblFTPPort.Text = "Port";
            // 
            // lblFTPHost
            // 
            this.lblFTPHost.BackColor = System.Drawing.SystemColors.Control;
            this.lblFTPHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFTPHost.Location = new System.Drawing.Point(15, 62);
            this.lblFTPHost.Name = "lblFTPHost";
            this.lblFTPHost.Size = new System.Drawing.Size(217, 19);
            this.lblFTPHost.TabIndex = 2;
            this.lblFTPHost.Text = "Server Address                        ";
            this.lblFTPHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(21, 325);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(211, 23);
            this.btnConnect.TabIndex = 14;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(18, 34);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(214, 20);
            this.txtServerName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Server Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmFtpFileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 392);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.optFtpModeSftp);
            this.Controls.Add(this.optFtpModeFtp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkPasswordVisibility);
            this.Controls.Add(this.udFTPPort);
            this.Controls.Add(this.txtFTPPassword);
            this.Controls.Add(this.txtFTPUsername);
            this.Controls.Add(this.lblFTPPassword);
            this.Controls.Add(this.lblFTPUsername);
            this.Controls.Add(this.txtFTPAddress);
            this.Controls.Add(this.lblFTPPort);
            this.Controls.Add(this.lblFTPHost);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lvwFileBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmFtpFileBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP Server Browser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFtpFileBrowser_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.udFTPPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwFileBrowser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList imagesFileBrowser;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RadioButton optFtpModeSftp;
        private System.Windows.Forms.RadioButton optFtpModeFtp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPasswordVisibility;
        private System.Windows.Forms.NumericUpDown udFTPPort;
        private System.Windows.Forms.TextBox txtFTPPassword;
        private System.Windows.Forms.TextBox txtFTPUsername;
        private System.Windows.Forms.Label lblFTPPassword;
        private System.Windows.Forms.Label lblFTPUsername;
        private System.Windows.Forms.TextBox txtFTPAddress;
        private System.Windows.Forms.Label lblFTPPort;
        private System.Windows.Forms.Label lblFTPHost;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
    }
}