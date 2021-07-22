namespace CompleX.Dialogs
{
    partial class PromptSaveDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptSaveDialog));
            this.ContainerPanel = new DevExpress.XtraEditors.PanelControl();
            this.groupControl = new DevExpress.XtraEditors.GroupControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.BtnSaveNone = new DevExpress.XtraEditors.SimpleButton();
            this.dlgCancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSaveSelected = new DevExpress.XtraEditors.SimpleButton();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).BeginInit();
            this.ContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).BeginInit();
            this.groupControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // ContainerPanel
            // 
            this.ContainerPanel.AccessibleDescription = null;
            this.ContainerPanel.AccessibleName = null;
            resources.ApplyResources(this.ContainerPanel, "ContainerPanel");
            this.ContainerPanel.Controls.Add(this.groupControl);
            this.ErrorProvider.SetError(this.ContainerPanel, resources.GetString("ContainerPanel.Error"));
            this.ErrorProvider.SetIconAlignment(this.ContainerPanel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ContainerPanel.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.ContainerPanel, ((int)(resources.GetObject("ContainerPanel.IconPadding"))));
            this.ContainerPanel.Name = "ContainerPanel";
            // 
            // groupControl
            // 
            this.groupControl.AccessibleDescription = null;
            this.groupControl.AccessibleName = null;
            resources.ApplyResources(this.groupControl, "groupControl");
            this.groupControl.Controls.Add(this.pictureBox2);
            this.groupControl.Controls.Add(this.pictureBox1);
            this.groupControl.Controls.Add(this.listViewFiles);
            this.ErrorProvider.SetError(this.groupControl, resources.GetString("groupControl.Error"));
            this.ErrorProvider.SetIconAlignment(this.groupControl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupControl.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.groupControl, ((int)(resources.GetObject("groupControl.IconPadding"))));
            this.groupControl.Name = "groupControl";
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleDescription = null;
            this.pictureBox2.AccessibleName = null;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackgroundImage = null;
            this.ErrorProvider.SetError(this.pictureBox2, resources.GetString("pictureBox2.Error"));
            this.pictureBox2.Font = null;
            this.ErrorProvider.SetIconAlignment(this.pictureBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox2.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.pictureBox2, ((int)(resources.GetObject("pictureBox2.IconPadding"))));
            this.pictureBox2.Image = global::CompleX.Properties.Resources.imgSmallIcons;
            this.pictureBox2.ImageLocation = null;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackgroundImage = null;
            this.ErrorProvider.SetError(this.pictureBox1, resources.GetString("pictureBox1.Error"));
            this.pictureBox1.Font = null;
            this.ErrorProvider.SetIconAlignment(this.pictureBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("pictureBox1.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.pictureBox1, ((int)(resources.GetObject("pictureBox1.IconPadding"))));
            this.pictureBox1.Image = global::CompleX.Properties.Resources.imgLargeIcons1;
            this.pictureBox1.ImageLocation = null;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // listViewFiles
            // 
            this.listViewFiles.AccessibleDescription = null;
            this.listViewFiles.AccessibleName = null;
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.BackgroundImage = null;
            this.listViewFiles.CheckBoxes = true;
            this.ErrorProvider.SetError(this.listViewFiles, resources.GetString("listViewFiles.Error"));
            this.listViewFiles.Font = null;
            this.listViewFiles.FullRowSelect = true;
            this.ErrorProvider.SetIconAlignment(this.listViewFiles, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("listViewFiles.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.listViewFiles, ((int)(resources.GetObject("listViewFiles.IconPadding"))));
            this.listViewFiles.LargeImageList = this.imageListLarge;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.SmallImageList = this.imageList;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.List;
            this.listViewFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewFiles_MouseUp);
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "auswaehlen_16.png");
            this.imageListLarge.Images.SetKeyName(1, "auswaehlen_16_tauschen.png");
            this.imageListLarge.Images.SetKeyName(2, "auswaehlen_16_auffheben.png");
            this.imageListLarge.Images.SetKeyName(3, "datenebenen_neu_32.png");
            this.imageListLarge.Images.SetKeyName(4, "imgSmallIcons.PNG");
            this.imageListLarge.Images.SetKeyName(5, "imgLargeIcons.png");
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "auswaehlen_16.png");
            this.imageList.Images.SetKeyName(1, "auswaehlen_16_tauschen.png");
            this.imageList.Images.SetKeyName(2, "auswaehlen_16_auffheben.png");
            this.imageList.Images.SetKeyName(3, "datenebenen_neu_16.png");
            this.imageList.Images.SetKeyName(4, "imgSmallIcons.PNG");
            this.imageList.Images.SetKeyName(5, "imgLargeIcons.png");
            // 
            // panelControl
            // 
            this.panelControl.AccessibleDescription = null;
            this.panelControl.AccessibleName = null;
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.Controls.Add(this.BtnSaveNone);
            this.panelControl.Controls.Add(this.ContainerPanel);
            this.panelControl.Controls.Add(this.dlgCancelBtn);
            this.panelControl.Controls.Add(this.BtnSaveSelected);
            this.ErrorProvider.SetError(this.panelControl, resources.GetString("panelControl.Error"));
            this.ErrorProvider.SetIconAlignment(this.panelControl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panelControl.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.panelControl, ((int)(resources.GetObject("panelControl.IconPadding"))));
            this.panelControl.Name = "panelControl";
            // 
            // BtnSaveNone
            // 
            this.BtnSaveNone.AccessibleDescription = null;
            this.BtnSaveNone.AccessibleName = null;
            resources.ApplyResources(this.BtnSaveNone, "BtnSaveNone");
            this.BtnSaveNone.BackgroundImage = null;
            this.BtnSaveNone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ErrorProvider.SetError(this.BtnSaveNone, resources.GetString("BtnSaveNone.Error"));
            this.ErrorProvider.SetIconAlignment(this.BtnSaveNone, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BtnSaveNone.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.BtnSaveNone, ((int)(resources.GetObject("BtnSaveNone.IconPadding"))));
            this.BtnSaveNone.LookAndFeel.SkinName = "Blue";
            this.BtnSaveNone.Name = "BtnSaveNone";
            // 
            // dlgCancelBtn
            // 
            this.dlgCancelBtn.AccessibleDescription = null;
            this.dlgCancelBtn.AccessibleName = null;
            resources.ApplyResources(this.dlgCancelBtn, "dlgCancelBtn");
            this.dlgCancelBtn.BackgroundImage = null;
            this.dlgCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ErrorProvider.SetError(this.dlgCancelBtn, resources.GetString("dlgCancelBtn.Error"));
            this.ErrorProvider.SetIconAlignment(this.dlgCancelBtn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("dlgCancelBtn.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.dlgCancelBtn, ((int)(resources.GetObject("dlgCancelBtn.IconPadding"))));
            this.dlgCancelBtn.LookAndFeel.SkinName = "Blue";
            this.dlgCancelBtn.Name = "dlgCancelBtn";
            // 
            // BtnSaveSelected
            // 
            this.BtnSaveSelected.AccessibleDescription = null;
            this.BtnSaveSelected.AccessibleName = null;
            resources.ApplyResources(this.BtnSaveSelected, "BtnSaveSelected");
            this.BtnSaveSelected.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.BtnSaveSelected.Appearance.Options.UseFont = true;
            this.BtnSaveSelected.BackgroundImage = null;
            this.BtnSaveSelected.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ErrorProvider.SetError(this.BtnSaveSelected, resources.GetString("BtnSaveSelected.Error"));
            this.ErrorProvider.SetIconAlignment(this.BtnSaveSelected, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BtnSaveSelected.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.BtnSaveSelected, ((int)(resources.GetObject("BtnSaveSelected.IconPadding"))));
            this.BtnSaveSelected.LookAndFeel.SkinName = "Blue";
            this.BtnSaveSelected.Name = "BtnSaveSelected";
            this.BtnSaveSelected.Click += new System.EventHandler(this.BtnSaveSelected_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.ErrorProvider, "ErrorProvider");
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barSubItem1,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barEditItem1,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8});
            this.barManager.MaxItemId = 10;
            this.barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.AccessibleDescription = null;
            this.barDockControlTop.AccessibleName = null;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            this.ErrorProvider.SetError(this.barDockControlTop, resources.GetString("barDockControlTop.Error"));
            this.barDockControlTop.Font = null;
            this.ErrorProvider.SetIconAlignment(this.barDockControlTop, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("barDockControlTop.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.barDockControlTop, ((int)(resources.GetObject("barDockControlTop.IconPadding"))));
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.AccessibleDescription = null;
            this.barDockControlBottom.AccessibleName = null;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            this.ErrorProvider.SetError(this.barDockControlBottom, resources.GetString("barDockControlBottom.Error"));
            this.barDockControlBottom.Font = null;
            this.ErrorProvider.SetIconAlignment(this.barDockControlBottom, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("barDockControlBottom.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.barDockControlBottom, ((int)(resources.GetObject("barDockControlBottom.IconPadding"))));
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.AccessibleDescription = null;
            this.barDockControlLeft.AccessibleName = null;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            this.ErrorProvider.SetError(this.barDockControlLeft, resources.GetString("barDockControlLeft.Error"));
            this.barDockControlLeft.Font = null;
            this.ErrorProvider.SetIconAlignment(this.barDockControlLeft, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("barDockControlLeft.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.barDockControlLeft, ((int)(resources.GetObject("barDockControlLeft.IconPadding"))));
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.AccessibleDescription = null;
            this.barDockControlRight.AccessibleName = null;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            this.ErrorProvider.SetError(this.barDockControlRight, resources.GetString("barDockControlRight.Error"));
            this.barDockControlRight.Font = null;
            this.ErrorProvider.SetIconAlignment(this.barDockControlRight, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("barDockControlRight.IconAlignment"))));
            this.ErrorProvider.SetIconPadding(this.barDockControlRight, ((int)(resources.GetObject("barDockControlRight.IconPadding"))));
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.AccessibleDescription = null;
            this.barButtonItem1.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageIndex = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.AccessibleDescription = null;
            this.barButtonItem2.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ImageIndex = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.AccessibleDescription = null;
            this.barButtonItem3.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem3, "barButtonItem3");
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageIndex = 1;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.AccessibleDescription = null;
            this.barSubItem1.AccessibleName = null;
            resources.ApplyResources(this.barSubItem1, "barSubItem1");
            this.barSubItem1.Id = 3;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.AccessibleDescription = null;
            this.barButtonItem4.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
            this.barButtonItem4.Id = 4;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.AccessibleDescription = null;
            this.barButtonItem6.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem6, "barButtonItem6");
            this.barButtonItem6.Id = 7;
            this.barButtonItem6.ImageIndex = 4;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.AccessibleDescription = null;
            this.barButtonItem7.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem7, "barButtonItem7");
            this.barButtonItem7.Id = 8;
            this.barButtonItem7.ImageIndex = 5;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.AccessibleDescription = null;
            this.barButtonItem5.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barEditItem1
            // 
            this.barEditItem1.AccessibleDescription = null;
            this.barEditItem1.AccessibleName = null;
            resources.ApplyResources(this.barEditItem1, "barEditItem1");
            this.barEditItem1.Edit = this.repositoryItemCheckEdit1;
            this.barEditItem1.Id = 6;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AccessibleDescription = null;
            this.repositoryItemCheckEdit1.AccessibleName = null;
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.AccessibleDescription = null;
            this.barButtonItem8.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem8, "barButtonItem8");
            this.barButtonItem8.Id = 9;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // PromptSaveDialog
            // 
            this.AcceptButton = this.BtnSaveSelected;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.dlgCancelBtn;
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromptSaveDialog";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).EndInit();
            this.ContainerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl)).EndInit();
            this.groupControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.PanelControl ContainerPanel;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.SimpleButton dlgCancelBtn;
        private DevExpress.XtraEditors.SimpleButton BtnSaveSelected;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        private DevExpress.XtraEditors.SimpleButton BtnSaveNone;
        private DevExpress.XtraEditors.GroupControl groupControl;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private System.Windows.Forms.ImageList imageListLarge;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}