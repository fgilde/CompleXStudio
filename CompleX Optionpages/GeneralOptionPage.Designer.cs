namespace CompleX_Optionpages
{
    partial class GeneralOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionPage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEditLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAutoLanguage = new System.Windows.Forms.CheckBox();
            this.groupBoxRecentFiles = new System.Windows.Forms.GroupBox();
            this.labelCheckRecent = new System.Windows.Forms.Label();
            this.checkBoxOpenLastRecentFiles = new System.Windows.Forms.CheckBox();
            this.spinEditItemCountRecent = new DevExpress.XtraEditors.SpinEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteDsa = new DevExpress.XtraEditors.SimpleButton();
            this.checkBoxShowFileIconOnTabPage = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepMainMenuOnFullScreen = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLanguage.Properties)).BeginInit();
            this.groupBoxRecentFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditItemCountRecent.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxEditLanguage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxAutoLanguage);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Name = "label2";
            // 
            // comboBoxEditLanguage
            // 
            resources.ApplyResources(this.comboBoxEditLanguage, "comboBoxEditLanguage");
            this.comboBoxEditLanguage.Name = "comboBoxEditLanguage";
            this.comboBoxEditLanguage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditLanguage.Properties.Buttons"))))});
            this.comboBoxEditLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxEditLanguage_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBoxAutoLanguage
            // 
            resources.ApplyResources(this.checkBoxAutoLanguage, "checkBoxAutoLanguage");
            this.checkBoxAutoLanguage.Name = "checkBoxAutoLanguage";
            this.checkBoxAutoLanguage.UseVisualStyleBackColor = true;
            this.checkBoxAutoLanguage.CheckedChanged += new System.EventHandler(this.checkBoxAutoLanguage_CheckedChanged);
            // 
            // groupBoxRecentFiles
            // 
            resources.ApplyResources(this.groupBoxRecentFiles, "groupBoxRecentFiles");
            this.groupBoxRecentFiles.Controls.Add(this.labelCheckRecent);
            this.groupBoxRecentFiles.Controls.Add(this.checkBoxOpenLastRecentFiles);
            this.groupBoxRecentFiles.Controls.Add(this.spinEditItemCountRecent);
            this.groupBoxRecentFiles.Controls.Add(this.label3);
            this.groupBoxRecentFiles.Name = "groupBoxRecentFiles";
            this.groupBoxRecentFiles.TabStop = false;
            // 
            // labelCheckRecent
            // 
            resources.ApplyResources(this.labelCheckRecent, "labelCheckRecent");
            this.labelCheckRecent.Name = "labelCheckRecent";
            // 
            // checkBoxOpenLastRecentFiles
            // 
            resources.ApplyResources(this.checkBoxOpenLastRecentFiles, "checkBoxOpenLastRecentFiles");
            this.checkBoxOpenLastRecentFiles.Name = "checkBoxOpenLastRecentFiles";
            this.checkBoxOpenLastRecentFiles.UseVisualStyleBackColor = true;
            // 
            // spinEditItemCountRecent
            // 
            resources.ApplyResources(this.spinEditItemCountRecent, "spinEditItemCountRecent");
            this.spinEditItemCountRecent.Name = "spinEditItemCountRecent";
            this.spinEditItemCountRecent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditItemCountRecent.Properties.IsFloatValue = false;
            this.spinEditItemCountRecent.Properties.Mask.EditMask = resources.GetString("spinEditItemCountRecent.Properties.Mask.EditMask");
            this.spinEditItemCountRecent.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEditItemCountRecent.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEditItemCountRecent.EditValueChanged += new System.EventHandler(this.spinEditItemCountRecent_EditValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.buttonDeleteDsa);
            this.groupBox2.Controls.Add(this.checkBoxShowFileIconOnTabPage);
            this.groupBox2.Controls.Add(this.checkBoxKeepMainMenuOnFullScreen);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // buttonDeleteDsa
            // 
            resources.ApplyResources(this.buttonDeleteDsa, "buttonDeleteDsa");
            this.buttonDeleteDsa.Name = "buttonDeleteDsa";
            this.buttonDeleteDsa.Click += new System.EventHandler(this.ButtonDeleteDsaClick);
            // 
            // checkBoxShowFileIconOnTabPage
            // 
            resources.ApplyResources(this.checkBoxShowFileIconOnTabPage, "checkBoxShowFileIconOnTabPage");
            this.checkBoxShowFileIconOnTabPage.Name = "checkBoxShowFileIconOnTabPage";
            this.checkBoxShowFileIconOnTabPage.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeepMainMenuOnFullScreen
            // 
            resources.ApplyResources(this.checkBoxKeepMainMenuOnFullScreen, "checkBoxKeepMainMenuOnFullScreen");
            this.checkBoxKeepMainMenuOnFullScreen.Name = "checkBoxKeepMainMenuOnFullScreen";
            this.checkBoxKeepMainMenuOnFullScreen.UseVisualStyleBackColor = true;
            // 
            // GeneralOptionPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxRecentFiles);
            this.Controls.Add(this.groupBox1);
            this.Name = "GeneralOptionPage";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditLanguage.Properties)).EndInit();
            this.groupBoxRecentFiles.ResumeLayout(false);
            this.groupBoxRecentFiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditItemCountRecent.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAutoLanguage;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxRecentFiles;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SpinEdit spinEditItemCountRecent;
        private System.Windows.Forms.CheckBox checkBoxOpenLastRecentFiles;
        private System.Windows.Forms.Label labelCheckRecent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxKeepMainMenuOnFullScreen;
        private System.Windows.Forms.CheckBox checkBoxShowFileIconOnTabPage;
        private DevExpress.XtraEditors.SimpleButton buttonDeleteDsa;

    }
}
