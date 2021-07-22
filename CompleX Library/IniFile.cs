//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Runtime.InteropServices;
using System.Text;

namespace CompleX_Library
{
    public class IniFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
          string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
          string key, string def, StringBuilder retVal,
          int size, string filePath);

        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.path);
        }

        public string ReadValue(string section, string key, string defaultValue = "")
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            string result = temp.ToString();
            if (string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(defaultValue))
                result = defaultValue;
            return result;
        }

        public static void WriteValue(string fileName, string section, string key, string value)
        {
            var ini = new IniFile(fileName);
            ini.WriteValue(section, key, value);
        }

        public static string ReadValue(string fileName, string section, string key, string defaultValue = "")
        {
            var ini = new IniFile(fileName);
            return ini.ReadValue(section, key, defaultValue);
        }


    }

}