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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class PromptSaveDialog : XtraForm
    {
        private readonly List<MainEditForm> notSavedEditors;
        public PromptSaveDialog()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            notSavedEditors = FileService.OpenForms.OfType<MainEditForm>().Where(form => !form.IsSaved).ToList();
            Init();
        }

        public PromptSaveDialog(IEnumerable<MainEditForm> files)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            notSavedEditors = files.ToList();
            Init();
        }

        private void Init()
        {
            foreach (var editForm in notSavedEditors)
            {
                int imageIndex = 3;
                if(File.Exists(editForm.FileName))
                {
                    imageList.Images.Add(ImageFunctions.GetFileIcon(editForm.FileName, false));
                    imageListLarge.Images.Add(editForm.FileName.GetAssociatedIcon());
                    imageIndex = imageList.Images.Count - 1;
                }else
                {
                    string ext = Path.GetExtension(editForm.FileName);
                    if(!String.IsNullOrEmpty(ext))
                    {
                        var icosmall = ImageFunctions.GetAssociatedIconByExtension(ext, false);
                        var icobig = ImageFunctions.GetAssociatedIconByExtension(ext, true);
                        if (icosmall != null && icobig != null)
                        {
                            imageList.Images.Add(icosmall);
                            imageListLarge.Images.Add(icobig);
                        }
                        imageIndex = imageList.Images.Count - 1;
                    }
                }
                listViewFiles.Items.Add(editForm.FileName, imageIndex);
            }
        }

        private void BtnSaveSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewFiles.Items.Count; i++)
            {
               if(listViewFiles.Items[i].Checked)
               {
                   MainEditForm editForm= notSavedEditors[i];
                   IContentEdit editor = editForm.EditControl;
                   if (editForm.ActiveEditMode == EditMode.Designer)
                   {
                       if (editForm.Designer != null)
                           editor = editForm.Designer;
                   }
                   FileService.SaveFile(editor);
               }
            }
            DialogResult = DialogResult.OK;
        }

        private void listViewFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(barManager, Cursor.Position);                          
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < listViewFiles.Items.Count; i++)
                listViewFiles.Items[i].Checked = true;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < listViewFiles.Items.Count; i++)
                listViewFiles.Items[i].Checked = false;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < listViewFiles.Items.Count; i++)
                listViewFiles.Items[i].Checked = !listViewFiles.Items[i].Checked;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listViewFiles.View = View.List;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listViewFiles.View = View.SmallIcon;
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listViewFiles.View = View.LargeIcon;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listViewFiles.View = View.LargeIcon;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            listViewFiles.View = View.List;
        }


    }
}
