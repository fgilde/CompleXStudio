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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CompleX_Settings;

namespace CompleX.Services
{
    public class LanguageService 
    {
        public static bool SetCulture(CultureInfo cultureInfo)
        {
            try
            {
                if(cultureInfo.IsNeutralCulture)
                {
                    cultureInfo = new CultureInfo(cultureInfo.TextInfo.CultureName);
                }
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Settings.Default.Language = cultureInfo.ToString();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the current language.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentLanguage()
        {
            return Thread.CurrentThread.CurrentUICulture.ToString();
        }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Gets the available cultures.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CultureInfo> GetAvailableCultures()
        {
            return GetInstalledCultures();
        }

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAvailableLanguages()
        {
            var cultures = GetInstalledCultures();
            foreach (CultureInfo culture in cultures)
                yield return culture.ToString();
        }


        /// <summary>
        /// returns a list of installed cultures based from the directory of this assembly
        /// </summary>
        /// <returns></returns>
        public static List<CultureInfo> GetInstalledCultures()
        {
            var result = new List<CultureInfo>();
            string[] directories = GetDirectories();
            foreach (string directory in directories)
            {
                try
                {
                    var info = new CultureInfo(directory);
                    result.Add(info);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }
            return result;
        }

        private static string[] GetDirectories()
        {
            try
            {
                string name = Path.GetDirectoryName((typeof(LanguageService).Assembly.Location));
                string[] directories = Directory
                    .GetDirectories(name)
                    .Select(file => Path.GetFileName(file))
                    .Where(file => !String.IsNullOrEmpty(file) && file.Length < 4)
                    .ToArray();
                return directories;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new string[0];
        }

    }
}
