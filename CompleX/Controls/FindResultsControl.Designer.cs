namespace CompleX.Controls
{
    partial class FindResultsControl
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
            this.dataSetFindResultsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetFindResults = new CompleX.DataSets.DataSetFindResults();
            this.gridControlResults = new DevExpress.XtraGrid.GridControl();
            this.gridViewResults = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFilename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinenumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartposition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndposition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.simpleButtonClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetFindResultsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetFindResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSetFindResultsBindingSource
            // 
            this.dataSetFindResultsBindingSource.DataMember = "TableFindResults";
            this.dataSetFindResultsBindingSource.DataSource = this.dataSetFindResults;
            // 
            // dataSetFindResults
            // 
            this.dataSetFindResults.DataSetName = "DataSetFindResults";
            this.dataSetFindResults.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridControlResults
            // 
            this.gridControlResults.DataSource = this.dataSetFindResultsBindingSource;
            this.gridControlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlResults.Location = new System.Drawing.Point(0, 0);
            this.gridControlResults.MainView = this.gridViewResults;
            this.gridControlResults.MenuManager = this.barManager;
            this.gridControlResults.Name = "gridControlResults";
            this.gridControlResults.Size = new System.Drawing.Size(598, 204);
            this.gridControlResults.TabIndex = 1;
            this.gridControlResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResults});
            // 
            // gridViewResults
            // 
            this.gridViewResults.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFilename,
            this.colMatch,
            this.colLinenumber,
            this.colStartposition,
            this.colEndposition});
            this.gridViewResults.GridControl = this.gridControlResults;
            this.gridViewResults.Name = "gridViewResults";
            this.gridViewResults.OptionsBehavior.Editable = false;
            this.gridViewResults.OptionsBehavior.ReadOnly = true;
            // 
            // colFilename
            // 
            this.colFilename.FieldName = "Filename";
            this.colFilename.Name = "colFilename";
            this.colFilename.Visible = true;
            this.colFilename.VisibleIndex = 0;
            // 
            // colMatch
            // 
            this.colMatch.FieldName = "Match";
            this.colMatch.Name = "colMatch";
            this.colMatch.Visible = true;
            this.colMatch.VisibleIndex = 1;
            // 
            // colLinenumber
            // 
            this.colLinenumber.FieldName = "Linenumber";
            this.colLinenumber.Name = "colLinenumber";
            this.colLinenumber.Visible = true;
            this.colLinenumber.VisibleIndex = 2;
            // 
            // colStartposition
            // 
            this.colStartposition.FieldName = "Startposition";
            this.colStartposition.Name = "colStartposition";
            this.colStartposition.Visible = true;
            this.colStartposition.VisibleIndex = 3;
            // 
            // colEndposition
            // 
            this.colEndposition.FieldName = "Endposition";
            this.colEndposition.Name = "colEndposition";
            this.colEndposition.Visible = true;
            this.colEndposition.VisibleIndex = 4;
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(598, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 204);
            this.barDockControlBottom.Size = new System.Drawing.Size(598, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 204);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(598, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 204);
            // 
            // simpleButtonClear
            // 
            this.simpleButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonClear.Location = new System.Drawing.Point(520, 6);
            this.simpleButtonClear.Name = "simpleButtonClear";
            this.simpleButtonClear.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonClear.TabIndex = 6;
            this.simpleButtonClear.Text = "Clear";
            this.simpleButtonClear.Click += new System.EventHandler(this.simpleButtonClear_Click);
            // 
            // FindResultsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButtonClear);
            this.Controls.Add(this.gridControlResults);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FindResultsControl";
            this.Size = new System.Drawing.Size(598, 204);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetFindResultsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetFindResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dataSetFindResultsBindingSource;
        private DataSets.DataSetFindResults dataSetFindResults;
        private DevExpress.XtraGrid.GridControl gridControlResults;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResults;
        private DevExpress.XtraGrid.Columns.GridColumn colFilename;
        private DevExpress.XtraGrid.Columns.GridColumn colMatch;
        private DevExpress.XtraGrid.Columns.GridColumn colLinenumber;
        private DevExpress.XtraGrid.Columns.GridColumn colStartposition;
        private DevExpress.XtraGrid.Columns.GridColumn colEndposition;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClear;

    }
}
