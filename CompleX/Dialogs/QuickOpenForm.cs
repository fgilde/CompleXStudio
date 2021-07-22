//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CompleX.Services;
using DevExpress.XtraEditors;
using CompleX_Library.Helper;

namespace CompleX.Dialogs
{
    public partial class QuickOpenForm : XtraForm
    {
        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public string Directory
        {
            get
            {
                return textBoxDirectory.Text.AddDirectorySeparatorChar();
            }
            set
            {
                if (System.IO.Directory.Exists(value))
                {
                    textBoxFileName.AutoCompleteCustomSource.Clear();
                    foreach (string file in System.IO.Directory.GetFiles(value))
                    {
                        textBoxFileName.AutoCompleteCustomSource.Add(Path.GetFileName(file));
                    }
                    textBoxDirectory.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get
            {
                return Directory+textBoxFileName.Text;
            }
            set
            {
                Directory = Path.GetDirectoryName(value);
                textBoxFileName.Text = Path.GetFileName(value);
            }
        }

        public QuickOpenForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Executes the dialog with specified directory.
        /// </summary>
        public static void Execute(string directory, Action<string> fileFound)
        {
            var dialog = new QuickOpenForm {Directory = directory};
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                    fileFound(dialog.FileName);
                if (dialog.checkBoxSubDirs.Checked || Path.GetFileName(dialog.FileName).StartsWith("*."))
                {
                    FileHelper.FindFile(dialog.Directory, Path.GetFileName(dialog.FileName), fileFound, dialog.checkBoxSubDirs.Checked);  
                }
            }
        }

        /// <summary>
        /// Executes the dialog with specified directory.
        /// </summary>
        public static string Execute(string directory)
        {
            var dialog = new QuickOpenForm { Directory = directory };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                    return dialog.FileName;
                if (dialog.checkBoxSubDirs.Checked)
                {
                    return FindFile(dialog.Directory, Path.GetFileName(dialog.FileName));
                }
            }
            return string.Empty;
        }

        private void SimpleButtonSearchClick(object sender, EventArgs e)
        {
            var dl = FileService.GetOpenFileDialog(Directory);
            if(dl.ShowDialog() == DialogResult.OK)
                FileName = dl.FileName;  
        }

        private static string FindFile(string directory, string filename)
        {
            string result = String.Empty;
            var evt = new AutoResetEvent(false);
            FileHelper.FindFile(directory, filename, s =>
                                                         {
                                                             evt.Set();
                                                             result = s;
                                                         },true);
            evt.WaitOne();
            return result;
        }


    }
}