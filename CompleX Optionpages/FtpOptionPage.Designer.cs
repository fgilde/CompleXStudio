namespace CompleX_Optionpages
{
    partial class FtpOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpOptionPage));
            this.listBoxFtpCollection = new DevExpress.XtraEditors.ListBoxControl();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.Server = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textEditServer = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.textEditUser = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.textEditPassword = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.textEditDefaultDir = new DevExpress.XtraEditors.TextEdit();
            this.checkBoxEnableSsl = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textEditPort = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonExport = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSearch = new DevExpress.XtraEditors.SimpleButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFtpCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDefaultDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxFtpCollection
            // 
            resources.ApplyResources(this.listBoxFtpCollection, "listBoxFtpCollection");
            this.listBoxFtpCollection.ContextMenuStrip = this.contextMenuStrip;
            this.listBoxFtpCollection.Name = "listBoxFtpCollection";
            this.listBoxFtpCollection.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFtpCollectionMouseDoubleClick);
            this.listBoxFtpCollection.SelectedIndexChanged += new System.EventHandler(this.ListBoxFtpCollectionSelectedIndexChanged);
            // 
            // textEditName
            // 
            resources.ApplyResources(this.textEditName, "textEditName");
            this.textEditName.Name = "textEditName";
            // 
            // Server
            // 
            resources.ApplyResources(this.Server, "Server");
            this.Server.Name = "Server";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textEditServer
            // 
            resources.ApplyResources(this.textEditServer, "textEditServer");
            this.textEditServer.Name = "textEditServer";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textEditUser
            // 
            resources.ApplyResources(this.textEditUser, "textEditUser");
            this.textEditUser.Name = "textEditUser";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textEditPassword
            // 
            resources.ApplyResources(this.textEditPassword, "textEditPassword");
            this.textEditPassword.Name = "textEditPassword";
            this.textEditPassword.Leave += new System.EventHandler(this.TextEditPasswordLeave);
            this.textEditPassword.Enter += new System.EventHandler(this.TextEditPasswordEnter);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textEditDefaultDir
            // 
            resources.ApplyResources(this.textEditDefaultDir, "textEditDefaultDir");
            this.textEditDefaultDir.Name = "textEditDefaultDir";
            // 
            // checkBoxEnableSsl
            // 
            resources.ApplyResources(this.checkBoxEnableSsl, "checkBoxEnableSsl");
            this.checkBoxEnableSsl.Name = "checkBoxEnableSsl";
            this.checkBoxEnableSsl.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textEditPort
            // 
            resources.ApplyResources(this.textEditPort, "textEditPort");
            this.textEditPort.Name = "textEditPort";
            // 
            // simpleButtonRefresh
            // 
            resources.ApplyResources(this.simpleButtonRefresh, "simpleButtonRefresh");
            this.simpleButtonRefresh.Image = global::CompleX_Optionpages.Images.aktualisieren_16;
            this.simpleButtonRefresh.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonRefresh.Name = "simpleButtonRefresh";
            this.simpleButtonRefresh.Click += new System.EventHandler(this.SimpleButtonRefreshClick);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.ErrorImage = global::CompleX_Optionpages.Images.löschen_16;
            this.pictureBox1.InitialImage = global::CompleX_Optionpages.Images.bestaetigen_16;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Image = global::CompleX_Optionpages.Images.minus16;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDeleteClick);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Image = global::CompleX_Optionpages.Images.plus16;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // simpleButtonExport
            // 
            resources.ApplyResources(this.simpleButtonExport, "simpleButtonExport");
            this.simpleButtonExport.Image = global::CompleX_Optionpages.Images.speichern_16;
            this.simpleButtonExport.Name = "simpleButtonExport";
            this.simpleButtonExport.Click += new System.EventHandler(this.SimpleButtonExportClick);
            // 
            // simpleButtonSearch
            // 
            resources.ApplyResources(this.simpleButtonSearch, "simpleButtonSearch");
            this.simpleButtonSearch.Name = "simpleButtonSearch";
            this.simpleButtonSearch.Click += new System.EventHandler(this.SimpleButtonSearchClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripOpening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // FtpOptionPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleButtonSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textEditPort);
            this.Controls.Add(this.simpleButtonExport);
            this.Controls.Add(this.simpleButtonRefresh);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxEnableSsl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textEditDefaultDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textEditPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textEditUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textEditServer);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textEditName);
            this.Controls.Add(this.listBoxFtpCollection);
            this.MinimumSize = new System.Drawing.Size(247, 285);
            this.Name = "FtpOptionPage";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFtpCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDefaultDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listBoxFtpCollection;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEditServer;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit textEditUser;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit textEditPassword;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit textEditDefaultDir;
        private System.Windows.Forms.CheckBox checkBoxEnableSsl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefresh;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExport;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.TextEdit textEditPort;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    }
}
