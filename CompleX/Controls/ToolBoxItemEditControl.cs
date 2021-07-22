//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Linq;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class ToolBoxItemEditControl : UserControl
    {
        public  ToolBoxItem ToolBoxItem { get; private set;}
        public  IInserter Inserter;
        private bool inInit;

        public event EventHandler ToolBoxItemChanged;

        private void InvokeToolBoxItemChanged(EventArgs e)
        {
            EventHandler changed = ToolBoxItemChanged;
            if (changed != null) changed(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBoxItemEditControl"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="canChangeGroup">if set to <c>true</c> [can change group].</param>
        public ToolBoxItemEditControl(ToolBoxItem item,bool canChangeGroup)
        {
            InitializeComponent();
            comboBoxEditCategory.Enabled = canChangeGroup;
            Init(item);
        }


        private void Init(ToolBoxItem item)
        {
            inInit = true;
            if (item.Image != null)
                imageEdit.Image = item.Image;

            stringListEditControl1.StringList = item.SupportedFileExtensions;
            ToolBoxItem = item.Clone() as ToolBoxItem;
            if(item.InserterId != Guid.Empty)
            {
                Inserter = ApplicationHost.Host.GetServiceById<IInserter>(item.InserterId);
                if (Inserter != null)
                {
                    labelInserterName.Text = Inserter.ServiceName;
                    stringListEditControl1.StringList = Inserter.SupportedFileExtensions;
                }
            }
            simpleButtonAdd.Enabled = Inserter == null;
            comboBoxEditSelectType.Enabled = Inserter == null;
            simpleButtonRemove.Enabled = Inserter == null;
            if (ToolBoxItem != null)
                labelObj.Text = ToolBoxItem.InserterId == Guid.Empty
                                    ? Resources.ObjectToInsert
                                    : Resources.InsertParam;

            simpleButtonRemoveInserter.Enabled = Inserter != null;

            textEditText.Text = item.Text;
           
            FillGroups();
            comboBoxEditCategory.SelectedItem = item.Category;

            UpdateExtensionText();
            if (ToolBoxItem != null && ToolBoxItem.Insert != null)
               popupContainerEditInsertObject.Text = ToolBoxItem.Insert.ToString();
            
            if (Inserter != null && Inserter.Image != null && item.Image == null)
                imageEdit.Image = Inserter.Image;

            inInit = false;
            FillPossibileTypes();
            
        }

        private void UpdateItem()
        {
            if (!inInit)
            {
                ToolBoxItem.Text = textEditText.Text;
                ToolBoxItem.Category = comboBoxEditCategory.SelectedItem.ToString();
                if (Inserter != null)
                {
                    ToolBoxItem.InserterId = Inserter.ID;
                    labelObj.Text = Resources.InsertParam;
                    labelInserterName.Text = Inserter.ServiceName;
                }
                else
                {
                    labelObj.Text = Resources.ObjectToInsert;
                    ToolBoxItem.InserterId = Guid.Empty;
                    labelInserterName.Text = "<none>";
                    ToolBoxItem.SupportedFileExtensions = stringListEditControl1.StringList.ToList();
                }

                ToolBoxItem.Image = imageEdit.Image;
                simpleButtonAdd.Enabled = Inserter == null;
                comboBoxEditSelectType.Enabled = Inserter == null;
                simpleButtonRemove.Enabled = Inserter == null;
                simpleButtonRemoveInserter.Enabled = Inserter != null;
                UpdateExtensionText();
                InvokeToolBoxItemChanged(new EventArgs());
            }
        }

        private void UpdateExtensionText()
        {
            popupContainerEditExtensions.Text = String.Empty;
            foreach (string s in stringListEditControl1.StringList)
                popupContainerEditExtensions.Text += s + ",";
            
        }

        private void simpleButtonAdd_Click_1(object sender, EventArgs e)
        {
            string ext = InputDlg.Execute(simpleButtonAdd.Text, Resources.Extension);
            if (!string.IsNullOrEmpty(ext))
            {
                if (!ext.StartsWith("."))
                    ext = "." + ext;
                stringListEditControl1.Add(ext);
                UpdateItem();
            }
        }

        private void simpleButtonRemove_Click_1(object sender, EventArgs e)
        {
            stringListEditControl1.DeleteSelected();
            UpdateItem();
        }

        private void textEditText_EditValueChanged(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void simpleButtonSelectInserter_Click(object sender, EventArgs e)
        {
            using(var control = new SelectPluginControl<IInserter>())
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(control);
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    IInserter service = control.GetSelectedService();
                    if (service != null)
                    {
                        Inserter = service;
                        if (Inserter.Image != null)
                            imageEdit.Image = Inserter.Image;
                        stringListEditControl1.StringList = Inserter.SupportedFileExtensions;
                        UpdateItem();
                        comboBoxEditSelectType.SelectedItem = Inserter.GetParameterType();
                    }
                }
            }
        }

        private void simpleButtonRemoveInserter_Click(object sender, EventArgs e)
        {
            if(Inserter != null)
            {
                Inserter = null;
                stringListEditControl1.StringList = ToolBoxItem.SupportedFileExtensions;
                UpdateItem();
            }
        }

        private void FillGroups()
        {
            comboBoxEditCategory.Properties.Items.Clear();
            foreach (string category in CompleX_Studio.ToolBox.Groups)
                comboBoxEditCategory.Properties.Items.Add(category);        
        }

        private void FillPossibileTypes()
        {
            comboBoxEditSelectType.Properties.Items.Clear();
            var paremeditors = ApplicationHost.Host.GetServices<IObjectEdit>();
            foreach (IObjectEdit edit in paremeditors)
                comboBoxEditSelectType.Properties.Items.Add(edit.GetPossibleObjectType());
            if (ToolBoxItem != null && ToolBoxItem.Insert != null && Inserter == null)
                comboBoxEditSelectType.SelectedItem = ToolBoxItem.Insert.GetType(); 
            else if(Inserter != null)
                comboBoxEditSelectType.SelectedItem = Inserter.GetParameterType(); 
        }

        private Type GetCurrentType()
        {
            if (comboBoxEditSelectType.SelectedItem != null && comboBoxEditSelectType.SelectedItem is Type)
                return comboBoxEditSelectType.SelectedItem as Type;
            if (ToolBoxItem.Insert != null)
                return ToolBoxItem.Insert.GetType();
            return typeof (string);
        }

        private void popupContainerEditInsertObject_Click(object sender, EventArgs e)
        {
            Type type = Inserter != null ? Inserter.GetParameterType() : GetCurrentType();
            var paremeditor = ApplicationHost.Host.GetService<IObjectEdit>(edit => edit.GetPossibleObjectType().Equals(type));
            if(paremeditor != null)
            {
                popupContainerParamEdit.Controls.Clear();
                paremeditor.Content = ToolBoxItem.Insert;
                paremeditor.ObjectEditingFinished += ParemeditorObjectEditingFinished;
                var control = paremeditor.Control;
                popupContainerParamEdit.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        void ParemeditorObjectEditingFinished(object sender, EventArgs e)
        {
            ToolBoxItem.Insert = ((IObjectEdit)sender).Content;
            InvokeToolBoxItemChanged(new EventArgs());
            popupContainerEditInsertObject.Text = ToolBoxItem.Insert.ToString();
            popupContainerEditInsertObject.ClosePopup();
        }

        private void imageEdit_ImageChanged(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void buttonClearParam_Click(object sender, EventArgs e)
        {
            popupContainerEditInsertObject.Text = String.Empty;
            ToolBoxItem.Insert = null;
        }

        private void comboBoxEditCategory_EditValueChanged(object sender, EventArgs e)
        {
            UpdateItem();
        }

        private void comboBoxEditCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateItem();
        }
    }
}
