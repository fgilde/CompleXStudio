namespace CompleX_Optionpages
{
    partial class ThemeOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeOptionPage));
            this.labelLookAndFeel = new System.Windows.Forms.Label();
            this.listBoxThemes = new System.Windows.Forms.ListBox();
            this.EnableFormSkin = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelLookAndFeel
            // 
            resources.ApplyResources(this.labelLookAndFeel, "labelLookAndFeel");
            this.labelLookAndFeel.Name = "labelLookAndFeel";
            // 
            // listBoxThemes
            // 
            resources.ApplyResources(this.listBoxThemes, "listBoxThemes");
            this.listBoxThemes.FormattingEnabled = true;
            this.listBoxThemes.Name = "listBoxThemes";
            this.listBoxThemes.SelectedIndexChanged += new System.EventHandler(this.listBoxThemes_SelectedIndexChanged);
            // 
            // EnableFormSkin
            // 
            resources.ApplyResources(this.EnableFormSkin, "EnableFormSkin");
            this.EnableFormSkin.Name = "EnableFormSkin";
            this.EnableFormSkin.UseVisualStyleBackColor = true;
            // 
            // ThemeOptionPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EnableFormSkin);
            this.Controls.Add(this.listBoxThemes);
            this.Controls.Add(this.labelLookAndFeel);
            this.Name = "ThemeOptionPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLookAndFeel;
        private System.Windows.Forms.ListBox listBoxThemes;
        private System.Windows.Forms.CheckBox EnableFormSkin;
    }
}
