/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlScript Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlScript.cs: implementation of the DHtmlScript class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser.Node
{
	/// <summary>
    /// The DHtmlScript node represents a simple piece of script from the document.
	/// </summary>
    public sealed class DHtmlScript : DHtmlNode, DIHtmlNodeHasAttribute, DOL.DBase.DIDiagnosisable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
        public DHtmlScript()
		{
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlScript(string script)
        {
            System.Diagnostics.Debug.Assert(script != null);
            m_script = script;
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlScript(DHtmlScript obj) 
            : base(obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();
            m_script = obj.m_script;

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
            return new DHtmlScript(this);
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

            if(m_script.Length == 0)
                buffer.Append(prefix + "Script content is empty\n");
            else
            {
                buffer.Append(prefix + "Script content:");
                buffer.Append("\n====================================================================================================================================\n");
                buffer.Append(m_script);
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
            DOL.DBase.DIVisitor<DHtmlScript> scriptVisitor = visitor as DOL.DBase.DIVisitor<DHtmlScript>;
            if(scriptVisitor != null) scriptVisitor.Visit(this);
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
                return "script";
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
                string temp = "<script";
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
		public string Script
		{
			get
			{
                return m_script;
			}
			set
			{
				System.Diagnostics.Debug.Assert(value != null);
                m_script = value;
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

            writer.Append("<script");
            for(int index = 0, count = m_attributes.Count; index < count; ++index)
            {
                writer.Append(" ");
                m_attributes[index].TransformHTML(writer);
            }
            writer.Append(">");

            writer.Append(m_script);
            writer.Append("</script>");
        }

#endregion	

	/////////////////////////////////////////////////////////////////////////////////
	#region	內部資料

		/// <summary>
		/// 
		/// </summary>
		private string m_script = "";
        /// <summary>
        /// 
        /// </summary>
        private DHtmlAttributeCollection m_attributes = new DHtmlAttributeCollection();

    #endregion	

	}
}
