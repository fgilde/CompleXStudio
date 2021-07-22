//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CompleX;
using CompleX.Dialogs;
using CompleX.Helper;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Types;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class ExternalToolOptionPage : BaseOptionPage
    {
        private List<ExternalTool> tools;
        private bool autoUpdate;
        public ExternalTool SelectedTool
        {
            get
            {
                if (tools != null && listBoxTools.SelectedItem != null && listBoxTools.SelectedIndex >=0)
                    return tools[listBoxTools.SelectedIndex];               
                return null;
            }
        }

        public ExternalToolOptionPage()
        {
            InitializeComponent();
            tools = null;
        }

        public override Guid ID
        {
            get
            {
                return new Guid("ACD44352-89AA-4D29-9E9C-2E2B29070452");
            }
        }

        public override void OnCancel()
        {
            Settings.Set("ExternalTools", Settings.Get<List<ExternalTool>>("ExternalTools") ?? new List<ExternalTool>());
            tools = null;
        }

        public override bool OnOk()
        {
            Settings.Set("ExternalTools", tools);
            tools = null;
            MenuService.UpdateToolsMenu();
            return true;
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
            if (activated && tools == null)
            {
                autoUpdate = true;
                textEditTitle.Text = String.Empty;
                textEditArgument.Text = String.Empty;
                textEditDirectory.Text = String.Empty;
                textEditCommand.Text = String.Empty;
                autoUpdate = false;
                tools = Settings.Get<List<ExternalTool>>("ExternalTools") ?? new List<ExternalTool>();
                UpdateList();
                EnableControls();
            }else if(tools != null)
            {
                EnableControls();
                UpdateTextEntries();
            }
        }

        private void UpdateList()
        {
            listBoxTools.BeginUpdate();
            listBoxTools.Items.Clear();
            foreach (ExternalTool tool in tools)
            {
                if(tool != null)
                   listBoxTools.Items.Add(tool.Name);
            }

            listBoxTools.EndUpdate();
        }

        private void UpdateSelectedTool()
        {
            if (SelectedTool != null && !autoUpdate)
            {
                SelectedTool.Name = textEditTitle.Text;
                SelectedTool.InitialDirectory = textEditDirectory.Text;
                SelectedTool.Argument = textEditArgument.Text;
                SelectedTool.Command = textEditCommand.Text;
                SelectedTool.ReloadFileAfterClose = checkBoxReload.Checked;
                SelectedTool.ShowModal = checkBoxModal.Checked;
                SelectedTool.FileExtensions = stringListEditControl1.StringList.Count() > 0 ? stringListEditControl1.StringList.ToList() : null;
                UpdateExtensionText();
            }
        }

        public override ValidationResult ValidatePage()
        {
            return new ValidationResult(true, String.Empty);
        }


        public override Image Image
        {
            get { return Images.werkzeuge_16; }
        }

        public override bool AllowedAsProjectOptions
        {
            get
            {
                return false;
            }
        }

        public override string PageID
        {
            get { return "ExternalToolsOptions"; }
        }

        public override string ParentPageID
        {
            get
            {
                return "ToolsOptions";
            }
        }

        public override string PageTitle
        {
            get
            {
                return "External Tools";
            }
        }

        private void ButtonEdit2EditValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedTool();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            string name = InputDlg.Execute(label2.Text, label2.Text);
            if (!string.IsNullOrEmpty(name))
            {
                tools.Add(new ExternalTool {Name = name});
                UpdateList();
            }
        }

        private void ButtonDeleteClick(object sender, EventArgs e)
        {
            if (SelectedTool != null)
            {
                tools.Remove(SelectedTool);
                UpdateList();
            }
        }

        private void ListBoxToolsSelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControls();
            UpdateTextEntries();
        }

        private void EnableControls()
        {
            buttonDelete.Enabled =
                checkBoxReload.Enabled =
                checkBoxModal.Enabled=
                textEditTitle.Enabled=
                textEditDirectory.Enabled =
                textEditCommand.Enabled =
                textEditShortCut.Enabled =
                simpleButton2.Enabled =
                simpleButton1.Enabled =
                buttonListPlaceHolders.Enabled =
                textEditArgument.Enabled = SelectedTool != null;
        }

        private void UpdateTextEntries()
        {
            if(SelectedTool != null)
            {
                autoUpdate = true;
                textEditTitle.Text = SelectedTool.Name;
                textEditArgument.Text = SelectedTool.Argument;
                textEditCommand.Text = SelectedTool.Command;
                textEditDirectory.Text = SelectedTool.InitialDirectory;
                textEditShortCut.Text = ((Keys)Convert.ToInt32(SelectedTool.Shortcut)).ToString();
                checkBoxReload.Checked = SelectedTool.ReloadFileAfterClose;
                checkBoxModal.Checked = SelectedTool.ShowModal;
                stringListEditControl1.StringList = SelectedTool.FileExtensions;
                UpdateExtensionText();
                autoUpdate = false;
            }
        }

        private void TextEditTitleEditValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedTool();
        }

        private void TextEditTitleLeave(object sender, EventArgs e)
        {
            if (listBoxTools.SelectedItem != null)
            {
                listBoxTools.Items[listBoxTools.SelectedIndex] = textEditTitle.Text;
            }
        }

        private void TextEditShortCutKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                SimpleButton2Click(sender, e);
            else
            {
                textEditShortCut.Text = e.KeyData.ToString();
                if (SelectedTool != null)
                {
                    int shortcut = Convert.ToInt32(e.KeyData);
                    SelectedTool.Shortcut = (Shortcut) shortcut;
                }
            }
        }

        private void SimpleButton2Click(object sender, EventArgs e)
        {
            if(SelectedTool != null)
            {
                textEditShortCut.Text = @"None";
                SelectedTool.Shortcut = Shortcut.None;                
            }
        }

        private void TextEditCommandButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if(dlg.ShowDialog()== DialogResult.OK)
            {
                textEditCommand.Text = dlg.FileName;
            }
        }

        private void TextEditDirectoryButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textEditDirectory.Text = dlg.SelectedPath;
            }
        }

        private void ButtonListPlaceHoldersClick(object sender, EventArgs e)
        {
            PlaceHolder.ShowPopup(s => textEditArgument.Text += s +@" ");
        }

        private void SimpleButton1Click(object sender, EventArgs e)
        {
            PlaceHolder.ShowPopup(s => textEditDirectory.Text += s + @" ");
        }

        private void CheckBoxModalCheckedChanged(object sender, EventArgs e)
        {
            UpdateSelectedTool();
        }

        private void CheckBoxReloadCheckedChanged(object sender, EventArgs e)
        {
            UpdateSelectedTool();
        }

        private void SimpleButtonAddClick(object sender, EventArgs e)
        {
            string ext = InputDlg.Execute(simpleButtonAdd.Text, Resources.Extension);
            if (!string.IsNullOrEmpty(ext))
            {
                if (!ext.StartsWith("."))
                    ext = "." + ext;
                stringListEditControl1.Add(ext);
                UpdateSelectedTool();
            }
        }

        private void SimpleButtonRemoveClick(object sender, EventArgs e)
        {
            stringListEditControl1.DeleteSelected();
            UpdateSelectedTool();
        }

        private void UpdateExtensionText()
        {
            popupContainerEditExtensions.Text = String.Empty;
            foreach (string s in stringListEditControl1.StringList)
                popupContainerEditExtensions.Text += s + @",";

        }

        private void CustomizeShortCutClick(object sender, EventArgs e)
        {
            if (SelectedTool != null)
            {
                var editor = new ShortcutKeysEditor();
                var serviceProvider = new RuntimeServiceProvider();
                var res = editor.EditValue(serviceProvider, serviceProvider, SelectedTool.Shortcut);
                if (res != null)
                {
                    textEditShortCut.Text = res.ToString();
                    SelectedTool.Shortcut = (Shortcut) res;
                }
            }
        }

    }
}
