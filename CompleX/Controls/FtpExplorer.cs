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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Dialogs;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Settings;
using CompleX_Types;
using CompleX_Types.Interfaces;
using DevExpress.XtraBars;

namespace CompleX.Controls
{
    public partial class FtpExplorer : UserControl
    {
        private string startPath;
        private readonly Dictionary<string, int> imageFileAssociation;
        private const int FolderTag = 0;
        private const int FileTag = 1;

        #region Events
        public delegate void ConnectionChangedHandler(FtpConnection connection);
        public delegate void FolderCreatedHandler(string folderName, string fullPath);

        public event ConnectionChangedHandler ConnectionChanged;
        public event FolderCreatedHandler FolderCreated;

        private void InvokeFolderCreated(string folderName, string fullPath)
        {
            FolderCreatedHandler created = FolderCreated;
            if (created != null) created(folderName, fullPath);
        }

        public event EventHandler Confirmed;


        public new event EventHandler ContextMenuChanged
        {
            add { treeMain.ContextMenuChanged += value; }
            remove { treeMain.ContextMenuChanged -= value; }
        }

        public new event EventHandler DoubleClick
        {
            add { treeMain.DoubleClick += value; }
            remove { treeMain.DoubleClick -= value; }
        }

        public new event EventHandler Enter
        {
            add { treeMain.Enter += value; }
            remove { treeMain.Enter -= value; }
        }

        public new event EventHandler GotFocus
        {
            add { treeMain.GotFocus += value; }
            remove { treeMain.GotFocus -= value; }
        }

        public new event KeyEventHandler KeyDown
        {
            add { treeMain.KeyDown += value; }
            remove { treeMain.KeyDown -= value; }
        }

        public new event KeyPressEventHandler KeyPress
        {
            add { treeMain.KeyPress += value; }
            remove { treeMain.KeyPress -= value; }
        }

        public new event KeyEventHandler KeyUp
        {
            add { treeMain.KeyUp += value; }
            remove { treeMain.KeyUp -= value; }
        }

        public new event EventHandler Leave
        {
            add { treeMain.Leave += value; }
            remove { treeMain.Leave -= value; }
        }

        public new event EventHandler LostFocus
        {
            add { treeMain.LostFocus += value; }
            remove { treeMain.LostFocus -= value; }
        }

        public new event MouseEventHandler MouseClick
        {
            add { treeMain.MouseClick += value; }
            remove { treeMain.MouseClick -= value; }
        }

        public new event MouseEventHandler MouseDoubleClick
        {
            add { treeMain.MouseDoubleClick += value; }
            remove { treeMain.MouseDoubleClick -= value; }
        }

        public new event MouseEventHandler MouseDown
        {
            add { treeMain.MouseDown += value; }
            remove { treeMain.MouseDown -= value; }
        }

        public new event EventHandler MouseEnter
        {
            add { treeMain.MouseEnter += value; }
            remove { treeMain.MouseEnter -= value; }
        }

        public new event EventHandler MouseLeave
        {
            add { treeMain.MouseLeave += value; }
            remove { treeMain.MouseLeave -= value; }
        }

        public new event EventHandler MouseHover
        {
            add { treeMain.MouseHover += value; }
            remove { treeMain.MouseHover -= value; }
        }

        public new event MouseEventHandler MouseMove
        {
            add { treeMain.MouseMove += value; }
            remove { treeMain.MouseMove -= value; }
        }

        public new event MouseEventHandler MouseUp
        {
            add { treeMain.MouseUp += value; }
            remove { treeMain.MouseUp -= value; }
        }

        public new event MouseEventHandler MouseWheel
        {
            add { treeMain.MouseWheel += value; }
            remove { treeMain.MouseWheel -= value; }
        }

        public new event EventHandler Move
        {
            add { treeMain.Move += value; }
            remove { treeMain.Move -= value; }
        }

        public new event CancelEventHandler Validating
        {
            add { treeMain.Validating += value; }
            remove { treeMain.Validating -= value; }
        }

        public event TreeViewEventHandler AfterExpand
        {
            add { treeMain.AfterExpand += value; }
            remove { treeMain.AfterExpand -= value; }
        }

        public event TreeViewEventHandler AfterSelect
        {
            add { treeMain.AfterSelect += value; }
            remove { treeMain.AfterSelect -= value; }
        }

        public event TreeViewCancelEventHandler BeforeSelect
        {
            add { treeMain.BeforeSelect += value; }
            remove { treeMain.BeforeSelect -= value; }
        }

        #endregion


        private void InvokeConnectionChanged(FtpConnection connection)
        {
            ConnectionChangedHandler changed = ConnectionChanged;
            if (changed != null) changed(connection);
        }

        public bool ShowToolBar
        {
            get { return toolbar.Visible; }
            set { toolbar.Visible = value; }
        }

        public bool DirectoriesOnly { get; set; }

        public bool MultiSelect { get; set; }
        public bool UseDefaultDir { get; set; }

        private FtpConnection ftpConnection;

        /// <summary>
        /// Connection
        /// </summary>
        public FtpConnection FtpConnection
        {
            get { return ftpConnection; }
            set
            {
                SetConnection(value, UseDefaultDir);
            }
        }

        public string SelectedPath
        {
            get
            {
                if (treeMain.SelectedNode != null)
                    return GetPathToNode(treeMain.SelectedNode);
                return String.Empty;
            }
            set
            {
                ChangeDirectory(value);
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public FtpExplorer()
        {
            InitializeComponent();
            UseDefaultDir = true;
            imageFileAssociation = new Dictionary<string, int>();
        }

        /// <summary>
        /// Updates the menu with configured settings
        /// </summary>
        public void UpdatePopupMenuConnections()
        {
            popupMenuConnections.ClearLinks();
            var connectionSettings = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
            foreach (var setting in connectionSettings)
            {
                var item = new BarButtonItem(barManager,setting.ToString())
                {
                    Name = "FTP_connection" + setting.Name,
                    Id = barManager.GetNewItemId()
                };
                popupMenuConnections.AddItem(item);
                FtpSettings tmpSetting = setting;
                item.ItemClick += (sender, args) =>
                                      {
                                          try
                                          {
                                              ConnectTo(tmpSetting.Clone() as FtpSettings,UseDefaultDir);
                                          }
                                          catch (Exception)
                                          {
                                              MessageService.ShowError(Resources.FailedToConnect);
                                          }
                                      };
            }

            var itemManage = new BarButtonItem(barManager, Resources.ManageFtpAccounts+@"...")
            {
                Name = "FTP_connectionmanager",
                Id = barManager.GetNewItemId()
            };
            popupMenuConnections.AddItem(itemManage);
            popupMenuConnections.ItemLinks[popupMenuConnections.ItemLinks.Count - 1].BeginGroup = true;
            itemManage.ItemClick += (sender, args) => CompleX_Studio.Instance.ManageFtpAccounts();
        }

        /// <summary>
        /// Connect to FTP Server with given settings
        /// </summary>
        public void ConnectTo(FtpSettings settings, bool setDefaultDir)
        {
            using (new WaitingScope(this,WaitingDialogDescription.Default))
            {
                SetConnection(new FtpConnection(settings) { OutputWriter = new ComplexOutPutWriter() }, setDefaultDir);
            }
        }

        public void TrySetDefaultDir(IFtpSettings settings)
        {
            if (FtpConnection == null)
                throw new FtpException(-1,"NOT CONNECTED");
            if (!String.IsNullOrEmpty(settings.DefaultDirectory))
            {
                try
                {
                    FtpConnection.SetCurrentDirectory(settings.DefaultDirectory);
                    startPath = settings.DefaultDirectory;
                }
                catch (Exception e)
                {
                    startPath = String.Empty;
                    CompleX_Studio.MessageLog.LogException(e);
                }
            }
        }

        public void SetConnection(FtpConnection connection,bool setDefaultDir)
        {
           
            UseDefaultDir = setDefaultDir;

            if (connection == null)
                return;

                startPath = String.Empty;

                CloseConnection();

                ftpConnection = connection;
                InvokeConnectionChanged(FtpConnection);
                using (new WaitingScope(this, WaitingDialogDescription.Default))
                {
                    ftpConnection.Open();
                    ftpConnection.Login();

                    if (setDefaultDir)
                        TrySetDefaultDir(ftpConnection.FtpSettings);

                    CreateDirectoryTree(treeMain.Nodes);

                    barButtonItemDisconnect.Enabled = true;
                }
        }

        private void CreateDirectoryTree(TreeNodeCollection nodes)
        {
            FtpDirectoryInfo[] directories = FtpConnection.GetDirectories();
            FtpFileInfo[] files = FtpConnection.GetFiles();

            foreach (FtpDirectoryInfo directory in directories)
            {
                var addedNode = new TreeNode(directory.Name, 1, 1) {Tag = FolderTag, Name = directory.Name};
                nodes.Add(addedNode);
                addedNode.Nodes.Add(""); // Damit man den ordner aufklappen kann
            }

            if (!DirectoriesOnly)
                nodes.AddRange(files.Select(file => new TreeNode(file.Name, AddFileExtensionImage(file.Extension), AddFileExtensionImage(file.Extension)){Tag = FileTag}).ToArray());
        }


        private void BarButtonItemConnectItemClick(object sender, ItemClickEventArgs e)
        {
            UpdatePopupMenuConnections();
            popupMenuConnections.ShowPopup(barManager, Cursor.Position);
        }

        private void TreeMainBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            using (new WaitingCursor(this))
            {
                UpdateLocation(e.Node);
                e.Node.Nodes.Clear();
                if (FtpConnection != null)
                {
                    FtpConnection.SetCurrentDirectory(labelDirectory.Caption);
                    CreateDirectoryTree(e.Node.Nodes);
                }
            }

        }

        private void TreeMainAfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateLocation(e.Node);
            if(Convert.ToInt32(e.Node.Tag) == FolderTag)
            {
                FtpConnection.SetCurrentDirectory(labelDirectory.Caption);
            }
        }

        private string GetPathToNode(TreeNode node)
        {
            string res;
            if (String.IsNullOrEmpty(startPath))
                 res = treeMain.PathSeparator + node.FullPath;
            else
                 res = treeMain.PathSeparator + startPath + treeMain.PathSeparator + node.FullPath;
               
            while(!res.StartsWith("//"))
                res = "/" + res; 
            
            return res;
        }



        private TreeNode GetNodeByPath(string path)
        {
            while (path.StartsWith("/"))
                path = path.Substring(1);
            TreeNode startNode = null;
            var strings = path.Split('/');
            foreach (string s in strings)
            {
                startNode = startNode == null 
                    ?treeMain.Nodes.Find(s, false).FirstOrDefault() 
                    : startNode.Nodes.Find(s, false).FirstOrDefault();

                if (startNode != null)
                {
                    startNode.Expand();
                    treeMain.SelectedNode = startNode;
                }
            }
            return startNode;
        }

        private void UpdateLocation(TreeNode node)
        {
            labelDirectory.Caption = GetPathToNode(node);
        }

        private int AddFileExtensionImage(string fileExtension)
        {
            if (imageFileAssociation.ContainsKey(fileExtension))
                return imageFileAssociation[fileExtension];

            Icon assiciatedIcon = ImageFunctions.GetAssociatedIconByExtension(fileExtension,false);
            if(assiciatedIcon != null)
            {
                imgMain.Images.Add(assiciatedIcon);
                int index = imgMain.Images.Count - 1;
                imageFileAssociation.Add(fileExtension,index);
                return index;
            }
            return 0;
        }

        private void TreeMainDoubleClick(object sender, EventArgs e)
        {
            CheckOpenOrConfirm();
        }

        private void CheckOpenOrConfirm()
        {
            EventHandler confirmed = Confirmed;
            if (confirmed != null)
            {
                confirmed(this, new EventArgs());
                return;
            }

            using (new WaitingScope(this, WaitingDialogDescription.Default))
            {
                foreach (TreeNode node in treeMain.SelectedNodes)
                {
                    if (Convert.ToInt32(node.Tag) == FileTag)
                        FileService.OpenFileFromFtp(FtpConnection, GetPathToNode(node));
                }
            }

        }


        private void BarButtonItemDisconnectItemClick(object sender, ItemClickEventArgs e)
        {
            CloseConnection();
        }

        public void CloseConnection()
        {
            barButtonItemDisconnect.Enabled = false;
            treeMain.Nodes.Clear();
            labelDirectory.Caption = String.Empty;
            if (FtpConnection != null)
            {
                FtpConnection.Close();
                FtpConnection.Dispose();
            }
            InvokeConnectionChanged(null);
        }

        private void TreeMainMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && treeMain.SelectedNodes.Count > 0)
            {
                var parent = ((TreeNode) treeMain.SelectedNodes[0]).Parent;
                if (treeMain.SelectedNodes.Cast<TreeNode>().Any(node => node != null && node.Parent != null && parent != null && !node.Parent.Equals(parent)))
                    return;
                
                popupMenuSelectedObject.ShowPopup(barManager, Cursor.Position);
            }
        }

        private void BarButtonDownloadItemClick(object sender, ItemClickEventArgs e)
        {
            // Wenn nur ein eintrag ausgewählt ist, der eine datei da stellt, 
            if(treeMain.SelectedNodes.Count == 1 && Convert.ToInt32(((TreeNode)treeMain.SelectedNodes[0]).Tag) == FileTag)
            {
                var node = treeMain.SelectedNodes[0] as TreeNode;
                if (node != null)
                {
                    var dlg = new SaveFileDialog {FileName = node.Text, DefaultExt = Path.GetExtension(node.Text)};
                    if(dlg.ShowDialog() == DialogResult.OK)
                        ftpConnection.GetFile(GetPathToNode(node), dlg.FileName, false);
                }

            }
            else if (treeMain.SelectedNodes.Count > 0)
            {
                DownloadNodes((TreeNode[]) treeMain.SelectedNodes.ToArray(typeof (TreeNode)));
            }
        }

        private void DownloadNodes(IEnumerable<TreeNode> nodes)
        {
            var folderDialog = new FolderBrowserDialog();
            if(folderDialog.ShowDialog() == DialogResult.OK)
                DownloadNodes(folderDialog.SelectedPath,nodes);
        }

        private void DownloadNodes(string directory, IEnumerable<TreeNode> nodes)
        {
            treeMain.Enabled = false;
            ThreadPool.QueueUserWorkItem(state =>
                     {
                         try
                         {
                             string tmppath = labelDirectory.Caption;
                             if (!Directory.Exists(directory))
                                 Directory.CreateDirectory(directory);
                             foreach (TreeNode node in nodes)
                             {
                                 if (Convert.ToInt32(node.Tag) == FileTag)
                                 {
                                     StatusBarService.SetProgress("Downloading " + node.Text, 1, 100);
                                     ftpConnection.GetFile(GetPathToNode(node), directory.AddDirectorySeparatorChar() + node.Text, false);
                                     StatusBarService.SetProgress("Downloading " + node.Text, 100, 100);
                                 }else
                                 {
                                     StatusBarService.SetProgress("Downloading " + node.Text, 1, 100);
                                     string dir = directory.AddDirectorySeparatorChar() + node.Text;
                                     ftpConnection.DownloadDirectory(dir, GetPathToNode(node));
                                     StatusBarService.SetProgress("Downloading " + node.Text, 100, 100);
                                 }
                             }
                             ftpConnection.SetCurrentDirectory(tmppath);
                            
                         }
                         catch (Exception e)
                         {
                            CompleX_Studio.MessageLog.LogException(e);
                         }
                         finally
                         {
                             treeMain.BeginInvoke(new MethodInvoker(() => { treeMain.Enabled = true; }));
                         }
                     });
        }

        private void BarButtonRenameItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (TreeNode node in treeMain.SelectedNodes)
            {
                string newName = InputDlg.Execute(Resources.Name, Resources.Name,node.Text);
                if(!String.IsNullOrEmpty(newName) && newName != node.Text)
                {
                    ftpConnection.RenameFile(GetPathToNode(node),newName);
                    node.Text = newName;
                }
            }
        }

        private void BarButtonItemDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelection();
        }

        private void DeleteSelection()
        {
            if (!MessageService.AskDsaOnYes(Resources.DeleteShowConfirmation, barButtonItemDelete.Caption, "DELETEFILEONFTP"))
                return;
            TreeNode parent = null;
            var nodeList = new List<TreeNode>();
            var wt = new WorkerThread();
            wt.SetDialogDescriptions(WaitingDialogDescription.DefaultWithBorder);
            wt.RunWorkerAsync(args =>
            {
                foreach (TreeNode node in treeMain.SelectedNodes)
                {
                    StatusBarService.SetProgress("Deleting " + node.Text, 1, 100);
                    if (Convert.ToInt32(node.Tag) == FileTag)
                    {
                        ftpConnection.RemoveFile(GetPathToNode(node));
                        parent = node.Parent;
                    }
                    else
                    {
                        ftpConnection.RemoveDirectory(GetPathToNode(node), true);
                        parent = node.Parent;
                    }
                    StatusBarService.SetProgress("Deleting " + node.Text, 100, 100);
                    nodeList.Add(node);
                }
            });
            if(parent != null)
            foreach (TreeNode treeNode in nodeList)
                parent.Nodes.Remove(treeNode);
        }

        private void TreeMainBeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!MultiSelect)
            {
                treeMain.ClearSelection();
            }
        }

        private void TreeMainAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string nameOld = ((TreeNode)treeMain.SelectedNodes[0]).Text;
            string nameNew =  e.Label;
            if(nameNew != null && !nameNew.Equals(nameOld))
            {
                ftpConnection.RenameFile(GetPathToNode(e.Node), nameNew);
            }else
            {
                e.CancelEdit = true;
            }
        }

        private void BarButtonItemCreateFolderItemClick(object sender, ItemClickEventArgs e)
        {
            CreateNewFolder();
        }

        public void CreateNewFolder()
        {
            var name = InputDlg.Execute(Resources.Name, Resources.Name);
            if (!String.IsNullOrEmpty(name))
            {
                ftpConnection.CreateDirectory(name);
                var node = treeMain.SelectedNodes[0] as TreeNode;
                if (node != null)
                {
                    var addedNode = node.Nodes.Add(name, name, 1,1);
                    addedNode.Nodes.Add(""); 
                    InvokeFolderCreated(name,SelectedPath+@"/"+name);
                }
            }
        }

        private void PopupMenuSelectedObjectPopup(object sender, EventArgs e)
        {
            barButtonCreateFolder.Enabled = (treeMain.SelectedNodes.Count == 1 && Convert.ToInt32(((TreeNode)treeMain.SelectedNodes[0]).Tag) == FolderTag);
        }

        public void ChangeDirectory(string dir)
        {
            TreeNode node = GetNodeByPath(dir);
            if(node != null)
                node.Expand();
        }

        

        public static string SelectDirectory(FtpSettings settings)
        {
            if (settings == null)
                throw new NullReferenceException();
            
            var connection = new FtpConnection(settings);
            if (!connection.CanConnect)
                throw new Exception(Resources.FailedToConnect);

            using (var control = new FtpExplorer { UseDefaultDir = false, MultiSelect = false, DirectoriesOnly = true, FtpConnection = connection, ShowToolBar = false })
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(control);
                dlg.Width = 360;
                control.Confirmed += (o, args) => dlg.DialogResult = DialogResult.OK;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    settings.DefaultDirectory = control.SelectedPath;
                    if (settings.DefaultDirectory.StartsWith("//"))
                        settings.DefaultDirectory = settings.DefaultDirectory.Substring(1);
                    return settings.DefaultDirectory;
                }
            }
            
            return String.Empty;
        }

        private void TreeMainKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                CheckOpenOrConfirm();
            }
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelection();
            }


            if (e.KeyCode == Keys.F10 && ModifierKeys == Keys.Shift)
            {
                popupMenuSelectedObject.ShowPopup(Cursor.Position);
            }

            if (e.KeyCode == Keys.F2)
                treeMain.SelectedNode.BeginEdit();
        }

   
    }

}

