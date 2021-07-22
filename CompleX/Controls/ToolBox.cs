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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings.Constants;
using CompleX_Types;
using DevExpress.XtraNavBar;
using MouseEventArgs=System.Windows.Forms.MouseEventArgs;

namespace CompleX.Controls
{

    public partial class ToolBox : HostedControl
    {

        private NavBarItemLink currentItemLink;
        private NavBarGroup currentGroup;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBox"/> class.
        /// </summary>
        public ToolBox()
        {
            InitializeComponent();
            LoadToolBox();
        }


        //[Browsable(true)]
        //public bool SearchEnabled 
        //{
        //    get
        //    {
        //        return panelCtrls.Visible;
        //    }
        //    set {
        //        panelCtrls.Visible = value;                          
        //    }
        //}


        public IEnumerable<NavBarItem> GetItems(string group)
        {
            NavBarGroup barGroup = FindBarGroup(group);
            if(barGroup != null)
            {
                foreach (NavBarItemLink link in barGroup.ItemLinks)
                {
                    yield return link.Item;
                }
            }
        }

        public IEnumerable<ToolBoxItem> GetToolBoxItems(string group)
        {
            NavBarGroup barGroup = FindBarGroup(group);
            if (barGroup != null)
            {
                foreach (NavBarItemLink link in barGroup.ItemLinks)
                {
                    if (link.Item.Tag is ToolBoxItem)
                    {
                        var toolBoxItem = link.Item.Tag as ToolBoxItem;
                        yield return toolBoxItem;
                    } 
                }
            }
        }

        public IEnumerable<string>Groups
        {
            get {
                for (int i = 0; i < navBarControl.Groups.Count; i++)
                    yield return navBarControl.Groups[i].Caption;
            }
        }

        /// <summary>
        /// Saves the tool box.
        /// </summary>
        /// <returns></returns>
        public bool SaveToolBox()
        {
            return SaveToolBox(Pathes.ToolBoxPath);            
        }

        /// <summary>
        /// Saves the tool box.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool SaveToolBox(string filename)
        {
            try
            {
                //SetVisibility(true,true);
                navBarControl.SaveToXml(filename);
                return File.Exists(filename);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Loads the tool box.
        /// </summary>
        /// <returns></returns>
        public bool LoadToolBox()
        {
            return LoadToolBox(Pathes.ToolBoxPath);
        }

        /// <summary>
        /// Loads the tool box.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool LoadToolBox(string filename)
        {
            if(File.Exists(filename))
            {
                try
                {
                    navBarControl.RestoreFromXml(filename);
                    UpdateAllImages();
                    DisableNotSupportedItems(CompleX_Studio.CurrentFile);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the expanded.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void SetExpanded(bool value)
        {
            for (int i = 0; i < navBarControl.Groups.Count; i++)
                navBarControl.Groups[i].Expanded = value;            
        }

        /// <summary>
        /// Collapses all.
        /// </summary>
        public void CollapseAll()
        {
            SetExpanded(false);
        }

        /// <summary>
        /// Expands all.
        /// </summary>
        public void ExpandAll()
        {
            SetExpanded(true);        
        }

        /// <summary>
        /// Filters the Toolbox by given search string.
        /// </summary>
        /// <param name="search">The searchstring.</param>
        /// <param name="hideEmptyGroups">if set to <c>true</c> [hide empty groups].</param>
        public void FilterToolbox(string search, bool hideEmptyGroups)
        {
            if (!hideEmptyGroups)
            {
                for (int i = 0; i < navBarControl.Items.Count; i++)
                {
                    if (navBarControl.Items[i].Enabled)
                        navBarControl.Items[i].Visible = navBarControl.Items[i].Caption.ToLower().Contains(search.ToLower());
                }
            }
            else
            {
                for (int i = 0; i < navBarControl.Groups.Count; i++)
                {
                    navBarControl.Groups[i].Visible = false;
                    for (int y = 0; y < navBarControl.Groups[i].ItemLinks.Count; y++)
                    {
                        if (navBarControl.Groups[i].ItemLinks[y].Item.Enabled && navBarControl.Groups[i].ItemLinks[y].Caption.ToLower().Contains(textEditSearch.Text.ToLower()))
                        {
                            navBarControl.Groups[i].ItemLinks[y].Visible = true;
                            navBarControl.Groups[i].Visible = true;
                            navBarControl.Groups[i].Expanded = true;
                        }
                        else
                        {
                            navBarControl.Groups[i].ItemLinks[y].Visible = false;
                        }
                    }
                }         
            }
        }

        /// <summary>
        /// Adds a item to the toolbox.
        /// </summary>
        /// <param name="toolBoxItem">The tool box item.</param>
        public bool AddItem(ToolBoxItem toolBoxItem)
        {
            if (String.IsNullOrEmpty(toolBoxItem.Category))
                toolBoxItem.Category = Const.DefaultToolBoxCategory;
            NavBarGroup barGroup = navBarControl.Groups[toolBoxItem.Category];
            if (barGroup == null)
            {
                barGroup = FindBarGroup(toolBoxItem.Category);
                if (barGroup == null)
                {
                    barGroup = navBarControl.Groups.Add(new NavBarGroup(toolBoxItem.Category));
                    barGroup.Name = toolBoxItem.Category;
                }
            }

            foreach (NavBarItemLink link in barGroup.ItemLinks)
            {
                if (link.Caption.Equals(toolBoxItem.Text) || link.Item.Caption.Equals(toolBoxItem.Text))
                    return false;
            }

            var item = new NavBarItem(toolBoxItem.Text)
                           {
                               Name = DateTime.Now.Ticks + "_" + toolBoxItem.Text,
                               SmallImage =
                                   ImageFunctions.ResizeImage(toolBoxItem.Image, 16, 16, true, Color.Transparent, 2.0, 2.0),
                               LargeImage =
                                   ImageFunctions.ResizeImage(toolBoxItem.Image, 32, 32, true, Color.Transparent, 2.0, 2.0),
                               Tag = toolBoxItem
                           };

            barGroup.ItemLinks.Add(item);

            SaveToolBox();
            return true;
        }

        /// <summary>
        /// Disables the not supported items.
        /// </summary>
        /// <param name="fileName">The file extension.</param>
        public void DisableNotSupportedItems(string fileName)
        {
   
            SetVisibility(!String.IsNullOrEmpty(fileName), true);
            if (!String.IsNullOrEmpty(fileName))
            {
                foreach (NavBarItem item in navBarControl.Items)
                {
                    item.Enabled = true;
                    if (item.Tag is ToolBoxItem)
                    {
                        var toolBoxItem = item.Tag as ToolBoxItem;
                        item.Enabled = Extensions.ExtensionIsAllowed(toolBoxItem.SupportedFileExtensions, fileName);
                        if (toolBoxItem.InserterId != Guid.Empty)
                        {
                            var inserter = ApplicationHost.Host.GetServiceById<IInserter>(toolBoxItem.InserterId);
                            if (inserter != null)
                                item.Enabled = Extensions.ExtensionIsAllowed(inserter.SupportedFileExtensions, fileName);
                        }
                    }
                    item.Visible = item.Enabled;
                }
            }
        }

        private NavBarGroup FindBarGroup(string caption)
        {
            foreach (NavBarGroup barGroup in navBarControl.Groups)
                if (barGroup.Caption.Equals(caption))
                    return barGroup;          
            return null;
        }

        private void SetVisibility(bool visible, bool alsoGroups)
        {
            if (!alsoGroups)
            {
                for (int i = 0; i < navBarControl.Items.Count; i++)
                {
                    navBarControl.Items[i].Visible = visible;
                    navBarControl.Items[i].Enabled = visible;
                }
            }
            else
            {
                for (int i = 0; i < navBarControl.Groups.Count; i++)
                {
                    navBarControl.Groups[i].Visible = visible;
                    for (int y = 0; y < navBarControl.Groups[i].ItemLinks.Count; y++)
                    {
                        navBarControl.Groups[i].ItemLinks[y].Visible = visible;
                        navBarControl.Groups[i].ItemLinks[y].Item.Enabled = visible;
                    }
                }
            }  
        }

        private void UpdateAllImages()
        {
            foreach (NavBarItem item in navBarControl.Items)
            {
               if(item.Tag is ToolBoxItem)
               {
                   var toolBoxItem = item.Tag as ToolBoxItem;
                   item.SmallImage = ImageFunctions.ResizeImage(toolBoxItem.Image, 16, 16, true, Color.Transparent, 2.0, 2.0);
                   item.LargeImage = ImageFunctions.ResizeImage(toolBoxItem.Image, 32, 32, true, Color.Transparent, 2.0, 2.0);
               }
            } 
        } 

        private void textEditSearch_EditValueChanged(object sender, EventArgs e)
        {
            FilterToolbox(textEditSearch.Text, true);
        }

        private void buttonExpand_Click(object sender, EventArgs e)
        {
            ExpandAll();
        }

        private void buttonCollapse_Click(object sender, EventArgs e)
        {
            CollapseAll();
        }


        private void navBarControl_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            CompleX_Studio.InsertToolBoxEntry(e.Link.Item);
        }

        private void navBarControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NavBarHitInfo info = navBarControl.CalcHitInfo(new Point(e.X, e.Y));
                if(info.InLink)
                {
                    ShowItemContextMenu(info.Link);
                }
                else if(info.InGroupCaption)
                {
                    ShowGroupContextMenu(info.Group);
                }
                else if (info.InGroup)
                {
                    currentGroup = info.Group;
                    ShowItemContextMenu(null);   
                }else
                {
                    ShowGroupContextMenu(null);                    
                }
            }
        }

        private void ShowGroupContextMenu(NavBarGroup barGroup)
        {
            // Menu for editing Groups
            currentGroup = barGroup;
            barButtonRenameGroup.Enabled =
            barButtonAddItem.Enabled =
            barButtonDeleteGroup.Enabled = barGroup != null;

            popupMenuEditGroup.ShowPopup(barManagerToolBox, Cursor.Position);          

        }

        private void ShowItemContextMenu(NavBarItemLink link)
        {
            //Menu for eteding itementry
            barButtonAddItem.Enabled = currentGroup != null;
            currentItemLink = link;
            barButtonRenameItem.Enabled =
            barButtonConfigureItem.Enabled =
            barButtonDeleteItem.Enabled = link != null;
            
            popupMenuEditEntry.ShowPopup(barManagerToolBox, Cursor.Position);          
        }



        private void navBarControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Move : DragDropEffects.None;
        }


        private void navBarControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var text = (string) e.Data.GetData(DataFormats.Text);
                
                NavBarHitInfo info = navBarControl.CalcHitInfo(PointToClient(new Point(e.X, e.Y)));
                if (info.InGroup)
                {
                    var itemToAdd = new ToolBoxItem(text, text) {Category = info.Group.Name};
                    if (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile))
                        itemToAdd.SupportedFileExtensions.Add(Path.GetExtension(CompleX_Studio.CurrentFile));
                    
                    AddItem(itemToAdd);
                }
            }
        }

        private void barButtonRenameItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Rename Item
            if(currentItemLink != null && currentItemLink.Item != null)
            {
               string newName = InputDlg.Execute(barButtonRenameItem.Caption,Properties.Resources.Name ,currentItemLink.Caption);
               if(!String.IsNullOrEmpty(newName) && !newName.Equals(currentItemLink.Item.Caption))
               {   
                   currentItemLink.Item.Caption = newName;
                   if (currentItemLink.Item.Tag is ToolBoxItem)
                   {
                       var item = (ToolBoxItem)currentItemLink.Item.Tag;
                       item.Text = newName;
                   }
                   SaveToolBox();
               }              
            }
        }

        private void barButtonDeleteItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Delete Item
            if (currentItemLink != null && currentItemLink.Item != null)
            {
                bool canDelete = MessageService.Ask(Properties.Resources.DeleteShowConfirmation, currentItemLink.Caption);
                if(canDelete)
                {
                    navBarControl.Items.Remove(currentItemLink.Item);
                    SaveToolBox();
                }
            }
        }

        private void barButtonRenameGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Rename Group
            if (currentGroup != null)
            {
                string newName = InputDlg.Execute(barButtonRenameGroup.Caption, Properties.Resources.Name, currentGroup.Caption);
                if (!String.IsNullOrEmpty(newName) && !newName.Equals(currentGroup.Caption))
                {
                    if (navBarControl.Groups[newName] == null && FindBarGroup(newName) == null)
                    {
                        currentGroup.Caption = newName;
                        currentGroup.Name = newName;
                        SaveToolBox();
                    }else
                    {
                        string errorMessage = String.Format(Properties.Resources.Exception_GroupAllwaysExists, newName);
                        MessageService.ShowError(errorMessage, Properties.Resources.Exception);                        
                    }
                }
            }
        }

        private void barButtonDeleteGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Delete Group
            if (currentGroup != null)
            {
                bool canDelete = MessageService.Ask(Properties.Resources.DeleteShowConfirmation, currentGroup.Caption);
                if (canDelete)
                {
                    navBarControl.Groups.Remove(currentGroup);
                    SaveToolBox();
                }
            }

        }

        private void barButtonAddGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Add Group
            AddGroup();
        }

        public void AddGroup()
        {
            string newName = InputDlg.Execute(barButtonAddGroup.Caption, Properties.Resources.Name);
            if (!String.IsNullOrEmpty(newName))
            {
                if (navBarControl.Groups[newName] == null && FindBarGroup(newName) == null)
                {
                    AddGroup(newName);
                }
                else
                {
                    string errorMessage = String.Format(Properties.Resources.Exception_GroupAllwaysExists, newName);
                    MessageService.ShowError(errorMessage, Properties.Resources.Exception);
                }
            }
        }

        public bool AddGroup(string newName)
        {
            if (!String.IsNullOrEmpty(newName))
            {
                if (navBarControl.Groups[newName] == null && FindBarGroup(newName) == null)
                {
                    var barGroup = navBarControl.Groups.Add(new NavBarGroup(newName));
                    barGroup.Name = newName;
                    SaveToolBox();
                    return true;
                }
            }
            return false;
        }

        private void barButtonConfigureItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Edit Item
            if(currentItemLink != null && currentItemLink.Item != null)
            {
                if (currentItemLink.Item.Tag is ToolBoxItem)
                {
                    var item = (ToolBoxItem)currentItemLink.Item.Tag;
                    using(var control = new ToolBoxItemEditControl(item,false))
                    {
                        var dlg = BaseDialogHelper.CreateBaseDialog(control);

                        dlg.IsValid = (() =>
                                           {
                                               if (!String.IsNullOrEmpty(control.ToolBoxItem.Category) &&
                                                   !String.IsNullOrEmpty(control.ToolBoxItem.Text))
                                                   return new ValidationResult(true, String.Empty);
                                               return new ValidationResult(false,
                                                                           Properties.Resources.
                                                                               ErrorToolboxEntryNameOrCategoryEmpty);
                                           });
                   
                        if(dlg.ShowDialog() == DialogResult.OK)
                        {
                            item = control.ToolBoxItem.Clone() as ToolBoxItem;
                            currentItemLink.Item.Caption = item.Text;
                            currentItemLink.Item.SmallImage = ImageFunctions.ResizeImage(item.Image, 16, 16, true, Color.Transparent, 2.0, 2.0);
                            currentItemLink.Item.LargeImage = ImageFunctions.ResizeImage(item.Image, 32, 32, true, Color.Transparent, 2.0, 2.0);
                            currentItemLink.Item.Tag = item.Clone();
                            SaveToolBox();
                        }
                    }

                }else
                {
                    CompleX_Studio.InspectObject(currentItemLink.Item);
                }
            }
        }

        private void barButtonExportToolBox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Export
            var dlg = new SaveFileDialog { Filter = "CompleX Toolbox | *.cstbx", DefaultExt = ".cstbx" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!Path.GetExtension(dlg.FileName).Equals(dlg.DefaultExt))
                    dlg.FileName += "."+dlg.DefaultExt;
                SaveToolBox(dlg.FileName);
            }

        }

        private void barButtonImportToolBox_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Import
            var dlg = new OpenFileDialog { Filter = "CompleX Toolbox | *.cstbx", DefaultExt = ".cstbx" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                navBarControl.Items.Clear();
                LoadToolBox(dlg.FileName);
                SaveToolBox();
            }
        }

        private void barButtonAddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Add Item
            AddItem();
        }

        public void AddItem()
        {
            string group = currentGroup != null ? currentGroup.Caption : String.Empty;
            var item = new ToolBoxItem(String.Empty, String.Empty) { Category = group };
            using (var control = new ToolBoxItemEditControl(item, true))
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(control);
                dlg.IsValid = (() =>
                                   {
                                       if (!String.IsNullOrEmpty(control.ToolBoxItem.Category) &&
                                           !String.IsNullOrEmpty(control.ToolBoxItem.Text))
                                           return new ValidationResult(true, String.Empty);
                                       return new ValidationResult(false,
                                                                   Properties.Resources.
                                                                       ErrorToolboxEntryNameOrCategoryEmpty);
                                   });
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    item = control.ToolBoxItem.Clone() as ToolBoxItem;
                    AddItem(item);
                    SaveToolBox();                   
                }
            }
        }

        private void barButtonShowAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowAll();
        }


        public void ShowAll()
        {
            SetVisibility(true, true);
            SetVisibility(true, false);
        }
    }
}
