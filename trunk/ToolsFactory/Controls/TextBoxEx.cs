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
using System.Media;
using System.ComponentModel;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Represents an extended version of the standard Windows text box control.
    /// </summary>
    public class TextBoxEx : TextBox
    {
        public enum TextBoxExNumbersOnly
        {
            None,
            Decimal,
            Hexadecimal
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            string str = e.KeyChar.ToString();

            if ((((e.KeyChar == '\b') || (e.KeyChar == '\t')) ||
                ((e.KeyChar == '\x001a') || (e.KeyChar == '\x0018'))) ||
                (((e.KeyChar == '\x0003') || (e.KeyChar == '\x0016')) ||
                ((str.Equals(Keys.Delete.ToString()) ||
                str.Equals(Keys.Home.ToString())) ||
                str.Equals(Keys.End.ToString()))))
            {
                base.OnKeyPress(e);
            }
            else
            {
                if (numbersOnly == TextBoxExNumbersOnly.Decimal)
                {
                    if (e.KeyChar.IsDecDigit())
                    {
                        base.OnKeyPress(e);
                        return;
                    }
                }
                else if ((numbersOnly == TextBoxExNumbersOnly.Hexadecimal) && e.KeyChar.IsHexDigit())
                {
                    base.OnKeyPress(e);
                    return;
                }

                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (numbersOnly != TextBoxExNumbersOnly.None)
            {
                TextToValue();
            }

            base.OnTextChanged(e);
        }

        private void SetText(string text)
        {
            int selectionStart = this.SelectionStart;
            this.Text = text;
            this.SelectionStart = selectionStart;
        }

        private void SetValue(string text)
        {
            uint value = 0;

            try
            {
                if (numbersOnly == TextBoxExNumbersOnly.Decimal)
                {
                    value = Convert.ToUInt32(text);
                }
                else if (numbersOnly == TextBoxExNumbersOnly.Hexadecimal)
                {
                    value = Convert.ToUInt32(text, 16);
                }

            }
            catch (OverflowException)
            {
                value = UInt32.MaxValue;
            }

            SetValue(value);
        }

        private void SetValue(uint value)
        {
            numericValue = value;

            if (numericValue > maximum)
            {
                numericValue = maximum;
            }
            else if (numericValue < minimum)
            {
                numericValue = minimum;
            }
        }

        private void TextToValue()
        {
            if (this.TextLength > 0)
            {
                string text = this.Text;

                if (!text.Equals(previousText, StringComparison.Ordinal))
                {
                    previousText = text;
                    char[] chArray = text.ToCharArray();

                    if (this.numbersOnly == TextBoxExNumbersOnly.Decimal)
                    {
                        for (int i = 0; i < chArray.Length; i++)
                        {
                            if (!chArray[i].IsDecDigit())
                            {
                                text = new string(chArray, 0, i);
                                SetText(text);
                                SetValue(text);
                                SystemSounds.Beep.Play();
                                return;
                            }
                        }

                        SetValue(text);
                        ValueToText();
                    }
                    else if (this.numbersOnly == TextBoxExNumbersOnly.Hexadecimal)
                    {
                        for (int i = 0; i < chArray.Length; i++)
                        {
                            if (!chArray[i].IsHexDigit())
                            {
                                text = new string(chArray, 0, i);
                                SetText(text);
                                SetValue(text);
                                SystemSounds.Beep.Play();
                                return;
                            }
                        }

                        SetValue(text);
                        ValueToText();
                    }
                }
            }
        }

        private void ValueToText()
        {
            if (numbersOnly == TextBoxExNumbersOnly.Decimal)
            {
                SetText(this.numericValue.ToString());
            }
            else if (numbersOnly == TextBoxExNumbersOnly.Hexadecimal)
            {
                if (this.CharacterCasing != CharacterCasing.Lower)
                {
                    SetText(numericValue.ToString("X"));
                }
                else
                {
                    SetText(numericValue.ToString("x"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum value for the text box.
        /// </summary>
        [Description("Indicates the minimum numeric value for the edit control."),
        Category("Data"), DefaultValue(100)]
        public uint Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                maximum = value;
                ValueToText();

                if (minimum > maximum)
                {
                    minimum = maximum;
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum allowed value for the text box.
        /// </summary>
        [Description("Indicates the minimum numeric value for the edit control."),
        Category("Data"), DefaultValue(0)]
        public uint Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                minimum = value;

                if (numericValue < minimum)
                {
                    numericValue = minimum;
                    ValueToText();
                }

                if (maximum < minimum)
                {
                    maximum = minimum;
                }
            }
        }

        [Description("Indicates whether the edit control will be limited to numeric values."),
        Category("Behavior"), DefaultValue(0)]
        public TextBoxExNumbersOnly NumbersOnly
        {
            get
            {
                return numbersOnly;
            }
            set
            {
                numbersOnly = value;
            }
        }

        /// <summary>
        /// Gets or sets the value assigned to the text box.
        /// </summary>
        [Description("The current numeric value for the edit control."),
        Category("Data"), DefaultValue(0)]
        public uint Value
        {
            get
            {
                return numericValue;
            }
            set
            {
                if (numericValue != value || this.TextLength == 0)
                {
                    SetValue(value);
                    ValueToText();
                }
            }
        }

        private uint minimum = 0;
        private uint maximum = 100;
        private uint numericValue = 0;
        private TextBoxExNumbersOnly numbersOnly = TextBoxExNumbersOnly.None;        
        private string previousText = String.Empty;
    }
}
