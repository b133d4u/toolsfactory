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
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

// References:
// http://www.pkware.com/documents/casestudies/APPNOTE.TXT
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static helper methods for handling .zip archive files.
    /// </summary>
    public static class ZipHelper
    {
        private enum Signature
        {
            LocalFileHeader = 0x04034b50,
            CentralFileHeader = 0x02014b50,
            CentralDirectoryEnd = 0x06054b50,
        }

        private enum CompressionMethod
        {
            Store = 0,
            Shrink = 1,
            Reduce1 = 2,
            Reduce2 = 3,
            Reduce3 = 4,
            Reduce4 = 5,
            Implode = 6,
            Tokenize = 7,
            Deflate = 8,
            Deflate64 = 9,
            PkwareImplode = 10,
            Bzip2 = 12,
            Lzma = 14,
            IbmTerse = 18,
            IbmLz77 = 19,
            WavPack = 97,
            Ppmd = 98,
        }

        private enum LanguageEncoding
        {
            IBM437 = 437,
            UTF8 = 65001,
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private class LocalFileHeader
        {
            public uint Signature;
            public ushort VersionNeededToExtract;
            public ushort GeneralPurposeBitFlag;
            public ushort CompressionMethod;
            public ushort LastModifiedFileTime;
            public ushort LastModifiedFileDate;
            public uint Crc32;
            public uint CompressedSize;
            public uint UncompressedSize;
            public ushort FileNameLength;
            public ushort ExtraFieldLength;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private class CentralFileHeader
        {
            public uint Signature;
            public ushort VersionMadeBy;
            public ushort VersionNeededToExtract;
            public ushort GeneralPurposeBitFlag;
            public ushort CompressionMethod;
            public ushort LastModifiedFileTime;
            public ushort LastModifiedFileDate;
            public uint Crc32;
            public uint CompressedSize;
            public uint UncompressedSize;
            public ushort FileNameLength;
            public ushort ExtraFieldLength;
            public ushort FileCommentLength;
            public ushort DiskNumberStart;
            public ushort InternalFileAttributes;
            public uint ExternalFileAttributes;
            public uint HeaderRelativeOffset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private class CentralDirectoryEnd
        {
            public uint Signature;
            public ushort DiskNumber;
            public ushort CentralDirectoryDiskNumber;
            public ushort DiskTotalEntries;
            public ushort TotalEntries;
            public uint CentralDirectorySize;
            public uint CentralDirectoryOffset;
            public ushort FileCommentLength;
        }

        /// <summary>
        /// Decompress a compressed (deflated) byte array.
        /// </summary>
        /// <param name="compressedData">The input to decompress.</param>
        /// <returns></returns>
        private static byte[] Inflate(byte[] compressedData)
        {
            using (DeflateStream deflate =
                    new DeflateStream(new MemoryStream(compressedData), CompressionMode.Decompress, true))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    const int bufferLength = 8192;
                    byte[] deflateBuffer = new byte[bufferLength];
                    int bytesRead = 0;

                    do
                    {
                        bytesRead = deflate.Read(deflateBuffer, 0, bufferLength);

                        if (bytesRead > 0)
                            memory.Write(deflateBuffer, 0, bytesRead);

                    } while (bytesRead > 0);

                    return memory.ToArray();
                }
            }
        }

        /// <summary>
        /// Extract a .zip archive file content to the target path.
        /// </summary>
        /// <param name="zipFile">The .zip archive file to extract.</param>
        /// <param name="targetDirectory">The destination of the extracted files.</param>
        /// <param name="overwrite">true to overwrite existing files; false otherwhise.</param>
        /// <returns>true if all the files were extracted successfully; false if some problems occurred.</returns>
        public static bool Extract(string zipFile, string targetDirectory, bool overwrite)
        {
            return Extract(File.ReadAllBytes(zipFile), targetDirectory, overwrite);
        }

        /// <summary>
        /// Extract a .zip archive file content to the target path.
        /// </summary>
        /// <param name="zipBuffer">The input to extract.</param>
        /// <param name="targetDirectory">The destination of the extracted files.</param>
        /// <param name="overwrite">true to overwrite existing files; false otherwhise.</param>
        /// <returns>true if all the files were extracted successfully; false if some problems occurred.</returns>
        public static bool Extract(byte[] zipBuffer, string targetDirectory, bool overwrite)
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(CentralDirectoryEnd))];

            if (zipBuffer.Length <= buffer.Length)
                return false;

            uint signatureValue = (uint)Signature.CentralDirectoryEnd;

            byte[] signature = new byte[] {
                (byte)(signatureValue & 0xff),
                (byte)((signatureValue >> 0x8) & 0xff),
                (byte)((signatureValue >> 0x10) & 0xff),
                (byte)(signatureValue >> 0x18),
            };

            for (int i = zipBuffer.Length - signature.Length; i >= 0; i--)
            {
                if (zipBuffer[i] == signature[0] &&
                    zipBuffer[i + 1] == signature[1] &&
                    zipBuffer[i + 2] == signature[2] &&
                    zipBuffer[i + 3] == signature[3])
                {
                    Array.Copy(zipBuffer, i, buffer, 0, buffer.Length);
                    break;
                }
            }

            CentralDirectoryEnd cde = StructureHelper.BytesToClass<CentralDirectoryEnd>(ref buffer);

            if (cde.TotalEntries == 0 || cde.CentralDirectorySize == 0)
                return false;

            CentralFileHeader cfh;
            buffer = new byte[Marshal.SizeOf(typeof(CentralFileHeader))];

            uint count = cde.CentralDirectorySize;
            uint offset = cde.CentralDirectoryOffset;
            uint skip = 0;

            List<uint> lfhOffsets = new List<uint>();

            do
            {
                Array.Copy(zipBuffer, offset, buffer, 0, buffer.Length);
                cfh = StructureHelper.BytesToClass<CentralFileHeader>(ref buffer);

                if (cfh.Signature == (uint)Signature.CentralFileHeader &&
                    (cfh.CompressionMethod == (ushort)CompressionMethod.Store ||
                    cfh.CompressionMethod == (ushort)CompressionMethod.Deflate) &&
                    cfh.FileNameLength > 0)
                {
                    lfhOffsets.Add(cfh.HeaderRelativeOffset);
                }

                skip = (uint)buffer.Length + cfh.FileNameLength +
                    cfh.ExtraFieldLength + cfh.FileCommentLength;

                count -= skip;
                offset += skip;

            } while (count > 0);

            if (lfhOffsets.Count != cde.TotalEntries)
                return false;

            LocalFileHeader lfh;
            buffer = new byte[Marshal.SizeOf(typeof(LocalFileHeader))];

            for (int i = 0; i < lfhOffsets.Count; i++)
            {
                Array.Copy(zipBuffer, lfhOffsets[i], buffer, 0, buffer.Length);
                lfh = StructureHelper.BytesToClass<LocalFileHeader>(ref buffer);

                if (lfh.Signature == (uint)Signature.LocalFileHeader &&
                    lfh.FileNameLength > 0)
                {
                    byte[] uncompressedData = new byte[0];

                    if (lfh.UncompressedSize > 0)
                    {
                        byte[] compressedData = new byte[lfh.CompressedSize];

                        Array.Copy(zipBuffer, lfhOffsets[i] + buffer.Length + lfh.FileNameLength + lfh.ExtraFieldLength,
                            compressedData, 0, lfh.CompressedSize);

                        if (lfh.CompressionMethod == (ushort)CompressionMethod.Deflate)
                        {
                            uncompressedData = Inflate(compressedData);
                        }
                        else if (lfh.CompressionMethod == (ushort)CompressionMethod.Store)
                        {
                            uncompressedData = new byte[lfh.UncompressedSize];
                            compressedData.CopyTo(uncompressedData, 0);
                        }
                    }

                    if (uncompressedData.Length == lfh.UncompressedSize &&
                        Crc.ComputeCrc32(uncompressedData) == lfh.Crc32)
                    {
                        int codePage = 0;

                        if (((lfh.GeneralPurposeBitFlag >> 0xB) & 1) != 1)
                        {
                            codePage = (int)LanguageEncoding.IBM437;
                        }
                        else
                        {
                            codePage = (int)LanguageEncoding.UTF8;
                        }

                        string outputPath =
                            Path.Combine(targetDirectory, Encoding.GetEncoding(codePage).GetString(zipBuffer,
                            (int)lfhOffsets[i] + buffer.Length, lfh.FileNameLength));

                        if (!File.Exists(outputPath) || overwrite)
                            File.WriteAllBytes(outputPath, uncompressedData);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
