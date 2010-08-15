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
using System.Globalization;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

// References:
// http://kohari.org/2008/04/04/extension-methods-in-net-20/
// http://dotnetperls.com/uppercase-first-letter
// http://stackoverflow.com/questions/228523/char-ishex-in-c
// http://www.codeproject.com/KB/string/fastestcscaseinsstringrep.aspx
// ---

namespace System.Runtime.CompilerServices
{
    // hack to support extension methods in .NET 2.0
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExtensionAttribute : Attribute { }
}

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static extensions methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Replaces all occurrences of a specified System.String in this instance, with
        /// another specified System.String.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="oldValue">A System.String to be replaced.</param>
        /// <param name="newValue">A System.String to replace all occurrences of oldValue.</param>
        /// <param name="comparisonType">One of the System.StringComparison values.</param>
        /// <returns>A System.String equivalent to this instance but with all instances
        /// of oldValue replaced with newValue.</returns>
        /// <exception cref="System.ArgumentNullException">oldValue is null.</exception>
        /// <exception cref="System.ArgumentException">oldValue is the empty string ("").</exception>
        private static string Replace(string s, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (oldValue == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(oldValue))
                throw new ArgumentException();

            if (String.IsNullOrEmpty(s))
                return s;

            int currentPosition = 0;
            int patternLength = oldValue.Length;
            int nextIndex = s.IndexOf(oldValue, comparisonType);
            StringBuilder result = new StringBuilder(Math.Min(4096, s.Length));

            while (nextIndex >= 0)
            {
                result.Append(s, currentPosition, nextIndex - currentPosition);
                result.Append(newValue);

                currentPosition = nextIndex + patternLength;

                nextIndex = s.IndexOf(oldValue, currentPosition, comparisonType);
            }

            result.Append(s, currentPosition, s.Length - currentPosition);
            return result.ToString();
        }

        /// <summary>
        /// Returns a copy of this System.String with the first character converted to uppercase,
        /// using the casing rules of the current culture.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>A System.String with the first character in uppercase.</returns>
        /// <exception cref="ArgumentNullException">s is null.</exception>
        public static string ToUpperFirst(this string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            return ToUpperFirst(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a copy of this System.String with the first character converted to uppercase,
        /// using the casing rules of the specified culture.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="culture"> A System.Globalization.CultureInfo object that supplies
        /// culture-specific casing rules.</param>
        /// <returns>A System.String with the first character in uppercase.</returns>
        /// <exception cref="ArgumentNullException">s is null.
        /// -or- culture is null.</exception>
        public static string ToUpperFirst(this string s, CultureInfo culture)
        {
            if (s == null || culture == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(s))
                return String.Empty;

            char[] chars = s.ToCharArray();
            chars[0] = char.ToUpper(chars[0], culture);

            return chars.GetString();
        }

        /// <summary>
        /// Returns a copy of this System.String with the first character converted to uppercase,
        /// using the casing rules of the invariant culture.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>A System.String with the first character in uppercase.</returns>
        /// <exception cref="ArgumentNullException">s is null.</exception>
        public static string ToUpperFirstInvariant(this string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            return ToUpperFirst(s, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determines whether this instance is a valid decimal digit.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>A System.Boolean value that is true if the character is
        /// a valid decimal digit; otherwise false.</returns>
        public static bool IsDecDigit(this char c)
        {
            return (c >= '0' && c <= '9');
        }

        /// <summary>
        /// Determines whether this instance is a valid hexadecimal digit.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>A System.Boolean value that is true if the character is
        /// a valid hexadecimal digit; otherwise false.</returns>
        public static bool IsHexDigit(this char c)
        {
            return (c >= '0' && c <= '9') ||
                     (c >= 'A' && c <= 'F') ||
                     (c >= 'a' && c <= 'f');
        }

        /// <summary>
        /// Returns a System.String representing the Unicode character array in this instance.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>A System.String whose individual characters are the
        /// Unicode character array of this instance.</returns>
        public static string GetString(this char[] c)
        {
            return new string(c);
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="hideBefore">true to hide the form before closing; false to close it only.</param>
        /// <exception cref="ArgumentNullException">form is null.</exception>
        public static void Close(this Form form, bool hideBefore)
        {
            if (form == null)
                throw new ArgumentNullException();

            if (hideBefore)
                form.Hide();

            form.Close();
        }

        /// <summary>
        /// Starts (or reuses) the process resource that is specified by the
        /// System.Diagnostics.Process.StartInfo property of this System.Diagnostics.Process
        /// component and associates it with the component.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="unixDetect">true to run the process through Mono on Unix platforms; otherwhise false.</param>
        /// <returns>true if a process resource is started; false if no new process resource is
        /// started (for example, if an existing process is reused).</returns>
        public static bool Start(this Process process, bool unixDetect)
        {
            if (unixDetect && process.StartInfo != null)
            {
                int platformId = (int)Environment.OSVersion.Platform;

                if (platformId == 4 || platformId == 6 || platformId == 128)
                {
                    if (!String.IsNullOrEmpty(process.StartInfo.Arguments))
                    {
                        process.StartInfo.Arguments = "\"" + process.StartInfo.FileName +
                           "\" " + process.StartInfo.Arguments;
                    }
                    else
                    {
                        process.StartInfo.Arguments = "\"" + process.StartInfo.FileName + "\"";
                    }

                    process.StartInfo.FileName = "mono";
                }
            }

            return process.Start();
        }
    }
}
