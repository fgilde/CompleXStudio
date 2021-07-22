namespace CompleX.Controls
{
    partial class FileExplorer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorer));
            this.treeMain = new CompleX.Controls.MultiTreeView();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStripFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.windowsDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExplorerManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.DirectoryEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.FilterEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.contextMenuStripFolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileExplorerManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeMain
            // 
            this.treeMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.treeMain, "treeMain");
            this.treeMain.ImageList = this.imgMain;
            this.treeMain.LabelEdit = true;
            this.treeMain.Name = "treeMain";
            this.treeMain.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("treeMain.SelectedNodes")));
            this.treeMain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeMainAfterLabelEdit);
            this.treeMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeMainAfterExpand);
            this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMainAfterSelect);
            this.treeMain.DoubleClick += new System.EventHandler(this.TreeMainDoubleClick);
            this.treeMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeMainKeyDown);
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "RemovableDrive.png");
            this.imgMain.Images.SetKeyName(1, "CDRom.png");
            this.imgMain.Images.SetKeyName(2, "Desktop.png");
            this.imgMain.Images.SetKeyName(3, "Drive.png");
            this.imgMain.Images.SetKeyName(4, "MyComputer.png");
            this.imgMain.Images.SetKeyName(5, "NetworkDrive.png");
            this.imgMain.Images.SetKeyName(6, "Folder.png");
            this.imgMain.Images.SetKeyName(7, "KRD_Editor_16.png");
            this.imgMain.Images.SetKeyName(8, "Properties.png");
            // 
            // contextMenuStripFolder
            // 
            this.contextMenuStripFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.toolStripMenuItemSplitter1,
            this.windowsDirectoryToolStripMenuItem,
            this.systemDirectoryToolStripMenuItem,
            this.programfilesToolStripMenuItem,
            this.applicationDataToolStripMenuItem,
            this.ownFilesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.otherToolStripMenuItem});
            this.contextMenuStripFolder.Name = "contextMenuStripFolder";
            resources.ApplyResources(this.contextMenuStripFolder, "contextMenuStripFolder");
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::CompleX.Properties.Resources.anhang_loeschen_16;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItemClick);
            // 
            // toolStripMenuItemSplitter1
            // 
            this.toolStripMenuItemSplitter1.Name = "toolStripMenuItemSplitter1";
            resources.ApplyResources(this.toolStripMenuItemSplitter1, "toolStripMenuItemSplitter1");
            // 
            // windowsDirectoryToolStripMenuItem
            // 
            this.windowsDirectoryToolStripMenuItem.Image = global::CompleX.Properties.Resources.ImgWindows;
            this.windowsDirectoryToolStripMenuItem.Name = "windowsDirectoryToolStripMenuItem";
            resources.ApplyResources(this.windowsDirectoryToolStripMenuItem, "windowsDirectoryToolStripMenuItem");
            this.windowsDirectoryToolStripMenuItem.Click += new System.EventHandler(this.WindowsDirectoryToolStripMenuItemClick);
            // 
            // systemDirectoryToolStripMenuItem
            // 
            this.systemDirectoryToolStripMenuItem.Name = "systemDirectoryToolStripMenuItem";
            resources.ApplyResources(this.systemDirectoryToolStripMenuItem, "systemDirectoryToolStripMenuItem");
            this.systemDirectoryToolStripMenuItem.Click += new System.EventHandler(this.SystemDirectoryToolStripMenuItemClick);
            // 
            // programfilesToolStripMenuItem
            // 
            this.programfilesToolStripMenuItem.Name = "programfilesToolStripMenuItem";
            resources.ApplyResources(this.programfilesToolStripMenuItem, "programfilesToolStripMenuItem");
            this.programfilesToolStripMenuItem.Click += new System.EventHandler(this.ProgramfilesToolStripMenuItemClick);
            // 
            // applicationDataToolStripMenuItem
            // 
            this.applicationDataToolStripMenuItem.Name = "applicationDataToolStripMenuItem";
            resources.ApplyResources(this.applicationDataToolStripMenuItem, "applicationDataToolStripMenuItem");
            this.applicationDataToolStripMenuItem.Click += new System.EventHandler(this.ApplicationDataToolStripMenuItemClick);
            // 
            // ownFilesToolStripMenuItem
            // 
            this.ownFilesToolStripMenuItem.Name = "ownFilesToolStripMenuItem";
            resources.ApplyResources(this.ownFilesToolStripMenuItem, "ownFilesToolStripMenuItem");
            this.ownFilesToolStripMenuItem.Click += new System.EventHandler(this.OwnFilesToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            resources.ApplyResources(this.otherToolStripMenuItem, "otherToolStripMenuItem");
            // 
            // fileExplorerManager
            // 
            this.fileExplorerManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar1});
            this.fileExplorerManager.DockControls.Add(this.barDockControlTop);
            this.fileExplorerManager.DockControls.Add(this.barDockControlBottom);
            this.fileExplorerManager.DockControls.Add(this.barDockControlLeft);
            this.fileExplorerManager.DockControls.Add(this.barDockControlRight);
            this.fileExplorerManager.Form = this;
            this.fileExplorerManager.Images = this.imgMain;
            this.fileExplorerManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem2,
            this.barButtonItem1});
            this.fileExplorerManager.MainMenu = this.bar2;
            this.fileExplorerManager.MaxItemId = 13;
            this.fileExplorerManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2,
            this.repositoryItemButtonEdit3,
            this.repositoryItemButtonEdit4});
            this.fileExplorerManager.StatusBar = this.bar1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.ImageIndex = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem1ItemClick);
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 7;
            this.barButtonItem2.ImageIndex = 8;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2ItemClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // repositoryItemTextEdit1
            // 
            resources.ApplyResources(this.repositoryItemTextEdit1, "repositoryItemTextEdit1");
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit1, "repositoryItemButtonEdit1");
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemButtonEdit2
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit2, "repositoryItemButtonEdit2");
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemButtonEdit2.Buttons"))))});
            this.repositoryItemButtonEdit2.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.ValidateOnEnterKey = true;
            // 
            // repositoryItemButtonEdit3
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit3, "repositoryItemButtonEdit3");
            this.repositoryItemButtonEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemButtonEdit3.Buttons"))))});
            this.repositoryItemButtonEdit3.Name = "repositoryItemButtonEdit3";
            // 
            // repositoryItemButtonEdit4
            // 
            resources.ApplyResources(this.repositoryItemButtonEdit4, "repositoryItemButtonEdit4");
            this.repositoryItemButtonEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemButtonEdit4.Buttons"))))});
            this.repositoryItemButtonEdit4.Name = "repositoryItemButtonEdit4";
            // 
            // DirectoryEdit
            // 
            resources.ApplyResources(this.DirectoryEdit, "DirectoryEdit");
            this.DirectoryEdit.Name = "DirectoryEdit";
            this.DirectoryEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("DirectoryEdit.Properties.Buttons"))))});
            this.DirectoryEdit.Properties.ContextMenuStrip = this.contextMenuStripFolder;
            // 
            // FilterEdit
            // 
            resources.ApplyResources(this.FilterEdit, "FilterEdit");
            this.FilterEdit.Name = "FilterEdit";
            this.FilterEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("FilterEdit.Properties.Buttons"))))});
            this.FilterEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterEdit_KeyDown);
            // 
            // FileExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FilterEdit);
            this.Controls.Add(this.DirectoryEdit);
            this.Controls.Add(this.treeMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FileExplorer";
            this.contextMenuStripFolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileExplorerManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilterEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MultiTreeView treeMain;
        private System.Windows.Forms.ImageList imgMain;
        private DevExpress.XtraBars.BarManager fileExplorerManager;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem buttonTrackItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit3;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit4;
        private DevExpress.XtraEditors.ButtonEdit DirectoryEdit;
        private DevExpress.XtraEditors.ButtonEdit FilterEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolder;
        private System.Windows.Forms.ToolStripMenuItem windowsDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ownFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSplitter1;
    }
}
