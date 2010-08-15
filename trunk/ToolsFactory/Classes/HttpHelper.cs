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
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Text;

// References:
// http://wiki.forum.nokia.com/index.php/HTTP_Post_multipart_file_upload_in_Java_ME
// ---

namespace HackMew.ToolsFactory
{
    /// <summary>
    /// Provides static helper methods for HTTP-based requests.
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Returns a GET response from an Internet resource.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A System.Net.HttpWebResponse that contains the response from the Internet resource.</returns>
        /// <exception cref="System.ArgumentNullException">request is null.</exception>
        public static HttpWebResponse Get(HttpWebRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Returns a POST response from an Internet resource.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestData"></param>
        /// <returns>A System.Net.HttpWebResponse that contains the response from the Internet resource.</returns>
        /// <exception cref="System.ArgumentNullException">request is null.
        /// -or- requestData is null.</exception>
        /// <exception cref="System.ArgumentException">requestData is the empty string ("").</exception>
        public static HttpWebResponse PostData(HttpWebRequest request, string requestData)
        {
            if (request == null || requestData == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(requestData))
                throw new ArgumentException();

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (Stream postStream = request.GetRequestStream())
            {
                byte[] postData = Encoding.ASCII.GetBytes(requestData);
                postStream.Write(postData, 0, postData.Length);
            }

            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Returns a byte array representing the contents of an Internet resource. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A byte array that contains the downloaded file contents.</returns>
        /// <exception cref="System.ArgumentNullException">request is null.</exception>
        public static byte[] DownloadData(HttpWebRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            return DownloadData(request, null);
        }

        /// <summary>
        /// Returns a byte array representing the downloaded content of an Internet resource. 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="worker"></param>
        /// <returns>A byte array that contains the downloaded file content.</returns>
        /// <exception cref="System.ArgumentNullException">request is null.</exception>
        public static byte[] DownloadData(HttpWebRequest request, BackgroundWorker worker)
        {
            if (request == null)
                throw new ArgumentNullException();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    const int bufferLength = 8192;
                    byte[] buffer = new byte[bufferLength];

                    int bytesRead = 0;
                    int currentBytes = 0;
                    int totalBytes = (int)response.ContentLength;

                    using (MemoryStream memory = new MemoryStream())
                    {
                        do
                        {
                            bytesRead = responseStream.Read(buffer, 0, bufferLength);

                            if (bytesRead > 0)
                            {
                                memory.Write(buffer, 0, bytesRead);
                                currentBytes += bytesRead;

                                if (worker != null)
                                {
                                    if (!worker.CancellationPending && worker.WorkerReportsProgress)
                                    {
                                        worker.ReportProgress((int)((double)currentBytes * 100.0 / totalBytes));
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                            }

                        } while (bytesRead > 0);

                        return memory.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Returns an upload response from an Internet resource.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="formData">A collection of form data to send.</param>
        /// <param name="fileField">A string representing the field value for the file to upload.</param>
        /// <param name="filePath">A string representing the file upload path.</param>
        /// <param name="fileBuffer">A byte array containing the file contents to upload.</param>
        /// <returns>A System.Net.HttpWebResponse that contains the response from the Internet resource.</returns>
        /// <exception cref="System.ArgumentNullException">request is null.
        /// -or- fileField is null.
        /// -or- filePath is null.
        /// -or- fileBuffer is null.</exception>
        /// <exception cref="System.ArgumentException">fileField is the empty string ("").
        /// -or- filePath is the empty string ("").</exception>
        public static HttpWebResponse UploadData(HttpWebRequest request, Dictionary<string, string> formData,
            string fileField, string filePath, byte[] fileBuffer)
        {
            if (request == null || fileField == null || filePath == null || fileBuffer == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(fileField) || string.IsNullOrEmpty(filePath))
                throw new ArgumentException();

            const string CRLF = "\r\n";
            string boundary = new string('-', 10) + DateTime.Now.Ticks.ToString("x");
            string endBoundary = CRLF + "--" + boundary + "--" + CRLF;

            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            StringBuilder boundaryMessage = new StringBuilder("--").Append(boundary);

            foreach (KeyValuePair<string, string> kvp in formData)
            {
                boundaryMessage.Append("Content-Disposition: form-data; name=\"").Append(kvp.Key)
                    .Append("\"").Append(CRLF).Append(CRLF).Append(kvp.Value).Append(CRLF)
                    .Append("--").Append(boundary).Append(CRLF);
            }

            boundaryMessage.Append("Content-Disposition: form-data; name=\"").Append(fileField)
                .Append("\"; filename=\"").Append(filePath).Append("\"").Append(CRLF)
                .Append("Content-Type: ").Append("application/octet-stream")
                .Append(CRLF).Append(CRLF);

            using (Stream postStream = request.GetRequestStream())
            {
                byte[] postData = Encoding.ASCII.GetBytes(boundaryMessage.ToString());
                postStream.Write(postData, 0, postData.Length);

                postStream.Write(fileBuffer, 0, fileBuffer.Length);

                postData = Encoding.ASCII.GetBytes(endBoundary);
                postStream.Write(postData, 0, postData.Length);
            }

            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Indicates whether the specified string represents a valid proxy.
        /// </summary>
        /// <param name="proxy">The proxy string to test.</param>
        /// <returns>true if the strings represents a valid proxy;
        /// otherwhise false.</returns>
        public static bool IsValidProxy(string proxy)
        {
            if (!String.IsNullOrEmpty(proxy))
            {
                string[] splitted = proxy.Split(':');

                if (splitted.Length == 2)
                {
                    IPAddress address;
                    ushort port;

                    return IPAddress.TryParse(splitted[0], out address) &&
                        UInt16.TryParse(splitted[1], out port) &&
                        port > 0;
                }
            }

            return false;
        }
    }
}
