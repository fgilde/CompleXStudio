namespace CompleX.Controls
{
    partial class FileListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileListControl));
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.panelHost = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new CompleX.Controls.SearchTextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "datenebenen_neu_16.png");
            // 
            // panelHost
            // 
            this.panelHost.AccessibleDescription = null;
            this.panelHost.AccessibleName = null;
            resources.ApplyResources(this.panelHost, "panelHost");
            this.panelHost.BackgroundImage = null;
            this.panelHost.Font = null;
            this.panelHost.Name = "panelHost";
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.textBoxSearch);
            this.panel1.Controls.Add(this.labelSearch);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.AccessibleDescription = null;
            this.textBoxSearch.AccessibleName = null;
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.BackgroundImage = null;
            this.textBoxSearch.Font = null;
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.TextBoxSearchTextChanged);
            // 
            // labelSearch
            // 
            this.labelSearch.AccessibleDescription = null;
            this.labelSearch.AccessibleName = null;
            resources.ApplyResources(this.labelSearch, "labelSearch");
            this.labelSearch.Font = null;
            this.labelSearch.Name = "labelSearch";
            // 
            // FileListControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHost);
            this.Font = null;
            this.Name = "FileListControl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgMain;
        private System.Windows.Forms.Panel panelHost;
        private System.Windows.Forms.Panel panel1;
        private CompleX.Controls.SearchTextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
    }
}
