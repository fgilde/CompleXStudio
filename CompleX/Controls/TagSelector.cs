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
using CompleX_Library.Interfaces;
using CompleX_Settings.Constants;
using CompleX_Types;

namespace CompleX.Controls
{
    public delegate void OnSelectedTagChange(Tag tag);

    public partial class TagSelector : UserControl, IWizardPageControl
    {
        private List<string> tagitems;
        private string[] hidden;
        public new event EventHandler DoubleClick
        {
            add { tagListBox.DoubleClick += value;
                treeViewLanguages.DoubleClick += value;
            }
            remove { tagListBox.DoubleClick -= value;
                    treeViewLanguages.DoubleClick -= value;
            }
        }

        public new event MouseEventHandler MouseDoubleClick
        {
            add { tagListBox.MouseDoubleClick += value;
            treeViewLanguages.MouseDoubleClick += value;
            }
            remove { tagListBox.MouseDoubleClick -= value;
            treeViewLanguages.MouseDoubleClick -= value;
            }
        }


        public event OnSelectedTagChange SelectedTagChange;
        public event OnSelectedTagChange TagChoosed;

        private void InvokeTagChoosed(Tag tag)
        {
            OnSelectedTagChange choosed = TagChoosed;
            if (choosed != null) choosed(tag);
        }

        private void InvokeSelectedTagChange()
        {
            OnSelectedTagChange change = SelectedTagChange;
            if (change != null) change(SelectedTag);
        }

        internal Tag SelectedTag
        {
            get
            {
                return GetSelectedTag();
            }
        }

        public TagSelector()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            treeViewLanguages.Nodes.Clear();
            hidden = new[] {".svn"};
            tagitems = new List<string>();
            if (Directory.Exists(Pathes.ApplicationPath + Pathes.TAGLIBARY)) 
            {
                var languages = Directory.GetDirectories(Pathes.ApplicationPath + Pathes.TAGLIBARY).OrderBy(s => s);
                foreach (var language in languages)
                {
                    if (!hidden.Contains(Path.GetFileName(language)))
                    {
                        treeViewLanguages.Nodes.Add(Path.GetFileName(language));
                    }
                }
            }
        }

        public void InvalidateData(object prevoiusPageData)
        {
            return;
        }

        public bool ValidatePageChange()
        {
            return SelectedTag != null;
        }

        public object GetData()
        {
            return SelectedTag;
        }

        public void SetData(object data)
        {
            return;
        }

        private void TreeViewLanguagesAfterSelect1(object sender, TreeViewEventArgs e)
        {
            tagListBox.BeginUpdate();
            tagListBox.Items.Clear();
            tagitems.Clear();
            textEditSearch.Text = String.Empty;

            foreach (var s in CompleX_Types.Tag.GetAvailableTags(treeViewLanguages.SelectedNode.Text))
            {
                tagListBox.Items.Add(s);
                tagitems.Add(s);
            }
            
            tagListBox.EndUpdate();
        }

        private Tag GetSelectedTag()
        {
            if (treeViewLanguages.SelectedNode != null && !String.IsNullOrEmpty(treeViewLanguages.SelectedNode.Text) && tagListBox.SelectedItem != null)
            {
                TagLanguage language = treeViewLanguages.SelectedNode.Text.AsTagLanguage();
                string tag = tagListBox.SelectedItem.ToString();
                return TagFactory.CreateTag(language,tag,true);
            }
            return null;
        }

        private void TagListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (tagListBox.SelectedItem != null)
                InvokeSelectedTagChange();
        }

        private void TextEditSearchEditValueChanged(object sender, EventArgs e)
        {
            var items = tagitems.Where(s => s.ToLower().Contains(textEditSearch.Text.ToLower()));
            tagListBox.BeginUpdate();
            tagListBox.Items.Clear();
            foreach (string s in items)
                tagListBox.Items.Add(s);
            
            tagListBox.EndUpdate();
        }

        private void TagListBoxKeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (tagListBox.SelectedItem != null)
                    InvokeTagChoosed(SelectedTag);
            }
        }

        private void TagListBoxDoubleClick(object sender, EventArgs e)
        {
            if (tagListBox.SelectedItem != null)
                InvokeTagChoosed(SelectedTag);
        }

        private void TextEditSearchKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && tagListBox.SelectedIndex > 0)
                tagListBox.SelectedIndex--;
            if (e.KeyCode == Keys.Down && tagListBox.SelectedIndex < tagListBox.Items.Count-1)
                tagListBox.SelectedIndex++;
        }



    }
}
