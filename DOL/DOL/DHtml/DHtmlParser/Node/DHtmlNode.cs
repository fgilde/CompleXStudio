/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlNode Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlNode.cs: implementation of the DHtmlNode class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser.Node
{
	/// <summary>
	/// The DHtmlNode is the base for all objects that may appear in HTML.
	/// </summary>
    public abstract class DHtmlNode : DOL.DBase.DIDiagnosisable, ICloneable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作	

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This constructor is used by the subclasses.
		/// </summary>
		protected DHtmlNode()
		{
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This constructor is used by the subclasses.
        /// </summary>
        protected DHtmlNode(DHtmlNode obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();

            m_parent = obj.m_parent;
            m_nodeID = obj.m_nodeID;
            m_bindObject = obj.m_bindObject;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

		/////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public abstract void AssertValid();

		/////////////////////////////////////////////////////////////////////////////       
		/// <summary>
		/// 傾印物件
		/// </summary>
		/// <param name="buffer"> 傾印之緩衝區 </param>
		/// <param name="prefix"> 縮排文字 </param>
		public abstract void Dump(StringBuilder buffer, string prefix);

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is the text associated with this node.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.HTML;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return m_nodeID;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public abstract void Accept(DOL.DBase.DIBaseVisitor visitor);

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 匯出

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return the full HTML to represent this node (and all child nodes).
		/// </summary>
        public string HTML 
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                TransformHTML(stringBuilder, 0);

                return stringBuilder.ToString();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full HTML to represent this node (and all child nodes)
        /// </summary>
        internal abstract void TransformHTML(StringBuilder writer, int indentDepth);

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 相關操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int NodeID
        {
            get
            {
                return m_nodeID;
            }
            set
            {
                m_nodeID = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public abstract string NodeName
        {
            get;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public Object BindObject
        {
            get
            {
                return m_bindObject;
            }
            set
            {
                m_bindObject = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the parent of this node, or null if there is none.
        /// </summary>
        public DHtmlElement Parent
        {
            get
            {
                return m_parent;
            }
            internal set
            {
                m_parent = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the previous sibling of this node, or null if there is none.
        /// </summary>
        public DHtmlNode PreviousSibling
        {
            get
            {
                DHtmlNode previousSibling = null;
                if(m_parent != null)
                {
                    int index = m_parent.Nodes.IndexOf(this);
                    System.Diagnostics.Debug.Assert(index != -1);                    
                    if(index - 1 >= 0)
                        previousSibling = m_parent.Nodes[index - 1];
                }

                return previousSibling;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the next sibling of this node, or null if there is none.
        /// </summary>
        public DHtmlNode NextSibling
        {
            get
            {
                DHtmlNode nextSibling = null;
                if(m_parent != null)
                {
                    int index = m_parent.Nodes.IndexOf(this);
                    System.Diagnostics.Debug.Assert(index != -1); 
                    if(index + 1 < m_parent.Nodes.Count)
                        nextSibling = m_parent.Nodes[index + 1];
                }

                return nextSibling;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return true if this is a root node (has no parent).
		/// </summary>
		public bool IsRoot
		{
			get
			{
				return m_parent == null;
			}
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public abstract bool IsLeaf { get; }
       
		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return true if the node passed is a descendent of this node.
		/// </summary>
		/// <param name="node">The node that might be the parent or grandparent (etc.)</param>
		/// <returns>True if this node is a descendent of the one passed in.</returns>
		public bool IsDescendentOf(DHtmlNode node)
		{
            System.Diagnostics.Debug.Assert(node != null);

            bool reault = false;

			DHtmlNode parent = m_parent;
			while(parent != null)
			{
                if (parent == node)
                {
                    reault = true;
                    break;
                }

				parent = parent.m_parent;
			}

            return reault;
		}

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return true if the node passed is one of the children or grandchildren of this node.
		/// </summary>
		/// <param name="node">The node that might be a child.</param>
		/// <returns>True if this node is an ancestor of the one specified.</returns>
		public bool IsAncestorOf(DHtmlNode node)
		{
            System.Diagnostics.Debug.Assert(node != null);
			return node.IsDescendentOf(this);
		}

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return the ancstor that is common to this node and the one specified.
		/// </summary>
		/// <param name="node">The possible node that is relative</param>
		/// <returns>The common ancestor, or null if there is none</returns>
		public DHtmlNode GetCommonAncestor(DHtmlNode node)
		{
            System.Diagnostics.Debug.Assert(node != null);

            DHtmlNode commonAncestor = null;

			DHtmlNode thisParent = this;
            while(commonAncestor != null && thisParent != null)
			{
				DHtmlNode thatParent = node;
                while(commonAncestor != null && thatParent != null)
				{
                    if(thisParent == thatParent)
                      commonAncestor = thisParent;
                   
					thatParent = thatParent.Parent;
				}
				thisParent = thisParent.Parent;
			}

            return commonAncestor;
		}

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部資料
		
		/// <summary>
		/// 
		/// </summary>
        protected DHtmlElement m_parent = null;
        /// <summary>
        /// 
        /// </summary>
        protected int m_nodeID = 0;
        /// <summary>
        /// 
        /// </summary>
        protected Object m_bindObject = null;

    #endregion

	}
}
