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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CompleX_Library.Helper
{
    public static class Extensions
    {


        public static string AsString(this TreeNode node)
        {
            bool cancelled = false;
            string result = String.Empty;
            while (!cancelled)
            {
                if (node != null)
                {
                    result = node.Text.AddDirectorySeparatorChar() + result;
                    if (node.Parent != null)
                        node = node.Parent;
                    else
                        cancelled = true;
                }
                else
                    cancelled = true;
            }
            return result;
        }

        public static bool ToFile(this string s, string filename)
        {
            try
            {
                var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                var sw = new StreamWriter(stream);
                sw.Write(s);
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static MemoryStream ToMemoryStream(this FileStream inStream)
        {
            var memoryStream = new MemoryStream();

            memoryStream.SetLength(inStream.Length);
            inStream.Read(memoryStream.GetBuffer(), 0, (int)inStream.Length);
            return memoryStream;
        }

        public static MemoryStream ToStream(this string s)
        {
            // Convert String to Stream
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            return new MemoryStream(byteArray);
        }

        public static String AsString(this Stream stream)
        {
            // convert stream to string
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static bool ExtensionIsAllowed(IEnumerable<string> supportedFileExtensions,string fileExtension)
        {
            if(String.IsNullOrEmpty(fileExtension))
            {
                return true;
            }
            if (!fileExtension.StartsWith("."))
            {
                if (fileExtension.Contains("."))
                    fileExtension = Path.GetExtension(fileExtension);
                else
                    fileExtension = "." + fileExtension;
            }
            if (supportedFileExtensions == null || supportedFileExtensions.Count() <= 0 ||
               (supportedFileExtensions.Count() == 1 && supportedFileExtensions.FirstOrDefault().IsForAll()))
            {
                return true;
            }
            return supportedFileExtensions.ToLower().Contains(fileExtension) || supportedFileExtensions.Contains("*.*");
        }

        public static bool IsForAll(this string s)
        {
            return s == "*.*" || s == "*" || s==".*" || String.IsNullOrEmpty(s);
        }

        public static void RaiseEvent<T>(this EventHandler<T> handler,
                                         object sender, T args) where T : EventArgs
        {
            if (handler != null) handler(sender, args);
        }

        public static Icon GetAssociatedIcon(this string fileName)
        {
            Icon retval = Icon.ExtractAssociatedIcon(fileName);
            return retval;
        }

        public static IEnumerable<T> Top<T>(this IEnumerable<T> list, int count)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (i < count)
                    yield return list.ElementAt(i);
            }
        }

        public static string FirstCharToUpper(this string input)
        {
            string temp = input.Substring(0, 1);
            return temp.ToUpper() + input.Remove(0, 1);
        }

        public static IEnumerable<string> ToLower(this IEnumerable<string> list)
        {
            foreach (string s in list)
            {
                yield return s.ToLower();
            }
        }

        public static string[] ToLower(string[] list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                list[i] = list[i].ToLower(); 
            }
            return list;
        }

        public static bool IsEqual(this Icon icon, Icon other)
        {
            if (icon.Equals(other))
                return true;
            return icon.ToBitmap().IsEqual(other.ToBitmap());
        }

        public static bool IsEqual(this Image image, Image other)
        {
            if (image.Equals(other))
                return true;

            MemoryStream Stream1 = new MemoryStream();
            MemoryStream Stream2 = new MemoryStream();
            image.Save(Stream1,ImageFormat.Bmp);
            other.Save(Stream2,ImageFormat.Bmp);

            return Stream1.IsEqual(Stream2);
        }

        public static bool IsEqual(this MemoryStream stream, MemoryStream other)
        {
            if (stream.Equals(other))
                return true;
            if (stream.Length == other.Length)
            {
                byte[] sArray1 = stream.ToArray();
                byte[] sArray2 = other.ToArray();
                for (int i = 0; i < sArray1.Count(); i++)
                {
                    if(!sArray1[i].Equals(sArray2[i]))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}