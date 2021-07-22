namespace CompleX.Dialogs
{
    partial class InputDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDlg));
            this.textEdit = new DevExpress.XtraEditors.TextEdit();
            this.TextLabel = new System.Windows.Forms.Label();
            this.dlgOkBtn = new DevExpress.XtraEditors.SimpleButton();
            this.dlgCancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.comboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit
            // 
            resources.ApplyResources(this.textEdit, "textEdit");
            this.textEdit.Name = "textEdit";
            // 
            // TextLabel
            // 
            resources.ApplyResources(this.TextLabel, "TextLabel");
            this.TextLabel.ForeColor = System.Drawing.Color.Black;
            this.TextLabel.Name = "TextLabel";
            // 
            // dlgOkBtn
            // 
            resources.ApplyResources(this.dlgOkBtn, "dlgOkBtn");
            this.dlgOkBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.dlgOkBtn.Appearance.Options.UseFont = true;
            this.dlgOkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dlgOkBtn.LookAndFeel.SkinName = "Blue";
            this.dlgOkBtn.Name = "dlgOkBtn";
            // 
            // dlgCancelBtn
            // 
            resources.ApplyResources(this.dlgCancelBtn, "dlgCancelBtn");
            this.dlgCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dlgCancelBtn.LookAndFeel.SkinName = "Blue";
            this.dlgCancelBtn.Name = "dlgCancelBtn";
            // 
            // comboBoxEdit
            // 
            resources.ApplyResources(this.comboBoxEdit, "comboBoxEdit");
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEdit.Properties.Buttons"))))});
            this.comboBoxEdit.Properties.LookAndFeel.SkinName = "Blue";
            // 
            // InputDlg
            // 
            this.AcceptButton = this.dlgOkBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.dlgCancelBtn;
            this.Controls.Add(this.comboBoxEdit);
            this.Controls.Add(this.dlgCancelBtn);
            this.Controls.Add(this.dlgOkBtn);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.textEdit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputDlg";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit;
        private System.Windows.Forms.Label TextLabel;
        private DevExpress.XtraEditors.SimpleButton dlgOkBtn;
        private DevExpress.XtraEditors.SimpleButton dlgCancelBtn;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit;
    }
}