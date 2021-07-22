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
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CompleX;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using DOL.DHtml.DHtmlParser;

namespace CompleX_ToolWindows
{
    [Export(typeof(IToolWindow))]
    public partial class HtmlDomExplorer : UserControl,IToolWindow
    {
        private DHtmlDocument htmlDoc;

        public HtmlDomExplorer()
        {
            InitializeComponent();
        }

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("424704DC-EF70-4A74-93CC-A353FC635137"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "HTML Document Explorer"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }
        }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
        public bool Initialize()
        {
            ComplexEvents.Default.FileChanged += DefaultOnToolWindowChanged;
            return true;
        }

        private void DefaultOnToolWindowChanged(object sender, EventArgs args)
        {
            if(Visible)
              LoadDom();
        }



        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion

        #region Implementation of IToolWindow

        public Icon Image
        {
            get { return Resource.baum_aufklappen; }
        }

        #endregion

        public void LoadDom()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    m_htmlTreeView.Nodes.Clear();
                    if (CompleX_Studio.CurrentContentEditor != null && CompleX_Studio.CurrentContentEditor.Content is string)
                    {
                        LoadDom((string)CompleX_Studio.CurrentContentEditor.Content);
                    }
                }));
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void LoadDom(string html)
        {         
            htmlDoc = new DHtmlDocument(html);
            var builder = new StringBuilder();
            htmlDoc.Dump(builder, "");
            Debug.Write("\n" + builder);

            CreateHtmlTree();
        }


        private void refreshDOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDom();
        }

        private void HtmlDomExplorer_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible)
               LoadDom();
        }


        /////////////////////////////////////////////////////////////////////////////////
        private void CreateHtmlTree()
        {
            m_htmlTreeView.SuspendLayout();
            m_htmlTreeView.Nodes.Clear();
            var root = new TreeNode("HTML Tree");


            foreach (DOL.DHtml.DHtmlParser.Node.DHtmlNode child in htmlDoc.Nodes)
                CreateHtmlTreeNode(root, child);

            m_htmlTreeView.Nodes.Add(root);
            m_htmlTreeView.ResumeLayout();
        }

        /////////////////////////////////////////////////////////////////////////////////
        private static void CreateHtmlTreeNode(TreeNode parent, DOL.DHtml.DHtmlParser.Node.DHtmlNode node)
        {
            var treeNode = new TreeNode("") {Tag = node};
            parent.Nodes.Add(treeNode);


            var text = node as DOL.DHtml.DHtmlParser.Node.DHtmlText;
            if (text != null)
            {
                treeNode.Text = text.IsWhiteSpace ? "White Space" : text.Text;
                return;
            }

            var element = node as DOL.DHtml.DHtmlParser.Node.DHtmlElement;
            if (element != null)
            {
                treeNode.Text = element.Tag;
                foreach (DOL.DHtml.DHtmlParser.Node.DHtmlNode child in element.Nodes)
                    CreateHtmlTreeNode(treeNode, child);
                return;
            }

            var style = node as DOL.DHtml.DHtmlParser.Node.DHtmlStyle;
            if (style != null)
            {
                treeNode.Text = style.Tag;
                return;
            }

            var pi = node as DOL.DHtml.DHtmlParser.Node.DHtmlProcessingInstruction;
            if (pi != null)
            {
                treeNode.Text = pi.Value;
                return;
            }

            var script = node as DOL.DHtml.DHtmlParser.Node.DHtmlScript;
            if (script != null)
            {
                treeNode.Text = script.Tag;
                return;
            }

            var comment = node as DOL.DHtml.DHtmlParser.Node.DHtmlComment;
            if (comment != null)
            {
                treeNode.Text = comment.Comment;
                return;

            }

            return;
        }

        private void m_htmlTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            CompleX_Studio.InspectObject(e.Node.Tag);

            //DOL.DHtml.DHtmlParser.Node.DHtmlNode node = e.Node.Tag as DOL.DHtml.DHtmlParser.Node.DHtmlNode;
            //if (node != null)
            //    m_textBox.Text = node.HTML;
            //else m_textBox.Text = "";

            //if (m_selectElement != null)
            //    m_selectElement.Attributes.RemoveAt(m_selectElement.Attributes.Count - 1);

            //m_selectElement = e.Node.Tag as DOL.DHtml.DHtmlParser.Node.DHtmlElement;
            //if (m_selectElement != null)
            //{
            //    m_propertyGrid.SelectedObject = m_selectElement;
            //    m_selectElement.Attributes.Add(new DOL.DHtml.DHtmlParser.DHtmlAttribute("style", "border:5px solid #FF0000"));

            //    StringBuilder builder = new StringBuilder("Selector Match:\r\n");
            //    for (int selectorIndex = 0, selectorCount = m_selectorList.Count; selectorIndex < selectorCount; ++selectorIndex)
            //    {
            //        DOL.DHtml.DCssResolver.DCssSelector selector = m_selectorList[selectorIndex];
            //        if (selector.HasPseudo == false && selector.IsMatching(m_selectElement) == true)
            //        {
            //            builder.Append(selector.CSS);
            //            builder.Append("\r\n");
            //        }
            //    }

            //    m_selectorTextBox.Text = builder.ToString();
            //}

            //htmlDoc.Save(m_path + ".temp.htm");
            //try
            //{
            //    m_webBrowser.AllowNavigation = true;
            //    m_webBrowser.Navigate(m_path + ".temp.htm");
            //}
            //catch
            //{
            //}

        }

    }
}
