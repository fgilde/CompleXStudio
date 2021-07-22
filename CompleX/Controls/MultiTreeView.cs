//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace CompleX.Controls
{
    /// <summary>
    /// Summary description for TreeViewMS.
    /// </summary>
    public class MultiTreeView : TreeView
    {
        protected ArrayList MColl;
        protected TreeNode LastNode, FirstNode;

        public MultiTreeView()
        {
            MColl = new ArrayList();
        }

        public void ClearSelection()
        {
            RemovePaintFromNodes();
            if (MColl != null) MColl.Clear();
            PaintSelectedNodes();
        }

        public ArrayList SelectedNodes
        {
            get
            {
                return MColl;
            }
            set
            {
                RemovePaintFromNodes();
                if(MColl != null) MColl.Clear();
                MColl = value;
                PaintSelectedNodes();
            }
        }



        // Triggers
        //
        // (overriden method, and base class called to ensure events are triggered)


        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);
            // selecting twice the node while pressing CTRL ?
            if (MColl == null)
                return;
            if (bControl && MColl.Contains(e.Node))
            {
                // unselect it (let framework know we don't want selection this time)
                e.Cancel = true;

                // update nodes
                RemovePaintFromNodes();
                MColl.Remove(e.Node);
                PaintSelectedNodes();
                return;
            }

            LastNode = e.Node;
            if (!bShift) FirstNode = e.Node; // store begin of shift sequence
        }


        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            if (bControl)
            {
                if (!MColl.Contains(e.Node)) // new node ?
                {
                    MColl.Add(e.Node);
                }
                else  // not new, remove it from the collection
                {
                    RemovePaintFromNodes();
                    MColl.Remove(e.Node);
                }
                PaintSelectedNodes();
            }
            else
            {
                // SHIFT is pressed
                if (bShift)
                {
                    var myQueue = new Queue();

                    TreeNode uppernode = FirstNode;
                    TreeNode bottomnode = e.Node;
                    // case 1 : begin and end nodes are parent
                    bool bParent = IsParent(FirstNode, e.Node); // is firstNode parent (direct or not) of e.Node
                    if (!bParent)
                    {
                        bParent = IsParent(bottomnode, uppernode);
                        if (bParent) // swap nodes
                        {
                            TreeNode t = uppernode;
                            uppernode = bottomnode;
                            bottomnode = t;
                        }
                    }
                    if (bParent)
                    {
                        TreeNode n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!MColl.Contains(n)) // new node ?
                                myQueue.Enqueue(n);

                            n = n.Parent;
                        }
                    }
                    // case 2 : nor the begin nor the end node are descendant one another
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            if (nIndexBottom < nIndexUpper) // reversed?
                            {
                                TreeNode t = uppernode;
                                uppernode = bottomnode;
                                bottomnode = t;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }

                            TreeNode n = uppernode;
                            while (nIndexUpper <= nIndexBottom)
                            {
                                if (!MColl.Contains(n)) // new node ?
                                    myQueue.Enqueue(n);

                                n = n.NextNode;

                                nIndexUpper++;
                            } // end while

                        }
                        else
                        {
                            if (!MColl.Contains(uppernode)) myQueue.Enqueue(uppernode);
                            if (!MColl.Contains(bottomnode)) myQueue.Enqueue(bottomnode);
                        }
                    }

                    MColl.AddRange(myQueue);

                    PaintSelectedNodes();
                    FirstNode = e.Node; // let us chain several SHIFTs if we like it
                } // end if m_bShift
                else
                {
                    // in the case of a simple click, just add this item
                    if (MColl != null && MColl.Count > 0)
                    {
                        RemovePaintFromNodes();
                        MColl.Clear();
                    }
                    if (MColl != null) MColl.Add(e.Node);
                }
            }
        }



        // Helpers
        //
        //


        protected bool IsParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            TreeNode n = childNode;
            bool bFound = false;
            while (!bFound && n != null)
            {
                n = n.Parent;
                bFound = (n == parentNode);
            }
            return bFound;
        }

        protected void PaintSelectedNodes()
        {
            if (MColl == null)
                return;
            foreach (TreeNode n in MColl)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        protected void RemovePaintFromNodes()
        {
            if (MColl == null || MColl.Count == 0) return;

            var n0 = (TreeNode)MColl[0];
            if (n0.TreeView != null)
            {
                Color back = n0.TreeView.BackColor;
                Color fore = n0.TreeView.ForeColor;


                foreach (TreeNode n in MColl)
                {
                    n.BackColor = back;
                    n.ForeColor = fore;
                }
            }

        }

    }
}
