
namespace ARKViewer
{
    partial class frmColourEditor
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
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.btnCcancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.udId = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColour = new System.Windows.Forms.Panel();
            this.udR = new System.Windows.Forms.NumericUpDown();
            this.udG = new System.Windows.Forms.NumericUpDown();
            this.udB = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.udId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udB)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.BackColor = System.Drawing.SystemColors.Control;
            this.lblDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayName.ForeColor = System.Drawing.Color.DimGray;
            this.lblDisplayName.Location = new System.Drawing.Point(13, 71);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(292, 23);
            this.lblDisplayName.TabIndex = 8;
            this.lblDisplayName.Text = "RGB";
            this.lblDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClassName
            // 
            this.lblClassName.BackColor = System.Drawing.SystemColors.Control;
            this.lblClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassName.ForeColor = System.Drawing.Color.DimGray;
            this.lblClassName.Location = new System.Drawing.Point(12, 9);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(293, 23);
            this.lblClassName.TabIndex = 6;
            this.lblClassName.Text = "Id";
            this.lblClassName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCcancel
            // 
            this.btnCcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCcancel.Location = new System.Drawing.Point(230, 249);
            this.btnCcancel.Name = "btnCcancel";
            this.btnCcancel.Size = new System.Drawing.Size(75, 23);
            this.btnCcancel.TabIndex = 11;
            this.btnCcancel.Text = "Cancel";
            this.btnCcancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(143, 249);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // udId
            // 
            this.udId.Location = new System.Drawing.Point(13, 36);
            this.udId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.udId.Name = "udId";
            this.udId.Size = new System.Drawing.Size(290, 20);
            this.udId.TabIndex = 12;
            this.udId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udId.ValueChanged += new System.EventHandler(this.udId_ValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(13, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Colour";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlColour
            // 
            this.pnlColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColour.Location = new System.Drawing.Point(13, 158);
            this.pnlColour.Name = "pnlColour";
            this.pnlColour.Size = new System.Drawing.Size(290, 25);
            this.pnlColour.TabIndex = 14;
            this.pnlColour.Click += new System.EventHandler(this.pnlColour_Click);
            // 
            // udR
            // 
            this.udR.Location = new System.Drawing.Point(16, 97);
            this.udR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udR.Name = "udR";
            this.udR.Size = new System.Drawing.Size(92, 20);
            this.udR.TabIndex = 15;
            this.udR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udR.ValueChanged += new System.EventHandler(this.udR_ValueChanged);
            // 
            // udG
            // 
            this.udG.Location = new System.Drawing.Point(113, 97);
            this.udG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udG.Name = "udG";
            this.udG.Size = new System.Drawing.Size(92, 20);
            this.udG.TabIndex = 16;
            this.udG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udG.ValueChanged += new System.EventHandler(this.udG_ValueChanged);
            // 
            // udB
            // 
            this.udB.Location = new System.Drawing.Point(211, 97);
            this.udB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udB.Name = "udB";
            this.udB.Size = new System.Drawing.Size(92, 20);
            this.udB.TabIndex = 17;
            this.udB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udB.ValueChanged += new System.EventHandler(this.udB_ValueChanged);
            // 
            // frmColourEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 284);
            this.Controls.Add(this.udB);
            this.Controls.Add(this.udG);
            this.Controls.Add(this.udR);
            this.Controls.Add(this.pnlColour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.udId);
            this.Controls.Add(this.lblDisplayName);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.btnCcancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmColourEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colour Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmColourEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.udId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Button btnCcancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown udId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlColour;
        private System.Windows.Forms.NumericUpDown udR;
        private System.Windows.Forms.NumericUpDown udG;
        private System.Windows.Forms.NumericUpDown udB;
    }
}