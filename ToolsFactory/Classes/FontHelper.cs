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

// References:
// http://www.codeproject.com/KB/cs/AdjustingFontAndLayout.aspx
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static methods for handling system-aware fonts.
    /// </summary>
    public static class FontHelper
    {
        /// <summary>
        /// Changes the font property for all form controls in order to match the system font.
        /// </summary>
        /// <param name="form">The form to apply the changes to.</param>
        public static void ApplySystemFont(Form form)
        {
            form.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name,
                SystemFonts.MessageBoxFont.Size);

            foreach (Control control in form.Controls)
            {
                control.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name,
                    SystemFonts.MessageBoxFont.Size);

                if (control.Controls.Count > 0)
                    SetControlFont(control);
            }
        }

        /// <summary>
        /// Sets the system font for the specified control and all its child controls.
        /// </summary>
        /// <param name="control">The control to apply the changes to.</param>
        private static void SetControlFont(Control control)
        {
            foreach (Control subControl in control.Controls)
            {
                subControl.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name,
                    SystemFonts.MessageBoxFont.Size);

                SetControlFont(subControl);
            }
        }

        /// <summary>
        /// Adjusts a control font while retaining its font name.
        /// </summary>
        /// <param name="control">The control to apply the changes to.</param>
        /// <param name="emSize">The em-size, in points, of the new font.</param>
        /// <exception cref="System.ArgumentException">emSize is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
        public static void SetCustomFont(Control control, float emSize)
        {
            if ((Single.IsNaN(emSize) || Single.IsInfinity(emSize)) || emSize <= 0f)
            {
                throw new ArgumentException();
            }

            SetCustomFont(control, emSize, control.Font.Style);
        }
        
        /// <summary>
        /// Adjusts a control font while retaining its font name.
        /// </summary>
        /// <param name="control">The control to apply the changes to.</param>
        /// <param name="style">The System.Drawing.FontStyle of the new font.</param>
        public static void SetCustomFont(Control control, FontStyle style)
        {
            SetCustomFont(control, control.Font.Size, style);
        }

        /// <summary>
        /// Adjusts a control font while retaining its font name.
        /// </summary>
        /// <param name="control">The control to apply the changes to.</param>
        /// <param name="emSize">The em-size, in points, of the new font.</param>
        /// <param name="style">The System.Drawing.FontStyle of the new font.</param>
        /// <exception cref="System.ArgumentException">emSize is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
        public static void SetCustomFont(Control control, float emSize, FontStyle style)
        {
            if ((Single.IsNaN(emSize) || Single.IsInfinity(emSize)) || emSize <= 0f)
            {
                throw new ArgumentException();
            }

            using (Font oldFont = control.Font)
                control.Font = new Font(oldFont.FontFamily.Name, emSize, style);
        }
    }
}
