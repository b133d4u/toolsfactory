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

// References:
// http://stackoverflow.com/questions/2623761/marshal-ptrtostructure-and-back-again-and-generic-solution-for-endianness-swapp
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static helper methods to manage structures.
    /// </summary>
    public static class StructureHelper
    {
        /// <summary>
        /// Maps a byte array into a class representing a structure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawData">The byte array to map.</param>
        /// <returns>The mapped class.</returns>
        public static T BytesToClass<T>(ref byte[] rawData) where T : class
        {
            T result = default(T);
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        /// <summary>
        /// Maps a class representing a structure to a byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The class to map.</param>
        /// <returns>The mapped byte array.</returns>
        public static byte[] ClassToBytes<T>(T data) where T : class
        {
            byte[] rawData = new byte[Marshal.SizeOf(typeof(T))];
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
            }
            finally
            {
                handle.Free();
            }

            return rawData;
        }

        /// <summary>
        /// Maps a byte array into a struct.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rawData">The byte array to map.</param>
        /// <returns>The mapped struct.</returns>
        public static T BytesToStruct<T>(ref byte[] rawData) where T : struct
        {
            T result = default(T);
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

            try
            {
                result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }

            return result;
        }

        /// <summary>
        /// Maps a struct to a byte array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The struct to map.</param>
        /// <returns>The mapped byte array.</returns>
        public static byte[] StructToBytes<T>(T data) where T : struct
        {
            byte[] rawData = new byte[Marshal.SizeOf(data)];
            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            
            try
            {
                Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
            }
            finally
            {
                handle.Free();
            }

            return rawData;
        }
    }
}
