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
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using CompleX.Classes;
using CompleX.Dialogs;
using CompleX.Helper;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Types;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;

namespace CompleX.Controls
{
    public partial class ProjectExplorer : HostedControl
    {

        #region Konstanten

        const string ItemTypeFolder = "FolderItem";
        const string ItemTypeFile = "FileItem";
        const string ItemTypeProject = "ProjectItem";

        #endregion

        private readonly Dictionary<string, int> imageFileAssociation;
        private string startUpFileName;

        #region Public

        /// <summary>
        /// Event if properties clicked
        /// </summary>
        public event EventHandler PropertiesItemClick;

        /// <summary>
        /// Occurs when [project closed].
        /// </summary>
        public event EventHandler ProjectClosed;

        /// <summary>
        /// Occurs when [project opened].
        /// </summary>
        public event EventHandler ProjectOpened;

        /// <summary>
        /// Gets a value indicating whether [complex creates the structure or not].
        /// </summary>
        public bool CheckAndCopyStructure
        {
            get
            {
                // ReSharper disable PossibleNullReferenceException
                return Path.GetExtension(ProjectFileName).ToLower().Equals(@".cspr");
                // ReSharper restore PossibleNullReferenceException
            }
        }

        /// <summary>
        /// Gets a value indicating whether [folder or project selected].
        /// </summary>
        public bool FolderOrProjectSelected
        {
            get
            {
                return SelectedNode != null && SelectedNodeView != null &&
                       (SelectedNodeView["itemType"].ToString() == ItemTypeFolder ||
                        SelectedNodeView["itemType"].ToString() == ItemTypeProject);
            }
        }

        /// <summary>
        /// Gets the project ID.
        /// </summary>
        public Guid ProjectID
        {
            get
            {
                DataRow dr = dataSetTree.DataTableTree.Rows[0];
                if (dr != null)
                    return new Guid(dr["id"].ToString());
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectExplorer()
        {
            InitializeComponent();
            ProjectFileName = String.Empty;
            imageFileAssociation = new Dictionary<string, int>();
            EnableControls();
        }

        /// <summary>
        /// Returns allfiles in project.
        /// </summary>
        public IEnumerable<string> ProjectFiles
        {
            get
            {
                try
                {
                    var nodes = treeListMain.GetAllNodes();
                    return nodes.Select(node => treeListMain.GetDataRecordByNode(node) as DataRowView).Where(nodeView => nodeView != null && nodeView["itemType"].ToString() == ItemTypeFile).Select(nodeView => GetFileNameByDataRow(nodeView.Row)).Where(File.Exists).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Datei die als startup benutzt werden soll, (dateiendung dieser datei ist die für den Executer (IExecutable (SupportedFileExtensions))
        /// </summary>
        public string StartUpFileName
        {
            get
            {
                if (String.IsNullOrEmpty(startUpFileName))
                    StartUpFileName = FindStartUpFileName();
                return startUpFileName;
            }
            set
            {
                if (ProjectFiles.Contains(value))
                {
                    startUpFileName = value;
                    UpdateStartUpFile();
                }
            }
        }

        private string FindStartUpFileName()
        {
            var nodes = treeListMain.GetAllNodes();
            foreach (var dataRowView in
                nodes.Select(node => treeListMain.GetDataRecordByNode(node)).OfType<DataRowView>().Where(dataRowView => Convert.ToBoolean(dataRowView["isStartUp"])))
            {
                return GetFileNameByDataRow(dataRowView.Row);
            }
            return String.Empty;
        }

        private void UpdateStartUpFile()
        {
            var nodes = treeListMain.GetAllNodes();
            foreach (TreeListNode node in nodes)
            {
                var dataRowView = treeListMain.GetDataRecordByNode(node) as DataRowView;

                if (dataRowView != null)
                {
                    dataRowView["isStartUp"] = false;
                    string file = GetFileNameByDataRow(dataRowView.Row);
                    if (file == StartUpFileName)
                    {
                        dataRowView["isStartUp"] = true;
                    }
                }
            }
            SaveProject();
        }


        /// <summary>
        /// Path to ProjectFileName
        /// </summary>
        public string ProjectDirectory
        {
            get
            {
                if (!String.IsNullOrEmpty(ProjectFileName))
                    return Path.GetDirectoryName(ProjectFileName).AddDirectorySeparatorChar();
                return String.Empty;
            }
        }

        /// <summary>
        /// Project file
        /// </summary>
        public string ProjectFileName { get; private set; }

        /// <summary>
        /// Gets value indicating whether this instance is project open.
        /// </summary>
        public bool IsProjectOpen
        {
            get
            {
                return !String.IsNullOrEmpty(ProjectFileName);
            }
        }

        /// <summary>
        /// Closes the project.
        /// </summary>
        public void CloseProject(bool closeOpenFiles)
        {
            var projectFiles = ProjectFiles;
            SaveProject();
            dataSetTree.DataTableTree.Clear();
            treeListMain.ClearNodes();
            currentItemPath.Caption = String.Empty;
            ProjectFileName = String.Empty;
            EnableControls();
            if (ProjectClosed != null)
                ProjectClosed(this, new EventArgs());

            if (closeOpenFiles && projectFiles.Count() > 0)
            {
                FileService.CloseFiles(projectFiles);
            }

        }


        /// <summary>
        /// Returns all selected filenames (only fileitems)
        /// </summary>
        public string[] SelectedFileNames
        {
            get
            {
                return (from selectedNodeView in
                            (from TreeListNode selectedNode in treeListMain.Selection
                             select treeListMain.GetDataRecordByNode(selectedNode)).OfType<DataRowView>()
                        where selectedNodeView["itemType"].ToString() == ItemTypeFile
                        select GetFileNameByDataRow(selectedNodeView.Row)).ToArray();
            }
        }

        /// <summary>
        /// selection[0]
        /// </summary>
        public TreeListNode SelectedNode
        {
            get
            {
                if (treeListMain.Selection.Count > 0)
                    return treeListMain.Selection[0];
                return null;
            }
            set
            {
                if (value != null)
                {
                    treeListMain.Selection.Clear();
                    value.Selected = true;
                }
            }
        }

        /// <summary>
        /// DataRowView of selection[0]
        /// </summary>
        public DataRowView SelectedNodeView
        {
            get
            {
                if (SelectedNode != null)
                    return treeListMain.GetDataRecordByNode(SelectedNode) as DataRowView;
                return null;
            }
        }



        /// <summary>
        /// Creates a new Project
        /// </summary>
        /// <param name="projectFile"></param>
        public void CreateNewProject(string projectFile)
        {
            dataSetTree.DataTableTree.AddDataTableTreeRow(Guid.NewGuid(), Guid.Empty, Path.GetFileName(projectFile), ItemTypeProject, 0, projectFile,false);
            ProjectFileName = projectFile;
            EnableControls();
            if (ProjectOpened != null)
                ProjectOpened(this, new EventArgs());
            SaveProject(projectFile);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Aufklappen der ersten node
        /// </summary>
        public void ExpandFirst()
        {
            CollapseAll();
            treeListMain.Nodes.FirstNode.Expanded = true;
        }

        /// <summary>
        /// Alle einklappen
        /// </summary>
        public void CollapseAll()
        {
            treeListMain.CollapseAll();
        }

        /// <summary>
        /// Alle aufklappen
        /// </summary>
        public void ExpandAll()
        {
            treeListMain.ExpandAll();
        }


        /// <summary>
        /// Loads a project from file
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadProject(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                CompleX_Studio.Instance.UpdateRecentList(fileName);
                if (IsProjectOpen)
                    CloseProject(false);

                ProjectFileName = fileName;
                if (File.Exists(fileName))
                {
                    treeListMain.BeginUpdate();
                    dataSetTree.DataTableTree.ReadXml(fileName);
                    treeListMain.FireChanged();
                    treeListMain.ForceInitialize();
                    treeListMain.EndUpdate();
                    UpdateItemImages();
                    EnableControls();
                    UpdateStartUpFile();
                    if (ProjectOpened != null)
                        ProjectOpened(this, new EventArgs());
                }
                else
                {
                    CreateNewProject(fileName);
                }
                SelectedNode.Expanded = true;
            }
        }


        /// <summary>
        ///  saves project to file
        /// </summary>
        public void SaveProject()
        {
            if (IsProjectOpen)
                SaveProject(ProjectFileName);
        }

        /// <summary>
        ///  saves project to file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveProject(string fileName)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            dataSetTree.DataTableTree.WriteXml(fileName);
            ProjectFileName = fileName;
        }

        /// <summary>
        /// Update all images of every fileItem
        /// </summary>
        public void UpdateItemImages()
        {
            var rows = dataSetTree.DataTableTree.Rows;

            foreach (DataRow row in rows)
            {
                UpdateItemImage(row);
            }
        }


        /// <summary>
        /// Adds a existing directory.
        /// </summary>
        public void AddExistingDirectory()
        {
            var dlg = new AddDirectoryDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                AddExistingDirectory(dlg.Directory, dlg.IncludeSubDirectories, dlg.Filter);
            }
        }


        /// <summary>
        /// Adds the existing directory.
        /// </summary>
        public void AddExistingDirectory(string dir, bool includeSubDirectories)
        {
            AddExistingDirectory(dir, includeSubDirectories, String.Empty);
        }

        /// <summary>
        /// Adds a existing directory with same files.
        /// </summary>
        public void AddExistingDirectory(string dir, bool includeSubDirectories, string searchPattern)
        {
            AddExistingDirectory(dir, includeSubDirectories, searchPattern, true);
        }

        /// <summary>
        /// Adds a existing directory with same files.
        /// </summary>
        public void AddExistingDirectory(string dir, bool includeSubDirectories, string searchPattern, bool addFirstFolder)
        {
            using (new WaitingScope(this, WaitingDialogDescription.Default))
            {
                if (Directory.Exists(dir))
                {
                    if (addFirstFolder)
                        AddFolder(Path.GetFileName(dir));

                    TreeListNode node = SelectedNode;
                    //Files

                    var directories = Directory.GetDirectories(dir);
                    foreach (var directory in directories)
                    {
                        if (!includeSubDirectories)
                        {
                            AddFolder(Path.GetFileName(directory));
                            SelectedNode = node;
                        }
                        else
                        {
                            AddExistingDirectory(directory, true, searchPattern);
                            SelectedNode = node;
                        }
                    }

                    SelectedNode = node;
                    AddExistingItems(!String.IsNullOrEmpty(searchPattern)
                                         ? Directory.GetFiles(dir, searchPattern)
                                         : Directory.GetFiles(dir));
                }
            }
        }

        /// <summary>
        /// Executes a file dialog and added all selected files to project
        /// </summary>
        /// <returns></returns>
        public bool AddExistingItems()
        {
            if (FolderOrProjectSelected)
            {
                var fileDialog = new OpenFileDialog { Multiselect = true };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    return AddExistingItems(fileDialog.FileNames);

            }
            return false;
        }

        /// <summary>
        /// Adds existing items.
        /// </summary>
        public bool AddExistingItems(params string[] files)
        {
            var currentFiles = ProjectFiles;
            using (new WaitingScope(this, WaitingDialogDescription.Default))
            {

                bool folderIsSelected = (SelectedNodeView["itemType"].ToString() == ItemTypeFolder ||
                                 SelectedNodeView["itemType"].ToString() == ItemTypeProject);

                var nodeView = SelectedNodeView;
                var node = SelectedNode;
                var parentId = new Guid(SelectedNodeView["id"].ToString());

                if (!folderIsSelected) // Falls kein Ordner selectiert ist, den übergeordneten order der datei benutzen
                {
                    node = GetParentFolder(SelectedNode);
                    parentId = new Guid(node["id"].ToString());
                    nodeView = treeListMain.GetDataRecordByNode(node) as DataRowView;
                }

                if (files.Count() > 0)
                {
                    Guid newItemId = Guid.Empty;
                    foreach (string f in files)
                    {
                        if (!currentFiles.Contains(f))
                        {
                            string fileName = f;
                            if (CheckAndCopyStructure && Path.GetExtension(fileName) != ".cspr")
                            {
                                string dir = nodeView != null &&
                                             nodeView["itemType"].ToString() == ItemTypeProject
                                                 ? ProjectDirectory.AddDirectorySeparatorChar()
                                                 : ProjectDirectory +
                                                   node.AsString(true).AddDirectorySeparatorChar();
                                if (!Directory.Exists(dir))
                                {
                                    try
                                    {
                                        Directory.CreateDirectory(dir);
                                    }
                                    catch (Exception e)
                                    {
                                        CompleX_Studio.MessageLog.LogException(e);
                                    }
                                }

                                if (!File.Exists(dir + Path.GetFileName(fileName)))
                                    File.Copy(fileName, dir + Path.GetFileName(fileName), true);
                                else
                                {
                                    if (!fileName.Equals(dir + Path.GetFileName(fileName)))
                                    {
                                        CompleX_Studio.MessageLog.LogWarning(
                                            String.Format(Resources.ProjectExplorerAddExists, Path.GetFileName(fileName)));
                                    }
                                }

                                fileName = dir + Path.GetFileName(fileName);
                            }

                            if (nodeView != null)
                            {
                                newItemId = Guid.NewGuid();
                                string newName = Path.GetFileName(fileName);
                                dataSetTree.DataTableTree.AddDataTableTreeRow(newItemId, parentId, newName, ItemTypeFile,
                                                                              -1,
                                                                              fileName,false);
                            }
                            OutputService.WriteLine("Added file: {0} to project {1}", f,
                                                    Path.GetFileName(ProjectFileName));
                            UpdateItemImage(dataSetTree.DataTableTree.Rows[dataSetTree.DataTableTree.Rows.Count - 1]);
                        }
                    }

                    if (node != null) node.Expanded = true;
                    treeListMain.Selection.Clear();
                    TreeListNode newNode = treeListMain.FindNodeByKeyID(newItemId);
                    if (newNode != null)
                        newNode.Selected = true;
                    SaveProject();
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Adding a new folder.
        /// </summary>
        public void AddFolder(string folderName)
        {
            Guid parentID;
            string dir = folderName;
            var node = SelectedNode;
            if (SelectedNode != null && SelectedNodeView != null &&
               (SelectedNodeView["itemType"].ToString() == ItemTypeFolder || SelectedNodeView["itemType"].ToString() == ItemTypeProject))
            {
                parentID = new Guid(SelectedNodeView["id"].ToString());
            }
            else
            {
                node = GetParentFolder(SelectedNode);
                parentID = new Guid(node["id"].ToString());
            }

            if (CheckAndCopyStructure)
            {
                dir = SelectedNodeView != null && SelectedNodeView["itemType"].ToString() == ItemTypeProject
                                 ? ProjectDirectory.AddDirectorySeparatorChar() + folderName
                                 : ProjectDirectory + node.AsString(true).AddDirectorySeparatorChar() + folderName;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }

            Guid newItemId = Guid.NewGuid();
            dataSetTree.DataTableTree.AddDataTableTreeRow(newItemId, parentID, folderName,
                                                          ItemTypeFolder, 2, dir,false);
            if (SelectedNode != null) SelectedNode.Expanded = true;
            TreeListNode newNode = treeListMain.FindNodeByKeyID(newItemId);
            treeListMain.Selection.Clear();
            newNode.Selected = true;
            SaveProject();
        }


        /// <summary>
        /// Adding a new folder.
        /// </summary>
        public void AddFolder()
        {
            string folderName = InputDlg.Execute(Resources.FolderName, Resources.Name);
            if (!String.IsNullOrEmpty(folderName))
            {
                AddFolder(folderName);
            }
        }

        /// <summary>
        /// Uploads project to ftp connection
        /// </summary>
        public void UploadProjectToFtp(FtpSettings settings, string directory, bool project, bool zip)
        {
            string tempDir = FileService.GetTempUniqueDir();
            ExportProject(tempDir, project, zip);
            StatusBarService.StartIndeterminateProgress(Resources.Upload);
            var connection = new FtpConnection(settings) { OutputWriter = new ComplexOutPutWriter() };
            connection.Open();
            connection.Login();
            if (!connection.DirectoryExists(directory))
                connection.CreateDirectory(directory);
            connection.UploadDirectoryAsync(tempDir, directory, b =>
                                                                    {
                                                                        StatusBarService.StopIndeterminateProgress();
                                                                        CompleX_Studio.MessageLog.LogInformation(
                                                                            Resources.UploadFinished);
                                                                    },
                                                                    s => StatusBarService.StartIndeterminateProgress(
                                                                        "Uploading: " + s));
        }

        /// <summary>
        ///  Exports the project to the given destination directory.
        /// </summary>
        public void ExportProject(string destination)
        {
            ExportProject(destination, false, false);
        }

        /// <summary>
        /// Exports the project to the given destination directory.
        /// </summary>
        public void ExportProject(string destination, bool exportProjectFile, bool storeInZip)
        {
            using (new WaitingScope(this, WaitingDialogDescription.Default))
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);

                string orgDestination = destination;

                if (storeInZip)
                {
                    destination = Path.GetTempPath().AddDirectorySeparatorChar() + "CS_TEMP_EXP";
                }

                ExportNode(treeListMain.Nodes[0], destination);
                if (exportProjectFile)
                {
                    string destProjectFile = destination.AddDirectorySeparatorChar() +
                                             Path.GetFileNameWithoutExtension(ProjectFileName) + ".cspr";

                    File.Copy(ProjectFileName, destProjectFile, true);
                }

                if (storeInZip)
                {
                    string zipFile = orgDestination.AddDirectorySeparatorChar() +
                                     Path.GetFileNameWithoutExtension(ProjectFileName) + ".zip";
                    var zipStorer = ZipStorer.Create(zipFile, ProjectFileName);
                    zipStorer.AddFolder(destination);
                    zipStorer.Close();
                    Directory.Delete(destination, true);
                }

            }
        }


        #endregion

        private void ExportNode(TreeListNode node, string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var nodeView = treeListMain.GetDataRecordByNode(node) as DataRowView;
            if (nodeView != null)
            {
                if (nodeView["itemType"].ToString() == ItemTypeFolder)
                {
                    dir = dir.AddDirectorySeparatorChar() + nodeView["text"];
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                }
                if (nodeView["itemType"].ToString() == ItemTypeFile)
                {
                    string file = GetFileNameByDataRow(nodeView.Row);
                    if (File.Exists(file))
                        File.Copy(file, dir.AddDirectorySeparatorChar() + Path.GetFileName(file), true);

                }
            }
            foreach (TreeListNode listNode in node.Nodes)
                ExportNode(listNode, dir);
        }

        private void EnableControls()
        {
            foreach (BarItemLink link in barHeader.ItemLinks)
                link.Item.Enabled = IsProjectOpen;
            treeListMain.Enabled = IsProjectOpen;
        }

        private void IPropertiesItemClick(object sender, ItemClickEventArgs e)
        {
            if (PropertiesItemClick != null) PropertiesItemClick(sender, EventArgs.Empty);
        }


        private void IAddItemItemClick(object sender, ItemClickEventArgs e)
        {
            popupMenu.ShowPopup(barManager1, Cursor.Position);

        }


        private TreeListNode GetParentFolder(TreeListNode node)
        {

            if (node.ParentNode == null)
                if (treeListMain.Nodes[0] != null) return treeListMain.Nodes[0];

            node = node.ParentNode;
            while (true)
            {
                var view = treeListMain.GetDataRecordByNode(node) as DataRowView;
                if (view != null && view["itemType"].ToString() == ItemTypeFolder)
                {
                    return node;
                }
                if (node != null)
                    node = node.ParentNode;
                else
                {
                    return treeListMain.Nodes[0];
                }
            }
        }

        private string GetFileNameByDataRow(DataRow row)
        {
            if (CheckAndCopyStructure && Path.GetExtension(row["path"].ToString()) != ".cspr")
            {
                var allNodes = treeListMain.GetAllNodes();
                foreach (TreeListNode node in allNodes)
                {
                    var view = treeListMain.GetDataRecordByNode(node) as DataRowView;
                    if (view != null && view.Row.Equals(row))
                    {
                        string path = node.AsString(true);
                        string result = ProjectDirectory + path.RemoveDirectorySeparatorChar();
                        return result;
                    }
                }
            }
            return row["path"].ToString();
        }

        private void UpdateItemImage(DataRow row)
        {
            if (row["itemType"].ToString() == ItemTypeFile)
            {
                string fileName = GetFileNameByDataRow(row);
                if ((row["ImageIndex"].ToString() != "0"))
                {
                    int imageIndex;
                    if (!imageFileAssociation.ContainsKey(Path.GetExtension(fileName)))
                    {
                        if (File.Exists(fileName))
                        {
                            Icon ico = ImageFunctions.GetFileIcon(fileName, false);
                            imageListFileTypes.Images.Add(ico.ToBitmap());
                            imageIndex = imageListFileTypes.Images.Count - 1;
                            imageFileAssociation.Add(Path.GetExtension(fileName), imageIndex);
                        }
                        else
                        {
                            imageIndex = 3;
                        }
                    }
                    else
                    {
                        imageIndex = imageFileAssociation[Path.GetExtension(fileName)];
                    }
                    row["ImageIndex"] = imageIndex;
                }
            }
        }


        private void IRefreshItemClick(object sender, ItemClickEventArgs e)
        {
            treeListMain.RefreshDataSource();
            UpdateItemImages();
            treeListMain.Refresh();
        }

        private void BarButtonAddExistingItemClick(object sender, ItemClickEventArgs e)
        {
            AddExistingItems();
        }

        private void BarButtonAddFolderItemClick(object sender, ItemClickEventArgs e)
        {
            AddFolder();
        }

        private void TreeListMainFocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (SelectedNodeView != null)
                currentItemPath.Caption = GetFileNameByDataRow(SelectedNodeView.Row);
        }

        private void TreeListMainSelectionChanged(object sender, EventArgs e)
        {
            if (SelectedNodeView != null)
                currentItemPath.Caption = GetFileNameByDataRow(SelectedNodeView.Row);
        }

        private void TreeListMainMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ModifierKeys == Keys.Control)
                {
                    MenuService.ShowShellContextMenu(SelectedFileNames);
                }
                else
                {
                    addExistingItemToolStripMenuItem.Enabled = (SelectedNode != null && SelectedNodeView != null &&
                                    (SelectedNodeView["itemType"].ToString() == ItemTypeFolder ||
                                     SelectedNodeView["itemType"].ToString() == ItemTypeProject));

                    addNewItemToolStripMenuItem.Enabled = addExistingItemToolStripMenuItem.Enabled;

                    MenuService.ShowDefaultFileContextMenu(null, contextMenuSelectedItem, MergeOrder.CustomFirst, false, SelectedFileNames);
                }
            }
        }

        private void AddExistingItemToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddExistingItems();
        }

        private void AddNewFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddFolder();
        }

        private void BarButtonAddNewItemItemClick(object sender, ItemClickEventArgs e)
        {
            AddNewItem();
        }


        private void AddNewItemToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddNewItem();
        }

        public void AddNewItem()
        {
            if (IsProjectOpen)
                CompleX_Studio.Instance.New(ProjectControlMode.FileAdd);
        }

        public void SelectRootNode()
        {
            treeListMain.Selection.Clear();
            treeListMain.Nodes[0].Selected = true;
        }

        private List<DataRowView> GetSelectedDataRows()
        {
            return (from TreeListNode node in treeListMain.Selection select treeListMain.GetDataRecordByNode(node)).OfType<DataRowView>().ToList();
        }

        private void DeleteSelectedToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void DeleteSelected()
        {
            if (treeListMain.Nodes[0].Selected)
            {
                bool delete = MessageService.ShowConfirmation(Resources.DeleteProjectBecauseSelected, Path.GetFileName(ProjectFileName), false, MessageIconType.Warning, @"DeleteHoleProjectNotification", Resources.DsaDefaultText) == DialogResult.Yes;
                if (delete)
                {
                    string dir = ProjectDirectory;
                    CloseProject(true);
                    Directory.Delete(dir, true);
                }
                return;
            }

            var views = GetSelectedDataRows();
            if (views.Count > 0 &&
                MessageService.AskDsaOnYes(Resources.RemoveFromProjectConfirmation, deleteSelectedToolStripMenuItem.Text, @"ProjectExplorerFileDeleteNotification"))
            {

                foreach (DataRowView row in views)
                {
                    string fileName = GetFileNameByDataRow(row.Row);
                    if (CheckAndCopyStructure)
                    {
                        try
                        {
                            if (File.Exists(fileName) && Path.GetExtension(fileName) != ".cspr")
                                File.Delete(fileName);
                            if (Directory.Exists(fileName))
                                Directory.Delete(fileName, true);
                        }
                        catch (Exception e)
                        {
                            CompleX_Studio.MessageLog.LogException(e);
                        }
                    }
                    else
                    {
                        if (row["itemType"].ToString() == ItemTypeFile)
                            FileService.DeleteFileWithConfirmation(fileName, @"DSA_DELETE_FILE_INPROJECT", true);
                    }
                    row.Delete();
                }
                treeListMain.RefreshDataSource();
                SaveProject();

            }
        }

        private void RenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            RenameSelected();
        }

        private void RenameSelected()
        {
            var views = GetSelectedDataRows();
            if (views.Count > 0)
            {
                foreach (DataRowView row in views)
                {
                    string newFileName = InputDlg.Execute(Resources.Rename, Resources.Name, row["Text"].ToString());
                    if (!String.IsNullOrEmpty(newFileName) && newFileName != row["Text"].ToString())
                    {
                        if (!treeListMain.GetDataRecordByNode(treeListMain.Nodes[0]).Equals(row))
                        {
                            string oldFileName = GetFileNameByDataRow(row.Row);
                            string newPath = Path.GetDirectoryName(oldFileName).AddDirectorySeparatorChar() +
                                             newFileName;
                            if (File.Exists(oldFileName))
                            {
                                FileService.RenameFile(oldFileName, newPath);
                            }
                            else if (Directory.Exists(oldFileName))
                            {
                                Directory.Move(oldFileName, newPath);
                            }
                        }
                        else
                        {
                            // Project File is renamed
                            FileService.RenameFile(ProjectFileName, ProjectDirectory.AddDirectorySeparatorChar() + newFileName);
                            ProjectFileName = ProjectDirectory.AddDirectorySeparatorChar() + newFileName;
                        }
                        row["path"] = Path.GetDirectoryName(row["path"].ToString()).AddDirectorySeparatorChar() + newFileName;
                        row["Text"] = newFileName;
                    }
                }
                treeListMain.RefreshDataSource();
                SaveProject();
            }
        }

        private void BarButtonItem1ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSelected();
        }


        private void TreeListMainMouseDoubleClick(object sender, MouseEventArgs e)
        {
            FileService.LoadFiles(SelectedFileNames.Where(File.Exists));
        }

        private void BarButtonItem4ItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.LoadFiles(ProjectFiles.Where(File.Exists));
        }

        private void BarButtonItem7ItemClick(object sender, ItemClickEventArgs e)
        {
            CollapseAll();
        }


        private void BarButtonItem6ItemClick(object sender, ItemClickEventArgs e)
        {
            ExpandAll();
        }

        private void TreeListMainKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                RenameSelected();

            if(e.KeyCode == Keys.Delete && SelectedNode != null)
                DeleteSelected();

            if (e.KeyCode == Keys.F10 && ModifierKeys == Keys.Shift)
            {
                MenuService.ShowDefaultFileContextMenu(null, contextMenuSelectedItem, MergeOrder.CustomFirst, false, SelectedFileNames.Where(File.Exists).ToArray());
            }

            if (e.KeyCode == Keys.Right && SelectedNode != null && SelectedNode.HasChildren)
                SelectedNode.Expanded = true;

            if (e.KeyCode == Keys.Left && SelectedNode != null && SelectedNode.Expanded)
                SelectedNode.Expanded = false;

            if (e.KeyCode == Keys.Enter)
                FileService.LoadFiles(SelectedFileNames.Where(File.Exists));


        }

        private void PopupMenuBeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            barButtonAddExisting.Enabled = (SelectedNode != null && SelectedNodeView != null &&
                                            (SelectedNodeView["itemType"].ToString() == ItemTypeFolder ||
                                             SelectedNodeView["itemType"].ToString() == ItemTypeProject));

            barButtonAddNewItem.Enabled = barButtonAddExisting.Enabled;
        }

        private void BarButtonItem2ItemClick(object sender, ItemClickEventArgs e)
        {
            AddExistingDirectory();
        }

        private void AddExistingDirectoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            AddExistingDirectory();
        }

        private void SetSelectedAsStartUpToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SelectedFileNames.First()))
            {
                StartUpFileName = SelectedFileNames.First();
            }
        }

        private void treeListMain_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {

            var dataRowView = treeListMain.GetDataRecordByNode(e.Node) as DataRowView;

            if (dataRowView != null)
            {
                bool isStartUp = false;
                if (dataRowView["isStartUp"] != null)
                    isStartUp = Convert.ToBoolean(dataRowView["isStartUp"]);
                e.Appearance.Font = isStartUp ? new Font(e.Appearance.Font, FontStyle.Bold) : new Font(e.Appearance.Font, FontStyle.Regular);
            }
        }


    }
}