namespace CompleX.Controls
{
    partial class DefaultEditControl
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
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxContent
            // 
            this.textBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxContent.Location = new System.Drawing.Point(0, 0);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxContent.Size = new System.Drawing.Size(273, 240);
            this.textBoxContent.TabIndex = 1;
            this.textBoxContent.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.textBoxContent.Click += new System.EventHandler(this.richTextBox1_Click);
            this.textBoxContent.CursorChanged += new System.EventHandler(this.textBoxContent_CursorChanged);
            this.textBoxContent.Enter += new System.EventHandler(this.richTextBox1_Enter);
            // 
            // DefaultEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxContent);
            this.Name = "DefaultEditControl";
            this.Size = new System.Drawing.Size(273, 240);
            this.Enter += new System.EventHandler(this.BaseEditControl_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxContent;
    }
}
