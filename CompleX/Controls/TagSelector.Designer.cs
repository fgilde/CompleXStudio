namespace CompleX.Controls
{
    partial class TagSelector
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
            this.treeViewLanguages = new System.Windows.Forms.TreeView();
            this.tagListBox = new DevExpress.XtraEditors.ListBoxControl();
            this.textEditSearch = new CompleX.Controls.SearchTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tagListBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewLanguages
            // 
            this.treeViewLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewLanguages.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewLanguages.Location = new System.Drawing.Point(8, 8);
            this.treeViewLanguages.Name = "treeViewLanguages";
            this.treeViewLanguages.Size = new System.Drawing.Size(175, 190);
            this.treeViewLanguages.TabIndex = 5;
            this.treeViewLanguages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewLanguagesAfterSelect1);
            // 
            // tagListBox
            // 
            this.tagListBox.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.tagListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tagListBox.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.tagListBox.Location = new System.Drawing.Point(189, 29);
            this.tagListBox.Name = "tagListBox";
            this.tagListBox.Size = new System.Drawing.Size(291, 169);
            this.tagListBox.TabIndex = 4;
            this.tagListBox.DoubleClick += new System.EventHandler(this.TagListBoxDoubleClick);
            this.tagListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TagListBoxKeyUp);
            this.tagListBox.SelectedIndexChanged += new System.EventHandler(this.TagListBoxSelectedIndexChanged);
            // 
            // textEditSearch
            // 
            this.textEditSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditSearch.Location = new System.Drawing.Point(189, 7);
            this.textEditSearch.Name = "textEditSearch";
            this.textEditSearch.Size = new System.Drawing.Size(291, 20);
            this.textEditSearch.TabIndex = 6;
            this.textEditSearch.EditValueChanged += new System.EventHandler(this.TextEditSearchEditValueChanged);
            this.textEditSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextEditSearchKeyUp);
            // 
            // TagSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEditSearch);
            this.Controls.Add(this.treeViewLanguages);
            this.Controls.Add(this.tagListBox);
            this.Name = "TagSelector";
            this.Size = new System.Drawing.Size(489, 205);
            ((System.ComponentModel.ISupportInitialize)(this.tagListBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLanguages;
        private DevExpress.XtraEditors.ListBoxControl tagListBox;
        private CompleX.Controls.SearchTextBox textEditSearch;
    }
}
