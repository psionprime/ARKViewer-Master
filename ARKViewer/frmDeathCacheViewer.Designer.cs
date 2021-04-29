
namespace ARKViewer
{
    partial class frmDeathCacheViewer
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
            this.pnlCreatureInventory = new System.Windows.Forms.Panel();
            this.chkApplyFilter = new System.Windows.Forms.CheckBox();
            this.lblCreatureFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lvwInventory = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblWindowTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTribeName = new System.Windows.Forms.Label();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.pnlCreatureInventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCreatureInventory
            // 
            this.pnlCreatureInventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreatureInventory.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlCreatureInventory.Controls.Add(this.chkApplyFilter);
            this.pnlCreatureInventory.Controls.Add(this.lblCreatureFilter);
            this.pnlCreatureInventory.Controls.Add(this.txtFilter);
            this.pnlCreatureInventory.Controls.Add(this.lvwInventory);
            this.pnlCreatureInventory.Location = new System.Drawing.Point(12, 71);
            this.pnlCreatureInventory.Name = "pnlCreatureInventory";
            this.pnlCreatureInventory.Size = new System.Drawing.Size(669, 346);
            this.pnlCreatureInventory.TabIndex = 25;
            // 
            // chkApplyFilter
            // 
            this.chkApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilter.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilter.Location = new System.Drawing.Point(616, 311);
            this.chkApplyFilter.Name = "chkApplyFilter";
            this.chkApplyFilter.Size = new System.Drawing.Size(33, 27);
            this.chkApplyFilter.TabIndex = 22;
            this.chkApplyFilter.UseVisualStyleBackColor = true;
            this.chkApplyFilter.CheckedChanged += new System.EventHandler(this.chkApplyFilterDinos_CheckedChanged);
            // 
            // lblCreatureFilter
            // 
            this.lblCreatureFilter.AutoSize = true;
            this.lblCreatureFilter.Location = new System.Drawing.Point(26, 317);
            this.lblCreatureFilter.Name = "lblCreatureFilter";
            this.lblCreatureFilter.Size = new System.Drawing.Size(29, 13);
            this.lblCreatureFilter.TabIndex = 1;
            this.lblCreatureFilter.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(72, 314);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(539, 20);
            this.txtFilter.TabIndex = 2;
            // 
            // lvwInventory
            // 
            this.lvwInventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwInventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader8,
            this.columnHeader12});
            this.lvwInventory.FullRowSelect = true;
            this.lvwInventory.HideSelection = false;
            this.lvwInventory.LargeImageList = this.imageList1;
            this.lvwInventory.Location = new System.Drawing.Point(24, 19);
            this.lvwInventory.Name = "lvwInventory";
            this.lvwInventory.Size = new System.Drawing.Size(625, 288);
            this.lvwInventory.SmallImageList = this.imageList1;
            this.lvwInventory.TabIndex = 0;
            this.lvwInventory.UseCompatibleStateImageBehavior = false;
            this.lvwInventory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 226;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Category";
            this.columnHeader8.Width = 270;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Qty";
            this.columnHeader12.Width = 48;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(30, 30);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lblWindowTitle
            // 
            this.lblWindowTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWindowTitle.ForeColor = System.Drawing.Color.Black;
            this.lblWindowTitle.Location = new System.Drawing.Point(503, 11);
            this.lblWindowTitle.Name = "lblWindowTitle";
            this.lblWindowTitle.Size = new System.Drawing.Size(178, 31);
            this.lblWindowTitle.TabIndex = 24;
            this.lblWindowTitle.Text = "Death Cache";
            this.lblWindowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(606, 428);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTribeName
            // 
            this.lblTribeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTribeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTribeName.ForeColor = System.Drawing.Color.DimGray;
            this.lblTribeName.Location = new System.Drawing.Point(14, 42);
            this.lblTribeName.Name = "lblTribeName";
            this.lblTribeName.Size = new System.Drawing.Size(389, 19);
            this.lblTribeName.TabIndex = 27;
            this.lblTribeName.Text = "Tribe Name";
            this.lblTribeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.Black;
            this.lblPlayerName.Location = new System.Drawing.Point(12, 11);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(391, 31);
            this.lblPlayerName.TabIndex = 26;
            this.lblPlayerName.Text = "Player Name";
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDeathCacheViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(693, 462);
            this.Controls.Add(this.pnlCreatureInventory);
            this.Controls.Add(this.lblWindowTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTribeName);
            this.Controls.Add(this.lblPlayerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDeathCacheViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Death Cache Inventory";
            this.pnlCreatureInventory.ResumeLayout(false);
            this.pnlCreatureInventory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCreatureInventory;
        private System.Windows.Forms.CheckBox chkApplyFilter;
        private System.Windows.Forms.Label lblCreatureFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ListView lvwInventory;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblWindowTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTribeName;
        private System.Windows.Forms.Label lblPlayerName;
    }
}