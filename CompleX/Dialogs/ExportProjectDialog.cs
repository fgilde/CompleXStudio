//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Settings;
using CompleX_Types;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class ExportProjectDialog : XtraForm
    {

        public ExportProjectDialog():this(false)
        {}

        public ExportProjectDialog(bool canUseFtp)
        {
            InitializeComponent();

            Size = !canUseFtp ? new Size(370,160) : new Size(370,190);

            comboBoxEditFtp.Visible = canUseFtp;
            labelConnection.Visible = canUseFtp;
            if(canUseFtp)
            {
                var tmpCollection = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
                if (tmpCollection.Count() > 0)
                {
                    foreach (var ftpSetting in tmpCollection)
                        comboBoxEditFtp.Properties.Items.Add(ftpSetting.Clone() as FtpSettings);
                }
                Text = Resources.Upload;
            }

        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public FtpSettings FtpSettings
        {
            get { return comboBoxEditFtp.SelectedItem as FtpSettings; }
            set { comboBoxEditFtp.SelectedItem = value; }
        }

        public bool ExportProject
        {
            get { return checkBoxProjectFile.Checked; }
            set { checkBoxProjectFile.Checked = value; }
        }

        public bool StoreInZip
        {
            get { return checkBoxZip.Checked; }
            set { checkBoxZip.Checked = value;}
        }

        public string Directory
        {
            get { return buttonEditDirectory.Text; }
            set { buttonEditDirectory.Text = value; }
        }

        private void ButtonEditDirectoryButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!comboBoxEditFtp.Visible)
            {
                var dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                    buttonEditDirectory.Text = dlg.SelectedPath;
            }else
            {
                try
                {
                    if(FtpSettings == null)
                    {
                        MessageService.ShowError(Resources.PleaseConnectToServer);
                        comboBoxEditFtp.Focus();
                        return;
                    }
                    buttonEditDirectory.Text = FtpExplorer.SelectDirectory(FtpSettings);
                }
                catch(Exception)
                {
                    MessageService.ShowError(Resources.FailedToConnect);
                }
            }
        }

        private void ExportProjectDialog_Shown(object sender, EventArgs e)
        {
            if (comboBoxEditFtp.Visible)
                comboBoxEditFtp.Focus();
            else
                buttonEditDirectory.Focus();
        }

        private void comboBoxEditFtp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CompleX_Studio.Instance.ManageFtpAccounts();
                comboBoxEditFtp.Properties.Items.Clear();
                var tmpCollection = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
                if (tmpCollection.Count() > 0)
                {
                    foreach (var ftpSetting in tmpCollection)
                        comboBoxEditFtp.Properties.Items.Add(ftpSetting.Clone() as FtpSettings);
                }
            }
        }


    }
}