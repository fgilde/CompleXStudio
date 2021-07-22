using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Settings.Constants;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class EventEditControl : UserControl,IAttributeEdit
    {
        public EventEditControl()
        {
            InitializeComponent();
        }

        public TagAttribute Attribute { get; set; }

        public void Init()
        {
            textBox.Text = Attribute.AtrributeValue;
            labelName.Text = Attribute.AtrributeName;
        }

        private void insertFunctionBtn_Click(object sender, EventArgs e)
        {
            if (CompleX_Studio.CurrentContentEditor != null && CompleX_Studio.CurrentContentEditor.Content is string)
            {
                var linkPattern = new Regex(Const.RegexFunction);
                MatchCollection collection = linkPattern.Matches((string)CompleX_Studio.CurrentContentEditor.Content);
                var values = new List<string>();
                foreach (var matchCollection in collection)
                {
                    string func = matchCollection.ToString().Substring(9);
                    if (!values.Contains(func))
                        values.Add(func);
                }
                var stringDlg = new SelectStringDialog(values);
                if(stringDlg.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text += stringDlg.SelectedText;
                }
            }else
            {
                MessageService.ShowError(Resources.ActionNotPossible,Resources.Exception);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            UpdateAttribute();
        }

        private void UpdateAttribute()
        {
            string code = textBox.Text;
            if(checkOneLine.Checked)
                code = Regex.Replace(code, "\r\n", " ");
            
            Attribute.AtrributeValue = code;
        }

        private void checkOneLine_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAttribute();
        }
    }
}
