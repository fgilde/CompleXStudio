namespace CompleX.Controls
{
    partial class SearchOptionsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchOptionsControl));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.checkBoxInvertSearch = new System.Windows.Forms.CheckBox();
            this.comboBoxRegexOrWildCard = new System.Windows.Forms.ComboBox();
            this.checkBoxWildCardsOrRegex = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreCase = new System.Windows.Forms.CheckBox();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Controls.Add(this.checkBoxInvertSearch);
            this.groupBox.Controls.Add(this.comboBoxRegexOrWildCard);
            this.groupBox.Controls.Add(this.checkBoxWildCardsOrRegex);
            this.groupBox.Controls.Add(this.checkBoxMatchWholeWord);
            this.groupBox.Controls.Add(this.checkBoxIgnoreCase);
            this.groupBox.Controls.Add(this.buttonExpand);
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // checkBoxInvertSearch
            // 
            resources.ApplyResources(this.checkBoxInvertSearch, "checkBoxInvertSearch");
            this.checkBoxInvertSearch.Name = "checkBoxInvertSearch";
            this.checkBoxInvertSearch.UseVisualStyleBackColor = true;
            this.checkBoxInvertSearch.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // comboBoxRegexOrWildCard
            // 
            resources.ApplyResources(this.comboBoxRegexOrWildCard, "comboBoxRegexOrWildCard");
            this.comboBoxRegexOrWildCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRegexOrWildCard.FormattingEnabled = true;
            this.comboBoxRegexOrWildCard.Items.AddRange(new object[] {
            resources.GetString("comboBoxRegexOrWildCard.Items"),
            resources.GetString("comboBoxRegexOrWildCard.Items1")});
            this.comboBoxRegexOrWildCard.Name = "comboBoxRegexOrWildCard";
            this.comboBoxRegexOrWildCard.SelectedIndexChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // checkBoxWildCardsOrRegex
            // 
            resources.ApplyResources(this.checkBoxWildCardsOrRegex, "checkBoxWildCardsOrRegex");
            this.checkBoxWildCardsOrRegex.Name = "checkBoxWildCardsOrRegex";
            this.checkBoxWildCardsOrRegex.UseVisualStyleBackColor = true;
            this.checkBoxWildCardsOrRegex.CheckedChanged += new System.EventHandler(this.CheckBoxWildCardsOrRegexCheckedChanged);
            // 
            // checkBoxMatchWholeWord
            // 
            resources.ApplyResources(this.checkBoxMatchWholeWord, "checkBoxMatchWholeWord");
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            this.checkBoxMatchWholeWord.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // checkBoxIgnoreCase
            // 
            resources.ApplyResources(this.checkBoxIgnoreCase, "checkBoxIgnoreCase");
            this.checkBoxIgnoreCase.Name = "checkBoxIgnoreCase";
            this.checkBoxIgnoreCase.UseVisualStyleBackColor = true;
            this.checkBoxIgnoreCase.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // buttonExpand
            // 
            resources.ApplyResources(this.buttonExpand, "buttonExpand");
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.UseVisualStyleBackColor = true;
            this.buttonExpand.Click += new System.EventHandler(this.ButtonExpandClick);
            // 
            // SearchOptionsControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "SearchOptionsControl";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonExpand;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCase;
        private System.Windows.Forms.ComboBox comboBoxRegexOrWildCard;
        private System.Windows.Forms.CheckBox checkBoxWildCardsOrRegex;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxInvertSearch;
    }
}
