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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

// References:
// http://www.cambiaresearch.com/c4/1b2bd29a-0d0f-41b7-bd19-78b651f4591a/How-Can-I-Easily-Manage-an-XML-Configuration-File-in-dotnet.aspx
// http://www.java2s.com/Code/CSharp/File-Stream/UseXMLSerializationwithCustomObjects.htm
// http://www.switchonthecode.com/tutorials/csharp-tutorial-xml-serialization
// http://www.devfuel.com/2007/03/xmlserializer-now-with-less-xmlnsxsi.html
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides basic setting handling for an application.
    /// </summary>
    [Serializable]
    public class SettingsBase
    {
        public SettingsBase()
        {
        }

        /// <summary>
        /// Loads the settings from the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingFile">A string path which to load the settings from.</param>
        /// <returns>An instance class containing the loaded settings.</returns>
        public static T Load<T>(string settingFile) where T : SettingsBase, new()
        {
            T settings = new T();

            try
            {
                if (File.Exists(settingFile))
                {
                    using (StreamReader reader = new StreamReader(settingFile, Encoding.UTF8))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(T));
                        settings = (T)xs.Deserialize(reader);
                    }
                }

                settings.settingFile = settingFile;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                settings.settingFile = settingFile;
            }

            return settings;
        }

        /// <summary>
        /// Saves the settings of an instance class to the specified file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance class to get the settings from.</param>
        public static void Save<T>(T instance) where T : SettingsBase
        {
            try
            {
                if (!String.IsNullOrEmpty(instance.settingFile))
                {
                    using (StreamWriter writer = new StreamWriter(instance.settingFile, false, Encoding.UTF8))
                    {
                        XmlSerializer xs = new XmlSerializer(instance.GetType());

                        XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
                        emptyNamespace.Add(String.Empty, String.Empty);

                        xs.Serialize(writer, instance, emptyNamespace);
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        /// <summary>
        /// Gets or sets the file used to load or save the settings.
        /// </summary>
        [XmlIgnore]
        public string SettingFile
        {
            get { return settingFile; }
            set { settingFile = value; }
        }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        [XmlElement]
        public string LanguageCode
        {
            get { return languageCode; }
            set { languageCode = value; }
        }

        /// <summary>
        /// Gets or sets a value determining whether to enable the automatic update check.
        /// </summary>
        [XmlElement]
        public bool AutomaticUpdateCheck
        {
            get { return automaticUpdateCheck; }
            set { automaticUpdateCheck = value; }
        }

        /// <summary>
        /// Gets the base name pattern to use for the setting file.
        /// </summary>
        public static string BaseFileName = "Settings.xml";

        protected string settingFile = String.Empty;
        protected string languageCode = String.Empty;
        protected bool automaticUpdateCheck = true;
    }
}
