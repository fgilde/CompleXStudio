namespace CompleX.Dialogs
{
    partial class ExportProjectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportProjectDialog));
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.ContainerPanel = new DevExpress.XtraEditors.PanelControl();
            this.comboBoxEditFtp = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelConnection = new System.Windows.Forms.Label();
            this.checkBoxZip = new DevExpress.XtraEditors.CheckEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxProjectFile = new DevExpress.XtraEditors.CheckEdit();
            this.buttonEditDirectory = new DevExpress.XtraEditors.ButtonEdit();
            this.CancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.OkBtn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).BeginInit();
            this.ContainerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFtp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxProjectFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDirectory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.ContainerPanel);
            this.panelControl.Controls.Add(this.CancelBtn);
            this.panelControl.Controls.Add(this.OkBtn);
            resources.ApplyResources(this.panelControl, "panelControl");
            this.panelControl.Name = "panelControl";
            // 
            // ContainerPanel
            // 
            resources.ApplyResources(this.ContainerPanel, "ContainerPanel");
            this.ContainerPanel.Controls.Add(this.comboBoxEditFtp);
            this.ContainerPanel.Controls.Add(this.labelConnection);
            this.ContainerPanel.Controls.Add(this.checkBoxZip);
            this.ContainerPanel.Controls.Add(this.label1);
            this.ContainerPanel.Controls.Add(this.checkBoxProjectFile);
            this.ContainerPanel.Controls.Add(this.buttonEditDirectory);
            this.ContainerPanel.Name = "ContainerPanel";
            // 
            // comboBoxEditFtp
            // 
            resources.ApplyResources(this.comboBoxEditFtp, "comboBoxEditFtp");
            this.comboBoxEditFtp.Name = "comboBoxEditFtp";
            this.comboBoxEditFtp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditFtp.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.comboBoxEditFtp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditFtp.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.comboBoxEditFtp_ButtonClick);
            // 
            // labelConnection
            // 
            resources.ApplyResources(this.labelConnection, "labelConnection");
            this.labelConnection.Name = "labelConnection";
            // 
            // checkBoxZip
            // 
            resources.ApplyResources(this.checkBoxZip, "checkBoxZip");
            this.checkBoxZip.Name = "checkBoxZip";
            this.checkBoxZip.Properties.Caption = resources.GetString("checkBoxZip.Properties.Caption");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBoxProjectFile
            // 
            resources.ApplyResources(this.checkBoxProjectFile, "checkBoxProjectFile");
            this.checkBoxProjectFile.Name = "checkBoxProjectFile";
            this.checkBoxProjectFile.Properties.Caption = resources.GetString("checkBoxProjectFile.Properties.Caption");
            // 
            // buttonEditDirectory
            // 
            resources.ApplyResources(this.buttonEditDirectory, "buttonEditDirectory");
            this.buttonEditDirectory.Name = "buttonEditDirectory";
            this.buttonEditDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditDirectory.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ButtonEditDirectoryButtonClick);
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.LookAndFeel.SkinName = "Blue";
            this.CancelBtn.Name = "CancelBtn";
            // 
            // OkBtn
            // 
            resources.ApplyResources(this.OkBtn, "OkBtn");
            this.OkBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.OkBtn.Appearance.Options.UseFont = true;
            this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBtn.LookAndFeel.SkinName = "Blue";
            this.OkBtn.Name = "OkBtn";
            // 
            // ExportProjectDialog
            // 
            this.AcceptButton = this.OkBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportProjectDialog";
            this.ShowIcon = false;
            this.Shown += new System.EventHandler(this.ExportProjectDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContainerPanel)).EndInit();
            this.ContainerPanel.ResumeLayout(false);
            this.ContainerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditFtp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxProjectFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDirectory.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        protected DevExpress.XtraEditors.PanelControl ContainerPanel;
        public DevExpress.XtraEditors.SimpleButton CancelBtn;
        public DevExpress.XtraEditors.SimpleButton OkBtn;
        private DevExpress.XtraEditors.CheckEdit checkBoxZip;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.CheckEdit checkBoxProjectFile;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDirectory;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditFtp;
        private System.Windows.Forms.Label labelConnection;

    }
}