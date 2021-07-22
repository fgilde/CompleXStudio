/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlDocument Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlDocument.cs: implementation of the DHtmlDocument class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using DOL.DHtml.DHtmlParser.Node;

namespace DOL.DHtml.DHtmlParser
{
	/// <summary>
/// This is the basic HTML document object used to represent a sequence of HTML.
	/// </summary>
    public class DHtmlDocument : DOL.DBase.DIDiagnosisable, ICloneable
	{

    /////////////////////////////////////////////////////////////////////////////////
    #region 基本操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will create a new document object by parsing the HTML specified.
        /// </summary>
        public DHtmlDocument()
        {
            m_parser = new DHtmlGeneralParser();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will create a new document object by parsing the HTML specified.
        /// </summary>
        /// <param name="parser"></param>
        public DHtmlDocument(DIHtmlParser parser)
        {
            System.Diagnostics.Debug.Assert(parser != null);
            m_parser = parser;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will create a new document object by parsing the HTML specified.
        /// </summary>
        /// <param name="html"></param>
        public DHtmlDocument(string html)
        {
            System.Diagnostics.Debug.Assert(html != null);
            m_parser = new DHtmlGeneralParser();
            LoadHtml(html);
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
        /// This will create a new document object by parsing the HTML specified.
		/// </summary>
		/// <param name="html"></param>
		/// <param name="parser"></param>
        public DHtmlDocument(string html, DIHtmlParser parser)
		{
            System.Diagnostics.Debug.Assert(html != null);
            System.Diagnostics.Debug.Assert(parser != null);
            m_parser = parser;
            LoadHtml(html);
		}

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            DHtmlDocument newDoc = new DHtmlDocument();
            newDoc.m_charset = m_charset;

            int count = m_nodeList.Count;
            newDoc.m_nodeList.Capacity = count;
            for(int index = 0; index < count; ++index)
                newDoc.m_nodeList.Add((DHtmlNode)m_nodeList[index].Clone());                

           return newDoc;
        }

        /////////////////////////////////////////////////////////////////////////////////        
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public void AssertValid()
		{
            for(int index = 0, count = m_nodeList.Count; index < count; ++index)
                ((DOL.DBase.DIDiagnosisable)m_nodeList[index]).AssertValid();
		}

        /////////////////////////////////////////////////////////////////////////////////       
		/// <summary>
		/// 傾印物件
		/// </summary>
		public void Dump(StringBuilder buffer, string prefix)
		{
			AssertValid();
			string old = prefix;
			buffer.Append(old + "├Object " + GetType().Name + " Dump : \n");							

			prefix += "│　";
			buffer.Append(prefix + "DHtmlNode number: " + m_nodeList.Count + "\n");	

			if(m_nodeList.Count != 0)
			{
				buffer.Append(prefix + "Deep dump in the following:\n");

                for(int index = 0, count = m_nodeList.Count; index < count; ++index) // 所有物件尋訪一次呼叫 Dump
                {
                    buffer.Append(prefix + "│\n");
                    m_nodeList[index].Dump(buffer, prefix);
                }      
			}
		}

    #endregion 

    /////////////////////////////////////////////////////////////////////////////////
    #region 文件操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void Load(string filePath)
        {
            System.Diagnostics.Debug.Assert(filePath != null);

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Load(fileStream);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public virtual void Load(Stream inStream)
        {
            System.Diagnostics.Debug.Assert(inStream != null);
            m_charset = null;

            StreamReader streamReader = null;

            m_charset = DetectCharset(inStream);
            if(m_charset != null)
                streamReader = new StreamReader(inStream, m_charset);
            else
            {
                streamReader = new StreamReader(inStream, true);
                m_charset = streamReader.CurrentEncoding;
            }

            m_nodeList.Clear();
            m_parser.Parse(streamReader.ReadToEnd(), m_nodeList);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public virtual void Load(TextReader reader)
        {            
            System.Diagnostics.Debug.Assert(reader != null);
            m_charset = null;

            LoadHtml(reader.ReadToEnd());            
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        public virtual void LoadHtml(string html)
        {
            System.Diagnostics.Debug.Assert(html != null);
            m_nodeList.Clear();
            m_parser.Parse(html, m_nodeList);

            if(m_charset == null)
            {
                m_charset = DetectCharset(m_nodeList);
                if(m_charset == null) m_charset = Encoding.Unicode;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public virtual void Save(string filePath)
        {
            System.Diagnostics.Debug.Assert(filePath != null);

            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            Save(fileStream);
            fileStream.Flush();
            fileStream.Close();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outStream"></param>
        public virtual void Save(Stream outStream)
        {
            StreamWriter writer = new StreamWriter(outStream, m_charset);
            Save(writer);
            writer.Flush();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public virtual void Save(TextWriter writer)
        {
             writer.Write(this.HTML);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        public void Visit(DOL.DBase.DIBaseVisitor visitor)
        {
            int count = m_nodeList.Count;
            for(int index = 0; index < count; ++index)
                m_nodeList[index].Accept(visitor);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public virtual Encoding Charset
        {
            get
            {
                return m_charset;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);

                if(!m_charset.Equals(value))
                {
                    m_charset = value;
                    
                    DHtmlNodeCollection metaNodes = new DHtmlNodeCollection();
                    DHtmlElement node = m_nodeList["html"] as DHtmlElement;
                    if(node != null) node = node.Nodes["head"] as DHtmlElement;
                    if(node != null) node.Nodes.FindByNameAttribute(metaNodes, "meta", "content", false);

                    for(int nodeIndex = 0, nodeCount = metaNodes.Count; nodeIndex < nodeCount; ++nodeIndex) // 所有物件尋訪一次呼叫 Dump
                    {
                        DHtmlElement metaElement = metaNodes[nodeIndex] as DHtmlElement;
                        if(metaElement != null)
                        {
                            int index = -1;
                            DHtmlAttributeCollection attributes = metaElement.Attributes.FindByName("content");
                            for(int attributeIndex = 0, attributeCount = attributes.Count; attributeIndex < attributeCount; ++attributeIndex) // 所有物件尋訪一次呼叫 Dump
                            {
                                DHtmlAttribute attribute = attributes[attributeIndex];
                                if((index = attribute.Value.IndexOf("charset")) != -1)
                                {
                                    string attributeValue = attribute.Value;
                                    // 取得 CodePage 描述 的開頭索引
                                    int startIndex = index + 7;
                                    while(startIndex < attributeValue.Length && DHtmlTextProcessor.EqualesOfAnyChar(attributeValue[startIndex], " =")) ++startIndex;
                                    // 取得 CodePage 描述 的結尾索引
                                    int endIndex = startIndex + 1;
                                    while(endIndex < attributeValue.Length && !DHtmlTextProcessor.EqualesOfAnyChar(attributeValue[endIndex], " ")) ++endIndex;

                                    // 取得 CodePage 描述
                                    if(startIndex < attributeValue.Length && endIndex - startIndex > 0)
                                    {
                                        attributeValue = attributeValue.Remove(startIndex, endIndex - startIndex);
                                        attributeValue = attributeValue.Insert(startIndex, m_charset.WebName);
                                        attribute.Value = attributeValue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// This will return the HTML used to represent this document.
		/// </summary>
        public virtual string HTML
		{
			get
			{
				StringBuilder writer = new StringBuilder();
                for(int index = 0, count = m_nodeList.Count; index < count; ++index)
                    m_nodeList[index].TransformHTML(writer, 0);

                return writer.ToString();
			}
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlNodeCollection Nodes
        {
            get
            {
                return m_nodeList;
            }
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region 內部函數

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlStream"></param>
        /// <returns></returns>
        private Encoding DetectCharset(DHtmlNodeCollection nodes)
        {
            Encoding result = null;

            // 辨識編碼
            string charset = "";

            DHtmlNodeCollection metaNodes = new DHtmlNodeCollection();
            DHtmlElement node = nodes["html"] as DHtmlElement;
            if(node != null) node = node.Nodes["head"] as DHtmlElement;
            if(node != null) node.Nodes.FindByNameAttribute(metaNodes, "meta", "content", false);

            for(int nodeIndex = 0, count = metaNodes.Count; nodeIndex < count; ++nodeIndex)
            {
                DHtmlElement metaElement = metaNodes[nodeIndex] as DHtmlElement;
                if(metaElement != null)
                {
                    int index = -1;
                    DHtmlAttributeCollection attributes = metaElement.Attributes.FindByName("content");
                    for(int attributeIndex = 0, attributeCount = attributes.Count; attributeIndex < attributeCount; ++attributeIndex) // 所有物件尋訪一次呼叫 Dump
                    {
                        DHtmlAttribute attribute = attributes[attributeIndex];
                        if((index = attribute.Value.IndexOf("charset")) != -1)
                        {
                            string value = attribute.Value;
                            // 取得 CodePage 描述 的開頭索引
                            int startIndex = index + 7;
                            while(startIndex < value.Length && DHtmlTextProcessor.EqualesOfAnyChar(value[startIndex], " =")) ++startIndex;
                            // 取得 CodePage 描述 的結尾索引
                            int endIndex = startIndex + 1;
                            while(endIndex < value.Length && !DHtmlTextProcessor.EqualesOfAnyChar(value[endIndex], " ")) ++endIndex;

                            // 取得 CodePage 描述
                            if(startIndex < value.Length && endIndex - startIndex > 0)
                            {
                                charset = value.Substring(startIndex, endIndex - startIndex);
                                try
                                {
                                    result = Encoding.GetEncoding(charset);
                                    break;
                                }
                                catch(Exception)
                                {
                                }
                            }
                        }
                    }
                }
            }
                
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlStream"></param>
        private Encoding DetectCharset(Stream inStream)
        {
            Encoding result = null;

            // 辨識編碼
            string charset = "";

            long position = inStream.Position;
            System.IO.StreamReader reader = new System.IO.StreamReader(inStream);
            while(reader.EndOfStream == false)
            {
                string buffer = reader.ReadLine();
                int index = buffer.IndexOf("charset");

                if(index != -1 && buffer.Length > "charset".Length)
                {
                    // 取得 CodePage 描述 的開頭索引
                    int startIndex = index + "charset".Length;
                    while(startIndex < buffer.Length && DHtmlTextProcessor.EqualesOfAnyChar(buffer[startIndex], " \r\n\t=\'\"<>")) ++startIndex;
                    // 取得 CodePage 描述 的結尾索引
                    int endIndex = startIndex + 1;
                    while(endIndex < buffer.Length && !DHtmlTextProcessor.EqualesOfAnyChar(buffer[endIndex], " \r\n\t=\'\"<>")) ++endIndex;

                    // 取得 CodePage 描述
                    if(startIndex < buffer.Length && endIndex - startIndex > 0)
                    {
                        charset = buffer.Substring(startIndex, endIndex - startIndex);
                        try
                        {
                            result = Encoding.GetEncoding(charset);
                            break;
                        }
                        catch(Exception)
                        {
                        }
                    }
                }
            }

            inStream.Position = position;

            return result;
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private DIHtmlParser m_parser = null;
        /// <summary>
        /// 
        /// </summary>
        private Encoding m_charset = Encoding.Default;
		/// <summary>
		/// 
		/// </summary>
		private DHtmlNodeCollection m_nodeList = new DHtmlNodeCollection(null);		

    #endregion 

	}
}
