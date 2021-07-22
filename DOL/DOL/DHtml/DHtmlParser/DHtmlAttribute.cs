/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlAttribute Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlAttribute.cs: implementation of the DHtmlAttribute class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser
{
	/// <summary>
	/// The DHtmlAttribute object represents a named value associated with an DHtmlElement.
	/// </summary>
    public sealed class DHtmlAttribute : DOL.DBase.DIDiagnosisable, ICloneable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作	

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 建構子
		/// </summary>
		public DHtmlAttribute()
		{
		}

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlAttribute(string name)
        {
            System.Diagnostics.Debug.Assert(name != null && DHtmlTextProcessor.ExistWhiteSpaceChar(name) == false);

            m_attributeName = name.Trim().ToLower();
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlAttribute(string name, string value)
        {
            System.Diagnostics.Debug.Assert(name != null && DHtmlTextProcessor.ExistWhiteSpaceChar(name) == false);
            System.Diagnostics.Debug.Assert(value != null);

            m_attributeName = name.Trim().ToLower();
            m_attributeValue = value;
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlAttribute(DHtmlAttribute obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();
            m_attributeName = obj.m_attributeName;
            m_attributeValue = obj.m_attributeValue;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DHtmlAttribute(this);
        }

		/////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public void AssertValid()
		{
            System.Diagnostics.Debug.Assert(m_attributeName != null && DHtmlTextProcessor.ExistWhiteSpaceChar(m_attributeName) == false, "Member variable, m_attributeName is invalid");
            System.Diagnostics.Debug.Assert(m_attributeValue != null, "Member variable, m_attributeValue is invalid");       	
		}

		/////////////////////////////////////////////////////////////////////////////       
		/// <summary>
		/// 傾印物件
		/// </summary>
		public void Dump(StringBuilder buffer, string prefix)
		{
            AssertValid();
            string old = prefix;
            buffer.Append(old + "├Object " + GetType().Name + " Dump : \n");

            prefix += "│　";

            if(m_attributeName.Length == 0)
                buffer.Append(prefix + "Attribute is empty\n");
            else
                buffer.Append(prefix + "Attribute: \"" + this.HTML + "\"\n");
		}

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 屬性設定

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 屬性名稱
		/// </summary>
		public string Name
		{
			get
			{
				return m_attributeName;
			}
			set
			{
                System.Diagnostics.Debug.Assert(value != null);
                System.Diagnostics.Debug.Assert(DHtmlTextProcessor.ExistWhiteSpaceChar(value) == false);

                m_attributeName = value.Trim().ToLower(); 
			}
		}

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 屬性值
		/// </summary>
		public string Value
		{
			get
			{
				return m_attributeValue;
			}
			set
			{
                System.Diagnostics.Debug.Assert(value != null);
                m_attributeValue = value; 
			}
		}

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DIHtmlNodeHasAttribute OwnerNode
        {
            get
            {
                return m_ownerNode;
            }
            internal set
            {
                m_ownerNode = value;
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

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 匯出成 HTML 字串
		/// </summary>		
        public string HTML
		{
            get
            {
                StringBuilder writer = new StringBuilder();
                TransformHTML(writer);

                return writer.ToString();
            }
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full HTML to represent this node (and all child nodes)
        /// </summary>
        internal void TransformHTML(StringBuilder writer)
        {
            System.Diagnostics.Debug.Assert(writer != null);

            writer.Append(m_attributeName);
            writer.Append("=\"");
            writer.Append(DHtmlTextProcessor.TranStrToHtmlText(m_attributeValue));
            writer.Append("\"");
        }
	
    #endregion	

	/////////////////////////////////////////////////////////////////////////////////
	#region 內部資料

		/// <summary>
		/// 屬性名稱
		/// </summary>
		private string m_attributeName = "";
		/// <summary>
		/// 屬性值
		/// </summary>
		private string m_attributeValue = "";
        /// <summary>
        /// 
        /// </summary>
        private DIHtmlNodeHasAttribute m_ownerNode = null;

    #endregion

	}

}
