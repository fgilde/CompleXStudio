namespace CompleX.Controls
{
    partial class TagEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagEditControl));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeViewCategories = new System.Windows.Forms.TreeView();
            this.panelConfig = new DevExpress.XtraEditors.PanelControl();
            this.groupSettings = new DevExpress.XtraEditors.GroupControl();
            this.panelEditHost = new DevExpress.XtraEditors.PanelControl();
            this.InfoLabel = new System.Windows.Forms.LinkLabel();
            this.groupControlValueEdit = new DevExpress.XtraEditors.GroupControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelConfig)).BeginInit();
            this.panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupSettings)).BeginInit();
            this.groupSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEditHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlValueEdit)).BeginInit();
            this.groupControlValueEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeViewCategories);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.splitContainerControl1.Panel2.Controls.Add(this.panelConfig);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 198;
            // 
            // treeViewCategories
            // 
            resources.ApplyResources(this.treeViewCategories, "treeViewCategories");
            this.treeViewCategories.Name = "treeViewCategories";
            this.treeViewCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCategories_AfterSelect);
            // 
            // panelConfig
            // 
            resources.ApplyResources(this.panelConfig, "panelConfig");
            this.panelConfig.Controls.Add(this.groupSettings);
            this.panelConfig.Name = "panelConfig";
            // 
            // groupSettings
            // 
            this.groupSettings.Controls.Add(this.panelEditHost);
            this.groupSettings.Controls.Add(this.InfoLabel);
            resources.ApplyResources(this.groupSettings, "groupSettings");
            this.groupSettings.Name = "groupSettings";
            // 
            // panelEditHost
            // 
            this.panelEditHost.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resources.ApplyResources(this.panelEditHost, "panelEditHost");
            this.panelEditHost.Name = "panelEditHost";
            // 
            // InfoLabel
            // 
            resources.ApplyResources(this.InfoLabel, "InfoLabel");
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.TabStop = true;
            this.InfoLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.InfoLabel_LinkClicked);
            // 
            // groupControlValueEdit
            // 
            this.groupControlValueEdit.Controls.Add(this.label1);
            this.groupControlValueEdit.Controls.Add(this.textBoxValue);
            resources.ApplyResources(this.groupControlValueEdit, "groupControlValueEdit");
            this.groupControlValueEdit.Name = "groupControlValueEdit";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxValue
            // 
            resources.ApplyResources(this.textBoxValue, "textBoxValue");
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged_1);
            // 
            // TagEditControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControlValueEdit);
            this.Controls.Add(this.splitContainerControl1);
            this.MinimumSize = new System.Drawing.Size(415, 220);
            this.Name = "TagEditControl";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelConfig)).EndInit();
            this.panelConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupSettings)).EndInit();
            this.groupSettings.ResumeLayout(false);
            this.groupSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEditHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlValueEdit)).EndInit();
            this.groupControlValueEdit.ResumeLayout(false);
            this.groupControlValueEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TreeView treeViewCategories;
        private DevExpress.XtraEditors.PanelControl panelConfig;
        private DevExpress.XtraEditors.GroupControl groupSettings;
        private DevExpress.XtraEditors.GroupControl groupControlValueEdit;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.LinkLabel InfoLabel;
        private DevExpress.XtraEditors.PanelControl panelEditHost;
        private System.Windows.Forms.Label label1;


    }
}
