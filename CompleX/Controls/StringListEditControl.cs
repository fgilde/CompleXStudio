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
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.Properties;

namespace CompleX.Controls
{
    public partial class StringListEditControl : UserControl
    {

        public void Add(string s)
        {
            listBox.Items.Add(s);
        }

        public IEnumerable<string> StringList
        {
            get
            {
                var result = new List<string>();
                result.Clear();
                foreach (var item in listBox.Items)
                    result.Add(item.ToString());
                return result;
            }set
            {
                UpdateListBox(value);
            }
        }

        private void UpdateListBox(IEnumerable<string> list)
        {
            listBox.BeginUpdate();
            listBox.Items.Clear();
            if (list != null)
                foreach (string s in list)
                {
                    listBox.Items.Add(s); 
                }
            listBox.EndUpdate();
        }

        public StringListEditControl()
        {
            InitializeComponent();
            toolStrip1.Renderer = new Office2007Renderer.Office2007Renderer();
        }
        public StringListEditControl(IEnumerable<string> strings):this()
        {
            UpdateListBox(strings);
        }

        private void listBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                DeleteSelected();
            }
        }

        public void DeleteSelected()
        {
            if (listBox.SelectedItem != null)
            {
                listBox.Items.Remove(listBox.SelectedItem);
            }
        }

        public void DeleteAll()
        {
            listBox.Items.Clear();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            string ext = InputDlg.Execute(toolStripButtonAdd.Text, Resources.Extension);
            if (!string.IsNullOrEmpty(ext))
            {
                Add(ext);
            }
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            DeleteSelected();
        }

        private void toolStripButtonDelAll_Click(object sender, EventArgs e)
        {
            DeleteAll(); 
        }
    }
}