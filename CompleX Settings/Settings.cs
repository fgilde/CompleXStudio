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
using System.Configuration;
using System.IO;
using CompleX_Library.Helper;
using CompleX_Settings.Constants;

namespace CompleX_Settings
{
    public partial class Settings
    {
        private static Dictionary<string, object> settings;

        /// <summary>
        /// Constructor
        /// </summary>
        public Settings()
        {
            try
            {
                if (!SerializationHelper.TryBinaryDeserialize(Pathes.SettingsFile, out settings))
                    settings = new Dictionary<string, object>();
            }
            catch (Exception)
            {
                settings = new Dictionary<string, object>();
                if(File.Exists(Pathes.SettingsFile))
                {
                    if(BackupSettings())
                       File.Delete(Pathes.SettingsFile);
                }
            }
        }


        /// <summary>
        /// returns the setting value for given key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static object Get(string key)
        {
            try
            {
                return Default[key];
            }
            catch 
            {
                if (settings.ContainsKey(key))
                {
                    return settings[key];
                }
                return null;
            }
        }

        /// <summary>
        /// Path
        /// </summary>
        public static string Path
        {
            get { return System.IO.Path.GetDirectoryName(Pathes.SettingsFile).AddDirectorySeparatorChar(); }
        }

        /// <summary>
        /// Path
        /// </summary>
        public static string SettingsFile
        {
            get { return Pathes.SettingsFile; }
        }

        /// <summary>
        /// returns the setting value for given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(string key, T defaultValue)
        {
            try
            {
                var result = Get(key);
                if (result != null)
                    return (T)result;
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// returns the setting value for given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return Get(key, default(T));
        }

        public static bool Exists()
        {
            return File.Exists(Pathes.SettingsFile);
        }


        /// <summary>
        /// returns all.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAllKeys()
        {
            return settings.Keys;
        }

        /// <summary>
        /// returns all.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> GetAll()
        {
            return settings;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            if (settings.ContainsKey(key))
                return settings.Remove(key);
            return false;
        }

        /// <summary>
        /// Sets the setting by key to value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Set<T>(string key, T value)
        {
            try
            {
                Default[key] = value;
            }
            catch (SettingsPropertyNotFoundException)
            {
                AddSetting(key, value);
            }
        }

        /// <summary>
        /// Adds a setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void AddSetting<T>(string key, T value)
        {
            if (settings.ContainsKey(key))
                settings[key] = value;
            else
                settings.Add(key, value);

            SaveSettings();
        }

        /// <summary>
        /// Exports settings to the specified destination file.
        /// </summary>
        /// <param name="destinationFile">The destination file.</param>
        /// <returns></returns>
        public static bool Export(string destinationFile)
        {
            if (File.Exists(Pathes.SettingsFile))
            {
                File.Copy(Pathes.SettingsFile, destinationFile,true);
                return File.Exists(destinationFile);
            }
            return false;
        }

        /// <summary>
        /// Backups the settings.
        /// </summary>
        /// <returns></returns>
        public static bool BackupSettings()
        {
            string backup;
            return BackupSettings(out backup);
        }

        /// <summary>
        /// Backups the settings.
        /// </summary>
        /// <param name="backupName">Name of the backup.</param>
        /// <returns></returns>
        public static bool BackupSettings(out string backupName)
        {
            backupName = Pathes.SettingsFile + ".bak_" + DateTime.Now.Millisecond;
            return Export(backupName);
        }

        /// <summary>
        /// Imports settings from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static bool ImportSettings(string fileName)
        {
            if(File.Exists(fileName))
            {
                try
                {
                    string backup;
                    BackupSettings(out backup);
                    if (!InternalImport(fileName))
                    {
                        InternalImport(backup);
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }


        /// <summary>
        /// Saves the settings.
        /// </summary>
        public static void SaveSettings()
        {
            Default.Save();
            settings.TryBinarySerialize(Pathes.SettingsFile);
        }


        private static bool InternalImport(string fileName)
        {
            if(File.Exists(fileName))
            {
                settings = null;
                GC.Collect();
                settings = new Dictionary<string, object>();
                if(File.Exists(Pathes.SettingsFile))
                   File.Delete(Pathes.SettingsFile);
               if(SerializationHelper.TryBinaryDeserialize(fileName, out settings))
               {
                   SaveSettings();
                   return true;
               }

            }
            return false;
        }

    }
}
