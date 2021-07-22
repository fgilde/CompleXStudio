using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CompleX_Library;

namespace CompleX.Helper
{
    public static class StringExtensions
    {

        public static string AsString(this IEnumerable<string> l)
        {
            return l.Aggregate(String.Empty, (current, s) => current + (s + ","));
        }

        public static int ToInt32(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return -1;
            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                return -1;
            }
        }
        public static string ToShortPath(this string path)
        {
            return ToShortPath(path, 40);
        }

        /// <summary>
        /// Diese Funktion kürzt einen Pfad ab so das aus
        /// "C:\Windows\System32\Test\Test.dll" dann "C:\Windows\...\Test.dll" wird.
        /// </summary>
        /// <param name="path">Der Pfad, der gekürzt zurückgegeben werden soll.</param>
        /// <param name="length">Die gewünschte Länge, die nicht überschritten werden darf.</param>
        public static string ToShortPath(this string path, int length)
        {
            string[] pathParts = path.Split('\\');
            var pathBuild = new StringBuilder(path.Length);
            string lastPart = pathParts[pathParts.Length - 1];
            string prevPath = String.Empty;

            //Erst prüfen ob der komplette String evtl. bereits kürzer als die Maximallänge ist
            if (path.Length < length)
                return path;
            
            for (int i = 0; i < pathParts.Length - 1; i++)
            {
                pathBuild.Append(pathParts[i] + @"\");
                if ( (pathBuild + @"...\" + lastPart).Length >= length)
                    return prevPath;
                prevPath = pathBuild + @"...\" + lastPart;
            }
            return prevPath;
        }


        /// <summary>
        /// Convert to the relative path.
        /// </summary>
        public static string ToRelativePathOrUri(this string s)
        {
            return s.ToRelativePathOrUri(CompleX_Studio.CurrentFile);
        }

        /// <summary>
        /// Convert to the relative path.
        /// </summary>
        public static string ToRelativePathOrUri(this string s, string referenceFileName)
        {
            if (File.Exists(s) && File.Exists(referenceFileName))
            {
                string result = FileSystemUtils.GetRelativePath(s, false, referenceFileName, false);
                if (result.Equals(s))
                    result = new Uri(s).AbsolutePath;
                return result;
            }
            if(File.Exists(s))
                s = new Uri(s).AbsolutePath;
            return s;
        }

        public static bool IsValidAlphaNumeric(this string strAlphanum)
        {
            var pattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return pattern.IsMatch(strAlphanum.Trim());
        }



    }
}
