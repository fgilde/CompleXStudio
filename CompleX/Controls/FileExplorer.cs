//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Settings;
using DevExpress.XtraEditors.Controls;

namespace CompleX.Controls
{
    public partial class FileExplorer : UserControl
    {

        /// <summary>
        /// Selects a path or file in the explorer.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public bool SelectPath(string path)
        {
              return this.CheckInvoke(() =>
                     {
                         DirectoryEdit.Text = path;
                         var startNode =
                             treeMain.Nodes.Find(Resources.Computer, false).FirstOrDefault();
                         if (startNode != null) startNode.Expand();
                         if (!File.Exists(path) && !Directory.Exists(path))
                             return false;
                         var strings = path.Split('\\');
                         foreach (string s in strings)
                         {
                             if (startNode != null)
                                 startNode = startNode.Nodes.Find(s, false).FirstOrDefault();
                             if (startNode != null)
                             {
                                 EnumerateDirectory(startNode);
                                 startNode.Expand();
                                 treeMain.SelectedNode = startNode;
                             }
                         }
                         return true;
                    });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileExplorer"/> class.
        /// </summary>
        public FileExplorer()
        {
            InitializeComponent();
            contextMenuStripFolder.Renderer = new Office2007Renderer.Office2007Renderer();
            FilleDirectoryMenu();
        }

        private void FilleDirectoryMenu()
        {
            Array values = Enum.GetValues(typeof(Environment.SpecialFolder));
            foreach (Environment.SpecialFolder folder in values)
            {
                Environment.SpecialFolder specialFolder = folder;
                otherToolStripMenuItem.DropDownItems.Add(folder.ToString(),null,(sender,args) =>
                                                                                    {
                                                                                        string path = Environment.GetFolderPath(specialFolder);
                                                                                        SelectPath(path);
                                                                                    });
            }
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
       public void Init()
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                this.CheckInvoke(() =>
                {
                    treeMain.Nodes.Clear();
                    var rootDesktop = new TreeNode(Resources.Desktop, 2, 2);
                    treeMain.Nodes.Add(rootDesktop);
                    rootDesktop.Name = Resources.Desktop;
                    rootDesktop.Nodes.Add("");
                    var myComputer = new TreeNode(Resources.Computer, 4, 4) { Name = Resources.Computer };
                    treeMain.Nodes.Add(myComputer);
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        string name = drive.Name;
                        if (name.EndsWith("\\"))
                            name = name.Remove(name.Length - 1, 1);
                        var driveNode = new TreeNode(name) { Name = name };
                        switch (drive.DriveType)
                        {
                            case DriveType.CDRom:
                                driveNode.SelectedImageIndex = 1;
                                driveNode.ImageIndex = 1;
                                break;
                            case DriveType.Network:
                                driveNode.SelectedImageIndex = 5;
                                driveNode.ImageIndex = 5;
                                break;
                            case DriveType.Removable:
                                driveNode.SelectedImageIndex = 0;
                                driveNode.ImageIndex = 0;
                                break;
                            default:
                                driveNode.SelectedImageIndex = 3;
                                driveNode.ImageIndex = 3;
                                break;
                        }
                        driveNode.Nodes.Add("");
                        myComputer.Nodes.Add(driveNode);
                    }


                });
            });
            treeMain.BeforeExpand += TreeMainBeforeExpand;
            treeMain.MouseUp += TreeMainMouseUp;
            DirectoryEdit.ButtonClick += DirectoryEditButtonClick;
            DirectoryEdit.KeyDown += DirectoryEditOnKeyKeyDown;
            FilterEdit.ButtonClick += ApplyFilterClick;
       }

        private void ApplyFilterClick(object sender, ButtonPressedEventArgs args)
        {
            string path = DirectoryEdit.Text;
            if(!String.IsNullOrEmpty(path) && File.Exists(path) && !Directory.Exists(path))
                path = Path.GetDirectoryName(path);
            if (!String.IsNullOrEmpty(path))
                path = path.AddDirectorySeparatorChar();
            treeMain.CollapseAll();
            SelectPath(path);
        }

        private void DirectoryEditOnKeyKeyDown(object sender, KeyEventArgs args)
        {
            if(args.KeyCode.Equals(Keys.Enter))
            {
               
                string path = DirectoryEdit.Text;
                SelectPath(path);
                args.Handled = true;
            }
        }

        private void DirectoryEditButtonClick(object sender, ButtonPressedEventArgs args)
        {
           if(args.Button.Index == 0)
           {
               var dlg = new OpenFileDialog();
               if(dlg.ShowDialog() == DialogResult.OK)
               {
                   SelectPath(dlg.FileName);
               }
           }else
           {
               SelectPath(DirectoryEdit.Text);
           }
        }


        void TreeMainMouseUp(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                treeMain.SelectedNode = treeMain.GetNodeAt(e.X, e.Y);
                // Only files Files selected?
                if (treeMain.SelectedNodes.Count > 0 && treeMain.SelectedNodes.ToArray().Where(o => ((TreeNode)o).Tag == null).Count() == 0)
                {
                    var files = (from TreeNode node in treeMain.SelectedNodes where node.Tag != null select node.Tag.ToString()).ToList();

                    if (ModifierKeys == Keys.Control)
                    {
                        MenuService.ShowShellContextMenu(files.ToArray());
                    }else
                    {
                        contextMenuStripFolder.Items.Remove(renameToolStripMenuItem);
                        contextMenuStripFolder.Items.Remove(deleteToolStripMenuItem);
                        contextMenuStripFolder.Items.Remove(toolStripMenuItemSplitter1);
                        MenuService.ShowDefaultFileContextMenu(contextMenuStripFolder, files.ToArray());
                    }
                }
                else // Directory
                {
                    if (treeMain.SelectedNode != null)
                    {
                        string directory = GetFolderPath(treeMain.SelectedNode);
                        if (Directory.Exists(directory) && ModifierKeys == Keys.Control)
                        {
                            try
                            {
                                var ctxMnu = new ShellContextMenu();
                                var dir = new DirectoryInfo[1];
                                dir[0] = new DirectoryInfo(directory);
                                ctxMnu.ShowContextMenu(dir, PointToScreen(new Point(e.X, e.Y)));
                            }
                            catch (Exception ex)
                            {
                                CompleX_Studio.MessageLog.LogException(ex);
                            }
                        }else
                        {
                            contextMenuStripFolder.Items.Add(toolStripMenuItemSplitter1);
                            contextMenuStripFolder.Items.Add(renameToolStripMenuItem);
                            contextMenuStripFolder.Items.Add(deleteToolStripMenuItem);
                            contextMenuStripFolder.Show(PointToScreen(new Point(e.X, e.Y)));
                        }
                    }
                }
            }
        }

        private void TreeMainBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text != Resources.Computer)
            {
                EnumerateDirectory(e.Node);
            }
        }

        private void EnumerateDirectory(TreeNode parentNode)
        {
            using (new WaitingCursor(this))
            {
                string path = GetFolderPath(parentNode);
                if (Directory.Exists(path))
                {
                    parentNode.Nodes.Clear();

                    var dirInfo = new DirectoryInfo(path);
                    DirectoryInfo[] dirs = dirInfo.GetDirectories();
                    Array.Sort(dirs, new DirectorySorter());
                    foreach (DirectoryInfo dirI in dirs)
                    {
                        var node = new TreeNode(dirI.Name, 6, 6) {Name = dirI.Name};
                        node.Nodes.Add("");
                        parentNode.Nodes.Add(node);
                    }

                    FileInfo[] files;
                    if (FilterEdit.Text.StartsWith("*.") && FilterEdit.Text != @"*.*")
                        files = dirInfo.GetFiles(FilterEdit.Text);
                    else
                        files = dirInfo.GetFiles();

                    Array.Sort(files, new FileSorter());
                    foreach (FileInfo file in files)
                    {
                        var node = new TreeNode(file.Name, 6, 6) {Name = file.Name};

                        imgMain.Images.Add(ImageFunctions.GetFileIcon(file.FullName, false));

                        int imgIndex = imgMain.Images.Count - 1;
                        node.Tag = file.FullName;
                        node.ImageIndex = imgIndex;
                        node.SelectedImageIndex = imgIndex;
                        parentNode.Nodes.Add(node);
                    }
                }
            }
        }

        private static string GetFolderPath(TreeNode parentNode)
        {
            string path = "";
            string[] pathSplit = parentNode.FullPath.Split('\\');
            if (pathSplit[0] == Resources.Desktop)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                for (int a = 1; a < pathSplit.Length; a++)
                {
                    if (pathSplit[a].Trim() != string.Empty)
                    {
                        path += pathSplit[a] + "\\";
                    }
                }
            }
            else
            {
                for (int a = 1; a < pathSplit.Length; a++)
                {
                    if (pathSplit[a].Trim() != string.Empty)
                    {
                        path += pathSplit[a] + "\\";
                    }
                }

            }
            if (path.EndsWith("\\") && !Directory.Exists(path) && File.Exists(path.Substring(0, path.Length - 1)))
            {
                path = path.Substring(0, path.Length - 1);
            }
 
            return path;
        }


        private void TreeMainAfterSelect(object sender, TreeViewEventArgs e)
        {
            SaveLastDirectory(e.Node);
            DirectoryEdit.Text = GetFolderPath(e.Node);
        }

        private static void SaveLastDirectory(TreeNode node)
        {
            try
            {
                string path = GetFolderPath(node);
                if (!String.IsNullOrEmpty(path))
                {
                    Settings.Default.LastFileExplorerPath = path;
                    Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
               CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private void TreeMainAfterExpand(object sender, TreeViewEventArgs e)
        {
            SaveLastDirectory(e.Node);
        }

        private void TreeMainDoubleClick(object sender, EventArgs e)
        {
            CheckAndOpenFile();
        }



        private void CheckAndOpenFile()
        {
            var files = (from TreeNode node in treeMain.SelectedNodes where node.Tag != null select node.Tag.ToString()).ToList();
            FileService.LoadFiles(files);
        }

        private void BarButtonItem1ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(!String.IsNullOrEmpty(CompleX_Studio.CurrentFile))
            {
                SelectPath(CompleX_Studio.CurrentFile);
            }
        }

        private void BarButtonItem2ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileSystemUtils.ShowProperties(GetFolderPath(treeMain.SelectedNode));
        }

        private void FilterEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                ApplyFilterClick(sender,new ButtonPressedEventArgs(FilterEdit.Properties.Buttons[0]));
        }

        private void WindowsDirectoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            var info = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System));
            if (info.Parent != null)
            {
                string path = info.Parent.FullName;
                SelectPath(path);
            }
        }

        private void SystemDirectoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
            SelectPath(path);
        }

        private void ProgramfilesToolStripMenuItemClick(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            SelectPath(path);
        }

        private void ApplicationDataToolStripMenuItemClick(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            SelectPath(path);
        }

        private void OwnFilesToolStripMenuItemClick(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            SelectPath(path);
        }

        private void TreeMainAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            
            string fileNameOld = e.Node.Tag != null ? e.Node.Tag.ToString():GetFolderPath(e.Node);

            string fileNameNew = String.Empty;
            if (File.Exists(fileNameOld))
            {
                fileNameNew = Path.GetDirectoryName(fileNameOld).AddDirectorySeparatorChar() + e.Label;
            }else if (Directory.Exists(fileNameOld))
            {
                var info = new DirectoryInfo(fileNameOld);
                if (info.Parent != null)
                {
                    fileNameNew = info.Parent.Name + e.Label;
                }else
                {
                    e.CancelEdit = true;
                    return;
                }
            }
            e.CancelEdit = String.IsNullOrEmpty(e.Label);
            if (!e.CancelEdit && !String.IsNullOrEmpty(e.Label))
            {
                if (File.Exists(fileNameOld))
                {
                    if (
                        MessageService.AskDsaOnYes(
                            String.Format(Resources.Confirm_Rename, Path.GetFileName(fileNameOld),
                                          Path.GetFileName(fileNameNew)), Text, @"RenameLabelEditNotification"))
                    {
                        FileService.RenameFile(fileNameOld, fileNameNew);
                        e.Node.Tag = fileNameNew;
                    }
                    else
                    {
                        e.CancelEdit = true;
                    }
                }
                else if(Directory.Exists(fileNameOld))
                {
                    if (
                        MessageService.AskDsaOnYes(
                            String.Format(Resources.Confirm_Rename, Path.GetFileName(fileNameOld.RemoveDirectorySeparatorChar()),
                                          Path.GetFileName(fileNameNew)), Text, @"RenameLabelEditNotification"))
                    {
                        Directory.Move(fileNameOld, fileNameNew);
                    }
                    else
                    {
                        e.CancelEdit = true;
                    } 
                }
            }
        }

        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            try
            {
                if (!MessageService.AskDsaOnYes(Resources.DeleteShowConfirmation, Resources.Delete, "DELETEFILEONFILEEXPLORER"))
                    return;
                var nodes = treeMain.SelectedNodes;
                foreach(TreeNode node in nodes)
                {
                    if (node.Tag != null ) // File
                    {
                        string fileName = node.Tag.ToString();
                        if(File.Exists(fileName))
                            File.Delete(fileName);
                    }else
                    {
                        string directory = GetFolderPath(node);
                        if(Directory.Exists(directory))
                            Directory.Delete(directory,true);
                    }
                    treeMain.Nodes.Remove(node);
                }
            }
            catch(InvalidOperationException)
            {}
            catch (Exception e)
            {
               CompleX_Studio.MessageLog.LogException(e);
            }
        }

        private void RenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            treeMain.SelectedNode.BeginEdit();
        }

        private void TreeMainKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && treeMain.Focused)
            {
                CheckAndOpenFile();
            }

            if (e.KeyCode == Keys.F10 && ModifierKeys == Keys.Shift)
            {
                MenuService.ShowDefaultFileContextMenu(contextMenuStripFolder, (from TreeNode node in treeMain.SelectedNodes where node.Tag != null select node.Tag.ToString()).ToArray());
            }

            if (e.KeyCode == Keys.F2)
                treeMain.SelectedNode.BeginEdit();
            if (e.KeyCode == Keys.Delete)
                DeleteSelected();
        }

    }
}
