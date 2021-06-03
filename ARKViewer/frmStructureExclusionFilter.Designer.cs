namespace ARKViewer
{
    partial class frmStructureExclusionFilter
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlCreatureInventory = new System.Windows.Forms.Panel();
            this.lstStructureFilter = new System.Windows.Forms.CheckedListBox();
            this.chkApplyFilter = new System.Windows.Forms.CheckBox();
            this.lblCreatureFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblWindowTitle = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.pnlCreatureInventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(296, 483);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlCreatureInventory
            // 
            this.pnlCreatureInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCreatureInventory.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlCreatureInventory.Controls.Add(this.lstStructureFilter);
            this.pnlCreatureInventory.Controls.Add(this.chkApplyFilter);
            this.pnlCreatureInventory.Controls.Add(this.lblCreatureFilter);
            this.pnlCreatureInventory.Controls.Add(this.txtFilter);
            this.pnlCreatureInventory.Location = new System.Drawing.Point(12, 46);
            this.pnlCreatureInventory.Name = "pnlCreatureInventory";
            this.pnlCreatureInventory.Size = new System.Drawing.Size(359, 420);
            this.pnlCreatureInventory.TabIndex = 16;
            // 
            // lstStructureFilter
            // 
            this.lstStructureFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStructureFilter.FormattingEnabled = true;
            this.lstStructureFilter.Location = new System.Drawing.Point(18, 15);
            this.lstStructureFilter.Name = "lstStructureFilter";
            this.lstStructureFilter.Size = new System.Drawing.Size(321, 364);
            this.lstStructureFilter.TabIndex = 23;
            this.lstStructureFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstStructureFilter_ItemCheck);
            // 
            // chkApplyFilter
            // 
            this.chkApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkApplyFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkApplyFilter.Image = global::ARKViewer.Properties.Resources.button_filter;
            this.chkApplyFilter.Location = new System.Drawing.Point(306, 385);
            this.chkApplyFilter.Name = "chkApplyFilter";
            this.chkApplyFilter.Size = new System.Drawing.Size(33, 27);
            this.chkApplyFilter.TabIndex = 22;
            this.chkApplyFilter.UseVisualStyleBackColor = true;
            this.chkApplyFilter.CheckedChanged += new System.EventHandler(this.chkApplyFilter_CheckedChanged);
            // 
            // lblCreatureFilter
            // 
            this.lblCreatureFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCreatureFilter.AutoSize = true;
            this.lblCreatureFilter.Location = new System.Drawing.Point(15, 391);
            this.lblCreatureFilter.Name = "lblCreatureFilter";
            this.lblCreatureFilter.Size = new System.Drawing.Size(29, 13);
            this.lblCreatureFilter.TabIndex = 1;
            this.lblCreatureFilter.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(50, 388);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(250, 20);
            this.txtFilter.TabIndex = 2;
            // 
            // lblWindowTitle
            // 
            this.lblWindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWindowTitle.ForeColor = System.Drawing.Color.Black;
            this.lblWindowTitle.Location = new System.Drawing.Point(12, 9);
            this.lblWindowTitle.Name = "lblWindowTitle";
            this.lblWindowTitle.Size = new System.Drawing.Size(301, 31);
            this.lblWindowTitle.TabIndex = 18;
            this.lblWindowTitle.Text = "Structure Exclusions";
            this.lblWindowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(215, 483);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 19;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // frmStructureExclusionFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 518);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblWindowTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlCreatureInventory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmStructureExclusionFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Structure Exclusions";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStructureExclusionFilter_FormClosed);
            this.pnlCreatureInventory.ResumeLayout(false);
            this.pnlCreatureInventory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlCreatureInventory;
        private System.Windows.Forms.CheckBox chkApplyFilter;
        private System.Windows.Forms.Label lblCreatureFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblWindowTitle;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckedListBox lstStructureFilter;
    }
}