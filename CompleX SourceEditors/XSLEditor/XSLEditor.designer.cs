namespace CompleX_SourceEditors.XSLEditor
{
    partial class XSLEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSLEditor));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lineToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.validateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enableISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pageSetupToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xslToolStrip = new System.Windows.Forms.ToolStrip();
            this.xslTextBox = new CompleX_SourceEditors.XSLEditor.XSLRichTextBox();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.xslToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineToolStripStatusLabel,
            this.columnToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 156);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(541, 22);
            this.statusStrip.TabIndex = 15;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lineToolStripStatusLabel
            // 
            this.lineToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lineToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lineToolStripStatusLabel.Name = "lineToolStripStatusLabel";
            this.lineToolStripStatusLabel.Size = new System.Drawing.Size(49, 17);
            this.lineToolStripStatusLabel.Text = "Line:     ";
            // 
            // columnToolStripStatusLabel
            // 
            this.columnToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.columnToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.columnToolStripStatusLabel.Name = "columnToolStripStatusLabel";
            this.columnToolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.columnToolStripStatusLabel.Text = "Col:    ";
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            this.printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.validateToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(541, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // validateToolStripMenuItem1
            // 
            this.validateToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableISToolStripMenuItem,
            this.toolStripMenuItem1,
            this.transformToolStripMenuItem,
            this.toolStripMenuItem2,
            this.validateToolStripMenuItem});
            this.validateToolStripMenuItem1.Name = "validateToolStripMenuItem1";
            this.validateToolStripMenuItem1.Size = new System.Drawing.Size(36, 20);
            this.validateToolStripMenuItem1.Text = "XSL";
            // 
            // enableISToolStripMenuItem
            // 
            this.enableISToolStripMenuItem.Checked = true;
            this.enableISToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableISToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.intellisense;
            this.enableISToolStripMenuItem.Name = "enableISToolStripMenuItem";
            this.enableISToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.enableISToolStripMenuItem.Text = "Enable IntelliSense";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordWrapToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // wordWrapToolStripMenuItem
            // 
            this.wordWrapToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.wordwrap;
            this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
            this.wordWrapToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.wordWrapToolStripMenuItem.Text = "Word Wrap";
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.xsl2;
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.transformToolStripMenuItem.Text = "Transform";
            this.transformToolStripMenuItem.Click += new System.EventHandler(this.transformToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(172, 6);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Image = global::CompleX_SourceEditors.Properties.Resources.tick;
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = global::CompleX_SourceEditors.Properties.Resources.find_doc;
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printPreviewToolStripButton.ToolTipText = "Print Preview";
            // 
            // pageSetupToolStripButton
            // 
            this.pageSetupToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageSetupToolStripButton.Image = global::CompleX_SourceEditors.Properties.Resources.JOB1;
            this.pageSetupToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pageSetupToolStripButton.Name = "pageSetupToolStripButton";
            this.pageSetupToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.pageSetupToolStripButton.ToolTipText = "Page Setup";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // findToolStripButton
            // 
            this.findToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findToolStripButton.Image = global::CompleX_SourceEditors.Properties.Resources.binoc;
            this.findToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findToolStripButton.Name = "findToolStripButton";
            this.findToolStripButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // xslToolStrip
            // 
            this.xslToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.pageSetupToolStripButton,
            this.toolStripSeparator,
            this.findToolStripButton,
            this.toolStripSeparator1});
            this.xslToolStrip.Location = new System.Drawing.Point(0, 24);
            this.xslToolStrip.Name = "xslToolStrip";
            this.xslToolStrip.Size = new System.Drawing.Size(541, 25);
            this.xslToolStrip.TabIndex = 1;
            this.xslToolStrip.Text = "toolStrip1";
            // 
            // xslTextBox
            // 
            this.xslTextBox.AcceptsTab = true;
            this.xslTextBox.Dirty = false;
            this.xslTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xslTextBox.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xslTextBox.Location = new System.Drawing.Point(0, 49);
            this.xslTextBox.Name = "xslTextBox";
            this.xslTextBox.Size = new System.Drawing.Size(541, 107);
            this.xslTextBox.SyntaxSettings.CommentColor = System.Drawing.Color.Gray;
            this.xslTextBox.SyntaxSettings.CommentPattern = null;
            this.xslTextBox.SyntaxSettings.EnableComments = true;
            this.xslTextBox.SyntaxSettings.IntegerColor = System.Drawing.Color.Empty;
            this.xslTextBox.SyntaxSettings.KeywordColor = System.Drawing.Color.Empty;
            this.xslTextBox.SyntaxSettings.StringColor = System.Drawing.Color.Empty;
            this.xslTextBox.TabIndex = 2;
            this.xslTextBox.Text = "";
            this.xslTextBox.WordWrap = false;
            this.xslTextBox.Enter += new System.EventHandler(this.xslTextBox_Enter);
            // 
            // XSLEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xslTextBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.xslToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Name = "XSLEditor";
            this.Size = new System.Drawing.Size(541, 178);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.xslToolStrip.ResumeLayout(false);
            this.xslToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lineToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel columnToolStripStatusLabel;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private XSLRichTextBox xslTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem enableISToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton pageSetupToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton findToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStrip xslToolStrip;
    }
}