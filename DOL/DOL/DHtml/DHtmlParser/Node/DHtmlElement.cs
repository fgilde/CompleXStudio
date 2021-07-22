/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlElement Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlElement.cs: implementation of the DHtmlElement class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser.Node
{
	/// <summary>
	/// The DHtmlElement object represents any HTML element. An element has a name
	/// and zero or more attributes.
	/// </summary>
    public sealed class DHtmlElement : DHtmlNode, DIHtmlNodeHasAttribute, DOL.DBase.DIDiagnosisable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 資料結構

        public enum EndTagType
        {
            HasChild,
            ExplicitlyTerminated,
            Terminated,
            NonTerminated,
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
	#region 基本操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This constructs a new HTML element with the specified tag name.
        /// </summary>
        public DHtmlElement(string name)
        {
            System.Diagnostics.Debug.Assert(name != null && DHtmlTextProcessor.ExistWhiteSpaceChar(name) == false);

            m_nodes = new DHtmlNodeCollection(this);
            m_name = name.Trim().ToLower();
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlElement(DHtmlElement obj) : base(obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();

            m_name = obj.m_name;
            m_nodes = new DHtmlNodeCollection(this);

            int count = obj.m_nodes.Count;
            m_nodes.Capacity = count;
            for(int index = 0; index < count; ++index)
                m_nodes.Add((DHtmlNode)obj.m_nodes[index].Clone());

            count = obj.m_attributes.Count;
            m_attributes.Capacity = count;
            for(int index = 0; index < count; ++index)
                m_attributes.Add((DHtmlAttribute)obj.m_attributes[index].Clone());
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new DHtmlElement(this);
        }

		/////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public override void AssertValid()
		{
            //System.Diagnostics.Debug.Assert(m_previousWithSameNameNode == null);
            //System.Diagnostics.Debug.Assert(m_close == true);

			m_nodes.AssertValid();
			m_attributes.AssertValid();

            if(m_bindObject is DOL.DBase.DIDiagnosisable)
                ((DOL.DBase.DIDiagnosisable)m_bindObject).AssertValid();
		}

		/////////////////////////////////////////////////////////////////////////////       
		/// <summary>
		/// 傾印物件
		/// </summary>
		public override void Dump(StringBuilder buffer, string prefix)
		{
			AssertValid();
			string old = prefix;
			buffer.Append(old + "├Object " + GetType().Name + " Dump : \n");

            prefix += "│　";
            buffer.Append(prefix + "Node ID: " + this.NodeID + " Parse Close: " + m_close + "\n");	
            buffer.Append(prefix + "HTML Tag: " + this.Tag + "\n");

            if(m_bindObject is DOL.DBase.DIDiagnosisable)
            {
                buffer.Append(prefix + "Bind Object:\n");
                buffer.Append(prefix + "│\n");
                ((DOL.DBase.DIDiagnosisable)m_bindObject).Dump(buffer, prefix);
                buffer.Append(prefix + "│\n");
            }

			if(m_nodes.Count != 0)
			{
                buffer.Append(prefix + "DHtmlNode number: " + m_nodes.Count + "\n");
                buffer.Append(prefix + "Child Object deep dump in the following:\n");

                for(int index = 0, count = m_nodes.Count; index < count; ++index) // 所有物件尋訪一次呼叫 Dump
                {
					buffer.Append(prefix + "│\n");
					m_nodes[index].Dump(buffer, prefix);
				}               
			}
            else if(this.TerminatedType == EndTagType.ExplicitlyTerminated) buffer.Append(prefix + "Element is explicitly terminated\n");
            else if(this.TerminatedType == EndTagType.Terminated) buffer.Append(prefix + "Element is terminated\n");
            else if(this.TerminatedType == EndTagType.NonTerminated) buffer.Append(prefix + "Element is non-terminated\n");
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(DOL.DBase.DIBaseVisitor visitor)
        {
            DOL.DBase.DIVisitor<DHtmlElement> elementVisitor = visitor as DOL.DBase.DIVisitor<DHtmlElement>;
            if(elementVisitor != null) elementVisitor.Visit(this);
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region 相關操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public override string NodeName
        {
            get
            {
                return m_name;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This is the tag name of the element. e.g. BR, BODY, TABLE etc.
		/// </summary>
		public string Name
		{
			get
			{
				return m_name;
			}
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                System.Diagnostics.Debug.Assert(DHtmlTextProcessor.ExistWhiteSpaceChar(value) == false);

                m_name = value.Trim().ToLower();
            }
		}

        /////////////////////////////////////////////////////////////////////////////////
        public EndTagType TerminatedType
        {
            get
            {
                if(m_nodes.Count > 0)
                    m_terminatedType = EndTagType.HasChild;
                return m_terminatedType;
            }
            set
            {
                if(m_nodes.Count > 0)
                    m_terminatedType = EndTagType.HasChild;
                else m_terminatedType = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public override bool IsLeaf
        {
            get
            {
                return (m_nodes.Count == 0);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is the collection of all child nodes of this one. If this node is actually
        /// a text node, this will throw an InvalidOperationException exception.
        /// </summary>
        public DHtmlNodeCollection Nodes
        {
            get
            {
                return m_nodes;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is the collection of attributes associated with this element.
        /// </summary>
        public DHtmlAttributeCollection Attributes
        {
            get
            {
                return m_attributes;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the HTML representation of this element.
        /// </summary>
        /// <returns></returns>		
        public string Tag
        {
            get
            {
                string temp = "<" + m_name;

                for(int index = 0, count = m_attributes.Count; index < count; ++index)
                    temp += " " + m_attributes[index].HTML;

                temp += ">";
                return temp;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
		public string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();

                for(int index = 0, count = m_nodes.Count; index < count; ++index)
                {
                    DHtmlText text = m_nodes[index] as DHtmlText;
                    if(text != null)
                        stringBuilder.Append(text.Text);
                }

				return stringBuilder.ToString();
			}
		}

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region 匯出

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full HTML to represent this node (and all child nodes)
        /// </summary>
        internal override void TransformHTML(StringBuilder writer, int indentDepth)
        {
            System.Diagnostics.Debug.Assert(writer != null);
            writer.Append("<" + m_name);

            for(int index = 0, count = m_attributes.Count; index < count; ++index)
            {
                writer.Append(" ");
                m_attributes[index].TransformHTML(writer);
            }

            if(m_nodes.Count > 0)
            {
                writer.Append(">");
                for(int index = 0, count = m_nodes.Count; index < count; ++index)
                    m_nodes[index].TransformHTML(writer, indentDepth + 1);
                writer.Append("</" + m_name + ">");
            }
            else if(this.TerminatedType == EndTagType.ExplicitlyTerminated)
                 writer.Append("></" + m_name + ">");
            else if(this.TerminatedType == EndTagType.Terminated)
                writer.Append("/>");
            else if(this.TerminatedType == EndTagType.NonTerminated)
                writer.Append(">");
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region 內部資料

		/// <summary>
		/// 
		/// </summary>
		private string m_name = "";
        /// <summary>
        /// 
        /// </summary>
        private EndTagType m_terminatedType = EndTagType.NonTerminated;
		/// <summary>
		/// 
		/// </summary>
		private DHtmlNodeCollection m_nodes = null; 
		/// <summary>
		/// 
		/// </summary>
        private DHtmlAttributeCollection m_attributes = new DHtmlAttributeCollection();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region Metadata of parsing

        /// <summary>
        /// 
        /// </summary>
        internal bool m_close = false;
        /// <summary>
        /// 
        /// </summary>
        internal DHtmlElement m_previousWithSameNameNode = null;

    #endregion

	}
}
