//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Diagnostics;
using Microsoft.Win32;

namespace CompleX_Library.Helper
{
    public static class FileShellExtension
    {
        public static void Register(string fileType,
                                    string shellKeyName, string menuText, string menuCommand)
        {
            // create path to registry location

            string regPath = string.Format(@"{0}\shell\{1}",
                                           fileType, shellKeyName);

            // add context menu to the registry
            //Registry.ClassesRoot.CreateSubKey(regPath))
            using (RegistryKey key =
                Registry.ClassesRoot.CreateSubKey(regPath))
            {
                if (key != null) key.SetValue(null, menuText);
            }

            // add command that is invoked to the registry

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                string.Format(@"{0}\command", regPath)))
            {
                if (key != null) key.SetValue(null, menuCommand);
            }
        }

        public static void Unregister(string fileType, string shellKeyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileType) &&
                         !string.IsNullOrEmpty(shellKeyName));

            // path to the registry location

            string regPath = string.Format(@"{0}\shell\{1}",
                                           fileType, shellKeyName);

            // remove context menu from the registry
            //Registry.ClassesRoot.DeleteSubKeyTree(regPath))
            Registry.ClassesRoot.DeleteSubKeyTree(regPath);
        }
    }
}