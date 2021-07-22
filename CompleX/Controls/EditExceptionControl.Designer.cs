namespace CompleX.Controls
{
    partial class EditExceptionControl
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
            this.textLabel = new System.Windows.Forms.Label();
            this.ErrorImage = new System.Windows.Forms.PictureBox();
            this.contentEdit = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contentEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textLabel.Location = new System.Drawing.Point(60, 12);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(338, 61);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "Fehler: Für die angegebene Datei konnte kein Editormodul geladen werden\r\n\r\n";
            // 
            // ErrorImage
            // 
            this.ErrorImage.ErrorImage = global::CompleX.Properties.Resources.Warning;
            this.ErrorImage.Image = global::CompleX.Properties.Resources.error;
            this.ErrorImage.Location = new System.Drawing.Point(7, 9);
            this.ErrorImage.Name = "ErrorImage";
            this.ErrorImage.Size = new System.Drawing.Size(50, 48);
            this.ErrorImage.TabIndex = 1;
            this.ErrorImage.TabStop = false;
            // 
            // contentEdit
            // 
            this.contentEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentEdit.Location = new System.Drawing.Point(9, 81);
            this.contentEdit.Name = "contentEdit";
            this.contentEdit.Size = new System.Drawing.Size(388, 118);
            this.contentEdit.TabIndex = 2;
            this.contentEdit.Visible = false;
            // 
            // EditExceptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.contentEdit);
            this.Controls.Add(this.ErrorImage);
            this.Controls.Add(this.textLabel);
            this.MinimumSize = new System.Drawing.Size(260, 87);
            this.Name = "EditExceptionControl";
            this.Size = new System.Drawing.Size(406, 213);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contentEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.MemoEdit contentEdit;
        public System.Windows.Forms.Label textLabel;
        public System.Windows.Forms.PictureBox ErrorImage;
    }
}
