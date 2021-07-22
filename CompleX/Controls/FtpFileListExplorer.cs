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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Dialogs;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Types;
using CompleX_Types.Interfaces;
using KeyEventArgs=System.Windows.Forms.KeyEventArgs;
using MouseEventArgs=System.Windows.Forms.MouseEventArgs;

namespace CompleX.Controls
{
    public partial class FtpFileListExplorer : UserControl
    {
        public FtpFileListExplorer()
        {
            InitializeComponent();
            contextMenuStrip.Renderer = new Office2007Renderer.Office2007Renderer();
        }

        public string SelectedPath
        {
            get { return ftpExplorer1.SelectedPath; }
            set { ftpExplorer1.SelectedPath = value; }
        }

        public bool MultiSelect
        {
            get { return listViewFiles.MultiSelect; }
            set { listViewFiles.MultiSelect = value; }
        }

        public string FileName
        {
            get { return textEditFileName.Text; }
            set { textEditFileName.Text = value; }
        }

        public event FtpExplorer.ConnectionChangedHandler ConnectionChanged
        {
            add { ftpExplorer1.ConnectionChanged += value; }
            remove { ftpExplorer1.ConnectionChanged -= value; }
        }

        public new bool Enabled
        {
            get { return base.Enabled; }
            set 
            {
                base.Enabled = value;
                listViewFiles.Enabled = value;
            }
        }

        public FtpFileListExplorer(FtpConnection connection):this()
        {
            FtpConnection = connection;
        }

        public FtpFileListExplorer(IFtpSettings settings)
            : this()
        {
            FtpConnection = new FtpConnection(settings) { OutputWriter = new ComplexOutPutWriter() };
        }

        public FtpConnection FtpConnection
        {
            get { return ftpExplorer1.FtpConnection; }
            set { ftpExplorer1.FtpConnection = value; }
        }

        public IEnumerable<string> SelectedItems
        {
            get {
                return from ListViewItem item in listViewFiles.SelectedItems where item.Tag.GetType() == typeof(FtpFileInfo) select ftpExplorer1.SelectedPath + @"/" + item.Text;
            }
        }

        public ValidationResult IsValid()
        {
            if (SelectedItems == null || SelectedItems.Count() <= 0)
                return new ValidationResult { ErrorMessage = Resources.EmptyItem, Result = false };

            return new ValidationResult { ErrorMessage = String.Empty, Result = true };
        }


        /// <summary>
        /// Upload a file to current connection in corrent dir
        /// </summary>
        public void UploadFile(string fileName)
        {
            if (FtpConnection == null)
                throw new FtpException(-1, "NOT CONNECTED");
            if (listViewFiles.CheckInvoke(() => listViewFiles.Items.ContainsKey(Path.GetFileName(fileName))))
            {
                if (MessageService.AskDsa(String.Format(Resources.OverwriteFile, Path.GetFileName(fileName)),
                                          Resources.FileExists, "OVERWRITE_BECAUSEFILE_EXISTS",
                                          MessageIconType.Warning))
                    FtpConnection.UploadFile(fileName);
            }
            else
            {
                FtpConnection.UploadFile(fileName);
            }
        }

        private void FtpExplorer1AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateView();
        }

        private void FtpExplorer1AfterExpand(object sender, TreeViewEventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            this.CheckInvoke(() =>
            {
                using (new WaitingCursor(this))
                {
                    listViewFiles.Items.Clear();

                    foreach (FtpDirectoryInfo info in FtpConnection.GetDirectories())
                    {
                        var item = new ListViewItem(info.Name, 1) { Name = info.Name };
                        item.SubItems.Add(info.LastWriteTime.ToString());
                        item.SubItems.Add(Resources.Folder);
                        item.Tag = info;
                        listViewFiles.Items.Add(item);
                    }
                    FtpFileInfo[] files;
                    if (String.IsNullOrEmpty(comboBoxEditMask.Text) || comboBoxEditMask.Text == @"*.*")
                        files = FtpConnection.GetFiles();
                    else
                        files = FtpConnection.GetFiles(comboBoxEditMask.Text);
                    comboBoxEditMask.Properties.Items.Clear();
                    foreach (FtpFileInfo info in files)
                    {
                        int imageIndex = 0;

                        Icon icon = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(info.Name), false);
                        if (icon != null)
                        {
                            imageList.Images.Add(icon);
                            imageIndex = imageList.Images.Count - 1;
                        }

                        var item = new ListViewItem(info.Name, imageIndex) { Name = info.Name };
                        string s;
                        item.SubItems.Add(info.LastWriteTime.ToString());
                        item.SubItems.Add(FileHelper.GetFileDescriptionByExtension(Path.GetExtension(info.Name), out s));
                        item.Tag = info;
                        listViewFiles.Items.Add(item);
                        if (!comboBoxEditMask.Properties.Items.Contains("*" + Path.GetExtension(info.Name)))
                            comboBoxEditMask.Properties.Items.Add("*" + Path.GetExtension(info.Name));
                    }
                }
            });
        }

        private void ListViewFilesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFiles.SelectedItems.Count == 1 && listViewFiles.SelectedItems[0].Tag.GetType() == typeof(FtpDirectoryInfo))
            {
                ftpExplorer1.ChangeDirectory(ftpExplorer1.SelectedPath+@"/"+listViewFiles.SelectedItems[0].Text);
                UpdateView();
            }else
            {
                if (ParentForm is BaseDialog && ((BaseDialog)ParentForm).OnAccept != null)
                {
                    ((BaseDialog)ParentForm).OnAccept();
                    ParentForm.Close();
                }
                else
                {
                    using (new WaitingScope(WaitingDialogDescription.Default))
                    {
                        foreach (string item in SelectedItems)
                            FileService.OpenFileFromFtp(FtpConnection, item);
                    }
                }
            }
        }

        private void FtpExplorer1ConnectionChanged(FtpConnection connection)
        {
            listViewFiles.Items.Clear();
        }

        private bool inEditing;
        private void ListViewFilesKeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && (ParentForm == null || ParentForm.AcceptButton == null))
            {
                if (inEditing)
                {
                    inEditing = false;
                    return;
                }
                if (listViewFiles.SelectedItems.Count == 1 && listViewFiles.SelectedItems[0].Tag.GetType() == typeof(FtpDirectoryInfo))
                {
                    ftpExplorer1.ChangeDirectory(ftpExplorer1.SelectedPath + @"/" + listViewFiles.SelectedItems[0].Text);
                    UpdateView();
                }
                else
                {
                    if (ParentForm is BaseDialog && ((BaseDialog)ParentForm).OnAccept != null )
                    {
                        ((BaseDialog) ParentForm).OnAccept();
                        ParentForm.Close();
                    }
                    else
                    {
                        using (new WaitingScope(WaitingDialogDescription.Default))
                        {
                            foreach (string item in SelectedItems)
                                FileService.OpenFileFromFtp(FtpConnection, item);
                        }
                    }
                }
            }
            if(e.KeyCode == Keys.Delete)
            {
                DeleteSelectedFiles();
            }
            if (e.KeyCode == Keys.F2)
            {
                if (listViewFiles.SelectedItems.Count > 0)
                    listViewFiles.SelectedItems[0].BeginEdit();
            }
        }

        private void DeleteSelectedFiles()
        {
            if (!MessageService.AskDsaOnYes(Resources.DeleteShowConfirmation, Resources.Delete, "DELETEFILEONFTPFILELISTCONTROLVIEW"))
                return;
            int i = 1;
            using (new WaitingScope(WaitingDialogDescription.Default))
            {
                foreach (ListViewItem item in listViewFiles.SelectedItems)
                {
                    StatusBarService.SetProgress("Deleting " + item.Text, i, listViewFiles.SelectedItems.Count);
                    if(item.Tag.GetType() == typeof(FtpFileInfo))
                        FtpConnection.RemoveFile(ftpExplorer1.SelectedPath + @"/" + item.Text);
                    else if(item.Tag.GetType() == typeof(FtpDirectoryInfo))
                        FtpConnection.RemoveDirectory(ftpExplorer1.SelectedPath + @"/" + item.Text, true);
                    i++;
                }
            }
            UpdateView();
        }

        private void ListViewFilesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            inEditing = true;
            string nameOld = listViewFiles.SelectedItems[0].Text;
            string nameNew = e.Label;
            if (nameNew != null && !nameNew.Equals(nameOld))
            {
                FtpConnection.RenameFile(ftpExplorer1.SelectedPath + @"/" + nameOld, nameNew);
            }
            else
            {
                e.CancelEdit = true;
            }
        }
        private void ListViewFilesDragDrop(object sender, DragEventArgs e)
        {
            // Dateien werden vom lokalen system in die listView gezogen
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var worker = new WorkerThread();
                worker.SetDialogDescriptions(WaitingDialogDescription.DefaultWithBorder);
                worker.IsCancelable = true;
                worker.IsIndeterminate = true;
                worker.RunWorkerAsync(args =>
                                          {
                                              var dragList = (string[]) e.Data.GetData(DataFormats.FileDrop);

                                              int i = 1;
                                              foreach (var s in dragList)
                                              {
                                                  if(args.Cancel)
                                                      return;
                                                  string statusText = "Uploading " + Path.GetFileName(s);
                                                  StatusBarService.SetProgress(statusText, i, dragList.Count());
                                                  args.Worker.MainText =statusText;
                                                  if (File.Exists(s))
                                                  {
                                                      if(!args.Cancel)
                                                        UploadFile(s);
                                                  }
                                                  else
                                                  {
                                                      if (Directory.Exists(s) && !args.Cancel)
                                                          FtpConnection.UploadDirectory(s.AddDirectorySeparatorChar(),
                                                                                        FtpConnection.
                                                                                            GetCurrentDirectory());
                                                  }

                                                  i++;
                                              }
                                              UpdateView();
                                          });

            }
            // Wenn keine Datein vom Lokalen system gedragt werden, prüfen ob aus anderer Ftp verbindung items gedragt werden
            else
            {
                var dragedFtpFiles = e.Data.GetData(typeof(DragFtpFiles)) as DragFtpFiles;
                // Wenn etwas von einem FtpFileListExplorer zu einem anderem geschoben wird, und sich entweder die verbindungen unterscheiden oder ein anderer Ordner benutzt wird
                if (dragedFtpFiles != null && (!dragedFtpFiles.Connection.Equals(FtpConnection) || dragedFtpFiles.Connection.GetCurrentDirectory() != FtpConnection.GetCurrentDirectory()))
                {
                    using (new WaitingCursor(this))
                    {
                        int i = 1;
                        string tempDir = FileService.GetTempUniqueDir();
                        foreach (var s in dragedFtpFiles.Files)
                        {
                            StatusBarService.SetProgress("Uploading " + s, i, dragedFtpFiles.Files.Count());
                            //Download from connection 1
                            string tempFile = tempDir + Path.GetFileName(s);
                            dragedFtpFiles.Connection.DownloadFile(s,tempFile,false);
                            //Upload to connection 2
                            if (File.Exists(tempFile))
                                UploadFile(tempFile);

                            i++;
                        }
                        UpdateView();
                    }
                
                }
            }
        }


        private void ListViewFilesDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
            // Wenn keine Datein vom Lokalen system gedragt werden, prüfen ob aus anderer Ftp verbindung items gedragt werden
            if (e.Effect == DragDropEffects.None && e.Data.GetData(typeof(DragFtpFiles)) is DragFtpFiles)
                e.Effect = DragDropEffects.Copy;
        }

        private void ListViewFilesItemDrag(object sender, ItemDragEventArgs e)
        {
            if (SelectedItems.Count() > 0)
            {
                var dragItem = new DragFtpFiles(FtpConnection, SelectedItems);
                listViewFiles.DoDragDrop(dragItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void ContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            selectAllToolStripMenuItem.Visible = MultiSelect;
            menuSplitter1.Visible = MultiSelect;
            downloadToolStripMenuItem.Enabled = listViewFiles.SelectedItems.Count > 0;
            renameToolStripMenuItem.Enabled = listViewFiles.SelectedItems.Count > 0;
            deleteToolStripMenuItem.Enabled = listViewFiles.SelectedItems.Count > 0;
            selectAllToolStripMenuItem.Enabled = listViewFiles.Items.Count > 0;
        }

        private void DownloadToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Wenn nur ein eintrag ausgewählt ist, der eine datei da stellt, 
            if (listViewFiles.SelectedItems.Count == 1 && listViewFiles.SelectedItems[0].Tag.GetType() == typeof(FtpFileInfo))
            {
                var item = listViewFiles.SelectedItems[0];
                if (item != null)
                {
                    var dlg = new SaveFileDialog { FileName = item.Text, DefaultExt = Path.GetExtension(item.Text) };
                    if (dlg.ShowDialog() == DialogResult.OK)
                        FtpConnection.GetFile(ftpExplorer1.SelectedPath + @"/" + item.Text, dlg.FileName, false);
                }

            }
            else if (listViewFiles.SelectedItems.Count > 0)
            {
                var dlg = new FolderBrowserDialog();
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    Enabled = false;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            int count = listViewFiles.CheckInvoke(() => listViewFiles.SelectedItems.Count);
                            for (int i = 0; i < count; i++)
                            {
                                int i1 = i;
                                ListViewItem item = listViewFiles.CheckInvoke(() =>listViewFiles.SelectedItems[i1]);
                                StatusBarService.SetProgress("Downloading " + item.Text, i, count - 1);
                                if (item.Tag.GetType() == typeof(FtpFileInfo))
                                {
                                    FtpConnection.GetFile(ftpExplorer1.CheckInvoke(() => ftpExplorer1.SelectedPath) + @"/" + item.Text,
                                                          dlg.SelectedPath.AddDirectorySeparatorChar() + item.Text, false);
                                }
                                else if (item.Tag.GetType() == typeof(FtpDirectoryInfo))
                                {
                                    string dir = dlg.SelectedPath.AddDirectorySeparatorChar() + item.Text;
                                    if (!Directory.Exists(dir))
                                        Directory.CreateDirectory(dir);
                                    FtpConnection.DownloadDirectory(dir, ftpExplorer1.CheckInvoke(() => ftpExplorer1.SelectedPath) + @"/" + item.Text);
                                }
                            }
                        }
                        finally
                        {
                            BeginInvoke(new MethodInvoker(() => { Enabled = true; }));
                        }
                    });                   
                }
            }
        }

        private void SelectAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewFiles.Items)
                item.Selected = true;
        }

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteSelectedFiles();
        }

        private void RenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            inEditing = true;
            foreach (ListViewItem item in listViewFiles.SelectedItems)
            {
                string newName = InputDlg.Execute(Resources.Name, Resources.Name, item.Text);
                if (!String.IsNullOrEmpty(newName) && newName != item.Text)
                    FtpConnection.RenameFile(ftpExplorer1.SelectedPath+@"/"+item.Text, newName);
            }
            UpdateView();
        }

        private void FtpExplorer1FolderCreated(string folderName, string fullPath)
        {
            UpdateView();
        }

        private void ListViewFilesSelectedIndexChanged(object sender, EventArgs e)
        {
            textEditFileName.Text = String.Empty;
            if (listViewFiles.SelectedItems.Count > 0 && listViewFiles.SelectedItems[0] != null)
                textEditFileName.Text = listViewFiles.SelectedItems[0].Text;
        }

        private void ComboBoxEditMaskEditValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }


    }


    public class DragFtpFiles
    {
        public IEnumerable<string> Files { get; set; }
        public FtpConnection Connection { get; set; }

        public DragFtpFiles(FtpConnection connection)
        {
            Connection = connection;
        }

        public DragFtpFiles(FtpConnection connection, IEnumerable<string> files)
        {
            Connection = connection;
            Files = files;
        }
    }

}
