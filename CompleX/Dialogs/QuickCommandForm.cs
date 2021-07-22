//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Windows.Forms;
using CompleX.Helper;

namespace CompleX.Dialogs
{
    public partial class QuickCommandForm : DevExpress.XtraEditors.XtraForm
    {
        public QuickCommandForm()
        {
            InitializeComponent();
        }

        public bool ShowConsole
        {
            get
            {
                return checkEditShoConsole.Checked;
            }
            set
            {
                checkEditShoConsole.Checked = value;
            }
        }

        public string Command
        {
            get
            {
                return PlaceHolder.ReplacePlaceHolder( buttonEditCommand.Text );
            }
            set
            {
                buttonEditCommand.Text = value;
            }
        }

        public string Directory
        {
            get
            {
                return PlaceHolder.ReplacePlaceHolder(buttonEdit1.Text);
            }set
            {
                buttonEdit1.Text = value;
            }
        }


        private void buttonEditCommand_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index <=0)
            {
                var dlg = new OpenFileDialog {Filter = "Exe, Bat, Cmd | *.exe;*.cmd;*.bat"};
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    buttonEditCommand.Text = dlg.FileName;
                }
            }else
            {
                PlaceHolder.ShowPopup(s=>buttonEditCommand.SelectedText = s);
            }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index <= 0)
            {
                var dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    buttonEdit1.Text = dlg.SelectedPath;
                }
            }
            else
            {
                PlaceHolder.ShowPopup(s => buttonEdit1.SelectedText = s);
            }
        }
    }
}