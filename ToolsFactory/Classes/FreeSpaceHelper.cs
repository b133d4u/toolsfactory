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
using System.Text;
using System.IO;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static free space searching methods.
    /// </summary>
    public static class FreeSpaceHelper
    {
        /// <summary>
        /// Reports the offset of the first occurrence of the specified amount of free space in the specified file.
        /// </summary>
        /// <param name="path">The path string from which to look for free space.</param>
        /// <param name="count">The number of bytes to search.</param>
        /// <returns>The zero-based offset of the first match if enough free space is found, or -1 if it is not.</returns>
        /// <exception cref="System.ArgumentNullException">path is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">count is out of range. This parameter requires
        /// a value higher than zero.</exception>
        public static int Search(string path, int count)
        {
            if (path == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(path))
                throw new ArgumentException();

            if (count < 1)
                throw new ArgumentOutOfRangeException();

            return Search(path, 0, count);
        }

        /// <summary>
        /// Reports the offset of the first occurrence of the specified amount of free space in the specified file.
        /// </summary>
        /// <param name="path">The path string from which to look for free space.</param>
        /// <param name="offset">The offset to start the search.</param>
        /// <param name="count">The number of bytes to search.</param>
        /// <returns>The zero-based offset of the first match if enough free space is found, or -1 if it is not.</returns>
        /// <exception cref="System.ArgumentNullException">path is null.</exception>
        /// <exception cref="System.ArgumentException">path is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by System.IO.Path.InvalidPathChars.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">offset is out of range.
        /// This parameter requires a non-negative number.
        /// -or- count is out of range. This parameter requires a value higher than zero.</exception>
        public static int Search(string path, int offset, int count)
        {
            if (path == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(path))
                throw new ArgumentException();

            if (offset < 0 || count < 1)
                throw new ArgumentOutOfRangeException();

            using (BinaryReader reader =
                new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
            {
                byte[] buffer = new byte[chunkSize];
                int maxLoop = (int)reader.BaseStream.Length / chunkSize;

                int maxBuffer = 0;
                bool match = false;

                reader.BaseStream.Position = offset;

                for (int i = 0; i < maxLoop; i++)
                {
                    buffer = reader.ReadBytes(chunkSize);

                    if (buffer.Length < count)
                        return -1;

                    maxBuffer = buffer.Length > count ? (buffer.Length - count) : 1;

                    for (int j = 0; j < maxBuffer; j += count)
                    {
                        if (buffer[j + (count - 1)] == freeSpaceByte)
                        {
                            if (buffer[j] == freeSpaceByte)
                            {
                                match = true;

                                for (int k = j + (count - 2); k > j; k--)
                                {
                                    if (buffer[k] != freeSpaceByte)
                                    {
                                        match = false;
                                        break;
                                    }
                                }

                                if (match)
                                    return offset + j + (chunkSize * i);
                            }
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets or sets the byte that represents free space.
        /// </summary>
        public static byte FreeSpaceByte
        {
            get { return freeSpaceByte; }
            set { freeSpaceByte = value; }
        }

        /// <summary>
        /// Gets or sets the length of the search chunks. 
        /// </summary>
        public static int ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        private static byte freeSpaceByte = 0xff;
        private static int chunkSize = 65536;
    }
}
