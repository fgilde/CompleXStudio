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
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using CompleX.Services;
using CompleX_Settings;
using CompleX_Types;
using CompleX_Library.Interfaces;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class ToolsOptionPage : BaseOptionPage
    {
        private List<string> directories;
        private readonly ImageListBox listBoxDirectories;
        public ToolsOptionPage()
        {
            InitializeComponent();
            directories = null;
            listBoxDirectories = new ImageListBox();
            panel1.Controls.Add(listBoxDirectories);
            listBoxDirectories.Dock = DockStyle.Fill;
            listBoxDirectories.ImageList = imgMain;
        }

        public override Guid ID
        {
            get
            {
                return new Guid("0747FD59-95A0-4C31-98F2-995198B80198");
            }
        }

        public override void OnCancel()
        {
            directories = null;
        }

        public override bool OnOk()
        {
            Settings.Set("ToolsDirectoryList",directories);
            directories = null;
            MenuService.UpdateToolsMenu();
            return true;
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
           if(activated && directories == null)
           {
               directories = Settings.Get<List<string>>("ToolsDirectoryList") ?? new List<string>();
               UpdateList();
           }
        }

        public override Image Image
        {
            get { return Images.dateiEinstellungen_16; }
        }

        public override bool AllowedAsProjectOptions
        {
            get
            {
                return false;
            }
        }

        public override string PageID
        {
            get { return "ToolsOptions"; }
        }

        public override string ParentPageID
        {
            get
            {
                return PageID;
            }
        }

        public override string PageTitle
        {
            get
            {
                return "Tools";
            }
        }

        private void UpdateList()
        {
            listBoxDirectories.BeginUpdate();
            listBoxDirectories.Items.Clear();
            foreach (var dir in directories)
                listBoxDirectories.Items.Add(new ImageListBoxItem(dir, 0));
            
            listBoxDirectories.EndUpdate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                directories.Add(dlg.SelectedPath);
                UpdateList();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDirectories.SelectedItem != null && listBoxDirectories.SelectedIndex >= 0)
            {
                directories.RemoveAt(listBoxDirectories.SelectedIndex);
                UpdateList();
            }
        }

    }
}
