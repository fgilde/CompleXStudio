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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Helper;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_Types;
using DevExpress.XtraBars;

namespace CompleX.Services
{

    public enum MergeOrder
    {
        Default,
        CustomFirst
    }

    public static class MenuService
    {

        public static void ShowShellContextMenu(params string[] fileNames)
        {
            var arrFi = new FileInfo[fileNames.Count()];
            for (int i = 0; i < fileNames.Count(); i++)
            {
                arrFi[i] = new FileInfo(fileNames[i]);
            }
            var ctxMnu = new ShellContextMenu();
            ctxMnu.ShowContextMenu(arrFi, Cursor.Position);
        }

        public static void ShowDefaultFileContextMenu(ContextMenuStrip menuToMerge,MergeOrder mergeOrder, params string[] fileNames)
        {
            ShowDefaultFileContextMenu(null, menuToMerge, mergeOrder,true, fileNames);
        }

        public static void ShowDefaultFileContextMenu(ContextMenuStrip menuToMerge,params string[] fileNames)
        {
            ShowDefaultFileContextMenu(null, menuToMerge,MergeOrder.Default, true, fileNames);
        }

        public static void ShowDefaultFileContextMenu(string fileName, ContextMenuStrip menuToMerge)
        {
            ShowDefaultFileContextMenu(fileName, null, menuToMerge);
        }

        public static void ShowDefaultFileContextMenu(string fileName, Action itemClicked)
        {
            ShowDefaultFileContextMenu(fileName, itemClicked, null);
        }

        public static void ShowDefaultFileContextMenu(string fileName)
        {
            ShowDefaultFileContextMenu(fileName, null, null);
        }

        public static void ShowDefaultFileContextMenu(string fileName, Action itemClicked, ContextMenuStrip menuToMerge)
        {
            ShowDefaultFileContextMenu(itemClicked, menuToMerge,MergeOrder.Default, true, fileName);
        }

        public static void ShowDefaultFileContextMenu(Action itemClicked, ContextMenuStrip menuToMerge, MergeOrder mergeOrder,bool allowRename, params string[] fileNames)
        {
            ContextMenuStrip contextMenu;
            if (mergeOrder == MergeOrder.Default) // Default Merge Order, first default context than menu to merge
            {
                contextMenu = GetDefaultFileContext(allowRename,fileNames);
                contextMenu.Renderer = new Office2007Renderer.Office2007Renderer();
                if (itemClicked != null)
                {
                    foreach (ToolStripMenuItem item in contextMenu.Items.OfType<ToolStripMenuItem>())
                        item.Click += (sender, args) => itemClicked();
                }

                if (menuToMerge != null)
                {
                    if (contextMenu.Items.Count > 0)
                        contextMenu.Items.Add(new ToolStripSeparator());
                    var mergeMenu = menuToMerge.Clone();
                    ToolStripManager.Merge(mergeMenu, contextMenu);
                }
            }
            else  // Custom First Merge Order, first menu to merge items than default Items 
            {
                contextMenu = new ContextMenuStrip();
                var tmpcontextMenu = GetDefaultFileContext(allowRename, fileNames);
                if (menuToMerge != null)
                {
                    var mergeMenu = menuToMerge.Clone();
                    ToolStripManager.Merge(mergeMenu, contextMenu);
                    if (tmpcontextMenu.Items.Count > 0)
                        contextMenu.Items.Add(new ToolStripSeparator());
                }

                if (itemClicked != null)
                {
                    foreach (ToolStripMenuItem item in contextMenu.Items.OfType<ToolStripMenuItem>())
                        item.Click += (sender, args) => itemClicked();
                }
                ToolStripManager.Merge(tmpcontextMenu, contextMenu);
            }

            contextMenu.Renderer = new Office2007Renderer.Office2007Renderer();
            contextMenu.Show(Cursor.Position);   
        }

        public static ContextMenuStrip GetDefaultFileContext(bool allowRename, params string[] fileNames)
        {
            if(fileNames.FirstOrDefault(File.Exists) == null)
                return new ContextMenuStrip();

            var contextMenuStrip = new ContextMenuStrip();

            var projectExplorer = ApplicationHost.Host.GetService<ProjectExplorer>();
            if (projectExplorer != null && projectExplorer.IsProjectOpen && fileNames.Except(projectExplorer.ProjectFiles).Count() > 0)
            {
                contextMenuStrip.Items.Add(new ToolStripMenuItem(String.Format(Resources.AddToCurrentProject,Path.GetFileName(projectExplorer.ProjectFileName)), null, (sender, args) => projectExplorer.AddExistingItems(fileNames)));                
            }

            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.ShellContextMenu, Resources.ImgWindows, (sender, args) => ShowShellContextMenu(fileNames)){ShortcutKeyDisplayString = Resources.CtrlRight});

            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.OpenFile, Resources.ImgOpen, ((sender, args) => FileService.LoadFiles(fileNames))));

            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.OpenFileWithDefaultApp, null, ((sender, args) => {foreach (string fileName in fileNames) Process.Start(fileName);})));
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.OpenWindowsFolder, Resources.ImgOpenWindowsFolder, ((sender, args) =>
            {
                foreach (string fileName in fileNames)
                         Process.Start(Path.GetDirectoryName(fileName)); })));


            contextMenuStrip.Items.Add(new ToolStripSeparator());
            
            if (allowRename)
                contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.Rename, null, ((sender, args) => {foreach (string fileName in fileNames)FileService.RenameFile(fileName); })));
            
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.Delete, Resources.anhang_loeschen_16, ((sender, args) =>
            {
                foreach (string fileName in fileNames)
                         FileService.DeleteFileWithConfirmation(fileName,@"DSA_DELETE_FILE_MS",false); })));

            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.CopyPathToClipboard, Resources.ImgCopy, ((sender, args) => {
                Clipboard.Clear();
                foreach (string fileName in fileNames)
                {
                    if (!String.IsNullOrEmpty(Clipboard.GetText()))
                         Clipboard.SetText(Clipboard.GetText()+Environment.NewLine+fileName);
                    else
                         Clipboard.SetText(fileName);
                }
            })));


            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.InsertFileContent, null, (sender, args) =>
            {
                if (CompleX_Studio.CurrentContentEditor != null)
                {
                    foreach (string fileName in fileNames)
                    {
                        var streamReader = new StreamReader(fileName);
                        string insert = streamReader.ReadToEnd();
                        CompleX_Studio.CurrentContentEditor.Insert(insert);
                        CompleX_Studio.CurrentContentEditor.Insert(Environment.NewLine);
                        streamReader.Close();
                    }
                }
            }));

            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add(new ToolStripMenuItem(Resources.Properties, Resources.ImgInfoImage, ((sender, args) => { foreach (string fileName in fileNames) FileSystemUtils.ShowProperties(fileName);})));

            return contextMenuStrip;
        }


        public static ContextMenuStrip Clone(this ContextMenuStrip menuStrip)
        {
            var result = new ContextMenuStrip();
            IEnumerable<ToolStripItem> cloneStripItemCollection = menuStrip.Items.Clone();
            foreach (ToolStripItem item in cloneStripItemCollection)
            {
                result.Items.Add(item);
            }
            return result;
        }

        public static ToolStripItem Clone(this ToolStripItem item)
        {
            if (item is ToolStripMenuItem)
                return new ToolStripMenuItem(item.Text, item.Image, (sender, args) => item.PerformClick()) 
                { Name = "Copy" + item.Name, 
                 ShortcutKeyDisplayString = ((ToolStripMenuItem)item).ShortcutKeyDisplayString, 
                 ShortcutKeys = ((ToolStripMenuItem)item).ShortcutKeys, 
                 ShowShortcutKeys = ((ToolStripMenuItem)item).ShowShortcutKeys,
                 Enabled = item.Enabled,
                };
            if (item is ToolStripSeparator)
                return new ToolStripSeparator();
            return null;
        }

        public static IEnumerable<ToolStripItem> Clone(this ToolStripItemCollection collection)
        {
            var itemList = new List<ToolStripItem>();
            foreach (ToolStripItem item in collection)
            {
                var newItem = item.Clone();
                itemList.Add(newItem);
                if ((item is ToolStripMenuItem) && ((ToolStripMenuItem)item).DropDownItems.Count > 0)
                {
                    ToolStripItemCollection itemCollection = ((ToolStripMenuItem)item).DropDownItems;
                    ((ToolStripMenuItem)newItem).DropDownItems.AddRange(Clone(itemCollection).ToArray());
                }
            }
            return itemList;
        }
        public static IEnumerable<ToolStripItem> Clone(this IEnumerable<ToolStripItem> collection)
        {
            var itemList = new List<ToolStripItem>();

            foreach (var item in collection)
            {
                var newItem = item.Clone();
                itemList.Add(newItem);
                if ((item is ToolStripMenuItem) && ((ToolStripMenuItem)item).DropDownItems.Count > 0)
                {
                    ToolStripItemCollection itemCollection = ((ToolStripMenuItem)item).DropDownItems;
                    ((ToolStripMenuItem)newItem).DropDownItems.AddRange(Clone(itemCollection).ToArray());
                }
            }
            return itemList;
        }


        /// <summary>
        /// Merges a given menu to CompleX MainMenu.
        /// </summary>
        /// <param name="menuStrip">The menu strip.</param>
        public static void MergeMenu(MenuStrip menuStrip)
        {
            if (menuStrip != null)
            {
                CompleX_Studio.Instance.MainMenuBar.Merge(menuStrip, menuStrip.Name);
                MoveMenuItemLastPosition(CompleX_Studio.Instance.iHelp);
                MoveMenuItemLastPosition(CompleX_Studio.Instance.iFile.ItemLinks,CompleX_Studio.Instance.iExit);
            }
        }

        /// <summary>
        /// Unmerges a given Menu from the CompleX MainMenu
        /// </summary>
        /// <param name="menuStrip">The menu strip.</param>
        public static void UnMergeMenu(MenuStrip menuStrip)
        {
            if (menuStrip != null)
                CompleX_Studio.Instance.MainMenuBar.UnMergeMenuStrip(menuStrip.Name);
        }


        /// <summary>
        /// Moves a Item from the MainMenu to the Last possible Position
        /// </summary>
        /// <param name="item">The item.</param>
        public static void MoveMenuItemLastPosition(BarItem item)
        {
            MoveMenuItemLastPosition(CompleX_Studio.Instance.MainMenuBar.ItemLinks,item);     
        }

        /// <summary>
        /// Moves a Item from the MainMenu to the Last possible Position
        /// </summary>
        /// <param name="item">The item.</param>
        public static void MoveMenuItemLastPosition(BarItemLinkCollection itemlinks, BarItem item )
        {
           // CompleX_Studio.Instance.iFile.ItemLinks
            foreach (BarItemLink link in itemlinks)
            {
                if (link.Item.Equals(item))
                {
                    itemlinks.Remove(link);
                    break;
                }
            }
            itemlinks.Add(item); 
        }

        internal static Bar GetToolBar(string name, string text)
        {
            var toolBar = CompleX_Studio.Instance.barManager.Bars[name] ?? new Bar
            {
                Text = text,
                BarName = name,
                DockStyle = BarDockStyle.Top,
            };
            return toolBar;
        }

        public static void UpdateEditMenu()
        {
            DevExpressHelper.RemoveCustomEditActions(CompleX_Studio.Instance.iEdit, "CS_CustomAction");

            if (CompleX_Studio.CurrentContentEditor != null && CompleX_Studio.CurrentContentEditor is IEditFeatures)
            {
                var editFeatures = CompleX_Studio.CurrentContentEditor as IEditFeatures;
                var itemActions = editFeatures.GetCustomActions();
                if (itemActions != null && itemActions.Count() > 0)
                {
                    CompleX_Studio.Instance.barManager.BeginUpdate();

                    int index = 0;
                    var toolBar = CompleX_Studio.Instance.barManager.Bars["CS_EditBar"] ?? new Bar
                    {
                        Text = CompleX_Studio.Instance.iEdit.Caption,
                        BarName = "CS_EditBar",
                        DockStyle = BarDockStyle.Top,
                    };
                    CompleX_Studio.Instance.barManager.Bars.Add(toolBar);
                    foreach (ItemAction action in itemActions)
                    {
                        var item = new BarButtonItem(CompleX_Studio.Instance.barManager, action.Caption)
                        {
                            Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                            Name = "CS_CustomAction_" + index,
                        };
                        ItemAction action1 = action;
                        item.ItemClick += (sender, e) => action1.Action();
                        if (action.Image != null)
                        {
                            item.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(action.Image);
                        }
                        item.ItemShortcut = new BarShortcut(action.ShortCut);
                        CompleX_Studio.Instance.iEdit.AddItem(item);
                        CompleX_Studio.Instance.iEdit.ItemLinks[CompleX_Studio.Instance.iEdit.ItemLinks.Count - 1].BeginGroup = action.BeginGroup;
                        toolBar.AddItem(item);
                        index++;
                    }
                    CompleX_Studio.Instance.barManager.EndUpdate();
                }
            }
        }


        public static void UpdateToolsMenu()
        {
            DevExpressHelper.RemoveCustomEditActions(CompleX_Studio.Instance.iTools, "CS_ToolsAction");
            DevExpressHelper.RemoveCustomEditActions(CompleX_Studio.Instance.iEditWith, "CS_ToolsAction");
            
            CompleX_Studio.Instance.barManager.BeginUpdate();

            #region Add tools from data directory

            int index = 0;
            var toolBar = GetToolBar("CS_Tools", "Tools");
            CompleX_Studio.Instance.barManager.Bars.Add(toolBar);

            var usersDirectories = Settings.Get<List<string>>("ToolsDirectoryList") ?? new List<string>();
            var directories = new List<string>();
            directories.AddRange(usersDirectories);
            directories.Add(Pathes.ToolsPath); // Denn complex tool pfad immer durchsuchen
            int keyIndex = 0;
            foreach (string dir in directories)
            {
                var tools = Directory.GetFiles(dir, "*.exe");

                index = 0;
                foreach (string tool in tools)
                {
                    var item = new BarButtonItem(CompleX_Studio.Instance.barManager, Path.GetFileNameWithoutExtension(tool))
                    {
                        Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                        Name = "CS_ToolsAction_" + keyIndex,
                    };

                    string name = tool;
                    item.Caption = "&" + item.Caption;
                    item.ItemClick += (sender, e) => Process.Start(name, CompleX_Studio.CurrentFile);
                    item.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(ImageFunctions.GetFileIcon(tool, false));

                    Keys sc = Keys.Control | Keys.Shift | (Keys)48 + keyIndex;
                    int shortcut = Convert.ToInt32(sc);
                    item.ItemShortcut = new BarShortcut((Shortcut)shortcut);
                    CompleX_Studio.Instance.iTools.AddItem(item);
                    CompleX_Studio.Instance.iTools.ItemLinks[CompleX_Studio.Instance.iTools.ItemLinks.Count - 1].BeginGroup = index == 0;
                    toolBar.AddItem(item);
                    index++;
                    keyIndex++;
                }
            }

            #endregion

            #region Add custom external tools
            
            var externalTools = Settings.Get<List<ExternalTool>>("ExternalTools");
            if (externalTools != null && externalTools.Count > 0)
            {
                index = 0;
                int editWithCunt = 0;
                toolBar = GetToolBar("CS_Ext_Tools", "Custom Tools");
                CompleX_Studio.Instance.barManager.Bars.Add(toolBar);
                foreach (ExternalTool tool in externalTools)
                {
                    if (tool.FileExtensions == null || tool.FileExtensions.Count() <= 0 || (Extensions.ExtensionIsAllowed(tool.FileExtensions, CompleX_Studio.CurrentFile) && !String.IsNullOrEmpty(CompleX_Studio.CurrentFile)))
                    {
                        var item = new BarButtonItem(CompleX_Studio.Instance.barManager, tool.Name)
                        {
                            Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                            Name = "CS_ToolsAction_ext" + index,
                        };

                        ExternalTool externalTool = tool;
                        item.ItemClick += (sender, e) => CompleX_Studio.StartExternalTool(externalTool);
                        if (File.Exists(tool.Command))
                        {
                            item.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(ImageFunctions.GetFileIcon(tool.Command, false));
                            toolBar.AddItem(item);
                        }
                        item.ItemShortcut = new BarShortcut(tool.Shortcut);
                        CompleX_Studio.Instance.iTools.AddItem(item);
                        CompleX_Studio.Instance.iTools.ItemLinks[CompleX_Studio.Instance.iTools.ItemLinks.Count - 1].BeginGroup = index == 0;

                        // Wenn das tool zum bearbeiten mit genutzt werden kann, dann in der edit with list zeigen
                        if (tool.ReloadFileAfterClose && tool.Argument.Contains(PlaceHolder.PossiblePlaceHolders.First()) && File.Exists(CompleX_Studio.CurrentFile))
                        {
                            CompleX_Studio.Instance.iEditWith.AddItem(item);
                            CompleX_Studio.Instance.iEditWith.ItemLinks[CompleX_Studio.Instance.iEditWith.ItemLinks.Count - 1].BeginGroup = editWithCunt == 0;
                            editWithCunt++;
                        }
                        
                        index++;
                    }
                }
            }

            #endregion

            #region Add Method to change tools and Options

            var page =
                ApplicationHost.Host.GetServiceById<ISettingsPage>(new Guid("ACD44352-89AA-4D29-9E9C-2E2B29070452"));

            if (page != null)
            {
                var editItem = new BarButtonItem(CompleX_Studio.Instance.barManager, Resources.ExternalToolsCaption)
                {
                    Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                    Name = "CS_ToolsAction_ext" + index,
                };

                editItem.ItemClick += ((sender, args) => CompleX_Studio.ShowOptionPage(page));

                editItem.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(page.Image);

                CompleX_Studio.Instance.iTools.AddItem(editItem);
                CompleX_Studio.Instance.iTools.ItemLinks[CompleX_Studio.Instance.iTools.ItemLinks.Count - 1].BeginGroup = true;

            }


            var optionsItem = new BarButtonItem(CompleX_Studio.Instance.barManager, Resources.Options + "...")
            {
                Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                Name = "CS_ToolsAction_ext" + index,
            };

            optionsItem.ItemClick += ((sender, args) => CompleX_Studio.Instance.ShowOptions());

            optionsItem.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(Resources.einstellungen_16);
            CompleX_Studio.Instance.iTools.AddItem(optionsItem);


            #endregion

            CompleX_Studio.Instance.barManager.EndUpdate();
        }

        public static void UpdateInsertMenu()
        {
            DevExpressHelper.RemoveCustomEditActions(CompleX_Studio.Instance.iInsert, "CS_Insert");
            if (CompleX_Studio.CurrentContentEditor == null) return;
            int index = 0;
            var toolBar = GetToolBar("CS_Inserters", "Insert");
            CompleX_Studio.Instance.barManager.Bars.Add(toolBar);
            var inserters = ApplicationHost.GetModulesByFileName<IInserter>(CompleX_Studio.CurrentFile);
            foreach (IInserter inserter in inserters)
            {
                if (inserter.SupportedFileExtensions == null || inserter.SupportedFileExtensions.Count() <= 0 || (Extensions.ExtensionIsAllowed(inserter.SupportedFileExtensions, CompleX_Studio.CurrentFile) && !String.IsNullOrEmpty(CompleX_Studio.CurrentFile)))
                {
                    var item = new BarButtonItem(CompleX_Studio.Instance.barManager, inserter.NameOfObjectToInsert)
                    {
                        Id = CompleX_Studio.Instance.barManager.GetNewItemId(),
                        Name = "CS_Insert" + index,
                    };

                    IInserter tmpInserter = inserter;

                    item.ItemShortcut = new BarShortcut(inserter.ShortCutKey1,inserter.ShortCutKey2);
                    item.ItemClick += (sender, e) => CompleX_Studio.Insert(tmpInserter);
                    if (tmpInserter.Image != null)
                    {
                        item.ImageIndex = CompleX_Studio.Instance.imageListMain.AddImage(tmpInserter.Image);
                        toolBar.AddItem(item);
                    }
                    CompleX_Studio.Instance.iInsert.AddItem(item);
                    index++;
                }
            }           
            
        }

    }
}
