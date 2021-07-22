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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX_Library.Helper;
using System.Linq;

namespace CompleX.Helper
{
    public static class PlaceHolder
    {
 
        public static IEnumerable<string> PossiblePlaceHolders
        {
            get
            {
               var result = new List<string> // erstes element muss immer das element sein, welches die die aktuelle datei erstetzt
                                {                                    
                                    "${Item Path}", 
                                    "${Item Directory}",
                                    "${Item Filename}",
                                    "${Item Extension}",
                                    "-",
                                    "${Item FileLastWriteTime}",
                                    "${Item FileLastAccessTime}",
                                    "${Item FileCreationTime}",
                                    "-",
                                    "${Selection}", 
                                    "-",
                                    "${Project Directory}",
                                    "${Project Filename}"
                                };
                return result;
            }
        }

        public static string ReplacePlaceHolder(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                if (CompleX_Studio.CurrentContentEditor != null &&
                    CompleX_Studio.CurrentContentEditor.SelectedContent != null)
                    s = s.Replace("${Selection}", CompleX_Studio.CurrentContentEditor.SelectedContent.ToString());

                s = s.Replace("${Item Path}", CompleX_Studio.CurrentFile);
                s = s.Replace("${Item Directory}", !String.IsNullOrEmpty(CompleX_Studio.CurrentFile) ? Path.GetDirectoryName(CompleX_Studio.CurrentFile).AddDirectorySeparatorChar() : String.Empty);
                s = s.Replace("${Item Filename}", !String.IsNullOrEmpty(CompleX_Studio.CurrentFile) ? Path.GetFileName(CompleX_Studio.CurrentFile) : String.Empty);
                s = s.Replace("${Item Extension}", !String.IsNullOrEmpty(CompleX_Studio.CurrentFile) ? Path.GetExtension(CompleX_Studio.CurrentFile) : String.Empty);

                // FileInformation 
                if(!String.IsNullOrEmpty(CompleX_Studio.CurrentFile)&& File.Exists(CompleX_Studio.CurrentFile))
                {
                    var info = new FileInfo(CompleX_Studio.CurrentFile);
                    s = s.Replace("${Item FileLastWriteTime}", info.LastWriteTime.ToString());
                    s = s.Replace("${Item FileLastAccessTime}", info.LastAccessTime.ToString());
                    s = s.Replace("${Item FileCreationTime}", info.CreationTime.ToString());
                }else
                {
                    s = s.Replace("${Item FileLastWriteTime}", String.Empty);
                    s = s.Replace("${Item FileLastAccessTime}", String.Empty);
                    s = s.Replace("${Item FileCreationTime}", String.Empty);
                }


                var projectExplorer = ApplicationHost.Host.GetService<ProjectExplorer>();
                string projectDir = String.Empty;
                string projectFile = String.Empty;
                if(projectExplorer != null)
                {
                    projectDir = projectExplorer.ProjectDirectory;
                    projectFile = projectExplorer.ProjectFileName;
                }
                s = s.Replace("${Project Directory}", projectDir);
                s = s.Replace("${Project Filename}", projectFile);
                
            }
            return s;
        }

        public static void ShowPopup(Point position, Action<string> action)
        {
            GetPopUpMenu(action,false).Show(position);
        }

        public static ContextMenuStrip GetPopUpMenu(Action<string> action,bool useMouseDown)
        {
            var menu = new ContextMenuStrip
                           {
                               Renderer = new Office2007Renderer.Office2007Renderer()
                           };
            foreach (string s in PossiblePlaceHolders)
            {
                if (s != "-")
                {
                    ToolStripItem item = menu.Items.Add(s);
                    string s1 = s;
                    if (!useMouseDown)
                        item.Click += (sender, args) => action(s1);
                    else
                        item.MouseDown += (sender, args) => action(s1);
                }else
                {
                    menu.Items.Add(new ToolStripSeparator());
                }
            }
            menu.Items.Add(new ToolStripSeparator());
            ToolStripItem testPlaceHolderItem = menu.Items.Add(Resources.TestPlaceholders+"...");
            testPlaceHolderItem.Click += TestPlaceHolders;
            return menu;
        }

        private static void TestPlaceHolders(object sender, EventArgs args)
        {
            var listView = new ListView {View = View.Details, MinimumSize = new Size(400, 200)};

            var header = listView.Columns.Add("PlaceHolder", Resources.PlaceHolder);
            header.Width = listView.Width/2;
            header = listView.Columns.Add("ReplacedValue", Resources.ReplacedValue);
            header.Width = 190;

            foreach (string s in PossiblePlaceHolders.Where(s => s != "-"))
            {
                var item = new ListViewItem(s);
                item.SubItems.Add(ReplacePlaceHolder(s));
                listView.Items.Add(item);
            }
            listView.Dock = DockStyle.Fill;
            var dialog = BaseDialogHelper.CreateBaseDialog(listView);
            dialog.OkBtn.Visible = false;
            dialog.VisibleInTaskBar = true;
            dialog.Show();
        }

        public static void ShowPopup(Action<string> action)
        {
            ShowPopup(Cursor.Position,action); 
        }

        public static string SelectPlaceHolderDialog()
        {
            var dlg = new SelectStringDialog(PossiblePlaceHolders.Where(s => s != "-"),false);
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.SelectedText;
            return String.Empty;
        }


    }
}
