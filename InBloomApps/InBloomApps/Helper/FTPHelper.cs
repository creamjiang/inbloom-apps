/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web;

namespace InBloomApps.Helper
{
    public class FTPHelper
    {
        internal static string ftphost = ConfigurationManager.AppSettings["FtpServerUrl"];
        internal static string user = ConfigurationManager.AppSettings["FtpServerUser"];
        internal static string pass = ConfigurationManager.AppSettings["FtpServerPass"];
        internal static bool useSSL = bool.Parse(ConfigurationManager.AppSettings["FtpUseSSL"]);

        /// <summary>
        /// Uploads the given file stream to the FTP server
        /// </summary>
        /// <param name="ftpfilepath"></param>
        /// <param name="stream"></param>
        public static void uploadFileFromStream(string directory, string ftpfilepath, Stream stream)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateServerCertificate);
            byte[] buffer = new byte[stream.Length];
            try
            {
                // upload to that directory
                string ftpfullpath = ftphost + "/" + directory + ftpfilepath;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(user, pass);
                //userid and password for the ftp server to given  
                ftp.EnableSsl = useSSL;
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;

                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    // directory doesnt exist
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFilenameNotAllowed)
                    {
                        createDir(directory);

                        // stream back from buffer
                        Stream s = new MemoryStream(buffer);
                        uploadFileFromStream(directory, ftpfilepath, s);
                    }
                }
            }
        }

        /// <summary>
        /// Delete the given dir from the FTP server
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static bool removeDir(string dirPath)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                   new RemoteCertificateValidationCallback(ValidateServerCertificate);

                string ftpfullpath = ftphost + "/" + dirPath;

                // delete all the files on the directory firts
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpfullpath);
                ftpRequest.Credentials =new NetworkCredential(user, pass);
                ftpRequest.EnableSsl = useSSL;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse listFilesResponse = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(listFilesResponse.GetResponseStream());

                List<string> directories = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    string filepath = ftphost + "/" + line;
                    FtpWebRequest ftpDeleteFileReq = (FtpWebRequest)FtpWebRequest.Create(filepath);
                    ftpDeleteFileReq.Credentials = new NetworkCredential(user, pass);
                    ftpDeleteFileReq.EnableSsl = useSSL;
                    ftpDeleteFileReq.KeepAlive = false;
                    ftpDeleteFileReq.UseBinary = true;
                    ftpDeleteFileReq.Method = WebRequestMethods.Ftp.DeleteFile;
                    FtpWebResponse deleteFileResponse = (FtpWebResponse)ftpDeleteFileReq.GetResponse();
                    deleteFileResponse.Close();
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(user, pass);
                ftp.EnableSsl = useSSL;
                ftp.KeepAlive = false;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete the given file from the FTP server
        /// </summary>
        /// <param name="ftpfilepath"></param>
        /// <returns></returns>
        public static bool deleteFile(string ftpfilepath)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateServerCertificate);

            string ftpfullpath = ftphost + ftpfilepath;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
            ftp.Credentials = new NetworkCredential(user, pass);
            //userid and password for the ftp server to given  
            ftp.EnableSsl = useSSL;
            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            response.Close();
            return true;
        }

        /// <summary>
        /// Downloads the specified file from the FTP server
        /// </summary>
        /// <param name="ftpfilepath"></param>
        /// <returns></returns>
        public static Stream downloadFile(string ftpfilepath)
        {
            ServicePointManager.ServerCertificateValidationCallback =
               new RemoteCertificateValidationCallback(ValidateServerCertificate);

            string ftpfullpath = ftphost + ftpfilepath;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
            ftp.Credentials = new NetworkCredential(user, pass);
            //userid and password for the ftp server to given  
            ftp.EnableSsl = useSSL;
            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            Stream fileStream = response.GetResponseStream();
            return fileStream;
        }

        /// <summary>
        /// Creates a directory with the given name
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public static bool createDir(string directoryName)
        {
            try
            {
                //create the directory
                string dirPath = ftphost + "/" + directoryName;
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(dirPath));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(user, pass);
                requestDir.UsePassive = true;
                requestDir.EnableSsl = useSSL;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns true if the given directory exists
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public static bool checkDirExists(string directoryName)
        {
            try
            {
                FtpWebRequest request = 
                    (FtpWebRequest)WebRequest.Create(ftphost + "/" + directoryName);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(user, pass);
                request.EnableSsl = useSSL;
                request.KeepAlive = false;
                request.Method = WebRequestMethods.Ftp.PrintWorkingDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;  
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;  
                    }
                }
            }

            return false;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            System.Diagnostics.Debug.WriteLine(certificate);
            return true;
        } 
}
}