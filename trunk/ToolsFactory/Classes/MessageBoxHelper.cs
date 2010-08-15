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

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static helper methods to display message boxes.
    /// </summary>
    public static class MessageBoxHelper
    {
        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, buttons, and icon.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            string caption = AssemblyHelper.Title;

            return MessageBoxHelper.Show(text, caption, buttons,
                icon, MessageBoxDefaultButton.Button1, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, buttons, icon, and default button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton
        /// values the specifies the default button for the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons,
            MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            string caption = AssemblyHelper.Title;

            return MessageBoxHelper.Show(text, caption, buttons,
                icon, defaultButton, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxHelper.Show(text, caption, buttons,
                icon, MessageBoxDefaultButton.Button1, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton
        /// values the specifies the default button for the message box.</param>
        /// <param name="options"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return MessageBox.Show(text, caption, buttons,
                icon, defaultButton, options);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, buttons, and icon.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            string caption = AssemblyHelper.Title;

            return MessageBoxHelper.Show(owner, text, caption, buttons,
                icon, MessageBoxDefaultButton.Button1, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, buttons, icon, and default button.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton
        /// values the specifies the default button for the message box.</param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons,
            MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            string caption = AssemblyHelper.Title;

            return MessageBoxHelper.Show(owner, text, caption, buttons,
                icon, defaultButton, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, caption, buttons, and icon.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBoxHelper.Show(owner, text, caption, buttons,
                icon, MessageBoxDefaultButton.Button1, Localization.MessageBoxRtl);
        }

        /// <summary>
        /// Displays a message box in front of the specified object and with the specified
        /// text, caption, buttons, icon, default button, and options.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton">One of the System.Windows.Forms.MessageBoxDefaultButton
        /// values the specifies the default button for the message box.</param>
        /// <param name="options"></param>
        /// <returns>One of the System.Windows.Forms.DialogResult values.</returns>
        public static DialogResult Show(IWin32Window owner, string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon,
            MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return MessageBox.Show(owner, text, caption, buttons,
                icon, defaultButton, options);
        }
    }
}
