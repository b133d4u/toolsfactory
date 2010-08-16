// Free Space Finder

// Whether you're scripting or replacing some images, this tool 
// helps you finding free space inside your ROMs.

// Copyright (C) 2010  HackMew

// This file is part of Free Space Finder.
// Free Space Finder is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Free Space Finder is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Free Space Finder.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HackMew.ToolsFactory;

namespace FreeSpaceFinder
{
    /// <summary>
    /// Provides setting handling for Free Space Finder.
    /// </summary>
    public class Settings : SettingsBase
    {
        public Settings()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the byte that represents free space.
        /// </summary>
        [XmlElement]
        public byte FreeSpaceByte
        {
            get { return freeSpaceByte; }
            set { freeSpaceByte = value; }
        }

        /// <summary>
        /// Gets or sets the filter index used in the open dialog.
        /// </summary>
        [XmlElement]
        public int OpenFilterIndex
        {
            get { return openFilterIndex; }
            set { openFilterIndex = value; }
        }

        protected byte freeSpaceByte = 0xff;
        protected int openFilterIndex = 1;
    }
}
