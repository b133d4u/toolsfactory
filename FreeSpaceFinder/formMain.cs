// Free Space Finder

// Whether you're scripting or replacing some images, this tool 
// helps you finding free space inside your ROMs.

// Copyright (C) 2010  HackMew

// This file is part of Free Space Finder.
// Free Space Finder is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Free Space Finder is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Free Space Finder.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using HackMew.ToolsFactory;

namespace FreeSpaceFinder
{
    public partial class formMain : Form
    {
        public static string[] Arguments;

        protected bool manualUpdateCheck = false;
        protected BackgroundWorker updateWorker = new BackgroundWorker();
        protected Settings settings = new Settings();

        public formMain()
        {
            InitializeComponent();

            string basePath = Path.Combine(AssemblyHelper.DirectoryName, GetType().Namespace + ".");

            ExceptionHandler.LogFile = basePath + ExceptionHandler.BaseFileName;
            settings = Settings.Load<Settings>(basePath + Settings.BaseFileName);

            // localize the form
            Localization.LocaleFile = basePath + Localization.BaseFileName;
            SetLocale(settings.LanguageCode, true);

            // set the form caption
            this.Text = AssemblyHelper.Title;

            FontHelper.ApplySystemFont(this);

            // initialize the update checking
            UpdateHelper.UpdateCheckUrl = UpdateHelper.UpdateCheckBase +
                this.GetType().Namespace + UpdateHelper.FileExtension;

            if (Arguments.Length > 0)
            {
                // load the file from the command line
                if (File.Exists(Arguments[0]))
                    LoadFile(Arguments[0]);
            }
        }

        private void SetLocale(string languageCode)
        {
            SetLocale(languageCode, false);
        }

        private void SetLocale(string languageCode, bool firstTime)
        {
            bool localized = false;

            if (!String.IsNullOrEmpty(languageCode))
            {
                localized = Localization.Localize(this, mmMain, languageCode);
            }
            else
            {
                localized = Localization.Localize(this, mmMain);
            }

            string currentLanguage = Localization.CurrentCulture.Name;
            settings.LanguageCode = currentLanguage;

            if (firstTime)
            {
                miLanguage.Enabled = localized;
                miLanguage.MenuItems.Clear();

                if (localized)
                {
                    // get the available languages
                    SortedDictionary<string, CultureInfo> languages = Localization.GetLanguages();

                    string languageName = String.Empty;
                    int i = 0;

                    // initialize the language menu items
                    foreach (KeyValuePair<string, CultureInfo> language in languages)
                    {
                        languageName = language.Value.Parent.NativeName.ToUpperFirst(language.Value) +
                            " - " + language.Value.Parent.EnglishName;

                        miLanguage.MenuItems.Add(new MenuItem(languageName));
                        miLanguage.MenuItems[i].RadioCheck = true;
                        miLanguage.MenuItems[i].Checked = (language.Value.Name == currentLanguage);
                        miLanguage.MenuItems[i].Tag = language.Value.Name;
                        miLanguage.MenuItems[i++].Click += new EventHandler(miLanguageChoice_Click);
                    }
                }
                else
                {
                    // add an dummy item
                    miLanguage.MenuItems.Add(String.Empty);
                }
            }
            else
            {
                // set the radio check on the selected language only
                foreach (MenuItem menuItem in miLanguage.MenuItems)
                    menuItem.Checked = (menuItem.Tag.ToString() == currentLanguage);
            }
        }

        private void CheckUpdates(bool manualUpdate)
        {
            if (updateWorker.IsBusy)
                return;

            updateWorker = new BackgroundWorker();
            updateWorker.DoWork += new DoWorkEventHandler(updateWorker_DoWork);
            updateWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(updateWorker_RunWorkerCompleted);

            manualUpdateCheck = manualUpdate;
            updateWorker.RunWorkerAsync();
        }

        private void Find()
        {
            Find(false);
        }

        private void Find(bool findNext)
        {
            FreeSpaceHelper.FreeSpaceByte = (byte)tbxFreeSpaceByte.Value;
            int startOffset = 0;

            if (!findNext)
            {
                startOffset = rbtnSearchFromBeginning.Checked ? 0 : (int)tbxOffset.Value;
            }
            else
            {
                startOffset = Convert.ToInt32(lbxResults.SelectedItem.ToString(), 16) +
                    (int)nudSkipInterval.Value;
            }

            int result = -1;

            try
            {
                result = FreeSpaceHelper.Search(GameManager.FileName, startOffset, (int)nudNeededBytes.Value);
                
                if (!findNext)
                    Clear();
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                ExceptionHandler.ShowMessage(ex);
                return;
            }

            if (result != -1)
            {
                if (!findNext)
                {
                    // enable find next, copy and clear
                    miFindNext.Enabled = true;
                    miCopy.Enabled = true;
                    miClear.Enabled = true;

                    btnFindNext.Enabled = true;
                    btnFindNext.Update();

                    btnCopy.Enabled = true;
                    btnCopy.Update();
                }

                // convert the result to hex
                string offset = result.ToString("X6");

                // check if the offset exists already
                if (!lbxResults.Items.Contains(offset))
                {
                    // add new item
                    lbxResults.Items.Add(offset);
                    lbxResults.SelectedIndex = lbxResults.Items.Count - 1;
                }
                else
                {
                    // select the existing item
                    lbxResults.SelectedItem = offset;
                }
            }
            else
            {
                // not enough free space found
                MessageBoxHelper.Show(this, Localization.GetString(this.Name + ".Find.CantFindEnoughSpace") +
                    "\n\n" + Localization.GetString(this.Name + ".Find.TryDifferentOffset"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Copy()
        {
            Clipboard.SetText(lbxResults.SelectedItem.ToString());
        }

        private void Disable()
        {
            miFind.Enabled = false;

            btnFind.Enabled = false;
            btnFind.Update();

            rbtnSearchFromBeginning.Enabled = false;
            rbtnSearchFromOffset.Enabled = false;
            tbxOffset.Enabled = false;
        }

        private void Clear()
        {
            // reset the results
            lbxResults.Items.Clear();

            // enable find only
            miFind.Enabled = true;
            miFindNext.Enabled = false;
            miCopy.Enabled = false;
            miClear.Enabled = false;

            btnFind.Enabled = true;
            btnFind.Update();

            btnFindNext.Enabled = false;
            btnFindNext.Update();

            btnCopy.Enabled = false;
            btnCopy.Update();
        }

        private void Open()
        {
            OpenFileDialog openRomDialog = new OpenFileDialog();

            openRomDialog.Filter = "Game Boy Advance (" + GameManager.GbaGameFilter +
                ")|" + GameManager.GbaGameFilter + "|" +
                "Game Boy (" + GameManager.GbGameFilter +
                ")|" + GameManager.GbGameFilter;

            openRomDialog.DefaultExt = GameManager.GbaGameFilter.Substring(2, 3);
            openRomDialog.FilterIndex = settings.OpenFilterIndex;

            DialogResult result = openRomDialog.ShowDialog(this);

            if (result != DialogResult.OK)
                return;

            // load the selected file
            settings.OpenFilterIndex = openRomDialog.FilterIndex;
            LoadFile(openRomDialog.FileName);
        }

        private void LoadFile(string fileName)
        {
            // check if the loaded file is the same as the currently loaded one
            if (GameManager.FileName.Equals(fileName, StringComparison.Ordinal))
                return;

            GameManager.FileName = fileName;
            string gameCode = String.Empty;

            try
            {
                if (GameManager.IsGameBoyAdvance())
                {
                    gameCode =
                        GameManager.ReadHeader<GameManager.GbaGameHeader>().GameCode.GetString();
                }
                else
                {
                    if (GameManager.IsGameBoyColor())
                    {
                        gameCode =
                            GameManager.ReadHeader<GameManager.GbcGameHeader>().GameTitle.GetString();
                    }
                    else if (GameManager.IsSuperGameBoy())
                    {
                        gameCode =
                            GameManager.ReadHeader<GameManager.SgbGameHeader>().GameTitle.GetString();
                    }
                    else
                    {
                        gameCode =
                            GameManager.ReadHeader<GameManager.GbGameHeader>().GameTitle.GetString();
                    }

                    // since older games don't have a game code
                    // just read the last 4 letters
                    int length = gameCode.Length;

                    for (int i = length - 1; i >= 0; i--)
                    {
                        if (gameCode[i] != Char.MinValue)
                            break;

                        length--;
                    }

                    if (length >= 4)
                        gameCode = gameCode.Substring(length - 4, 4);
                }
            }
            catch (Exception ex)
            {
                GameManager.FileName = String.Empty;
                lblGame.Text = "ROM: ???";

                ExceptionHandler.LogException(ex);
                Clear();
                Disable();

                ExceptionHandler.ShowMessage(ex);
                return;
            }

            lblGame.Text = "ROM: " + gameCode;
            ttGame.SetToolTip(lblGame, GameManager.FileName);
            
            // update value limits
            uint fileLength = (uint)new FileInfo(GameManager.FileName).Length;
            nudNeededBytes.Maximum = fileLength > 65535 ? 65535 : fileLength;
            nudSkipInterval.Maximum = nudNeededBytes.Maximum;

            rbtnSearchFromBeginning.Enabled = true;
            rbtnSearchFromOffset.Enabled = fileLength > 0;

            tbxOffset.Maximum = fileLength > 0 ? fileLength - 1 : 0;
            tbxOffset.Value = fileLength / 2;
            tbxOffset.MaxLength = fileLength.ToString("x").Length;
            tbxOffset.Enabled = rbtnSearchFromOffset.Enabled;

            // reset controls
            Clear();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            if (settings.AutomaticUpdateCheck)
            {
                CheckUpdates(false);
            }

            miAutomaticallyCheck.Checked = settings.AutomaticUpdateCheck;
            tbxFreeSpaceByte.Value = settings.FreeSpaceByte;
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateHelper.RenameExecutable();
            Settings.Save<Settings>(settings);
        }

        private void updateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = UpdateHelper.CheckUpdates();

            // add some short delay when checking automatically
            if (!manualUpdateCheck)
                Thread.Sleep(1500);
        }

        private void updateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string namePrefix = this.Name + ".updateWorker.";

            if (e.Result != null)
            {
                UpdateHelper.UpdateInfo updateInfo = (UpdateHelper.UpdateInfo)e.Result;

                if (updateInfo.IsUpdateAvailable)
                {
                    // ask whether to update
                    DialogResult result =
                        MessageBoxHelper.Show(Localization.GetString(namePrefix + "UpdateAvailable") +
                        "\n\n" + String.Format(Localization.GetString(namePrefix + "UpdateVersion"),
                        Application.ProductVersion + "\n> ", updateInfo.UpdateVersion) +
                        "\n\n" + Localization.GetString(namePrefix + "DownloadNow"),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        // load the update form
                        Form formUpdate = new formUpdate();
                        formUpdate.Show(this);
                    }
                }
                else
                {
                    // no need to display anything if the update checking was automatic
                    if (manualUpdateCheck)
                    {
                        MessageBoxHelper.Show(Localization.GetString(namePrefix + "NoUpdateAvailable"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                if (manualUpdateCheck)
                {
                    MessageBoxHelper.Show(Localization.GetString(namePrefix + "UpdateServerUnavailable") +
                        "\n\n" + Localization.GetString(namePrefix + "CheckInternetConnection"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            manualUpdateCheck = false;
        }

        private void miLanguageChoice_Click(object sender, EventArgs e)
        {
            string newLanguage = ((MenuItem)sender).Tag.ToString();

            // set the language only if different
            if (newLanguage != Localization.CurrentCulture.Name)
            {
                SetLocale(newLanguage);
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close(true);
        }

        private void miOpenROM_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void miCheckNow_Click(object sender, EventArgs e)
        {
            CheckUpdates(true);
        }

        private void miReportIssue_Click(object sender, EventArgs e)
        {
            Process.Start(Urls.ReportIssue);
        }

        private void miWebsite_Click(object sender, EventArgs e)
        {
            Process.Start(Urls.Website);
        }

        private void miDonate_Click(object sender, EventArgs e)
        {
            Process.Start(Urls.Donate);
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            // load the about box
            Form formAbout = new formAbout();
            formAbout.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            Find();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            Find(true);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            Copy();
        }

        private void miCopy_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            Copy();
        }

        private void miClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void miFind_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void miFindNext_Click(object sender, EventArgs e)
        {
            Find(true);
        }

        private void rbtnSearchFromBeginning_CheckedChanged(object sender, EventArgs e)
        {
            // enable the offset only when searching from a custom offset
            tbxOffset.Enabled = !rbtnSearchFromBeginning.Checked;
        }

        private void miAutomaticallyCheck_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            settings.AutomaticUpdateCheck = menuItem.Checked;
        }

        private void lbxResults_DoubleClick(object sender, EventArgs e)
        {
            if (((ListBox)sender).Items.Count > 0)
            {
                Copy();
            }
        }

        private void tbxDec_Enter(object sender, EventArgs e)
        {
            tbxDec.ReadOnly = false;
            tbxHex.ReadOnly = true;
        }

        private void tbxHex_Enter(object sender, EventArgs e)
        {
            tbxDec.ReadOnly = true;
            tbxHex.ReadOnly = false;
        }

        private void tbxFreeSpaceByte_TextChanged(object sender, EventArgs e)
        {
            settings.FreeSpaceByte = (byte)((TextBoxEx)sender).Value;
        }

        private void tbxDec_TextChanged(object sender, EventArgs e)
        {
            if (tbxDec.TextLength > 0)
                tbxHex.Value = tbxDec.Value;
            else
                tbxHex.Text = String.Empty;
        }

        private void tbxHex_TextChanged(object sender, EventArgs e)
        {
            if (tbxHex.TextLength > 0)
                tbxDec.Value = tbxHex.Value;
            else
                tbxDec.Text = String.Empty;
        }

        private void formMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (droppedFiles != null && droppedFiles.Length > 0)
                LoadFile(droppedFiles[0]);
        }

        private void formMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void nudNeededBytes_ValueChanged(object sender, EventArgs e)
        {
            nudSkipInterval.Minimum = ((NumericUpDown)sender).Value;
            nudSkipInterval.Value = nudSkipInterval.Minimum;
        }
    }
}
