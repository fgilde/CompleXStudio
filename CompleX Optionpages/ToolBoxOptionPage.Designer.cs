namespace CompleX_Optionpages
{
    partial class ToolBoxOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBoxOptionPage));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.listBoxCategories = new System.Windows.Forms.ListBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.panelEditHost = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AccessibleDescription = null;
            this.groupControl1.AccessibleName = null;
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Controls.Add(this.listBoxCategories);
            this.groupControl1.Name = "groupControl1";
            // 
            // listBoxCategories
            // 
            this.listBoxCategories.AccessibleDescription = null;
            this.listBoxCategories.AccessibleName = null;
            resources.ApplyResources(this.listBoxCategories, "listBoxCategories");
            this.listBoxCategories.BackgroundImage = null;
            this.listBoxCategories.Font = null;
            this.listBoxCategories.FormattingEnabled = true;
            this.listBoxCategories.Name = "listBoxCategories";
            this.listBoxCategories.SelectedIndexChanged += new System.EventHandler(this.listBoxCategories_SelectedIndexChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.AccessibleDescription = null;
            this.groupControl2.AccessibleName = null;
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Controls.Add(this.listBoxItems);
            this.groupControl2.Name = "groupControl2";
            // 
            // listBoxItems
            // 
            this.listBoxItems.AccessibleDescription = null;
            this.listBoxItems.AccessibleName = null;
            resources.ApplyResources(this.listBoxItems, "listBoxItems");
            this.listBoxItems.BackgroundImage = null;
            this.listBoxItems.DisplayMember = "Caption";
            this.listBoxItems.Font = null;
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.SelectedIndexChanged += new System.EventHandler(this.listBoxItems_SelectedIndexChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.AccessibleDescription = null;
            this.groupControl3.AccessibleName = null;
            resources.ApplyResources(this.groupControl3, "groupControl3");
            this.groupControl3.Controls.Add(this.panelEditHost);
            this.groupControl3.Name = "groupControl3";
            // 
            // panelEditHost
            // 
            this.panelEditHost.AccessibleDescription = null;
            this.panelEditHost.AccessibleName = null;
            resources.ApplyResources(this.panelEditHost, "panelEditHost");
            this.panelEditHost.BackgroundImage = null;
            this.panelEditHost.Font = null;
            this.panelEditHost.Name = "panelEditHost";
            // 
            // ToolBoxOptionPage
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "ToolBoxOptionPage";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ListBox listBoxCategories;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.ListBox listBoxItems;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private System.Windows.Forms.Panel panelEditHost;


    }
}
