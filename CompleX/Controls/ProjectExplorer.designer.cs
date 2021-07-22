namespace CompleX.Controls
{
    partial class ProjectExplorer {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorer));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barHeader = new DevExpress.XtraBars.Bar();
            this.iRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.iProperties = new DevExpress.XtraBars.BarButtonItem();
            this.iAddItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.StatusBar = new DevExpress.XtraBars.Bar();
            this.currentItemPath = new DevExpress.XtraBars.BarStaticItem();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.iShow = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddExisting = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddFolder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddNewItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeListMain = new DevExpress.XtraTreeList.TreeList();
            this.coltext = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dataSetTree = new CompleX.DataSets.DataSetTree();
            this.imageListFileTypes = new System.Windows.Forms.ImageList(this.components);
            this.dataTableTreeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.contextMenuSelectedItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setSelectedAsStartUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.addExistingItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTreeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.contextMenuSelectedItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barHeader,
            this.StatusBar});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.iRefresh,
            this.iShow,
            this.iProperties,
            this.iAddItem,
            this.barButtonAddExisting,
            this.barButtonAddFolder,
            this.barButtonAddNewItem,
            this.currentItemPath,
            this.barButtonItem1,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 13;
            // 
            // barHeader
            // 
            this.barHeader.BarName = "Explorer";
            this.barHeader.DockCol = 0;
            this.barHeader.DockRow = 0;
            this.barHeader.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barHeader.FloatLocation = new System.Drawing.Point(53, 102);
            this.barHeader.FloatSize = new System.Drawing.Size(29, 21);
            this.barHeader.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iProperties, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iAddItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7)});
            this.barHeader.OptionsBar.AllowQuickCustomization = false;
            this.barHeader.OptionsBar.DrawDragBorder = false;
            this.barHeader.OptionsBar.RotateWhenVertical = false;
            this.barHeader.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barHeader, "barHeader");
            // 
            // iRefresh
            // 
            resources.ApplyResources(this.iRefresh, "iRefresh");
            this.iRefresh.Enabled = false;
            this.iRefresh.Id = 0;
            this.iRefresh.ImageIndex = 0;
            this.iRefresh.Name = "iRefresh";
            this.iRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.IRefreshItemClick);
            // 
            // iProperties
            // 
            resources.ApplyResources(this.iProperties, "iProperties");
            this.iProperties.Enabled = false;
            this.iProperties.Id = 2;
            this.iProperties.ImageIndex = 2;
            this.iProperties.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.iProperties.Name = "iProperties";
            this.iProperties.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.IPropertiesItemClick);
            // 
            // iAddItem
            // 
            resources.ApplyResources(this.iAddItem, "iAddItem");
            this.iAddItem.Enabled = false;
            this.iAddItem.Id = 0;
            this.iAddItem.ImageIndex = 18;
            this.iAddItem.Name = "iAddItem";
            this.iAddItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.IAddItemItemClick);
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Enabled = false;
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.ImageIndex = 17;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem1ItemClick);
            // 
            // barButtonItem4
            // 
            resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
            this.barButtonItem4.Enabled = false;
            this.barButtonItem4.Id = 8;
            this.barButtonItem4.ImageIndex = 1;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem4ItemClick);
            // 
            // barButtonItem5
            // 
            resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
            this.barButtonItem5.Enabled = false;
            this.barButtonItem5.Id = 9;
            this.barButtonItem5.ImageIndex = 6;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            resources.ApplyResources(this.barButtonItem6, "barButtonItem6");
            this.barButtonItem6.Enabled = false;
            this.barButtonItem6.Id = 10;
            this.barButtonItem6.ImageIndex = 19;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem6ItemClick);
            // 
            // barButtonItem7
            // 
            resources.ApplyResources(this.barButtonItem7, "barButtonItem7");
            this.barButtonItem7.Enabled = false;
            this.barButtonItem7.Id = 11;
            this.barButtonItem7.ImageIndex = 20;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem7ItemClick);
            // 
            // StatusBar
            // 
            this.StatusBar.BarName = "StatusBar";
            this.StatusBar.DockCol = 0;
            this.StatusBar.DockRow = 0;
            this.StatusBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.StatusBar.FloatLocation = new System.Drawing.Point(82, 470);
            this.StatusBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.currentItemPath)});
            this.StatusBar.OptionsBar.AllowQuickCustomization = false;
            this.StatusBar.OptionsBar.DisableClose = true;
            this.StatusBar.OptionsBar.DisableCustomization = true;
            this.StatusBar.OptionsBar.DrawDragBorder = false;
            this.StatusBar.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.StatusBar, "StatusBar");
            // 
            // currentItemPath
            // 
            resources.ApplyResources(this.currentItemPath, "currentItemPath");
            this.currentItemPath.Id = 4;
            this.currentItemPath.Name = "currentItemPath";
            this.currentItemPath.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            resources.ApplyResources(this.barDockControl1, "barDockControl1");
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            resources.ApplyResources(this.barDockControl2, "barDockControl2");
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            resources.ApplyResources(this.barDockControl3, "barDockControl3");
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            resources.ApplyResources(this.barDockControl4, "barDockControl4");
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "Img5.png");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "Img4.png");
            this.imageList.Images.SetKeyName(15, "folderDelete.ico");
            this.imageList.Images.SetKeyName(16, "folderNew.ico");
            this.imageList.Images.SetKeyName(17, "minus16.png");
            this.imageList.Images.SetKeyName(18, "plus16.png");
            this.imageList.Images.SetKeyName(19, "aufklappen_16.png");
            this.imageList.Images.SetKeyName(20, "zuklappen_16.png");
            this.imageList.Images.SetKeyName(21, "warning.ico");
            // 
            // iShow
            // 
            this.iShow.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            resources.ApplyResources(this.iShow, "iShow");
            this.iShow.Enabled = false;
            this.iShow.Id = 1;
            this.iShow.ImageIndex = 1;
            this.iShow.Name = "iShow";
            // 
            // barButtonAddExisting
            // 
            resources.ApplyResources(this.barButtonAddExisting, "barButtonAddExisting");
            this.barButtonAddExisting.Id = 1;
            this.barButtonAddExisting.ImageIndex = 10;
            this.barButtonAddExisting.Name = "barButtonAddExisting";
            this.barButtonAddExisting.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonAddExistingItemClick);
            // 
            // barButtonAddFolder
            // 
            resources.ApplyResources(this.barButtonAddFolder, "barButtonAddFolder");
            this.barButtonAddFolder.Id = 2;
            this.barButtonAddFolder.ImageIndex = 16;
            this.barButtonAddFolder.Name = "barButtonAddFolder";
            this.barButtonAddFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonAddFolderItemClick);
            // 
            // barButtonAddNewItem
            // 
            resources.ApplyResources(this.barButtonAddNewItem, "barButtonAddNewItem");
            this.barButtonAddNewItem.Id = 3;
            this.barButtonAddNewItem.ImageIndex = 14;
            this.barButtonAddNewItem.Name = "barButtonAddNewItem";
            this.barButtonAddNewItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonAddNewItemItemClick);
            // 
            // barButtonItem2
            // 
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 12;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2ItemClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.treeListMain);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // treeListMain
            // 
            this.treeListMain.Appearance.Empty.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.Appearance.Empty.Font")));
            this.treeListMain.Appearance.Empty.Options.UseFont = true;
            this.treeListMain.Appearance.EvenRow.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.EvenRow.BackColor")));
            this.treeListMain.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeListMain.Appearance.EvenRow.Options.UseTextOptions = true;
            this.treeListMain.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedCell.BackColor")));
            this.treeListMain.Appearance.FocusedCell.BackColor2 = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedCell.BackColor2")));
            this.treeListMain.Appearance.FocusedCell.BorderColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedCell.BorderColor")));
            this.treeListMain.Appearance.FocusedCell.ForeColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedCell.ForeColor")));
            this.treeListMain.Appearance.FocusedCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("treeListMain.Appearance.FocusedCell.GradientMode")));
            this.treeListMain.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeListMain.Appearance.FocusedCell.Options.UseBorderColor = true;
            this.treeListMain.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeListMain.Appearance.FocusedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedRow.BackColor")));
            this.treeListMain.Appearance.FocusedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedRow.BackColor2")));
            this.treeListMain.Appearance.FocusedRow.BorderColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedRow.BorderColor")));
            this.treeListMain.Appearance.FocusedRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.FocusedRow.ForeColor")));
            this.treeListMain.Appearance.FocusedRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("treeListMain.Appearance.FocusedRow.GradientMode")));
            this.treeListMain.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeListMain.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.treeListMain.Appearance.FocusedRow.Options.UseForeColor = true;
            this.treeListMain.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.Appearance.HeaderPanel.Font")));
            this.treeListMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeListMain.Appearance.HideSelectionRow.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.HideSelectionRow.BackColor")));
            this.treeListMain.Appearance.HideSelectionRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.HideSelectionRow.BackColor2")));
            this.treeListMain.Appearance.HideSelectionRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.HideSelectionRow.ForeColor")));
            this.treeListMain.Appearance.HideSelectionRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("treeListMain.Appearance.HideSelectionRow.GradientMode")));
            this.treeListMain.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.treeListMain.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.treeListMain.Appearance.OddRow.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.OddRow.BackColor")));
            this.treeListMain.Appearance.OddRow.Options.UseBackColor = true;
            this.treeListMain.Appearance.SelectedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.SelectedRow.BackColor")));
            this.treeListMain.Appearance.SelectedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.SelectedRow.BackColor2")));
            this.treeListMain.Appearance.SelectedRow.BorderColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.SelectedRow.BorderColor")));
            this.treeListMain.Appearance.SelectedRow.ForeColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.SelectedRow.ForeColor")));
            this.treeListMain.Appearance.SelectedRow.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("treeListMain.Appearance.SelectedRow.GradientMode")));
            this.treeListMain.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeListMain.Appearance.SelectedRow.Options.UseBorderColor = true;
            this.treeListMain.Appearance.SelectedRow.Options.UseForeColor = true;
            this.treeListMain.Appearance.TreeLine.BackColor = ((System.Drawing.Color)(resources.GetObject("treeListMain.Appearance.TreeLine.BackColor")));
            this.treeListMain.Appearance.TreeLine.Options.UseBackColor = true;
            this.treeListMain.AppearancePrint.EvenRow.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.EvenRow.Font")));
            this.treeListMain.AppearancePrint.EvenRow.Options.UseFont = true;
            this.treeListMain.AppearancePrint.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.FooterPanel.Font")));
            this.treeListMain.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.treeListMain.AppearancePrint.GroupFooter.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.GroupFooter.Font")));
            this.treeListMain.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.treeListMain.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.HeaderPanel.Font")));
            this.treeListMain.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.treeListMain.AppearancePrint.Lines.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.Lines.Font")));
            this.treeListMain.AppearancePrint.Lines.Options.UseFont = true;
            this.treeListMain.AppearancePrint.OddRow.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.OddRow.Font")));
            this.treeListMain.AppearancePrint.OddRow.Options.UseFont = true;
            this.treeListMain.AppearancePrint.Preview.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.Preview.Font")));
            this.treeListMain.AppearancePrint.Preview.Options.UseFont = true;
            this.treeListMain.AppearancePrint.Row.Font = ((System.Drawing.Font)(resources.GetObject("treeListMain.AppearancePrint.Row.Font")));
            this.treeListMain.AppearancePrint.Row.Options.UseFont = true;
            this.treeListMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.coltext});
            resources.ApplyResources(this.treeListMain, "treeListMain");
            this.treeListMain.DataSource = this.dataSetTree;
            this.treeListMain.KeyFieldName = "id";
            this.treeListMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.treeListMain.LookAndFeel.UseWindowsXPTheme = true;
            this.treeListMain.Name = "treeListMain";
            this.treeListMain.OptionsBehavior.AllowIncrementalSearch = true;
            this.treeListMain.OptionsBehavior.AutoChangeParent = false;
            this.treeListMain.OptionsBehavior.AutoMoveRowFocus = true;
            this.treeListMain.OptionsBehavior.ExpandNodesOnIncrementalSearch = true;
            this.treeListMain.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeListMain.OptionsPrint.UsePrintStyles = true;
            this.treeListMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListMain.OptionsSelection.MultiSelect = true;
            this.treeListMain.OptionsView.ShowColumns = false;
            this.treeListMain.OptionsView.ShowHorzLines = false;
            this.treeListMain.OptionsView.ShowIndicator = false;
            this.treeListMain.OptionsView.ShowVertLines = false;
            this.treeListMain.ParentFieldName = "parentid";
            this.treeListMain.SelectImageList = this.imageListFileTypes;
            this.treeListMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListMain_NodeCellStyle);
            this.treeListMain.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.TreeListMainFocusedNodeChanged);
            this.treeListMain.SelectionChanged += new System.EventHandler(this.TreeListMainSelectionChanged);
            this.treeListMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TreeListMainKeyUp);
            this.treeListMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeListMainMouseDoubleClick);
            this.treeListMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TreeListMainMouseUp);
            // 
            // coltext
            // 
            resources.ApplyResources(this.coltext, "coltext");
            this.coltext.FieldName = "text";
            this.coltext.Name = "coltext";
            this.coltext.OptionsColumn.AllowEdit = false;
            this.coltext.OptionsColumn.ReadOnly = true;
            // 
            // dataSetTree
            // 
            this.dataSetTree.DataSetName = "DataSetTree";
            this.dataSetTree.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // imageListFileTypes
            // 
            this.imageListFileTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFileTypes.ImageStream")));
            this.imageListFileTypes.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListFileTypes.Images.SetKeyName(0, "");
            this.imageListFileTypes.Images.SetKeyName(1, "neues_objekt.ico");
            this.imageListFileTypes.Images.SetKeyName(2, "ordner_16.png");
            this.imageListFileTypes.Images.SetKeyName(3, "warning.ico");
            // 
            // dataTableTreeBindingSource
            // 
            this.dataTableTreeBindingSource.DataMember = "DataTableTree";
            this.dataTableTreeBindingSource.DataSource = this.dataSetTree;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddExisting),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddFolder, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddNewItem)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.BeforePopup += new System.ComponentModel.CancelEventHandler(this.PopupMenuBeforePopup);
            // 
            // contextMenuSelectedItem
            // 
            this.contextMenuSelectedItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSelectedAsStartUpToolStripMenuItem,
            this.toolStripMenuItem4,
            this.addExistingItemToolStripMenuItem,
            this.addExistingDirectoryToolStripMenuItem,
            this.toolStripMenuItem3,
            this.addNewFolderToolStripMenuItem,
            this.addNewItemToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteSelectedToolStripMenuItem,
            this.toolStripMenuItem2,
            this.renameToolStripMenuItem});
            this.contextMenuSelectedItem.Name = "contextMenuSelectedItem";
            resources.ApplyResources(this.contextMenuSelectedItem, "contextMenuSelectedItem");
            // 
            // setSelectedAsStartUpToolStripMenuItem
            // 
            this.setSelectedAsStartUpToolStripMenuItem.Name = "setSelectedAsStartUpToolStripMenuItem";
            resources.ApplyResources(this.setSelectedAsStartUpToolStripMenuItem, "setSelectedAsStartUpToolStripMenuItem");
            this.setSelectedAsStartUpToolStripMenuItem.Click += new System.EventHandler(this.SetSelectedAsStartUpToolStripMenuItemClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // addExistingItemToolStripMenuItem
            // 
            this.addExistingItemToolStripMenuItem.Image = global::CompleX.Properties.Resources.Img5;
            this.addExistingItemToolStripMenuItem.Name = "addExistingItemToolStripMenuItem";
            resources.ApplyResources(this.addExistingItemToolStripMenuItem, "addExistingItemToolStripMenuItem");
            this.addExistingItemToolStripMenuItem.Click += new System.EventHandler(this.AddExistingItemToolStripMenuItemClick);
            // 
            // addExistingDirectoryToolStripMenuItem
            // 
            this.addExistingDirectoryToolStripMenuItem.Name = "addExistingDirectoryToolStripMenuItem";
            resources.ApplyResources(this.addExistingDirectoryToolStripMenuItem, "addExistingDirectoryToolStripMenuItem");
            this.addExistingDirectoryToolStripMenuItem.Click += new System.EventHandler(this.AddExistingDirectoryToolStripMenuItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // addNewFolderToolStripMenuItem
            // 
            this.addNewFolderToolStripMenuItem.Image = global::CompleX.Properties.Resources.fileNew_16;
            this.addNewFolderToolStripMenuItem.Name = "addNewFolderToolStripMenuItem";
            resources.ApplyResources(this.addNewFolderToolStripMenuItem, "addNewFolderToolStripMenuItem");
            this.addNewFolderToolStripMenuItem.Click += new System.EventHandler(this.AddNewFolderToolStripMenuItemClick);
            // 
            // addNewItemToolStripMenuItem
            // 
            this.addNewItemToolStripMenuItem.Image = global::CompleX.Properties.Resources.Img4;
            this.addNewItemToolStripMenuItem.Name = "addNewItemToolStripMenuItem";
            resources.ApplyResources(this.addNewItemToolStripMenuItem, "addNewItemToolStripMenuItem");
            this.addNewItemToolStripMenuItem.Click += new System.EventHandler(this.AddNewItemToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Image = global::CompleX.Properties.Resources.löschen_16;
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            resources.ApplyResources(this.deleteSelectedToolStripMenuItem, "deleteSelectedToolStripMenuItem");
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItemClick);
            // 
            // ProjectExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "ProjectExplorer";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTableTreeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.contextMenuSelectedItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.BarButtonItem iRefresh;
        private DevExpress.XtraBars.BarButtonItem iShow;
        private DevExpress.XtraBars.BarButtonItem iProperties;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.Bar barHeader;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private System.ComponentModel.IContainer components;
        private CompleX.DataSets.DataSetTree dataSetTree;
        private System.Windows.Forms.BindingSource dataTableTreeBindingSource;
        private DevExpress.XtraTreeList.TreeList treeListMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn coltext;
        private DevExpress.XtraBars.BarButtonItem iAddItem;
        private System.Windows.Forms.ImageList imageListFileTypes;
        private DevExpress.XtraBars.BarButtonItem barButtonAddExisting;
        private DevExpress.XtraBars.BarButtonItem barButtonAddFolder;
        private DevExpress.XtraBars.BarButtonItem barButtonAddNewItem;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.Bar StatusBar;
        private DevExpress.XtraBars.BarStaticItem currentItemPath;
        private System.Windows.Forms.ContextMenuStrip contextMenuSelectedItem;
        private System.Windows.Forms.ToolStripMenuItem addExistingItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private System.Windows.Forms.ToolStripMenuItem addExistingDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem setSelectedAsStartUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;

    }
}