namespace CompleX_Optionpages
{
    partial class ExternalToolOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalToolOptionPage));
            this.listBoxTools = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textEditTitle = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.textEditCommand = new DevExpress.XtraEditors.ButtonEdit();
            this.textEditArgument = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textEditDirectory = new DevExpress.XtraEditors.ButtonEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textEditShortCut = new DevExpress.XtraEditors.TextEdit();
            this.checkBoxModal = new System.Windows.Forms.CheckBox();
            this.checkBoxReload = new System.Windows.Forms.CheckBox();
            this.popupContainerEditExtensions = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControlExtensions = new DevExpress.XtraEditors.PopupContainerControl();
            this.simpleButtonRemove = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.stringListEditControl1 = new CompleX.Controls.StringListEditControl();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonListPlaceHolders = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.buttonDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCommand.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArgument.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditShortCut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditExtensions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlExtensions)).BeginInit();
            this.popupContainerControlExtensions.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxTools
            // 
            resources.ApplyResources(this.listBoxTools, "listBoxTools");
            this.listBoxTools.FormattingEnabled = true;
            this.listBoxTools.Name = "listBoxTools";
            this.listBoxTools.SelectedIndexChanged += new System.EventHandler(this.ListBoxToolsSelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textEditTitle
            // 
            resources.ApplyResources(this.textEditTitle, "textEditTitle");
            this.textEditTitle.Name = "textEditTitle";
            this.textEditTitle.EditValueChanged += new System.EventHandler(this.TextEditTitleEditValueChanged);
            this.textEditTitle.Leave += new System.EventHandler(this.TextEditTitleLeave);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textEditCommand
            // 
            resources.ApplyResources(this.textEditCommand, "textEditCommand");
            this.textEditCommand.Name = "textEditCommand";
            this.textEditCommand.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.textEditCommand.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TextEditCommandButtonClick);
            this.textEditCommand.EditValueChanged += new System.EventHandler(this.ButtonEdit2EditValueChanged);
            // 
            // textEditArgument
            // 
            resources.ApplyResources(this.textEditArgument, "textEditArgument");
            this.textEditArgument.Name = "textEditArgument";
            this.textEditArgument.EditValueChanged += new System.EventHandler(this.ButtonEdit2EditValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textEditDirectory
            // 
            resources.ApplyResources(this.textEditDirectory, "textEditDirectory");
            this.textEditDirectory.Name = "textEditDirectory";
            this.textEditDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.textEditDirectory.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TextEditDirectoryButtonClick);
            this.textEditDirectory.EditValueChanged += new System.EventHandler(this.ButtonEdit2EditValueChanged);
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Image = global::CompleX_Optionpages.Images.anfangswert_16;
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Name = "label6";
            this.label6.Click += new System.EventHandler(this.CustomizeShortCutClick);
            // 
            // textEditShortCut
            // 
            resources.ApplyResources(this.textEditShortCut, "textEditShortCut");
            this.textEditShortCut.Name = "textEditShortCut";
            this.textEditShortCut.Properties.ReadOnly = true;
            this.textEditShortCut.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextEditShortCutKeyDown);
            // 
            // checkBoxModal
            // 
            resources.ApplyResources(this.checkBoxModal, "checkBoxModal");
            this.checkBoxModal.Name = "checkBoxModal";
            this.checkBoxModal.UseVisualStyleBackColor = true;
            this.checkBoxModal.CheckedChanged += new System.EventHandler(this.CheckBoxModalCheckedChanged);
            // 
            // checkBoxReload
            // 
            resources.ApplyResources(this.checkBoxReload, "checkBoxReload");
            this.checkBoxReload.Name = "checkBoxReload";
            this.checkBoxReload.UseVisualStyleBackColor = true;
            this.checkBoxReload.CheckedChanged += new System.EventHandler(this.CheckBoxReloadCheckedChanged);
            // 
            // popupContainerEditExtensions
            // 
            resources.ApplyResources(this.popupContainerEditExtensions, "popupContainerEditExtensions");
            this.popupContainerEditExtensions.Name = "popupContainerEditExtensions";
            this.popupContainerEditExtensions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popupContainerEditExtensions.Properties.Buttons"))))});
            this.popupContainerEditExtensions.Properties.PopupControl = this.popupContainerControlExtensions;
            // 
            // popupContainerControlExtensions
            // 
            this.popupContainerControlExtensions.Controls.Add(this.simpleButtonRemove);
            this.popupContainerControlExtensions.Controls.Add(this.simpleButtonAdd);
            this.popupContainerControlExtensions.Controls.Add(this.stringListEditControl1);
            resources.ApplyResources(this.popupContainerControlExtensions, "popupContainerControlExtensions");
            this.popupContainerControlExtensions.Name = "popupContainerControlExtensions";
            // 
            // simpleButtonRemove
            // 
            resources.ApplyResources(this.simpleButtonRemove, "simpleButtonRemove");
            this.simpleButtonRemove.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonRemove.Image")));
            this.simpleButtonRemove.Name = "simpleButtonRemove";
            this.simpleButtonRemove.Click += new System.EventHandler(this.SimpleButtonRemoveClick);
            // 
            // simpleButtonAdd
            // 
            resources.ApplyResources(this.simpleButtonAdd, "simpleButtonAdd");
            this.simpleButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonAdd.Image")));
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.Click += new System.EventHandler(this.SimpleButtonAddClick);
            // 
            // stringListEditControl1
            // 
            resources.ApplyResources(this.stringListEditControl1, "stringListEditControl1");
            this.stringListEditControl1.Name = "stringListEditControl1";
            this.stringListEditControl1.StringList = ((System.Collections.Generic.IEnumerable<string>)(resources.GetObject("stringListEditControl1.StringList")));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // buttonListPlaceHolders
            // 
            resources.ApplyResources(this.buttonListPlaceHolders, "buttonListPlaceHolders");
            this.buttonListPlaceHolders.Image = global::CompleX_Optionpages.Images.anfangswert_16;
            this.buttonListPlaceHolders.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.buttonListPlaceHolders.Name = "buttonListPlaceHolders";
            this.buttonListPlaceHolders.Click += new System.EventHandler(this.ButtonListPlaceHoldersClick);
            // 
            // simpleButton2
            // 
            resources.ApplyResources(this.simpleButton2, "simpleButton2");
            this.simpleButton2.Image = global::CompleX_Optionpages.Images.anhang_loeschen_16;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.SimpleButton2Click);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Image = global::CompleX_Optionpages.Images.minus16;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDeleteClick);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Image = global::CompleX_Optionpages.Images.plus16;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
            // 
            // ExternalToolOptionPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.popupContainerEditExtensions);
            this.Controls.Add(this.popupContainerControlExtensions);
            this.Controls.Add(this.checkBoxReload);
            this.Controls.Add(this.checkBoxModal);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.textEditShortCut);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEditDirectory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonListPlaceHolders);
            this.Controls.Add(this.textEditArgument);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textEditCommand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textEditTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxTools);
            this.MinimumSize = new System.Drawing.Size(380, 350);
            this.Name = "ExternalToolOptionPage";
            ((System.ComponentModel.ISupportInitialize)(this.textEditTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCommand.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditArgument.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditShortCut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditExtensions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlExtensions)).EndInit();
            this.popupContainerControlExtensions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTools;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton buttonDelete;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit textEditTitle;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ButtonEdit textEditCommand;
        private DevExpress.XtraEditors.TextEdit textEditArgument;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton buttonListPlaceHolders;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ButtonEdit textEditDirectory;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit textEditShortCut;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.CheckBox checkBoxModal;
        private System.Windows.Forms.CheckBox checkBoxReload;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditExtensions;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlExtensions;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRemove;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAdd;
        private CompleX.Controls.StringListEditControl stringListEditControl1;
        private System.Windows.Forms.Label label7;
    }
}
