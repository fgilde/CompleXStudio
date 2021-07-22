//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CompleX.Helper;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class SelectStringDialog : DevExpress.XtraEditors.XtraForm
    {
        public string SelectedText
        {
            get
            {
                return memoEdit1.Text;
            }
        }
        public SelectStringDialog(IEnumerable<string> stringlist):this(stringlist,true)
        {
        }

        public SelectStringDialog(IEnumerable<string> stringlist,bool preview)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            foreach (string s in stringlist)
                dataSetClipboard.Clipboard.AddClipboardRow(s);
            if(!preview)
            {
                Height -= memoEdit1.Height;
                splitContainerControl.PanelVisibility = SplitPanelVisibility.Panel1;
            }
                  
        }

        private void UpdateMemo()
        {
           memoEdit1.Text = gridView1.GetFocusedDataRowItemText(1);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            UpdateMemo();
        }

        private void clipboardGridControl_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}