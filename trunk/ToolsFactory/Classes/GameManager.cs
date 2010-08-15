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
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

// References:
// http://nocash.emubase.de/pandocs.htm#thecartridgeheader
// http://nocash.emubase.de/gbatek.htm#gbacartridgeheader
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static methods to handle games.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Provides the core abstract structure for a game header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public abstract class GameHeader
        {
        }

        /// <summary>
        /// Represents a Game Boy (GB) game header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GbGameHeader : GameHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] GameTitle;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Reserved;

            public byte CartridgeType;
            public byte RomSize;
            public byte RamSize;
            public byte DestinationCode;
            public byte LicenseeCode;
            public byte SoftwareVersion;
            public byte HeaderChecksum;
            public ushort GlobalChecksum;
        }

        /// <summary>
        /// Represents a Super Game Boy (SGB) game header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class SgbGameHeader : GameHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] GameTitle;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] LicenseeCodeNew;

            public byte SgbFlag;
            public byte CartridgeType;
            public byte RomSize;
            public byte RamSize;
            public byte DestinationCode;
            public byte LicenseeCodeOld;
            public byte SoftwareVersion;
            public byte HeaderChecksum;
            public ushort GlobalChecksum;
        }

        /// <summary>
        /// Represents a Game Boy Color (GBC) game header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GbcGameHeader : GameHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public char[] GameTitle;

            public byte GbcFlag;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] LicenseeCodeNew;

            public byte SgbFlag;
            public byte CartridgeType;
            public byte RomSize;
            public byte RamSize;
            public byte DestinationCode;
            public byte LicenseeCodeOld;
            public byte SoftwareVersion;
            public byte HeaderChecksum;
            public ushort GlobalChecksum;
        }

        /// <summary>
        /// Represents a Game Boy Advance (GBA) game header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class GbaGameHeader : GameHeader
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] GameTitle;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public char[] GameCode;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public char[] MakerCode;

            public byte FixedValue;
            public byte MainUnitCode;
            public byte DeviceType;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] Reserved;

            public byte SoftwareVersion;
            public byte HeaderChecksum;
        }

        /// <summary>
        /// Indicates whether the specified path represents a GB/SGB/GBC game.
        /// </summary>
        /// <param name="path">The path string to test.</param>
        /// <returns>true if the value parameter contains a valid GB/SGB/GBC extension;
        /// otherwise, false.</returns>
        private static bool IsGbAll(string path)
        {
            string ext = Path.GetExtension(path);

            for (int i = 0; i < gbExt.Length; i++)
            {
                if (String.Equals(ext, gbExt[i], StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the currently loaded game represents a GB game.
        /// </summary>
        /// <returns>true if the value parameter contains a valid GB extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoy()
        {
            return IsGameBoy(filePath);
        }

        /// <summary>
        /// Indicates whether the specified path represents a GB game.
        /// </summary>
        /// <param name="path">The path string to test.</param>
        /// <returns>true if the value parameter contains a valid GB extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoy(string path)
        {
            string ext = Path.GetExtension(path);

            if (!IsGbAll(path))
                return false;

            return ext.IndexOf("s", StringComparison.OrdinalIgnoreCase) == -1 &
                ext.IndexOf("c", StringComparison.OrdinalIgnoreCase) == -1;
        }

        /// <summary>
        /// Indicates whether the currently loaded game represents a SGB game.
        /// </summary>
        /// <returns>true if the value parameter contains a valid SGB extension;
        /// otherwise, false.</returns>
        public static bool IsSuperGameBoy()
        {
            return IsSuperGameBoy(filePath);
        }

        /// <summary>
        /// Indicates whether the specified path represents a SGB game.
        /// </summary>
        /// <param name="path">The path string to test.</param>
        /// <returns>true if the value parameter contains a valid SGB extension;
        /// otherwise, false.</returns>
        public static bool IsSuperGameBoy(string path)
        {
            if (!IsGbAll(path))
                return false;

            return Path.GetExtension(path).IndexOf("s", StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>
        /// Indicates whether the currently loaded game represents a GBC game.
        /// </summary>
        /// <returns>true if the value parameter contains a valid GBC extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoyColor()
        {
            return IsGameBoyColor(filePath);
        }

        /// <summary>
        /// Indicates whether the specified path represents a GBC game.
        /// </summary>
        /// <param name="path">The path string to test.</param>
        /// <returns>true if the value parameter contains a valid GBC extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoyColor(string path)
        {
            if (!IsGbAll(path))
                return false;

            return Path.GetExtension(path).IndexOf("c", StringComparison.OrdinalIgnoreCase) != -1;
        }

        /// <summary>
        /// Indicates whether the currently loaded game represents a GBA game.
        /// </summary>
        /// <returns>true if the value parameter contains a valid GBA extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoyAdvance()
        {
            return IsGameBoyAdvance(filePath);
        }

        /// <summary>
        /// Indicates whether the specified path represents a GBA game.
        /// </summary>
        /// <param name="path">The path string to test.</param>
        /// <returns>true if the value parameter contains a valid GBA extension;
        /// otherwise, false.</returns>
        public static bool IsGameBoyAdvance(string path)
        {
            string ext = Path.GetExtension(path);

            for (int i = 0; i < gbaExt.Length; i++)
            {
                if (String.Equals(ext, gbaExt[i], StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the game header for the currently loaded game.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ReadHeader<T>() where T : GameHeader 
        {
            using (BinaryReader reader =
                new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read,
                    FileShare.Read), Encoding.ASCII))
            {
                byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
                reader.BaseStream.Position = IsGameBoyAdvance(filePath) ? gbaHeaderOffset : gbHeaderOffset;
                buffer = reader.ReadBytes(buffer.Length);

                return (T)StructureHelper.BytesToClass<T>(ref buffer);
            }
        }

        /// <summary>
        /// Writes the specified game header to the currently loaded game.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header">The header information to write.</param>
        public static void WriteHeader<T>(T header) where T : GameHeader
        {
            using (BinaryWriter writer =
                new BinaryWriter(File.Open(filePath, FileMode.Open, FileAccess.Write,
                    FileShare.Read), Encoding.ASCII))
            {
                byte[] buffer = StructureHelper.ClassToBytes<T>(header);

                writer.BaseStream.Position = IsGameBoyAdvance(filePath) ? gbaHeaderOffset : gbHeaderOffset;
                writer.Write(buffer);
            }
        }

        /// <summary>
        /// Returns a string representing a filter value based on the specified extensions.
        /// </summary>
        /// <param name="extensions">A string array containing all extensions to use.</param>
        /// <returns></returns>
        private static string ExtToFilter(string[] extensions)
        {
            StringBuilder filter = new StringBuilder();

            for (int i = 0; i < extensions.Length; i++)
            {
                filter.Append("*").Append(extensions[i]);

                if (i != extensions.Length - 1)
                    filter.Append(";");
            }

            return filter.ToString();
        }

        /// <summary>
        /// Gets the Game Boy filter to use in open dialogs.
        /// </summary>
        public static string GbGameFilter
        {
            get { return gbFilter; }
        }

        /// <summary>
        /// Gets the Game Boy Advance filter to use in open dialogs.
        /// </summary>
        public static string GbaGameFilter
        {
            get { return gbaFilter; }
        }

        /// <summary>
        /// Gets or sets the file name associated with the currently loaded game.
        /// </summary>
        public static string FileName
        {
            get { return filePath; }
            set { filePath = value; }
        }

        protected static readonly string[] gbExt = {".gb", ".sgb", ".cgb", ".gbc"};
        protected static readonly string[] gbaExt = { ".gba", ".agb", ".bin" };

        protected static readonly string gbFilter = ExtToFilter(gbExt);
        protected static readonly string gbaFilter = ExtToFilter(gbaExt);

        protected static int gbHeaderOffset = 0x134;
        protected static int gbaHeaderOffset = 0xa0;

        protected static string filePath = String.Empty;
    }
}
