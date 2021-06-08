namespace ARKViewer
{
    partial class frmMarkerEditor
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
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblBorderSize = new System.Windows.Forms.Label();
            this.lblBorderColour = new System.Windows.Forms.Label();
            this.lblBackgroundColour = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.udBorderSize = new System.Windows.Forms.NumericUpDown();
            this.udLat = new System.Windows.Forms.NumericUpDown();
            this.lblLat = new System.Windows.Forms.Label();
            this.udLon = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBorderColour = new System.Windows.Forms.Panel();
            this.pnlBackgroundColour = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLon)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Image = global::ARKViewer.Properties.Resources.marker_0;
            this.picIcon.Location = new System.Drawing.Point(95, 269);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(64, 64);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 2;
            this.picIcon.TabStop = false;
            this.picIcon.Click += new System.EventHandler(this.picIcon_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DimGray;
            this.lblName.Location = new System.Drawing.Point(10, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(239, 23);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBorderSize
            // 
            this.lblBorderSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBorderSize.BackColor = System.Drawing.SystemColors.Control;
            this.lblBorderSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorderSize.ForeColor = System.Drawing.Color.DimGray;
            this.lblBorderSize.Location = new System.Drawing.Point(11, 58);
            this.lblBorderSize.Name = "lblBorderSize";
            this.lblBorderSize.Size = new System.Drawing.Size(238, 23);
            this.lblBorderSize.TabIndex = 2;
            this.lblBorderSize.Text = "Border Size";
            this.lblBorderSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBorderColour
            // 
            this.lblBorderColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBorderColour.BackColor = System.Drawing.SystemColors.Control;
            this.lblBorderColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorderColour.ForeColor = System.Drawing.Color.DimGray;
            this.lblBorderColour.Location = new System.Drawing.Point(12, 114);
            this.lblBorderColour.Name = "lblBorderColour";
            this.lblBorderColour.Size = new System.Drawing.Size(237, 23);
            this.lblBorderColour.TabIndex = 4;
            this.lblBorderColour.Text = "Border Colour";
            this.lblBorderColour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBackgroundColour
            // 
            this.lblBackgroundColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackgroundColour.BackColor = System.Drawing.SystemColors.Control;
            this.lblBackgroundColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackgroundColour.ForeColor = System.Drawing.Color.DimGray;
            this.lblBackgroundColour.Location = new System.Drawing.Point(12, 175);
            this.lblBackgroundColour.Name = "lblBackgroundColour";
            this.lblBackgroundColour.Size = new System.Drawing.Size(237, 23);
            this.lblBackgroundColour.TabIndex = 6;
            this.lblBackgroundColour.Text = "Background Colour";
            this.lblBackgroundColour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIcon
            // 
            this.lblIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIcon.BackColor = System.Drawing.SystemColors.Control;
            this.lblIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon.ForeColor = System.Drawing.Color.DimGray;
            this.lblIcon.Location = new System.Drawing.Point(12, 237);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(237, 23);
            this.lblIcon.TabIndex = 8;
            this.lblIcon.Text = "Icon";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(13, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(236, 22);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // udBorderSize
            // 
            this.udBorderSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udBorderSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udBorderSize.Location = new System.Drawing.Point(14, 86);
            this.udBorderSize.Name = "udBorderSize";
            this.udBorderSize.Size = new System.Drawing.Size(235, 22);
            this.udBorderSize.TabIndex = 3;
            this.udBorderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // udLat
            // 
            this.udLat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udLat.DecimalPlaces = 2;
            this.udLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udLat.Location = new System.Drawing.Point(14, 364);
            this.udLat.Name = "udLat";
            this.udLat.Size = new System.Drawing.Size(235, 22);
            this.udLat.TabIndex = 12;
            this.udLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udLat.Enter += new System.EventHandler(this.udLat_Enter);
            // 
            // lblLat
            // 
            this.lblLat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLat.BackColor = System.Drawing.SystemColors.Control;
            this.lblLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLat.ForeColor = System.Drawing.Color.DimGray;
            this.lblLat.Location = new System.Drawing.Point(11, 340);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(238, 23);
            this.lblLat.TabIndex = 11;
            this.lblLat.Text = "Latitude";
            this.lblLat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // udLon
            // 
            this.udLon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udLon.DecimalPlaces = 2;
            this.udLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udLon.Location = new System.Drawing.Point(14, 413);
            this.udLon.Name = "udLon";
            this.udLon.Size = new System.Drawing.Size(235, 22);
            this.udLon.TabIndex = 14;
            this.udLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udLon.Enter += new System.EventHandler(this.udLon_Enter);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(11, 389);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(238, 23);
            this.label7.TabIndex = 13;
            this.label7.Text = "Longitude";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(93, 449);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(174, 449);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlBorderColour
            // 
            this.pnlBorderColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBorderColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBorderColour.Location = new System.Drawing.Point(13, 141);
            this.pnlBorderColour.Name = "pnlBorderColour";
            this.pnlBorderColour.Size = new System.Drawing.Size(236, 27);
            this.pnlBorderColour.TabIndex = 5;
            this.pnlBorderColour.Click += new System.EventHandler(this.pnlBorderColour_Click);
            // 
            // pnlBackgroundColour
            // 
            this.pnlBackgroundColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackgroundColour.Location = new System.Drawing.Point(12, 204);
            this.pnlBackgroundColour.Name = "pnlBackgroundColour";
            this.pnlBackgroundColour.Size = new System.Drawing.Size(236, 27);
            this.pnlBackgroundColour.TabIndex = 7;
            this.pnlBackgroundColour.Click += new System.EventHandler(this.pnlBackgroundColour_Click);
            // 
            // frmMarkerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 486);
            this.Controls.Add(this.pnlBackgroundColour);
            this.Controls.Add(this.pnlBorderColour);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.udLon);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.udLat);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.udBorderSize);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblIcon);
            this.Controls.Add(this.lblBackgroundColour);
            this.Controls.Add(this.lblBorderColour);
            this.Controls.Add(this.lblBorderSize);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMarkerEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marker Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMarkerEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBorderSize;
        private System.Windows.Forms.Label lblBorderColour;
        private System.Windows.Forms.Label lblBackgroundColour;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown udBorderSize;
        private System.Windows.Forms.NumericUpDown udLat;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.NumericUpDown udLon;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlBorderColour;
        private System.Windows.Forms.Panel pnlBackgroundColour;
    }
}