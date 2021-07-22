namespace Complex_Designers
{
    partial class HTMLDesigner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HTMLDesigner));
            this.HtmlEditor = new System.Windows.Forms.WebBrowser();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.urlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popupContainerEditTag = new DevExpress.XtraEditors.PopupContainerControl();
            this.popupEditTag = new DevExpress.XtraEditors.PopupContainerEdit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditTag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupEditTag.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // HtmlEditor
            // 
            resources.ApplyResources(this.HtmlEditor, "HtmlEditor");
            this.HtmlEditor.IsWebBrowserContextMenuEnabled = false;
            this.HtmlEditor.MinimumSize = new System.Drawing.Size(15, 16);
            this.HtmlEditor.Name = "HtmlEditor";
            this.HtmlEditor.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.insertToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.editTagToolStripMenuItem,
            this.toolStripMenuItem2,
            this.fontToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // editTagToolStripMenuItem
            // 
            this.editTagToolStripMenuItem.Name = "editTagToolStripMenuItem";
            resources.ApplyResources(this.editTagToolStripMenuItem, "editTagToolStripMenuItem");
            this.editTagToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedTagClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            resources.ApplyResources(this.fontToolStripMenuItem, "fontToolStripMenuItem");
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.urlToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            resources.ApplyResources(this.insertToolStripMenuItem, "insertToolStripMenuItem");
            // 
            // urlToolStripMenuItem
            // 
            this.urlToolStripMenuItem.Name = "urlToolStripMenuItem";
            resources.ApplyResources(this.urlToolStripMenuItem, "urlToolStripMenuItem");
            this.urlToolStripMenuItem.Click += new System.EventHandler(this.urlToolStripMenuItem_Click);
            // 
            // popupContainerEditTag
            // 
            resources.ApplyResources(this.popupContainerEditTag, "popupContainerEditTag");
            this.popupContainerEditTag.Name = "popupContainerEditTag";
            // 
            // popupEditTag
            // 
            resources.ApplyResources(this.popupEditTag, "popupEditTag");
            this.popupEditTag.Name = "popupEditTag";
            this.popupEditTag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popupEditTag.Properties.Buttons"))))});
            this.popupEditTag.Properties.CloseOnLostFocus = false;
            this.popupEditTag.Properties.CloseOnOuterMouseClick = false;
            this.popupEditTag.Properties.PopupControl = this.popupContainerEditTag;
            this.popupEditTag.Properties.Click += new System.EventHandler(this.popupOptions_Properties_Click);
            this.popupEditTag.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.popupEditTag_Closed);
            // 
            // HTMLDesigner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerEditTag);
            this.Controls.Add(this.HtmlEditor);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.popupEditTag);
            this.Name = "HTMLDesigner";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditTag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupEditTag.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser HtmlEditor;
        internal System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem urlToolStripMenuItem;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerEditTag;
        private DevExpress.XtraEditors.PopupContainerEdit popupEditTag;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;

  
    }
}