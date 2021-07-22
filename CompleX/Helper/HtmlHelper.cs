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
using System.Linq;
using System.Windows.Forms;
using DOL.DHtml.DCssResolver;

namespace CompleX.Helper
{
    public class HtmlHelper
    {
        /////////////////////////////////////////////////////////////////////////////////
        public static void CreateCssTree(string html, TreeView cssTreeView)
        {
            var cssResolver = new DCssResolver();
            var selectorList = new List<DCssSelector>();
            try
            {
                 selectorList = cssResolver.Resolve(html);
            }
            catch (Exception e)
            {
                CompleX_Studio.MessageLog.LogException(e);
            }
            for (int selectorIndex = 0, selectorCount = selectorList.Count; selectorIndex < selectorCount; ++selectorIndex)
                selectorList[selectorIndex].Priority = selectorIndex;

            selectorList.Sort();

            cssTreeView.SuspendLayout();
            cssTreeView.Nodes.Clear();
            var root = new TreeNode("CSS Styles");


            foreach (DCssSelector selector in selectorList)
                CreateCssTreeNode(root, selector);

            cssTreeView.Nodes.Add(root);
            cssTreeView.ResumeLayout();
        }

        /////////////////////////////////////////////////////////////////////////////////
        private static void CreateCssTreeNode(TreeNode parent, DCssSelector selector)
        {
            var treeNode = new TreeNode(selector.Selector) { Tag = selector };
            parent.Nodes.Add(treeNode);

            foreach (DCssProperty property in selector.Properties)
            {
                var propertyNode = new TreeNode(property.CSS) {Tag = property};
                treeNode.Nodes.Add(propertyNode);
            }
        }
    }
}
