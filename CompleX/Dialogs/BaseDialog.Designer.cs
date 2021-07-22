namespace CompleX.Dialogs
{
    partial class BaseDialog : IBaseDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseDialog));
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.NoBtn = new DevExpress.XtraEditors.SimpleButton();
            this.CheckBox = new System.Windows.Forms.CheckBox();
            this.ContainerPanel = new DevExpress.XtraEditors.PanelControl();
            this.CancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.OkBtn = new DevExpress.XtraEditors.SimpleButton();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.NoBtn);
            this.panelControl.Controls.Add(this.CheckBox);
            this.panelControl.Controls.Add(this.ContainerPanel);
            this.panelControl.Controls.Add(this.CancelBtn);
            this.panelControl.Controls.Add(this.OkBtn);
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.Name = "panelControl";
            // 
            // NoBtn
            // 
            resources.ApplyResources(this.NoBtn, "NoBtn");
            this.NoBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoBtn.LookAndFeel.SkinName = "Blue";
            this.NoBtn.Name = "NoBtn";
            this.NoBtn.VisibleChanged += new System.EventHandler(this.NoBtnVisibleChanged);
            this.NoBtn.Click += new System.EventHandler(this.NoBtnClick);
            // 
            // CheckBox
            // 
            resources.ApplyResources(this.CheckBox, "CheckBox");
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.UseVisualStyleBackColor = true;
            // 
            // ContainerPanel
            // 
            resources.ApplyResources(this.ContainerPanel, "ContainerPanel");
            this.ContainerPanel.Name = "ContainerPanel";
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.LookAndFeel.SkinName = "Blue";
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Click += new System.EventHandler(this.DlgCancelBtnClick);
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.OkBtn.Appearance.Options.UseFont = true;
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.LookAndFeel.SkinName = "Blue";
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Click += new System.EventHandler(this.DlgOkBtnClick);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            resources.ApplyResources(this.ErrorProvider, "ErrorProvider");
            // 
            // BaseDialog
            // 
            this.AcceptButton = this.OkBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.Controls.Add(this.panelControl);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "BaseDialog";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseDialogFormClosing);
            this.Shown += new System.EventHandler(this.BaseDialogShown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        protected DevExpress.XtraEditors.PanelControl ContainerPanel;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
        public DevExpress.XtraEditors.SimpleButton CancelBtn;
        public DevExpress.XtraEditors.SimpleButton OkBtn;
        public DevExpress.XtraEditors.SimpleButton NoBtn;
        private System.Windows.Forms.CheckBox CheckBox;

    }
}