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
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CompleX.Presentation.Controls.Extensions;
using DevExpress.XtraEditors;
using Microsoft.Win32;

namespace CompleX.Dialogs
{
    public partial class AddDirectoryDialog : XtraForm
    {
        private readonly List<string> history;

        public AddDirectoryDialog()
        {
            InitializeComponent();

            history = CompleX_Settings.Settings.Get(@"History_AddDirectory" + Name, Enumerable.Empty<string>().ToList());
            if(history.Count() > 0 )
                textBoxDirectory.Properties.Items.AddRange(history.Where(System.IO.Directory.Exists).ToArray());             
            
            ThreadPool.QueueUserWorkItem(o => AddPossibleExtensions());
        }

        public string Filter
        {
            get
            {
                string result = textBoxFilter.Text != @"*.*" ? textBoxFilter.Text : String.Empty;
                if (!String.IsNullOrEmpty(result) && !result.StartsWith("*"))
                    result = "*" + result;
                return result;
            }set
            {
                textBoxFilter.Text = value;
            }
        }

        public bool IncludeSubDirectories
        {
            get
            {
                return checkBoxSubDirs.Checked;
            }
            set
            {
                checkBoxSubDirs.Checked = value;
            }
        }

        public string Directory
        {
            get
            {
                return textBoxDirectory.Text;
            }set
            {
                textBoxDirectory.Text = value;
            }
        }

        private void AddPossibleExtensions()
        {
            RegistryKey root = Registry.ClassesRoot;
            string[] subKeys = root.GetSubKeyNames();
            var extensions = subKeys.Where(s => s.StartsWith(".")).ToArray();
            this.CheckInvoke(() =>
            {
                textBoxFilter.AutoCompleteCustomSource.Clear();
                textBoxFilter.AutoCompleteCustomSource.AddRange(extensions);
            });
        }

        private void SimpleButtonSearchClick(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog {ShowNewFolderButton = false};
            if(dlg.ShowDialog() == DialogResult.OK)
                Directory = dlg.SelectedPath;           
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(Directory))
            {
                if(!history.Contains(Directory))
                    history.Add(Directory);
                CompleX_Settings.Settings.Set(@"History_AddDirectory" + Name, history);

                DialogResult = DialogResult.OK;
            }
            else
            {
                textBoxDirectory.Focus();
                textBoxDirectory.BackColor = Color.Red;
                var timer = new System.Timers.Timer(500);
                timer.Elapsed += ((o, args) =>
                {
                    timer.Stop();
                    textBoxDirectory.CheckInvoke(() => textBoxDirectory.BackColor = Color.White);
                });
                timer.Start();
            }
        }
    }
}
