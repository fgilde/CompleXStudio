using CompleX_Library.Interfaces;

namespace CompleX.Controls
{
    partial class SelectPluginControl<T> where T : IHostedService
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
            this.listViewServices1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listViewServices1
            // 
            this.listViewServices1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewServices1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewServices1.FullRowSelect = true;
            this.listViewServices1.HideSelection = false;
            this.listViewServices1.Location = new System.Drawing.Point(0, 0);
            this.listViewServices1.MultiSelect = false;
            this.listViewServices1.Name = "listViewServices1";
            this.listViewServices1.Size = new System.Drawing.Size(561, 143);
            this.listViewServices1.TabIndex = 3;
            this.listViewServices1.UseCompatibleStateImageBehavior = false;
            this.listViewServices1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 259;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Version";
            this.columnHeader2.Width = 130;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 166;
            // 
            // SelectPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewServices1);
            this.MinimumSize = new System.Drawing.Size(300, 70);
            this.Name = "SelectPluginControl";
            this.Size = new System.Drawing.Size(561, 143);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewServices1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;

    }
}
