namespace CompleX.Controls
{
    partial class SearchReplaceControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchReplaceControl));
            this.regexTextBox = new CompleX.Controls.RegexTextBox();
            this.searchOptionsControl = new CompleX.Controls.SearchOptionsControl();
            this.comboBoxSearchLocation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSearchLocation.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // regexTextBox
            // 
            resources.ApplyResources(this.regexTextBox, "regexTextBox");
            this.regexTextBox.Name = "regexTextBox";
            this.regexTextBox.Regex = ((System.Text.RegularExpressions.Regex)(resources.GetObject("regexTextBox.Regex")));
            // 
            // searchOptionsControl
            // 
            resources.ApplyResources(this.searchOptionsControl, "searchOptionsControl");
            this.searchOptionsControl.IsExpanded = false;
            this.searchOptionsControl.Name = "searchOptionsControl";
            this.searchOptionsControl.SearchOptions = GrepWrap.SearchOptions.Defaults;
            this.searchOptionsControl.SearchOptionsChanged += new System.EventHandler(this.SearchOptionsControlSearchOptionsChanged);
            // 
            // comboBoxSearchLocation
            // 
            resources.ApplyResources(this.comboBoxSearchLocation, "comboBoxSearchLocation");
            this.comboBoxSearchLocation.Name = "comboBoxSearchLocation";
            this.comboBoxSearchLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxSearchLocation.Properties.Buttons"))))});
            this.comboBoxSearchLocation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxSearchLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchLocation_SelectedIndexChanged);
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
            // SearchReplaceControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSearchLocation);
            this.Controls.Add(this.regexTextBox);
            this.Controls.Add(this.searchOptionsControl);
            this.Name = "SearchReplaceControl";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSearchLocation.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SearchOptionsControl searchOptionsControl;
        private RegexTextBox regexTextBox;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxSearchLocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
