
namespace ARKViewer
{
    partial class frmMapView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapView));
            this.pnlZoom = new System.Windows.Forms.Panel();
            this.trackZoom = new System.Windows.Forms.TrackBar();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.btnMapMarkers = new System.Windows.Forms.Button();
            this.btnMapStructures = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).BeginInit();
            this.pnlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlZoom
            // 
            this.pnlZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlZoom.BackColor = System.Drawing.Color.MintCream;
            this.pnlZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlZoom.Controls.Add(this.trackZoom);
            this.pnlZoom.Location = new System.Drawing.Point(4, 648);
            this.pnlZoom.Name = "pnlZoom";
            this.pnlZoom.Size = new System.Drawing.Size(628, 26);
            this.pnlZoom.TabIndex = 46;
            // 
            // trackZoom
            // 
            this.trackZoom.AutoSize = false;
            this.trackZoom.LargeChange = 10;
            this.trackZoom.Location = new System.Drawing.Point(10, 1);
            this.trackZoom.Maximum = 200;
            this.trackZoom.Minimum = 1;
            this.trackZoom.Name = "trackZoom";
            this.trackZoom.Size = new System.Drawing.Size(605, 23);
            this.trackZoom.TabIndex = 3;
            this.trackZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackZoom.Value = 5;
            this.trackZoom.ValueChanged += new System.EventHandler(this.trackZoom_ValueChanged);
            // 
            // pnlMap
            // 
            this.pnlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMap.AutoScroll = true;
            this.pnlMap.BackColor = System.Drawing.Color.MintCream;
            this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMap.Controls.Add(this.picMap);
            this.pnlMap.Location = new System.Drawing.Point(4, 64);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(628, 581);
            this.pnlMap.TabIndex = 42;
            // 
            // picMap
            // 
            this.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMap.Location = new System.Drawing.Point(2, 2);
            this.picMap.Margin = new System.Windows.Forms.Padding(0);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(256, 256);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            this.picMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseClick);
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            // 
            // btnMapMarkers
            // 
            this.btnMapMarkers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMapMarkers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMapMarkers.Image = ((System.Drawing.Image)(resources.GetObject("btnMapMarkers.Image")));
            this.btnMapMarkers.Location = new System.Drawing.Point(578, 8);
            this.btnMapMarkers.Name = "btnMapMarkers";
            this.btnMapMarkers.Size = new System.Drawing.Size(50, 50);
            this.btnMapMarkers.TabIndex = 48;
            this.btnMapMarkers.UseVisualStyleBackColor = true;
            this.btnMapMarkers.Click += new System.EventHandler(this.btnMapMarkers_Click);
            // 
            // btnMapStructures
            // 
            this.btnMapStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMapStructures.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMapStructures.Image = ((System.Drawing.Image)(resources.GetObject("btnMapStructures.Image")));
            this.btnMapStructures.Location = new System.Drawing.Point(522, 8);
            this.btnMapStructures.Name = "btnMapStructures";
            this.btnMapStructures.Size = new System.Drawing.Size(50, 50);
            this.btnMapStructures.TabIndex = 47;
            this.btnMapStructures.UseVisualStyleBackColor = true;
            this.btnMapStructures.Click += new System.EventHandler(this.btnMapStructures_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(60, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 31);
            this.lblTitle.TabIndex = 49;
            this.lblTitle.Text = "Map View";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ARKViewer.Properties.Resources.ModernXP_73_Globe_icon;
            this.pictureBox1.Location = new System.Drawing.Point(4, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // frmMapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(637, 680);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnMapMarkers);
            this.Controls.Add(this.btnMapStructures);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.pnlZoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmMapView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMapView_FormClosed);
            this.pnlZoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackZoom)).EndInit();
            this.pnlMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Panel pnlZoom;
        private System.Windows.Forms.TrackBar trackZoom;
        private System.Windows.Forms.Button btnMapMarkers;
        private System.Windows.Forms.Button btnMapStructures;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}