////============================================================================================
//// Projekt:			CompleX Studio 
////
//// (C) Copyright Florian Gilde
//// http://www.nksoft.de
////
//// Alle Rechte vorbehalten. All rights reserved.
////============================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using CompleX_Library.Helper;
using CompleX_Types.Extensions;
using CompleX_Types.Interfaces;

namespace CompleX_Types
{
    /// <summary>
    /// The <c>FtpConnection</c> class provides the ability to connect to FTP servers.
    /// </summary>
    public class FtpConnection : IDisposable, IFtpConnection, ICloneable
    {

        private IntPtr hInternet;
        private IntPtr hConnect;
        private List<string> history;

        /// <summary>
        /// Output writer
        /// </summary>
        public TextWriter OutputWriter { get; set; }

        /// <summary>
        /// FTP Einstellungen, um eine FTP Verbindung aufzubauen
        /// </summary>
        /// <value></value>
        public IFtpSettings FtpSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect to.</param>
        public FtpConnection(string host)
        {
            FtpSettings = new FtpSettings(host) { Server = host, Port = WININET.INTERNET_DEFAULT_FTP_PORT };
            history = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        public FtpConnection(string host, int port)
        {
            FtpSettings = new FtpSettings(host) { Server = host,Port = port};
            history = new List<string>();

        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpConnection(string host, string username, string password)
        {
            FtpSettings = new FtpSettings(host) { Server = host, UserName = username, Password = password, Port = WININET.INTERNET_DEFAULT_FTP_PORT };
            history = new List<string>();

        }

        /// <summary>
        /// Initializes a new instance of the <c>FtpConnection</c> type.
        /// </summary>
        /// <param name="host">A <see cref="String"/> type representing the server name or IP to connect.</param>
        /// <param name="port">An <see cref="Int32"/> type representing the port on which to connect.</param>
        /// <param name="username">A <see cref="String"/> type representing the username with which to authenticate.</param>
        /// <param name="password">A <see cref="String"/> type representing the password with which to authenticate.</param>
        public FtpConnection(string host, int port, string username, string password)
        {
            FtpSettings = new FtpSettings(host) { Server = host, UserName = username, Password = password,Port = port};
            history = new List<string>();

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FtpConnection"/> class.
        /// </summary>
        public FtpConnection(IFtpSettings ftpSettings)
        {
            FtpSettings = ftpSettings;
            history = new List<string>();

        }

        /// <summary>
        /// The Port used to connect
        /// </summary>
        public int Port
        {
            get { return FtpSettings.Port; }
        }

        /// <summary>
        /// The host used to connect.
        /// </summary>
        public string Host
        {
            get { return FtpSettings.Server; }
        }


        /// <summary>
        /// Says if connection can connect with given settings
        /// </summary>
        public bool CanConnect
        {
            get
            {
                if (String.IsNullOrEmpty(FtpSettings.Server))
                    return false;
                try
                {
                    Open();
                    Login();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }




        /// <summary>
        /// Establishes a connection to the host.
        /// </summary>
        /// <exception cref="ArgumentNullException">If Host is null or empty.</exception>
        public void Open()
        {
            if (String.IsNullOrEmpty(FtpSettings.Server)) throw new Exception(@"Host is not set");
            Log(String.Format("Open Connection to {0}", FtpSettings.Server));
            hInternet = WININET.InternetOpen(
                Environment.UserName,
                WININET.INTERNET_OPEN_TYPE_PRECONFIG,
                null,
                null,
                WININET.INTERNET_FLAG_SYNC);

            if (hInternet == IntPtr.Zero)
                Error();
            
        }

        /// <summary>
        /// Logs into the host server using the credentials provided when the class was instantiated.
        /// </summary>
        public void Login()
        {
            Login(FtpSettings.UserName, FtpSettings.Password);
        }

        /// <summary>
        /// Logs into the host server using the provided credentials.
        /// </summary>
        /// <exception cref="ArgumentNullException">If <paramref name="username"/> or <paramref name="password"/> are null.</exception>
        /// <param name="username">A <see cref="String" /> type representing the user name with which to authenticate.</param>
        /// <param name="password">A <see cref="String" /> type representing the password with which to authenticate.</param>
        public void Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");

            Log(String.Format("Login to {0} with {1}", FtpSettings.Server, username));

            hConnect = WININET.InternetConnect(hInternet,
                                               FtpSettings.Server,
                                               FtpSettings.Port,
                                               username,
                                               password,
                                               WININET.INTERNET_SERVICE_FTP,
                                               WININET.INTERNET_FLAG_PASSIVE,
                                               IntPtr.Zero);

            if (hConnect == IntPtr.Zero)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the current FTP working directory to the specified path.
        /// </summary>
        /// <exception cref="FtpException">If the directory does not exist on the FTP server.</exception>
        /// <param name="directory">A <see cref="String"/> representing the file path of the directory.</param>
        public void SetCurrentDirectory(string directory)
        {
            int ret = WININET.FtpSetCurrentDirectory(
                hConnect,
                directory);
            Log(String.Format("Set directory to {0}", directory));
            if (ret == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Changes the local working directory to the specified path.
        /// </summary>
        /// <exception cref="InvalidDataException">If the directory does not exist on the local system.</exception>
        /// <param name="directory"></param>
        public void SetLocalDirectory(string directory)
        {
            if (Directory.Exists(directory))
                Environment.CurrentDirectory = directory;
            else
                throw new InvalidDataException(String.Format("{0} is not a directory!", directory));
        }

        /// <summary>
        /// Gets the current working FTP directory
        /// </summary>
        /// <returns>A <see cref="String"> representing the current working directory.</see></returns>
        public string GetCurrentDirectory()
        {
            int buffLength = WINAPI.MAX_PATH + 1;
            var str = new StringBuilder(buffLength);
            int ret = WININET.FtpGetCurrentDirectory(hConnect, str, ref buffLength);
            Log(String.Format("Search directory..."));
            if (ret == 0)
            {
                Error();
                return null;
            }
            Log(String.Format("Directory is {0}", str));
            return str.ToString();
        }

        /// <summary>
        /// Get the current FtpDirectory information for the current working directory
        /// </summary>
        /// <returns>A <see cref="FtpDirectoryInfo"/> with available details about the current working directory.</returns>
        public FtpDirectoryInfo GetCurrentDirectoryInfo()
        {
            string dir = GetCurrentDirectory();
            return new FtpDirectoryInfo(this, dir);
        }

        /// <summary>
        /// Download Directory
        /// </summary>
        /// <param name="localDirectory"></param>
        /// <param name="ftpDirectory"></param>
        public void DownloadDirectory(string localDirectory, string ftpDirectory)
        {
            Log(String.Format("Start downloading Directory \"{0}\" to \"{1}\" ", ftpDirectory, localDirectory));
            string dir = localDirectory;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            SetCurrentDirectory(ftpDirectory);
            var files = GetFiles();
            foreach (FtpFileInfo info in files)
            {
                GetFile(ftpDirectory + "/" + info.Name, dir.AddDirectorySeparatorChar() + info.Name, false);
            }
            var dirs = GetDirectories();
            foreach (FtpDirectoryInfo ftpDir in dirs)
            {
                DownloadDirectory(localDirectory.AddDirectorySeparatorChar() + ftpDir.Name, ftpDirectory + "/" + ftpDir.Name);
            }
        }

        public void DownloadDirectoryAsync(string localDirectory, string ftpDirectory, Action<bool> finished)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                DownloadDirectory(localDirectory, ftpDirectory);
                finished(true);
            });
        }

        public void DownloadFile(string remoteFile, string localFile, bool failIfExists)
        {
            GetFile(remoteFile, localFile, failIfExists);
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <remarks>The file will be downloaded to the local working directory with the same name it has on the FTP server.</remarks>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, bool failIfExists)
        {
            GetFile(remoteFile, remoteFile, failIfExists);
        }

        /// <summary>
        /// Downloads a file from the FTP server to the local system
        /// </summary>
        /// <exception cref="FtpException">If the file does not exist.</exception>
        /// <param name="remoteFile">A <see cref="String"/> representing the full or relative path to the file to download.</param>
        /// <param name="localFile">A <see cref="String"/> representing the local file path to save the file.</param>
        /// <param name="failIfExists">A <see cref="Boolean"/> that determines whether an existing local file should be overwritten.</param>
        public void GetFile(string remoteFile, string localFile, bool failIfExists)
        {
            Log(String.Format("Download File \"{0}\" as \"{1}\" ", remoteFile, localFile));
            int ret = WININET.FtpGetFile(hConnect,
                 remoteFile,
                 localFile,
                 failIfExists,
                 WINAPI.FILE_ATTRIBUTE_NORMAL,
                 WININET.FTP_TRANSFER_TYPE_BINARY,
                 IntPtr.Zero);

            if (ret == 0)
            {
                Log(String.Format("Error on Downloading File {0} ", remoteFile));
                Error();
            }
            Log(String.Format("Finished Downloading File {0} ", remoteFile ));
        }

        /// <summary>
        /// Uploads a directory asyncron 
        /// </summary>
        public void UploadDirectoryAsync(string localDirectory, string ftpDirectory, Action<bool> finished, Action<string> fileChanged)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    UploadDirectory(localDirectory, ftpDirectory, fileChanged);
                }
                catch (Exception)
                {
                    if(finished != null) finished(false);
                    throw;
                }
                if (finished != null) finished(true);
            });
        }

        /// <summary>
        /// Upload Directory
        /// </summary>
        public void UploadDirectory(string localDirectory, string ftpDirectory)
        {
            UploadDirectory(localDirectory, ftpDirectory, null);
        }

        /// <summary>
        /// Upload Directory
        /// </summary>
        public void UploadDirectory(string localDirectory, string ftpDirectory, Action<string> fileChanged)
        {
            Log(String.Format("Uploading Directory \"{0}\" to \"{1}\" ", localDirectory, ftpDirectory));
            if (!Directory.Exists(localDirectory))
                throw new DirectoryNotFoundException();

            try
            {
                CreateDirectory(ftpDirectory);
                SetCurrentDirectory(ftpDirectory);
            }
            catch (Exception)
            {}

            if (!ftpDirectory.EndsWith("/"))
                ftpDirectory += "/";
  
            var files = Directory.GetFiles(localDirectory);
            foreach (string file in files)
            {
                if (fileChanged != null)
                    fileChanged(Path.GetFileName(file));
                PutFile(file, ftpDirectory + Path.GetFileName(file));
            }
            var dirs = Directory.GetDirectories(localDirectory);
            foreach (string dir in dirs)
            {
                UploadDirectory(dir, ftpDirectory + Path.GetFileName(dir),fileChanged);
            }
            Log(String.Format("Finished Uploading Directory \"{0}\"", ftpDirectory));
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        public void UploadFile(string localFile, string remoteFile)
        {
            PutFile(localFile,remoteFile);
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"> representing the local file path to upload.</see></param>
        public void UploadFile(string fileName)
        {
            PutFile(fileName, Path.GetFileName(fileName));
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"> representing the local file path to upload.</see></param>
        public void PutFile(string fileName)
        {
            PutFile(fileName, Path.GetFileName(fileName));
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        public void PutFile(string localFile, string remoteFile)
        {
            Log(String.Format("Uploading File \"{0}\" to \"{1}\" ", localFile, remoteFile));
            int ret = WININET.FtpPutFile(hConnect,
                localFile,
                remoteFile,
                WININET.FTP_TRANSFER_TYPE_BINARY,
                IntPtr.Zero);

            if (ret == 0)
            {
                Log(String.Format("Error while Uploading File \"{0}\" ", localFile));
                Error();
            }
            Log(String.Format("Finished Uploading File \"{0}\" ", localFile));
        }

        /// <summary>
        /// Renames a file on the FTP server
        /// </summary>
        /// <param name="existingFile">A <see cref="String"/> representing the current file name</param>
        /// <param name="newFile">A <see cref="String"/> representing the new file name</param>
        public void RenameFile(string existingFile, string newFile)
        {
            int ret = WININET.FtpRenameFile(hConnect, existingFile, newFile);
            Log(String.Format("Rename File \"{0}\" to \"{1}\" ", existingFile, newFile));
            if (ret == 0)
                Error();
        }

        /// <summary>
        /// Deletes a file from the FTP server
        /// </summary>
        /// <param name="fileName">A <see cref="String"/> representing the path of the file to delete.</param>
        public void RemoveFile(string fileName)
        {
            int ret = WININET.FtpDeleteFile(hConnect, fileName);
            Log(String.Format("Delete File \"{0}\" ", fileName));
            if (ret == 0)
            {
                Log(String.Format("Error Failed to delete File \"{0}\" ", fileName));
                Error();
            }
            Log(String.Format("File \"{0}\" deleted ", fileName));
        }

        /// <summary>
        /// Deletes a directory from the FTP server
        /// </summary>
        /// <param name="directory">A <see cref="String"/> representing the path of the directory to delete.</param>
        /// <param name="recursive"></param>
        public void RemoveDirectory(string directory, bool recursive)
        {
            Log(String.Format("Deleting Directory \"{0}\" ", directory));
            if(recursive)
            {
                SetCurrentDirectory(directory);
                foreach (FtpDirectoryInfo dir in GetDirectories())
                    RemoveDirectory(directory+@"/"+dir.Name, true);
                SetCurrentDirectory(directory);
                foreach (FtpFileInfo file in GetFiles())
                    RemoveFile(directory + @"/" + file.Name);
            }
            int ret = WININET.FtpRemoveDirectory(hConnect, directory);
            if (ret == 0)
                Error();
            Log(String.Format("Directory \"{0}\" deleted ", directory));
        }

        /// <summary>
        /// List all files and directories in the current working directory.
        /// </summary>
        /// <returns>A list of file and directory names.</returns>
        [Obsolete("Use GetFiles or GetDirectories instead.")]
        public List<string> List()
        {
            return List(null, false);
        }
        /// <summary>
        /// Provides backwards compatibility
        /// </summary>
        /// <param name="mask">The file mask used in the search.</param>
        /// <returns>A list of file matching the mask.</returns>
        [Obsolete("Use GetFiles or GetDirectories instead.")]
        public List<string> List(string mask)
        {
            return List(mask, false);
        }

        [Obsolete("Will be removed in later releases.")]
        private List<string> List(string mask, bool onlyDirectories)
        {
            Log("Receiving files");
            var findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                hConnect,
                mask,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                var files = new List<string>();
                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return files;
                    }
                    else
                    {
                        Error();
                        return files;
                    }
                }

                if (onlyDirectories && (findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    files.Add(new string(findData.fileName).TrimEnd('\0'));
                }
                else if (!onlyDirectories)
                {
                    files.Add(new string(findData.fileName).TrimEnd('\0'));
                }

                findData = new WINAPI.WIN32_FIND_DATA();
                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if (onlyDirectories && (findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        files.Add(new string(findData.fileName).TrimEnd('\0'));
                    }
                    else if (!onlyDirectories)
                    {
                        files.Add(new string(findData.fileName).TrimEnd('\0'));
                    }
                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return files;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory.
        /// </summary>
        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(GetCurrentDirectory());
        }

        /// <summary>
        /// Gets details of all files and their available FTP file information from the current working FTP directory that match the file mask.
        /// </summary>
        /// <param name="mask">A <see cref="String"/> representing the file mask to match files.</param>
        public FtpFileInfo[] GetFiles(string mask)
        {
            var findData = new WINAPI.WIN32_FIND_DATA();
            Log(String.Format("Receiving files..."));
            IntPtr hFindFile = WININET.FtpFindFirstFile(
                hConnect,
                mask,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                var files = new List<FtpFileInfo>();
                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return files.ToArray();
                    }
                    else
                    {
                        Error();
                        return files.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    var file = new FtpFileInfo(this, new string(findData.fileName).TrimEnd('\0'))
                                   {
                                       LastAccessTime = findData.ftLastAccessTime.ToDateTime(),
                                       LastWriteTime = findData.ftLastWriteTime.ToDateTime(),
                                       CreationTime = findData.ftCreationTime.ToDateTime(),
                                       Attributes = (FileAttributes) findData.dfFileAttributes
                                   };
                    Log(file.Name);
                    files.Add(file);
                }

                findData = new WINAPI.WIN32_FIND_DATA();
                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) != WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        var file = new FtpFileInfo(this, new string(findData.fileName).TrimEnd('\0'))
                                       {
                                           LastAccessTime = findData.ftLastAccessTime.ToDateTime(),
                                           LastWriteTime = findData.ftLastWriteTime.ToDateTime(),
                                           CreationTime = findData.ftCreationTime.ToDateTime(),
                                           Attributes = (FileAttributes) findData.dfFileAttributes
                                       };
                        Log(file.Name);
                        files.Add(file);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return files.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory.
        /// </summary>
        public FtpDirectoryInfo[] GetDirectories()
        {
            return GetDirectories(GetCurrentDirectory());
        }

        /// <summary>
        /// Gets details of all directories and their available FTP directory information from the current working FTP directory that match the directory mask.
        /// </summary>
        public FtpDirectoryInfo[] GetDirectories(string path)
        {
            var findData = new WINAPI.WIN32_FIND_DATA();
            Log(String.Format("Receiving Directories in \"{0}\" ...", path));
            IntPtr hFindFile = WININET.FtpFindFirstFile(hConnect,path,ref findData,WININET.INTERNET_FLAG_NO_CACHE_WRITE,IntPtr.Zero);
            try
            {
                var directories = new List<FtpDirectoryInfo>();

                if (hFindFile == IntPtr.Zero)
                {
                    if (Marshal.GetLastWin32Error() == WINAPI.ERROR_NO_MORE_FILES)
                    {
                        return directories.ToArray();
                    }
                    else
                    {
                        Error();
                        return directories.ToArray();
                    }
                }

                if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                {
                    var dir = new FtpDirectoryInfo(this, new string(findData.fileName).TrimEnd('\0'))
                                  {
                                      LastAccessTime = findData.ftLastAccessTime.ToDateTime(),
                                      LastWriteTime = findData.ftLastWriteTime.ToDateTime(),
                                      CreationTime = findData.ftCreationTime.ToDateTime(),
                                      Attributes = (FileAttributes) findData.dfFileAttributes
                                  };
                    Log(dir.Name);
                    directories.Add(dir);
                }

                findData = new WINAPI.WIN32_FIND_DATA();

                while (WININET.InternetFindNextFile(hFindFile, ref findData) != 0)
                {
                    if ((findData.dfFileAttributes & WINAPI.FILE_ATTRIBUTE_DIRECTORY) == WINAPI.FILE_ATTRIBUTE_DIRECTORY)
                    {
                        var dir = new FtpDirectoryInfo(this, new string(findData.fileName).TrimEnd('\0'))
                                      {
                                          LastAccessTime = findData.ftLastAccessTime.ToDateTime(),
                                          LastWriteTime = findData.ftLastWriteTime.ToDateTime(),
                                          CreationTime = findData.ftCreationTime.ToDateTime(),
                                          Attributes = (FileAttributes) findData.dfFileAttributes
                                      };
                        Log(dir.Name);
                        directories.Add(dir);
                    }

                    findData = new WINAPI.WIN32_FIND_DATA();
                }

                if (Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                    Error();

                return directories.ToArray();
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Creates a directory on the FTP server.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the full or relative path of the directory to create.</param>
        public void CreateDirectory(string path)
        {
            Log(String.Format("Create new Directory \"{0}\" ", path));
            if (WININET.FtpCreateDirectory(hConnect, path) == 0)
            {
                Error();
            }
        }

        /// <summary>
        /// Checks if a directory exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the directory exists.</returns>
        public bool DirectoryExists(string path)
        {
            var findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero && Marshal.GetLastWin32Error() != WINAPI.ERROR_NO_MORE_FILES)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }

        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="path">A <see cref="String"/> representing the path to check.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether the file exists.</returns>
        public bool FileExists(string path)
        {
            var findData = new WINAPI.WIN32_FIND_DATA();

            IntPtr hFindFile = WININET.FtpFindFirstFile(
                hConnect,
                path,
                ref findData,
                WININET.INTERNET_FLAG_NO_CACHE_WRITE,
                IntPtr.Zero);
            try
            {
                if (hFindFile == IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }
            finally
            {
                if (hFindFile != IntPtr.Zero)
                    WININET.InternetCloseHandle(hFindFile);
            }
        }

        /// <summary>
        /// Sends a command to the FTP server
        /// </summary>
        /// <param name="cmd">A <see cref="String"/> representing the command to send.</param>
        /// <returns>A <see cref="String"/> containing the server response.</returns>
        public string SendCommand(string cmd)
        {
            Log(String.Format("Send Command \"{0}\" ", cmd));
            int result;
            var dataSocket = new IntPtr();
            switch (cmd)
            {
                case "PASV":
                    result = WININET.FtpCommand(hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
                default:
                    result = WININET.FtpCommand(hConnect, false, WININET.FTP_TRANSFER_TYPE_ASCII, cmd, IntPtr.Zero, ref dataSocket);
                    break;
            }

            const int bufferSize = 8192;

            if (result == 0)
            {
                Error();
            }
            else if (dataSocket != IntPtr.Zero)
            {
                var buffer = new StringBuilder(bufferSize);
                int bytesRead = 0;

                do
                {
                    result = WININET.InternetReadFile(dataSocket, buffer, bufferSize, ref bytesRead);
                } while (result == 1 && bytesRead > 1);

                return buffer.ToString();

            }

            return "";
        }

        /// <summary>
        /// Closes the current FTP connection
        /// </summary>
        public void Close()
        {
            Log(String.Format("Closing connection \"{0}\" ", FtpSettings.Server));
            WININET.InternetCloseHandle(hConnect);
            hConnect = IntPtr.Zero;

            WININET.InternetCloseHandle(hInternet);
            hInternet = IntPtr.Zero;
        }

        /// <summary>
        /// Retrieves error message text
        /// </summary>
        /// <param name="code">A <see cref="Int32"/> representing the system error code.</param>
        /// <returns>A <see cref="String"/> containing the error text.</returns>
        private static string InternetLastResponseInfo(ref int code)
        {
            int bufferSize = 8192;

            var buff = new StringBuilder(bufferSize);
            WININET.InternetGetLastResponseInfo(ref code, buff, ref bufferSize);
            return buff.ToString();
        }

        /// <summary>
        /// Retrieves error text and throws an Exception.
        /// </summary>
        private static void Error()
        {
            int code = Marshal.GetLastWin32Error();

            if (code == WININET.ERROR_INTERNET_EXTENDED_ERROR)
            {
                string errorText = InternetLastResponseInfo(ref code);
                throw new FtpException(code, errorText);
            }
            throw new Win32Exception(code);
        }

        private void Log(string s)
        {
            if(OutputWriter != null)
                OutputWriter.WriteLine(s);
            history.Add(s);
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (hConnect != IntPtr.Zero)
                WININET.InternetCloseHandle(hConnect);

            if (hInternet != IntPtr.Zero)
                WININET.InternetCloseHandle(hInternet);
        }

        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }

        public T Clone<T>() where T : class
        {
            return Clone() as T;
        }

    }
}


public class FtpException : Exception
{
    public FtpException(int error, string message)
        : base(message)
    {
        _error = error;
    }

    private int _error;

    public int ErrorCode
    {
        get { return _error; }
    }
}
