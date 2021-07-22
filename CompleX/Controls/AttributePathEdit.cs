using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX.Services;
using CompleX_Settings.Constants;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class AttributePathEdit : UserControl,IAttributeEdit
    {
        public AttributePathEdit()
        {
            InitializeComponent();
        }
        public TagAttribute Attribute { get; set; }
        public void Init()
        {
            buttonEdit1.Text = Attribute.AtrributeValue;
            if (CompleX_Studio.CurrentContentEditor != null && CompleX_Studio.CurrentContentEditor.Content is string)
                FillLinks();
                
            
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var fdlg = new OpenFileDialog {FileName = buttonEdit1.Text};
            if(fdlg.ShowDialog() == DialogResult.OK)
               buttonEdit1.Text = fdlg.FileName.ToRelativePathOrUri();
        }

        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Attribute.AtrributeValue = buttonEdit1.Text;
        }

        private void FillLinks()
        {
            //insert links
            comboBoxEditLinks.Properties.Items.Clear();
            var links = ContentService.GetUsedHtmlHrefs();
            foreach (var link in links)
                    comboBoxEditLinks.Properties.Items.Add(link);

            if (File.Exists(CompleX_Studio.CurrentFile) && Directory.Exists(Path.GetDirectoryName(CompleX_Studio.CurrentFile)))
            {
                string[] files = Directory.GetFiles(Path.GetDirectoryName(CompleX_Studio.CurrentFile));
                foreach (var file in files)
                    if (!comboBoxEditLinks.Properties.Items.Contains(Path.GetFileName(file)))
                        comboBoxEditLinks.Properties.Items.Add(Path.GetFileName(file));
            }

        }

        private void comboBoxEditLinks_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonEdit1.Text = comboBoxEditLinks.SelectedItem.ToString();
        }

    }
}
