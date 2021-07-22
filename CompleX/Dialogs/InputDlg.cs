//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Collections;
using System.Windows.Forms;

namespace CompleX.Dialogs
{
    public partial class InputDlg : DevExpress.XtraEditors.XtraForm
    {
        public InputDlg()
        {
            InitializeComponent();
        }

        public static DialogResult Execute(string caption, string text, ref string inputtext)
        {
            var inputBox = new InputDlg();
            inputBox.textEdit.Text = inputtext;
            var result = inputBox.Execute(caption, text, true);
            inputtext = inputBox.textEdit.Text;
            return result;
        }

        public static string Execute(string caption, string text)
        {
            var inputBox = new InputDlg();
            if (inputBox.Execute(caption, text, true) == DialogResult.OK)
                return inputBox.textEdit.Text;

            return "";
        }

        public static string Execute(string caption, string text, string defaulttext)
        {
            var inputBox = new InputDlg();
            inputBox.textEdit.Text = defaulttext;
            if (inputBox.Execute(caption, text, true) == DialogResult.OK)
                return inputBox.textEdit.Text;

            return "";
        }

        public static string Execute(string caption, string text, IEnumerable items)
        {
            var inputBox = new InputDlg();
            foreach (var entry in items)
            {
                inputBox.comboBoxEdit.Properties.Items.Add(entry);
            }
            inputBox.comboBoxEdit.Visible = true;
            if (inputBox.Execute(caption, text, true) == DialogResult.OK)
                return inputBox.comboBoxEdit.Text;

            return "";
        }

        public static string Execute(string caption, string text, IEnumerable items, string defaulttext)
        {
            var inputBox = new InputDlg();
            foreach (var entry in items)
            {
                inputBox.comboBoxEdit.Properties.Items.Add(entry);
            }
            inputBox.comboBoxEdit.Visible = true;
            inputBox.comboBoxEdit.Text = defaulttext;
            if (inputBox.Execute(caption, text, true) == DialogResult.OK)
                return inputBox.comboBoxEdit.Text;

            return "";
        }

        public DialogResult Execute(string caption, string text, bool showModal)
        {
            Text = caption;
            TextLabel.Text = text;
            if (showModal)
            {
                return ShowDialog();
            }
            Show();
            return DialogResult.None;
        }

        public static string ExecutePassword(string caption, string text)
        {
            var inputBox = new InputDlg();
            inputBox.textEdit.Properties.PasswordChar = '*';
            if (inputBox.Execute(caption, text, true) == DialogResult.OK)
                return inputBox.textEdit.Text;

            return "";
        }

    }
}