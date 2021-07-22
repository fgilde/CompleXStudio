//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.IO;

namespace CompleX_Settings
{
    internal static class InternalHelper
    {
        public static string AddDirectorySeparatorChar(this string s)
        {
            if (!s.EndsWith(Path.DirectorySeparatorChar.ToString()))
                s += Path.DirectorySeparatorChar;
            return s;
        }
    }
}
