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
using System.Threading;
using System.Windows.Forms;
using CompleX;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings.Constants;
using Microsoft.Win32;


namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class FileAssociationOptionPage : BaseWizardOptionPage
    {
        private readonly List<ExtensionInfo> extensions = new List<ExtensionInfo>();
        private readonly List<ExtensionInfo> associatedItems = new List<ExtensionInfo>();
        public FileAssociationOptionPage()
        {
            InitializeComponent();
        }
        public override Guid ID
        {
            get
            {
                return new Guid("A0966F55-8082-490B-A1B0-D4FECB355E42");
            }
        }

        public override ValidationResult ValidatePage()
        {
            bool result = !WindowsSecurityHelper.IsVistaOrHigher || WindowsSecurityHelper.IsAdmin;
            return new ValidationResult(result, label2.Text);
        }

        public override bool Initialize()
        {
            if (WindowsSecurityHelper.IsVistaOrHigher && !WindowsSecurityHelper.IsAdmin)
            {
                panel1.Visible = true;
                WindowsSecurityHelper.AddShieldToButton(button1);
            }
            return true;
        }

        private void FillExtensionsList()
        {
            RegistryKey root = Registry.ClassesRoot;
            string[] subKeys = root.GetSubKeyNames();
            extensionsListView.BeginUpdate();
            extensionsListView.Items.Clear();
            extensions.Clear();
            foreach (string subKey in subKeys)
            {
                if (subKey.StartsWith("."))
                {
                    bool isAssociated = FileService.IsAssociated(subKey);
                    // Nur extensions anzeigen, die manuell gesetzt werden können, CompleX Projects werden z.B immer mit CS assoziert
                    if (!Const.CompleXFileExtensions.Keys.Contains(subKey)) 
                    {
                        var item = new ListViewItem(subKey);
                        string extensionName;
                        var descriptionByExtension = FileHelper.GetFileDescriptionByExtension(subKey, out extensionName);
                        item.SubItems.Add(descriptionByExtension);
                        item.Checked = isAssociated;
                        var info = new ExtensionInfo(subKey, descriptionByExtension, isAssociated);
                        extensions.Add(info);
                        if(isAssociated)
                        {
                            associatedItems.Add(info);
                            item.Font = label1.Font;
                        }
                        extensionsListView.Items.Add(item);
                    }
                }
            }
            extensionsListView.EndUpdate();
        }

        public override void OnCancel()
        {
           extensionsListView.Items.Clear();
           extensions.Clear();
           associatedItems.Clear();
        }

        public override bool OnOk()
        {
            bool result = false;
            if (!WindowsSecurityHelper.IsVistaOrHigher || WindowsSecurityHelper.IsAdmin)
            {
                // Remove always existing assiciations where user has unchecked item now
                foreach (ExtensionInfo associatedItem in associatedItems.Where(info => !info.IsChecked))
                {
                    FileService.RemoveFileAssociation(associatedItem.Extension);
                }

                // Add New Associations
                var newAssociations = extensions.Where(info => info.IsChecked && !associatedItems.Contains(info));
                foreach (ExtensionInfo newAssociation in newAssociations)
                {
                    ExtensionInfo association = newAssociation;
                    ThreadPool.QueueUserWorkItem(state => FileService.CreateFileAssociation(association.Extension, association.Description));
                }

                result = true;
            }

            extensionsListView.Items.Clear();
            extensions.Clear();
            associatedItems.Clear();
            return result;
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
           if(activated)
           {
               if (extensionsListView.Items.Count <= 0)
               {
                   FillExtensionsList();
               }
           }
        }

        public override Image Image
        {
            get { return Images.datenebenen; }
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
            get { return "FileAssociationOptions"; }
        }

        public override string ParentPageID
        {
            get
            {
                return "GeneralOptions";
            }
        }

        public override string PageTitle
        {
            get
            {
                return "File Association";
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchBy(textBoxSearch.Text);
        }

        private void SearchBy(string search)
        {
            extensionsListView.BeginUpdate();
            extensionsListView.Items.Clear();
            var foundedExtensions = extensions.Where(pair => pair.Extension.ToLower().Contains(search));
            if(checkBoxShowChecked.Checked)
            {
                foundedExtensions = foundedExtensions.Where(info => info.IsChecked);
            }
            foreach (ExtensionInfo extensionInfo in foundedExtensions)
            {
                bool isAssociated = extensionInfo.IsChecked;

                var item = new ListViewItem(extensionInfo.Extension);
                item.SubItems.Add(extensionInfo.Description);
                item.Checked = isAssociated;
                extensionsListView.Items.Add(item);               
            }
            extensionsListView.EndUpdate();
        }

        private void extensionsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var extensionInfo = extensions.FirstOrDefault(info => info.Extension.Equals(e.Item.Text));
            if (extensionInfo != null)
                extensionInfo.IsChecked = e.Item.Checked;
            
            var associatedInfo = extensions.FirstOrDefault(info => info.Extension.Equals(e.Item.Text));
            if (associatedInfo != null)
                associatedInfo.IsChecked = e.Item.Checked;
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CompleX_Studio.Restart(true);
        }

        private void checkBoxShowChecked_CheckedChanged(object sender, EventArgs e)
        {
            SearchBy(textBoxSearch.Text);
        }

        private void extensionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListViewItem item = extensionsListView.SelectedItems[0];
                if(item != null)
                {
                    pictureBoxIcon.Image = ImageFunctions.GetAssociatedIconByExtension(item.Text, true).ToBitmap();
                }
            }
            catch (Exception)
            {
            }
        }
    }


    /// <summary>
    /// CLASS ExtensionInfo
    /// </summary>
    class ExtensionInfo
    {
        internal string Extension { get; set; }
        internal string Description { get; set; }
        internal bool IsChecked { get; set; }

        public ExtensionInfo(string extension, string description, bool isChecked)
        {
            Extension = extension;
            Description = description;
            IsChecked = isChecked;
        }
    }
}
