namespace FreeSpaceFinder
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.mmMain = new System.Windows.Forms.MainMenu(this.components);
            this.miFile = new System.Windows.Forms.MenuItem();
            this.miOpenROM = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miEdit = new System.Windows.Forms.MenuItem();
            this.miFind = new System.Windows.Forms.MenuItem();
            this.miFindNext = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.miCopy = new System.Windows.Forms.MenuItem();
            this.miClear = new System.Windows.Forms.MenuItem();
            this.miHelp = new System.Windows.Forms.MenuItem();
            this.miReportIssue = new System.Windows.Forms.MenuItem();
            this.miUpdates = new System.Windows.Forms.MenuItem();
            this.miCheckNow = new System.Windows.Forms.MenuItem();
            this.miAutomaticallyCheck = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miLanguage = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.miWebsite = new System.Windows.Forms.MenuItem();
            this.miDonate = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.ttGame = new System.Windows.Forms.ToolTip(this.components);
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.gbxSearchOptions = new System.Windows.Forms.GroupBox();
            this.tbxOffset = new HackMew.ToolsFactory.TextBoxEx();
            this.tbxFreeSpaceByte = new HackMew.ToolsFactory.TextBoxEx();
            this.lblNote = new System.Windows.Forms.Label();
            this.nudSkipInterval = new HackMew.ToolsFactory.NumericUpDownEx();
            this.nudNeededBytes = new HackMew.ToolsFactory.NumericUpDownEx();
            this.lblSkipInterval = new System.Windows.Forms.Label();
            this.rbtnSearchFromOffset = new System.Windows.Forms.RadioButton();
            this.rbtnSearchFromBeginning = new System.Windows.Forms.RadioButton();
            this.lblNeededBytes = new System.Windows.Forms.Label();
            this.lblFreeSpaceByte = new System.Windows.Forms.Label();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.lblGame = new System.Windows.Forms.Label();
            this.gbxDecHex = new System.Windows.Forms.GroupBox();
            this.tbxHex = new HackMew.ToolsFactory.TextBoxEx();
            this.tbxDec = new HackMew.ToolsFactory.TextBoxEx();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.lbxResults = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.gbxSearchOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNeededBytes)).BeginInit();
            this.gbxDecHex.SuspendLayout();
            this.SuspendLayout();
            // 
            // mmMain
            // 
            this.mmMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFile,
            this.miEdit,
            this.miHelp});
            // 
            // miFile
            // 
            this.miFile.Index = 0;
            this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miOpenROM,
            this.miExit});
            this.miFile.Tag = "miFile";
            this.miFile.Text = "File";
            // 
            // miOpenROM
            // 
            this.miOpenROM.Index = 0;
            this.miOpenROM.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.miOpenROM.Tag = "miOpenROM";
            this.miOpenROM.Text = "Open ROM...";
            this.miOpenROM.Click += new System.EventHandler(this.miOpenROM_Click);
            // 
            // miExit
            // 
            this.miExit.Index = 1;
            this.miExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.miExit.Tag = "miExit";
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miEdit
            // 
            this.miEdit.Index = 1;
            this.miEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miFind,
            this.miFindNext,
            this.menuItem8,
            this.miCopy,
            this.miClear});
            this.miEdit.Tag = "miEdit";
            this.miEdit.Text = "Edit";
            // 
            // miFind
            // 
            this.miFind.Enabled = false;
            this.miFind.Index = 0;
            this.miFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.miFind.Tag = "miFind";
            this.miFind.Text = "Find";
            this.miFind.Click += new System.EventHandler(this.miFind_Click);
            // 
            // miFindNext
            // 
            this.miFindNext.Enabled = false;
            this.miFindNext.Index = 1;
            this.miFindNext.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.miFindNext.Tag = "miFindNext";
            this.miFindNext.Text = "Find next";
            this.miFindNext.Click += new System.EventHandler(this.miFindNext_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.Text = "-";
            // 
            // miCopy
            // 
            this.miCopy.Enabled = false;
            this.miCopy.Index = 3;
            this.miCopy.Tag = "miCopy";
            this.miCopy.Text = "Copy";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            // 
            // miClear
            // 
            this.miClear.Enabled = false;
            this.miClear.Index = 4;
            this.miClear.Tag = "miClear";
            this.miClear.Text = "Clear";
            this.miClear.Click += new System.EventHandler(this.miClear_Click);
            // 
            // miHelp
            // 
            this.miHelp.Index = 2;
            this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miReportIssue,
            this.miUpdates,
            this.menuItem1,
            this.miLanguage,
            this.menuItem7,
            this.miWebsite,
            this.miDonate,
            this.menuItem6,
            this.miAbout});
            this.miHelp.Tag = "miHelp";
            this.miHelp.Text = "Help";
            // 
            // miReportIssue
            // 
            this.miReportIssue.Index = 0;
            this.miReportIssue.Tag = "miReportIssue";
            this.miReportIssue.Text = "Report issue";
            this.miReportIssue.Click += new System.EventHandler(this.miReportIssue_Click);
            // 
            // miUpdates
            // 
            this.miUpdates.Index = 1;
            this.miUpdates.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miCheckNow,
            this.miAutomaticallyCheck});
            this.miUpdates.Tag = "miUpdates";
            this.miUpdates.Text = "Update";
            // 
            // miCheckNow
            // 
            this.miCheckNow.Index = 0;
            this.miCheckNow.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.miCheckNow.Tag = "miCheckNow";
            this.miCheckNow.Text = "Check now";
            this.miCheckNow.Click += new System.EventHandler(this.miCheckNow_Click);
            // 
            // miAutomaticallyCheck
            // 
            this.miAutomaticallyCheck.Checked = true;
            this.miAutomaticallyCheck.Index = 1;
            this.miAutomaticallyCheck.Tag = "miAutomaticallyCheck";
            this.miAutomaticallyCheck.Text = "Automatically check";
            this.miAutomaticallyCheck.Click += new System.EventHandler(this.miAutomaticallyCheck_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // miLanguage
            // 
            this.miLanguage.Enabled = false;
            this.miLanguage.Index = 3;
            this.miLanguage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.miLanguage.Tag = "miLanguage";
            this.miLanguage.Text = "Language";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.Text = "-";
            // 
            // miWebsite
            // 
            this.miWebsite.Index = 5;
            this.miWebsite.Tag = "miWebsite";
            this.miWebsite.Text = "Website";
            this.miWebsite.Click += new System.EventHandler(this.miWebsite_Click);
            // 
            // miDonate
            // 
            this.miDonate.Index = 6;
            this.miDonate.Tag = "miDonate";
            this.miDonate.Text = "Donate";
            this.miDonate.Click += new System.EventHandler(this.miDonate_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 7;
            this.menuItem6.Text = "-";
            // 
            // miAbout
            // 
            this.miAbout.Index = 8;
            this.miAbout.Tag = "miAbout";
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // ttGame
            // 
            this.ttGame.IsBalloon = true;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogo.Image")));
            this.pbxLogo.Location = new System.Drawing.Point(12, 12);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(291, 64);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxLogo.TabIndex = 34;
            this.pbxLogo.TabStop = false;
            // 
            // gbxSearchOptions
            // 
            this.gbxSearchOptions.Controls.Add(this.tbxOffset);
            this.gbxSearchOptions.Controls.Add(this.tbxFreeSpaceByte);
            this.gbxSearchOptions.Controls.Add(this.lblNote);
            this.gbxSearchOptions.Controls.Add(this.nudSkipInterval);
            this.gbxSearchOptions.Controls.Add(this.nudNeededBytes);
            this.gbxSearchOptions.Controls.Add(this.lblSkipInterval);
            this.gbxSearchOptions.Controls.Add(this.rbtnSearchFromOffset);
            this.gbxSearchOptions.Controls.Add(this.rbtnSearchFromBeginning);
            this.gbxSearchOptions.Controls.Add(this.lblNeededBytes);
            this.gbxSearchOptions.Controls.Add(this.lblFreeSpaceByte);
            this.gbxSearchOptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearchOptions.Location = new System.Drawing.Point(12, 82);
            this.gbxSearchOptions.Name = "gbxSearchOptions";
            this.gbxSearchOptions.Size = new System.Drawing.Size(242, 192);
            this.gbxSearchOptions.TabIndex = 41;
            this.gbxSearchOptions.TabStop = false;
            this.gbxSearchOptions.Text = "Search options";
            // 
            // tbxOffset
            // 
            this.tbxOffset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxOffset.Enabled = false;
            this.tbxOffset.Location = new System.Drawing.Point(155, 125);
            this.tbxOffset.Maximum = ((uint)(16777215u));
            this.tbxOffset.MaxLength = 6;
            this.tbxOffset.Minimum = ((uint)(1u));
            this.tbxOffset.Name = "tbxOffset";
            this.tbxOffset.NumbersOnly = HackMew.ToolsFactory.TextBoxEx.TextBoxExNumbersOnly.Hexadecimal;
            this.tbxOffset.Size = new System.Drawing.Size(70, 21);
            this.tbxOffset.TabIndex = 4;
            this.tbxOffset.Text = "800000";
            this.tbxOffset.Value = ((uint)(8388608u));
            // 
            // tbxFreeSpaceByte
            // 
            this.tbxFreeSpaceByte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxFreeSpaceByte.Location = new System.Drawing.Point(155, 21);
            this.tbxFreeSpaceByte.Maximum = ((uint)(255u));
            this.tbxFreeSpaceByte.MaxLength = 2;
            this.tbxFreeSpaceByte.Minimum = ((uint)(0u));
            this.tbxFreeSpaceByte.Name = "tbxFreeSpaceByte";
            this.tbxFreeSpaceByte.NumbersOnly = HackMew.ToolsFactory.TextBoxEx.TextBoxExNumbersOnly.Hexadecimal;
            this.tbxFreeSpaceByte.Size = new System.Drawing.Size(26, 21);
            this.tbxFreeSpaceByte.TabIndex = 0;
            this.tbxFreeSpaceByte.Text = "FF";
            this.tbxFreeSpaceByte.Value = ((uint)(255u));
            this.tbxFreeSpaceByte.TextChanged += new System.EventHandler(this.tbxFreeSpaceByte_TextChanged);
            // 
            // lblNote
            // 
            this.lblNote.Enabled = false;
            this.lblNote.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNote.Location = new System.Drawing.Point(16, 163);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(210, 13);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "All offsets are in hexadecimal.";
            // 
            // nudSkipInterval
            // 
            this.nudSkipInterval.Location = new System.Drawing.Point(155, 73);
            this.nudSkipInterval.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudSkipInterval.MaxLength = 5;
            this.nudSkipInterval.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudSkipInterval.Name = "nudSkipInterval";
            this.nudSkipInterval.Size = new System.Drawing.Size(71, 21);
            this.nudSkipInterval.TabIndex = 2;
            this.nudSkipInterval.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // nudNeededBytes
            // 
            this.nudNeededBytes.Location = new System.Drawing.Point(155, 47);
            this.nudNeededBytes.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudNeededBytes.MaxLength = 5;
            this.nudNeededBytes.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudNeededBytes.Name = "nudNeededBytes";
            this.nudNeededBytes.Size = new System.Drawing.Size(71, 21);
            this.nudNeededBytes.TabIndex = 1;
            this.nudNeededBytes.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudNeededBytes.ValueChanged += new System.EventHandler(this.nudNeededBytes_ValueChanged);
            // 
            // lblSkipInterval
            // 
            this.lblSkipInterval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkipInterval.Location = new System.Drawing.Point(16, 75);
            this.lblSkipInterval.Name = "lblSkipInterval";
            this.lblSkipInterval.Size = new System.Drawing.Size(133, 13);
            this.lblSkipInterval.TabIndex = 11;
            this.lblSkipInterval.Text = "Skip interval";
            // 
            // rbtnSearchFromOffset
            // 
            this.rbtnSearchFromOffset.Checked = true;
            this.rbtnSearchFromOffset.Enabled = false;
            this.rbtnSearchFromOffset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbtnSearchFromOffset.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSearchFromOffset.Location = new System.Drawing.Point(19, 127);
            this.rbtnSearchFromOffset.Name = "rbtnSearchFromOffset";
            this.rbtnSearchFromOffset.Size = new System.Drawing.Size(128, 16);
            this.rbtnSearchFromOffset.TabIndex = 3;
            this.rbtnSearchFromOffset.TabStop = true;
            this.rbtnSearchFromOffset.Text = "Search from offset";
            this.rbtnSearchFromOffset.UseVisualStyleBackColor = true;
            // 
            // rbtnSearchFromBeginning
            // 
            this.rbtnSearchFromBeginning.Enabled = false;
            this.rbtnSearchFromBeginning.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbtnSearchFromBeginning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnSearchFromBeginning.Location = new System.Drawing.Point(19, 106);
            this.rbtnSearchFromBeginning.Name = "rbtnSearchFromBeginning";
            this.rbtnSearchFromBeginning.Size = new System.Drawing.Size(207, 16);
            this.rbtnSearchFromBeginning.TabIndex = 3;
            this.rbtnSearchFromBeginning.Text = "Search from the beginning of the ROM";
            this.rbtnSearchFromBeginning.UseVisualStyleBackColor = true;
            this.rbtnSearchFromBeginning.CheckedChanged += new System.EventHandler(this.rbtnSearchFromBeginning_CheckedChanged);
            // 
            // lblNeededBytes
            // 
            this.lblNeededBytes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNeededBytes.Location = new System.Drawing.Point(16, 49);
            this.lblNeededBytes.Name = "lblNeededBytes";
            this.lblNeededBytes.Size = new System.Drawing.Size(133, 13);
            this.lblNeededBytes.TabIndex = 3;
            this.lblNeededBytes.Text = "Needed bytes";
            // 
            // lblFreeSpaceByte
            // 
            this.lblFreeSpaceByte.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreeSpaceByte.Location = new System.Drawing.Point(16, 24);
            this.lblFreeSpaceByte.Name = "lblFreeSpaceByte";
            this.lblFreeSpaceByte.Size = new System.Drawing.Size(133, 13);
            this.lblFreeSpaceByte.TabIndex = 1;
            this.lblFreeSpaceByte.Text = "Free space byte (Hex)";
            // 
            // btnFindNext
            // 
            this.btnFindNext.Enabled = false;
            this.btnFindNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFindNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindNext.Location = new System.Drawing.Point(395, 120);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(114, 23);
            this.btnFindNext.TabIndex = 7;
            this.btnFindNext.Text = "Find next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // lblGame
            // 
            this.lblGame.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGame.Location = new System.Drawing.Point(271, 243);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(107, 13);
            this.lblGame.TabIndex = 40;
            this.lblGame.Text = "ROM: ???";
            this.lblGame.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbxDecHex
            // 
            this.gbxDecHex.Controls.Add(this.tbxHex);
            this.gbxDecHex.Controls.Add(this.tbxDec);
            this.gbxDecHex.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDecHex.Location = new System.Drawing.Point(395, 178);
            this.gbxDecHex.Name = "gbxDecHex";
            this.gbxDecHex.Size = new System.Drawing.Size(114, 93);
            this.gbxDecHex.TabIndex = 39;
            this.gbxDecHex.TabStop = false;
            this.gbxDecHex.Text = "Dec/Hex";
            // 
            // tbxHex
            // 
            this.tbxHex.Location = new System.Drawing.Point(13, 56);
            this.tbxHex.Maximum = ((uint)(4294967295u));
            this.tbxHex.MaxLength = 8;
            this.tbxHex.Minimum = ((uint)(0u));
            this.tbxHex.Name = "tbxHex";
            this.tbxHex.NumbersOnly = HackMew.ToolsFactory.TextBoxEx.TextBoxExNumbersOnly.Hexadecimal;
            this.tbxHex.ReadOnly = true;
            this.tbxHex.Size = new System.Drawing.Size(88, 21);
            this.tbxHex.TabIndex = 10;
            this.tbxHex.Text = "0";
            this.tbxHex.Value = ((uint)(0u));
            this.tbxHex.TextChanged += new System.EventHandler(this.tbxHex_TextChanged);
            this.tbxHex.Enter += new System.EventHandler(this.tbxHex_Enter);
            // 
            // tbxDec
            // 
            this.tbxDec.Location = new System.Drawing.Point(13, 26);
            this.tbxDec.Maximum = ((uint)(4294967295u));
            this.tbxDec.MaxLength = 10;
            this.tbxDec.Minimum = ((uint)(0u));
            this.tbxDec.Name = "tbxDec";
            this.tbxDec.NumbersOnly = HackMew.ToolsFactory.TextBoxEx.TextBoxExNumbersOnly.Decimal;
            this.tbxDec.Size = new System.Drawing.Size(88, 21);
            this.tbxDec.TabIndex = 9;
            this.tbxDec.Text = "0";
            this.tbxDec.Value = ((uint)(0u));
            this.tbxDec.TextChanged += new System.EventHandler(this.tbxDec_TextChanged);
            this.tbxDec.Enter += new System.EventHandler(this.tbxDec_Enter);
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Location = new System.Drawing.Point(395, 149);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(114, 23);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnFind
            // 
            this.btnFind.Enabled = false;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(394, 91);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(114, 23);
            this.btnFind.TabIndex = 6;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lbxResults
            // 
            this.lbxResults.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxResults.FormattingEnabled = true;
            this.lbxResults.Location = new System.Drawing.Point(271, 91);
            this.lbxResults.Name = "lbxResults";
            this.lbxResults.Size = new System.Drawing.Size(107, 134);
            this.lbxResults.TabIndex = 5;
            this.lbxResults.DoubleClick += new System.EventHandler(this.lbxResults_DoubleClick);
            // 
            // formMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(521, 285);
            this.Controls.Add(this.gbxSearchOptions);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.gbxDecHex);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.lbxResults);
            this.Controls.Add(this.pbxLogo);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mmMain;
            this.MinimumSize = new System.Drawing.Size(527, 334);
            this.Name = "formMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.formMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.formMain_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.gbxSearchOptions.ResumeLayout(false);
            this.gbxSearchOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkipInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNeededBytes)).EndInit();
            this.gbxDecHex.ResumeLayout(false);
            this.gbxDecHex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu mmMain;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miOpenROM;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miEdit;
        private System.Windows.Forms.MenuItem miFind;
        private System.Windows.Forms.MenuItem miFindNext;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem miCopy;
        private System.Windows.Forms.MenuItem miHelp;
        private System.Windows.Forms.MenuItem miUpdates;
        private System.Windows.Forms.MenuItem miAbout;
        private System.Windows.Forms.MenuItem miCheckNow;
        private System.Windows.Forms.MenuItem miAutomaticallyCheck;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miReportIssue;
        private System.Windows.Forms.MenuItem miLanguage;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem miClear;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miWebsite;
        private System.Windows.Forms.MenuItem miDonate;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.ToolTip ttGame;
        private System.Windows.Forms.PictureBox pbxLogo;
        private System.Windows.Forms.GroupBox gbxSearchOptions;
        private HackMew.ToolsFactory.TextBoxEx tbxOffset;
        private HackMew.ToolsFactory.TextBoxEx tbxFreeSpaceByte;
        private System.Windows.Forms.Label lblNote;
        private HackMew.ToolsFactory.NumericUpDownEx nudSkipInterval;
        private HackMew.ToolsFactory.NumericUpDownEx nudNeededBytes;
        private System.Windows.Forms.Label lblSkipInterval;
        private System.Windows.Forms.RadioButton rbtnSearchFromOffset;
        private System.Windows.Forms.RadioButton rbtnSearchFromBeginning;
        private System.Windows.Forms.Label lblNeededBytes;
        private System.Windows.Forms.Label lblFreeSpaceByte;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.GroupBox gbxDecHex;
        private HackMew.ToolsFactory.TextBoxEx tbxHex;
        private HackMew.ToolsFactory.TextBoxEx tbxDec;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ListBox lbxResults;
    }
}

