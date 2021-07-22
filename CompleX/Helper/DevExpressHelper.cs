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
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX_Library;
using CompleX_Library.Helper;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraVerticalGrid.Rows;

namespace CompleX.Helper
{
    public static class DevExpressHelper
    {

        public static void FilterBaseRow(BaseRow row, string text)
        {
            if (row.HasChildren)
            {
                foreach (BaseRow childRow in row.ChildRows)
                    FilterBaseRow(childRow, text);
                row.Visible = row.ChildRows.FirstVisible != null;

            }
            else
                row.Visible = row.Name.Replace("row", String.Empty).ToLower().Contains(text.ToLower());
        }

        public static void UnMergeMenuStrip(this Bar bar)
        {
          UnMergeMenuStrip(bar,String.Empty); 
        }

        public static void UnMergeMenuStrip(this Bar bar, string keyName)
        {
            for (int i = bar.ItemLinks.Count - 1; i >= 0; i--)
            {
                UnMergeMenuStrip(bar.ItemLinks[i].Item, keyName);
            }
        }

        public static void SetFileImageOnTabPage(XtraMdiTabPage selectedPage)
        {
            if (selectedPage.MdiChild is MainEditForm)
            {
                var icon = File.Exists(((MainEditForm)selectedPage.MdiChild).FileName)
                               ? ImageFunctions.GetFileIcon(((MainEditForm)selectedPage.MdiChild).FileName)
                               : ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(((MainEditForm)selectedPage.MdiChild).FileName),
                                                                             false);
                if (icon != null)
                    selectedPage.Image = icon.ToBitmap();
            }
        }

        public static void SetFormIconOnTabPage(XtraMdiTabPage selectedPage)
        {
            if (selectedPage != null && selectedPage.MdiChild != null && selectedPage.MdiChild.Icon != null)
                selectedPage.Image = ImageFunctions.ResizeImage(selectedPage.MdiChild.Icon.ToBitmap(), 16, 16, true, Color.Transparent, 2.0, 2.0); 
        }

        public static void UnMergeMenuStrip(this BarItem barItem, string keyName)
        {
            if (barItem.Name.StartsWith("CS_AUTOMERGE"+keyName))
            {
                //if (barItem.Manager.Images != null && barItem.ImageIndex >= 0)
                //    ((ImageList)barItem.Manager.Images).Images.RemoveAt(barItem.ImageIndex);
                barItem.Manager.Items.Remove(barItem);
            }
            if (barItem is BarSubItem && ((BarSubItem) barItem).ItemLinks.Count > 0)
            {
                for (int i = ((BarSubItem) barItem).ItemLinks.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        UnMergeMenuStrip(((BarSubItem) barItem).ItemLinks[i].Item,keyName);
                    }
                    catch(Exception ex) 
                    {
                        CompleX_Studio.MessageLog.LogException(ex);
                    }
                }
            }
            
        }

        public static void Merge(this Bar bar, MenuStrip menuStrip, string keyName)
        {
            menuStrip.Visible = false;
            try
            {
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    var baritem = bar.FindItem(item.Text);
                    if (baritem == null)
                        bar.AddItem(ToolStripMenuItemAsBarItem(null, item, keyName));
                }
                AddItemsRecursive(bar, menuStrip.Items, keyName);
            }
            catch (Exception e)
            {
                CompleX_Studio.MessageLog.LogException(e);
            }
        }

        private static BarItem ToolStripMenuItemAsBarItem(object parentBar, ToolStripMenuItem menuItem, string keyName)
        {
            BarItem item = null;
            BarManager manager = null;
            if (parentBar != null && parentBar is Bar)
                manager = ((Bar) parentBar).Manager;         
            else if (parentBar != null && parentBar is BarItem)
                manager = ((BarItem)parentBar).Manager;

            if (menuItem.DropDownItems.Count > 0)
            { 
                // Subitem
                if (parentBar != null && parentBar is Bar)
                    item = ((Bar) parentBar).FindItem(menuItem.Text);                
                if (parentBar != null && parentBar is BarItem)
                    item = ((BarItem) parentBar).FindItem(menuItem.Text);                

                if (item == null)
                {
                    item = new BarSubItem(manager, menuItem.Text) { Name = "CS_AUTOMERGE" + keyName + menuItem.Name };
                }
            } 
            else
            {
                // Item with action
                item = new BarButtonItem(manager,menuItem.Text)
                           {
                               Name = "CS_AUTOMERGE" + keyName + menuItem.Name,
                               ItemShortcut = new BarShortcut(menuItem.ShortcutKeys)
                           };
                item.ItemClick += (sender, args) => menuItem.PerformClick();      

                // Create toolbar if not exists and add the itemaction
                if (item.Manager != null && menuItem.Image != null)
                {
                    var toolBar = item.Manager.Bars[keyName] ?? new Bar{Text = keyName,BarName = keyName, DockStyle = BarDockStyle.Top,Visible = false};
                    if (!item.Manager.Bars.Contains(toolBar))
                        item.Manager.Bars.Add(toolBar);
                    toolBar.AddItem(item);
                }


            }
            
            menuItem.EnabledChanged += ((sender, args) => EnableBarItem(menuItem, manager, item));
            menuItem.VisibleChanged += ((sender, args) => SetBarItemVisibility(menuItem, manager, item));
            menuItem.CheckedChanged += ((sender, args) => CheckBarItem(menuItem, manager, item));

            // -----------------------------------------------------------------------------------------------------------

            item.Tag = menuItem.Tag;
            item.Hint = menuItem.Text;
           
            item.Enabled = menuItem.Enabled;
            CheckBarItem(menuItem, manager, item);
            //TODO: eigentlich muss hier auch der visible status gesetzt werden, doch da leider das ganze menü unsichtbar geschaltet wird, damit es im control niocht zu sehen ist, wären dann alle einträge unsichtbar
            
            if (menuItem.Image != null && item.Manager != null && item.Manager.Images != null)
            {
                ((ImageList) item.Manager.Images).Images.Add(menuItem.Image);
                item.ImageIndex = ((ImageList) item.Manager.Images).Images.Count - 1;
            }

            return item;
        }

        private static void CheckBarItem(ToolStripMenuItem item, BarManager manager, BarItem barItem)
        {
            var barButtonItem = barItem as BarButtonItem;
            if (barButtonItem != null)
            {
                if (manager != null) manager.BeginUpdate();
                if (item.Checked)
                    barButtonItem.ButtonStyle = BarButtonStyle.Check;
                barButtonItem.Down = item.Checked;
                if (manager != null) manager.EndUpdate();
            }
        }

        private static void SetBarItemVisibility(ToolStripItem sender, BarManager manager, BarItem item)
        {
            if (manager != null) manager.BeginUpdate();
            item.Visibility = sender.Visible ? BarItemVisibility.Always : BarItemVisibility.Never;
            if (manager != null) manager.EndUpdate();
        }

        private static void EnableBarItem(ToolStripItem sender, BarManager manager, BarItem item)
        {
            if(manager != null) manager.BeginUpdate();
            item.Enabled = sender.Enabled;
            if (manager != null)  manager.EndUpdate();
        }

        private static void AddItemsRecursive(object parentBar, ToolStripItemCollection itemCollection, string keyName)
        {
            bool beginGroup = false;
            foreach (var item in itemCollection)
            {
                if (!(item is ToolStripSeparator))
                {
                    var barItem = ToolStripMenuItemAsBarItem(parentBar, item as ToolStripMenuItem, keyName);
                    if (parentBar is BarSubItem)
                    {
                        // Adds the real new Link
                        BarItemLink link = ((BarSubItem) parentBar).AddItem(barItem);
                        link.BeginGroup = beginGroup;
                    }

                    if (barItem is BarSubItem)
                    {
                        // Item has subitems and should 
                        AddItemsRecursive(barItem, ((ToolStripMenuItem)item).DropDownItems, keyName);
                    }
                    beginGroup = false;
                }else
                {
                    beginGroup = true;
                }

            }
        }

        public static BarItem FindItem(this BarItem barItem, string caption)
        {
            if (barItem is BarSubItem)
            {
                caption = caption.Replace("&", "");
                foreach (BarItemLink link in ((BarSubItem)barItem).ItemLinks)
                {
                    if (link.Item.Name.ToLower().Equals("i"+caption.ToLower()))
                    {
                        return link.Item; 
                    }
                    if (link.Caption.Replace("&", "").ToLower().Equals(caption.ToLower()))
                    {
                        return link.Item;
                    }
                }
                return null;
            }
            return null;
        }

        public static BarItem FindItem(this Bar bar, string caption)
        {
            caption = caption.Replace("&", "");
            foreach (BarItemLink link in bar.ItemLinks)
            {
               if (link.Item.Name.ToLower().Equals("i" + caption.ToLower()))
               {
                   return link.Item;
               }
               if(link.Caption.Replace("&","").ToLower().Equals(caption.ToLower()))
               {
                   return link.Item;
               }
            }
            return null;
        }


        public static void RemoveCustomEditActions(BarCustomContainerItem subitem, string name)
        {
           // var imgList = (ImageList) subitem.Manager.Images;
            for (int i = subitem.ItemLinks.Count - 1; i >= 0; i--)
            {
                BarItemLink itemLink = subitem.ItemLinks[i];
                if (itemLink.Item.Name.StartsWith(name))
                {
                    //if (itemLink.Item.ImageIndex >= 0 && imgList != null)
                    //    imgList.Images.RemoveAt(itemLink.Item.ImageIndex);
                    subitem.Manager.Items.Remove(itemLink.Item);
                }
            }
        }


        public static IEnumerable<TreeListNode> GetAllNodes(this TreeList tree)
        {
            var result = new List<TreeListNode>();
            foreach (TreeListNode node in tree.Nodes)
            {
                if (node.HasChildren)
                {
                    result.AddRange(GetAllNodes(node));
                }
                else
                {
                    result.Add(node);
                }
            }
            return result;
        }

        public static IEnumerable<TreeListNode> GetAllNodes(this TreeListNode node)
        {
            var result = new List<TreeListNode> { node };
            foreach (TreeListNode listNode in node.Nodes)
            {
                if (listNode.HasChildren)
                    result.AddRange(GetAllNodes(listNode));            
                else
                    result.Add(listNode);     
            }
            return result;
        }



        public static string AsString (this TreeListNode node, bool ignoreFirst)
        {
            bool cancelled = false;
            string result = String.Empty;
            while (!cancelled)
            {
                if (node != null)
                {
                    if(node.ParentNode != null || !ignoreFirst )
                        result = node.GetDisplayText(0).AddDirectorySeparatorChar() + result;
                    if (node.ParentNode != null)
                        node = node.ParentNode;
                    else
                        cancelled = true;
                }
                else
                    cancelled = true;
            }
            return result;
        }


        public static string GetFocusedDataRowItemText(this GridView grid, int columnItemIndex)
        {
            var dataRowView = GetFocusedDataRowView(grid);
            if (dataRowView != null)
            {
                return dataRowView.Row.ItemArray[columnItemIndex].ToString();
            }
            return "";
        }

        public static object GetFocusedDataRowItem(this GridView grid, int columnItemIndex)
        {
            var dataRowView = GetFocusedDataRowView(grid);
            if (dataRowView != null)
            {
                return dataRowView.Row.ItemArray[columnItemIndex];
            }
            return "";
        }

        public static DataRowView GetFocusedDataRowView(this GridView grid)
        {
            var moduleRow = grid.GetFocusedRow(); //as DataSetLicenceModules.ModulesRow;
            if (moduleRow != null)
            {
                var dataRowView = moduleRow as DataRowView;
                if (dataRowView != null)
                {
                    return dataRowView;
                }
            }
            return null;
        }

        public static bool RestoreLayoutFromFile(this GridView gridView, string fileName)
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    gridView.RestoreLayoutFromStream(stream);
                    return true;
                }
            }
            return false;
        }

        public static void SaveLayoutToFile(this GridView gridView, string fileName)
        {
            using (FileStream stream = File.OpenWrite(fileName))
            {
                gridView.SaveLayoutToStream(stream);
            }
        }

        public static List<string> GetLayoutSet(string path)
        {
            var result = new List<string>();
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, "*.xml");
                result.AddRange(files.Select(Path.GetFileNameWithoutExtension));
            }
            return result;
        }
    }
}