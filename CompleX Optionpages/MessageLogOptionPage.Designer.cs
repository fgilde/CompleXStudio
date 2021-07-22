namespace CompleX_Optionpages
{
    partial class MessageLogOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageLogOptionPage));
            this.checkBoxActivateToolWindow = new System.Windows.Forms.CheckBox();
            this.checkBoxAlert = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveLog = new System.Windows.Forms.CheckBox();
            this.checkBoxToday = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxActivateToolWindow
            // 
            this.checkBoxActivateToolWindow.AccessibleDescription = null;
            this.checkBoxActivateToolWindow.AccessibleName = null;
            resources.ApplyResources(this.checkBoxActivateToolWindow, "checkBoxActivateToolWindow");
            this.checkBoxActivateToolWindow.BackgroundImage = null;
            this.checkBoxActivateToolWindow.Font = null;
            this.checkBoxActivateToolWindow.Name = "checkBoxActivateToolWindow";
            this.checkBoxActivateToolWindow.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlert
            // 
            this.checkBoxAlert.AccessibleDescription = null;
            this.checkBoxAlert.AccessibleName = null;
            resources.ApplyResources(this.checkBoxAlert, "checkBoxAlert");
            this.checkBoxAlert.BackgroundImage = null;
            this.checkBoxAlert.Font = null;
            this.checkBoxAlert.Name = "checkBoxAlert";
            this.checkBoxAlert.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveLog
            // 
            this.checkBoxSaveLog.AccessibleDescription = null;
            this.checkBoxSaveLog.AccessibleName = null;
            resources.ApplyResources(this.checkBoxSaveLog, "checkBoxSaveLog");
            this.checkBoxSaveLog.BackgroundImage = null;
            this.checkBoxSaveLog.Font = null;
            this.checkBoxSaveLog.Name = "checkBoxSaveLog";
            this.checkBoxSaveLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxToday
            // 
            this.checkBoxToday.AccessibleDescription = null;
            this.checkBoxToday.AccessibleName = null;
            resources.ApplyResources(this.checkBoxToday, "checkBoxToday");
            this.checkBoxToday.BackgroundImage = null;
            this.checkBoxToday.Font = null;
            this.checkBoxToday.Name = "checkBoxToday";
            this.checkBoxToday.UseVisualStyleBackColor = true;
            // 
            // MessageLogOptionPage
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.checkBoxToday);
            this.Controls.Add(this.checkBoxSaveLog);
            this.Controls.Add(this.checkBoxAlert);
            this.Controls.Add(this.checkBoxActivateToolWindow);
            this.Name = "MessageLogOptionPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxActivateToolWindow;
        private System.Windows.Forms.CheckBox checkBoxAlert;
        private System.Windows.Forms.CheckBox checkBoxSaveLog;
        private System.Windows.Forms.CheckBox checkBoxToday;
    }
}
