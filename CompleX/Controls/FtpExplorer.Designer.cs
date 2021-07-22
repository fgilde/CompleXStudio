namespace CompleX.Controls
{
    partial class FtpExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpExplorer));
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.treeMain = new CompleX.Controls.MultiTreeView();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.toolbar = new DevExpress.XtraBars.Bar();
            this.barButtonItemConnect = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDisconnect = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.labelDirectory = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonCreateFolder = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuConnections = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuSelectedObject = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuConnections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuSelectedObject)).BeginInit();
            this.SuspendLayout();
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "file.png");
            this.imgMain.Images.SetKeyName(1, "Folder.png");
            this.imgMain.Images.SetKeyName(2, "ftp.png");
            this.imgMain.Images.SetKeyName(3, "beenden_16.png");
            this.imgMain.Images.SetKeyName(4, "Img41.png");
            this.imgMain.Images.SetKeyName(5, "importieren.PNG");
            // 
            // treeMain
            // 
            this.treeMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.treeMain, "treeMain");
            this.treeMain.ImageList = this.imgMain;
            this.treeMain.LabelEdit = true;
            this.treeMain.Name = "treeMain";
            this.treeMain.PathSeparator = "/";
            this.treeMain.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("treeMain.SelectedNodes")));
            this.treeMain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeMainAfterLabelEdit);
            this.treeMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeMainBeforeExpand);
            this.treeMain.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeMainBeforeSelect);
            this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMainAfterSelect);
            this.treeMain.DoubleClick += new System.EventHandler(this.TreeMainDoubleClick);
            this.treeMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeMainKeyDown);
            this.treeMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeMainMouseUp);
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolbar,
            this.bar3});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imgMain;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.labelDirectory,
            this.barButtonItemConnect,
            this.barButtonItemDisconnect,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItemDelete,
            this.barButtonItem4,
            this.barButtonCreateFolder});
            this.barManager.MainMenu = this.toolbar;
            this.barManager.MaxItemId = 9;
            this.barManager.StatusBar = this.bar3;
            // 
            // toolbar
            // 
            this.toolbar.BarName = "Main menu";
            this.toolbar.DockCol = 0;
            this.toolbar.DockRow = 0;
            this.toolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemConnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDisconnect)});
            this.toolbar.OptionsBar.AllowQuickCustomization = false;
            this.toolbar.OptionsBar.DrawDragBorder = false;
            this.toolbar.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.toolbar, "toolbar");
            // 
            // barButtonItemConnect
            // 
            resources.ApplyResources(this.barButtonItemConnect, "barButtonItemConnect");
            this.barButtonItemConnect.Id = 2;
            this.barButtonItemConnect.ImageIndex = 2;
            this.barButtonItemConnect.Name = "barButtonItemConnect";
            this.barButtonItemConnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemConnectItemClick);
            // 
            // barButtonItemDisconnect
            // 
            resources.ApplyResources(this.barButtonItemDisconnect, "barButtonItemDisconnect");
            this.barButtonItemDisconnect.Enabled = false;
            this.barButtonItemDisconnect.Id = 3;
            this.barButtonItemDisconnect.ImageIndex = 3;
            this.barButtonItemDisconnect.Name = "barButtonItemDisconnect";
            this.barButtonItemDisconnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemDisconnectItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.labelDirectory)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // labelDirectory
            // 
            this.labelDirectory.Id = 0;
            this.labelDirectory.Name = "labelDirectory";
            this.labelDirectory.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.ImageIndex = 5;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonDownloadItemClick);
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonRenameItemClick);
            // 
            // barButtonItemDelete
            // 
            resources.ApplyResources(this.barButtonItemDelete, "barButtonItemDelete");
            this.barButtonItemDelete.Id = 6;
            this.barButtonItemDelete.ImageIndex = 4;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemDeleteItemClick);
            // 
            // barButtonItem4
            // 
            resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
            this.barButtonItem4.Id = 7;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonCreateFolder
            // 
            resources.ApplyResources(this.barButtonCreateFolder, "barButtonCreateFolder");
            this.barButtonCreateFolder.Id = 8;
            this.barButtonCreateFolder.ImageIndex = 1;
            this.barButtonCreateFolder.Name = "barButtonCreateFolder";
            this.barButtonCreateFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItemCreateFolderItemClick);
            // 
            // popupMenuConnections
            // 
            this.popupMenuConnections.Manager = this.barManager;
            this.popupMenuConnections.Name = "popupMenuConnections";
            // 
            // popupMenuSelectedObject
            // 
            this.popupMenuSelectedObject.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCreateFolder, true)});
            this.popupMenuSelectedObject.Manager = this.barManager;
            this.popupMenuSelectedObject.Name = "popupMenuSelectedObject";
            this.popupMenuSelectedObject.Popup += new System.EventHandler(this.PopupMenuSelectedObjectPopup);
            // 
            // FtpExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FtpExplorer";
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuConnections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuSelectedObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgMain;
        private MultiTreeView treeMain;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar toolbar;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem labelDirectory;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItemConnect;
        private DevExpress.XtraBars.PopupMenu popupMenuConnections;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDisconnect;
        private DevExpress.XtraBars.PopupMenu popupMenuSelectedObject;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonCreateFolder;
    }
}
