/*
FTPFactory.cs
Better view with tab space=4

Written by Jaimon Mathew (jaimonmathew@rediffmail.com)
Rolander,Dan (Dan.Rolander@marriott.com) has modified the 
Download
method to cope with file name with path information. He also 
provided
the XML comments so that the library provides Intellisense 
descriptions.

use the following line to compile
csc /target:library /out:FTPLib.dll /r:System.DLL FTPFactory.cs
*/

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace FtpLib
{
    public class FTPFactory
    {
        private string hostName;
        private string remotePath;
        private string userName;
        private string password;
        private string mes;

        private int port;
        private int bytes;
        private Socket clientSocket;

        private int retValue;
        private Boolean debug;
        private Boolean logined;
        private string reply;

        private static int BLOCK_SIZE = 512;

        private Byte[] buffer = new Byte[BLOCK_SIZE];
        private Encoding ASCII = Encoding.ASCII;
        private int retries;

        public FTPFactory()
        {
            Port = 21;
            Debug = false;
            logined = false;
        }

        public FTPFactory(string hostName, string password, string username, int port, int retries)
        {
            HostName = hostName;
            Password = password;
            UserName = username;
            Port = port;
            this.retries = retries == 0 ? 1 : retries;
        }

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        public string RemotePath
        {
            get { return remotePath; }
            set { remotePath = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        public int RetValue
        {
            get { return retValue; }
            set { retValue = value; }
        }


        public string[] GetFileList(string mask)
        {
            if (!logined)
            {
                Login();
            }

            Socket cSocket = CreateDataSocket();
            Logger.Write(new LogEntry(
                                       "NLST " + mask,
                                       "FTP.Information",
                                       0,
                                       0,
                                       TraceEventType.Information,
                                       null,
                                       null));
            SendCommand("NLST " + mask);
            Logger.Write(new LogEntry(
                                     "done",
                                     "FTP.Information",
                                     0,
                                     0,
                                     TraceEventType.Information,
                                     null,
                                     null));
            if (!(RetValue == 150 || RetValue == 125))
            {
                throw new IOException(reply.Substring(4));
            }

            mes = "";

            while (bytes > 0)
            {
                bytes = cSocket.Receive(buffer, buffer.Length, 0);
                mes += ASCII.GetString(buffer, 0, bytes);
            }



            string[] mess;

            mess = mes.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (mess.Length == 0)
            {
                mess = mes.Split(new char[] { '\n' });
            }

            cSocket.Close();

            ReadReply();

            if (RetValue != 226)
            {
                throw new IOException(reply.Substring(4));
            }
            return mess;
        }

        ///
        /// Return the size of a file.
        ///
        ///
        ///
        public long FileSize(string fileName)
        {
            if (!logined)
            {
                Login();
            }

            SendCommand("SIZE " + fileName);
            long size;

            if (RetValue == 213)
            {
                size = Int64.Parse(reply.Substring(4));
            }
            else
            {
                throw new IOException(reply.Substring(4));
            }

            return size;
        }

        ///
        /// Login to the remote server.
        ///
        public void Login()
        {
          

            clientSocket = new
                Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



            IPEndPoint ep = new
                IPEndPoint(Dns.GetHostEntry(HostName).AddressList[0], Port);

        

            for (int attempts = 1; attempts < retries + 1; attempts++)
            {
                try
                {
                    clientSocket.Connect(ep);
                }
                catch (Exception)
                {

                    if (attempts == retries)
                    {
                        throw new IOException("Couldn't connect to remote server");
                    }
                    else
                    {
                        continue;
                    }
                }

             
                ReadReply();

           

                if (RetValue != 220)
                {
                    if (attempts == retries)
                    {
                        Cleanup();
                        throw new IOException(reply.Substring(4));
                    }
                    else
                    {
                        continue;
                    }
                }
                if (debug)
                {
                    Logger.Write(new LogEntry(
                           "USER " + UserName,
                           "FTP.Information",
                           0,
                           0,
                           TraceEventType.Information,
                           null,
                           null));


                }


                SendCommand("USER " + UserName);

                if (!(RetValue == 331 || RetValue == 230))
                {
                    Cleanup();
                    throw new IOException(reply.Substring(4));
                }

                if (RetValue != 230)
                {
                    if (Debug)
                    {
                        Logger.Write(new LogEntry(
                                         "PASS xxx",
                                         "FTP.Information",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
                    }
                    SendCommand("PASS " + Password);
                    if (!(RetValue == 230 || RetValue == 202))
                    {
                        Cleanup();
                        throw new IOException(reply.Substring(4));
                    }
                }

                logined = true;
                if (Debug)
                {
                    Logger.Write(new LogEntry(
                                     "Connected to " + HostName,
                                     "FTP.Information",
                                     0,
                                     0,
                                     TraceEventType.Information,
                                     null,
                                     null));
                }

                ChangeDirectory(RemotePath);
                break;
            }
        }

        ///
        /// If the value of mode is true, set binary mode for downloads.
        /// Else, set Ascii mode.
        ///
        ///
        public void SetBinaryMode(Boolean mode)
        {
            if (mode)
            {
                SendCommand("TYPE I");
            }
            else
            {
                SendCommand("TYPE A");
            }

       
            
            if (RetValue != 200)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Download a file to the Assembly's local directory,
        /// keeping the same file name.
        ///
        ///
        public void Download(string remFileName)
        {
            Download(remFileName, "", false);
        }

        ///
        /// Download a remote file to the Assembly's local directory,
        /// keeping the same file name, and set the resume flag.
        ///
        ///
        ///
        public void Download(string remFileName, Boolean resume)
        {
            Download(remFileName, "", resume);
        }

        ///
        /// Download a remote file to a local file name which can include
        /// a path. The local file name will be created or overwritten,
        /// but the path must exist.
        ///
        ///
        ///
        public void Download(string remFileName, string locFileName)
        {
            Download(remFileName, locFileName, false);
        }

        ///
        /// Download a remote file to a local file name which can include
        /// a path, and set the resume flag. The local file name will be
        /// created or overwritten, but the path must exist.
        ///
        ///
        ///
        ///
        public void Download(string remFileName, string locFileName, Boolean resume)
        {
            if (!logined)
            {
                Login();
            }

            SetBinaryMode(true);

            if (debug)
            {
                Logger.Write(new LogEntry(
                       "Downloading file " + remFileName + " from " + HostName + "/" + RemotePath + " to : " + locFileName,
                       "FTP.Information",
                       0,
                       0,
                       TraceEventType.Information,
                       null,
                       null));


            }

            if (locFileName.Equals(""))
            {
                locFileName = remFileName;
            }

            if (!File.Exists(locFileName))
            {
                Stream st = File.Create(locFileName);
                st.Close();
            }

            FileStream output = new
                FileStream(locFileName, FileMode.Open);

            Socket cSocket = CreateDataSocket();

            long offset;

            if (resume)
            {
                offset = output.Length;

                if (offset > 0)
                {
                    SendCommand("REST " + offset);
                    if (RetValue != 350)
                    {
                        //throw new IOException(reply.Substring(4));
                        //Some servers may not support resuming.
                        offset = 0;
                    }
                }

                if (offset > 0)
                {
                    if (Debug)
                    {
                        if (Debug)
                        {
                            Logger.Write(new LogEntry(
                                            "seeking to " + offset,
                                             "FTP.Information",
                                             0,
                                             0,
                                             TraceEventType.Information,
                                             null,
                                             null));
                        }
                    }
                    long npos = output.Seek(offset, SeekOrigin.Begin);
                    Console.WriteLine("new pos=" + npos);
                }
            }

            SendCommand("RETR " + remFileName);
            if (Debug)
            {
                Logger.Write(new LogEntry(
                                "RETR " + remFileName,
                                 "FTP.Information",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
            }

            if (!(RetValue == 150 || RetValue == 125))
            {
                throw new IOException(reply.Substring(4));
            }

            while (true)
            {
                bytes = cSocket.Receive(buffer, buffer.Length, 0);
                output.Write(buffer, 0, bytes);

                if (bytes <= 0)
                {
                    break;
                }
            }

            output.Close();
            if (cSocket.Connected)
            {
                cSocket.Close();
            }

            Console.WriteLine("");

            ReadReply();

            if (!(RetValue == 226 || RetValue == 250))
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Upload a file.
        ///
        ///
        public void Upload(string fileName)
        {
            Upload(fileName, false);
        }

        ///
        /// Upload a file and set the resume flag.
        ///
        ///
        ///
        public void Upload(string fileName, Boolean resume)
        {
            if (!logined)
            {
                Login();
            }

            Socket cSocket = CreateDataSocket();
            long offset = 0;

            if (resume)
            {
                try
                {
                    SetBinaryMode(true);
                    offset = FileSize(fileName);
                }
                catch (Exception)
                {
                    offset = 0;
                }
            }

            if (offset > 0)
            {
                SendCommand("REST " + offset);
                if (RetValue != 350)
                {
                    //throw new IOException(reply.Substring(4));
                    //Remote server may not support resuming.
                    offset = 0;
                }
            }

            SendCommand("STOR " + Path.GetFileName(fileName));

            if (!(RetValue == 125 || RetValue == 150))
            {
                throw new IOException(reply.Substring(4));
            }

            // open input stream to read source file
            FileStream input = new FileStream(fileName, FileMode.Open);

            if (offset != 0)
            {
                if (Debug)
                {
                    Console.WriteLine("seeking to " + offset);
                }
                input.Seek(offset, SeekOrigin.Begin);
            }

            if (Debug)
            {
                Logger.Write(new LogEntry(
                                "Uploading file " + fileName + " to  " + RemotePath,
                                 "FTP.Information",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
            }

            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                cSocket.Send(buffer, bytes, 0);
            }
            input.Close();



            if (cSocket.Connected)
            {
                cSocket.Close();
            }

            ReadReply();
            if (!(RetValue == 226 || RetValue == 250))
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Delete a file from the remote FTP server.
        ///
        ///
        public void DeleteRemoteFile(string fileName)
        {
            if (!logined)
            {
                Login();
            }

            SendCommand("DELE " + fileName);

            if (RetValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Rename a file on the remote FTP server.
        ///
        ///
        ///
        public void renameRemoteFile(string oldFileName, string
                                                             newFileName)
        {
            if (!logined)
            {
                Login();
            }

            SendCommand("RNFR " + oldFileName);

            if (RetValue != 350)
            {
                throw new IOException(reply.Substring(4));
            }

            //  known problem
            //  rnto will not take care of existing file.
            //  i.e. It will overwrite if newFileName exist
            SendCommand("RNTO " + newFileName);
            if (RetValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Create a directory on the remote FTP server.
        ///
        ///
        public void CreateDirectory(string dirName)
        {
            if (!logined)
            {
                Login();
            }

            SendCommand("MKD " + dirName);

            if (RetValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Delete a directory on the remote FTP server.
        ///
        ///
        public void DeleteDirectory(string dirName)
        {
            if (!logined)
            {
                Login();
            }

            SendCommand("RMD " + dirName);

            if (RetValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }
        }

        ///
        /// Change the current working directory on the remote FTP server.
        ///
        ///
        public void ChangeDirectory(string dirName)
        {
            if (string.IsNullOrEmpty(dirName) || dirName.Equals("."))
            {
                return;
            }

            if (!logined)
            {
                Login();
            }

            SendCommand("CWD " + dirName);

            if (RetValue != 250)
            {
                throw new IOException(reply.Substring(4));
            }

            RemotePath = dirName;

            if (Debug)
            {
                Logger.Write(new LogEntry(
                                "Current directory is " + RemotePath,
                                 "FTP.Information",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
            }
        }

        ///
        /// Close the FTP connection.
        ///
        public void Close()
        {
            if (clientSocket != null)
            {
                SendCommand("QUIT");
            }

            Cleanup();

            if (Debug)
            {
                Logger.Write(new LogEntry(
                                "Closing...",
                                 "FTP.Information",
                                 0,
                                 0,
                                 TraceEventType.Information,
                                 null,
                                 null));
            }
        }


        private void ReadReply()
        {
            mes = "";
            reply = ReadLine();
            RetValue = Int32.Parse(reply.Substring(0, 3));
        }

        private void Cleanup()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
            logined = false;
        }

        private string ReadLine()
        {
            while (true)
            {
                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
                mes += ASCII.GetString(buffer, 0, bytes);
                if (bytes < buffer.Length)
                {
                    break;
                }
            }

            char[] seperator = { '\n' };
            string[] mess = mes.Split(seperator);

            if (mes.Length > 2)
            {
                mes = mess[mess.Length - 2];
            }
            else
            {
                mes = mess[0];
            }

            if (!mes.Substring(3, 1).Equals(" "))
            {
                return ReadLine();
            }

            if (Debug)
            {
                for (int k = 0; k < mess.Length - 1; k++)
                {

                    if (Debug)
                    {
                        Logger.Write(new LogEntry(
                                        mess[k],
                                         "FTP.Information",
                                         0,
                                         0,
                                         TraceEventType.Information,
                                         null,
                                         null));
                    }
                }
            }
            return mes;
        }

        private void SendCommand(String command)
        {
#if DEBUG
            Logger.Write(new LogEntry(
                      string.Format("FTP Command: '{0}' executed.", command),
                      "FTP.Information",
                      0,
                      0,
                      TraceEventType.Information,
                      null,
                      null));


#endif
            for (int attempts = 1; attempts < retries + 1; attempts++)
            {
                try
                {
                    Byte[] cmdBytes =
                        Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
                    clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
                    ReadReply();
                    break;
                }
                catch (Exception)
                {
                    if (attempts == retries)
                    {
                        throw;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private Socket CreateDataSocket()
        {
            SendCommand("PASV");

            if (RetValue != 227)
            {
                throw new IOException(reply.Substring(4));
            }

            int index1 = reply.IndexOf('(');
            int index2 = reply.IndexOf(')');
            string ipData =
                reply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];


            int len = ipData.Length;
            int partCount = 0;
            string buf = "";

            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV reply: " +
                                          reply);
                }

                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV reply: " +
                                              reply);
                    }
                }
            }


            string ipAddress = parts[0] + "." + parts[1] + "." +
                               parts[2] + "." + parts[3];

            int port = (parts[4] << 8) + parts[5];

            Socket s = new
                Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new
                IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);

            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server ");
            }

            return s;
        }

        public bool FileExists(string fileName)
        {
            bool exists = false;
            try
            {
                FileSize(fileName);
                exists = true;
            }
            catch (IOException ex)
            {
                if (ex.Message.IndexOf("Size not available.") == -1)
                {
                    throw ex;
                }
            }
            return exists;
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace FTP
{

    #region "FTP client class"

    /// <summary>
    /// A wrapper class for .NET 2.0 FTP
    /// </summary>
    /// <remarks>
    /// This class does not hold open an FTP connection but
    /// instead is stateless: for each FTP request it
    /// connects, performs the request and disconnects.
    /// </remarks>
    public class FtpClient
    {
        #region "CONSTRUCTORS"

        /// <summary>
        /// Blank constructor
        /// </summary>
        /// <remarks>Hostname, username and password must be set manually</remarks>
        public FtpClient()
        {
        }

        /// <summary>
        /// Constructor just taking the hostname
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <remarks></remarks>
        public FtpClient(string Hostname)
        {
            _hostname = Hostname;
        }

        /// <summary>
        /// Constructor taking hostname, username and password
        /// </summary>
        /// <param name="Hostname">in either ftp://ftp.host.com or ftp.host.com form</param>
        /// <param name="Username">Leave blank to use 'anonymous' but set password to your email</param>
        /// <param name="Password"></param>
        /// <remarks></remarks>
        public FtpClient(string Hostname, string Username, string Password)
        {
            _hostname = Hostname;
            _username = Username;
            _password = Password;
        }

        #endregion

        #region "Directory functions"

        /// <summary>
        /// Return a simple directory listing
        /// </summary>
        /// <param name="directory">Directory to list, e.g. /pub</param>
        /// <returns>A list of filenames and directories as a List(of String)</returns>
        /// <remarks>For a detailed directory listing, use ListDirectoryDetail</remarks>
        public List<string> ListDirectory(string directory)
        {
            //return a simple list of filenames in directory
            FtpWebRequest ftp = GetRequest(GetDirectory(directory));
            //Set request to do simple list
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;

            string str = GetStringResponse(ftp);
            //replace CRLF to CR, remove last instance
            str = str.Replace("\r\n", "\r").TrimEnd('\r');
            //split the string into a list
            List<string> result = new List<string>();
            result.AddRange(str.Split('\r'));
            return result;
        }

        /// <summary>
        /// Return a detailed directory listing
        /// </summary>
        /// <param name="directory">Directory to list, e.g. /pub/etc</param>
        /// <returns>An FTPDirectory object</returns>
        public FTPDirectory ListDirectoryDetail(string directory)
        {
            FtpWebRequest ftp = GetRequest(GetDirectory(directory));
            //Set request to do simple list
            ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            string str = GetStringResponse(ftp);
            //replace CRLF to CR, remove last instance
            str = str.Replace("\r\n", "\r").TrimEnd('\r');
            //split the string into a list
            return new FTPDirectory(str, _lastDirectory);
        }

        #endregion

        #region "Upload: File transfer TO ftp server"

        /// <summary>
        /// Copy a local file to the FTP server
        /// </summary>
        /// <param name="localFilename">Full path of the local file</param>
        /// <param name="targetFilename">Target filename, if required</param>
        /// <returns></returns>
        /// <remarks>If the target filename is blank, the source filename is used
        /// (assumes current directory). Otherwise use a filename to specify a name
        /// or a full path and filename if required.</remarks>
        public bool Upload(string localFilename, string targetFilename)
        {
            //1. check source
            if (!File.Exists(localFilename))
            {
                throw (new ApplicationException("File " + localFilename + " not found"));
            }
            //copy to FI
            FileInfo fi = new FileInfo(localFilename);
            return Upload(fi, targetFilename);
        }

        /// <summary>
        /// Upload a local file to the FTP server
        /// </summary>
        /// <param name="fi">Source file</param>
        /// <param name="targetFilename">Target filename (optional)</param>
        /// <returns></returns>
        public bool Upload(FileInfo fi, string targetFilename)
        {
            //copy the file specified to target file: target file can be full path or just filename (uses current dir)

            //1. check target
            string target;
            if (targetFilename.Trim() == "")
            {
                //Blank target: use source filename & current dir
                target = CurrentDirectory + fi.Name;
            }
            else if (targetFilename.Contains("/"))
            {
                //If contains / treat as a full path
                target = AdjustDir(targetFilename);
            }
            else
            {
                //otherwise treat as filename only, use current directory
                target = CurrentDirectory + targetFilename;
            }

            string URI = Hostname + target;
            //perform copy
            FtpWebRequest ftp = GetRequest(URI);

            //Set request to Upload a file in binary
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;

            //Notify FTP of the expected size
            ftp.ContentLength = fi.Length;

            //create byte array to store: ensure at least 1 byte!
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //open file for reading
            using (FileStream fs = fi.OpenRead())
            {
                try
                {
                    //open request to send
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    //ensure file closed
                    fs.Close();
                }
            }


            ftp = null;
            return true;
        }

        #endregion

        #region "Download: File transfer FROM ftp server"

        /// <summary>
        /// Copy a file from FTP server to local
        /// </summary>
        /// <param name="sourceFilename">Target filename, if required</param>
        /// <param name="localFilename">Full path of the local file</param>
        /// <returns></returns>
        /// <remarks>Target can be blank (use same filename), or just a filename
        /// (assumes current directory) or a full path and filename</remarks>
        public bool Download(string sourceFilename, string localFilename, bool PermitOverwrite)
        {
            //2. determine target file
            FileInfo fi = new FileInfo(localFilename);
            return Download(sourceFilename, fi, PermitOverwrite);
        }

        //Version taking an FtpFileInfo
        public bool Download(FTPFileInfo file, string localFilename, bool PermitOverwrite)
        {
            return Download(file.FullName, localFilename, PermitOverwrite);
        }

        //Another version taking FtpFileInfo and FileInfo
        public bool Download(FTPFileInfo file, FileInfo localFI, bool PermitOverwrite)
        {
            return Download(file.FullName, localFI, PermitOverwrite);
        }

        //Version taking string/FileInfo
        public bool Download(string sourceFilename, FileInfo targetFI, bool PermitOverwrite)
        {
            //1. check target
            if (targetFI.Exists && !(PermitOverwrite))
            {
                throw (new ApplicationException("Target file already exists"));
            }

            //2. check source
            string target;
            if (sourceFilename.Trim() == "")
            {
                throw (new ApplicationException("File not specified"));
            }
            else if (sourceFilename.Contains("/"))
            {
                //treat as a full path
                target = AdjustDir(sourceFilename);
            }
            else
            {
                //treat as filename only, use current directory
                target = CurrentDirectory + sourceFilename;
            }

            string URI = Hostname + target;

            //3. perform copy
            FtpWebRequest ftp = GetRequest(URI);

            //Set request to Download a file in binary mode
            ftp.Method = WebRequestMethods.Ftp.DownloadFile;
            ftp.UseBinary = true;

            //open request and get response stream
            using (FtpWebResponse response = (FtpWebResponse) ftp.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    //loop to read & write to file
                    using (FileStream fs = targetFI.OpenWrite())
                    {
                        try
                        {
                            byte[] buffer = new byte[2048];
                            int read = 0;
                            do
                            {
                                read = responseStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);
                            } while (!(read == 0));
                            responseStream.Close();
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception)
                        {
                            //catch error and delete file only partially downloaded
                            fs.Close();
                            //delete target file as it's incomplete
                            targetFI.Delete();
                            throw;
                        }
                    }

                    responseStream.Close();
                }

                response.Close();
            }


            return true;
        }

        #endregion

        #region "Other functions: Delete rename etc."

        /// <summary>
        /// Delete remote file
        /// </summary>
        /// <param name="filename">filename or full path</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool FtpDelete(string filename)
        {
            //Determine if file or full path
            string URI = Hostname + GetFullPath(filename);

            FtpWebRequest ftp = GetRequest(URI);
          //  ftp.Proxy = null;
            //Set request to delete
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determine if file exists on remote FTP site
        /// </summary>
        /// <param name="filename">Filename (for current dir) or full path</param>
        /// <returns></returns>
        /// <remarks>Note this only works for files</remarks>
        public bool FtpFileExists(string filename)
        {
            //Try to obtain filesize: if we get error msg containing "550"
            //the file does not exist
            try
            {
                long size = GetFileSize(filename);
                return true;
            }
            catch (Exception ex)
            {
                //only handle expected not-found exception
                if (ex is WebException)
                {
                    //file does not exist/no rights error = 550
                    if (ex.Message.Contains("550"))
                    {
                        //clear
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Determine size of remote file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <remarks>Throws an exception if file does not exist</remarks>
        public long GetFileSize(string filename)
        {
            string path;
            if (filename.Contains("/"))
            {
                path = AdjustDir(filename);
            }
            else
            {
                path = CurrentDirectory + filename;
            }
            string URI = Hostname + path;
            FtpWebRequest ftp = GetRequest(URI);
            //Try to get info on file/dir?
            ftp.Method = WebRequestMethods.Ftp.GetFileSize;
            string tmp = GetStringResponse(ftp);
            return GetSize(ftp);
        }

        public bool FtpRename(string sourceFilename, string newName)
        {
            //Does file exist?
            string source = GetFullPath(sourceFilename);
            if (!FtpFileExists(source))
            {
                throw (new FileNotFoundException("File " + source + " not found"));
            }

            //build target name, ensure it does not exist
            string target = GetFullPath(newName);
            if (target == source)
            {
                throw (new ApplicationException("Source and target are the same"));
            }
            else if (FtpFileExists(target))
            {
                throw (new ApplicationException("Target file " + target + " already exists"));
            }

            //perform rename
            string URI = Hostname + source;

            FtpWebRequest ftp = GetRequest(URI);
            //Set request to delete
            ftp.Method = WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = target;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool FtpCreateDirectory(string dirpath)
        {
            //perform create
            string URI = Hostname + AdjustDir(dirpath);
            FtpWebRequest ftp = GetRequest(URI);
            //Set request to MkDir
            ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool FtpDeleteDirectory(string dirpath)
        {
            //perform remove
            string URI = Hostname + AdjustDir(dirpath);
            FtpWebRequest ftp = GetRequest(URI);
            //Set request to RmDir
            ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
            try
            {
                //get response but ignore it
                string str = GetStringResponse(ftp);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region "private supporting functions"

        //Get the basic FtpWebRequest object with the
        //common settings and security
        private FtpWebRequest GetRequest(string URI)
        {
            //create request
            FtpWebRequest result = (FtpWebRequest) FtpWebRequest.Create(URI);
            //Set the Login details
            result.Credentials = GetCredentials();
            
            //Do not keep alive (stateless mode)
            result.KeepAlive = false;
            return result;
        }


        /// <summary>
        /// Get the credentials from username/password
        /// </summary>
        private ICredentials GetCredentials()
        {
            return new NetworkCredential(Username, Password);
        }

        /// <summary>
        /// returns a full path using CurrentDirectory for a relative file reference
        /// </summary>
        private string GetFullPath(string file)
        {
            if (file.Contains("/"))
            {
                return AdjustDir(file);
            }
            else
            {
                return CurrentDirectory + file;
            }
        }

        /// <summary>
        /// Amend an FTP path so that it always starts with /
        /// </summary>
        /// <param name="path">Path to adjust</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string AdjustDir(string path)
        {
            return ((path.StartsWith("/")) ? "" : "/").ToString() + path;
        }

        private string GetDirectory(string directory)
        {
            string URI;
            if (directory == "")
            {
                //build from current
                URI = Hostname + CurrentDirectory;
                _lastDirectory = CurrentDirectory;
            }
            else
            {
                if (!directory.StartsWith("/"))
                {
                    throw (new ApplicationException("Directory should start with /"));
                }
                URI = Hostname + directory;
                _lastDirectory = directory;
            }
            return URI;
        }

        //stores last retrieved/set directory
        private string _lastDirectory = "";

        /// <summary>
        /// Obtains a response stream as a string
        /// </summary>
        /// <param name="ftp">current FTP request</param>
        /// <returns>String containing response</returns>
        /// <remarks>FTP servers typically return strings with CR and
        /// not CRLF. Use respons.Replace(vbCR, vbCRLF) to convert
        /// to an MSDOS string</remarks>
        private string GetStringResponse(FtpWebRequest ftp)
        {
            //Get the result, streaming to a string
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse) ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(datastream))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }

                    datastream.Close();
                }

                response.Close();
            }

            return result;
        }

        /// <summary>
        /// Gets the size of an FTP request
        /// </summary>
        /// <param name="ftp"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private long GetSize(FtpWebRequest ftp)
        {
            long size;
            using (FtpWebResponse response = (FtpWebResponse) ftp.GetResponse())
            {
                size = response.ContentLength;
                response.Close();
            }

            return size;
        }

        #endregion

        #region "Properties"

        private string _hostname;

        /// <summary>
        /// Hostname
        /// </summary>
        /// <value></value>
        /// <remarks>Hostname can be in either the full URL format
        /// ftp://ftp.myhost.com or just ftp.myhost.com
        /// </remarks>
        public string Hostname
        {
            get
            {
                if (_hostname.StartsWith("ftp://"))
                {
                    return _hostname;
                }
                else
                {
                    return "ftp://" + _hostname;
                }
            }
            set { _hostname = value; }
        }

        private string _username;

        /// <summary>
        /// Username property
        /// </summary>
        /// <value></value>
        /// <remarks>Can be left blank, in which case 'anonymous' is returned</remarks>
        public string Username
        {
            get { return (_username == "" ? "anonymous" : _username); }
            set { _username = value; }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// The CurrentDirectory value
        /// </summary>
        /// <remarks>Defaults to the root '/'</remarks>
        private string _currentDirectory = "/";

        public string CurrentDirectory
        {
            get
            {
                //return directory, ensure it ends with /
                return _currentDirectory + ((_currentDirectory.EndsWith("/")) ? "" : "/").ToString();
            }
            set
            {
                if (!value.StartsWith("/"))
                {
                    throw (new ApplicationException("Directory should start with /"));
                }
                _currentDirectory = value;
            }
        }

        #endregion
    }

    #endregion

    #region "FTP file info class"

    /// <summary>
    /// Represents a file or directory entry from an FTP listing
    /// </summary>
    /// <remarks>
    /// This class is used to parse the results from a detailed
    /// directory list from FTP. It supports most formats of
    /// </remarks>
    public class FTPFileInfo
    {
        //Stores extended info about FTP file

        #region "Properties"

        public string FullName
        {
            get { return Path + Filename; }
        }

        public string Filename
        {
            get { return _filename; }
        }

        public string Path
        {
            get { return _path; }
        }

        public DirectoryEntryTypes FileType
        {
            get { return _fileType; }
        }

        public long Size
        {
            get { return _size; }
        }

        public DateTime FileDateTime
        {
            get { return _fileDateTime; }
        }

        public string Permission
        {
            get { return _permission; }
        }

        public string Extension
        {
            get
            {
                int i = Filename.LastIndexOf(".");
                if (i >= 0 && i < (Filename.Length - 1))
                {
                    return Filename.Substring(i + 1);
                }
                else
                {
                    return "";
                }
            }
        }

        public string NameOnly
        {
            get
            {
                int i = Filename.LastIndexOf(".");
                if (i > 0)
                {
                    return Filename.Substring(0, i);
                }
                else
                {
                    return Filename;
                }
            }
        }

        private string _filename;
        private string _path;
        private DirectoryEntryTypes _fileType;
        private long _size;
        private DateTime _fileDateTime;
        private string _permission;

        #endregion

        /// <summary>
        /// Identifies entry as either File or Directory
        /// </summary>
        public enum DirectoryEntryTypes
        {
            File,
            Directory
        }

        /// <summary>
        /// Constructor taking a directory listing line and path
        /// </summary>
        /// <param name="line">The line returned from the detailed directory list</param>
        /// <param name="path">Path of the directory</param>
        /// <remarks></remarks>
        public FTPFileInfo(string line, string path)
        {
            //parse line
            Match m = GetMatchingRegex(line);
            if (m == null)
            {
                //failed
                //  throw (new ApplicationException("Unable to parse line: " + line));
            }
            else
            {
                _filename = m.Groups["name"].Value;
                _path = path;

                Int64.TryParse(m.Groups["size"].Value, out _size);
                //_size = System.Convert.ToInt32(m.Groups["size"].Value);

                _permission = m.Groups["permission"].Value;
                string _dir = m.Groups["dir"].Value;
                if (_dir != "" && _dir != "-")
                {
                    _fileType = DirectoryEntryTypes.Directory;
                }
                else
                {
                    _fileType = DirectoryEntryTypes.File;
                }

                try
                {
                    _fileDateTime = DateTime.Parse(m.Groups["timestamp"].Value);
                }

                catch (Exception)
                {
                    try
                    {
                        //vms
                        _fileDateTime =
                            DateTime.ParseExact(m.Groups["timestamp"].Value, "MM/dd/yy hh:mmtt",
                                                CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        _fileDateTime = Convert.ToDateTime(null);
                    }
                }
            }
        }

        private Match GetMatchingRegex(string line)
        {
            Regex rx;
            Match m;
            for (int i = 0; i <= _ParseFormats.Length - 1; i++)
            {
                rx = new Regex(_ParseFormats[i]);
                m = rx.Match(line);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }

        #region "Regular expressions for parsing LIST results"

        /// <summary>
        /// List of REGEX formats for different FTP server listing formats
        /// </summary>
        /// <remarks>
        /// The first three are various UNIX/LINUX formats, fourth is for MS FTP
        /// in detailed mode and the last for MS FTP in 'DOS' mode.
        /// I wish VB.NET had support for Const arrays like C# but there you go
        /// </remarks>
        private static string[] _ParseFormats = new string[]
            {
                "(?<timestamp>^.{16})\\s*((?<size>[0-9]+)|&lt;(?<dir>DIR{1})&gt;|<A)\\s*.*\\\">(?<name>[^<]+)",
                "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)"
                ,
                "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)"
                ,
                "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
                ,
                "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
                ,
                "(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<name>.+)"
                ,
                "(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>.+)"
            };

        #endregion
    }

    #endregion

    #region "FTP Directory class"

    /// <summary>
    /// Stores a list of files and directories from an FTP result
    /// </summary>
    /// <remarks></remarks>
    public class FTPDirectory : List<FTPFileInfo>
    {
        public FTPDirectory()
        {
            //creates a blank directory listing
        }

        /// <summary>
        /// Constructor: create list from a (detailed) directory string
        /// </summary>
        /// <param name="dir">directory listing string</param>
        /// <param name="path"></param>
        /// <remarks></remarks>
        public FTPDirectory(string dir, string path)
        {
            foreach (
                string line in dir.Replace("\n", "").Split(new char[] {'\r'}, StringSplitOptions.RemoveEmptyEntries))
            {
                //parse
                if (line != "")
                {
                    FTPFileInfo fileInfo = new FTPFileInfo(line, path);
                    if (!string.IsNullOrEmpty(fileInfo.Filename))
                    {
                        Add(fileInfo);
                    }
                }
            }
        }

        /// <summary>
        /// Filter out only files from directory listing
        /// </summary>
        /// <param name="ext">optional file extension filter</param>
        /// <returns>FTPDirectory listing</returns>
        public FTPDirectory GetFiles(string ext)
        {
            return GetFileOrDir(FTPFileInfo.DirectoryEntryTypes.File, ext);
        }

        /// <summary>
        /// Returns a list of only subdirectories
        /// </summary>
        /// <returns>FTPDirectory list</returns>
        /// <remarks></remarks>
        public FTPDirectory GetDirectories()
        {
            return GetFileOrDir(FTPFileInfo.DirectoryEntryTypes.Directory, "");
        }

        //internal: share use function for GetDirectories/Files
        private FTPDirectory GetFileOrDir(FTPFileInfo.DirectoryEntryTypes type, string ext)
        {
            FTPDirectory result = new FTPDirectory();
            foreach (FTPFileInfo fi in this)
            {
                if (fi.FileType == type)
                {
                    if (ext == "")
                    {
                        result.Add(fi);
                    }
                    else if (ext == fi.Extension)
                    {
                        result.Add(fi);
                    }
                }
            }
            return result;
        }

        public FTPFileInfo GetFile(string filename)
        {
            foreach (FTPFileInfo ftpfile in this)
            {
                if (ftpfile.Filename == filename)
                {
                    return ftpfile;
                }
            }
            return null;
        }

        public bool FileExists(string filename)
        {
            foreach (FTPFileInfo ftpfile in this)
            {
                if (ftpfile.Filename == filename)
                {
                    return true;
                }
            }
            return false;
        }

        private const char slash = '/';

        public static string GetParentDirectory(string dir)
        {
            string tmp = dir.TrimEnd(slash);
            int i = tmp.LastIndexOf(slash);
            if (i > 0)
            {
                return tmp.Substring(0, i - 1);
            }
            else
            {
                throw (new ApplicationException("No parent for root"));
            }
        }
    }

    #endregion
}*/