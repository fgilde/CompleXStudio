//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.ComponentModel.Composition;
using System.Drawing;
using CompleX;
using CompleX.Controls;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class MessageLogOptionPage : BaseOptionPage
    {
        public MessageLogOptionPage()
        {
            InitializeComponent();
        }

        public override bool Initialize()
        {
            checkBoxSaveLog.Checked = Settings.Get(Const.SettingSaveMessageLog, false);
            checkBoxAlert.Checked = Settings.Get(Const.SettingShowLogAlert, true);
            checkBoxActivateToolWindow.Checked = Settings.Get(Const.SettingShowLogToolWindow, true);
            checkBoxToday.Checked = Settings.Get(Const.SettingShowLogFromTodayOnly, true);
            return true;
        }

        public override bool OnOk()
        {
            Settings.Set(Const.SettingSaveMessageLog, checkBoxSaveLog.Checked);
            Settings.Set(Const.SettingShowLogAlert, checkBoxAlert.Checked);
            Settings.Set(Const.SettingShowLogToolWindow, checkBoxActivateToolWindow.Checked);
            Settings.Set(Const.SettingShowLogFromTodayOnly, checkBoxToday.Checked);
            var messageLogControl = ApplicationHost.Host.GetService<MessageLogControl>();
            if (messageLogControl != null)
            {
                messageLogControl.SetCheckBoxes();
            }
            return true;
        }


        public override string PageID
        {
            get { return "MessageLogOptions"; }
        }

        public override string ParentPageID
        {
            get { return "GeneralOptions"; }
        }

        public override string PageTitle
        {
            get { return "Message Log"; }
        }

        public override Image Image
        {
            get { return Images.liste_16; }
        }

    }
}
