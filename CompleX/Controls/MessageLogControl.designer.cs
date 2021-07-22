using CompleX.DataSets;

namespace CompleX.Controls
{
    partial class MessageLogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageLogControl));
            this.dataSetMessageLog = new CompleX.DataSets.DataSetMessageLog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.comboBoxLogType = new System.Windows.Forms.ToolStripComboBox();
            this.logBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxTime = new System.Windows.Forms.CheckBox();
            this.logGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridViewLogEntries = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colException = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.popupOptions = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerOptions = new DevExpress.XtraEditors.PopupContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMessageLog)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLogEntries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOptions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSetMessageLog
            // 
            this.dataSetMessageLog.DataSetName = "DataSetMessageLog";
            this.dataSetMessageLog.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripButtonClear,
            this.comboBoxLogType});
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSave, "toolStripButtonSave");
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Click += new System.EventHandler(this.ToolStripButtonSaveClick);
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonClear, "toolStripButtonClear");
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Click += new System.EventHandler(this.ToolStripButtonClearClick);
            // 
            // comboBoxLogType
            // 
            this.comboBoxLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogType.Name = "comboBoxLogType";
            resources.ApplyResources(this.comboBoxLogType, "comboBoxLogType");
            this.comboBoxLogType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLogTypeSelectedIndexChanged);
            // 
            // logBindingSource
            // 
            this.logBindingSource.DataMember = "Log";
            this.logBindingSource.DataSource = this.dataSetMessageLog;
            // 
            // checkBoxTime
            // 
            resources.ApplyResources(this.checkBoxTime, "checkBoxTime");
            this.checkBoxTime.Checked = true;
            this.checkBoxTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTime.Name = "checkBoxTime";
            this.checkBoxTime.UseVisualStyleBackColor = true;
            this.checkBoxTime.CheckedChanged += new System.EventHandler(this.CheckBoxTimeCheckedChanged);
            // 
            // logGridControl
            // 
            this.logGridControl.DataSource = this.logBindingSource;
            resources.ApplyResources(this.logGridControl, "logGridControl");
            this.logGridControl.MainView = this.gridViewLogEntries;
            this.logGridControl.MenuManager = this.barManager1;
            this.logGridControl.Name = "logGridControl";
            this.logGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLogEntries});
            this.logGridControl.DoubleClick += new System.EventHandler(this.LogGridControlDoubleClick);
            // 
            // gridViewLogEntries
            // 
            this.gridViewLogEntries.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colType,
            this.colMessage,
            this.colFile,
            this.colLine,
            this.colProject,
            this.colException});
            this.gridViewLogEntries.GridControl = this.logGridControl;
            this.gridViewLogEntries.Name = "gridViewLogEntries";
            this.gridViewLogEntries.OptionsBehavior.Editable = false;
            this.gridViewLogEntries.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridViewLogEntries.OptionsCustomization.AllowGroup = false;
            this.gridViewLogEntries.OptionsView.ShowGroupPanel = false;
            // 
            // colTime
            // 
            resources.ApplyResources(this.colTime, "colTime");
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            // 
            // colMessage
            // 
            resources.ApplyResources(this.colMessage, "colMessage");
            this.colMessage.FieldName = "Message";
            this.colMessage.Name = "colMessage";
            // 
            // colFile
            // 
            resources.ApplyResources(this.colFile, "colFile");
            this.colFile.FieldName = "File";
            this.colFile.Name = "colFile";
            // 
            // colLine
            // 
            resources.ApplyResources(this.colLine, "colLine");
            this.colLine.FieldName = "Line";
            this.colLine.Name = "colLine";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "Project";
            this.colProject.Name = "colProject";
            // 
            // colException
            // 
            resources.ApplyResources(this.colException, "colException");
            this.colException.FieldName = "Exception";
            this.colException.Name = "colException";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
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
            // popupOptions
            // 
            resources.ApplyResources(this.popupOptions, "popupOptions");
            this.popupOptions.Name = "popupOptions";
            this.popupOptions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popupOptions.Properties.Buttons"))))});
            this.popupOptions.Properties.PopupControl = this.popupContainerOptions;
            this.popupOptions.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.PopupOptionsClosed);
            this.popupOptions.Click += new System.EventHandler(this.PopupOptionsClick);
            // 
            // popupContainerOptions
            // 
            resources.ApplyResources(this.popupContainerOptions, "popupContainerOptions");
            this.popupContainerOptions.Name = "popupContainerOptions";
            // 
            // MessageLogControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.popupContainerOptions);
            this.Controls.Add(this.popupOptions);
            this.Controls.Add(this.logGridControl);
            this.Controls.Add(this.checkBoxTime);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "MessageLogControl";
            ((System.ComponentModel.ISupportInitialize)(this.dataSetMessageLog)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLogEntries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupOptions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerOptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataSetMessageLog dataSetMessageLog;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.ToolStripComboBox comboBoxLogType;
        private System.Windows.Forms.BindingSource logBindingSource;
        private System.Windows.Forms.CheckBox checkBoxTime;
        private DevExpress.XtraGrid.GridControl logGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLogEntries;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colMessage;
        private DevExpress.XtraGrid.Columns.GridColumn colFile;
        private DevExpress.XtraGrid.Columns.GridColumn colLine;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraGrid.Columns.GridColumn colException;
        private DevExpress.XtraEditors.PopupContainerEdit popupOptions;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerOptions;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}