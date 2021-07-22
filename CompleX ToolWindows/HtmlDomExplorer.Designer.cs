namespace CompleX_ToolWindows
{
    partial class HtmlDomExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlDomExplorer));
            this.m_htmlTreeView = new System.Windows.Forms.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshDOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_htmlTreeView
            // 
            this.m_htmlTreeView.ContextMenuStrip = this.contextMenu;
            resources.ApplyResources(this.m_htmlTreeView, "m_htmlTreeView");
            this.m_htmlTreeView.Name = "m_htmlTreeView";
            this.m_htmlTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_htmlTreeView_BeforeSelect);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDOMToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            resources.ApplyResources(this.contextMenu, "contextMenu");
            // 
            // refreshDOMToolStripMenuItem
            // 
            this.refreshDOMToolStripMenuItem.Name = "refreshDOMToolStripMenuItem";
            resources.ApplyResources(this.refreshDOMToolStripMenuItem, "refreshDOMToolStripMenuItem");
            this.refreshDOMToolStripMenuItem.Click += new System.EventHandler(this.refreshDOMToolStripMenuItem_Click);
            // 
            // HtmlDomExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_htmlTreeView);
            this.Name = "HtmlDomExplorer";
            this.VisibleChanged += new System.EventHandler(this.HtmlDomExplorer_VisibleChanged);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_htmlTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshDOMToolStripMenuItem;
    }
}
