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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using System.Globalization;

// References:
// http://www.doachick.com/development/c-tutorials/54-add-language-localization-to-your-program
// ---

namespace HackMew.ToolsFactory
{
    public class UnableToLocalizeException : Exception
    {
        public UnableToLocalizeException()
            : base()
        {
        }
    }

    /// <summary>
    /// Provides static methods to handle localization.
    /// </summary>
    public static class Localization
    {
        /// <summary>
        /// Returns a list of the languages available in the locale file.
        /// </summary>
        /// <returns>A System.Collections.Generic.SortedDictionary containing the available languages.</returns>
        public static SortedDictionary<string, CultureInfo> GetLanguages()
        {
            if (availableLanguages != null)
                return availableLanguages;

            try
            {
                availableLanguages = new SortedDictionary<string, CultureInfo>();
                XmlTextReader xmlReader = new XmlTextReader(localeFile);

                while (xmlReader.Read())
                {
                    if (xmlReader.AttributeCount > 0 &&
                        xmlReader.NodeType == XmlNodeType.Element &&
                        xmlReader.Name.Equals("Language", StringComparison.Ordinal))
                    {
                        try
                        {
                            CultureInfo culture = new CultureInfo(xmlReader.GetAttribute(0));
                            availableLanguages.Add(culture.TwoLetterISOLanguageName, culture);
                            xmlReader.Skip();
                        }
                        catch
                        {
                        }
                    }
                }

                return availableLanguages;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Loads into memory all the localized strings associated with the specified language code.
        /// </summary>
        /// <param name="languageCode">The language code to load the localized strings for.</param>
        /// <returns>true if the operation was successful;
        /// false if some errors occured.</returns>
        private static bool LoadStrings(string languageCode)
        {
            try
            {
                bool gotLanguage = false;
                string prefix = String.Empty;
                XmlTextReader xmlReader = new XmlTextReader(localeFile);

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.AttributeCount > 0 &&
                            xmlReader.Name.Equals("Language", StringComparison.Ordinal))
                        {
                            if (!gotLanguage)
                            {
                                if (xmlReader.GetAttribute(0) == languageCode)
                                {
                                    gotLanguage = true;
                                }
                                else
                                {
                                    xmlReader.Skip();
                                }
                            }
                            else
                            {
                                return localizedStrings.Count > 0;
                            }
                        }
                        else if (gotLanguage)
                        {
                            if (xmlReader.AttributeCount == 0)
                            {
                                string key = prefix + "." + xmlReader.Name;

                                if (!localizedStrings.ContainsKey(key))
                                {
                                    localizedStrings.Add(key, xmlReader.ReadString());
                                }
                            }
                            else
                            {
                                prefix = xmlReader.GetAttribute(0);
                            }
                        }
                    }
                }

                return localizedStrings.Count > 0;
            }
            catch (XmlException ex)
            {
                ExceptionHandler.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Applies localization using the system language to all form controls
        /// and the form itself.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool Localize(Form form)
        {
            return Localize(form, null,
                CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentUICulture.Name).Name);
        }

        /// <summary>
        /// Applies localization using the system language to all form controls,
        /// the main menu, and the form itself.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="menu">The main form menu.</param>
        /// <returns></returns>
        public static bool Localize(Form form, MainMenu menu)
        {
            return Localize(form, menu,
                CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentUICulture.Name).Name);
        }

        /// <summary>
        /// Applies localization using the specified language code to all form controls,
        /// the main menu and the form itself.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="menu">The main form menu.</param>
        /// <param name="languageCode">The language code to use the localized strings for.</param>
        /// <returns></returns>
        public static bool Localize(Form form, MainMenu menu, string languageCode)
        {
            try
            {
                SortedDictionary<string, CultureInfo> availableLanguages = GetLanguages();

                if (availableLanguages == null || availableLanguages.Count == 0)
                    throw new UnableToLocalizeException();

                CultureInfo culture;

                try
                {
                     culture = new CultureInfo(languageCode);
                }
                catch
                {
                    culture = new CultureInfo(defaultLanguage);
                }

                if (culture.TwoLetterISOLanguageName != currentCulture.TwoLetterISOLanguageName)
                {
                    localizedStrings.Clear();

                    if (!availableLanguages.ContainsKey(culture.TwoLetterISOLanguageName))
                        culture = new CultureInfo(defaultLanguage);
                }

                if (localizedStrings.Count == 0)
                {
                    if (!LoadStrings(availableLanguages[culture.TwoLetterISOLanguageName].Name))
                        throw new UnableToLocalizeException();
                }

                currentCulture = availableLanguages[culture.TwoLetterISOLanguageName];
                isRtlCulture = currentCulture.TextInfo.IsRightToLeft;

                string formPrefix = form.Name + ".";
                string localizedText = String.Empty;

                if (!isRtlCulture)
                    form.RightToLeft = RightToLeft.No;
                else
                    form.RightToLeft = RightToLeft.Yes;

                if (localizedStrings.TryGetValue(formPrefix + "Text", out localizedText))
                    form.Text = localizedText;

                foreach (Control control in form.Controls)
                    LocalizeControl(control, formPrefix);

                if (menu != null)
                {
                    if (!isRtlCulture)
                        menu.RightToLeft = RightToLeft.No;
                    else
                        menu.RightToLeft = RightToLeft.Yes;

                    foreach (MenuItem menuItem in menu.MenuItems)
                        LocalizeMenu(menuItem, formPrefix);
                }

                if (form.RightToLeftLayout != isRtlCulture)
                    form.RightToLeftLayout = isRtlCulture;

                Thread.CurrentThread.CurrentUICulture = currentCulture;
                Thread.CurrentThread.CurrentCulture = currentCulture;
                return true;
            }
            catch (FileNotFoundException ex)
            {
                string text = "Can't translate the program because the language file doesn't exists." +
                    "\n\nExtract the \"" + Path.GetFileName(localeFile) +
                    "\" file from the program archive.";

                ExceptionHandler.LogException(ex);
                ExceptionHandler.ShowMessage(text);
                return false;
            }
            catch (UnableToLocalizeException ex)
            {
                ExceptionHandler.LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Applies localization to a control and all its child controls.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="formPrefix">A string representing a prefix of the form which the control is in.</param>
        private static void LocalizeControl(Control control, string formPrefix)
        {
            string localizedText = String.Empty;

            if (!isRtlCulture)
                control.RightToLeft = RightToLeft.No;

            if (localizedStrings.TryGetValue(formPrefix + control.Name, out localizedText))
                control.Text = localizedText;

            foreach (Control subControl in control.Controls)
            {
                if (!isRtlCulture)
                    subControl.RightToLeft = RightToLeft.No;
                else
                    subControl.RightToLeft = RightToLeft.Yes;

                if (localizedStrings.TryGetValue(formPrefix + subControl.Name, out localizedText))
                    subControl.Text = localizedText;

                LocalizeControl(subControl, formPrefix);
            }
        }

        /// <summary>
        /// Applies localization to a menu and all its child items.
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="formPrefix">A string representing a prefix of the form which the menu is in.</param>
        private static void LocalizeMenu(MenuItem menuItem, string formPrefix)
        {
            string localizedText = String.Empty;

            if (localizedStrings.TryGetValue(formPrefix + menuItem.Tag, out localizedText))
                menuItem.Text = localizedText;

            foreach (MenuItem subMenuItem in menuItem.MenuItems)
            {
                if (localizedStrings.TryGetValue(formPrefix + subMenuItem.Tag, out localizedText))
                    subMenuItem.Text = localizedText;

                LocalizeMenu(subMenuItem, formPrefix);
            }
        }

        /// <summary>
        /// Applies localization to a context menu and all its child items.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="contextMenu"></param>
        public static void LocalizeContextMenu(Form form, ContextMenu contextMenu)
        {
            if (!isRtlCulture)
                contextMenu.RightToLeft = RightToLeft.No;
            else
                contextMenu.RightToLeft = RightToLeft.Yes;

            string formPrefix = form.Name + ".";

            foreach (MenuItem menuItem in contextMenu.MenuItems)
                LocalizeMenu(menuItem, formPrefix);
        }

        /// <summary>
        /// Returns the localized value associated with the specified string.
        /// </summary>
        /// <param name="stringId">The string value to get.</param>
        /// <returns>The localized string.</returns>
        public static string GetString(string stringId)
        {
            try
            {
                return localizedStrings[stringId];
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                return "(" + stringId + ")";
            }
        }

        /// <summary>
        /// Gets the currently used culture.
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            get { return currentCulture; }
        }

        /// <summary>
        /// Gets or sets the file to load the languages from.
        /// </summary>
        public static string LocaleFile
        {
            get { return localeFile; }
            set { localeFile = value; }
        }

        /// <summary>
        /// Gets a value based on the current culture whether to display message boxes
        /// using a right to left layout.
        /// </summary>
        public static MessageBoxOptions MessageBoxRtl
        {
            get
            {
                if (!isRtlCulture)
                {
                    return (MessageBoxOptions)0;
                }
                else
                {
                    return MessageBoxOptions.RightAlign |
                        MessageBoxOptions.RtlReading;
                }
            }
        }

        /// <summary>
        /// Gets the base name pattern to use for the locale file.
        /// </summary>
        public static string BaseFileName = "Languages.xml";

        private static string defaultLanguage = "en-US";
        private static CultureInfo currentCulture = new CultureInfo(defaultLanguage);
        private static bool isRtlCulture = false; 
        private static SortedDictionary<string, CultureInfo> availableLanguages; 
        private static string localeFile = String.Empty;
        private static Dictionary<string, string> localizedStrings = new Dictionary<string, string>();
    }
}
