//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.IO;

namespace CompleX_Types
{
    public class FtpDirectoryInfo : FileSystemInfo
    {
        private DateTime? creationTime;
        private readonly FtpConnection ftp;
        private DateTime? lastAccessTime;
        private DateTime? lastWriteTime;

        public FtpDirectoryInfo(FtpConnection ftp, string path)
        {
            this.ftp = ftp;
            FullPath = path;
        }

        public FtpConnection FtpConnection
        {
            get { return ftp; }
        }

        public new DateTime? LastAccessTime
        {
            get { return lastAccessTime.HasValue ? (DateTime?) lastAccessTime.Value : null; }
            internal set { lastAccessTime = value; }
        }

        public new DateTime? CreationTime
        {
            get { return creationTime.HasValue ? (DateTime?) creationTime.Value : null; }
            internal set { creationTime = value; }
        }

        public new DateTime? LastWriteTime
        {
            get { return lastWriteTime.HasValue ? (DateTime?) lastWriteTime.Value : null; }
            internal set { lastWriteTime = value; }
        }

        public new DateTime? LastAccessTimeUtc
        {
            get { return lastAccessTime.HasValue ? (DateTime?) lastAccessTime.Value.ToUniversalTime() : null; }
        }

        public new DateTime? CreationTimeUtc
        {
            get { return creationTime.HasValue ? (DateTime?) creationTime.Value.ToUniversalTime() : null; }
        }

        public new DateTime? LastWriteTimeUtc
        {
            get { return lastWriteTime.HasValue ? (DateTime?) lastWriteTime.Value.ToUniversalTime() : null; }
        }

        public new FileAttributes Attributes { get; internal set; }

        public override bool Exists
        {
            get { return FtpConnection.DirectoryExists(FullName); }
        }

        public override string Name
        {
            get { return Path.GetFileName(FullPath); }
        }

        public override void Delete()
        {
            try
            {
                ftp.RemoveDirectory(Name,false);
            }
            catch (FtpException ex)
            {
                throw new Exception("Unable to delete directory.", ex);
            }
        }

        public FtpDirectoryInfo[] GetDirectories()
        {
            return FtpConnection.GetDirectories(FullPath);
        }

        public FtpDirectoryInfo[] GetDirectories(string path)
        {
            path = Path.Combine(FullPath, path);
            return FtpConnection.GetDirectories(path);
        }

        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(FtpConnection.GetCurrentDirectory());
        }

        public FtpFileInfo[] GetFiles(string mask)
        {
            return FtpConnection.GetFiles(mask);
        }
    }
}