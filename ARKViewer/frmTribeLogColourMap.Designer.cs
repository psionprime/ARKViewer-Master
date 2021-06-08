
namespace ARKViewer
{
    partial class frmTribeLogColourMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTribeLogColourMap));
            this.lvwTextColours = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlGameColour = new System.Windows.Forms.Panel();
            this.pnlCustomColour = new System.Windows.Forms.Panel();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlForeground = new System.Windows.Forms.Panel();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwTextColours
            // 
            this.lvwTextColours.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwTextColours.HideSelection = false;
            this.lvwTextColours.Location = new System.Drawing.Point(12, 157);
            this.lvwTextColours.Name = "lvwTextColours";
            this.lvwTextColours.Size = new System.Drawing.Size(533, 193);
            this.lvwTextColours.TabIndex = 0;
            this.lvwTextColours.UseCompatibleStateImageBehavior = false;
            this.lvwTextColours.View = System.Windows.Forms.View.Details;
            this.lvwTextColours.SelectedIndexChanged += new System.EventHandler(this.lvwTextColours_SelectedIndexChanged);
            this.lvwTextColours.Click += new System.EventHandler(this.lvwTextColours_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Game Colour";
            this.columnHeader1.Width = 263;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Custom Colour";
            this.columnHeader2.Width = 205;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(386, 408);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(468, 408);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Background Colour:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Foreground Colour:";
            // 
            // pnlGameColour
            // 
            this.pnlGameColour.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGameColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGameColour.Enabled = false;
            this.pnlGameColour.Location = new System.Drawing.Point(15, 355);
            this.pnlGameColour.Name = "pnlGameColour";
            this.pnlGameColour.Size = new System.Drawing.Size(243, 30);
            this.pnlGameColour.TabIndex = 31;
            this.pnlGameColour.Click += new System.EventHandler(this.pnlGameColour_Click);
            // 
            // pnlCustomColour
            // 
            this.pnlCustomColour.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCustomColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomColour.Enabled = false;
            this.pnlCustomColour.Location = new System.Drawing.Point(269, 355);
            this.pnlCustomColour.Name = "pnlCustomColour";
            this.pnlCustomColour.Size = new System.Drawing.Size(208, 30);
            this.pnlCustomColour.TabIndex = 32;
            this.pnlCustomColour.Click += new System.EventHandler(this.pnlCustomColour_Click);
            this.pnlCustomColour.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCustomColour_Paint);
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.Black;
            this.pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBackground.Location = new System.Drawing.Point(12, 27);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(251, 30);
            this.pnlBackground.TabIndex = 33;
            this.pnlBackground.Click += new System.EventHandler(this.pnlBackground_Click);
            // 
            // pnlForeground
            // 
            this.pnlForeground.BackColor = System.Drawing.Color.Black;
            this.pnlForeground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlForeground.Location = new System.Drawing.Point(12, 84);
            this.pnlForeground.Name = "pnlForeground";
            this.pnlForeground.Size = new System.Drawing.Size(251, 30);
            this.pnlForeground.TabIndex = 34;
            this.pnlForeground.Click += new System.EventHandler(this.pnlForeground_Click);
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Enabled = false;
            this.btnAddUpdate.Location = new System.Drawing.Point(483, 355);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(60, 29);
            this.btnAddUpdate.TabIndex = 35;
            this.btnAddUpdate.Text = "UPDATE";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Text Colours";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(455, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // frmTribeLogColourMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 443);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddUpdate);
            this.Controls.Add(this.pnlForeground);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.pnlCustomColour);
            this.Controls.Add(this.pnlGameColour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvwTextColours);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTribeLogColourMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tribe Log Colour Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTribeLogColourMap_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwTextColours;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlGameColour;
        private System.Windows.Forms.Panel pnlCustomColour;
        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlForeground;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}