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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Services;
using CompleX_Library;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class FileListControl : HostedControl, IEquatable<FileListControl>
    {
        private readonly ImageListBox listBoxOpenFiles;
        private readonly List<ImageListBoxItem> smallList;

        public FileListControl()
        {
            InitializeComponent();

            lock (this)
            {
                listBoxOpenFiles = new ImageListBox {ImageList = imgMain};
                smallList = new List<ImageListBoxItem>();
                listBoxOpenFiles.DoubleClick += ListBoxOpenFilesOnDoubleClick;
                listBoxOpenFiles.AllowDrop = true;
                listBoxOpenFiles.MouseUp += ListBoxOpenFilesOnMouseUp;
                listBoxOpenFiles.KeyUp += ListBoxOpenFilesOnKeyUp;
                panelHost.Controls.Add(listBoxOpenFiles);
                listBoxOpenFiles.Dock = DockStyle.Fill;
                listBoxOpenFiles.ItemHeight = 16;
                listBoxOpenFiles.SelectionMode = SelectionMode.MultiExtended;
            }

        }

        public IEnumerable<string> SelectedFiles
        {
            get {
                return from object item in listBoxOpenFiles.SelectedItems select GetFileNameByItem(item);
            }
        }

        public string SelectedFileName
        {
            get
            {
                return GetFileNameByItem(listBoxOpenFiles.SelectedItem);
            }
        }

        private static string GetFileNameByItem(object listItem)
        {
            if (listItem != null)
            {
                if (listItem is ImageListBoxItem)
                {
                    if (((ImageListBoxItem)listItem).Tag is MainEditForm)
                    {
                        var form = ((ImageListBoxItem)listItem).Tag as MainEditForm;
                        return form.FileName;
                    }
                    if (((ImageListBoxItem)listItem).Tag is string)
                        return ((ImageListBoxItem)listItem).Tag as string;
                }
                else
                {
                    return listItem.ToString();
                }
            }
            return String.Empty;
        }

        private void ListBoxOpenFilesOnKeyUp(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                foreach (string file in SelectedFiles)
                {
                    if (!String.IsNullOrEmpty(file))
                        FileService.OpenFile(file);
                }
            }

            if (args.KeyCode == Keys.F10 && ModifierKeys == Keys.Shift)
            {
                MenuService.ShowDefaultFileContextMenu(ContextMenuStrip, SelectedFiles.ToArray());
            }
        }


        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            this.CheckInvoke(() =>
                                 {
                                     smallList.Clear();
                                     listBoxOpenFiles.BeginUpdate();
                                     listBoxOpenFiles.Items.Clear();
                                     listBoxOpenFiles.EndUpdate();
                                 });
        }

        /// <summary>
        /// Renames a file in the list. 
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="newName">The new name.</param>
        public void RenameFile(string file, string newName)
        { 
            foreach (var item in listBoxOpenFiles.Items)
            {
                var currentItem = item as ImageListBoxItem;
                if(currentItem != null && currentItem.Tag is string)
                {
                    if (currentItem.Text.Equals(Path.GetFileName(file)) && (string)currentItem.Tag == file )
                    {
                        currentItem.Text = Path.GetFileName(newName);
                        currentItem.Tag = newName;
                        return;
                    }
                }
                else if (currentItem != null && currentItem.Tag is MainEditForm)
                {
                    if (currentItem.Text.Equals(Path.GetFileName(file)) && (((MainEditForm)currentItem.Tag).FileName == newName || ((MainEditForm)currentItem.Tag).FileName == file))
                    {
                        currentItem.Text = Path.GetFileName(newName);
                        ((MainEditForm)currentItem.Tag).FileName = newName;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets all files.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFiles()
        {
            foreach (var item in listBoxOpenFiles.Items)
            {
                var currentItem = item as ImageListBoxItem;
                if (currentItem != null && currentItem.Tag is string)
                    yield return (string) currentItem.Tag;
                
                else if (currentItem != null && currentItem.Tag is MainEditForm)
                    yield return ((MainEditForm) currentItem.Tag).FileName; 
            } 
        }

        /// <summary>
        /// Add files to list
        /// </summary>
        /// <param name="files">The files.</param>
        public void AddFiles(IEnumerable<string> files)
        {
            this.CheckInvoke(() =>
                                 {
                                     listBoxOpenFiles.BeginUpdate();
                                     foreach (string file in files)
                                         AddFile(file);

                                     listBoxOpenFiles.EndUpdate();
                                 });
        }

        /// <summary>
        /// Adds a file to the list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void AddFile(string fileName)
        {
            AddFile(fileName, fileName);
        }


        /// <summary>
        /// Updates list entries for every open file in CS
        /// </summary>
        public void UpdateOpenFiles()
        {
            this.CheckInvoke(() =>
                                 {
                                     listBoxOpenFiles.BeginUpdate();
                                     listBoxOpenFiles.Items.Clear();
                                     foreach (var form in FileService.OpenForms.OfType<MainEditForm>())
                                     {
                                         AddFile(form.FileName, form);
                                     }

                                     listBoxOpenFiles.EndUpdate();
                                 });
        }

        /// <summary>
        /// Sets the list to specifiefd files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void SetFiles(IEnumerable<string> files)
        {
            Clear();
            AddFiles(files);
        }

        private void ListBoxOpenFilesOnDoubleClick(object sender, EventArgs args)
        {
            foreach (string file in SelectedFiles)
            {
                if (!String.IsNullOrEmpty(file))
                    FileService.OpenFile(file);
            }
        }

        private void ListBoxOpenFilesOnMouseUp(object sender, MouseEventArgs args)
        {
            //Display ContextMenu
            if (args.Button == MouseButtons.Right)
            {
                if (ModifierKeys == Keys.Control && SelectedFiles.Count() > 0)
                {
                    MenuService.ShowShellContextMenu(SelectedFiles.ToArray());
                }
                else
                {
                    MenuService.ShowDefaultFileContextMenu(ContextMenuStrip, SelectedFiles.ToArray());
                }
            }
        }



        private void AddFile(string fileName, object tag)
        {
            this.CheckInvoke(() =>
                                 {
                                     int imgIndex = 0;
                                     if (File.Exists(fileName))
                                     {
                                         imgMain.Images.Add(ImageFunctions.GetFileIcon(fileName, false));
                                         imgIndex = imgMain.Images.Count - 1;
                                     }
                                     var item = new ImageListBoxItem(Path.GetFileName(fileName), imgIndex) {Tag = tag};
                                     if (!listBoxOpenFiles.Items.Contains(item))
                                     {
                                         smallList.Add(item);
                                         listBoxOpenFiles.Items.Add(item);
                                     }
                                 });
        }

        private void TextBoxSearchTextChanged(object sender, EventArgs e)
        {
            var items = smallList.Where(s => s.Text.ToLower().Contains(textBoxSearch.Text.ToLower())).Distinct();
            listBoxOpenFiles.BeginUpdate();
            listBoxOpenFiles.Items.Clear();
            foreach (var item in items)
            {
                if (!listBoxOpenFiles.Items.Contains(item))
                    listBoxOpenFiles.Items.Add(item);
            }

            listBoxOpenFiles.EndUpdate();
        }

        public bool Equals(FileListControl other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.listBoxOpenFiles, listBoxOpenFiles) && Equals(other.smallList, smallList) && Equals(other.components, components) && Equals(other.imgMain, imgMain) && Equals(other.panelHost, panelHost) && Equals(other.panel1, panel1) && Equals(other.textBoxSearch, textBoxSearch) && Equals(other.labelSearch, labelSearch);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FileListControl)) return false;
            return Equals((FileListControl) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (listBoxOpenFiles != null ? listBoxOpenFiles.GetHashCode() : 0);
                result = (result*397) ^ (smallList != null ? smallList.GetHashCode() : 0);
                result = (result*397) ^ (components != null ? components.GetHashCode() : 0);
                result = (result*397) ^ (imgMain != null ? imgMain.GetHashCode() : 0);
                result = (result*397) ^ (panelHost != null ? panelHost.GetHashCode() : 0);
                result = (result*397) ^ (panel1 != null ? panel1.GetHashCode() : 0);
                result = (result*397) ^ (textBoxSearch != null ? textBoxSearch.GetHashCode() : 0);
                result = (result*397) ^ (labelSearch != null ? labelSearch.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(FileListControl left, FileListControl right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileListControl left, FileListControl right)
        {
            return !Equals(left, right);
        }
    }
}
