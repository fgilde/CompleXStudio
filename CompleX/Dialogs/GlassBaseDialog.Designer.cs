namespace CompleX.Dialogs
{
    partial class GlassBaseDialog : IBaseDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlassBaseDialog));
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.NoBtn = new DevExpress.XtraEditors.SimpleButton();
            this.CheckBox = new System.Windows.Forms.CheckBox();
            this.CancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.OkBtn = new DevExpress.XtraEditors.SimpleButton();
            this.ContainerPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.ErrorProvider, "ErrorProvider");
            // 
            // NoBtn
            // 
            resources.ApplyResources(this.NoBtn, "NoBtn");
            this.NoBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoBtn.LookAndFeel.SkinName = "Blue";
            this.NoBtn.Name = "NoBtn";
            this.NoBtn.VisibleChanged += new System.EventHandler(this.NoBtn_VisibleChanged);
            this.NoBtn.Click += new System.EventHandler(this.NoBtn_Click);
            // 
            // CheckBox
            // 
            resources.ApplyResources(this.CheckBox, "CheckBox");
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.LookAndFeel.SkinName = "Blue";
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Click += new System.EventHandler(this.dlgCancelBtn_Click);
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.OkBtn.Appearance.Options.UseFont = true;
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.LookAndFeel.SkinName = "Blue";
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Click += new System.EventHandler(this.dlgOkBtn_Click);
            // 
            // ContainerPanel
            // 
            resources.ApplyResources(this.ContainerPanel, "ContainerPanel");
            this.ContainerPanel.Name = "ContainerPanel";
            // 
            // GlassBaseDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ContainerPanel);
            this.Controls.Add(this.NoBtn);
            this.Controls.Add(this.CheckBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "GlassBaseDialog";
            this.ShowIcon = false;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseDialog_FormClosing);
            this.Shown += new System.EventHandler(this.BaseDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected DevExpress.XtraEditors.PanelControl ContainerPanel;
        public DevExpress.XtraEditors.SimpleButton NoBtn;
        private System.Windows.Forms.CheckBox CheckBox;
        public DevExpress.XtraEditors.SimpleButton CancelBtn;
        public DevExpress.XtraEditors.SimpleButton OkBtn;

    }
}