//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Collections.Generic;

namespace CompleX_Settings.Constants
{
    public static class Const
    {
        public const int AutoVisibleChanged = 8273;
        //http://theforce.net/swenc/planets.asp  (Beta is Sanyassa)
        public const string ApplicationName = "CompleX Studio";
        public const string CodeName = " Tarhassan ";
        public const string ShortName = "COMPLEX";
        public const string State = " Alpha Preview";
        public const string TemporaryDockFile = "_tmpLastDock.xml";
        public const string NewGroupTag = "NGT";
        public const string DefaultToolBoxCategory = "Standard";
        public const string DefaultContentEditMergeKey = "ContentEdit";

        // ID constants
        public const int ToToolWindowId = 0x100;

        //REGEX
        //public const string RegexHref = "href=[\'\"]?([^\'\" >]+)";
        public const string RegexHref = "href=((\"[^\"]*\")|('[^']*')|[^>]*)";
        public const string RegexFunctionSwk = @"(?<=function\s*)([a-zA-Z][a-zA-Z0-9]*\s*)\(([a-zA-Z][a-zA-Z0-9]*\s*,?\s*)*\)";
        public const string RegexFunction = @"function(\s)+[a-zA-Z][a-zA-Z0-9]*\((\s)*([a-zA-Z0-9]+(\s)*,?(\s)*)*\)";
        public const string RegexFunctionBlock = @"(?<=function\s*?([a-zA-Z][a-zA-Z0-9]*\s*)\(([a-zA-Z][a-zA-Z0-9]*\s*,?\s*)*\))\{[^\{\}]*(((?<Open>\{)[^\{\}]*)+((?<Close-Open>\})[^\{\}]*)+)*(?(Open)(?!))\}";
        //matcht Tags vom ersten das er findet bis zum zugehörigen Closing Tag
        public const string RegexMatchAllHtmlTags = @"(<(?<tag>\w+)(?<params>(?:\s[^>]*)*)>(?<content>(?((<\k<tag>\W*>)|(</\k<tag>>))(?!)|.)*(((?<open><\k<tag>\W*>)(?((<\k<tag>\W*>)|(</\k<tag>>))(?!)|.)*)+((?<close-open>(</\k<tag>>))(?((<\k<tag>\W*>)|(</\k<tag>>))(?!)|.)*)+)*(?(open)(?!)))</\k<tag>>)|(<(?<tag>\w+)(?<params>(?:\s[^>]*)*)/>(?<content>))";

        // DSA KEYS
        public const string DsaDefault = "DSA_";
    
        // SETTING KEYS
        public const string SettingsWasEverStarted = "Setting_WasEverStarted_V3"; 
        public const string SettingShowLogAlert = "Setting_ShowLogAlert";
        public const string SettingShowLogToolWindow = "Setting_ShowToolWindowOnNewLog";
        public const string SettingSaveMessageLog = "Setting_SaveMessageLog";
        public const string SettingShowLogFromTodayOnly = "Setting_ShowLogFromTodayOnly";
        public const string SettingShowDosConsole = "Setting_ShowDosCommandConsole";
        public const string SettingLastDosCommand = "Setting_LastDosCommand";
        public const string SettingLastDosDir = "Setting_LastDosDir";
        public const string SettingKeepMainMenuOnFullScreen = "Setting_KeepMainMenuOnFullScreen";

        public static Dictionary<string, string> CompleXFileExtensions = new Dictionary<string, string>
                                                 {
                                                     {".cspr", ApplicationName + " " + "Project Files"},
                                                     {".cslpr", ApplicationName + " " + "Linked Project Files"},
                                                     {".cssf", ApplicationName + " " + "Settings"},
                                                     {".cstbx", ApplicationName + " " + "ToolBox"},
                                                     {".csms", ApplicationName + " " + "Script Files"}
                                                 };

        public const string TasksAdd = ".Tasks.tskl";
    }
}