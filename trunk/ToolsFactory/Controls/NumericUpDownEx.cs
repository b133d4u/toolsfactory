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
using System.ComponentModel;
using System.Globalization;
using System.Media;
using System.Reflection;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Represents an extended version of the standard Windows numeric up-down control.
    /// </summary>
    public class NumericUpDownEx : NumericUpDown
    {
        public NumericUpDownEx()
            : base()
        {
            FieldInfo[] fi =
                typeof(UpDownBase).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            upDownEdit = (TextBox)fi[0].GetValue(this);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((this.DecimalPlaces == 0 && e.KeyChar.ToString() ==
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) ||
                (!this.ThousandsSeparator && e.KeyChar.ToString() ==
                CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator) ||
                (this.Minimum >= 0 && e.KeyChar.ToString() ==
                CultureInfo.CurrentCulture.NumberFormat.NegativeSign))
            {
                e.Handled = true;
                SystemSounds.Beep.Play();
                return;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// Gets or sets whether the edit control modifies the case of characters as they are typed.
        /// </summary>
        [Description("Indicates if all characters should be left alone or converted to lowercase or uppercase."),
        Category("Behavior"), DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get { return upDownEdit.CharacterCasing; }
            set { upDownEdit.CharacterCasing = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters the user can type or paste into the edit control.
        /// </summary>
        [Description("Specifies the maximum number of characters that can be entered in the edit control."),
        Category("Behavior"), DefaultValue(32767)]
        public int MaxLength
        {
            get { return upDownEdit.MaxLength; }
            set { upDownEdit.MaxLength = value; }
        }

        protected TextBox upDownEdit;
    }
}
