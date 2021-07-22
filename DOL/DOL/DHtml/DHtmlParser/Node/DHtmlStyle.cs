/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlStyle Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlStyle.cs: implementation of the DHtmlStyle class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser.Node
{
	/// <summary>
    /// The DHtmlStyle node represents a simple piece of style from the document.
	/// </summary>
    public sealed class DHtmlStyle : DHtmlNode, DIHtmlNodeHasAttribute, DOL.DBase.DIDiagnosisable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
        public DHtmlStyle()
		{
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlStyle(string style)
        {
            System.Diagnostics.Debug.Assert(style != null);
            m_style = style;
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlStyle(DHtmlStyle obj) 
            : base(obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();
            m_style = obj.m_style;

            int count = obj.m_attributes.Count;
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
            return new DHtmlStyle(this);
        }

		/////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public override void AssertValid()
		{          			
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
            buffer.Append(prefix + "Node ID: " + this.NodeID + "\n");	
            buffer.Append(prefix + "HTML Tag: " + this.Tag + "\n");		

            if(m_style.Length == 0)
                buffer.Append(prefix + "Style content is empty\n");
            else
            {
                buffer.Append(prefix + "Style content:");
                buffer.Append("\n====================================================================================================================================\n");
                buffer.Append(m_style);
                buffer.Append("\n====================================================================================================================================\n");
            }
		}
        
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public override void Accept(DOL.DBase.DIBaseVisitor visitor)
        {
            DOL.DBase.DIVisitor<DHtmlStyle> styleVisitor = visitor as DOL.DBase.DIVisitor<DHtmlStyle>;
            if(styleVisitor != null) styleVisitor.Visit(this);
        }

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 屬性設定

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public override string NodeName
        {
            get
            {
                return "style";
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
                return true;
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
                string temp = "<style";
                for(int index = 0, count = m_attributes.Count; index < count; ++index)
                    temp += " " + m_attributes[index].HTML;

                temp += ">";
                return temp;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This is the Script associated with this node.
		/// </summary>
		public string Style
		{
			get
			{
                return m_style;
			}
			set
			{
                System.Diagnostics.Debug.Assert(value != null);
                m_style = value;
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
        /// This is the text associated with this node.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.HTML;
        }

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 匯出

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///  匯出成 HTML 字串
		/// </summary>
        /// 
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full HTML to represent this node (and all child nodes)
        /// </summary>
        internal override void TransformHTML(StringBuilder writer, int indentDepth)
        {
            System.Diagnostics.Debug.Assert(writer != null);		

            writer.Append("<style");
            for(int index = 0, count = m_attributes.Count; index < count; ++index)
            {
                writer.Append(" ");
                m_attributes[index].TransformHTML(writer);
            }
            writer.Append(">");

            writer.Append(m_style);
            writer.Append("</style>");
        }

    #endregion	

	/////////////////////////////////////////////////////////////////////////////////
	#region	內部資料

		/// <summary>
		/// 
		/// </summary>
        private string m_style = "";
        /// <summary>
        /// 
        /// </summary>
        private DHtmlAttributeCollection m_attributes = new DHtmlAttributeCollection();

    #endregion	

	}
}
