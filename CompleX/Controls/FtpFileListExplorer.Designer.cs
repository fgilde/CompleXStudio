namespace CompleX.Controls
{
    partial class FtpFileListExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpFileListExplorer));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ftpExplorer1 = new CompleX.Controls.FtpExplorer();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnDate = new System.Windows.Forms.ColumnHeader();
            this.columnType = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSplitter1 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.textEditFileName = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEditMask = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMask.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ftpExplorer1);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.listViewFiles);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 187;
            // 
            // ftpExplorer1
            // 
            this.ftpExplorer1.DirectoriesOnly = true;
            resources.ApplyResources(this.ftpExplorer1, "ftpExplorer1");
            this.ftpExplorer1.FtpConnection = null;
            this.ftpExplorer1.MultiSelect = false;
            this.ftpExplorer1.Name = "ftpExplorer1";
            this.ftpExplorer1.SelectedPath = "";
            this.ftpExplorer1.ShowToolBar = true;
            this.ftpExplorer1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FtpExplorer1AfterSelect);
            this.ftpExplorer1.FolderCreated += new CompleX.Controls.FtpExplorer.FolderCreatedHandler(this.FtpExplorer1FolderCreated);
            this.ftpExplorer1.ConnectionChanged += new CompleX.Controls.FtpExplorer.ConnectionChangedHandler(this.FtpExplorer1ConnectionChanged);
            this.ftpExplorer1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.FtpExplorer1AfterExpand);
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowDrop = true;
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDate,
            this.columnType});
            this.listViewFiles.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.listViewFiles, "listViewFiles");
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.LabelEdit = true;
            this.listViewFiles.LargeImageList = this.imageList;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.SmallImageList = this.imageList;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewFilesMouseDoubleClick);
            this.listViewFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListViewFilesAfterLabelEdit);
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.ListViewFilesSelectedIndexChanged);
            this.listViewFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewFilesDragDrop);
            this.listViewFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewFilesDragEnter);
            this.listViewFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewFilesKeyUp);
            this.listViewFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListViewFilesItemDrag);
            // 
            // columnName
            // 
            resources.ApplyResources(this.columnName, "columnName");
            // 
            // columnDate
            // 
            resources.ApplyResources(this.columnDate, "columnDate");
            // 
            // columnType
            // 
            resources.ApplyResources(this.columnType, "columnType");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.menuSplitter1,
            this.downloadToolStripMenuItem,
            this.toolStripMenuItem1,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItemClick);
            // 
            // menuSplitter1
            // 
            this.menuSplitter1.Name = "menuSplitter1";
            resources.ApplyResources(this.menuSplitter1, "menuSplitter1");
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Image = global::CompleX.Properties.Resources.importieren1;
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            resources.ApplyResources(this.downloadToolStripMenuItem, "downloadToolStripMenuItem");
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItemClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::CompleX.Properties.Resources.löschen_16;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "file.png");
            this.imageList.Images.SetKeyName(1, "Folder.png");
            // 
            // textEditFileName
            // 
            resources.ApplyResources(this.textEditFileName, "textEditFileName");
            this.textEditFileName.Name = "textEditFileName";
            // 
            // comboBoxEditMask
            // 
            resources.ApplyResources(this.comboBoxEditMask, "comboBoxEditMask");
            this.comboBoxEditMask.Name = "comboBoxEditMask";
            this.comboBoxEditMask.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditMask.Properties.Buttons"))))});
            this.comboBoxEditMask.EditValueChanged += new System.EventHandler(this.ComboBoxEditMaskEditValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // FtpFileListExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textEditFileName);
            this.Controls.Add(this.comboBoxEditMask);
            this.MinimumSize = new System.Drawing.Size(460, 234);
            this.Name = "FtpFileListExplorer";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditMask.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private FtpExplorer ftpExplorer1;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator menuSplitter1;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraEditors.TextEdit textEditFileName;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
