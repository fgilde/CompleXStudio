namespace CompleX.Controls
{
    partial class NewProjectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectControl));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewTemplates = new System.Windows.Forms.ListView();
            this.textEditDescription = new DevExpress.XtraEditors.TextEdit();
            this.labelName = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxLocation = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.checkCreateDir = new DevExpress.XtraEditors.CheckEdit();
            this.textEditProjectName = new DevExpress.XtraEditors.TextEdit();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.treeViewTypes = new System.Windows.Forms.TreeView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.textEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCreateDir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // listViewTemplates
            // 
            this.listViewTemplates.AccessibleDescription = null;
            this.listViewTemplates.AccessibleName = null;
            resources.ApplyResources(this.listViewTemplates, "listViewTemplates");
            this.listViewTemplates.BackgroundImage = null;
            this.listViewTemplates.Font = null;
            this.listViewTemplates.LabelEdit = true;
            this.listViewTemplates.MultiSelect = false;
            this.listViewTemplates.Name = "listViewTemplates";
            this.listViewTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewTemplates.View = System.Windows.Forms.View.Tile;
            this.listViewTemplates.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListViewTemplatesAfterLabelEdit);
            this.listViewTemplates.SelectedIndexChanged += new System.EventHandler(this.ListViewTemplatesSelectedIndexChanged);
            // 
            // textEditDescription
            // 
            resources.ApplyResources(this.textEditDescription, "textEditDescription");
            this.textEditDescription.BackgroundImage = null;
            this.textEditDescription.EditValue = null;
            this.textEditDescription.Name = "textEditDescription";
            this.textEditDescription.Properties.AccessibleDescription = null;
            this.textEditDescription.Properties.AccessibleName = null;
            this.textEditDescription.Properties.AutoHeight = ((bool)(resources.GetObject("textEditDescription.Properties.AutoHeight")));
            this.textEditDescription.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditDescription.Properties.Mask.AutoComplete")));
            this.textEditDescription.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditDescription.Properties.Mask.BeepOnError")));
            this.textEditDescription.Properties.Mask.EditMask = resources.GetString("textEditDescription.Properties.Mask.EditMask");
            this.textEditDescription.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditDescription.Properties.Mask.IgnoreMaskBlank")));
            this.textEditDescription.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditDescription.Properties.Mask.MaskType")));
            this.textEditDescription.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditDescription.Properties.Mask.PlaceHolder")));
            this.textEditDescription.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditDescription.Properties.Mask.SaveLiteral")));
            this.textEditDescription.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditDescription.Properties.Mask.ShowPlaceHolders")));
            this.textEditDescription.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditDescription.Properties.Mask.UseMaskAsDisplayFormat")));
            // 
            // labelName
            // 
            this.labelName.AccessibleDescription = null;
            this.labelName.AccessibleName = null;
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelLocation
            // 
            this.labelLocation.AccessibleDescription = null;
            this.labelLocation.AccessibleName = null;
            resources.ApplyResources(this.labelLocation, "labelLocation");
            this.labelLocation.Name = "labelLocation";
            // 
            // textEditName
            // 
            resources.ApplyResources(this.textEditName, "textEditName");
            this.textEditName.BackgroundImage = null;
            this.textEditName.EditValue = null;
            this.textEditName.Name = "textEditName";
            this.textEditName.Properties.AccessibleDescription = null;
            this.textEditName.Properties.AccessibleName = null;
            this.textEditName.Properties.AutoHeight = ((bool)(resources.GetObject("textEditName.Properties.AutoHeight")));
            this.textEditName.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditName.Properties.Mask.AutoComplete")));
            this.textEditName.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditName.Properties.Mask.BeepOnError")));
            this.textEditName.Properties.Mask.EditMask = resources.GetString("textEditName.Properties.Mask.EditMask");
            this.textEditName.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditName.Properties.Mask.IgnoreMaskBlank")));
            this.textEditName.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditName.Properties.Mask.MaskType")));
            this.textEditName.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditName.Properties.Mask.PlaceHolder")));
            this.textEditName.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditName.Properties.Mask.SaveLiteral")));
            this.textEditName.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditName.Properties.Mask.ShowPlaceHolders")));
            this.textEditName.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditName.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEditName.EditValueChanged += new System.EventHandler(this.TextEditNameEditValueChanged);
            // 
            // comboBoxLocation
            // 
            resources.ApplyResources(this.comboBoxLocation, "comboBoxLocation");
            this.comboBoxLocation.BackgroundImage = null;
            this.comboBoxLocation.EditValue = null;
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Properties.AccessibleDescription = null;
            this.comboBoxLocation.Properties.AccessibleName = null;
            this.comboBoxLocation.Properties.AutoHeight = ((bool)(resources.GetObject("comboBoxLocation.Properties.AutoHeight")));
            this.comboBoxLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxLocation.Properties.Buttons"))))});
            // 
            // btnBrowse
            // 
            this.btnBrowse.AccessibleDescription = null;
            this.btnBrowse.AccessibleName = null;
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.BackgroundImage = null;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
            // 
            // checkCreateDir
            // 
            resources.ApplyResources(this.checkCreateDir, "checkCreateDir");
            this.checkCreateDir.BackgroundImage = null;
            this.checkCreateDir.Name = "checkCreateDir";
            this.checkCreateDir.Properties.AccessibleDescription = null;
            this.checkCreateDir.Properties.AccessibleName = null;
            this.checkCreateDir.Properties.AutoHeight = ((bool)(resources.GetObject("checkCreateDir.Properties.AutoHeight")));
            this.checkCreateDir.Properties.Caption = resources.GetString("checkCreateDir.Properties.Caption");
            // 
            // textEditProjectName
            // 
            resources.ApplyResources(this.textEditProjectName, "textEditProjectName");
            this.textEditProjectName.BackgroundImage = null;
            this.textEditProjectName.EditValue = null;
            this.textEditProjectName.Name = "textEditProjectName";
            this.textEditProjectName.Properties.AccessibleDescription = null;
            this.textEditProjectName.Properties.AccessibleName = null;
            this.textEditProjectName.Properties.AutoHeight = ((bool)(resources.GetObject("textEditProjectName.Properties.AutoHeight")));
            this.textEditProjectName.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditProjectName.Properties.Mask.AutoComplete")));
            this.textEditProjectName.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditProjectName.Properties.Mask.BeepOnError")));
            this.textEditProjectName.Properties.Mask.EditMask = resources.GetString("textEditProjectName.Properties.Mask.EditMask");
            this.textEditProjectName.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditProjectName.Properties.Mask.IgnoreMaskBlank")));
            this.textEditProjectName.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditProjectName.Properties.Mask.MaskType")));
            this.textEditProjectName.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditProjectName.Properties.Mask.PlaceHolder")));
            this.textEditProjectName.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditProjectName.Properties.Mask.SaveLiteral")));
            this.textEditProjectName.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditProjectName.Properties.Mask.ShowPlaceHolders")));
            this.textEditProjectName.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditProjectName.Properties.Mask.UseMaskAsDisplayFormat")));
            // 
            // labelProjectName
            // 
            this.labelProjectName.AccessibleDescription = null;
            this.labelProjectName.AccessibleName = null;
            resources.ApplyResources(this.labelProjectName, "labelProjectName");
            this.labelProjectName.Name = "labelProjectName";
            // 
            // treeViewTypes
            // 
            this.treeViewTypes.AccessibleDescription = null;
            this.treeViewTypes.AccessibleName = null;
            resources.ApplyResources(this.treeViewTypes, "treeViewTypes");
            this.treeViewTypes.BackgroundImage = null;
            this.treeViewTypes.Name = "treeViewTypes";
            this.treeViewTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewTypesAfterSelect);
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleDescription = null;
            this.pictureBox2.AccessibleName = null;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackgroundImage = null;
            this.pictureBox2.Font = null;
            this.pictureBox2.Image = global::CompleX.Properties.Resources.imgSmallIcons;
            this.pictureBox2.ImageLocation = null;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.PictureBox2Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackgroundImage = null;
            this.pictureBox1.Font = null;
            this.pictureBox1.Image = global::CompleX.Properties.Resources.imgLargeIcons1;
            this.pictureBox1.ImageLocation = null;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1Click);
            // 
            // NewProjectControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.treeViewTypes);
            this.Controls.Add(this.textEditProjectName);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.checkCreateDir);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.comboBoxLocation);
            this.Controls.Add(this.textEditName);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textEditDescription);
            this.Controls.Add(this.listViewTemplates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "NewProjectControl";
            ((System.ComponentModel.ISupportInitialize)(this.textEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkCreateDir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewTemplates;
        private DevExpress.XtraEditors.TextEdit textEditDescription;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelLocation;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxLocation;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.CheckEdit checkCreateDir;
        private DevExpress.XtraEditors.TextEdit textEditProjectName;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TreeView treeViewTypes;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
