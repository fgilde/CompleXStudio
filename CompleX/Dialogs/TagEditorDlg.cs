//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Drawing;
using CompleX_Types;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class TagEditorDlg : XtraForm
    {
        public TagEditorDlg(Tag tag)
        {
            InitializeComponent();
            this.Text += " - "+tag.TagName;
            tagEditControl.Tag = tag;
            tagEditControl.Init();
            if (tagEditControl.MinimumSize.Height > 0 && tagEditControl.MinimumSize.Width > 0)
            {
                var minSize = new Size(tagEditControl.MinimumSize.Width, tagEditControl.MinimumSize.Height + 80);
                MinimumSize = minSize;
            }
        }
    }
}
