//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Types;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class FtpOptionPage : BaseOptionPage
    {
        private BindingList<FtpSettings> ftpCollection;
        private bool editIsStarted;
        private string tmpPassword;
        private static string PasswordChangeText
        {
            get {return @"[ " + Resources.ClickToChangePassword + @" ]";}
        }

        public override Guid ID
        {
            get
            {
                return new Guid("557DE147-5DBB-4942-AB92-182625C90BD4");
            }
        }

        public override string PageTitle
        {
            get
            {
                return "FTP Connections";
            }
        }

        public override System.Drawing.Image Image
        {
            get
            {
                return Images.ftp;
            }
        }

        public FtpSettings SelectedSetting
        {
            get { return listBoxFtpCollection.SelectedValue as FtpSettings; }
        }

        public FtpOptionPage()
        {
            InitializeComponent();
            contextMenuStrip.Renderer = new Office2007Renderer.Office2007Renderer();
        }

        public override string PageID
        {
            get
            {
                return "FtpOptions";
            }
        }

        public override ValidationResult ValidatePage()
        {
            if (ftpCollection != null)
            {
                foreach (var ftpSettings in ftpCollection)
                {
                    if (String.IsNullOrEmpty(ftpSettings.Server) )
                        return new ValidationResult(false, ftpSettings + " "+Resources.PleaseEnterServer);
                    if(String.IsNullOrEmpty(ftpSettings.Name))
                        return new ValidationResult(false, ftpSettings + " "+Resources.PleaseEnterName);
                }
            }
            return new ValidationResult(true,String.Empty);
        }

        public override bool OnOk()
        {
            foreach (var ftpSettings in ftpCollection)
                ftpSettings.EndEdit();

            var tmpCollection = ftpCollection.Select(ftpSetting => ftpSetting.Clone() as FtpSettings).ToList();
            Settings.Set("FtpCollection", tmpCollection);

            editIsStarted = false;
            ftpCollection = null;
            return true;
        }

        public override void OnCancel()
        {
            foreach (var ftpSettings in ftpCollection)
                ftpSettings.CancelEdit();
            editIsStarted = false;
            ftpCollection = null;
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
            if(activated && ftpCollection == null)
            {
                ftpCollection = new BindingList<FtpSettings>();
                var tmpCollection = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
                if(tmpCollection.Count() > 0)
                {
                    foreach (var ftpSetting in tmpCollection)
                        ftpCollection.Add(ftpSetting.Clone() as FtpSettings);
                }


                listBoxFtpCollection.DataSource = ftpCollection;
                EnableControls();
            }
            if(activated && !editIsStarted)
            {
                foreach (FtpSettings ftpSettings in ftpCollection)
                    ftpSettings.BeginEdit();
                editIsStarted = true;
            }
        }

        private void EnableControls()
        {
            bool enabled = SelectedSetting != null;
            textEditDefaultDir.Enabled = enabled;
            textEditName.Enabled = enabled;
            textEditPassword.Enabled = enabled;
            textEditPort.Enabled = enabled;
            textEditServer.Enabled = enabled;
            textEditUser.Enabled = enabled;
            simpleButtonRefresh.Enabled = enabled;
            checkBoxEnableSsl.Enabled = enabled;
            simpleButtonExport.Enabled = enabled;
            buttonDelete.Enabled = enabled;
        }

        private void ListBoxFtpCollectionSelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControls();
            SetBinding(textEditName, "Text","Name");
            SetBinding(textEditServer, "Text","Server");
            SetBinding(textEditUser, "Text", "UserName");
            if (SelectedSetting == null || String.IsNullOrEmpty(SelectedSetting.Password))
            {
                textEditPassword.Properties.PasswordChar = '*';
                SetBinding(textEditPassword, "Text", "Password");
            }else
            {
                tmpPassword = SelectedSetting.Password;
                textEditPassword.DataBindings.Clear();
                textEditPassword.Properties.PasswordChar = (char)0; 
                textEditPassword.Text = PasswordChangeText;
            }
            SetBinding(textEditDefaultDir, "Text", "DefaultDirectory");
            SetBinding(textEditPort, "Text", "Port");
            SetBinding(checkBoxEnableSsl, "Checked", "EnableSsl");
            ThreadPool.QueueUserWorkItem(CheckFtpServerSettings);
        }

        private void CheckFtpServerSettings(object state)
        {
            if (SelectedSetting != null)
            {
                pictureBox1.Image = null;
                var tmpSettings = SelectedSetting.Clone() as FtpSettings;
                var connection = new FtpConnection(tmpSettings);
                bool canConnect = connection.CanConnect;
                pictureBox1.Image = canConnect ? pictureBox1.InitialImage : pictureBox1.ErrorImage;
            }
        }

        private void SetBinding(Control control,string propertyName, string dataMember )
        {
            if (SelectedSetting != null)
            {
                control.DataBindings.Clear();
                control.DataBindings.Add(propertyName, SelectedSetting, dataMember);
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            var ftpSettings = new FtpSettings("Server"+ftpCollection.Count);
            ftpCollection.Add(ftpSettings);
            ftpSettings.BeginEdit();
            listBoxFtpCollection.SelectedIndex = listBoxFtpCollection.ItemCount - 1;
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            SelectedSetting.EndEdit();
            ftpCollection.Remove(SelectedSetting);
        }

        private void SimpleButtonRefreshClick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(CheckFtpServerSettings);
        }

        private void TextEditPasswordEnter(object sender, EventArgs e)
        {
            if (SelectedSetting != null && textEditPassword.Text == PasswordChangeText)
            {
                textEditPassword.Properties.PasswordChar = '*';
                SelectedSetting.Password = String.Empty;
                SetBinding(textEditPassword, "Text", "Password");
            }
        }

        private void TextEditPasswordLeave(object sender, EventArgs e)
        {
            if (SelectedSetting != null && String.IsNullOrEmpty(textEditPassword.Text) && !String.IsNullOrEmpty(tmpPassword) && !MessageService.AskDsa(Resources.RemoveSettedPassword, label3.Text, @"RemoveSettedPasswordOnLeave"))
            {
                SelectedSetting.Password = tmpPassword;
                textEditPassword.DataBindings.Clear();
                textEditPassword.Properties.PasswordChar = (char) 0;
                textEditPassword.Text = PasswordChangeText;
            }
        }

        private void SimpleButtonExportClick(object sender, EventArgs e)
        {
            if (SelectedSetting != null)
            {
                var settingsToExport = SelectedSetting.Clone() as FtpSettings;
                if (settingsToExport != null)
                {
                    bool exportWithPw = MessageService.AskDsa(Resources.QuestionExportWithPassword,
                                                              simpleButtonExport.Text,
                                                              "ExportFTPSettingsWithPasswordConfirmation");

                    if (!exportWithPw)
                        settingsToExport.Password = String.Empty;
                    else
                    {
                        string pw = InputDlg.ExecutePassword(Resources.EnterPassword,Resources.Password);
                        if(!pw.Equals(settingsToExport.Password))
                        {
                            if(!String.IsNullOrEmpty(pw))
                                MessageService.ShowError(Resources.PasswordWrong, Resources.Exception);
                            return;
                        }
                    }

                    var dlg = new SaveFileDialog
                                  {
                                      DefaultExt = ".xml",
                                      Filter = @"XML (*.xml) | *.xml",
                                      FileName = settingsToExport.Name + ".xml"
                                  };

                    if(dlg.ShowDialog() == DialogResult.OK)
                        settingsToExport.TryXmlSerialize(dlg.FileName);
                    

                }
            }
        }

        private void SimpleButtonSearchClick(object sender, EventArgs e)
        {
            try
            {
                FtpExplorer.SelectDirectory(SelectedSetting);
            }
            catch (Exception)
            {
                MessageService.ShowError(Resources.FailedToConnect);
            }
        }

        private void ListBoxFtpCollectionMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedConnection();
        }

        private void OpenSelectedConnection()
        {
            FileService.OpenFtp(SelectedSetting);
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenSelectedConnection();
        }

        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            openToolStripMenuItem.Enabled = SelectedSetting != null;
        }
    }
}
