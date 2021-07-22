using CompleX.Controls;

namespace CompleX
{
    internal partial class ComplexMainForm {
        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplexMainForm));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.MainMenuBar = new DevExpress.XtraBars.Bar();
            this.iFile = new DevExpress.XtraBars.BarSubItem();
            this.siNew = new DevExpress.XtraBars.BarSubItem();
            this.iCreateNewProject = new DevExpress.XtraBars.BarButtonItem();
            this.siFile = new DevExpress.XtraBars.BarButtonItem();
            this.iCreateEmptyProject = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.iOpen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.iOpenProject = new DevExpress.XtraBars.BarButtonItem();
            this.iOpenFromFtp = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.iSave = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveAll = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveAllExistingFiles = new DevExpress.XtraBars.BarButtonItem();
            this.iSaveToFtp = new DevExpress.XtraBars.BarButtonItem();
            this.iClose = new DevExpress.XtraBars.BarButtonItem();
            this.iCloseAll = new DevExpress.XtraBars.BarButtonItem();
            this.iCloseProject = new DevExpress.XtraBars.BarButtonItem();
            this.iRecentFiles = new DevExpress.XtraBars.BarSubItem();
            this.iRecentProjects = new DevExpress.XtraBars.BarSubItem();
            this.iAddProject = new DevExpress.XtraBars.BarSubItem();
            this.iAddNewItem = new DevExpress.XtraBars.BarButtonItem();
            this.iAddExistingItem = new DevExpress.XtraBars.BarButtonItem();
            this.iAddExistingDirectory = new DevExpress.XtraBars.BarButtonItem();
            this.iPageSetup = new DevExpress.XtraBars.BarButtonItem();
            this.iPrint = new DevExpress.XtraBars.BarButtonItem();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.iEdit = new DevExpress.XtraBars.BarSubItem();
            this.iUndo = new DevExpress.XtraBars.BarButtonItem();
            this.iRedo = new DevExpress.XtraBars.BarButtonItem();
            this.iCut = new DevExpress.XtraBars.BarButtonItem();
            this.iCopy = new DevExpress.XtraBars.BarButtonItem();
            this.iPaste = new DevExpress.XtraBars.BarButtonItem();
            this.iCycle = new DevExpress.XtraBars.BarButtonItem();
            this.iClipboard = new DevExpress.XtraBars.BarButtonItem();
            this.iSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.siFind = new DevExpress.XtraBars.BarSubItem();
            this.iFind = new DevExpress.XtraBars.BarButtonItem();
            this.iReplace = new DevExpress.XtraBars.BarButtonItem();
            this.iFindinFiles = new DevExpress.XtraBars.BarButtonItem();
            this.iEditWith = new DevExpress.XtraBars.BarSubItem();
            this.iSelectApplication = new DevExpress.XtraBars.BarButtonItem();
            this.iView = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.iExportProject = new DevExpress.XtraBars.BarButtonItem();
            this.iUploadProject = new DevExpress.XtraBars.BarButtonItem();
            this.iInsert = new DevExpress.XtraBars.BarSubItem();
            this.iTools = new DevExpress.XtraBars.BarSubItem();
            this.iExtras = new DevExpress.XtraBars.BarSubItem();
            this.iStartNewInstance = new DevExpress.XtraBars.BarButtonItem();
            this.iBrowser = new DevExpress.XtraBars.BarButtonItem();
            this.iDOSCommand = new DevExpress.XtraBars.BarButtonItem();
            this.iLastDosCommand = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.iWindow = new DevExpress.XtraBars.BarSubItem();
            this.biTabbedMDI = new DevExpress.XtraBars.BarButtonItem();
            this.iCascade = new DevExpress.XtraBars.BarButtonItem();
            this.iHorizontal = new DevExpress.XtraBars.BarButtonItem();
            this.iVertical = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAsToolWindow = new DevExpress.XtraBars.BarButtonItem();
            this.BarMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.iHelp = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemSendError = new DevExpress.XtraBars.BarButtonItem();
            this.iAbout = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.iNew = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuNew = new DevExpress.XtraBars.PopupMenu(this.components);
            this.iOpenFile = new DevExpress.XtraBars.BarButtonItem();
            this.eFind = new DevExpress.XtraBars.BarEditItem();
            this.searchItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.iSolutionExplorer = new DevExpress.XtraBars.BarButtonItem();
            this.iProperties = new DevExpress.XtraBars.BarButtonItem();
            this.iToolbox = new DevExpress.XtraBars.BarButtonItem();
            this.iClassView = new DevExpress.XtraBars.BarButtonItem();
            this.popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageListSE = new System.Windows.Forms.ImageList(this.components);
            this.iTaskList = new DevExpress.XtraBars.BarButtonItem();
            this.iFindResults = new DevExpress.XtraBars.BarButtonItem();
            this.iOutput = new DevExpress.XtraBars.BarButtonItem();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.iStatus1 = new DevExpress.XtraBars.BarStaticItem();
            this.labelAction = new DevExpress.XtraBars.BarStaticItem();
            this.progressBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.progressBarMarquee = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.iSaveLayout = new DevExpress.XtraBars.BarButtonItem();
            this.iDeleteLayout = new DevExpress.XtraBars.BarButtonItem();
            this.beiDockMode = new DevExpress.XtraBars.BarEditItem();
            this.dockingComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.bar6 = new DevExpress.XtraBars.Bar();
            this.iStart = new DevExpress.XtraBars.BarButtonItem();
            this.eExecuter = new DevExpress.XtraBars.BarEditItem();
            this.ComboBoxExecuters = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barFtpConnections = new DevExpress.XtraBars.Bar();
            this.barButtonItemConnectTo = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemConnections = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBoxConnections = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanelToolbox = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel6_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.toolBox = new CompleX.Controls.ToolBox();
            this.dockPanelActionList = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer6 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.actionListControl = new CompleX.Controls.ActionListControl();
            this.dockPanelRecentFiles = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer5 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.recentFileListControl = new CompleX.Controls.FileListControl();
            this.dockPanelOpenFiles = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer4 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.openFilesControl = new CompleX.Controls.FileListControl();
            this.contextMenuStripOpenFileExplorer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelProperties = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.textEditSearchProperties = new CompleX.Controls.SearchTextBox();
            this.propertyGrid = new DevExpress.DXperience.Demos.XtraPropertyGrid();
            this.dockPanelSolutionExplorer = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.projectExplorer = new CompleX.Controls.ProjectExplorer();
            this.dockPanelFileExplorer = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.fileExplorer = new CompleX.Controls.FileExplorer();
            this.dockPanelFtpExplorer = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer7 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ftpExplorer1 = new CompleX.Controls.FtpExplorer();
            this.imageListMain = new System.Windows.Forms.ImageList(this.components);
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelFindResults = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.findResultsControl = new CompleX.Controls.FindResultsControl();
            this.textBoxFindResult = new DevExpress.XtraEditors.MemoEdit();
            this.dockPanelTaskList = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.taskListControl = new CompleX.Controls.TaskListControl();
            this.dockPanelOutPut = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.textBoxOutPut = new DevExpress.XtraEditors.MemoEdit();
            this.dockPanelMessageLog = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.messageLogControl = new CompleX.Controls.MessageLogControl();
            this.dockPanelCmd = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.shellControl = new CompleX.Controls.ShellControl();
            this.siLayouts = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.iCloseAllButThis = new DevExpress.XtraBars.BarButtonItem();
            this.iCopyFullPath = new DevExpress.XtraBars.BarButtonItem();
            this.iOpenContainingFolder = new DevExpress.XtraBars.BarButtonItem();
            this.iFileInformation = new DevExpress.XtraBars.BarButtonItem();
            this.iRename = new DevExpress.XtraBars.BarButtonItem();
            this.iCloseAllFiles = new DevExpress.XtraBars.BarButtonItem();
            this.iCloseAllExistingFiles = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAddFileToProject = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.SaveOutputButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.OpenOutputButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.ClearOutputButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonShowFileSystemMenu = new DevExpress.XtraBars.BarButtonItem();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.defaultEditPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.xtraTabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.popupMenuTab = new DevExpress.XtraBars.PopupMenu(this.components);
            this.iToggleFullscreen = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuOutput = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).BeginInit();
            this.popupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockingComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxExecuters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxConnections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.hideContainerLeft.SuspendLayout();
            this.dockPanelToolbox.SuspendLayout();
            this.dockPanel6_Container.SuspendLayout();
            this.dockPanelActionList.SuspendLayout();
            this.controlContainer6.SuspendLayout();
            this.dockPanelRecentFiles.SuspendLayout();
            this.controlContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.dockPanelOpenFiles.SuspendLayout();
            this.controlContainer4.SuspendLayout();
            this.contextMenuStripOpenFileExplorer.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanelProperties.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearchProperties.Properties)).BeginInit();
            this.dockPanelSolutionExplorer.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanelFileExplorer.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            this.dockPanelFtpExplorer.SuspendLayout();
            this.controlContainer7.SuspendLayout();
            this.panelContainer2.SuspendLayout();
            this.dockPanelFindResults.SuspendLayout();
            this.dockPanel4_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFindResult.Properties)).BeginInit();
            this.dockPanelTaskList.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            this.dockPanelOutPut.SuspendLayout();
            this.dockPanel5_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOutPut.Properties)).BeginInit();
            this.dockPanelMessageLog.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            this.dockPanelCmd.SuspendLayout();
            this.controlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defaultEditPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.MainMenuBar,
            this.bar2,
            this.bar3,
            this.bar4,
            this.bar5,
            this.bar6,
            this.barFtpConnections});
            this.barManager.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Built-in Menus", new System.Guid("a984a9d9-f96f-425a-8857-fe4de6df48c2")),
            new DevExpress.XtraBars.BarManagerCategory("File", new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52")),
            new DevExpress.XtraBars.BarManagerCategory("Edit", new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe")),
            new DevExpress.XtraBars.BarManagerCategory("Standard", new System.Guid("fbaaf85d-943d-4ccd-8517-fc398efe9c7b")),
            new DevExpress.XtraBars.BarManagerCategory("View", new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4")),
            new DevExpress.XtraBars.BarManagerCategory("Window", new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01")),
            new DevExpress.XtraBars.BarManagerCategory("Status", new System.Guid("d3532f9f-c716-4c40-8731-d110e1a41e64")),
            new DevExpress.XtraBars.BarManagerCategory("Layouts", new System.Guid("f2b2eae8-5b98-43eb-81aa-d999b20fd3d3")),
            new DevExpress.XtraBars.BarManagerCategory("PaintStyles", new System.Guid("d0a113b2-425b-47f5-a6b5-0aefb1859507"))});
            this.barManager.Controller = this.barAndDockingController;
            this.barManager.DockControls.Add(this.barDockControl1);
            this.barManager.DockControls.Add(this.barDockControl2);
            this.barManager.DockControls.Add(this.barDockControl3);
            this.barManager.DockControls.Add(this.barDockControl4);
            this.barManager.DockManager = this.dockManager;
            this.barManager.Form = this;
            this.barManager.Images = this.imageListMain;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.iFile,
            this.iEdit,
            this.siNew,
            this.iOpen,
            this.iClose,
            this.iCreateNewProject,
            this.siFile,
            this.iCreateEmptyProject,
            this.iAddNewItem,
            this.iAddExistingItem,
            this.iAddProject,
            this.iAddExistingDirectory,
            this.iSaveAll,
            this.iOpenProject,
            this.iCloseProject,
            this.iPageSetup,
            this.iPrint,
            this.iExit,
            this.iUndo,
            this.iRedo,
            this.iCut,
            this.iCopy,
            this.iPaste,
            this.iCycle,
            this.iSelectAll,
            this.siFind,
            this.iFind,
            this.iReplace,
            this.iFindinFiles,
            this.iNew,
            this.iOpenFile,
            this.iSave,
            this.iStart,
            this.eExecuter,
            this.eFind,
            this.iSolutionExplorer,
            this.iProperties,
            this.iToolbox,
            this.iClassView,
            this.iTaskList,
            this.iFindResults,
            this.iOutput,
            this.iCascade,
            this.iHorizontal,
            this.iVertical,
            this.BarMdiChildrenListItem1,
            this.iWindow,
            this.iStatus1,
            this.iDeleteLayout,
            this.iSaveLayout,
            this.siLayouts,
            this.biTabbedMDI,
            this.beiDockMode,
            this.iInsert,
            this.barButtonItem1,
            this.iView,
            this.iClipboard,
            this.iTools,
            this.iHelp,
            this.iCloseAll,
            this.iAbout,
            this.iSaveAs,
            this.iCloseAllButThis,
            this.iCopyFullPath,
            this.iOpenContainingFolder,
            this.iFileInformation,
            this.iRecentFiles,
            this.iRename,
            this.barButtonItem3,
            this.iEditWith,
            this.iSelectApplication,
            this.iCloseAllFiles,
            this.iCloseAllExistingFiles,
            this.iSaveAllExistingFiles,
            this.barButtonItem2,
            this.iExtras,
            this.iBrowser,
            this.iDOSCommand,
            this.iStartNewInstance,
            this.iLastDosCommand,
            this.barSubItem1,
            this.barButtonItemAddFileToProject,
            this.iExportProject,
            this.barButtonItem4,
            this.iRecentProjects,
            this.barSubItem2,
            this.progressBar,
            this.labelAction,
            this.iOpenFromFtp,
            this.barButtonItemConnectTo,
            this.barEditItemConnections,
            this.barSubItem3,
            this.iSaveToFtp,
            this.barButtonItemAsToolWindow,
            this.barSubItem4,
            this.iUploadProject,
            this.progressBarMarquee,
            this.SaveOutputButtonItem,
            this.OpenOutputButtonItem,
            this.ClearOutputButtonItem,
            this.barButtonItemSendError,
            this.barButtonShowFileSystemMenu});
            this.barManager.MainMenu = this.MainMenuBar;
            this.barManager.MaxItemId = 137;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ComboBoxExecuters,
            this.searchItemComboBox1,
            this.dockingComboBox,
            this.repositoryItemProgressBar1,
            this.repositoryItemComboBoxConnections,
            this.repositoryItemMarqueeProgressBar1});
            this.barManager.StatusBar = this.bar4;
            this.barManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarManager1ItemClick);
            // 
            // MainMenuBar
            // 
            this.MainMenuBar.BarName = "MainMenu";
            this.MainMenuBar.DockCol = 0;
            this.MainMenuBar.DockRow = 0;
            this.MainMenuBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.MainMenuBar.FloatLocation = new System.Drawing.Point(92, 126);
            this.MainMenuBar.FloatSize = new System.Drawing.Size(29, 21);
            this.MainMenuBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.iEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.iView),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.iInsert),
            new DevExpress.XtraBars.LinkPersistInfo(this.iTools),
            new DevExpress.XtraBars.LinkPersistInfo(this.iExtras),
            new DevExpress.XtraBars.LinkPersistInfo(this.iWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.iHelp)});
            this.MainMenuBar.OptionsBar.AllowQuickCustomization = false;
            this.MainMenuBar.OptionsBar.DrawDragBorder = false;
            this.MainMenuBar.OptionsBar.MultiLine = true;
            this.MainMenuBar.OptionsBar.UseWholeRow = true;
            this.MainMenuBar.Text = "MainMenu";
            // 
            // iFile
            // 
            this.iFile.Caption = "&File";
            this.iFile.CategoryGuid = new System.Guid("a984a9d9-f96f-425a-8857-fe4de6df48c2");
            this.iFile.Id = 0;
            this.iFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.siNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClose, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRecentFiles, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRecentProjects),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.iPageSetup, "", true, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.iPrint, "", false, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(this.iExit, true)});
            this.iFile.Name = "iFile";
            // 
            // siNew
            // 
            this.siNew.Caption = "&New";
            this.siNew.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.siNew.Id = 2;
            this.siNew.ImageIndex = 10;
            this.siNew.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iCreateNewProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.siFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCreateEmptyProject)});
            this.siNew.Name = "siNew";
            // 
            // iCreateNewProject
            // 
            this.iCreateNewProject.Caption = "New &Project...";
            this.iCreateNewProject.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iCreateNewProject.Hint = "New Project";
            this.iCreateNewProject.Id = 5;
            this.iCreateNewProject.ImageIndex = 0;
            this.iCreateNewProject.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.N));
            this.iCreateNewProject.Name = "iCreateNewProject";
            this.iCreateNewProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNewProjectClick);
            // 
            // siFile
            // 
            this.siFile.Caption = "New &File...";
            this.siFile.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.siFile.Hint = "New File";
            this.siFile.Id = 6;
            this.siFile.ImageIndex = 1;
            this.siFile.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.siFile.Name = "siFile";
            this.siFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNewFileClick);
            // 
            // iCreateEmptyProject
            // 
            this.iCreateEmptyProject.Caption = "&Empty Project...";
            this.iCreateEmptyProject.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iCreateEmptyProject.Hint = "Create Empty Project";
            this.iCreateEmptyProject.Id = 7;
            this.iCreateEmptyProject.ImageIndex = 2;
            this.iCreateEmptyProject.Name = "iCreateEmptyProject";
            this.iCreateEmptyProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNewBlankProjectClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Open";
            this.barSubItem2.Id = 115;
            this.barSubItem2.ImageIndex = 57;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpenProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpenFromFtp, true)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // iOpen
            // 
            this.iOpen.Caption = "&Open File";
            this.iOpen.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iOpen.Id = 3;
            this.iOpen.ImageIndex = 20;
            this.iOpen.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.iOpen.Name = "iOpen";
            this.iOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "&Quick Open";
            this.barButtonItem3.Id = 97;
            this.barButtonItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem3ItemClick);
            // 
            // iOpenProject
            // 
            this.iOpenProject.Caption = "Op&en Project...";
            this.iOpenProject.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iOpenProject.Id = 14;
            this.iOpenProject.ImageIndex = 5;
            this.iOpenProject.Name = "iOpenProject";
            this.iOpenProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenProjectItemClick);
            // 
            // iOpenFromFtp
            // 
            this.iOpenFromFtp.Caption = "Open From FTP...";
            this.iOpenFromFtp.Id = 119;
            this.iOpenFromFtp.ImageIndex = 56;
            this.iOpenFromFtp.Name = "iOpenFromFtp";
            this.iOpenFromFtp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenFromFtpItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "Save";
            this.barSubItem3.Id = 124;
            this.barSubItem3.ImageIndex = 7;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAllExistingFiles),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveToFtp, true)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // iSave
            // 
            this.iSave.Caption = "&Save";
            this.iSave.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iSave.Enabled = false;
            this.iSave.Hint = "Save";
            this.iSave.Id = 33;
            this.iSave.ImageIndex = 21;
            this.iSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.iSave.Name = "iSave";
            this.iSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ISaveItemClick);
            // 
            // iSaveAs
            // 
            this.iSaveAs.Caption = "Sa&ve As...";
            this.iSaveAs.Enabled = false;
            this.iSaveAs.Id = 89;
            this.iSaveAs.ImageIndex = 52;
            this.iSaveAs.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
                | System.Windows.Forms.Keys.S));
            this.iSaveAs.Name = "iSaveAs";
            this.iSaveAs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2ItemClick);
            // 
            // iSaveAll
            // 
            this.iSaveAll.Caption = "Save A&ll";
            this.iSaveAll.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iSaveAll.Hint = "Save All";
            this.iSaveAll.Id = 13;
            this.iSaveAll.ImageIndex = 51;
            this.iSaveAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.S));
            this.iSaveAll.Name = "iSaveAll";
            this.iSaveAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ISaveAllItemClick);
            // 
            // iSaveAllExistingFiles
            // 
            this.iSaveAllExistingFiles.Caption = "Save All Existing Files";
            this.iSaveAllExistingFiles.Id = 102;
            this.iSaveAllExistingFiles.ImageIndex = 7;
            this.iSaveAllExistingFiles.Name = "iSaveAllExistingFiles";
            this.iSaveAllExistingFiles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ISaveAllExistingFilesItemClick);
            // 
            // iSaveToFtp
            // 
            this.iSaveToFtp.Caption = "Save to FTP...";
            this.iSaveToFtp.Id = 125;
            this.iSaveToFtp.ImageIndex = 58;
            this.iSaveToFtp.Name = "iSaveToFtp";
            this.iSaveToFtp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveToFtpItemClick);
            // 
            // iClose
            // 
            this.iClose.Caption = "&Close";
            this.iClose.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iClose.Id = 4;
            this.iClose.ImageIndex = 47;
            this.iClose.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4));
            this.iClose.Name = "iClose";
            this.iClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ICloseItemClick);
            // 
            // iCloseAll
            // 
            this.iCloseAll.Caption = "Close &All";
            this.iCloseAll.Id = 86;
            this.iCloseAll.ImageIndex = 48;
            this.iCloseAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.F4));
            this.iCloseAll.Name = "iCloseAll";
            this.iCloseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ICloseAllItemClick);
            // 
            // iCloseProject
            // 
            this.iCloseProject.Caption = "Close Pro&ject";
            this.iCloseProject.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iCloseProject.Enabled = false;
            this.iCloseProject.Id = 15;
            this.iCloseProject.ImageIndex = 6;
            this.iCloseProject.Name = "iCloseProject";
            this.iCloseProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CloseProjectItemClick);
            // 
            // iRecentFiles
            // 
            this.iRecentFiles.Caption = "Recent &Files";
            this.iRecentFiles.Id = 95;
            this.iRecentFiles.Name = "iRecentFiles";
            // 
            // iRecentProjects
            // 
            this.iRecentProjects.Caption = "Recent &Projects";
            this.iRecentProjects.Id = 114;
            this.iRecentProjects.Name = "iRecentProjects";
            // 
            // iAddProject
            // 
            this.iAddProject.Caption = "A&dd to Project";
            this.iAddProject.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iAddProject.Enabled = false;
            this.iAddProject.Id = 10;
            this.iAddProject.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddNewItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddExistingItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddExistingDirectory)});
            this.iAddProject.Name = "iAddProject";
            // 
            // iAddNewItem
            // 
            this.iAddNewItem.Caption = "Add Ne&w Item...";
            this.iAddNewItem.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iAddNewItem.Enabled = false;
            this.iAddNewItem.Hint = "Add New Item";
            this.iAddNewItem.Id = 8;
            this.iAddNewItem.ImageIndex = 3;
            this.iAddNewItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.A));
            this.iAddNewItem.Name = "iAddNewItem";
            this.iAddNewItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AddNewItemItemClick);
            // 
            // iAddExistingItem
            // 
            this.iAddExistingItem.Caption = "Add Existin&g Item...";
            this.iAddExistingItem.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iAddExistingItem.Enabled = false;
            this.iAddExistingItem.Id = 9;
            this.iAddExistingItem.ImageIndex = 4;
            this.iAddExistingItem.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.B));
            this.iAddExistingItem.Name = "iAddExistingItem";
            this.iAddExistingItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AddExistingItemItemClick);
            // 
            // iAddExistingDirectory
            // 
            this.iAddExistingDirectory.Caption = "&Add Existing Directory...";
            this.iAddExistingDirectory.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iAddExistingDirectory.Enabled = false;
            this.iAddExistingDirectory.Hint = "New Project";
            this.iAddExistingDirectory.Id = 11;
            this.iAddExistingDirectory.Name = "iAddExistingDirectory";
            this.iAddExistingDirectory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AddExistingDirectoryItemClick);
            // 
            // iPageSetup
            // 
            this.iPageSetup.Caption = "Page Set&up...";
            this.iPageSetup.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iPageSetup.Id = 16;
            this.iPageSetup.ImageIndex = 8;
            this.iPageSetup.Name = "iPageSetup";
            // 
            // iPrint
            // 
            this.iPrint.Caption = "&Print...";
            this.iPrint.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iPrint.Id = 17;
            this.iPrint.ImageIndex = 9;
            this.iPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.iPrint.Name = "iPrint";
            // 
            // iExit
            // 
            this.iExit.Caption = "E&xit";
            this.iExit.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iExit.Id = 18;
            this.iExit.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4));
            this.iExit.Name = "iExit";
            this.iExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExitItemClick);
            // 
            // iEdit
            // 
            this.iEdit.Caption = "&Edit";
            this.iEdit.CategoryGuid = new System.Guid("a984a9d9-f96f-425a-8857-fe4de6df48c2");
            this.iEdit.Id = 1;
            this.iEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iUndo),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRedo),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.iPaste),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.iCycle, "", false, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClipboard),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSelectAll, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.siFind, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.iEditWith, "", true, false, true, 0)});
            this.iEdit.Name = "iEdit";
            // 
            // iUndo
            // 
            this.iUndo.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.iUndo.Caption = "&Undo";
            this.iUndo.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iUndo.Enabled = false;
            this.iUndo.Hint = "Undo";
            this.iUndo.Id = 19;
            this.iUndo.ImageIndex = 11;
            this.iUndo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.iUndo.Name = "iUndo";
            this.iUndo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UndoItemClick);
            // 
            // iRedo
            // 
            this.iRedo.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.iRedo.Caption = "&Redo";
            this.iRedo.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iRedo.Enabled = false;
            this.iRedo.Hint = "Redo";
            this.iRedo.Id = 20;
            this.iRedo.ImageIndex = 12;
            this.iRedo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.iRedo.Name = "iRedo";
            this.iRedo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RedoItemClick);
            // 
            // iCut
            // 
            this.iCut.Caption = "Cu&t";
            this.iCut.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iCut.Hint = "Cut";
            this.iCut.Id = 21;
            this.iCut.ImageIndex = 13;
            this.iCut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.iCut.Name = "iCut";
            this.iCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CutItemClick);
            // 
            // iCopy
            // 
            this.iCopy.Caption = "&Copy";
            this.iCopy.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iCopy.Hint = "Copy";
            this.iCopy.Id = 22;
            this.iCopy.ImageIndex = 14;
            this.iCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.iCopy.Name = "iCopy";
            this.iCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CopyItemClick);
            // 
            // iPaste
            // 
            this.iPaste.Caption = "&Paste";
            this.iPaste.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iPaste.Enabled = false;
            this.iPaste.Hint = "Paste";
            this.iPaste.Id = 23;
            this.iPaste.ImageIndex = 15;
            this.iPaste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
            this.iPaste.Name = "iPaste";
            this.iPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PasteItemClick);
            // 
            // iCycle
            // 
            this.iCycle.Caption = "C&ycle Clipboard Ring";
            this.iCycle.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iCycle.Id = 25;
            this.iCycle.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.V));
            this.iCycle.Name = "iCycle";
            // 
            // iClipboard
            // 
            this.iClipboard.Caption = "Select from &Clipboard...";
            this.iClipboard.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iClipboard.Id = 79;
            this.iClipboard.ItemShortcut = new DevExpress.XtraBars.BarShortcut((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.Alt) 
                | System.Windows.Forms.Keys.V));
            this.iClipboard.Name = "iClipboard";
            this.iClipboard.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ClipboardItemClick);
            // 
            // iSelectAll
            // 
            this.iSelectAll.Caption = "Select &All";
            this.iSelectAll.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iSelectAll.Id = 26;
            this.iSelectAll.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
            this.iSelectAll.Name = "iSelectAll";
            this.iSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SelectAllItemClick);
            // 
            // siFind
            // 
            this.siFind.Caption = "&Find and Replace";
            this.siFind.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.siFind.Id = 27;
            this.siFind.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iFind),
            new DevExpress.XtraBars.LinkPersistInfo(this.iReplace),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFindinFiles)});
            this.siFind.Name = "siFind";
            // 
            // iFind
            // 
            this.iFind.Caption = "&Find";
            this.iFind.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iFind.Id = 28;
            this.iFind.ImageIndex = 16;
            this.iFind.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.iFind.Name = "iFind";
            this.iFind.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iFind_ItemClick);
            // 
            // iReplace
            // 
            this.iReplace.Caption = "R&eplace";
            this.iReplace.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iReplace.Id = 29;
            this.iReplace.ImageIndex = 17;
            this.iReplace.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H));
            this.iReplace.Name = "iReplace";
            // 
            // iFindinFiles
            // 
            this.iFindinFiles.Caption = "F&ind in Files";
            this.iFindinFiles.CategoryGuid = new System.Guid("ac82dbe7-c530-4aa2-b6de-94a7777426fe");
            this.iFindinFiles.Hint = "Find in Files";
            this.iFindinFiles.Id = 30;
            this.iFindinFiles.ImageIndex = 18;
            this.iFindinFiles.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.F));
            this.iFindinFiles.Name = "iFindinFiles";
            // 
            // iEditWith
            // 
            this.iEditWith.Caption = "Edit with";
            this.iEditWith.Id = 98;
            this.iEditWith.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iSelectApplication)});
            this.iEditWith.Name = "iEditWith";
            // 
            // iSelectApplication
            // 
            this.iSelectApplication.Caption = "Select other Application...";
            this.iSelectApplication.Id = 99;
            this.iSelectApplication.Name = "iSelectApplication";
            // 
            // iView
            // 
            this.iView.Caption = "&View";
            this.iView.Id = 77;
            this.iView.Name = "iView";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Project";
            this.barSubItem1.Id = 110;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iCreateNewProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpenProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddNewItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddExistingItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddExistingDirectory),
            new DevExpress.XtraBars.LinkPersistInfo(this.iExportProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iUploadProject)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // iExportProject
            // 
            this.iExportProject.Caption = "Export Project...";
            this.iExportProject.Enabled = false;
            this.iExportProject.Id = 112;
            this.iExportProject.Name = "iExportProject";
            this.iExportProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExportProjectItemClick);
            // 
            // iUploadProject
            // 
            this.iUploadProject.Caption = "Upload Project to FTP...";
            this.iUploadProject.Enabled = false;
            this.iUploadProject.Id = 128;
            this.iUploadProject.ImageIndex = 56;
            this.iUploadProject.Name = "iUploadProject";
            this.iUploadProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.UploadProjectItemClick);
            // 
            // iInsert
            // 
            this.iInsert.Caption = "&Insert";
            this.iInsert.Id = 71;
            this.iInsert.Name = "iInsert";
            // 
            // iTools
            // 
            this.iTools.Caption = "&Tools";
            this.iTools.Id = 82;
            this.iTools.Name = "iTools";
            // 
            // iExtras
            // 
            this.iExtras.Caption = "E&xtras";
            this.iExtras.Id = 104;
            this.iExtras.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iStartNewInstance),
            new DevExpress.XtraBars.LinkPersistInfo(this.iBrowser, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iDOSCommand, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iLastDosCommand),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4, true)});
            this.iExtras.Name = "iExtras";
            // 
            // iStartNewInstance
            // 
            this.iStartNewInstance.Caption = "Start new Instance of CompleX";
            this.iStartNewInstance.Id = 105;
            this.iStartNewInstance.ImageIndex = 55;
            this.iStartNewInstance.Name = "iStartNewInstance";
            this.iStartNewInstance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.IStartNewInstanceItemClick);
            // 
            // iBrowser
            // 
            this.iBrowser.Caption = "Browser";
            this.iBrowser.Id = 107;
            this.iBrowser.ImageIndex = 54;
            this.iBrowser.Name = "iBrowser";
            this.iBrowser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BrowserItemClick);
            // 
            // iDOSCommand
            // 
            this.iDOSCommand.Caption = "DOS Command...";
            this.iDOSCommand.Id = 108;
            this.iDOSCommand.ImageIndex = 44;
            this.iDOSCommand.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.iDOSCommand.Name = "iDOSCommand";
            this.iDOSCommand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DosCommandItemClick);
            // 
            // iLastDosCommand
            // 
            this.iLastDosCommand.Caption = "Last DOS Command";
            this.iLastDosCommand.Id = 109;
            this.iLastDosCommand.ImageIndex = 44;
            this.iLastDosCommand.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9));
            this.iLastDosCommand.Name = "iLastDosCommand";
            this.iLastDosCommand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.LastDosCommandItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Manage FTP Accounts...";
            this.barButtonItem4.Id = 113;
            this.barButtonItem4.ImageIndex = 56;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonFtpSitesClick);
            // 
            // iWindow
            // 
            this.iWindow.Caption = "&Window";
            this.iWindow.CategoryGuid = new System.Guid("a984a9d9-f96f-425a-8857-fe4de6df48c2");
            this.iWindow.Id = 45;
            this.iWindow.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biTabbedMDI),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCascade, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iHorizontal),
            new DevExpress.XtraBars.LinkPersistInfo(this.iVertical),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAsToolWindow, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarMdiChildrenListItem1, true)});
            this.iWindow.Name = "iWindow";
            // 
            // biTabbedMDI
            // 
            this.biTabbedMDI.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.biTabbedMDI.Caption = "Use TabbedMDI";
            this.biTabbedMDI.CategoryGuid = new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01");
            this.biTabbedMDI.Down = true;
            this.biTabbedMDI.Id = 66;
            this.biTabbedMDI.Name = "biTabbedMDI";
            this.biTabbedMDI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BiTabbedMdiItemClick);
            // 
            // iCascade
            // 
            this.iCascade.Caption = "&Cascade";
            this.iCascade.CategoryGuid = new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01");
            this.iCascade.Id = 41;
            this.iCascade.ImageIndex = 30;
            this.iCascade.Name = "iCascade";
            this.iCascade.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CascadeItemClick);
            // 
            // iHorizontal
            // 
            this.iHorizontal.Caption = "Tile &Horizontal";
            this.iHorizontal.CategoryGuid = new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01");
            this.iHorizontal.Id = 42;
            this.iHorizontal.ImageIndex = 31;
            this.iHorizontal.Name = "iHorizontal";
            this.iHorizontal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.HorizontalItemClick);
            // 
            // iVertical
            // 
            this.iVertical.Caption = "Tile &Vertical";
            this.iVertical.CategoryGuid = new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01");
            this.iVertical.Id = 43;
            this.iVertical.ImageIndex = 32;
            this.iVertical.Name = "iVertical";
            this.iVertical.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.VerticalItemClick);
            // 
            // barButtonItemAsToolWindow
            // 
            this.barButtonItemAsToolWindow.Caption = "As Toolwindow";
            this.barButtonItemAsToolWindow.Id = 126;
            this.barButtonItemAsToolWindow.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W), System.Windows.Forms.Keys.C);
            this.barButtonItemAsToolWindow.Name = "barButtonItemAsToolWindow";
            this.barButtonItemAsToolWindow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemAsToolWindowItemClick);
            // 
            // BarMdiChildrenListItem1
            // 
            this.BarMdiChildrenListItem1.Caption = "Window";
            this.BarMdiChildrenListItem1.CategoryGuid = new System.Guid("faa74de1-bd23-44b9-955d-6ba635fa0f01");
            this.BarMdiChildrenListItem1.Id = 44;
            this.BarMdiChildrenListItem1.Name = "BarMdiChildrenListItem1";
            // 
            // iHelp
            // 
            this.iHelp.Caption = "&Help";
            this.iHelp.Id = 83;
            this.iHelp.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSendError),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAbout, true)});
            this.iHelp.Name = "iHelp";
            // 
            // barButtonItemSendError
            // 
            this.barButtonItemSendError.Caption = "Send Feedback, Errors or wishes ...";
            this.barButtonItemSendError.Id = 135;
            this.barButtonItemSendError.Name = "barButtonItemSendError";
            this.barButtonItemSendError.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSendError_ItemClick);
            // 
            // iAbout
            // 
            this.iAbout.Caption = "About CompleX Studio...";
            this.iAbout.Id = 87;
            this.iAbout.ImageIndex = 53;
            this.iAbout.Name = "iAbout";
            this.iAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AboutItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Standard";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 1;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(38, 139);
            this.bar2.FloatSize = new System.Drawing.Size(29, 21);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddNewItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpenFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.iPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.iUndo, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRedo),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFindinFiles, true),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.eFind, "", false, true, true, 97)});
            this.bar2.Text = "Standard";
            // 
            // iNew
            // 
            this.iNew.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.iNew.Caption = "New Project";
            this.iNew.CategoryGuid = new System.Guid("fbaaf85d-943d-4ccd-8517-fc398efe9c7b");
            this.iNew.DropDownControl = this.popupMenuNew;
            this.iNew.Hint = "New Project";
            this.iNew.Id = 31;
            this.iNew.ImageIndex = 10;
            this.iNew.Name = "iNew";
            this.iNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ItemNewProjectClick);
            // 
            // popupMenuNew
            // 
            this.popupMenuNew.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iCreateNewProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.siFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCreateEmptyProject)});
            this.popupMenuNew.Manager = this.barManager;
            this.popupMenuNew.Name = "popupMenuNew";
            // 
            // iOpenFile
            // 
            this.iOpenFile.Caption = "Open File";
            this.iOpenFile.CategoryGuid = new System.Guid("ec880574-4d2a-4f26-8779-903acfad8a52");
            this.iOpenFile.Hint = "Open File";
            this.iOpenFile.Id = 32;
            this.iOpenFile.ImageIndex = 20;
            this.iOpenFile.Name = "iOpenFile";
            this.iOpenFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenItemClick);
            // 
            // eFind
            // 
            this.eFind.Caption = "Find";
            this.eFind.CategoryGuid = new System.Guid("fbaaf85d-943d-4ccd-8517-fc398efe9c7b");
            this.eFind.Edit = this.searchItemComboBox1;
            this.eFind.Hint = "Find";
            this.eFind.Id = 36;
            this.eFind.Name = "eFind";
            this.eFind.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            // 
            // searchItemComboBox1
            // 
            this.searchItemComboBox1.AllowFocused = false;
            this.searchItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchItemComboBox1.Name = "searchItemComboBox1";
            this.searchItemComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RepositoryItemComboBox1KeyDown);
            // 
            // bar3
            // 
            this.bar3.BarName = "View";
            this.bar3.DockCol = 1;
            this.bar3.DockRow = 1;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.FloatLocation = new System.Drawing.Point(36, 181);
            this.bar3.FloatSize = new System.Drawing.Size(38, 161);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iSolutionExplorer),
            new DevExpress.XtraBars.LinkPersistInfo(this.iProperties),
            new DevExpress.XtraBars.LinkPersistInfo(this.iToolbox),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClassView),
            new DevExpress.XtraBars.LinkPersistInfo(this.iTaskList, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFindResults),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOutput)});
            this.bar3.Text = "View";
            // 
            // iSolutionExplorer
            // 
            this.iSolutionExplorer.Caption = "Solution Ex&plorer";
            this.iSolutionExplorer.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iSolutionExplorer.Hint = "Solution Explorer";
            this.iSolutionExplorer.Id = 37;
            this.iSolutionExplorer.ImageIndex = 23;
            this.iSolutionExplorer.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.L));
            this.iSolutionExplorer.Name = "iSolutionExplorer";
            this.iSolutionExplorer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SolutionExplorerItemClick);
            // 
            // iProperties
            // 
            this.iProperties.Caption = "Properties &Window";
            this.iProperties.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iProperties.Hint = "Properties Window";
            this.iProperties.Id = 38;
            this.iProperties.ImageIndex = 24;
            this.iProperties.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.iProperties.Name = "iProperties";
            this.iProperties.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PropertiesItemClick);
            // 
            // iToolbox
            // 
            this.iToolbox.Caption = "Toolbo&x";
            this.iToolbox.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iToolbox.Hint = "Toolbox";
            this.iToolbox.Id = 39;
            this.iToolbox.ImageIndex = 25;
            this.iToolbox.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.X));
            this.iToolbox.Name = "iToolbox";
            this.iToolbox.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolboxItemClick);
            // 
            // iClassView
            // 
            this.iClassView.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.iClassView.Caption = "Cl&ass View";
            this.iClassView.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iClassView.DropDownControl = this.popupControlContainer1;
            this.iClassView.Hint = "Class View";
            this.iClassView.Id = 40;
            this.iClassView.ImageIndex = 26;
            this.iClassView.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.C));
            this.iClassView.Name = "iClassView";
            // 
            // popupControlContainer1
            // 
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupControlContainer1.Controls.Add(this.treeView1);
            this.popupControlContainer1.Location = new System.Drawing.Point(91, 125);
            this.popupControlContainer1.Manager = this.barManager;
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Size = new System.Drawing.Size(265, 237);
            this.popupControlContainer1.TabIndex = 5;
            this.popupControlContainer1.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageListSE;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(265, 237);
            this.treeView1.TabIndex = 0;
            // 
            // imageListSE
            // 
            this.imageListSE.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSE.ImageStream")));
            this.imageListSE.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListSE.Images.SetKeyName(0, "");
            this.imageListSE.Images.SetKeyName(1, "");
            this.imageListSE.Images.SetKeyName(2, "");
            this.imageListSE.Images.SetKeyName(3, "");
            this.imageListSE.Images.SetKeyName(4, "");
            this.imageListSE.Images.SetKeyName(5, "");
            this.imageListSE.Images.SetKeyName(6, "");
            this.imageListSE.Images.SetKeyName(7, "");
            this.imageListSE.Images.SetKeyName(8, "");
            this.imageListSE.Images.SetKeyName(9, "");
            this.imageListSE.Images.SetKeyName(10, "");
            // 
            // iTaskList
            // 
            this.iTaskList.Caption = "Task List";
            this.iTaskList.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iTaskList.Hint = "Task List";
            this.iTaskList.Id = 68;
            this.iTaskList.ImageIndex = 27;
            this.iTaskList.Name = "iTaskList";
            this.iTaskList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.TaskListItemClick);
            // 
            // iFindResults
            // 
            this.iFindResults.Caption = "Find Results";
            this.iFindResults.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iFindResults.Hint = "Find Results";
            this.iFindResults.Id = 69;
            this.iFindResults.ImageIndex = 28;
            this.iFindResults.Name = "iFindResults";
            this.iFindResults.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FindResultsItemClick);
            // 
            // iOutput
            // 
            this.iOutput.Caption = "Output";
            this.iOutput.CategoryGuid = new System.Guid("0cb4cc3e-4798-4d61-9457-672bdc2a90d4");
            this.iOutput.Hint = "Output";
            this.iOutput.Id = 70;
            this.iOutput.ImageIndex = 29;
            this.iOutput.Name = "iOutput";
            this.iOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OutputItemClick);
            // 
            // bar4
            // 
            this.bar4.BarName = "StatusBar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.FloatLocation = new System.Drawing.Point(25, 282);
            this.bar4.FloatSize = new System.Drawing.Size(29, 21);
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iStatus1),
            new DevExpress.XtraBars.LinkPersistInfo(this.labelAction),
            new DevExpress.XtraBars.LinkPersistInfo(this.progressBar),
            new DevExpress.XtraBars.LinkPersistInfo(this.progressBarMarquee)});
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.DrawSizeGrip = true;
            this.bar4.OptionsBar.RotateWhenVertical = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "StatusBar";
            // 
            // iStatus1
            // 
            this.iStatus1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.iStatus1.Caption = "Ready";
            this.iStatus1.CategoryGuid = new System.Guid("d3532f9f-c716-4c40-8731-d110e1a41e64");
            this.iStatus1.Id = 50;
            this.iStatus1.Name = "iStatus1";
            this.iStatus1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.iStatus1.Width = 695;
            // 
            // labelAction
            // 
            this.labelAction.Caption = "Action";
            this.labelAction.Id = 117;
            this.labelAction.Name = "labelAction";
            this.labelAction.TextAlignment = System.Drawing.StringAlignment.Near;
            this.labelAction.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // progressBar
            // 
            this.progressBar.Caption = "ProgressBar";
            this.progressBar.Edit = this.repositoryItemProgressBar1;
            this.progressBar.Id = 116;
            this.progressBar.Name = "progressBar";
            this.progressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBar.Width = 90;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // progressBarMarquee
            // 
            this.progressBarMarquee.Caption = "marqueeBar";
            this.progressBarMarquee.Edit = this.repositoryItemMarqueeProgressBar1;
            this.progressBarMarquee.Id = 129;
            this.progressBarMarquee.Name = "progressBarMarquee";
            this.progressBarMarquee.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBarMarquee.Width = 90;
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // bar5
            // 
            this.bar5.BarName = "Layouts";
            this.bar5.DockCol = 1;
            this.bar5.DockRow = 2;
            this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar5.FloatLocation = new System.Drawing.Point(159, 118);
            this.bar5.FloatSize = new System.Drawing.Size(29, 21);
            this.bar5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.iDeleteLayout),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beiDockMode, "", false, true, true, 89)});
            this.bar5.Offset = 159;
            this.bar5.Text = "Layouts";
            // 
            // iSaveLayout
            // 
            this.iSaveLayout.Caption = "&Save Layout...";
            this.iSaveLayout.CategoryGuid = new System.Guid("f2b2eae8-5b98-43eb-81aa-d999b20fd3d3");
            this.iSaveLayout.Hint = "Save Layout";
            this.iSaveLayout.Id = 48;
            this.iSaveLayout.ImageIndex = 34;
            this.iSaveLayout.Name = "iSaveLayout";
            this.iSaveLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveLayoutItemClick);
            // 
            // iDeleteLayout
            // 
            this.iDeleteLayout.Caption = "&Delete Layout...";
            this.iDeleteLayout.CategoryGuid = new System.Guid("f2b2eae8-5b98-43eb-81aa-d999b20fd3d3");
            this.iDeleteLayout.Hint = "Delete Layout";
            this.iDeleteLayout.Id = 47;
            this.iDeleteLayout.ImageIndex = 40;
            this.iDeleteLayout.Name = "iDeleteLayout";
            this.iDeleteLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.DeleteLayoutItemClick);
            // 
            // beiDockMode
            // 
            this.beiDockMode.Caption = "Layout:";
            this.beiDockMode.Edit = this.dockingComboBox;
            this.beiDockMode.Id = 67;
            this.beiDockMode.Name = "beiDockMode";
            this.beiDockMode.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.beiDockMode.EditValueChanged += new System.EventHandler(this.BeiDockModeEditValueChanged);
            // 
            // dockingComboBox
            // 
            this.dockingComboBox.AutoHeight = false;
            this.dockingComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dockingComboBox.Name = "dockingComboBox";
            // 
            // bar6
            // 
            this.bar6.BarName = "Execute";
            this.bar6.DockCol = 0;
            this.bar6.DockRow = 2;
            this.bar6.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar6.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iStart),
            new DevExpress.XtraBars.LinkPersistInfo(this.eExecuter)});
            this.bar6.Offset = 1;
            this.bar6.Text = "Execute";
            // 
            // iStart
            // 
            this.iStart.Caption = "Start";
            this.iStart.CategoryGuid = new System.Guid("fbaaf85d-943d-4ccd-8517-fc398efe9c7b");
            this.iStart.Hint = "Start";
            this.iStart.Id = 34;
            this.iStart.ImageIndex = 22;
            this.iStart.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.iStart.Name = "iStart";
            this.iStart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.StartItemClick);
            // 
            // eExecuter
            // 
            this.eExecuter.Caption = "Executer";
            this.eExecuter.CategoryGuid = new System.Guid("fbaaf85d-943d-4ccd-8517-fc398efe9c7b");
            this.eExecuter.Edit = this.ComboBoxExecuters;
            this.eExecuter.EditValue = "Debug";
            this.eExecuter.Hint = "Execution Configuration";
            this.eExecuter.Id = 35;
            this.eExecuter.Name = "eExecuter";
            this.eExecuter.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            this.eExecuter.Width = 160;
            // 
            // ComboBoxExecuters
            // 
            this.ComboBoxExecuters.AllowFocused = false;
            this.ComboBoxExecuters.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ComboBoxExecuters.Name = "ComboBoxExecuters";
            // 
            // barFtpConnections
            // 
            this.barFtpConnections.BarName = "FtpConnections";
            this.barFtpConnections.DockCol = 2;
            this.barFtpConnections.DockRow = 2;
            this.barFtpConnections.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barFtpConnections.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemConnectTo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItemConnections)});
            this.barFtpConnections.Offset = 486;
            this.barFtpConnections.Text = "FtpConnections";
            // 
            // barButtonItemConnectTo
            // 
            this.barButtonItemConnectTo.Caption = "Connect";
            this.barButtonItemConnectTo.Id = 122;
            this.barButtonItemConnectTo.ImageIndex = 56;
            this.barButtonItemConnectTo.Name = "barButtonItemConnectTo";
            this.barButtonItemConnectTo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemConnectToItemClick);
            // 
            // barEditItemConnections
            // 
            this.barEditItemConnections.Caption = "connectionCombo";
            this.barEditItemConnections.Edit = this.repositoryItemComboBoxConnections;
            this.barEditItemConnections.Id = 123;
            this.barEditItemConnections.Name = "barEditItemConnections";
            this.barEditItemConnections.Width = 130;
            // 
            // repositoryItemComboBoxConnections
            // 
            this.repositoryItemComboBoxConnections.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.repositoryItemComboBoxConnections.AutoHeight = false;
            this.repositoryItemComboBoxConnections.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxConnections.Name = "repositoryItemComboBoxConnections";
            this.repositoryItemComboBoxConnections.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.barAndDockingController.PaintStyleName = "Skin";
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(768, 77);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 607);
            this.barDockControl2.Size = new System.Drawing.Size(768, 29);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 77);
            this.barDockControl3.Size = new System.Drawing.Size(0, 530);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(768, 77);
            this.barDockControl4.Size = new System.Drawing.Size(0, 530);
            // 
            // dockManager
            // 
            this.dockManager.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerLeft});
            this.dockManager.Controller = this.barAndDockingController;
            this.dockManager.Form = this;
            this.dockManager.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelActionList,
            this.dockPanelRecentFiles,
            this.dockPanelOpenFiles});
            this.dockManager.Images = this.imageListMain;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer2,
            this.panelContainer1});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            this.dockManager.ActivePanelChanged += new DevExpress.XtraBars.Docking.ActivePanelChangedEventHandler(this.DockManagerActivePanelChanged);
            this.dockManager.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.DockManagerVisibilityChanged);
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.hideContainerLeft.Controls.Add(this.dockPanelToolbox);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 77);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(22, 530);
            // 
            // dockPanelToolbox
            // 
            this.dockPanelToolbox.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelToolbox.Appearance.Options.UseBackColor = true;
            this.dockPanelToolbox.Controls.Add(this.dockPanel6_Container);
            this.dockPanelToolbox.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelToolbox.FloatSize = new System.Drawing.Size(146, 428);
            this.dockPanelToolbox.ID = new System.Guid("24977e30-0ea6-44aa-8fa4-9abaeb178b5e");
            this.dockPanelToolbox.ImageIndex = 25;
            this.dockPanelToolbox.Location = new System.Drawing.Point(0, 0);
            this.dockPanelToolbox.Name = "dockPanelToolbox";
            this.dockPanelToolbox.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanelToolbox.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelToolbox.SavedIndex = 2;
            this.dockPanelToolbox.Size = new System.Drawing.Size(200, 530);
            this.dockPanelToolbox.Text = "Toolbox";
            this.dockPanelToolbox.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel6_Container
            // 
            this.dockPanel6_Container.Controls.Add(this.toolBox);
            this.dockPanel6_Container.Location = new System.Drawing.Point(3, 24);
            this.dockPanel6_Container.Name = "dockPanel6_Container";
            this.dockPanel6_Container.Size = new System.Drawing.Size(194, 503);
            this.dockPanel6_Container.TabIndex = 0;
            // 
            // toolBox
            // 
            this.toolBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBox.Location = new System.Drawing.Point(0, 0);
            this.toolBox.Name = "toolBox";
            this.toolBox.Size = new System.Drawing.Size(194, 503);
            this.toolBox.TabIndex = 0;
            // 
            // dockPanelActionList
            // 
            this.dockPanelActionList.Controls.Add(this.controlContainer6);
            this.dockPanelActionList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelActionList.ID = new System.Guid("2663012b-ae90-422f-b2c5-280324d22762");
            this.dockPanelActionList.ImageIndex = 22;
            this.dockPanelActionList.Location = new System.Drawing.Point(22, 74);
            this.dockPanelActionList.Name = "dockPanelActionList";
            this.dockPanelActionList.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockPanelActionList.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelActionList.SavedIndex = 0;
            this.dockPanelActionList.Size = new System.Drawing.Size(200, 539);
            this.dockPanelActionList.Text = "Action List";
            this.dockPanelActionList.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer6
            // 
            this.controlContainer6.Controls.Add(this.actionListControl);
            this.controlContainer6.Location = new System.Drawing.Point(3, 24);
            this.controlContainer6.Name = "controlContainer6";
            this.controlContainer6.Size = new System.Drawing.Size(194, 512);
            this.controlContainer6.TabIndex = 0;
            // 
            // actionListControl
            // 
            this.actionListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionListControl.Location = new System.Drawing.Point(0, 0);
            this.actionListControl.Name = "actionListControl";
            this.actionListControl.Size = new System.Drawing.Size(194, 512);
            this.actionListControl.TabIndex = 0;
            // 
            // dockPanelRecentFiles
            // 
            this.dockPanelRecentFiles.Controls.Add(this.controlContainer5);
            this.dockPanelRecentFiles.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelRecentFiles.ID = new System.Guid("32f33aad-8f0f-4289-8eee-83036ebb2278");
            this.dockPanelRecentFiles.ImageIndex = 35;
            this.dockPanelRecentFiles.Location = new System.Drawing.Point(0, 0);
            this.dockPanelRecentFiles.Name = "dockPanelRecentFiles";
            this.dockPanelRecentFiles.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockPanelRecentFiles.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelRecentFiles.SavedIndex = 0;
            this.dockPanelRecentFiles.Size = new System.Drawing.Size(249, 537);
            this.dockPanelRecentFiles.Text = "Recent Files";
            this.dockPanelRecentFiles.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer5
            // 
            this.controlContainer5.Controls.Add(this.groupControl1);
            this.controlContainer5.Location = new System.Drawing.Point(3, 24);
            this.controlContainer5.Name = "controlContainer5";
            this.controlContainer5.Size = new System.Drawing.Size(243, 510);
            this.controlContainer5.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.recentFileListControl);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(243, 510);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Your recent open Files";
            // 
            // recentFileListControl
            // 
            this.recentFileListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentFileListControl.Location = new System.Drawing.Point(2, 22);
            this.recentFileListControl.Name = "recentFileListControl";
            this.recentFileListControl.Size = new System.Drawing.Size(239, 486);
            this.recentFileListControl.TabIndex = 1;
            // 
            // dockPanelOpenFiles
            // 
            this.dockPanelOpenFiles.Controls.Add(this.controlContainer4);
            this.dockPanelOpenFiles.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelOpenFiles.ID = new System.Guid("cc2cb879-430f-481f-96d2-3133765e6807");
            this.dockPanelOpenFiles.ImageIndex = 1;
            this.dockPanelOpenFiles.Location = new System.Drawing.Point(3, 24);
            this.dockPanelOpenFiles.Name = "dockPanelOpenFiles";
            this.dockPanelOpenFiles.OriginalSize = new System.Drawing.Size(0, 0);
            this.dockPanelOpenFiles.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelOpenFiles.SavedIndex = 4;
            this.dockPanelOpenFiles.SavedParent = this.panelContainer1;
            this.dockPanelOpenFiles.SavedTabbed = true;
            this.dockPanelOpenFiles.Size = new System.Drawing.Size(353, 288);
            this.dockPanelOpenFiles.Text = "Open Files";
            this.dockPanelOpenFiles.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // controlContainer4
            // 
            this.controlContainer4.Controls.Add(this.openFilesControl);
            this.controlContainer4.Location = new System.Drawing.Point(0, 0);
            this.controlContainer4.Name = "controlContainer4";
            this.controlContainer4.Size = new System.Drawing.Size(353, 288);
            this.controlContainer4.TabIndex = 0;
            // 
            // openFilesControl
            // 
            this.openFilesControl.ContextMenuStrip = this.contextMenuStripOpenFileExplorer;
            this.openFilesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openFilesControl.Location = new System.Drawing.Point(0, 0);
            this.openFilesControl.Name = "openFilesControl";
            this.openFilesControl.Size = new System.Drawing.Size(353, 288);
            this.openFilesControl.TabIndex = 0;
            // 
            // contextMenuStripOpenFileExplorer
            // 
            this.contextMenuStripOpenFileExplorer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.contextMenuStripOpenFileExplorer.Name = "contextMenuStripOpenFileExplorer";
            this.contextMenuStripOpenFileExplorer.Size = new System.Drawing.Size(104, 26);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::CompleX.Properties.Resources.schliessen_16;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanelProperties;
            this.panelContainer1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.panelContainer1.Appearance.Options.UseBackColor = true;
            this.panelContainer1.Controls.Add(this.dockPanelProperties);
            this.panelContainer1.Controls.Add(this.dockPanelSolutionExplorer);
            this.panelContainer1.Controls.Add(this.dockPanelFileExplorer);
            this.panelContainer1.Controls.Add(this.dockPanelFtpExplorer);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.FloatSize = new System.Drawing.Size(253, 266);
            this.panelContainer1.FloatVertical = true;
            this.panelContainer1.ID = new System.Guid("6c74c2c4-9754-4cea-b108-63e9e7e6c9a0");
            this.panelContainer1.ImageIndex = 24;
            this.panelContainer1.Location = new System.Drawing.Point(479, 77);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(289, 200);
            this.panelContainer1.Size = new System.Drawing.Size(289, 330);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanelProperties
            // 
            this.dockPanelProperties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelProperties.Appearance.Options.UseBackColor = true;
            this.dockPanelProperties.Controls.Add(this.dockPanel2_Container);
            this.dockPanelProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelProperties.FloatSize = new System.Drawing.Size(253, 266);
            this.dockPanelProperties.ID = new System.Guid("96144626-be47-440b-ae5f-2c5507db5076");
            this.dockPanelProperties.ImageIndex = 24;
            this.dockPanelProperties.Location = new System.Drawing.Point(3, 24);
            this.dockPanelProperties.Name = "dockPanelProperties";
            this.dockPanelProperties.OriginalSize = new System.Drawing.Size(283, 283);
            this.dockPanelProperties.Size = new System.Drawing.Size(283, 281);
            this.dockPanelProperties.Text = "Properties";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.textEditSearchProperties);
            this.dockPanel2_Container.Controls.Add(this.propertyGrid);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(283, 281);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // textEditSearchProperties
            // 
            this.textEditSearchProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditSearchProperties.EditValue = "Search";
            this.textEditSearchProperties.Location = new System.Drawing.Point(88, 3);
            this.textEditSearchProperties.MenuManager = this.barManager;
            this.textEditSearchProperties.Name = "textEditSearchProperties";
            this.textEditSearchProperties.Properties.Appearance.ForeColor = System.Drawing.Color.LightGray;
            this.textEditSearchProperties.Properties.Appearance.Options.UseForeColor = true;
            this.textEditSearchProperties.Size = new System.Drawing.Size(194, 20);
            this.textEditSearchProperties.TabIndex = 3;
            this.textEditSearchProperties.EditValueChanged += new System.EventHandler(this.TextEditSearchPropertiesEditValueChanged);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(283, 281);
            this.propertyGrid.TabIndex = 2;
            // 
            // dockPanelSolutionExplorer
            // 
            this.dockPanelSolutionExplorer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelSolutionExplorer.Appearance.Options.UseBackColor = true;
            this.dockPanelSolutionExplorer.Controls.Add(this.dockPanel1_Container);
            this.dockPanelSolutionExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelSolutionExplorer.FloatSize = new System.Drawing.Size(253, 266);
            this.dockPanelSolutionExplorer.ID = new System.Guid("70a62a0b-1c55-4e72-bef0-661a97c3e934");
            this.dockPanelSolutionExplorer.ImageIndex = 23;
            this.dockPanelSolutionExplorer.Location = new System.Drawing.Point(3, 24);
            this.dockPanelSolutionExplorer.Name = "dockPanelSolutionExplorer";
            this.dockPanelSolutionExplorer.OriginalSize = new System.Drawing.Size(283, 283);
            this.dockPanelSolutionExplorer.Size = new System.Drawing.Size(283, 281);
            this.dockPanelSolutionExplorer.TabText = "Project Explorer";
            this.dockPanelSolutionExplorer.Text = "Project Explorer";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.projectExplorer);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(283, 281);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // projectExplorer
            // 
            this.projectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectExplorer.Location = new System.Drawing.Point(0, 0);
            this.projectExplorer.Name = "projectExplorer";
            this.projectExplorer.SelectedNode = null;
            this.projectExplorer.Size = new System.Drawing.Size(283, 281);
            this.projectExplorer.StartUpFileName = null;
            this.projectExplorer.TabIndex = 0;
            this.projectExplorer.PropertiesItemClick += new System.EventHandler(this.ProjectExplorerPropertiesItemClick);
            this.projectExplorer.ProjectClosed += new System.EventHandler(this.UpdateProjectSpecificItems);
            this.projectExplorer.ProjectOpened += new System.EventHandler(this.UpdateProjectSpecificItems);
            // 
            // dockPanelFileExplorer
            // 
            this.dockPanelFileExplorer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelFileExplorer.Appearance.Options.UseBackColor = true;
            this.dockPanelFileExplorer.Controls.Add(this.controlContainer2);
            this.dockPanelFileExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelFileExplorer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockPanelFileExplorer.ID = new System.Guid("66fa6a88-5314-4616-8dbf-f74ece9d982a");
            this.dockPanelFileExplorer.ImageIndex = 20;
            this.dockPanelFileExplorer.Location = new System.Drawing.Point(3, 24);
            this.dockPanelFileExplorer.Name = "dockPanelFileExplorer";
            this.dockPanelFileExplorer.OriginalSize = new System.Drawing.Size(283, 283);
            this.dockPanelFileExplorer.Size = new System.Drawing.Size(283, 281);
            this.dockPanelFileExplorer.Text = "File Explorer";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.fileExplorer);
            this.controlContainer2.Location = new System.Drawing.Point(0, 0);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(283, 281);
            this.controlContainer2.TabIndex = 0;
            // 
            // fileExplorer
            // 
            this.fileExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorer.Location = new System.Drawing.Point(0, 0);
            this.fileExplorer.Name = "fileExplorer";
            this.fileExplorer.Size = new System.Drawing.Size(283, 281);
            this.fileExplorer.TabIndex = 0;
            // 
            // dockPanelFtpExplorer
            // 
            this.dockPanelFtpExplorer.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelFtpExplorer.Appearance.Options.UseBackColor = true;
            this.dockPanelFtpExplorer.Controls.Add(this.controlContainer7);
            this.dockPanelFtpExplorer.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelFtpExplorer.ID = new System.Guid("282d2452-38d5-4ed1-a385-aba8f8a1ac8a");
            this.dockPanelFtpExplorer.ImageIndex = 56;
            this.dockPanelFtpExplorer.Location = new System.Drawing.Point(3, 24);
            this.dockPanelFtpExplorer.Name = "dockPanelFtpExplorer";
            this.dockPanelFtpExplorer.OriginalSize = new System.Drawing.Size(283, 283);
            this.dockPanelFtpExplorer.Size = new System.Drawing.Size(283, 281);
            this.dockPanelFtpExplorer.Text = "FTP Explorer";
            // 
            // controlContainer7
            // 
            this.controlContainer7.Controls.Add(this.ftpExplorer1);
            this.controlContainer7.Location = new System.Drawing.Point(0, 0);
            this.controlContainer7.Name = "controlContainer7";
            this.controlContainer7.Size = new System.Drawing.Size(283, 281);
            this.controlContainer7.TabIndex = 0;
            // 
            // ftpExplorer1
            // 
            this.ftpExplorer1.DirectoriesOnly = false;
            this.ftpExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ftpExplorer1.FtpConnection = null;
            this.ftpExplorer1.Location = new System.Drawing.Point(0, 0);
            this.ftpExplorer1.MultiSelect = true;
            this.ftpExplorer1.Name = "ftpExplorer1";
            this.ftpExplorer1.SelectedPath = "";
            this.ftpExplorer1.ShowToolBar = true;
            this.ftpExplorer1.Size = new System.Drawing.Size(283, 281);
            this.ftpExplorer1.TabIndex = 0;
            this.ftpExplorer1.UseDefaultDir = true;
            this.ftpExplorer1.ConnectionChanged += new CompleX.Controls.FtpExplorer.ConnectionChangedHandler(this.FtpExplorer1ConnectionChanged);
            // 
            // imageListMain
            // 
            this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
            this.imageListMain.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListMain.Images.SetKeyName(0, "");
            this.imageListMain.Images.SetKeyName(1, "");
            this.imageListMain.Images.SetKeyName(2, "");
            this.imageListMain.Images.SetKeyName(3, "");
            this.imageListMain.Images.SetKeyName(4, "");
            this.imageListMain.Images.SetKeyName(5, "");
            this.imageListMain.Images.SetKeyName(6, "");
            this.imageListMain.Images.SetKeyName(7, "");
            this.imageListMain.Images.SetKeyName(8, "");
            this.imageListMain.Images.SetKeyName(9, "");
            this.imageListMain.Images.SetKeyName(10, "");
            this.imageListMain.Images.SetKeyName(11, "");
            this.imageListMain.Images.SetKeyName(12, "");
            this.imageListMain.Images.SetKeyName(13, "");
            this.imageListMain.Images.SetKeyName(14, "");
            this.imageListMain.Images.SetKeyName(15, "");
            this.imageListMain.Images.SetKeyName(16, "");
            this.imageListMain.Images.SetKeyName(17, "");
            this.imageListMain.Images.SetKeyName(18, "");
            this.imageListMain.Images.SetKeyName(19, "");
            this.imageListMain.Images.SetKeyName(20, "öffnen_16.png");
            this.imageListMain.Images.SetKeyName(21, "");
            this.imageListMain.Images.SetKeyName(22, "");
            this.imageListMain.Images.SetKeyName(23, "");
            this.imageListMain.Images.SetKeyName(24, "");
            this.imageListMain.Images.SetKeyName(25, "");
            this.imageListMain.Images.SetKeyName(26, "");
            this.imageListMain.Images.SetKeyName(27, "");
            this.imageListMain.Images.SetKeyName(28, "");
            this.imageListMain.Images.SetKeyName(29, "");
            this.imageListMain.Images.SetKeyName(30, "");
            this.imageListMain.Images.SetKeyName(31, "");
            this.imageListMain.Images.SetKeyName(32, "");
            this.imageListMain.Images.SetKeyName(33, "");
            this.imageListMain.Images.SetKeyName(34, "");
            this.imageListMain.Images.SetKeyName(35, "");
            this.imageListMain.Images.SetKeyName(36, "");
            this.imageListMain.Images.SetKeyName(37, "");
            this.imageListMain.Images.SetKeyName(38, "");
            this.imageListMain.Images.SetKeyName(39, "");
            this.imageListMain.Images.SetKeyName(40, "löschen.ico");
            this.imageListMain.Images.SetKeyName(41, "error.ico");
            this.imageListMain.Images.SetKeyName(42, "warning.ico");
            this.imageListMain.Images.SetKeyName(43, "bestaetigen.ico");
            this.imageListMain.Images.SetKeyName(44, "cmd_16_16.png");
            this.imageListMain.Images.SetKeyName(45, "liste_16.png");
            this.imageListMain.Images.SetKeyName(46, "schliessenVerwerfen_16.png");
            this.imageListMain.Images.SetKeyName(47, "schliessen_16.png");
            this.imageListMain.Images.SetKeyName(48, "schliessenDialog_16.png");
            this.imageListMain.Images.SetKeyName(49, "speichernSchliessen_16.png");
            this.imageListMain.Images.SetKeyName(50, "speichern_16.png");
            this.imageListMain.Images.SetKeyName(51, "speichern_alles_16.png");
            this.imageListMain.Images.SetKeyName(52, "speichern_unter_24.png");
            this.imageListMain.Images.SetKeyName(53, "info_16.png");
            this.imageListMain.Images.SetKeyName(54, "hyperlink_16.png");
            this.imageListMain.Images.SetKeyName(55, "cs.ico");
            this.imageListMain.Images.SetKeyName(56, "ftp.png");
            this.imageListMain.Images.SetKeyName(57, "folder.png");
            this.imageListMain.Images.SetKeyName(58, "ftpsave.png");
            // 
            // panelContainer2
            // 
            this.panelContainer2.ActiveChild = this.dockPanelFindResults;
            this.panelContainer2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.panelContainer2.Appearance.Options.UseBackColor = true;
            this.panelContainer2.Controls.Add(this.dockPanelTaskList);
            this.panelContainer2.Controls.Add(this.dockPanelFindResults);
            this.panelContainer2.Controls.Add(this.dockPanelOutPut);
            this.panelContainer2.Controls.Add(this.dockPanelMessageLog);
            this.panelContainer2.Controls.Add(this.dockPanelCmd);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.panelContainer2.FloatSize = new System.Drawing.Size(304, 139);
            this.panelContainer2.FloatVertical = true;
            this.panelContainer2.ID = new System.Guid("ec7b92c0-cfe1-43c3-9ff0-c24e6320f016");
            this.panelContainer2.ImageIndex = 28;
            this.panelContainer2.Location = new System.Drawing.Point(22, 407);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.OriginalSize = new System.Drawing.Size(200, 200);
            this.panelContainer2.Size = new System.Drawing.Size(746, 200);
            this.panelContainer2.Tabbed = true;
            this.panelContainer2.Text = "panelContainer2";
            // 
            // dockPanelFindResults
            // 
            this.dockPanelFindResults.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelFindResults.Appearance.Options.UseBackColor = true;
            this.dockPanelFindResults.Controls.Add(this.dockPanel4_Container);
            this.dockPanelFindResults.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelFindResults.FloatSize = new System.Drawing.Size(304, 139);
            this.dockPanelFindResults.ID = new System.Guid("47b3ea95-3900-46d6-b24c-5f3a779b1ae7");
            this.dockPanelFindResults.ImageIndex = 28;
            this.dockPanelFindResults.Location = new System.Drawing.Point(3, 24);
            this.dockPanelFindResults.Name = "dockPanelFindResults";
            this.dockPanelFindResults.OriginalSize = new System.Drawing.Size(740, 151);
            this.dockPanelFindResults.Size = new System.Drawing.Size(740, 151);
            this.dockPanelFindResults.Text = "Find Results";
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Controls.Add(this.findResultsControl);
            this.dockPanel4_Container.Controls.Add(this.textBoxFindResult);
            this.dockPanel4_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(740, 151);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // findResultsControl
            // 
            this.findResultsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findResultsControl.Location = new System.Drawing.Point(0, 0);
            this.findResultsControl.Name = "findResultsControl";
            this.findResultsControl.Size = new System.Drawing.Size(740, 151);
            this.findResultsControl.TabIndex = 1;
            // 
            // textBoxFindResult
            // 
            this.textBoxFindResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFindResult.EditValue = "";
            this.textBoxFindResult.Location = new System.Drawing.Point(0, 0);
            this.textBoxFindResult.Name = "textBoxFindResult";
            this.textBoxFindResult.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.textBoxFindResult.Properties.Appearance.Options.UseFont = true;
            this.textBoxFindResult.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textBoxFindResult.Size = new System.Drawing.Size(740, 151);
            this.textBoxFindResult.TabIndex = 0;
            // 
            // dockPanelTaskList
            // 
            this.dockPanelTaskList.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelTaskList.Appearance.Options.UseBackColor = true;
            this.dockPanelTaskList.Controls.Add(this.dockPanel3_Container);
            this.dockPanelTaskList.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelTaskList.FloatSize = new System.Drawing.Size(304, 139);
            this.dockPanelTaskList.ID = new System.Guid("7351d5e2-6da1-45c0-a5b6-13e4e7d7a56e");
            this.dockPanelTaskList.ImageIndex = 27;
            this.dockPanelTaskList.Location = new System.Drawing.Point(3, 24);
            this.dockPanelTaskList.Name = "dockPanelTaskList";
            this.dockPanelTaskList.OriginalSize = new System.Drawing.Size(740, 151);
            this.dockPanelTaskList.Size = new System.Drawing.Size(740, 151);
            this.dockPanelTaskList.TabText = "Task List";
            this.dockPanelTaskList.Text = "Task List";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.taskListControl);
            this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(740, 151);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // taskListControl
            // 
            this.taskListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListControl.Location = new System.Drawing.Point(0, 0);
            this.taskListControl.Name = "taskListControl";
            this.taskListControl.Size = new System.Drawing.Size(740, 151);
            this.taskListControl.TabIndex = 0;
            // 
            // dockPanelOutPut
            // 
            this.dockPanelOutPut.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelOutPut.Appearance.Options.UseBackColor = true;
            this.dockPanelOutPut.Controls.Add(this.dockPanel5_Container);
            this.dockPanelOutPut.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelOutPut.FloatSize = new System.Drawing.Size(304, 139);
            this.dockPanelOutPut.ID = new System.Guid("dbdb0ba9-5443-476b-93ad-ec35678d61ef");
            this.dockPanelOutPut.ImageIndex = 29;
            this.dockPanelOutPut.Location = new System.Drawing.Point(3, 24);
            this.dockPanelOutPut.Name = "dockPanelOutPut";
            this.dockPanelOutPut.OriginalSize = new System.Drawing.Size(740, 151);
            this.dockPanelOutPut.Size = new System.Drawing.Size(740, 151);
            this.dockPanelOutPut.Text = "Output";
            // 
            // dockPanel5_Container
            // 
            this.dockPanel5_Container.Controls.Add(this.textBoxOutPut);
            this.dockPanel5_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel5_Container.Name = "dockPanel5_Container";
            this.dockPanel5_Container.Size = new System.Drawing.Size(740, 151);
            this.dockPanel5_Container.TabIndex = 0;
            // 
            // textBoxOutPut
            // 
            this.textBoxOutPut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxOutPut.EditValue = "";
            this.textBoxOutPut.Location = new System.Drawing.Point(0, 0);
            this.textBoxOutPut.Name = "textBoxOutPut";
            this.textBoxOutPut.Properties.Appearance.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.textBoxOutPut.Properties.Appearance.Options.UseFont = true;
            this.textBoxOutPut.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textBoxOutPut.Properties.ReadOnly = true;
            this.textBoxOutPut.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutPut.Properties.WordWrap = false;
            this.textBoxOutPut.Size = new System.Drawing.Size(740, 151);
            this.textBoxOutPut.TabIndex = 0;
            this.textBoxOutPut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox2KeyDown);
            this.textBoxOutPut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBoxOutPutMouseUp);
            // 
            // dockPanelMessageLog
            // 
            this.dockPanelMessageLog.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelMessageLog.Appearance.Options.UseBackColor = true;
            this.dockPanelMessageLog.Controls.Add(this.controlContainer1);
            this.dockPanelMessageLog.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelMessageLog.ID = new System.Guid("3263bf43-a1d5-4794-a4c9-969ab8e709aa");
            this.dockPanelMessageLog.ImageIndex = 45;
            this.dockPanelMessageLog.Location = new System.Drawing.Point(3, 24);
            this.dockPanelMessageLog.Name = "dockPanelMessageLog";
            this.dockPanelMessageLog.OriginalSize = new System.Drawing.Size(740, 151);
            this.dockPanelMessageLog.Size = new System.Drawing.Size(740, 151);
            this.dockPanelMessageLog.Text = "Messages";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.messageLogControl);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(740, 151);
            this.controlContainer1.TabIndex = 0;
            // 
            // messageLogControl
            // 
            this.messageLogControl.AllowDrop = true;
            this.messageLogControl.BackColor = System.Drawing.Color.White;
            this.messageLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogControl.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.messageLogControl.Location = new System.Drawing.Point(0, 0);
            this.messageLogControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.messageLogControl.Name = "messageLogControl";
            this.messageLogControl.Size = new System.Drawing.Size(740, 151);
            this.messageLogControl.TabIndex = 0;
            // 
            // dockPanelCmd
            // 
            this.dockPanelCmd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dockPanelCmd.Appearance.Options.UseBackColor = true;
            this.dockPanelCmd.Controls.Add(this.controlContainer3);
            this.dockPanelCmd.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelCmd.FloatSize = new System.Drawing.Size(313, 295);
            this.dockPanelCmd.ID = new System.Guid("e77e95dc-b4f7-4f0a-a16f-79d97bf80698");
            this.dockPanelCmd.ImageIndex = 44;
            this.dockPanelCmd.Location = new System.Drawing.Point(3, 24);
            this.dockPanelCmd.Name = "dockPanelCmd";
            this.dockPanelCmd.OriginalSize = new System.Drawing.Size(740, 151);
            this.dockPanelCmd.Size = new System.Drawing.Size(740, 151);
            this.dockPanelCmd.Text = "Command Window";
            // 
            // controlContainer3
            // 
            this.controlContainer3.Controls.Add(this.shellControl);
            this.controlContainer3.Location = new System.Drawing.Point(0, 0);
            this.controlContainer3.Name = "controlContainer3";
            this.controlContainer3.Size = new System.Drawing.Size(740, 151);
            this.controlContainer3.TabIndex = 0;
            // 
            // shellControl
            // 
            this.shellControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellControl.Location = new System.Drawing.Point(0, 0);
            this.shellControl.Name = "shellControl";
            this.shellControl.Prompt = ">>>";
            this.shellControl.ShellTextBackColor = System.Drawing.Color.Silver;
            this.shellControl.ShellTextFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shellControl.ShellTextForeColor = System.Drawing.Color.White;
            this.shellControl.Size = new System.Drawing.Size(740, 151);
            this.shellControl.TabIndex = 0;
            this.shellControl.UseAutoDirectoryPrompt = true;
            // 
            // siLayouts
            // 
            this.siLayouts.Caption = "&Layouts";
            this.siLayouts.CategoryGuid = new System.Guid("a984a9d9-f96f-425a-8857-fe4de6df48c2");
            this.siLayouts.Id = 49;
            this.siLayouts.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iDeleteLayout),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveLayout)});
            this.siLayouts.Name = "siLayouts";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Tag...";
            this.barButtonItem1.Id = 72;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // iCloseAllButThis
            // 
            this.iCloseAllButThis.Caption = "Close All but this";
            this.iCloseAllButThis.Id = 90;
            this.iCloseAllButThis.ImageIndex = 46;
            this.iCloseAllButThis.Name = "iCloseAllButThis";
            this.iCloseAllButThis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CloseAllButThisItemClick);
            // 
            // iCopyFullPath
            // 
            this.iCopyFullPath.Caption = "Copy Full Path";
            this.iCopyFullPath.Id = 91;
            this.iCopyFullPath.Name = "iCopyFullPath";
            this.iCopyFullPath.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.CopyFullPathItemClick);
            // 
            // iOpenContainingFolder
            // 
            this.iOpenContainingFolder.Caption = "Open Containing Folder...";
            this.iOpenContainingFolder.Id = 92;
            this.iOpenContainingFolder.ImageIndex = 35;
            this.iOpenContainingFolder.Name = "iOpenContainingFolder";
            this.iOpenContainingFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenContainingFolderItemClick);
            // 
            // iFileInformation
            // 
            this.iFileInformation.Caption = "File Information...";
            this.iFileInformation.Id = 93;
            this.iFileInformation.ImageIndex = 53;
            this.iFileInformation.Name = "iFileInformation";
            this.iFileInformation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.IFileInformationItemClick);
            // 
            // iRename
            // 
            this.iRename.Caption = "Rename...";
            this.iRename.Id = 96;
            this.iRename.Name = "iRename";
            this.iRename.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2ItemClick1);
            // 
            // iCloseAllFiles
            // 
            this.iCloseAllFiles.Caption = "Close All Files";
            this.iCloseAllFiles.Id = 100;
            this.iCloseAllFiles.ImageIndex = 49;
            this.iCloseAllFiles.Name = "iCloseAllFiles";
            this.iCloseAllFiles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ICloseAllFilesItemClick);
            // 
            // iCloseAllExistingFiles
            // 
            this.iCloseAllExistingFiles.Caption = "Close All Existing Files";
            this.iCloseAllExistingFiles.Id = 101;
            this.iCloseAllExistingFiles.ImageIndex = 49;
            this.iCloseAllExistingFiles.Name = "iCloseAllExistingFiles";
            this.iCloseAllExistingFiles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ICloseAllExistingFilesItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 103;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItemAddFileToProject
            // 
            this.barButtonItemAddFileToProject.Caption = "Add this File to Current Project";
            this.barButtonItemAddFileToProject.Id = 111;
            this.barButtonItemAddFileToProject.Name = "barButtonItemAddFileToProject";
            this.barButtonItemAddFileToProject.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItemAddFileToProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemAddFileToProjectItemClick);
            // 
            // barSubItem4
            // 
            this.barSubItem4.Caption = "Save";
            this.barSubItem4.Id = 127;
            this.barSubItem4.ImageIndex = 7;
            this.barSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAs),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveAllExistingFiles),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSaveToFtp, true)});
            this.barSubItem4.Name = "barSubItem4";
            // 
            // SaveOutputButtonItem
            // 
            this.SaveOutputButtonItem.Caption = "Save Output...";
            this.SaveOutputButtonItem.Id = 130;
            this.SaveOutputButtonItem.ImageIndex = 21;
            this.SaveOutputButtonItem.Name = "SaveOutputButtonItem";
            this.SaveOutputButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveOutputButtonItemItemClick);
            // 
            // OpenOutputButtonItem
            // 
            this.OpenOutputButtonItem.Caption = "Open Output";
            this.OpenOutputButtonItem.Id = 131;
            this.OpenOutputButtonItem.Name = "OpenOutputButtonItem";
            this.OpenOutputButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OpenOutputButtonItemItemClick);
            // 
            // ClearOutputButtonItem
            // 
            this.ClearOutputButtonItem.Caption = "Clear Output";
            this.ClearOutputButtonItem.Id = 132;
            this.ClearOutputButtonItem.ImageIndex = 40;
            this.ClearOutputButtonItem.Name = "ClearOutputButtonItem";
            this.ClearOutputButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ClearOutPutButtonItemItemClick);
            // 
            // barButtonShowFileSystemMenu
            // 
            this.barButtonShowFileSystemMenu.Caption = "Show Filesystem Menu";
            this.barButtonShowFileSystemMenu.Id = 136;
            this.barButtonShowFileSystemMenu.Name = "barButtonShowFileSystemMenu";
            this.barButtonShowFileSystemMenu.ShortcutKeyDisplayString = "Ctrl+RightClick";
            this.barButtonShowFileSystemMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonShowFileSystemMenu_ItemClick);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList3.Images.SetKeyName(0, "");
            this.imageList3.Images.SetKeyName(1, "");
            this.imageList3.Images.SetKeyName(2, "");
            this.imageList3.Images.SetKeyName(3, "");
            this.imageList3.Images.SetKeyName(4, "");
            this.imageList3.Images.SetKeyName(5, "");
            this.imageList3.Images.SetKeyName(6, "");
            this.imageList3.Images.SetKeyName(7, "");
            this.imageList3.Images.SetKeyName(8, "");
            this.imageList3.Images.SetKeyName(9, "");
            this.imageList3.Images.SetKeyName(10, "");
            this.imageList3.Images.SetKeyName(11, "");
            this.imageList3.Images.SetKeyName(12, "");
            this.imageList3.Images.SetKeyName(13, "");
            this.imageList3.Images.SetKeyName(14, "");
            this.imageList3.Images.SetKeyName(15, "");
            this.imageList3.Images.SetKeyName(16, "");
            this.imageList3.Images.SetKeyName(17, "");
            this.imageList3.Images.SetKeyName(18, "");
            this.imageList3.Images.SetKeyName(19, "");
            this.imageList3.Images.SetKeyName(20, "");
            this.imageList3.Images.SetKeyName(21, "");
            this.imageList3.Images.SetKeyName(22, "");
            this.imageList3.Images.SetKeyName(23, "");
            // 
            // defaultEditPopupMenu
            // 
            this.defaultEditPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iUndo),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRedo),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.iPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCycle),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClipboard),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSelectAll, true)});
            this.defaultEditPopupMenu.Manager = this.barManager;
            this.defaultEditPopupMenu.Name = "defaultEditPopupMenu";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
            // 
            // xtraTabbedMdiManager
            // 
            this.xtraTabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiManager.Controller = this.barAndDockingController;
            this.xtraTabbedMdiManager.MdiParent = null;
            this.xtraTabbedMdiManager.SelectedPageChanged += new System.EventHandler(this.XtraTabbedMdiManagerSelectedPageChanged);
            this.xtraTabbedMdiManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.XtraTabbedMdiManagerMouseUp);
            this.xtraTabbedMdiManager.PageAdded += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.XtraTabbedMdiManagerPageAdded);
            this.xtraTabbedMdiManager.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.XtraTabbedMdiManagerPageRemoved);
            // 
            // popupMenuTab
            // 
            this.popupMenuTab.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAddFileToProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemAsToolWindow),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRename, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iClose, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseAllButThis),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseAllFiles),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCloseAllExistingFiles),
            new DevExpress.XtraBars.LinkPersistInfo(this.iCopyFullPath, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpenContainingFolder),
            new DevExpress.XtraBars.LinkPersistInfo(this.iEditWith, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFileInformation),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonShowFileSystemMenu)});
            this.popupMenuTab.Manager = this.barManager;
            this.popupMenuTab.Name = "popupMenuTab";
            this.popupMenuTab.BeforePopup += new System.ComponentModel.CancelEventHandler(this.PopupMenuTabBeforePopup);
            // 
            // iToggleFullscreen
            // 
            this.iToggleFullscreen.Caption = "Toggle Fullscreen";
            this.iToggleFullscreen.Id = 94;
            this.iToggleFullscreen.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
            this.iToggleFullscreen.Name = "iToggleFullscreen";
            this.iToggleFullscreen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToggleFullscreenItemClick);
            // 
            // popupMenuOutput
            // 
            this.popupMenuOutput.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.SaveOutputButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.OpenOutputButtonItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.ClearOutputButtonItem, true)});
            this.popupMenuOutput.Manager = this.barManager;
            this.popupMenuOutput.Name = "popupMenuOutput";
            // 
            // ComplexMainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(768, 636);
            this.Controls.Add(this.popupControlContainer1);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.panelContainer2);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "ComplexMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompleX Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComplexMainFormFormClosing);
            this.Load += new System.EventHandler(this.FrmMainLoad);
            this.MdiChildActivate += new System.EventHandler(this.FrmMainMdiChildActivate);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ComplexMainFormDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ComplexMainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).EndInit();
            this.popupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockingComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxExecuters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxConnections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.hideContainerLeft.ResumeLayout(false);
            this.dockPanelToolbox.ResumeLayout(false);
            this.dockPanel6_Container.ResumeLayout(false);
            this.dockPanelActionList.ResumeLayout(false);
            this.controlContainer6.ResumeLayout(false);
            this.dockPanelRecentFiles.ResumeLayout(false);
            this.controlContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.dockPanelOpenFiles.ResumeLayout(false);
            this.controlContainer4.ResumeLayout(false);
            this.contextMenuStripOpenFileExplorer.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dockPanelProperties.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearchProperties.Properties)).EndInit();
            this.dockPanelSolutionExplorer.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanelFileExplorer.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            this.dockPanelFtpExplorer.ResumeLayout(false);
            this.controlContainer7.ResumeLayout(false);
            this.panelContainer2.ResumeLayout(false);
            this.dockPanelFindResults.ResumeLayout(false);
            this.dockPanel4_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFindResult.Properties)).EndInit();
            this.dockPanelTaskList.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            this.dockPanelOutPut.ResumeLayout(false);
            this.dockPanel5_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxOutPut.Properties)).EndInit();
            this.dockPanelMessageLog.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            this.dockPanelCmd.ResumeLayout(false);
            this.controlContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.defaultEditPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarSubItem siNew;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.BarButtonItem iClose;
        private DevExpress.XtraBars.BarButtonItem iCreateNewProject;
        private DevExpress.XtraBars.BarButtonItem siFile;
        private DevExpress.XtraBars.BarButtonItem iCreateEmptyProject;
        private DevExpress.XtraBars.BarButtonItem iAddNewItem;
        private DevExpress.XtraBars.BarButtonItem iAddExistingItem;
        private DevExpress.XtraBars.BarSubItem iAddProject;
        private DevExpress.XtraBars.BarButtonItem iAddExistingDirectory;
        private DevExpress.XtraBars.BarButtonItem iSaveAll;
        private DevExpress.XtraBars.BarButtonItem iOpenProject;
        private DevExpress.XtraBars.BarButtonItem iCloseProject;
        private DevExpress.XtraBars.BarButtonItem iPageSetup;
        private DevExpress.XtraBars.BarButtonItem iPrint;
        private DevExpress.XtraBars.BarButtonItem iUndo;
        private DevExpress.XtraBars.BarButtonItem iRedo;
        private DevExpress.XtraBars.BarButtonItem iCut;
        private DevExpress.XtraBars.BarButtonItem iCopy;
        private DevExpress.XtraBars.BarButtonItem iPaste;
        private DevExpress.XtraBars.BarButtonItem iCycle;
        private DevExpress.XtraBars.BarButtonItem iSelectAll;
        private DevExpress.XtraBars.BarSubItem siFind;
        private DevExpress.XtraBars.BarButtonItem iFind;
        private DevExpress.XtraBars.BarButtonItem iReplace;
        private DevExpress.XtraBars.BarButtonItem iFindinFiles;
        private DevExpress.XtraBars.BarButtonItem iNew;
        private DevExpress.XtraBars.PopupMenu popupMenuNew;
        private DevExpress.XtraBars.BarButtonItem iOpenFile;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private DevExpress.XtraBars.BarButtonItem iStart;
        private DevExpress.XtraBars.BarEditItem eExecuter;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox ComboBoxExecuters;
        private DevExpress.XtraBars.BarEditItem eFind;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox searchItemComboBox1;
        private DevExpress.XtraBars.BarButtonItem iSolutionExplorer;
        private DevExpress.XtraBars.BarButtonItem iProperties;
        private DevExpress.XtraBars.BarButtonItem iToolbox;
        private DevExpress.XtraBars.BarButtonItem iClassView;
        private DevExpress.XtraEditors.MemoEdit textBoxOutPut;
        private DevExpress.XtraEditors.MemoEdit textBoxFindResult;
        private DevExpress.XtraBars.BarButtonItem iTaskList;
        private DevExpress.XtraBars.BarButtonItem iFindResults;
        private DevExpress.XtraBars.BarButtonItem iOutput;
        private DevExpress.XtraBars.BarButtonItem iCascade;
        private DevExpress.XtraBars.BarButtonItem iHorizontal;
        private DevExpress.XtraBars.BarButtonItem iVertical;
        private DevExpress.XtraBars.BarMdiChildrenListItem BarMdiChildrenListItem1;
        private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
        private System.Windows.Forms.ImageList imageListSE;
        private DevExpress.XtraBars.BarButtonItem iDeleteLayout;
        private DevExpress.XtraBars.BarButtonItem iSaveLayout;
        private DevExpress.XtraBars.BarSubItem siLayouts;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.Bar bar5;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel5_Container;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel6_Container;
        private System.Windows.Forms.ImageList imageList3;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarButtonItem biTabbedMDI;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox dockingComboBox;
        private DevExpress.XtraBars.BarEditItem beiDockMode;
        private DevExpress.DXperience.Demos.XtraPropertyGrid propertyGrid;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.BarButtonItem iClipboard;
        internal DevExpress.XtraBars.BarManager barManager;
        internal DevExpress.XtraBars.Docking.DockManager dockManager;
        internal DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager;
        private DevExpress.XtraBars.Bar bar6;
        private MessageLogControl messageLogControl;
        private ToolBox toolBox;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private FileExplorer fileExplorer;
        private DevExpress.XtraBars.BarButtonItem iCloseAll;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraBars.BarButtonItem iSaveAs;
        private DevExpress.XtraBars.BarButtonItem iCloseAllButThis;
        private DevExpress.XtraBars.BarButtonItem iCopyFullPath;
        private DevExpress.XtraBars.BarButtonItem iOpenContainingFolder;
        private DevExpress.XtraBars.BarButtonItem iFileInformation;
        private DevExpress.XtraBars.PopupMenu popupMenuTab;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer3;
        private ShellControl shellControl;
        private DevExpress.XtraBars.BarButtonItem iToggleFullscreen;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer4;
        private FileListControl openFilesControl;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer5;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private FileListControl recentFileListControl;
        internal DevExpress.XtraBars.Bar MainMenuBar;
        private DevExpress.XtraBars.BarSubItem iRecentFiles;
        private DevExpress.XtraBars.BarButtonItem iRename;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        internal DevExpress.XtraBars.BarSubItem iFile;
        internal DevExpress.XtraBars.BarSubItem iEdit;
        internal System.Windows.Forms.ImageList imageListMain;
        internal DevExpress.XtraBars.BarSubItem iWindow;
        internal DevExpress.XtraBars.BarSubItem iInsert;
        internal DevExpress.XtraBars.BarSubItem iView;
        internal DevExpress.XtraBars.BarSubItem iTools;
        internal DevExpress.XtraBars.BarSubItem iHelp;
        internal DevExpress.XtraBars.BarSubItem iEditWith;
        internal DevExpress.XtraBars.BarButtonItem iSelectApplication;
        private DevExpress.XtraBars.BarButtonItem iCloseAllFiles;
        private DevExpress.XtraBars.BarButtonItem iCloseAllExistingFiles;
        private DevExpress.XtraBars.BarButtonItem iSaveAllExistingFiles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripOpenFileExplorer;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelSolutionExplorer;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelProperties;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelTaskList;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelFindResults;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelOutPut;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelToolbox;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelMessageLog;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelFileExplorer;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelCmd;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelOpenFiles;
        internal DevExpress.XtraBars.Docking.DockPanel dockPanelRecentFiles;
        private DevExpress.XtraBars.BarSubItem iExtras;
        private DevExpress.XtraBars.BarButtonItem iStartNewInstance;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem iBrowser;
        private DevExpress.XtraBars.BarButtonItem iDOSCommand;
        private DevExpress.XtraBars.BarButtonItem iLastDosCommand;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelActionList;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer6;
        private ActionListControl actionListControl;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAddFileToProject;
        private DevExpress.XtraBars.BarButtonItem iExportProject;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelFtpExplorer;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer7;
        private FtpExplorer ftpExplorer1;
        private DevExpress.XtraBars.BarSubItem iRecentProjects;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private TaskListControl taskListControl;
        private DevExpress.XtraBars.BarButtonItem iOpenFromFtp;
        private DevExpress.XtraBars.Bar barFtpConnections;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConnectTo;
        private DevExpress.XtraBars.BarEditItem barEditItemConnections;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxConnections;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem iSaveToFtp;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAsToolWindow;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarButtonItem iUploadProject;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private DevExpress.XtraBars.BarButtonItem SaveOutputButtonItem;
        private DevExpress.XtraBars.BarButtonItem OpenOutputButtonItem;
        private DevExpress.XtraBars.BarButtonItem ClearOutputButtonItem;
        private DevExpress.XtraBars.PopupMenu popupMenuOutput;
        internal DevExpress.XtraBars.BarStaticItem iStatus1;
        internal DevExpress.XtraBars.BarEditItem progressBar;
        internal DevExpress.XtraBars.BarStaticItem labelAction;
        internal DevExpress.XtraBars.BarEditItem progressBarMarquee;
        internal DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private CompleX.Controls.SearchTextBox textEditSearchProperties;
        internal DevExpress.XtraBars.BarButtonItem iExit;
        internal DevExpress.XtraBars.PopupMenu defaultEditPopupMenu;
        internal DevExpress.XtraBars.Docking.DockPanel panelContainer2;
        internal FindResultsControl findResultsControl;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSendError;
        internal ProjectExplorer projectExplorer;
        private DevExpress.XtraBars.BarButtonItem barButtonShowFileSystemMenu;

    }
}