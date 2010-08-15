// Tools Factory

// A set of professional and user-friendly tools aimed at 
// editing all major aspects of Pokémon GBA games.

// Copyright (C) 2010  HackMew

// This file is part of Tools Factory.
// Tools Factory is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Tools Factory is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Tools Factory.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HackMew.ToolsFactory
{
    public partial class formUpdate : Form
    {
        private BackgroundWorker downloadWorker = new BackgroundWorker();
        private byte[] downloadBuffer;
        private bool isClosePending = false;

        public formUpdate()
        {
            InitializeComponent();

            Localization.Localize(this);
            FontHelper.ApplySystemFont(this);
        }

        private void formUpdate_Load(object sender, EventArgs e)
        {
            // needed to center the form properly
            this.CenterToParent();

            // initialize and start the update download
            downloadWorker = new BackgroundWorker();
            downloadWorker.WorkerReportsProgress = true;
            downloadWorker.WorkerSupportsCancellation = true;
            downloadWorker.DoWork += new DoWorkEventHandler(downloadWorker_DoWork);
            downloadWorker.ProgressChanged += new ProgressChangedEventHandler(downloadWorker_ProgressChanged);
            downloadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(downloadWorker_RunWorkerCompleted);
            downloadWorker.RunWorkerAsync();
        }

        private void downloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = UpdateHelper.DownloadUpdate(sender as BackgroundWorker);
        }

        private void downloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // update the progress bar while downloading
            pbrDownload.Value = e.ProgressPercentage;
        }

        private void downloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                // download completed successfully
                downloadBuffer = (byte[])e.Result;
                lblProgress.Text = Localization.GetString(this.Name + ".lblProgress.DownloadComplete");

                // disable the cancel button and enable the restart one
                btnCancel.Enabled = false;
                btnCancel.Update();

                btnRestart.Enabled = true;
                btnRestart.Focus();
                btnRestart.Update();
            }
            else
            {
                // some problems occured while downloading the updates
                ExceptionHandler.ShowMessage(ExceptionHandler.LoggedException);
                btnCancel.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            // check if the form is closing already
            if (isClosePending)
                return;

            isClosePending = true;

            // cancel the download
            if (downloadWorker != null && downloadWorker.IsBusy)
                downloadWorker.CancelAsync();

            this.Close(true);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            // Mono workaround: disabled buttons fire the Click event
            if (!((Button)sender).Enabled)
                return;

            if (isClosePending)
                return;

            isClosePending = true;

            // hide all the forms
            foreach (Form frm in Application.OpenForms)
                frm.Hide();

            Application.Exit();
            UpdateHelper.ExtractUpdateFiles(downloadBuffer);
            UpdateHelper.RestartExecutable();
        }

        private void formUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosePending)
            {
                // simulate a button click when closing the form manually
                if (btnCancel.Enabled)
                {
                    btnCancel.PerformClick();
                }
                else if (btnRestart.Enabled)
                {
                    btnRestart.PerformClick();
                }
            }
        }
    }
}
