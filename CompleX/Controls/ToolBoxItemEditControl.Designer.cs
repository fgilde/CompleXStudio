namespace CompleX.Controls
{
    partial class ToolBoxItemEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBoxItemEditControl));
            this.popupContainerControlExtensions = new DevExpress.XtraEditors.PopupContainerControl();
            this.simpleButtonRemove = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.stringListEditControl1 = new CompleX.Controls.StringListEditControl();
            this.popupContainerParamEdit = new DevExpress.XtraEditors.PopupContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxEditCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEditSelectType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.textEditText = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.inserterHostContainer = new System.Windows.Forms.Panel();
            this.labelInserterName = new System.Windows.Forms.Label();
            this.simpleButtonRemoveInserter = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonSelectInserter = new DevExpress.XtraEditors.SimpleButton();
            this.buttonClearParam = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.imageEdit = new DevExpress.XtraEditors.ImageEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.popupContainerEditExtensions = new DevExpress.XtraEditors.PopupContainerEdit();
            this.labelObj = new System.Windows.Forms.Label();
            this.popupContainerEditInsertObject = new DevExpress.XtraEditors.PopupContainerEdit();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlExtensions)).BeginInit();
            this.popupContainerControlExtensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerParamEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.inserterHostContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditExtensions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditInsertObject.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.simpleButtonRemove.Image = global::CompleX.Properties.Resources.minus16;
            this.simpleButtonRemove.Name = "simpleButtonRemove";
            this.simpleButtonRemove.Click += new System.EventHandler(this.simpleButtonRemove_Click_1);
            // 
            // simpleButtonAdd
            // 
            resources.ApplyResources(this.simpleButtonAdd, "simpleButtonAdd");
            this.simpleButtonAdd.Image = global::CompleX.Properties.Resources.plus16;
            this.simpleButtonAdd.Name = "simpleButtonAdd";
            this.simpleButtonAdd.Click += new System.EventHandler(this.simpleButtonAdd_Click_1);
            // 
            // stringListEditControl1
            // 
            resources.ApplyResources(this.stringListEditControl1, "stringListEditControl1");
            this.stringListEditControl1.Name = "stringListEditControl1";
            this.stringListEditControl1.StringList = ((System.Collections.Generic.IEnumerable<string>)(resources.GetObject("stringListEditControl1.StringList")));
            // 
            // popupContainerParamEdit
            // 
            resources.ApplyResources(this.popupContainerParamEdit, "popupContainerParamEdit");
            this.popupContainerParamEdit.Name = "popupContainerParamEdit";
            // 
            // groupControl1
            // 
            resources.ApplyResources(this.groupControl1, "groupControl1");
            this.groupControl1.Controls.Add(this.popupContainerParamEdit);
            this.groupControl1.Controls.Add(this.popupContainerControlExtensions);
            this.groupControl1.Controls.Add(this.comboBoxEditCategory);
            this.groupControl1.Controls.Add(this.comboBoxEditSelectType);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.textEditText);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            // 
            // comboBoxEditCategory
            // 
            resources.ApplyResources(this.comboBoxEditCategory, "comboBoxEditCategory");
            this.comboBoxEditCategory.Name = "comboBoxEditCategory";
            this.comboBoxEditCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditCategory.Properties.Buttons"))))});
            this.comboBoxEditCategory.EditValueChanged += new System.EventHandler(this.comboBoxEditCategory_EditValueChanged);
            this.comboBoxEditCategory.SelectedValueChanged += new System.EventHandler(this.comboBoxEditCategory_SelectedValueChanged);
            // 
            // comboBoxEditSelectType
            // 
            resources.ApplyResources(this.comboBoxEditSelectType, "comboBoxEditSelectType");
            this.comboBoxEditSelectType.Name = "comboBoxEditSelectType";
            this.comboBoxEditSelectType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditSelectType.Properties.Buttons"))))});
            this.comboBoxEditSelectType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textEditText
            // 
            resources.ApplyResources(this.textEditText, "textEditText");
            this.textEditText.Name = "textEditText";
            this.textEditText.EditValueChanged += new System.EventHandler(this.textEditText_EditValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupControl2
            // 
            resources.ApplyResources(this.groupControl2, "groupControl2");
            this.groupControl2.Controls.Add(this.inserterHostContainer);
            this.groupControl2.Controls.Add(this.buttonClearParam);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.imageEdit);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Controls.Add(this.popupContainerEditExtensions);
            this.groupControl2.Controls.Add(this.labelObj);
            this.groupControl2.Controls.Add(this.popupContainerEditInsertObject);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Name = "groupControl2";
            // 
            // inserterHostContainer
            // 
            resources.ApplyResources(this.inserterHostContainer, "inserterHostContainer");
            this.inserterHostContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inserterHostContainer.Controls.Add(this.labelInserterName);
            this.inserterHostContainer.Controls.Add(this.simpleButtonRemoveInserter);
            this.inserterHostContainer.Controls.Add(this.simpleButtonSelectInserter);
            this.inserterHostContainer.Name = "inserterHostContainer";
            // 
            // labelInserterName
            // 
            resources.ApplyResources(this.labelInserterName, "labelInserterName");
            this.labelInserterName.Name = "labelInserterName";
            // 
            // simpleButtonRemoveInserter
            // 
            resources.ApplyResources(this.simpleButtonRemoveInserter, "simpleButtonRemoveInserter");
            this.simpleButtonRemoveInserter.Image = global::CompleX.Properties.Resources.anhang_loeschen_16;
            this.simpleButtonRemoveInserter.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonRemoveInserter.Name = "simpleButtonRemoveInserter";
            this.simpleButtonRemoveInserter.Click += new System.EventHandler(this.simpleButtonRemoveInserter_Click);
            // 
            // simpleButtonSelectInserter
            // 
            resources.ApplyResources(this.simpleButtonSelectInserter, "simpleButtonSelectInserter");
            this.simpleButtonSelectInserter.Name = "simpleButtonSelectInserter";
            this.simpleButtonSelectInserter.Click += new System.EventHandler(this.simpleButtonSelectInserter_Click);
            // 
            // buttonClearParam
            // 
            resources.ApplyResources(this.buttonClearParam, "buttonClearParam");
            this.buttonClearParam.Image = global::CompleX.Properties.Resources.anhang_loeschen_16;
            this.buttonClearParam.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.buttonClearParam.Name = "buttonClearParam";
            this.buttonClearParam.Click += new System.EventHandler(this.buttonClearParam_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // imageEdit
            // 
            resources.ApplyResources(this.imageEdit, "imageEdit");
            this.imageEdit.Name = "imageEdit";
            this.imageEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("imageEdit.Properties.Buttons"))))});
            this.imageEdit.ImageChanged += new System.EventHandler(this.imageEdit_ImageChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // popupContainerEditExtensions
            // 
            resources.ApplyResources(this.popupContainerEditExtensions, "popupContainerEditExtensions");
            this.popupContainerEditExtensions.Name = "popupContainerEditExtensions";
            this.popupContainerEditExtensions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popupContainerEditExtensions.Properties.Buttons"))))});
            this.popupContainerEditExtensions.Properties.PopupControl = this.popupContainerControlExtensions;
            // 
            // labelObj
            // 
            resources.ApplyResources(this.labelObj, "labelObj");
            this.labelObj.Name = "labelObj";
            // 
            // popupContainerEditInsertObject
            // 
            resources.ApplyResources(this.popupContainerEditInsertObject, "popupContainerEditInsertObject");
            this.popupContainerEditInsertObject.Name = "popupContainerEditInsertObject";
            this.popupContainerEditInsertObject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("popupContainerEditInsertObject.Properties.Buttons"))))});
            this.popupContainerEditInsertObject.Properties.PopupControl = this.popupContainerParamEdit;
            this.popupContainerEditInsertObject.Click += new System.EventHandler(this.popupContainerEditInsertObject_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ToolBoxItemEditControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.MinimumSize = new System.Drawing.Size(300, 290);
            this.Name = "ToolBoxItemEditControl";
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControlExtensions)).EndInit();
            this.popupContainerControlExtensions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerParamEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSelectType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.inserterHostContainer.ResumeLayout(false);
            this.inserterHostContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditExtensions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEditInsertObject.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlExtensions;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRemove;
        private DevExpress.XtraEditors.SimpleButton simpleButtonAdd;
        private StringListEditControl stringListEditControl1;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerParamEdit;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSelectType;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit textEditText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton buttonClearParam;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ImageEdit imageEdit;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditExtensions;
        private System.Windows.Forms.Label labelObj;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEditInsertObject;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCategory;
        private System.Windows.Forms.Panel inserterHostContainer;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRemoveInserter;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelectInserter;
        private System.Windows.Forms.Label labelInserterName;
    }
}
