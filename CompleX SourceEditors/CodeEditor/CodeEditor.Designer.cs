namespace CompleX_SourceEditors.CodeEditor
{
    partial class CodeEditor
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surroundObjectWithTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllOpenTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.syntaxHighlightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.buttonChangeColor = new System.Windows.Forms.Button();
            this.labelColorText = new System.Windows.Forms.Label();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.CodeEditorControl = new CompleX_SourceEditors.CodeEditor.CodeEditorControl();
            this.menuStrip.SuspendLayout();
            this.colorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(394, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportCodeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportCodeToolStripMenuItem
            // 
            this.exportCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asHTMLToolStripMenuItem});
            this.exportCodeToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.exportieren;
            this.exportCodeToolStripMenuItem.Name = "exportCodeToolStripMenuItem";
            this.exportCodeToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exportCodeToolStripMenuItem.Text = "Export Code";
            // 
            // asHTMLToolStripMenuItem
            // 
            this.asHTMLToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.html16;
            this.asHTMLToolStripMenuItem.Name = "asHTMLToolStripMenuItem";
            this.asHTMLToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.asHTMLToolStripMenuItem.Text = "As HTML...";
            this.asHTMLToolStripMenuItem.Click += new System.EventHandler(this.AsHtmlToolStripMenuItemClick1);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSelectedTagToolStripMenuItem,
            this.surroundObjectWithTagToolStripMenuItem,
            this.closeAllOpenTagsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // editSelectedTagToolStripMenuItem
            // 
            this.editSelectedTagToolStripMenuItem.Name = "editSelectedTagToolStripMenuItem";
            this.editSelectedTagToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editSelectedTagToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.editSelectedTagToolStripMenuItem.Text = "Edit Selected Tag";
            this.editSelectedTagToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedTagToolStripMenuItemClick);
            // 
            // surroundObjectWithTagToolStripMenuItem
            // 
            this.surroundObjectWithTagToolStripMenuItem.Name = "surroundObjectWithTagToolStripMenuItem";
            this.surroundObjectWithTagToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.J)));
            this.surroundObjectWithTagToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.surroundObjectWithTagToolStripMenuItem.Text = "Surround Object with Tag";
            this.surroundObjectWithTagToolStripMenuItem.Click += new System.EventHandler(this.SurroundObjectWithTagToolStripMenuItemClick);
            // 
            // closeAllOpenTagsToolStripMenuItem
            // 
            this.closeAllOpenTagsToolStripMenuItem.Name = "closeAllOpenTagsToolStripMenuItem";
            this.closeAllOpenTagsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.closeAllOpenTagsToolStripMenuItem.Size = new System.Drawing.Size(275, 22);
            this.closeAllOpenTagsToolStripMenuItem.Text = "&Close All Open Tags";
            this.closeAllOpenTagsToolStripMenuItem.Click += new System.EventHandler(this.CloseAllOpenTagsToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.syntaxHighlightingToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(174, 6);
            // 
            // syntaxHighlightingToolStripMenuItem
            // 
            this.syntaxHighlightingToolStripMenuItem.Name = "syntaxHighlightingToolStripMenuItem";
            this.syntaxHighlightingToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.syntaxHighlightingToolStripMenuItem.Text = "Syntax Highlighting";
            // 
            // colorPanel
            // 
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPanel.Controls.Add(this.buttonChangeColor);
            this.colorPanel.Controls.Add(this.labelColorText);
            this.colorPanel.Location = new System.Drawing.Point(165, 3);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(95, 25);
            this.colorPanel.TabIndex = 2;
            this.colorPanel.Visible = false;
            // 
            // buttonChangeColor
            // 
            this.buttonChangeColor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonChangeColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonChangeColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonChangeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeColor.Location = new System.Drawing.Point(67, -1);
            this.buttonChangeColor.Name = "buttonChangeColor";
            this.buttonChangeColor.Size = new System.Drawing.Size(27, 25);
            this.buttonChangeColor.TabIndex = 1;
            this.buttonChangeColor.Text = "...";
            this.buttonChangeColor.UseVisualStyleBackColor = true;
            this.buttonChangeColor.Click += new System.EventHandler(this.ButtonChangeColorClick);
            // 
            // labelColorText
            // 
            this.labelColorText.AutoSize = true;
            this.labelColorText.Location = new System.Drawing.Point(3, 5);
            this.labelColorText.Name = "labelColorText";
            this.labelColorText.Size = new System.Drawing.Size(0, 13);
            this.labelColorText.TabIndex = 0;
            // 
            // elementHost
            // 
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(0, 24);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(394, 326);
            this.elementHost.TabIndex = 0;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = this.CodeEditorControl;
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.elementHost);
            this.Controls.Add(this.menuStrip);
            this.Name = "CodeEditor";
            this.Size = new System.Drawing.Size(394, 350);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost;
        private CodeEditorControl CodeEditorControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSelectedTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syntaxHighlightingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem surroundObjectWithTagToolStripMenuItem;
        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label labelColorText;
        private System.Windows.Forms.Button buttonChangeColor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllOpenTagsToolStripMenuItem;
    }
}
