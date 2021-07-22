namespace CompleX.Controls
{
    partial class ActionListControl
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
            this.actionlistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // actionlistBox
            // 
            this.actionlistBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionlistBox.FormattingEnabled = true;
            this.actionlistBox.Location = new System.Drawing.Point(0, 0);
            this.actionlistBox.Name = "actionlistBox";
            this.actionlistBox.Size = new System.Drawing.Size(180, 290);
            this.actionlistBox.TabIndex = 0;
            this.actionlistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.actionlistBox_MouseDoubleClick);
            // 
            // ActionListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.actionlistBox);
            this.Name = "ActionListControl";
            this.Size = new System.Drawing.Size(180, 290);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox actionlistBox;

    }
}
