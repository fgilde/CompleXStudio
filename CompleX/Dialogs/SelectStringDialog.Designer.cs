namespace CompleX.Dialogs
{
    partial class SelectStringDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectStringDialog));
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.clipboardGridControl = new DevExpress.XtraGrid.GridControl();
            this.clipboardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetClipboard = new CompleX.DataSets.DataSetClipboard();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.dlgCancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.dlgOkBtn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetClipboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.AccessibleDescription = null;
            this.splitContainerControl.AccessibleName = null;
            resources.ApplyResources(this.splitContainerControl, "splitContainerControl");
            this.splitContainerControl.Horizontal = false;
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.clipboardGridControl);
            resources.ApplyResources(this.splitContainerControl.Panel1, "splitContainerControl.Panel1");
            this.splitContainerControl.Panel2.Controls.Add(this.memoEdit1);
            resources.ApplyResources(this.splitContainerControl.Panel2, "splitContainerControl.Panel2");
            this.splitContainerControl.SplitterPosition = 98;
            // 
            // clipboardGridControl
            // 
            this.clipboardGridControl.AccessibleDescription = null;
            this.clipboardGridControl.AccessibleName = null;
            resources.ApplyResources(this.clipboardGridControl, "clipboardGridControl");
            this.clipboardGridControl.BackgroundImage = null;
            this.clipboardGridControl.DataSource = this.clipboardBindingSource;
            this.clipboardGridControl.EmbeddedNavigator.AccessibleDescription = null;
            this.clipboardGridControl.EmbeddedNavigator.AccessibleName = null;
            this.clipboardGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.clipboardGridControl.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.Anchor")));
            this.clipboardGridControl.EmbeddedNavigator.BackgroundImage = null;
            this.clipboardGridControl.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.BackgroundImageLayout")));
            this.clipboardGridControl.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.ImeMode")));
            this.clipboardGridControl.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.TextLocation")));
            this.clipboardGridControl.EmbeddedNavigator.ToolTip = resources.GetString("clipboardGridControl.EmbeddedNavigator.ToolTip");
            this.clipboardGridControl.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("clipboardGridControl.EmbeddedNavigator.ToolTipIconType")));
            this.clipboardGridControl.EmbeddedNavigator.ToolTipTitle = resources.GetString("clipboardGridControl.EmbeddedNavigator.ToolTipTitle");
            this.clipboardGridControl.Font = null;
            this.clipboardGridControl.MainView = this.gridView1;
            this.clipboardGridControl.Name = "clipboardGridControl";
            this.clipboardGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.clipboardGridControl.DoubleClick += new System.EventHandler(this.clipboardGridControl_DoubleClick);
            // 
            // clipboardBindingSource
            // 
            this.clipboardBindingSource.DataMember = "Clipboard";
            this.clipboardBindingSource.DataSource = this.dataSetClipboard;
            // 
            // dataSetClipboard
            // 
            this.dataSetClipboard.DataSetName = "DataSetClipboard";
            this.dataSetClipboard.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colText});
            this.gridView1.GridControl = this.clipboardGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.OptionsView.ShowPreviewLines = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colText
            // 
            resources.ApplyResources(this.colText, "colText");
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            // 
            // memoEdit1
            // 
            resources.ApplyResources(this.memoEdit1, "memoEdit1");
            this.memoEdit1.BackgroundImage = null;
            this.memoEdit1.EditValue = null;
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.AccessibleDescription = null;
            this.memoEdit1.Properties.AccessibleName = null;
            this.memoEdit1.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.memoEdit1.Properties.Appearance.Options.UseBackColor = true;
            // 
            // dlgCancelBtn
            // 
            this.dlgCancelBtn.AccessibleDescription = null;
            this.dlgCancelBtn.AccessibleName = null;
            resources.ApplyResources(this.dlgCancelBtn, "dlgCancelBtn");
            this.dlgCancelBtn.BackgroundImage = null;
            this.dlgCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dlgCancelBtn.LookAndFeel.SkinName = "Blue";
            this.dlgCancelBtn.Name = "dlgCancelBtn";
            // 
            // dlgOkBtn
            // 
            this.dlgOkBtn.AccessibleDescription = null;
            this.dlgOkBtn.AccessibleName = null;
            resources.ApplyResources(this.dlgOkBtn, "dlgOkBtn");
            this.dlgOkBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dlgOkBtn.Appearance.Options.UseFont = true;
            this.dlgOkBtn.BackgroundImage = null;
            this.dlgOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dlgOkBtn.LookAndFeel.SkinName = "Blue";
            this.dlgOkBtn.Name = "dlgOkBtn";
            // 
            // SelectStringDialog
            // 
            this.AcceptButton = this.dlgOkBtn;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.dlgCancelBtn;
            this.Controls.Add(this.splitContainerControl);
            this.Controls.Add(this.dlgCancelBtn);
            this.Controls.Add(this.dlgOkBtn);
            this.Icon = null;
            this.Name = "SelectStringDialog";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clipboardGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clipboardBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetClipboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CompleX.DataSets.DataSetClipboard dataSetClipboard;
        private System.Windows.Forms.BindingSource clipboardBindingSource;
        private DevExpress.XtraEditors.SimpleButton dlgCancelBtn;
        private DevExpress.XtraEditors.SimpleButton dlgOkBtn;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraGrid.GridControl clipboardGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}