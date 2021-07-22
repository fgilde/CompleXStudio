//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.Dialogs;
using CompleX.Helper;
using CompleX_Library.Interfaces;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class TagEditControl : UserControl, IWizardPageControl
    {
      
        public new Tag Tag { get; set; }


        public TagEditControl()
        {
            InitializeComponent();
            textBoxValue.ContextMenuStrip = PlaceHolder.GetPopUpMenu(s => textBoxValue.Text = s,false);    
        }

        public TagEditControl(Tag tag):this()
        {
            Tag = tag;
        }

        public void Init()
        {
            if (Tag != null)
                InvalidateData(null);
        }

        public void InvalidateData(object prevoiusPageData)
        {
            if (prevoiusPageData != null && prevoiusPageData is Tag)
            {
                Tag = (Tag)prevoiusPageData;
            }

            InfoLabel.Visible = File.Exists(Tag.Helpfile);
            treeViewCategories.Nodes.Clear();
            textBoxValue.Text = Tag.TagValue;
            groupControlValueEdit.Visible = Tag.Endtag;
            if (!Tag.Endtag)
                splitContainerControl1.Dock = DockStyle.Fill;

            bool actionAdd = false;
            foreach (string category in Tag.AttribCategories)
            {
                var node = treeViewCategories.Nodes.Add(category);
                if (category.ToLower().Equals("actions") || category.ToLower().Equals("action"))
                {
                    foreach (TagAttribute tagAttribute in Tag.Attributes.Where(attribute => attribute.IsEventOrAction))
                        node.Nodes.Add(tagAttribute.AtrributeName);
                    actionAdd = true;
                }
            }

            if (!actionAdd)
            {
                var node = treeViewCategories.Nodes.Add("Actions");
                foreach (TagAttribute tagAttribute in Tag.Attributes.Where(attribute => attribute.IsEventOrAction))
                    node.Nodes.Add(tagAttribute.AtrributeName);
            }
            treeViewCategories.Scrollable = true;
            treeViewCategories.SelectedNode = treeViewCategories.Nodes[0];
        }

        public bool ValidatePageChange()
        {
            return Tag != null;
        }

        public object GetData()
        {
            return Tag;
        }

        public void SetData(object data)
        {
            Tag = data as Tag;
        }

        private void treeViewCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string groupName = treeViewCategories.SelectedNode.Text;

            LoadGroup(groupName,treeViewCategories.SelectedNode.Parent != null);
        }

        private void LoadGroup(string groupName, bool isEvent)
        {
            splitContainerControl1.Panel2.Visible = false;
            InfoLabel.Parent = groupControlValueEdit;
            panelEditHost.Controls.Clear();
            InfoLabel.Parent = groupSettings;
            if (!isEvent)
            {       
                groupSettings.Text = Tag.TagName + " - " + groupName;
                var groupAttributes =
                    Tag.Attributes.Where(
                        attribute => attribute.AttribCategoryGroup.Equals(groupName) && !attribute.IsEventOrAction);
                int tmpHeight = 0;
                for (int i = groupAttributes.Count() - 1; i >= 0; i--)
                {
                    TagAttribute attribute = groupAttributes.ToList()[i];
                    var aEdit = new AttributeEditor {Attribute = attribute};
                    aEdit.Init();
                    aEdit.Parent = panelEditHost;
                    aEdit.Dock = DockStyle.Top;
                    aEdit.Height = 30;
                    tmpHeight += aEdit.Height;
                }
                panelEditHost.AutoScroll = true;
                panelEditHost.AutoScrollMinSize = new Size(0, tmpHeight);
            }else
            {
                TagAttribute attribute =
                    Tag.Attributes.FirstOrDefault(
                        tagAttribute =>
                        tagAttribute.AtrributeName.ToLower().Equals(groupName.ToLower()) && 
                        tagAttribute.IsEventOrAction);
                if(attribute != null)
                {
                    groupSettings.Text = Tag.TagName + " - " + attribute.AtrributeName;
                    var eEdit = new EventEditControl {Attribute = attribute};
                    eEdit.Init();
                    eEdit.Parent = panelEditHost;
                    eEdit.Dock = DockStyle.Fill;
                }
                panelEditHost.AutoScroll = false;

            }
            splitContainerControl1.Panel2.Visible = true;
        }

        private void InfoLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var infoDlg = new SimpleBrowserDialog(Tag.Helpfile);
            infoDlg.Text = Tag.TagName.ToUpper();
            infoDlg.Show();
        }

        private void textBoxValue_TextChanged_1(object sender, EventArgs e)
        {
            Tag.TagValue = textBoxValue.Text;
        }

    }
}
