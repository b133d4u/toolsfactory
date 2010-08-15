namespace HackMew.ToolsFactory
{
    partial class formAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formAbout));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblShortDescription = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lnkWebsite = new System.Windows.Forms.LinkLabel();
            this.lnkDonate = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbxGPL = new System.Windows.Forms.PictureBox();
            this.lblProgramName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGPL)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(14, 30);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "version";
            // 
            // lblShortDescription
            // 
            this.lblShortDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblShortDescription.Location = new System.Drawing.Point(14, 83);
            this.lblShortDescription.Name = "lblShortDescription";
            this.lblShortDescription.Size = new System.Drawing.Size(338, 16);
            this.lblShortDescription.TabIndex = 3;
            this.lblShortDescription.Text = "Short description";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Enabled = false;
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCopyright.Location = new System.Drawing.Point(14, 99);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(54, 13);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "Copyright";
            // 
            // lnkWebsite
            // 
            this.lnkWebsite.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.lnkWebsite.AutoSize = true;
            this.lnkWebsite.BackColor = System.Drawing.Color.Transparent;
            this.lnkWebsite.LinkColor = System.Drawing.SystemColors.Highlight;
            this.lnkWebsite.Location = new System.Drawing.Point(15, 129);
            this.lnkWebsite.Name = "lnkWebsite";
            this.lnkWebsite.Size = new System.Drawing.Size(46, 13);
            this.lnkWebsite.TabIndex = 5;
            this.lnkWebsite.TabStop = true;
            this.lnkWebsite.Text = "Website";
            this.lnkWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebsite_LinkClicked);
            // 
            // lnkDonate
            // 
            this.lnkDonate.ActiveLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.lnkDonate.AutoSize = true;
            this.lnkDonate.BackColor = System.Drawing.Color.Transparent;
            this.lnkDonate.LinkColor = System.Drawing.SystemColors.Highlight;
            this.lnkDonate.Location = new System.Drawing.Point(15, 146);
            this.lnkDonate.Name = "lnkDonate";
            this.lnkDonate.Size = new System.Drawing.Size(42, 13);
            this.lnkDonate.TabIndex = 6;
            this.lnkDonate.TabStop = true;
            this.lnkDonate.Text = "Donate";
            this.lnkDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWebsite_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(266, 142);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbxGPL
            // 
            this.pbxGPL.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbxGPL.BackgroundImage")));
            this.pbxGPL.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbxGPL.Location = new System.Drawing.Point(215, 13);
            this.pbxGPL.Name = "pbxGPL";
            this.pbxGPL.Size = new System.Drawing.Size(136, 58);
            this.pbxGPL.TabIndex = 7;
            this.pbxGPL.TabStop = false;
            // 
            // lblProgramName
            // 
            this.lblProgramName.BackColor = System.Drawing.Color.Transparent;
            this.lblProgramName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramName.Location = new System.Drawing.Point(14, 13);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(194, 15);
            this.lblProgramName.TabIndex = 9;
            this.lblProgramName.Text = "Program name";
            // 
            // formAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(364, 177);
            this.Controls.Add(this.lblProgramName);
            this.Controls.Add(this.pbxGPL);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lnkDonate);
            this.Controls.Add(this.lnkWebsite);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblShortDescription);
            this.Controls.Add(this.lblVersion);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pbxGPL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblShortDescription;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel lnkWebsite;
        private System.Windows.Forms.LinkLabel lnkDonate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbxGPL;
        private System.Windows.Forms.Label lblProgramName;
    }
}