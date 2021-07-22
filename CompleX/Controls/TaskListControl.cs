//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CompleX_Library.Helper;
using CompleX_Settings;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class TaskListControl : UserControl
    {
        //NOTE: To Style default menus in dataview use Property MenuManager
        private BindingList<TaskListEntry> taskList;
        private string currentFileName;

        public TaskListControl()
        {
            InitializeComponent();
            taskList = new BindingList<TaskListEntry>();
            taskList.AddNew();
            gridControl.DataSource = taskList;
            LoadTasks();
        }

        public bool LoadTasks()
        {
            return LoadTasks(Settings.Path + "Default TaskList.tskl");            
        }

        public bool LoadTasks(string fileName)
        {
            Save();
            currentFileName = fileName;

            var taskListFile = new TaskListFile {File = fileName, FileName = Path.GetFileNameWithoutExtension(fileName)};
            if (!comboBoxSource.Properties.Items.Contains(taskListFile))
                comboBoxSource.Properties.Items.Add(taskListFile);
            comboBoxSource.SelectedItem = taskListFile;

            if (!SerializationHelper.TryBinaryDeserialize(fileName, out taskList))
            {
                taskList = new BindingList<TaskListEntry>();
                RefreshData();
                return false;
            }
            RefreshData();
            return true;
        }

        private void RefreshData()
        {
            gridControl.DataSource = taskList;
        }

        public void Save()
        {
            if (!String.IsNullOrEmpty(currentFileName))
                Save(currentFileName);
        }

        public void Save(string filename)
        {
            if(File.Exists(filename))
                File.Delete(filename);
            if (taskList.Count > 0)
                taskList.TryBinarySerialize(filename);
        }



        private void ComboBoxSourceSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = comboBoxSource.SelectedItem as TaskListFile;
            if (selected != null && selected.File != currentFileName)
                LoadTasks(selected.File);
        }

        private void BarButtonItem1ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            taskList.AddNew();
        }

        private void GridView1KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                var entry = gridView1.GetFocusedRow() as TaskListEntry;
                if (entry != null)
                    taskList.Remove(entry);
            }
        }
    }

    class TaskListFile : IEquatable<TaskListFile>
    {
         public string FileName;
         public string File;

        public bool Equals(TaskListFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.File, File);
        }

        public override string ToString()
         {
             return FileName;
         }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TaskListFile)) return false;
            return Equals((TaskListFile) obj);
        }

        public override int GetHashCode()
        {
            return (File != null ? File.GetHashCode() : 0);
        }
    }

}
