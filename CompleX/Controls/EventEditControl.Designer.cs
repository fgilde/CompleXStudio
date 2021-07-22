namespace CompleX.Controls
{
    partial class EventEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventEditControl));
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.insertFunctionBtn = new DevExpress.XtraEditors.SimpleButton();
            this.checkOneLine = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.checkOneLine.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.AccessibleDescription = null;
            this.textBox.AccessibleName = null;
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.BackgroundImage = null;
            this.textBox.Font = null;
            this.textBox.Name = "textBox";
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelName
            // 
            this.labelName.AccessibleDescription = null;
            this.labelName.AccessibleName = null;
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // insertFunctionBtn
            // 
            this.insertFunctionBtn.AccessibleDescription = null;
            this.insertFunctionBtn.AccessibleName = null;
            resources.ApplyResources(this.insertFunctionBtn, "insertFunctionBtn");
            this.insertFunctionBtn.BackgroundImage = null;
            this.insertFunctionBtn.Name = "insertFunctionBtn";
            this.insertFunctionBtn.Click += new System.EventHandler(this.insertFunctionBtn_Click);
            // 
            // checkOneLine
            // 
            resources.ApplyResources(this.checkOneLine, "checkOneLine");
            this.checkOneLine.BackgroundImage = null;
            this.checkOneLine.Name = "checkOneLine";
            this.checkOneLine.Properties.AccessibleDescription = null;
            this.checkOneLine.Properties.AccessibleName = null;
            this.checkOneLine.Properties.AutoHeight = ((bool)(resources.GetObject("checkOneLine.Properties.AutoHeight")));
            this.checkOneLine.Properties.Caption = resources.GetString("checkOneLine.Properties.Caption");
            this.checkOneLine.CheckedChanged += new System.EventHandler(this.checkOneLine_CheckedChanged);
            // 
            // EventEditControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.checkOneLine);
            this.Controls.Add(this.insertFunctionBtn);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBox);
            this.Font = null;
            this.Name = "EventEditControl";
            ((System.ComponentModel.ISupportInitialize)(this.checkOneLine.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labelName;
        private DevExpress.XtraEditors.SimpleButton insertFunctionBtn;
        private DevExpress.XtraEditors.CheckEdit checkOneLine;
    }
}
