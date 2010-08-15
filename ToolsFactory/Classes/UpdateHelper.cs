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
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static helper methods to check for updates and update an application.
    /// </summary>
    public static class UpdateHelper
    {
        public class UpdateInfo
        {
            public bool IsUpdateAvailable;
            public string UpdateVersion;
            public string UpdateUrl;
        }

        /// <summary>
        /// Checks if there are any updates available for the application.
        /// </summary>
        /// <returns>A UpdateHelper.UpdateInfo representing the received update data.</returns>
        public static UpdateInfo CheckUpdates()
        {
            try
            {
                updateData = null;

                WebClient downloader = new WebClient();
                string[] update = downloader.DownloadString(updateCheckUrl).Split(';');

                if (update.Length >= 2)
                {
                    updateData = new UpdateInfo();
                    updateData.UpdateVersion = update[0];
                    updateData.UpdateUrl = update[1];

                    string[] current = AssemblyHelper.Version.Split('.');
                    string[] latest = update[0].Split('.');

                    if (latest.Length == current.Length)
                    {
                        bool validNumber = true;
                        int[] latestVersion = new int[latest.Length];

                        for (int i = 0; i < latest.Length; i++)
                            validNumber &= Int32.TryParse(latest[i], out latestVersion[i]);

                        if (validNumber)
                        {
                            int[] currentVersion = new int[current.Length];

                            for (int i = 0; i < current.Length; i++)
                                Int32.TryParse(current[i], out currentVersion[i]);

                            for (int i = 0; i < currentVersion.Length; i++)
                            {
                                if (latestVersion[i] > currentVersion[i])
                                {
                                    updateData.IsUpdateAvailable = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                return updateData;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                return updateData;
            }
        }

        /// <summary>
        /// Downloads the update archive for the application.
        /// </summary>
        /// <returns>The downloaded update archive.</returns>
        public static byte[] DownloadUpdate()
        {
            return DownloadUpdate(null);
        }

        /// <summary>
        /// Downloads the update archive for the application.
        /// </summary>
        /// <param name="downloadWorker"></param>
        /// <returns>The download update archive.</returns>
        public static byte[] DownloadUpdate(BackgroundWorker downloadWorker)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UpdateHelper.UpdateData.UpdateUrl);
                request.Proxy = HttpWebRequest.DefaultWebProxy;

                return HttpHelper.DownloadData(request, downloadWorker);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
                return null;
            }
        }

        /// <summary>
        /// Extracts the content of the update archive.
        /// </summary>
        /// <param name="buffer">The input to extract.</param>
        public static void ExtractUpdateFiles(byte[] buffer)
        {
            try
            {
                string assemblyPath = AssemblyHelper.DirectoryName;
                string targetPath = Path.GetTempPath();

                targetPath = Path.Combine(targetPath, Guid.NewGuid().ToString("n"));

                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                if (ZipHelper.Extract(buffer, targetPath, true))
                {
                    string[] extractedFiles = Directory.GetFiles(targetPath, "*.*");
                    string destFileName = String.Empty;

                    for (int i = 0; i < extractedFiles.Length; i++)
                    {
                        if (extractedFiles[i].IndexOf(exeExt, StringComparison.Ordinal) == -1)
                        {
                            destFileName = Path.Combine(assemblyPath,
                                Path.GetFileName(extractedFiles[i]));

                            if (File.Exists(destFileName))
                                File.Delete(destFileName);

                            File.Move(extractedFiles[i], destFileName);
                        }
                        else
                        {
                            destFileName = Path.Combine(assemblyPath,
                                Path.GetFileNameWithoutExtension(extractedFiles[i]) + exeNewExt);

                            if (File.Exists(destFileName))
                                File.Delete(destFileName);

                            File.Move(extractedFiles[i], destFileName);
                        }
                    }

                    Directory.Delete(targetPath);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        /// <summary>
        /// Restarts the application launching the updated executable.
        /// </summary>
        public static void RestartExecutable()
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(AssemblyHelper.DirectoryName,
                Path.GetFileNameWithoutExtension(AssemblyHelper.FileName) + exeNewExt);

            process.Start(true);
        }

        /// <summary>
        /// Renames the updated application executable backing up the outdated one.
        /// </summary>
        public static void RenameExecutable()
        {
            if (AssemblyHelper.FileName.IndexOf(exeNewExt, StringComparison.Ordinal) == -1)
                return;

            string path = Path.GetFileNameWithoutExtension(AssemblyHelper.FileName);

            while (Path.HasExtension(path))
                path = Path.GetFileNameWithoutExtension(path);

            path = Path.Combine(AssemblyHelper.DirectoryName, path);

            string oldPath = path + exeExt;
            string newPath = path + exeNewExt;
            string backupPath = oldPath + bakExt;

            try
            {
                if (File.Exists(backupPath))
                    File.Delete(backupPath);

                File.Move(oldPath, backupPath);
                File.Move(newPath, oldPath);
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
        }

        /// <summary>
        /// Gets or sets the address for update checks.
        /// </summary>
        public static string UpdateCheckUrl
        {
            get { return updateCheckUrl; }
            set { updateCheckUrl = value; }
        }

        /// <summary>
        /// Gets the received update data.
        /// </summary>
        public static UpdateInfo UpdateData
        {
            get { return updateData; }
        }

        /// <summary>
        /// Gets the base address pattern to use for update checks.
        /// </summary>
        public static string UpdateCheckBase = "http://toolsfactory.googlecode.com/svn/updates/";
        
        /// <summary>
        /// Gets the file extensions used by the update data.
        /// </summary>
        public static string FileExtension = ".txt";

        private static string updateCheckUrl = String.Empty;
        private static UpdateInfo updateData;
        private static string exeExt = ".exe";
        private static string exeNewExt = ".new.exe";
        private static string bakExt = ".bak";
    }
}
