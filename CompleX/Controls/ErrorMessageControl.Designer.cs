namespace CompleX.Controls
{
    partial class ErrorMessageControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageControl));
            this.errorImage = new System.Windows.Forms.PictureBox();
            this.errorLabel = new System.Windows.Forms.LinkLabel();
            this.alert = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorImage)).BeginInit();
            this.SuspendLayout();
            // 
            // errorImage
            // 
            this.errorImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorImage.Image = ((System.Drawing.Image)(resources.GetObject("errorImage.Image")));
            this.errorImage.Location = new System.Drawing.Point(193, 0);
            this.errorImage.Name = "errorImage";
            this.errorImage.Size = new System.Drawing.Size(20, 17);
            this.errorImage.TabIndex = 0;
            this.errorImage.TabStop = false;
            this.errorImage.Click += new System.EventHandler(this.errorImage_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.LinkColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(3, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(36, 15);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.TabStop = true;
            this.errorLabel.Text = "error";
            this.errorLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.errorLabel_LinkClicked);
            // 
            // alert
            // 
            this.alert.LookAndFeel.SkinName = "Blue";
            this.alert.LookAndFeel.UseDefaultLookAndFeel = false;
            this.alert.AlertClick += new DevExpress.XtraBars.Alerter.AlertClickEventHandler(this.alert_AlertClick);
            // 
            // ErrorMessageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.errorImage);
            this.Name = "ErrorMessageControl";
            this.Size = new System.Drawing.Size(220, 17);
            ((System.ComponentModel.ISupportInitialize)(this.errorImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox errorImage;
        private System.Windows.Forms.LinkLabel errorLabel;
        private DevExpress.XtraBars.Alerter.AlertControl alert;
    }
}