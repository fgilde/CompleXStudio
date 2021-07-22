//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CompleX_Library.Helper
{
    public static class FileHelper
    {


        /// <summary>
        /// Determines whether [is text file] [the specified file name].
        /// </summary>
        public static bool IsTextFile(string fileName)
        {
            byte[] file;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                file = new byte[stream.Length];
                stream.Read(file, 0, file.Length);
            }

            if (file.Length > 3 && ((file[0] == 0x00 && file[1] == 0x00 && file[2] == 0xFE && file[2] == 0xFF /*UCS-4*/)))
                return true;
            if (file.Length > 2 && ((file[0] == 0xEF && file[1] == 0xBB && file[2] == 0xBF /*UTF-8*/)))
                return true;
            if (file.Length > 1 && ((file[0] == 0xFF && file[1] == 0xFE /*Unicode*/)))
                return true;
            if (file.Length > 1 && (file[0] == 0xFE && file[1] == 0xFF /*Unicode Big Endian*/))
                return true;
            return file.All(t => t <= 0x80);
        }

        /// <summary>
        /// returns Mimetype by given extension
        /// </summary>
        /// <param name="sExtension">The s extension.</param>
        /// <returns></returns>
        public static string GetMimeType(string sExtension)
        {
            string extension = sExtension.ToLower();
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("MIME\\Database\\Content Type");
            if (key != null)
                foreach (string keyName in key.GetSubKeyNames())
                {
                    RegistryKey temp = key.OpenSubKey(keyName);
                    if (temp != null)
                        if (extension.Equals(temp.GetValue("Extension")))
                            return keyName;
                }
            return String.Empty;
        }

        /// <summary>
        /// Gets the file description by extension.
        /// </summary>
        /// <param name="sExtension">The s extension.</param>
        /// <param name="extensionFileName"></param>
        /// <returns></returns>
        public static string GetFileDescriptionByExtension(string sExtension,out string extensionFileName)
        { 
            if(String.IsNullOrEmpty(sExtension))
            {
                extensionFileName = String.Empty;
                return String.Empty;
            }
            string extension = sExtension.ToLower();
            extensionFileName = extension.Substring(1) + "file";
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension);
            if (key != null)
            {
                object result = key.GetValue(String.Empty);
                if (result != null)
                {                    
                    string nextKey = result.ToString();
                    extensionFileName = nextKey;
                    result = null;
                    key = Registry.ClassesRoot.OpenSubKey(nextKey);
                    if (key != null) result = key.GetValue(String.Empty);
                    if (result != null)
                    {
                        if (!String.IsNullOrEmpty(result.ToString()))
                            return result.ToString();
                    }
                }
            }

            return sExtension.Substring(1).FirstCharToUpper() + "-File";
        }

        /// <summary>
        /// Fasts the file copy.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public static void FastFileCopy(string file, string destination, bool overwrite)
        {
            var copy = new Thread(() => File.Copy(file, destination, overwrite));
            copy.Start();
        }

        /// <summary>
        /// Copies a directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        public static void CopyDirectory(string source, string destination, bool overWrite)
        {
            if (!source.EndsWith(Path.DirectorySeparatorChar.ToString()))
                source += Path.DirectorySeparatorChar;

            if (!destination.EndsWith(Path.DirectorySeparatorChar.ToString()))
                destination += Path.DirectorySeparatorChar;

            destination += Path.GetFileName(source);
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            var files = Directory.GetFiles(source);
            foreach (string file in files)
            {
                if (File.Exists(file))
                    FastFileCopy(file, destination + Path.GetFileName(file), overWrite);
            }

            var directories = Directory.GetDirectories(source);
            foreach (var dir in directories)
            {
                CopyDirectory(dir, destination + "\\" + Path.GetFileName(dir), overWrite);
            }

        }

        /// <summary>
        /// Adds the directory separator char.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string RemoveDirectorySeparatorChar(this string s)
        {
            if (s.EndsWith(Path.DirectorySeparatorChar.ToString()))
                s = s.Substring(0,s.Length-1);
            return s;
        }

        /// <summary>
        /// Adds the directory separator char.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string AddDirectorySeparatorChar(this string s)
        {
            if (!s.EndsWith(Path.DirectorySeparatorChar.ToString()))
                s += Path.DirectorySeparatorChar;
            return s;
        }

        /// <summary>
        /// Fügt alle Ordner+Unterordner zu den nodes hinzu
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="path">The path.</param>
        public static void InsertDirectories(this TreeNodeCollection nodes, string path)
        {
            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                var info = new DirectoryInfo(directory);
                if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    var node = new TreeNode(Path.GetFileName(directory));
                    nodes.Add(node);
                    InsertDirectories(node.Nodes, directory);
                }
            }
        }

        /// <summary>
        /// Sucht eine Datei mit dem fileName in dem ordner + unterordner 
        /// </summary>
        public static void FindFile(string dir, string fileName, Action<string >fileFound, bool recursiv)
        {
            fileName = Path.GetFileName(fileName);
            if(fileName.StartsWith("*."))
            {
                FindFilesWithExtionsion(dir, fileName, fileFound, recursiv);
                return;
            }
            ThreadPool.QueueUserWorkItem(state =>
            {
                dir = dir.AddDirectorySeparatorChar();
                var files = Directory.GetFiles(dir);
                string result = files.FirstOrDefault(s => Path.GetFileName(s).ToLower() == fileName.ToLower());
                if (String.IsNullOrEmpty(result) && recursiv)
                {
                    var directories = Directory.GetDirectories(dir);
                    foreach (string directory in directories)
                    {
                        FindFile(directory, fileName, fileFound, recursiv);
                    }
                }
                else
                {
                    fileFound(result);
                }
            });
        }

        /// <summary>
        /// Sucht eine Datei mit dem fileName in dem ordner + unterordner 
        /// </summary>
        public static void FindFilesWithExtionsion(string dir, string ext, Action<string> fileFound, bool recursiv)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                dir = dir.AddDirectorySeparatorChar();
                var files = Directory.GetFiles(dir,ext);
                foreach (string file in files)
                {
                    string s = file;
                    ThreadPool.QueueUserWorkItem(o => fileFound(s));
                }

                if (recursiv)
                {
                    var directories = Directory.GetDirectories(dir);
                    foreach (string directory in directories)
                    {
                        FindFilesWithExtionsion(directory, ext, fileFound,recursiv);
                    }
                }
                
            });
        }



        /// <summary>
        /// Gibt alle Datein aus dem übergebenem Ordner+alle unterodner zurück
        /// </summary>
        /// <param name="dir">The dir.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="exculdedExtensions">The exculded extensions.</param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string dir, string searchPattern, params string[] exculdedExtensions)
        {
            List<string> result = !String.IsNullOrEmpty(searchPattern) ? Directory.GetFiles(dir, searchPattern).ToList() : Directory.GetFiles(dir).ToList();
            var directories = Directory.GetDirectories(dir);
            foreach (var directory in directories)
            {
                var info = new DirectoryInfo(directory);
                if ((info.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    result.AddRange(GetAllFiles(directory, searchPattern, exculdedExtensions));
            }
            if(exculdedExtensions != null && exculdedExtensions.Count() > 0)
                result.RemoveAll(s => exculdedExtensions.Contains(Path.GetExtension(s)));          
            return result;
        }

        /// <summary>
        /// Gibt alle Datein aus dem übergebenem Ordner+alle unterodner zurück
        /// </summary>
        /// <param name="dir">The dir.</param>
        /// <param name="exculdedExtensions">The exculded extensions.</param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string dir, params string[] exculdedExtensions)
        {
            return GetAllFiles(dir, String.Empty, exculdedExtensions);
        }

        public static void AddFileItems(this ListView view, string path, Func<string, bool> condition, params string[] exculdedExtensions)
        {
            if (Directory.Exists(path))
            {
                var images = new ImageList { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(32, 32) };
                var imagesSmall = new ImageList { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };

                var files = GetAllFiles(path, string.Empty, exculdedExtensions);
                view.BeginUpdate();
                view.Clear();
                view.SmallImageList = imagesSmall;
                view.LargeImageList = images;
                foreach (string file in files)
                {
                    if (condition(file))
                    {
                        int imageIndex = -1;
                        Icon ico = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(file), true);
                        Icon icoSmall = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(file), false);
                        if (ico != null && icoSmall != null)
                        {
                            images.Images.Add(ico);
                            imagesSmall.Images.Add(icoSmall);
                            imageIndex = images.Images.Count - 1;
                        }
                        view.Items.Add(file, Path.GetFileName(file), imageIndex);
                    }
                }

                view.EndUpdate();
            }

        }

        /// <summary>
        /// Fügt alle Datein aus path+alle Unterordner der ListView hinzu
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="path">The path.</param>
        /// <param name="exculdedExtensions"></param>
        public static void AddFileItems(this ListView view, string path, params string[] exculdedExtensions)
        {
            if (Directory.Exists(path))
            {
                var images = new ImageList {ColorDepth = ColorDepth.Depth32Bit,ImageSize = new Size(32,32)};
                var imagesSmall = new ImageList {ColorDepth = ColorDepth.Depth32Bit,ImageSize = new Size(16,16)};

                var files = GetAllFiles(path, string.Empty, exculdedExtensions);
                view.BeginUpdate();
                view.Clear();
                view.SmallImageList = imagesSmall;
                view.LargeImageList = images;
                foreach (string file in files)
                {
                    int imageIndex = -1;
                    Icon ico = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(file),true);
                    Icon icoSmall = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(file), false);
                    if (ico != null && icoSmall != null)
                    {
                        images.Images.Add(ico);
                        imagesSmall.Images.Add(icoSmall);
                        imageIndex = images.Images.Count - 1;
                    }
                    view.Items.Add(file,Path.GetFileName(file),imageIndex);
                }

                view.EndUpdate();
            }

        }

    }
}