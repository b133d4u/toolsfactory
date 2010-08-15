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
using System.Windows.Forms;
using System.IO;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static methods to handle exceptions.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Displays a simple error message for a thrown exception.
        /// </summary>
        /// <param name="exception">The exception to take the message box text from.</param>
        public static void ShowMessage(Exception exception)
        {
            try
            {
                IWin32Window owner = Form.ActiveForm;
                ShowMessage(owner, exception.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Displays a simple error message for a thrown exception.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        public static void ShowMessage(string text)
        {
            try
            {
                IWin32Window owner = Form.ActiveForm;
                ShowMessage(owner, text);
            }
            catch (Exception ex)
            { 
               LogException(ex);
            }
        }

        /// <summary>
        /// Displays a simple error message for a thrown exception.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="text">The text to display in the message box.</param>
        public static void ShowMessage(IWin32Window owner, string text)
        {
            try
            {
                MessageBoxHelper.Show(owner, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Records a thrown exception to the error log file.
        /// </summary>
        /// <param name="exception">The exception to record.</param>
        public static void LogException(Exception exception)
        {
            try
            {
                loggedException = exception;

                const string separator = "|";
                StringBuilder logBuilder = new StringBuilder();

                logBuilder.AppendFormat("{0:u}", DateTime.Now).Append(separator)
                    .Append(Environment.OSVersion.Version.ToString()).Append(separator)
                    .Append(AssemblyHelper.Title).Append(separator)
                    .Append(AssemblyHelper.Version).Append(separator)
                    .Append(exception.Message);

                string path = Path.Combine(AssemblyHelper.DirectoryName, logFile);

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(logBuilder.ToString());
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the last recorded exception.
        /// </summary>
        public static Exception LoggedException
        {
            get { return loggedException; }
        }

        /// <summary>
        /// Gets or set the log file to record exceptions to.
        /// </summary>
        public static string LogFile
        {
            get { return logFile; }
            set { logFile = value; }
        }

        /// <summary>
        /// Gets the base name pattern to use for the log file.
        /// </summary>
        public static string BaseFileName = "Errors.log";
  
        private static Exception loggedException = new Exception();
        private static string logFile = String.Empty;
    }
}
