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

namespace CompleX_Settings.Constants
{
    public partial class Pathes
    {
        private const string ClientName = "CompleX";
        private const string CompanyName = "nksoft";
        private const string TemplateDir = @"CompleX Data\DocumentTypes\NewDocuments";

        public static string REFERENCE = @"CompleX Data\Content";	
        public static string TAGLIBARY = @"CompleX Data\TagLibraries";	


        /// <summary>
        /// client folder 
        /// </summary>
        public static readonly string ClientFolder;

        /// <summary>
        /// common application data folder 
        /// </summary>
        public static readonly string CommonApplicationData;

        /// <summary>
        /// company folder 
        /// </summary>
        public static readonly string CompanyFolder;

        /// <summary>
        /// local applkication data folder
        /// </summary>
        public static readonly string LocalApplicationData;

        /// <summary>
        /// settings
        /// </summary>
        public static readonly string SettingsFolder;

        /// <summary>
        /// Location of Dockings
        /// </summary>
        public static readonly string LayoutFolder;

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <value>The application path.</value>
        public static string ApplicationPath
        {
            get
            {
                return Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath).AddDirectorySeparatorChar();
            }
        }

        public static string TemplatePath
        {
            get
            {
                return ApplicationPath.AddDirectorySeparatorChar() + TemplateDir.AddDirectorySeparatorChar();
            }
        }

        //public static string ExternalConfigPath
        //{
        //    get
        //    {
        //        return SettingsFolder.AddDirectorySeparatorChar() + "ExternalTools.conf";
        //    }
        //}

        //public static string ToolsDirectoryList
        //{
        //    get
        //    {
        //        return SettingsFolder.AddDirectorySeparatorChar() + "ToolDirectories.xml";
        //    }
        //}

        public static string ToolBoxPath
        {
            get
            {
                return SettingsFolder.AddDirectorySeparatorChar()+ "ToolBox.cstbx";
                //return ApplicationPath.AddDirectorySeparatorChar() + "ToolBox.cstbx";
            }
        }

        public static string RecentFileList
        {
            get
            {
                return SettingsFolder.AddDirectorySeparatorChar() + "RecentFileList.xml";
                //return ApplicationPath.AddDirectorySeparatorChar() + "ToolBox.cstbx";
            }
        }

        public static string ToolsPath
        {
            get
            {
                return ApplicationPath.AddDirectorySeparatorChar() + @"CompleX Data\Tools";
            }
        }

        public static string SettingsFile
        {
            get
            {
                return SettingsFolder.AddDirectorySeparatorChar() + "CompleXSettings.bin";
            }
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        public static string GetFolderPath(Environment.SpecialFolder folder )
        {
            return Environment.GetFolderPath(folder);
        }

        static Pathes()
        {
            CommonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            LocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            CompanyFolder = GetFolder(true, CommonApplicationData, CompanyName);
            ClientFolder = GetFolder(true, CommonApplicationData, CompanyName, ClientName);

            SettingsFolder = GetFolder(true, LocalApplicationData, CompanyName, ClientName, "Settings");
            LayoutFolder = GetFolder(true, SettingsFolder, "Dockings");
            
        }

        private static string GetFolder(bool autoCreate, params string[] pathElement)
        {
            string result = String.Empty;
            foreach (var element in pathElement)
            {
                if (String.IsNullOrEmpty(result))
                    result = element;
                else
                {
                    result = Path.Combine(result, element);
                }
                AutoCreateDirectory(autoCreate, result);
            }
            return result;
        }

        private static void AutoCreateDirectory(bool autoCreate, string result)
        {
            if (autoCreate)
            {
                if (!Directory.Exists(result))
                    Directory.CreateDirectory(result);
            }
        }
    }
}