namespace CompleX.Controls
{
    partial class ToolBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBox));
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem3 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem5 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem7 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem8 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem9 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem10 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem11 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem12 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem13 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem14 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem15 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem16 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem17 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem18 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem19 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem20 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem21 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem22 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem23 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem24 = new DevExpress.XtraNavBar.NavBarItem();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.barManagerToolBox = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonRenameItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDeleteItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonConfigureItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonRenameGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDeleteGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonAddItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonExportToolBox = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonImportToolBox = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonShowAll = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuEditEntry = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenuEditGroup = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelCtrls = new System.Windows.Forms.Panel();
            this.textEditSearch = new CompleX.Controls.SearchTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerToolBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuEditEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuEditGroup)).BeginInit();
            this.panelCtrls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl
            // 
            this.navBarControl.ActiveGroup = null;
            this.navBarControl.ContentButtonHint = null;
            resources.ApplyResources(this.navBarControl, "navBarControl");
            this.navBarControl.DragDropFlags = ((DevExpress.XtraNavBar.NavBarDragDrop)(((DevExpress.XtraNavBar.NavBarDragDrop.AllowDrag | DevExpress.XtraNavBar.NavBarDragDrop.AllowDrop) 
            | DevExpress.XtraNavBar.NavBarDragDrop.AllowOuterDrop)));
            this.navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2,
            this.navBarItem3,
            this.navBarItem4,
            this.navBarItem5,
            this.navBarItem7,
            this.navBarItem8,
            this.navBarItem9,
            this.navBarItem10,
            this.navBarItem11,
            this.navBarItem12,
            this.navBarItem13,
            this.navBarItem14,
            this.navBarItem15,
            this.navBarItem16,
            this.navBarItem17,
            this.navBarItem18,
            this.navBarItem19,
            this.navBarItem20,
            this.navBarItem21,
            this.navBarItem22,
            this.navBarItem23,
            this.navBarItem24});
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.OptionsNavPane.ExpandedWidth = ((int)(resources.GetObject("resource.ExpandedWidth")));
            this.navBarControl.SmallImages = this.imageList3;
            this.navBarControl.StoreDefaultPaintStyleName = true;
            this.navBarControl.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarControl_LinkClicked);
            this.navBarControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.navBarControl_DragDrop);
            this.navBarControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.navBarControl_DragEnter);
            this.navBarControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.navBarControl_MouseUp);
            // 
            // navBarItem1
            // 
            resources.ApplyResources(this.navBarItem1, "navBarItem1");
            this.navBarItem1.Name = "navBarItem1";
            // 
            // navBarItem2
            // 
            resources.ApplyResources(this.navBarItem2, "navBarItem2");
            this.navBarItem2.Name = "navBarItem2";
            // 
            // navBarItem3
            // 
            resources.ApplyResources(this.navBarItem3, "navBarItem3");
            this.navBarItem3.Name = "navBarItem3";
            // 
            // navBarItem4
            // 
            resources.ApplyResources(this.navBarItem4, "navBarItem4");
            this.navBarItem4.Enabled = false;
            this.navBarItem4.Name = "navBarItem4";
            // 
            // navBarItem5
            // 
            resources.ApplyResources(this.navBarItem5, "navBarItem5");
            this.navBarItem5.Name = "navBarItem5";
            // 
            // navBarItem7
            // 
            resources.ApplyResources(this.navBarItem7, "navBarItem7");
            this.navBarItem7.Name = "navBarItem7";
            // 
            // navBarItem8
            // 
            resources.ApplyResources(this.navBarItem8, "navBarItem8");
            this.navBarItem8.Name = "navBarItem8";
            // 
            // navBarItem9
            // 
            resources.ApplyResources(this.navBarItem9, "navBarItem9");
            this.navBarItem9.Name = "navBarItem9";
            // 
            // navBarItem10
            // 
            resources.ApplyResources(this.navBarItem10, "navBarItem10");
            this.navBarItem10.Name = "navBarItem10";
            // 
            // navBarItem11
            // 
            resources.ApplyResources(this.navBarItem11, "navBarItem11");
            this.navBarItem11.Name = "navBarItem11";
            // 
            // navBarItem12
            // 
            resources.ApplyResources(this.navBarItem12, "navBarItem12");
            this.navBarItem12.Name = "navBarItem12";
            // 
            // navBarItem13
            // 
            resources.ApplyResources(this.navBarItem13, "navBarItem13");
            this.navBarItem13.Enabled = false;
            this.navBarItem13.Name = "navBarItem13";
            // 
            // navBarItem14
            // 
            resources.ApplyResources(this.navBarItem14, "navBarItem14");
            this.navBarItem14.Enabled = false;
            this.navBarItem14.Name = "navBarItem14";
            // 
            // navBarItem15
            // 
            resources.ApplyResources(this.navBarItem15, "navBarItem15");
            this.navBarItem15.Name = "navBarItem15";
            // 
            // navBarItem16
            // 
            resources.ApplyResources(this.navBarItem16, "navBarItem16");
            this.navBarItem16.Name = "navBarItem16";
            // 
            // navBarItem17
            // 
            resources.ApplyResources(this.navBarItem17, "navBarItem17");
            this.navBarItem17.Name = "navBarItem17";
            // 
            // navBarItem18
            // 
            resources.ApplyResources(this.navBarItem18, "navBarItem18");
            this.navBarItem18.Name = "navBarItem18";
            // 
            // navBarItem19
            // 
            resources.ApplyResources(this.navBarItem19, "navBarItem19");
            this.navBarItem19.Name = "navBarItem19";
            // 
            // navBarItem20
            // 
            resources.ApplyResources(this.navBarItem20, "navBarItem20");
            this.navBarItem20.Name = "navBarItem20";
            // 
            // navBarItem21
            // 
            resources.ApplyResources(this.navBarItem21, "navBarItem21");
            this.navBarItem21.Name = "navBarItem21";
            // 
            // navBarItem22
            // 
            resources.ApplyResources(this.navBarItem22, "navBarItem22");
            this.navBarItem22.Name = "navBarItem22";
            // 
            // navBarItem23
            // 
            resources.ApplyResources(this.navBarItem23, "navBarItem23");
            this.navBarItem23.Name = "navBarItem23";
            // 
            // navBarItem24
            // 
            resources.ApplyResources(this.navBarItem24, "navBarItem24");
            this.navBarItem24.Name = "navBarItem24";
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
            this.imageList3.Images.SetKeyName(24, "löschen_16.png");
            this.imageList3.Images.SetKeyName(25, "bearbeiten_16.png");
            this.imageList3.Images.SetKeyName(26, "neues_objekt_16.png");
            this.imageList3.Images.SetKeyName(27, "neuer_eintrag_16x16.png");
            this.imageList3.Images.SetKeyName(28, "werkzeuge_16.png");
            this.imageList3.Images.SetKeyName(29, "ordner_16.png");
            this.imageList3.Images.SetKeyName(30, "exportieren_16.png");
            this.imageList3.Images.SetKeyName(31, "importieren_16.png");
            // 
            // barManagerToolBox
            // 
            this.barManagerToolBox.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            ((DevExpress.XtraBars.BarManagerCategory)(resources.GetObject("barManagerToolBox.Categories"))),
            ((DevExpress.XtraBars.BarManagerCategory)(resources.GetObject("barManagerToolBox.Categories1")))});
            this.barManagerToolBox.DockControls.Add(this.barDockControlTop);
            this.barManagerToolBox.DockControls.Add(this.barDockControlBottom);
            this.barManagerToolBox.DockControls.Add(this.barDockControlLeft);
            this.barManagerToolBox.DockControls.Add(this.barDockControlRight);
            this.barManagerToolBox.Form = this;
            this.barManagerToolBox.Images = this.imageList3;
            this.barManagerToolBox.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonRenameItem,
            this.barButtonDeleteItem,
            this.barButtonConfigureItem,
            this.barButtonRenameGroup,
            this.barButtonDeleteGroup,
            this.barButtonAddGroup,
            this.barButtonAddItem,
            this.barButtonExportToolBox,
            this.barButtonImportToolBox,
            this.barButtonShowAll});
            this.barManagerToolBox.MaxItemId = 10;
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
            // barButtonRenameItem
            // 
            resources.ApplyResources(this.barButtonRenameItem, "barButtonRenameItem");
            this.barButtonRenameItem.CategoryGuid = new System.Guid("65f1349a-6957-411e-b524-27413d7a0757");
            this.barButtonRenameItem.Id = 0;
            this.barButtonRenameItem.ImageIndex = 25;
            this.barButtonRenameItem.Name = "barButtonRenameItem";
            this.barButtonRenameItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRenameItem_ItemClick);
            // 
            // barButtonDeleteItem
            // 
            resources.ApplyResources(this.barButtonDeleteItem, "barButtonDeleteItem");
            this.barButtonDeleteItem.CategoryGuid = new System.Guid("65f1349a-6957-411e-b524-27413d7a0757");
            this.barButtonDeleteItem.Id = 1;
            this.barButtonDeleteItem.ImageIndex = 24;
            this.barButtonDeleteItem.Name = "barButtonDeleteItem";
            this.barButtonDeleteItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDeleteItem_ItemClick);
            // 
            // barButtonConfigureItem
            // 
            resources.ApplyResources(this.barButtonConfigureItem, "barButtonConfigureItem");
            this.barButtonConfigureItem.CategoryGuid = new System.Guid("65f1349a-6957-411e-b524-27413d7a0757");
            this.barButtonConfigureItem.Id = 2;
            this.barButtonConfigureItem.ImageIndex = 28;
            this.barButtonConfigureItem.Name = "barButtonConfigureItem";
            this.barButtonConfigureItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonConfigureItem_ItemClick);
            // 
            // barButtonRenameGroup
            // 
            resources.ApplyResources(this.barButtonRenameGroup, "barButtonRenameGroup");
            this.barButtonRenameGroup.CategoryGuid = new System.Guid("dbe3466e-a02e-4aed-b38e-8013fd7d2b81");
            this.barButtonRenameGroup.Id = 3;
            this.barButtonRenameGroup.ImageIndex = 25;
            this.barButtonRenameGroup.Name = "barButtonRenameGroup";
            this.barButtonRenameGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonRenameGroup_ItemClick);
            // 
            // barButtonDeleteGroup
            // 
            resources.ApplyResources(this.barButtonDeleteGroup, "barButtonDeleteGroup");
            this.barButtonDeleteGroup.CategoryGuid = new System.Guid("dbe3466e-a02e-4aed-b38e-8013fd7d2b81");
            this.barButtonDeleteGroup.Id = 4;
            this.barButtonDeleteGroup.ImageIndex = 24;
            this.barButtonDeleteGroup.Name = "barButtonDeleteGroup";
            this.barButtonDeleteGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDeleteGroup_ItemClick);
            // 
            // barButtonAddGroup
            // 
            resources.ApplyResources(this.barButtonAddGroup, "barButtonAddGroup");
            this.barButtonAddGroup.CategoryGuid = new System.Guid("dbe3466e-a02e-4aed-b38e-8013fd7d2b81");
            this.barButtonAddGroup.Id = 5;
            this.barButtonAddGroup.ImageIndex = 29;
            this.barButtonAddGroup.Name = "barButtonAddGroup";
            this.barButtonAddGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAddGroup_ItemClick);
            // 
            // barButtonAddItem
            // 
            resources.ApplyResources(this.barButtonAddItem, "barButtonAddItem");
            this.barButtonAddItem.CategoryGuid = new System.Guid("65f1349a-6957-411e-b524-27413d7a0757");
            this.barButtonAddItem.Id = 6;
            this.barButtonAddItem.ImageIndex = 26;
            this.barButtonAddItem.Name = "barButtonAddItem";
            this.barButtonAddItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonAddItem_ItemClick);
            // 
            // barButtonExportToolBox
            // 
            resources.ApplyResources(this.barButtonExportToolBox, "barButtonExportToolBox");
            this.barButtonExportToolBox.Id = 7;
            this.barButtonExportToolBox.ImageIndex = 30;
            this.barButtonExportToolBox.Name = "barButtonExportToolBox";
            this.barButtonExportToolBox.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonExportToolBox_ItemClick);
            // 
            // barButtonImportToolBox
            // 
            resources.ApplyResources(this.barButtonImportToolBox, "barButtonImportToolBox");
            this.barButtonImportToolBox.Id = 8;
            this.barButtonImportToolBox.ImageIndex = 31;
            this.barButtonImportToolBox.Name = "barButtonImportToolBox";
            this.barButtonImportToolBox.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonImportToolBox_ItemClick);
            // 
            // barButtonShowAll
            // 
            resources.ApplyResources(this.barButtonShowAll, "barButtonShowAll");
            this.barButtonShowAll.Id = 9;
            this.barButtonShowAll.Name = "barButtonShowAll";
            this.barButtonShowAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonShowAll_ItemClick);
            // 
            // popupMenuEditEntry
            // 
            this.popupMenuEditEntry.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonConfigureItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRenameItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonDeleteItem),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonExportToolBox, "", true, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonImportToolBox, "", false, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonShowAll, "", true, false, true, 0)});
            this.popupMenuEditEntry.Manager = this.barManagerToolBox;
            this.popupMenuEditEntry.Name = "popupMenuEditEntry";
            // 
            // popupMenuEditGroup
            // 
            this.popupMenuEditGroup.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonAddItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonRenameGroup, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonDeleteGroup),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonExportToolBox, "", true, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonImportToolBox, "", false, false, true, 0),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, this.barButtonShowAll, "", true, false, true, 0)});
            this.popupMenuEditGroup.Manager = this.barManagerToolBox;
            this.popupMenuEditGroup.Name = "popupMenuEditGroup";
            // 
            // panelCtrls
            // 
            this.panelCtrls.Controls.Add(this.textEditSearch);
            resources.ApplyResources(this.panelCtrls, "panelCtrls");
            this.panelCtrls.Name = "panelCtrls";
            // 
            // textEditSearch
            // 
            resources.ApplyResources(this.textEditSearch, "textEditSearch");
            this.textEditSearch.Name = "textEditSearch";
            this.textEditSearch.Properties.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("textEditSearch.Properties.Appearance.ForeColor")));
            this.textEditSearch.Properties.Appearance.Options.UseForeColor = true;
            this.textEditSearch.EditValueChanged += new System.EventHandler(this.textEditSearch_EditValueChanged);
            // 
            // ToolBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl);
            this.Controls.Add(this.panelCtrls);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ToolBox";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerToolBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuEditEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuEditGroup)).EndInit();
            this.panelCtrls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraNavBar.NavBarItem navBarItem7;
        private DevExpress.XtraNavBar.NavBarItem navBarItem8;
        private DevExpress.XtraNavBar.NavBarItem navBarItem9;
        private DevExpress.XtraNavBar.NavBarItem navBarItem10;
        private DevExpress.XtraNavBar.NavBarItem navBarItem11;
        private DevExpress.XtraNavBar.NavBarItem navBarItem12;
        private DevExpress.XtraNavBar.NavBarItem navBarItem13;
        private DevExpress.XtraNavBar.NavBarItem navBarItem14;
        private DevExpress.XtraNavBar.NavBarItem navBarItem15;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarItem navBarItem3;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarItem navBarItem5;
        private DevExpress.XtraNavBar.NavBarItem navBarItem20;
        private DevExpress.XtraNavBar.NavBarItem navBarItem23;
        private DevExpress.XtraNavBar.NavBarItem navBarItem16;
        private DevExpress.XtraNavBar.NavBarItem navBarItem21;
        private DevExpress.XtraNavBar.NavBarItem navBarItem22;
        private DevExpress.XtraNavBar.NavBarItem navBarItem18;
        private DevExpress.XtraNavBar.NavBarItem navBarItem17;
        private DevExpress.XtraNavBar.NavBarItem navBarItem19;
        private DevExpress.XtraNavBar.NavBarItem navBarItem24;
        private System.Windows.Forms.ImageList imageList3;
        private DevExpress.XtraBars.BarManager barManagerToolBox;
        //private DevExpress.XtraBars.BarDockControl barDockControlTop;
        //private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        //private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        //private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenuEditEntry;
        private DevExpress.XtraBars.BarButtonItem barButtonRenameItem;
        private DevExpress.XtraBars.BarButtonItem barButtonDeleteItem;
        private DevExpress.XtraBars.BarButtonItem barButtonConfigureItem;
        private DevExpress.XtraBars.BarButtonItem barButtonRenameGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonDeleteGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonAddGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonAddItem;
        private DevExpress.XtraBars.PopupMenu popupMenuEditGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonExportToolBox;
        private DevExpress.XtraBars.BarButtonItem barButtonImportToolBox;
        private DevExpress.XtraBars.BarButtonItem barButtonShowAll;
        private System.Windows.Forms.Panel panelCtrls;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private SearchTextBox textEditSearch;
    }
}
