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
using System.Reflection;
using System.Runtime.InteropServices;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static methods to retrieve the executing assembly attributes.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets the title attribute associated with the executing assembly.
        /// </summary>
        public static string Title
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

                    if (!String.IsNullOrEmpty(titleAttribute.Title))
                        return titleAttribute.Title;
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the version attribute associated with the executing assembly.
        /// </summary>
        public static string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the description attribute associated with the executing assembly.
        /// </summary>
        public static string Description
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the product attribute associated with the executing assembly.
        /// </summary>
        public static string Product
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the copyright attribute associated with the executing assembly.
        /// </summary>
        public static string Copyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the trademark attribute associated with the executing assembly.
        /// </summary>
        public static string Trademark
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((AssemblyTrademarkAttribute)attributes[0]).Trademark;
            }
        }

        /// <summary>
        /// Gets the company attribute associated with the executing assembly.
        /// </summary>
        public static string Company
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// Gets the GUID attribute associated with the executing assembly.
        /// </summary>
        public static string Guid
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().
                    GetCustomAttributes(typeof(GuidAttribute), false);

                if (attributes == null || attributes.Length == 0)
                    return String.Empty;

                return ((GuidAttribute)attributes[0]).Value.ToString();
            }
        }

        /// <summary>
        /// Returns the directory information for the executing assembly.
        /// </summary>
        public static string DirectoryName
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Returns the file name and extension of the executing assembly.
        /// </summary>
        public static string FileName
        {
            get
            {
                return Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Returns the absolute path for the executing assembly.
        /// </summary>
        public static string FullPath
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }
    }
}
