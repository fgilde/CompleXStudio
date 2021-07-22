/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlComment Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlComment: implementation of the DHtmlComment class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace DOL.DHtml.DHtmlParser.Node
{
	/// <summary>
	/// The DHtmlComment node represents a simple piece of comment from the document.
	/// </summary>
    public sealed class DHtmlComment : DHtmlNode, DOL.DBase.DIDiagnosisable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlComment()
        {
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		/// <param name="comment"></param>
		public DHtmlComment(string comment)
		{
            System.Diagnostics.Debug.Assert(comment != null);
            m_comment = comment;
		}

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DHtmlComment(DHtmlComment obj) 
            : base(obj)
        {
            System.Diagnostics.Debug.Assert(obj != null);

            obj.AssertValid();
            m_comment = obj.m_comment;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new DHtmlComment(this);
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

            if (m_comment.Length == 0)
				buffer.Append(prefix + "Comment content is empty\n");
            else
            {
                buffer.Append(prefix + "Comment content:");
                buffer.Append("\n====================================================================================================================================\n");
                buffer.Append(m_comment);
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
            DOL.DBase.DIVisitor<DHtmlComment> commentVisitor = visitor as DOL.DBase.DIVisitor<DHtmlComment>;
            if(commentVisitor != null) commentVisitor.Visit(this);
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
                return "";
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
        /// 
        /// </summary>
        public bool SGMLComment
        {
            get
            {
                return m_SGMLComment;
            }
            set
            {
                m_SGMLComment = value;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This is the comment associated with this node.
		/// </summary>
		public string Comment
		{
			get
			{
                return m_comment;
			}
			set
			{
				System.Diagnostics.Debug.Assert(value != null);
                m_comment = value;
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

            for(int count = 0; count < indentDepth; ++count)
                writer.Append("\t");

            if(m_SGMLComment == false)
                writer.Append("<!--");
            else writer.Append("<!");
            
            writer.Append(m_comment);

            if(m_SGMLComment == false)
                writer.Append("-->\n");
            else writer.Append(">\n");
        }

    #endregion	

	/////////////////////////////////////////////////////////////////////////////////
	#region	內部資料

		/// <summary>
		/// 
		/// </summary>
        private string m_comment = "";
        /// <summary>
        /// 
        /// </summary>
        private bool m_SGMLComment = false;

    #endregion	

	}
}
