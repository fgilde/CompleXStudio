namespace CompleX.Dialogs
{
    public partial class MainEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainEditForm));
            this.splitContainerMainHost = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelEditHost = new System.Windows.Forms.Panel();
            this.barManagerEditor = new DevExpress.XtraBars.BarManager(this.components);
            this.barViewMode = new DevExpress.XtraBars.Bar();
            this.barButtonCode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonSplit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDesign = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItemChangeSourceEdit = new DevExpress.XtraBars.BarSubItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.labelFileName = new DevExpress.XtraBars.BarStaticItem();
            this.CheckItemSaved = new DevExpress.XtraBars.BarCheckItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.popupMenuDesigners = new DevExpress.XtraBars.PopupMenu(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainHost)).BeginInit();
            this.splitContainerMainHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDesigners)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMainHost
            // 
            resources.ApplyResources(this.splitContainerMainHost, "splitContainerMainHost");
            this.splitContainerMainHost.Horizontal = false;
            this.splitContainerMainHost.Name = "splitContainerMainHost";
            this.splitContainerMainHost.Panel1.Controls.Add(this.panelEditHost);
            resources.ApplyResources(this.splitContainerMainHost.Panel1, "splitContainerMainHost.Panel1");
            resources.ApplyResources(this.splitContainerMainHost.Panel2, "splitContainerMainHost.Panel2");
            this.splitContainerMainHost.SplitterPosition = 221;
            // 
            // panelEditHost
            // 
            resources.ApplyResources(this.panelEditHost, "panelEditHost");
            this.panelEditHost.Name = "panelEditHost";
            // 
            // barManagerEditor
            // 
            this.barManagerEditor.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barViewMode,
            this.bar3});
            this.barManagerEditor.DockControls.Add(this.barDockControlTop);
            this.barManagerEditor.DockControls.Add(this.barDockControlBottom);
            this.barManagerEditor.DockControls.Add(this.barDockControlLeft);
            this.barManagerEditor.DockControls.Add(this.barDockControlRight);
            this.barManagerEditor.Form = this;
            this.barManagerEditor.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonCode,
            this.barButtonSplit,
            this.barButtonDesign,
            this.barSubItem1,
            this.barSubItem2,
            this.labelFileName,
            this.CheckItemSaved,
            this.barSubItemChangeSourceEdit});
            this.barManagerEditor.MaxItemId = 19;
            this.barManagerEditor.StatusBar = this.bar3;
            // 
            // barViewMode
            // 
            this.barViewMode.BarName = "Tools";
            this.barViewMode.DockCol = 0;
            this.barViewMode.DockRow = 0;
            this.barViewMode.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barViewMode.FloatLocation = new System.Drawing.Point(1314, 217);
            this.barViewMode.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonCode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemChangeSourceEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonSplit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonDesign, true)});
            this.barViewMode.OptionsBar.AllowQuickCustomization = false;
            this.barViewMode.OptionsBar.DrawDragBorder = false;
            this.barViewMode.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barViewMode, "barViewMode");
            this.barViewMode.Visible = false;
            // 
            // barButtonCode
            // 
            this.barButtonCode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            resources.ApplyResources(this.barButtonCode, "barButtonCode");
            this.barButtonCode.GroupIndex = 1;
            this.barButtonCode.Id = 1;
            this.barButtonCode.Name = "barButtonCode";
            this.barButtonCode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem1ItemClick);
            // 
            // barButtonSplit
            // 
            this.barButtonSplit.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            resources.ApplyResources(this.barButtonSplit, "barButtonSplit");
            this.barButtonSplit.Enabled = false;
            this.barButtonSplit.GroupIndex = 1;
            this.barButtonSplit.Id = 2;
            this.barButtonSplit.Name = "barButtonSplit";
            this.barButtonSplit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem2ItemClick);
            // 
            // barButtonDesign
            // 
            this.barButtonDesign.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            resources.ApplyResources(this.barButtonDesign, "barButtonDesign");
            this.barButtonDesign.GroupIndex = 1;
            this.barButtonDesign.Id = 6;
            this.barButtonDesign.Name = "barButtonDesign";
            this.barButtonDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonDesignItemClick);
            // 
            // barSubItemChangeSourceEdit
            // 
            this.barSubItemChangeSourceEdit.Id = 18;
            this.barSubItemChangeSourceEdit.Name = "barSubItemChangeSourceEdit";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.labelFileName),
            new DevExpress.XtraBars.LinkPersistInfo(this.CheckItemSaved, true)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // labelFileName
            // 
            this.labelFileName.Id = 15;
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // CheckItemSaved
            // 
            this.CheckItemSaved.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.CheckItemSaved, "CheckItemSaved");
            this.CheckItemSaved.Enabled = false;
            this.CheckItemSaved.Id = 16;
            this.CheckItemSaved.Name = "CheckItemSaved";
            // 
            // barDockControlTop
            // 
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // barSubItem1
            // 
            resources.ApplyResources(this.barSubItem1, "barSubItem1");
            this.barSubItem1.Id = 8;
            this.barSubItem1.MergeType = DevExpress.XtraBars.BarMenuMerge.MergeItems;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barSubItem2
            // 
            resources.ApplyResources(this.barSubItem2, "barSubItem2");
            this.barSubItem2.Id = 9;
            this.barSubItem2.MergeType = DevExpress.XtraBars.BarMenuMerge.Replace;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // popupMenuDesigners
            // 
            this.popupMenuDesigners.Manager = this.barManagerEditor;
            this.popupMenuDesigners.Name = "popupMenuDesigners";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "abweichungsanalyse_16.png");
            this.imageList1.Images.SetKeyName(1, "abc_analyse_16.png");
            // 
            // MainEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMainHost);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainEditForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainEditForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainHost)).EndInit();
            this.splitContainerMainHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDesigners)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerMainHost;
        private System.Windows.Forms.Panel panelEditHost;
        private DevExpress.XtraBars.BarManager barManagerEditor;
        private DevExpress.XtraBars.Bar barViewMode;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonCode;
        private DevExpress.XtraBars.BarButtonItem barButtonSplit;
        private DevExpress.XtraBars.BarButtonItem barButtonDesign;
        private DevExpress.XtraBars.PopupMenu popupMenuDesigners;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarStaticItem labelFileName;
        private DevExpress.XtraBars.BarCheckItem CheckItemSaved;
        private DevExpress.XtraBars.BarSubItem barSubItemChangeSourceEdit;


    }
}