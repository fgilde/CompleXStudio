namespace CompleX.Controls
{
    partial class ServiceInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceInfo));
            this.labelName = new System.Windows.Forms.Label();
            this.labelSupported = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.labelGetVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AccessibleDescription = null;
            this.labelName.AccessibleName = null;
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelSupported
            // 
            this.labelSupported.AccessibleDescription = null;
            this.labelSupported.AccessibleName = null;
            resources.ApplyResources(this.labelSupported, "labelSupported");
            this.labelSupported.Font = null;
            this.labelSupported.Name = "labelSupported";
            // 
            // labelId
            // 
            this.labelId.AccessibleDescription = null;
            this.labelId.AccessibleName = null;
            resources.ApplyResources(this.labelId, "labelId");
            this.labelId.Font = null;
            this.labelId.Name = "labelId";
            // 
            // labelGetVersion
            // 
            this.labelGetVersion.AccessibleDescription = null;
            this.labelGetVersion.AccessibleName = null;
            resources.ApplyResources(this.labelGetVersion, "labelGetVersion");
            this.labelGetVersion.Font = null;
            this.labelGetVersion.Name = "labelGetVersion";
            // 
            // ServiceInfo
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.labelGetVersion);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.labelSupported);
            this.Controls.Add(this.labelName);
            this.Name = "ServiceInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSupported;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelGetVersion;
    }
}
