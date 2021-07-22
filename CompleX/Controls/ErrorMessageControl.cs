using System;
using System.ComponentModel;
using System.Windows.Forms;
using CompleX_Library;

namespace CompleX.Controls
{
    public partial class ErrorMessageControl : UserControl
    {
        private MessageLog MessageLog;
        public LogEntry Entry;

        [Category("Notofication"), Description("Shows a Desktopalert if new Error occures")]
        public bool DesktopAlertOnNewEntry { get; set; }


        public ErrorMessageControl()
        {
            InitializeComponent();

        }

        public void Init(MessageLog messageLog)
        {
            MessageLog = messageLog;
            if(MessageLog != null)
                MessageLog.AddEntry += (MessageLog_AddEntry);
        }

        void MessageLog_AddEntry(object sender, LogEntry entry)
        {
            if (entry.LogType == LogType.Error)
            {
                this.Visible = true;
                Entry = entry;
                errorLabel.Text = entry.Message;
                if (errorLabel.Width > this.Width - errorImage.Width)
                {
                    while (errorLabel.Width > this.Width - errorImage.Width)
                    {
                        errorLabel.Text = errorLabel.Text.Substring(0, errorLabel.Text.Length - 1);
                    }
                    errorLabel.Text = errorLabel.Text.Substring(0, errorLabel.Text.Length - 5);
                    errorLabel.Text = errorLabel.Text + "...";
                }

                if (DesktopAlertOnNewEntry)
                    alert.Show(Form.ActiveForm,LogEntry.GetLogType(Entry.LogType),entry.Message,errorImage.Image);
            }
        }

        private void errorLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          //  Logging.ShowEntry(Entry);
        }

        private void errorImage_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void alert_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
          //  Logging.ShowEntry(Entry);
        }
    }
}