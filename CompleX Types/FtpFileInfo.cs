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
    public class FtpFileInfo : FileSystemInfo
    {
        private DateTime? creationTime;
        private readonly FtpConnection ftp;

        private DateTime? lastAccessTime;
        private DateTime? lastWriteTime;
        private readonly string name;

        public FtpFileInfo(FtpConnection ftp, string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            OriginalPath = filePath;
            FullPath = filePath;

            this.ftp = ftp;

            name = Path.GetFileName(filePath);
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

        public override string Name
        {
            get { return name; }
        }

        public override bool Exists
        {
            get { return FtpConnection.FileExists(FullName); }
        }

        public override void Delete()
        {
            FtpConnection.RemoveFile(FullName);
        }
    }
}