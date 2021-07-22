using System;
using System.IO;
using System.Windows.Forms;
using CompleX.Helper;
using CompleX_Types;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class TagSelectorDlg : XtraForm
    {
        public TagSelectorDlg()
        {
            InitializeComponent();

            tagSelector1.SelectedTagChange += (UpdateHelpInformation);
            
        }

        void UpdateHelpInformation(Tag tag)
        {   
            if (File.Exists(tag.Helpfile))
                helpBrowser.Navigate(tag.Helpfile);            
            else
                helpBrowser.Navigate("about:blank");  
        }

        private void tagSelector1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public static Tag Execute()
        {
           TagSelectorDlg dlg = new TagSelectorDlg();
           dlg.ShowDialog();
           if (dlg.DialogResult == DialogResult.OK)
               return dlg.tagSelector1.SelectedTag;
            return null;
        }

        public static bool Execute(out Tag tag)
        {
            tag = Execute();
            if(tag != null)
                return true;
            return false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (splitContainerControl.PanelVisibility == SplitPanelVisibility.Both)
                splitContainerControl.PanelVisibility = SplitPanelVisibility.Panel1;
            else
                splitContainerControl.PanelVisibility = SplitPanelVisibility.Both;     
        }

    }
}
