//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Globalization;
using System.Linq;
using CompleX;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_Types;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class GeneralOptionPage : BaseWizardOptionPage
    {
        
        private bool languageHasChanged;
        public GeneralOptionPage()
        {
            InitializeComponent();
            var cultures = LanguageService.GetAvailableCultures();
            foreach (CultureInfo cultureInfo in cultures)
            {
                comboBoxEditLanguage.Properties.Items.Add(new CustomCulture(cultureInfo));
            }
        }

        public override bool Initialize()
        {
            checkBoxKeepMainMenuOnFullScreen.Checked = Settings.Get(Const.SettingKeepMainMenuOnFullScreen, false);
            checkBoxShowFileIconOnTabPage.Checked = Settings.Get("ShowFileIconOnTabPage", true);
            return true;
        }

        public override bool OnOk()
        {
            try
            {
                bool result = true;
                Settings.Set(Const.SettingKeepMainMenuOnFullScreen,checkBoxKeepMainMenuOnFullScreen.Checked);
                Settings.Default.OpenLastRecentFiles = checkBoxOpenLastRecentFiles.Checked;
                Settings.Default.MaxRecentMenuCount = Convert.ToInt32(spinEditItemCountRecent.Value);
                Settings.Set("ShowFileIconOnTabPage", checkBoxShowFileIconOnTabPage.Checked);
                if (comboBoxEditLanguage.Enabled)
                {
                    if (languageHasChanged && comboBoxEditLanguage.SelectedItem != LanguageService.GetCurrentCulture() || Settings.Default.Language == "auto")
                    {
                        result = LanguageService.SetCulture(comboBoxEditLanguage.SelectedItem as CultureInfo);
                        TryRestart();
                    }
                }
                else if (languageHasChanged && Settings.Default.Language != "auto")
                {
                    Settings.Default.Language = "auto";
                    TryRestart();
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

        private void TryRestart()
        {
            if (MessageService.Ask(Resources.RestartConfirmation, PageTitle))
            {
                CompleX_Studio.Restart(false);
            }
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
           if(activated)
           {
               CultureInfo culture = LanguageService.GetCurrentCulture();
               if (culture.Parent != null && !comboBoxEditLanguage.Properties.Items.Contains(culture))
                   culture = culture.Parent;
               comboBoxEditLanguage.SelectedItem = new CustomCulture(culture);
               checkBoxAutoLanguage.Checked = Settings.Default.Language.Equals("auto");               
               comboBoxEditLanguage.Enabled = !checkBoxAutoLanguage.Checked;
               checkBoxOpenLastRecentFiles.Checked = Settings.Default.OpenLastRecentFiles;
               spinEditItemCountRecent.Value = Settings.Default.MaxRecentMenuCount;
               checkBoxOpenLastRecentFiles.Text = String.Format(labelCheckRecent.Text,spinEditItemCountRecent.Value);
               languageHasChanged = false;
               buttonDeleteDsa.Enabled = MessageService.GetAllUsedDsaKeys().Count() > 0;
           }
        }

        public override ValidationResult ValidatePage()
        {
            return base.ValidatePage();
        }

        public override string PageID
        {
            get { return "GeneralOptions"; }
        }


        public override Image Image
        {
            get { return Images.einstellungen_16; }
        }

        public override string PageTitle
        {
            get
            {
                return "General";
            }
        }

        private void checkBoxAutoLanguage_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditLanguage.Enabled = !checkBoxAutoLanguage.Checked;
            languageHasChanged = true;
        }

        private void spinEditItemCountRecent_EditValueChanged(object sender, EventArgs e)
        {
            checkBoxOpenLastRecentFiles.Text = String.Format(labelCheckRecent.Text, spinEditItemCountRecent.Value);
        }

        private void comboBoxEditLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            languageHasChanged = true;
        }

        private void ButtonDeleteDsaClick(object sender, EventArgs e)
        {
            if(MessageService.Ask(Resources.DeleteDsaConfirmation,Resources.Delete))
            {
                MessageService.DeleteAllUsedDsaKeys();
                buttonDeleteDsa.Enabled = false;
            }
        }


    }
}
