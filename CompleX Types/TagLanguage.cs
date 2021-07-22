//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;

namespace CompleX_Types
{
    public enum TagLanguage
    {
        ASPNet = 0,
        CFML = 1,
        HTML = 2,
        JRun = 3,
        JSP = 4,
        PHP = 5,
        VTML = 6,
        WML = 7,
        XSLT = 8,
    }

    public static class TagLanguageHelper
    {
        public static TagLanguage AsTagLanguage(this string s)
        {
            switch (s.ToLower())
            {
                case "aspnet":
                    return TagLanguage.ASPNet;
                case "cfml":
                    return TagLanguage.CFML;
                case "html":
                    return TagLanguage.HTML;
                case "jrun":
                    return TagLanguage.JRun;
                case "jsp":
                    return TagLanguage.JSP;
                case "php":
                    return TagLanguage.PHP;
                case "wml":
                    return TagLanguage.WML;
                case "vtml":
                    return TagLanguage.VTML;
                case "xslt":
                    return TagLanguage.XSLT;
                default:
                    return TagLanguage.HTML;
            }
        }

        public static string AsString(this TagLanguage language)
        {
            return language.ToString();
        }
        public static int AsInteger(this TagLanguage language)
        {
            return Convert.ToInt32(language);
        }
    }
}