using CompleX.Controls;

namespace CompleX.Dialogs
{
    partial class BrowserDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserDialog));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.iBack = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iForward = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iStop = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iRefresh = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iHome = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iSearch = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iFavorites = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iHistory = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iMail = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iPrint = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iEdit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.eAddress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.iGo = new DevExpress.XtraBars.BarButtonItem();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.iText = new DevExpress.XtraBars.BarStaticItem();
            this.eProgress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.favorites = new CompleX.Controls.FavoritesControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.iMedia = new DevExpress.XtraBars.BarLargeButtonItem();
            this.iToolBars = new DevExpress.XtraBars.BarToolbarsListItem();
            this.iAbout = new DevExpress.XtraBars.BarButtonItem();
            this.iExit = new DevExpress.XtraBars.BarButtonItem();
            this.iAdd = new DevExpress.XtraBars.BarButtonItem();
            this.iOpen = new DevExpress.XtraBars.BarButtonItem();
            this.iSave = new DevExpress.XtraBars.BarButtonItem();
            this.ipsWXP = new DevExpress.XtraBars.BarButtonItem();
            this.ipsOXP = new DevExpress.XtraBars.BarButtonItem();
            this.ipsO2K = new DevExpress.XtraBars.BarButtonItem();
            this.iPaintStyle = new DevExpress.XtraBars.BarSubItem();
            this.ipsDefault = new DevExpress.XtraBars.BarButtonItem();
            this.ipsO3 = new DevExpress.XtraBars.BarButtonItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3,
            this.bar4});
            this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Built-in Menus", new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb")),
            new DevExpress.XtraBars.BarManagerCategory("Standard", new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b")),
            new DevExpress.XtraBars.BarManagerCategory("Address", new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d")),
            new DevExpress.XtraBars.BarManagerCategory("StatusBar", new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283")),
            new DevExpress.XtraBars.BarManagerCategory("Items", new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13")),
            new DevExpress.XtraBars.BarManagerCategory("Favorites", new System.Guid("e1ba440c-33dc-4df6-b712-79cdc4dcd983"))});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.iBack,
            this.iForward,
            this.iStop,
            this.iRefresh,
            this.iHome,
            this.iSearch,
            this.iFavorites,
            this.iMedia,
            this.iHistory,
            this.iMail,
            this.iPrint,
            this.iEdit,
            this.iGo,
            this.eAddress,
            this.iText,
            this.eProgress,
            this.iToolBars,
            this.iAbout,
            this.iExit,
            this.iAdd,
            this.iOpen,
            this.iSave,
            this.ipsWXP,
            this.ipsOXP,
            this.ipsO2K,
            this.iPaintStyle,
            this.ipsO3,
            this.ipsDefault});
            this.barManager1.LargeImages = this.imageList1;
            this.barManager1.MaxItemId = 43;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemProgressBar1});
            this.barManager1.ShowCloseButton = true;
            this.barManager1.StatusBar = this.bar4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Standard Buttons";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(48, 104);
            this.bar1.FloatSize = new System.Drawing.Size(16, 16);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iBack),
            new DevExpress.XtraBars.LinkPersistInfo(this.iForward),
            new DevExpress.XtraBars.LinkPersistInfo(this.iStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.iRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.iHome),
            new DevExpress.XtraBars.LinkPersistInfo(this.iSearch, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iFavorites),
            new DevExpress.XtraBars.LinkPersistInfo(this.iHistory),
            new DevExpress.XtraBars.LinkPersistInfo(this.iMail, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.iEdit)});
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Standard Buttons";
            // 
            // iBack
            // 
            this.iBack.Caption = "Back";
            this.iBack.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iBack.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iBack.Enabled = false;
            this.iBack.Hint = "Back";
            this.iBack.Id = 6;
            this.iBack.LargeImageIndex = 0;
            this.iBack.LargeImageIndexDisabled = 12;
            this.iBack.Name = "iBack";
            this.iBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iBack_ItemClick);
            // 
            // iForward
            // 
            this.iForward.Caption = "Forward";
            this.iForward.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iForward.Enabled = false;
            this.iForward.Hint = "Forward";
            this.iForward.Id = 8;
            this.iForward.LargeImageIndex = 3;
            this.iForward.LargeImageIndexDisabled = 13;
            this.iForward.Name = "iForward";
            this.iForward.ShowCaptionOnBar = false;
            this.iForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iForward_ItemClick);
            // 
            // iStop
            // 
            this.iStop.Caption = "Stop";
            this.iStop.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iStop.Hint = "Stop";
            this.iStop.Id = 9;
            this.iStop.LargeImageIndex = 11;
            this.iStop.Name = "iStop";
            this.iStop.ShowCaptionOnBar = false;
            this.iStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iStop_ItemClick);
            // 
            // iRefresh
            // 
            this.iRefresh.Caption = "Refresh";
            this.iRefresh.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iRefresh.Hint = "Refresh";
            this.iRefresh.Id = 10;
            this.iRefresh.ImageIndex = 9;
            this.iRefresh.LargeImageIndex = 9;
            this.iRefresh.Name = "iRefresh";
            this.iRefresh.ShowCaptionOnBar = false;
            this.iRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iRefresh_ItemClick);
            // 
            // iHome
            // 
            this.iHome.Caption = "Home";
            this.iHome.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iHome.Hint = "Home";
            this.iHome.Id = 11;
            this.iHome.LargeImageIndex = 5;
            this.iHome.Name = "iHome";
            this.iHome.ShowCaptionOnBar = false;
            this.iHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iHome_ItemClick);
            // 
            // iSearch
            // 
            this.iSearch.Caption = "Search";
            this.iSearch.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iSearch.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iSearch.Hint = "Search";
            this.iSearch.Id = 12;
            this.iSearch.LargeImageIndex = 10;
            this.iSearch.Name = "iSearch";
            this.iSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iSearch_ItemClick);
            // 
            // iFavorites
            // 
            this.iFavorites.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.iFavorites.Caption = "Favorites";
            this.iFavorites.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iFavorites.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iFavorites.Down = true;
            this.iFavorites.Hint = "Favorites";
            this.iFavorites.Id = 13;
            this.iFavorites.LargeImageIndex = 2;
            this.iFavorites.Name = "iFavorites";
            this.iFavorites.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iFavorites_ItemClick);
            // 
            // iHistory
            // 
            this.iHistory.Caption = "History";
            this.iHistory.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iHistory.Enabled = false;
            this.iHistory.Hint = "History";
            this.iHistory.Id = 16;
            this.iHistory.LargeImageIndex = 4;
            this.iHistory.LargeImageIndexDisabled = 14;
            this.iHistory.Name = "iHistory";
            this.iHistory.ShowCaptionOnBar = false;
            // 
            // iMail
            // 
            this.iMail.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.iMail.Caption = "Mail";
            this.iMail.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iMail.Enabled = false;
            this.iMail.Hint = "Mail";
            this.iMail.Id = 17;
            this.iMail.LargeImageIndex = 6;
            this.iMail.LargeImageIndexDisabled = 15;
            this.iMail.Name = "iMail";
            this.iMail.ShowCaptionOnBar = false;
            // 
            // iPrint
            // 
            this.iPrint.Caption = "&Print...";
            this.iPrint.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iPrint.Hint = "Print";
            this.iPrint.Id = 18;
            this.iPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.iPrint.LargeImageIndex = 8;
            this.iPrint.Name = "iPrint";
            this.iPrint.ShowCaptionOnBar = false;
            this.iPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iPrint_ItemClick);
            // 
            // iEdit
            // 
            this.iEdit.Caption = "Edit";
            this.iEdit.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iEdit.Hint = "Open Notepad";
            this.iEdit.Id = 19;
            this.iEdit.LargeImageIndex = 1;
            this.iEdit.Name = "iEdit";
            this.iEdit.ShowCaptionOnBar = false;
            this.iEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iEdit_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Address Bar";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 1;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.eAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.iGo)});
            this.bar3.Text = "Address Bar";
            // 
            // eAddress
            // 
            this.eAddress.AutoFillWidth = true;
            this.eAddress.Caption = "Address";
            this.eAddress.CategoryGuid = new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d");
            this.eAddress.Edit = this.repositoryItemComboBox1;
            this.eAddress.Id = 21;
            this.eAddress.IEBehavior = true;
            this.eAddress.Name = "eAddress";
            this.eAddress.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.eAddress.Width = 400;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AllowFocused = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.CycleOnDblClick = false;
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.repositoryItemComboBox1_KeyDown);
            this.repositoryItemComboBox1.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.repositoryItemComboBox1_CloseUp);
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedItemChanged);
            // 
            // iGo
            // 
            this.iGo.Caption = "Go";
            this.iGo.CategoryGuid = new System.Guid("fb82a187-cdf0-4f39-a566-c00dbaba593d");
            this.iGo.Glyph = ((System.Drawing.Image)(resources.GetObject("iGo.Glyph")));
            this.iGo.Hint = "Go to ...";
            this.iGo.Id = 20;
            this.iGo.Name = "iGo";
            this.iGo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.iGo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iGo_ItemClick);
            // 
            // bar4
            // 
            this.bar4.BarName = "Status Bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.FloatLocation = new System.Drawing.Point(30, 434);
            this.bar4.FloatSize = new System.Drawing.Size(20, 22);
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iText),
            new DevExpress.XtraBars.LinkPersistInfo(this.eProgress)});
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.DrawSizeGrip = true;
            this.bar4.OptionsBar.RotateWhenVertical = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Status Bar";
            // 
            // iText
            // 
            this.iText.AutoSize = DevExpress.XtraBars.BarStaticItemSize.Spring;
            this.iText.CategoryGuid = new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283");
            this.iText.Id = 22;
            this.iText.Name = "iText";
            this.iText.RightIndent = 3;
            this.iText.TextAlignment = System.Drawing.StringAlignment.Near;
            this.iText.Width = 32;
            // 
            // eProgress
            // 
            this.eProgress.CanOpenEdit = false;
            this.eProgress.CategoryGuid = new System.Guid("2ca54f89-3af6-4cbb-93d8-4a4a9387f283");
            this.eProgress.Edit = this.repositoryItemProgressBar1;
            this.eProgress.EditHeight = 10;
            this.eProgress.Id = 24;
            this.eProgress.Name = "eProgress";
            this.eProgress.Width = 70;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.repositoryItemProgressBar1.Appearance.Options.UseBackColor = true;
            this.repositoryItemProgressBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PaintStyleName = "Skin";
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("4cffd522-cc78-448d-b022-189564c3fbac");
            this.dockPanel1.Location = new System.Drawing.Point(0, 55);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(200, 412);
            this.dockPanel1.Text = "Favorites";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.favorites);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 385);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // favorites
            // 
            this.favorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favorites.Location = new System.Drawing.Point(0, 0);
            this.favorites.Name = "favorites";
            this.favorites.Size = new System.Drawing.Size(194, 385);
            this.favorites.TabIndex = 0;
            this.favorites.AddNewFavorite += new System.EventHandler(this.ctrlFavorites1_AddNewFavorite);
            this.favorites.OpenFavorite += new System.EventHandler(this.ctrlFavorites1_OpenFavorite);
            this.favorites.EditFavorite += new System.EventHandler(this.ctrlFavorites1_EditFavorite);
            this.favorites.DeleteFavorite += new System.EventHandler(this.ctrlFavorites1_DeleteFavorite);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "");
            // 
            // iMedia
            // 
            this.iMedia.Caption = "Media";
            this.iMedia.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.iMedia.CategoryGuid = new System.Guid("8e707040-b093-4d7e-8f27-277ae2456d3b");
            this.iMedia.Hint = "Media";
            this.iMedia.Id = 15;
            this.iMedia.LargeImageIndex = 7;
            this.iMedia.Name = "iMedia";
            // 
            // iToolBars
            // 
            this.iToolBars.Caption = "ToolBarsList";
            this.iToolBars.CategoryGuid = new System.Guid("4712321c-b9cd-461f-b453-4a7791063abb");
            this.iToolBars.Id = 25;
            this.iToolBars.Name = "iToolBars";
            // 
            // iAbout
            // 
            this.iAbout.Caption = "&About";
            this.iAbout.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iAbout.Id = 26;
            this.iAbout.Name = "iAbout";
            // 
            // iExit
            // 
            this.iExit.Caption = "E&xit";
            this.iExit.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iExit.Id = 27;
            this.iExit.Name = "iExit";
            this.iExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iExit_ItemClick);
            // 
            // iAdd
            // 
            this.iAdd.Caption = "Add to Favorites...";
            this.iAdd.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iAdd.Id = 28;
            this.iAdd.Name = "iAdd";
            this.iAdd.OwnFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.iAdd.UseOwnFont = true;
            this.iAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iAdd_ItemClick);
            // 
            // iOpen
            // 
            this.iOpen.Caption = "&Open...";
            this.iOpen.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iOpen.Id = 29;
            this.iOpen.ImageIndex = 0;
            this.iOpen.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.iOpen.Name = "iOpen";
            this.iOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iOpen_ItemClick);
            // 
            // iSave
            // 
            this.iSave.Caption = "&Save";
            this.iSave.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iSave.Enabled = false;
            this.iSave.Id = 30;
            this.iSave.ImageIndex = 1;
            this.iSave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.iSave.Name = "iSave";
            // 
            // ipsWXP
            // 
            this.ipsWXP.Id = 38;
            this.ipsWXP.Name = "ipsWXP";
            // 
            // ipsOXP
            // 
            this.ipsOXP.Id = 39;
            this.ipsOXP.Name = "ipsOXP";
            // 
            // ipsO2K
            // 
            this.ipsO2K.Id = 40;
            this.ipsO2K.Name = "ipsO2K";
            // 
            // iPaintStyle
            // 
            this.iPaintStyle.Caption = "Paint Style";
            this.iPaintStyle.CategoryGuid = new System.Guid("b086ef9d-c758-46ba-a35f-058eada7ad13");
            this.iPaintStyle.Id = 35;
            this.iPaintStyle.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsDefault),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsWXP),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsOXP),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsO2K),
            new DevExpress.XtraBars.LinkPersistInfo(this.ipsO3)});
            this.iPaintStyle.Name = "iPaintStyle";
            this.iPaintStyle.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // ipsDefault
            // 
            this.ipsDefault.Id = 42;
            this.ipsDefault.Name = "ipsDefault";
            // 
            // ipsO3
            // 
            this.ipsO3.Id = 41;
            this.ipsO3.Name = "ipsO3";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 55);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(668, 410);
            this.webBrowser1.TabIndex = 4;
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            // 
            // BrowserDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(668, 490);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "BrowserDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompleX WebBrowser";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarLargeButtonItem iBack;
        private DevExpress.XtraBars.BarLargeButtonItem iForward;
        private DevExpress.XtraBars.BarLargeButtonItem iStop;
        private DevExpress.XtraBars.BarLargeButtonItem iRefresh;
        private DevExpress.XtraBars.BarLargeButtonItem iHome;
        private DevExpress.XtraBars.BarLargeButtonItem iSearch;
        private DevExpress.XtraBars.BarLargeButtonItem iFavorites;
        private DevExpress.XtraBars.BarLargeButtonItem iMedia;
        private DevExpress.XtraBars.BarLargeButtonItem iHistory;
        private DevExpress.XtraBars.BarLargeButtonItem iMail;
        private DevExpress.XtraBars.BarLargeButtonItem iPrint;
        private DevExpress.XtraBars.BarLargeButtonItem iEdit;
        private DevExpress.XtraBars.BarButtonItem iGo;
        private DevExpress.XtraBars.BarEditItem eAddress;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private DevExpress.XtraBars.BarEditItem eProgress;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarStaticItem iText;
        private DevExpress.XtraBars.BarToolbarsListItem iToolBars;
        private DevExpress.XtraBars.BarButtonItem iAbout;
        private DevExpress.XtraBars.BarButtonItem iExit;
        private DevExpress.XtraBars.BarButtonItem iAdd;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.BarButtonItem iSave;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarButtonItem ipsWXP;
        private DevExpress.XtraBars.BarButtonItem ipsOXP;
        private DevExpress.XtraBars.BarButtonItem ipsO2K;
        private DevExpress.XtraBars.BarSubItem iPaintStyle;
        private DevExpress.XtraBars.BarButtonItem ipsO3;
        private DevExpress.XtraBars.BarButtonItem ipsDefault;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private FavoritesControl favorites;
    }
}