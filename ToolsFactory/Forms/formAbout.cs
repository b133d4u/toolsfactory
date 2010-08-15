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
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace HackMew.ToolsFactory
{
    public partial class formAbout : Form
    {
        public formAbout()
        {
            InitializeComponent();

            // retrieve the application info
            lblCopyright.Text = AssemblyHelper.Copyright;
            lblProgramName.Text = AssemblyHelper.Title;
            lblVersion.Text = "v" + AssemblyHelper.Version;

            Localization.Localize(this);

            // set the links
            lnkWebsite.Links.Add(0, lnkWebsite.Text.Length, Urls.Website);
            lnkDonate.Links.Add(0, lnkDonate.Text.Length, Urls.Donate);

            FontHelper.ApplySystemFont(this);
            FontHelper.SetCustomFont(lblProgramName, FontStyle.Bold);
        }

        private void lnkWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // execute the link in the default browser
            Process.Start(e.Link.LinkData.ToString());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(true);
        }
    }
}
