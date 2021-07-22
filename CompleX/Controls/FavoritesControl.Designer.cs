namespace CompleX.Controls
{
    partial class FavoritesControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoritesControl));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.iAdd = new DevExpress.XtraBars.BarButtonItem();
            this.iEdit = new DevExpress.XtraBars.BarButtonItem();
            this.iDelete = new DevExpress.XtraBars.BarButtonItem();
            this.iOpen = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listBox1 = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.iAdd,
            this.iEdit,
            this.iDelete,
            this.iOpen});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Edit";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(43, 86);
            this.bar1.FloatSize = new System.Drawing.Size(20, 22);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.iAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.iDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.iOpen)});
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // iAdd
            // 
            resources.ApplyResources(this.iAdd, "iAdd");
            this.iAdd.Id = 0;
            this.iAdd.ImageIndex = 0;
            this.iAdd.Name = "iAdd";
            this.iAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iAdd_ItemClick);
            // 
            // iEdit
            // 
            resources.ApplyResources(this.iEdit, "iEdit");
            this.iEdit.Id = 1;
            this.iEdit.ImageIndex = 1;
            this.iEdit.Name = "iEdit";
            this.iEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iEdit_ItemClick);
            // 
            // iDelete
            // 
            resources.ApplyResources(this.iDelete, "iDelete");
            this.iDelete.Id = 2;
            this.iDelete.ImageIndex = 2;
            this.iDelete.Name = "iDelete";
            this.iDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iDelete_ItemClick);
            // 
            // iOpen
            // 
            resources.ApplyResources(this.iOpen, "iOpen");
            this.iOpen.Id = 3;
            this.iOpen.ImageIndex = 3;
            this.iOpen.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.iOpen.Name = "iOpen";
            this.iOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.iOpen_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.ItemHeight = 15;
            this.listBox1.Name = "listBox1";
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // FavoritesControl
            // 
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "FavoritesControl";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarButtonItem iAdd;
        private DevExpress.XtraBars.BarButtonItem iEdit;
        private DevExpress.XtraBars.BarButtonItem iDelete;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.ListBoxControl listBox1;
        private DevExpress.XtraBars.BarButtonItem iOpen;
        private DevExpress.XtraBars.Bar bar1;
        private System.ComponentModel.IContainer components;

    }
}