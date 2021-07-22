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
using System.Linq;
using System.Text;
using CompleX_Settings;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace CompleX.Themes
{
    public static class Themes
    {
        /// <summary>
        /// Returns aktive theme
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentThemeName()
        {
            return UserLookAndFeel.Default.SkinName;
        }

        /// <summary>
        /// Sets a Theme/Skin
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public static bool SetTheme(string themeName, bool autoSave)
        {
            if (GetThemes().Contains(themeName))
            {
                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SetSkinStyle(themeName);
                CompleX_Studio.Instance.barManager.GetController().PaintStyleName = "Skin";
                if (autoSave)
                {
                    Settings.Default.Theme = themeName;
                    Settings.Default.Save();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns all possible skins
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetThemes()
        {
            var result = new List<string>();
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
                result.Add(cnt.SkinName);
            return result;    
        }

    }
}
