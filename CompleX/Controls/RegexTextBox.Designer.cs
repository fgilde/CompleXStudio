namespace CompleX.Controls
{
    partial class RegexTextBox
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
            this.comboEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.contextMenuStripRegex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zeroOrMoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneOrMoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.beginningOfLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endOfLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginningOfWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endOfWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nLineBrealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.anyOneCharacterNotInTheSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.escapeSpecialCharactersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagExpressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.iCCIdentifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qQuotedStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSpaceOrTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zIntegerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.openRegExTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.comboEdit.Properties)).BeginInit();
            this.contextMenuStripRegex.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboEdit
            // 
            this.comboEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboEdit.Location = new System.Drawing.Point(0, 0);
            this.comboEdit.Name = "comboEdit";
            this.comboEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Right)});
            this.comboEdit.Size = new System.Drawing.Size(363, 20);
            this.comboEdit.TabIndex = 0;
            this.comboEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.ComboEditButtonClick);
            // 
            // contextMenuStripRegex
            // 
            this.contextMenuStripRegex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.zeroOrMoreToolStripMenuItem,
            this.oneOrMoreToolStripMenuItem,
            this.toolStripMenuItem2,
            this.beginningOfLineToolStripMenuItem,
            this.endOfLineToolStripMenuItem,
            this.beginningOfWordToolStripMenuItem,
            this.endOfWordToolStripMenuItem,
            this.nLineBrealToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem5,
            this.anyOneCharacterNotInTheSetToolStripMenuItem,
            this.orToolStripMenuItem,
            this.escapeSpecialCharactersToolStripMenuItem,
            this.tagExpressionToolStripMenuItem,
            this.toolStripMenuItem4,
            this.iCCIdentifierToolStripMenuItem,
            this.qQuotedStringToolStripMenuItem,
            this.bSpaceOrTabToolStripMenuItem,
            this.zIntegerToolStripMenuItem,
            this.toolStripMenuItem6,
            this.openRegExTesterToolStripMenuItem});
            this.contextMenuStripRegex.Name = "contextMenuStripRegex";
            this.contextMenuStripRegex.Size = new System.Drawing.Size(261, 446);
            this.contextMenuStripRegex.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripRegexOpening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItem1.Tag = ".";
            this.toolStripMenuItem1.Text = ". Any single character";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // zeroOrMoreToolStripMenuItem
            // 
            this.zeroOrMoreToolStripMenuItem.Name = "zeroOrMoreToolStripMenuItem";
            this.zeroOrMoreToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.zeroOrMoreToolStripMenuItem.Tag = "*";
            this.zeroOrMoreToolStripMenuItem.Text = "* Zero or more";
            this.zeroOrMoreToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // oneOrMoreToolStripMenuItem
            // 
            this.oneOrMoreToolStripMenuItem.Name = "oneOrMoreToolStripMenuItem";
            this.oneOrMoreToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.oneOrMoreToolStripMenuItem.Tag = "*";
            this.oneOrMoreToolStripMenuItem.Text = "+ One or more";
            this.oneOrMoreToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(257, 6);
            // 
            // beginningOfLineToolStripMenuItem
            // 
            this.beginningOfLineToolStripMenuItem.Name = "beginningOfLineToolStripMenuItem";
            this.beginningOfLineToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.beginningOfLineToolStripMenuItem.Tag = "^";
            this.beginningOfLineToolStripMenuItem.Text = "^ Beginning of line";
            this.beginningOfLineToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // endOfLineToolStripMenuItem
            // 
            this.endOfLineToolStripMenuItem.Name = "endOfLineToolStripMenuItem";
            this.endOfLineToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.endOfLineToolStripMenuItem.Tag = "$";
            this.endOfLineToolStripMenuItem.Text = "$ End of line";
            this.endOfLineToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // beginningOfWordToolStripMenuItem
            // 
            this.beginningOfWordToolStripMenuItem.Name = "beginningOfWordToolStripMenuItem";
            this.beginningOfWordToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.beginningOfWordToolStripMenuItem.Tag = "<";
            this.beginningOfWordToolStripMenuItem.Text = "< Beginning of word";
            this.beginningOfWordToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // endOfWordToolStripMenuItem
            // 
            this.endOfWordToolStripMenuItem.Name = "endOfWordToolStripMenuItem";
            this.endOfWordToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.endOfWordToolStripMenuItem.Tag = ">";
            this.endOfWordToolStripMenuItem.Text = "> End of word";
            this.endOfWordToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // nLineBrealToolStripMenuItem
            // 
            this.nLineBrealToolStripMenuItem.Name = "nLineBrealToolStripMenuItem";
            this.nLineBrealToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.nLineBrealToolStripMenuItem.Tag = "\\n";
            this.nLineBrealToolStripMenuItem.Text = "\\n Line breal";
            this.nLineBrealToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(257, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(260, 22);
            this.toolStripMenuItem5.Tag = "[]";
            this.toolStripMenuItem5.Text = "[] Any one character in the set";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // anyOneCharacterNotInTheSetToolStripMenuItem
            // 
            this.anyOneCharacterNotInTheSetToolStripMenuItem.Name = "anyOneCharacterNotInTheSetToolStripMenuItem";
            this.anyOneCharacterNotInTheSetToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.anyOneCharacterNotInTheSetToolStripMenuItem.Tag = "[^]";
            this.anyOneCharacterNotInTheSetToolStripMenuItem.Text = "[^] Any one character not in the set";
            this.anyOneCharacterNotInTheSetToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // orToolStripMenuItem
            // 
            this.orToolStripMenuItem.Name = "orToolStripMenuItem";
            this.orToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.orToolStripMenuItem.Tag = "|";
            this.orToolStripMenuItem.Text = "| Or";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // escapeSpecialCharactersToolStripMenuItem
            // 
            this.escapeSpecialCharactersToolStripMenuItem.Name = "escapeSpecialCharactersToolStripMenuItem";
            this.escapeSpecialCharactersToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.escapeSpecialCharactersToolStripMenuItem.Tag = "\\";
            this.escapeSpecialCharactersToolStripMenuItem.Text = "\\ Escape special character";
            this.escapeSpecialCharactersToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // tagExpressionToolStripMenuItem
            // 
            this.tagExpressionToolStripMenuItem.Name = "tagExpressionToolStripMenuItem";
            this.tagExpressionToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.tagExpressionToolStripMenuItem.Tag = "{}";
            this.tagExpressionToolStripMenuItem.Text = "{} Tag expression";
            this.tagExpressionToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(257, 6);
            // 
            // iCCIdentifierToolStripMenuItem
            // 
            this.iCCIdentifierToolStripMenuItem.Name = "iCCIdentifierToolStripMenuItem";
            this.iCCIdentifierToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.iCCIdentifierToolStripMenuItem.Tag = ":i";
            this.iCCIdentifierToolStripMenuItem.Text = ":i C/C++ identifier";
            this.iCCIdentifierToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // qQuotedStringToolStripMenuItem
            // 
            this.qQuotedStringToolStripMenuItem.Name = "qQuotedStringToolStripMenuItem";
            this.qQuotedStringToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.qQuotedStringToolStripMenuItem.Tag = ":q";
            this.qQuotedStringToolStripMenuItem.Text = ":q Quoted string";
            this.qQuotedStringToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // bSpaceOrTabToolStripMenuItem
            // 
            this.bSpaceOrTabToolStripMenuItem.Name = "bSpaceOrTabToolStripMenuItem";
            this.bSpaceOrTabToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.bSpaceOrTabToolStripMenuItem.Tag = ":b";
            this.bSpaceOrTabToolStripMenuItem.Text = ":b Space or Tab";
            this.bSpaceOrTabToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // zIntegerToolStripMenuItem
            // 
            this.zIntegerToolStripMenuItem.Name = "zIntegerToolStripMenuItem";
            this.zIntegerToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.zIntegerToolStripMenuItem.Tag = ":z";
            this.zIntegerToolStripMenuItem.Text = ":z Integer";
            this.zIntegerToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertRegexClick);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(257, 6);
            // 
            // openRegExTesterToolStripMenuItem
            // 
            this.openRegExTesterToolStripMenuItem.Name = "openRegExTesterToolStripMenuItem";
            this.openRegExTesterToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.openRegExTesterToolStripMenuItem.Text = "Open RegEx Tester...";
            this.openRegExTesterToolStripMenuItem.Click += new System.EventHandler(this.OpenRegExTesterToolStripMenuItemClick);
            // 
            // RegexTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboEdit);
            this.Name = "RegexTextBox";
            this.Size = new System.Drawing.Size(363, 198);
            ((System.ComponentModel.ISupportInitialize)(this.comboEdit.Properties)).EndInit();
            this.contextMenuStripRegex.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripRegex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem zeroOrMoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneOrMoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem beginningOfLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endOfLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginningOfWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endOfWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nLineBrealToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem anyOneCharacterNotInTheSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem escapeSpecialCharactersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagExpressionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem iCCIdentifierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qQuotedStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bSpaceOrTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zIntegerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem openRegExTesterToolStripMenuItem;

    }
}
