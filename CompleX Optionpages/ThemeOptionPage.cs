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
using System.ComponentModel.Composition;
using System.Drawing;
using CompleX.Themes;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class ThemeOptionPage : BaseWizardOptionPage
    {
        private bool inInit;

        public ThemeOptionPage()
        {
            InitializeComponent();
        }

        private void InitPage()
        {
            string currentTheme = Themes.GetCurrentThemeName();
            inInit = true;
            if (listBoxThemes.Items.Count <= 0)
            {
                IEnumerable<string> themes = Themes.GetThemes();
                foreach (var theme in themes)
                    listBoxThemes.Items.Add(theme);
            }

            for (int i = 0; i < listBoxThemes.Items.Count; i++)
            {
                if(listBoxThemes.Items[i].ToString() == currentTheme )
                {
                    listBoxThemes.SelectedIndex = i;
                }
            }
            inInit = false;
        }

        public override void OnCancel()
        {
            if (Settings.Default.Theme != Themes.GetCurrentThemeName())
                Themes.SetTheme(Settings.Default.Theme, false);              
        }

        public override bool OnOk()
        {
            if (listBoxThemes.SelectedItem != null)
            {
                Settings.Default.Theme = listBoxThemes.SelectedItem.ToString();
            }
            Settings.Set("EnableFormSkin", EnableFormSkin.Checked);

            return true;
        }

        public override void OnActivated(bool activated,bool asProjectOptions)
        {
            if (activated)
            {
                InitPage();
                if (Environment.OSVersion.Version.Major < 6)
                {
                    EnableFormSkin.Checked = true;
                    EnableFormSkin.Enabled = false;
                }
                else
                {
                    EnableFormSkin.Checked = Settings.Get<bool>("EnableFormSkin");
                }
            }
        }

        public override ValidationResult ValidatePage()
        {
            return new ValidationResult(true,String.Empty);
        }


        public override Image Image
        {
            get { return Images.verbindungsstatus_aktiv_16;  }
        }

        public override bool AllowedAsProjectOptions
        {
            get
            {
                return false;
            }
        }

        public override string PageID
        {
            get { return "ThemeOptions"; }
        }

        public override string ParentPageID
        {
            get
            {
                return "GeneralOptions";
            }
        }

        public override string PageTitle
        {
            get
            {
                return "Theme";
            }
        }

        private void listBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxThemes.SelectedItem != null && !inInit)
            {
                Themes.SetTheme(listBoxThemes.SelectedItem.ToString(), false);
            }
        }
    }
}
