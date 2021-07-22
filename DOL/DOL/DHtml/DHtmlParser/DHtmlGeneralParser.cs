/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlGeneralParser Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlGeneralParser.cs: implementation of the DHtmlGeneralParser class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using DOL.DHtml.DHtmlParser.Node;

namespace DOL.DHtml.DHtmlParser
{
	/// <summary>
	/// This is the interface of HTML parser.
	/// </summary>
    public sealed class DHtmlGeneralParser : DIHtmlParser
	{
    /////////////////////////////////////////////////////////////////////////////////
    #region 資料結構

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        private class Token
        {
            public enum TokenType
            {
                None,
                EndOfHtml,                   // End of HTML
                TagName,                     // tag name
                TagBegin,                    // <
                TagEnd,                      // >
                TagCloseBegin,               // </
                TagCloseEnd,                 // />
                Comment,                     // comment
                SGMLComment,                 // SGML Comment
                ProcessingInstruction,       // processing instruction
                AttributeName,               // attribute name
                AttributeValue,              // attribute value
                Text,                        // text
                NoEscapingText,              // no escaping text
            }
            
            public Token(TokenType type, string value)
            {
                m_type = type;
                m_content = value;
            }

            public TokenType Type
            {
                get
                {
                    return m_type;
                }
                set
                {
                    m_type = value;
                }
            }

            public string Content
            {
                get
                {
                    return m_content;
                }
                set
                {
                    m_content = value;
                }
            }

            private TokenType m_type = TokenType.None;
            private string m_content = null;

            public static readonly Token TagBegin = new Token(Token.TokenType.TagBegin, null);
            public static readonly Token TagEnd = new Token(Token.TokenType.TagEnd, null);
            public static readonly Token TagCloseBegin = new Token(Token.TokenType.TagCloseBegin, null);
            public static readonly Token TagCloseEnd = new Token(Token.TokenType.TagCloseEnd, null);
            public static readonly Token EndOfHtml = new Token(Token.TokenType.EndOfHtml, null);
        }

        /// <summary>
        /// 
        /// </summary>
        private class ParseExeception : System.Exception
        {
            public ParseExeception()
                : base()
            {
            }
        }

    #endregion 

    /////////////////////////////////////////////////////////////////////////////////
    #region 基本操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlGeneralParser()
        {
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will parse a string containing HTML and will produce a domain tree.
        /// </summary>
        /// <param name="html">The HTML to be parsed</param> 
        /// <returns></returns>
        public void Parse(string html, DHtmlNodeCollection result)
        {
            System.Diagnostics.Debug.Assert(html != null);
            System.Diagnostics.Debug.Assert(result != null);

            m_input = html;
            m_result = result;
            m_currentLevel = m_result;            

            ParseTokens();
            ResolveTokens();

            m_result = null;            
            m_input = null;
            m_currentLevel = null;
            m_lastOpenNodes.Clear();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will parse a string containing HTML and will produce a domain tree.
        /// </summary>
        /// <param name="html">The HTML to be parsed</param> 
        /// <returns></returns>
        public DHtmlNodeCollection Parse(string html)
        {
            System.Diagnostics.Debug.Assert(html != null);

            DHtmlNodeCollection result = new DHtmlNodeCollection();     
            Parse(html, result);
            return result;
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 事件操作

        /// <summary>
        /// 
        /// </summary>
        public event ElementEventHandler ElementCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event StyleEventHandler StyleCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event ScriptEventHandler ScriptCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event CommentEventHandler CommentCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event ProcessingInstructionEventHandler ProcessingInstructionCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event TextEventHandler TextCreatedEvent;
    
    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 解析 Token

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void ResolveTokens()
        {
            System.Diagnostics.Debug.Assert(m_result != null);

            int nodeIDCount = 0;
            
            int visitor = 0;
            while(visitor < m_tokens.Count)
            {
                Token token = m_tokens[visitor];
                //=====================================================================================//
                if(token.Type == Token.TokenType.TagBegin) // Read Start Tag   
                {
                    ResolveStartTag(ref visitor, ref nodeIDCount);
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.TagCloseBegin) // Read End Tag
                {
                    ResolveEndTag(ref visitor);
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.Text) // Text
                {                    
                    StringBuilder builder = new StringBuilder();
                    do
                    {
                        builder.Append(token.Content);
                        ++ visitor;
                        token = m_tokens[visitor];
                    }
                    while(visitor < m_tokens.Count && token.Type == Token.TokenType.Text);

                    string text = builder.ToString();
                    text = DHtmlTextProcessor.ComapctWSCString(text);
                    text = DHtmlTextProcessor.TranHtmlTextToStr(text);

                    DHtmlText node = new DHtmlText(text);
                    ++nodeIDCount;
                    node.NodeID = nodeIDCount;
                    if(TextCreatedEvent != null) TextCreatedEvent(node);

                    m_currentLevel.Add(node);
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.Comment) // 一般 Comment
                {
                    DHtmlComment node = new DHtmlComment(token.Content);
                    ++nodeIDCount;
                    node.NodeID = nodeIDCount;
                    if(CommentCreatedEvent != null) CommentCreatedEvent(node);

                    m_currentLevel.Add(node);
                    ++visitor;
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.SGMLComment) // SGML Comment
                {
                    DHtmlComment node = new DHtmlComment(token.Content);
                    node.SGMLComment = true;
                    ++nodeIDCount;
                    node.NodeID = nodeIDCount;
                    if(CommentCreatedEvent != null) CommentCreatedEvent(node);

                    m_currentLevel.Add(node);
                    ++visitor;
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.ProcessingInstruction) // SGML Comment
                {
                    DHtmlProcessingInstruction node = new DHtmlProcessingInstruction(token.Content);
                    ++nodeIDCount;
                    node.NodeID = nodeIDCount;
                    if(ProcessingInstructionCreatedEvent != null) ProcessingInstructionCreatedEvent(node);

                    m_currentLevel.Add(node);
                    ++visitor;
                }
                //=====================================================================================//
                else if(token.Type == Token.TokenType.EndOfHtml)
                {   
                    /*
                    for(int index = m_result.Count - 1; index >= 0; --index)
                    {
                        DHtmlElement childElement = m_result[index] as DHtmlElement;
                        if(childElement != null && childElement.m_close == false)
                            CloseElement(childElement);
                    }                     
                    */

                    m_tokens.Clear();
                    break;
                }
                //=====================================================================================//
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="nodeIDCount"></param>
        /// <returns></returns>
        private void ResolveStartTag(ref int visitor, ref int nodeIDCount)
        {
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagBegin);
            ++visitor;
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName);

            string tagName = m_tokens[visitor].Content;
            switch(tagName)
            {
                // Paragraph
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "h5":
                case "h6":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "h1", "h2", "h3", "h4", "h5", "h6" } , new string[] { "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                case "p":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "p" }, new string[] { "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                // Table 
                case "td":
                case "th":
                    ResolveEndTagOptionalElement(new string[] { "td", "th" }, new string[] { "tr", "tbody", "thead", "tfoot", "table", "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                case "tr":
                    ResolveEndTagOptionalElement(new string[] { "td", "th" }, new string[] { "tr", "tbody", "thead", "tfoot", "table", "body" });
                    ResolveEndTagOptionalElement(new string[] { "tr" }, new string[] { "tbody", "thead", "tfoot", "table", "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                case "tbody":
                case "thead":
                case "tfoot":
                case "colgroup":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "td", "th" }, new string[] { "tr", "tbody", "thead", "tfoot", "table", "body" });
                    ResolveEndTagOptionalElement(new string[] { "tr" }, new string[] { "tbody", "thead", "tfoot", "table", "body" });
                    ResolveEndTagOptionalElement(new string[] { "tbody", "thead", "tfoot", "colgroup" }, new string[] { "table", "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;
                    
                // List
                case "li":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "li" }, new string[] { "ol", "ul", "menu", "dir", "body" });

                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                case "dt":
                case "dd":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "dt", "dd" }, new string[] { "dl", "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;

                // Forms 
                case "option":
                    // 處理 end tag 為 optional 的 tag
                    ResolveEndTagOptionalElement(new string[] { "option" }, new string[] { "select", "optgroup", "body" });
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;
                    
                // Special
                case "script":
                    ResolveScript(ref visitor, ref nodeIDCount);
                    break;
                case "style":
                    ResolveStyle(ref visitor, ref nodeIDCount);
                    break;
                    
                default:
                    ResolveElement(ref visitor, ref nodeIDCount);
                    break;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="nodeIDCount"></param>
        /// <returns></returns>
        private void ResolveElement(ref int visitor, ref int nodeIDCount)
        {
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName);
            
            DHtmlElement element = new DHtmlElement(m_tokens[visitor].Content);
            element.m_close = false;
            ++nodeIDCount;
            element.NodeID = nodeIDCount;

            ++visitor;
            ResolveAttribute(ref visitor, element.Attributes);

            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd || m_tokens[visitor].Type == Token.TokenType.TagCloseEnd);
            
            if(m_tokens[visitor].Type == Token.TokenType.TagEnd) // <tag>
            {
                switch(element.Name)
                {
                    // Empty tag
                    case "area":
                    case "bgsound":
                    case "base":
                    case "br":
                    case "basefont":
                    case "col":
                    case "embed":
                    case "frame":
                    case "hr":
                    case "img":
                    case "isindex":
                    case "input":
                    case "keygen":
                    case "link":
                    case "meta":
                    case "nextid":
                    case "option":
                    case "param":
                    case "sound":
                    case "spacer":
                    case "wbr":
                        element.TerminatedType = DHtmlElement.EndTagType.NonTerminated;
                        CloseElement(element);

                        if(ElementCreatedEvent != null) ElementCreatedEvent(element);
                        m_currentLevel.Add(element);
                        break;

                    default:
                        {
                            element.m_previousWithSameNameNode = (DHtmlElement)m_lastOpenNodes[element.Name];
                            m_lastOpenNodes[element.Name] = element;

                            if(ElementCreatedEvent != null) ElementCreatedEvent(element);
                            m_currentLevel.Add(element);
                            m_currentLevel = element.Nodes;
                        }
                        break;
                }     
            }
            else // <tag/>
            {                
                element.TerminatedType = DHtmlElement.EndTagType.Terminated;
                CloseElement(element);

                if(ElementCreatedEvent != null) ElementCreatedEvent(element);
                m_currentLevel.Add(element);
            }
                    
            ++visitor;                       
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="nodeIDCount"></param>
        /// <returns></returns>
        private void ResolveScript(ref int visitor, ref int nodeIDCount)
        {
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName && m_tokens[visitor].Content.Equals("script"));

            DHtmlScript script = new DHtmlScript();
            ++visitor;
            ResolveAttribute(ref visitor, script.Attributes);

            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd || m_tokens[visitor].Type == Token.TokenType.TagCloseEnd);
            if(m_tokens[visitor].Type == Token.TokenType.TagEnd)
            {
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.NoEscapingText);
                script.Script = m_tokens[visitor].Content;

                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagCloseBegin);
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName && m_tokens[visitor].Content.Equals("script"));
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd);

                ++visitor;
            }

            ++nodeIDCount;
            script.NodeID = nodeIDCount;
            if(ScriptCreatedEvent != null) ScriptCreatedEvent(script);

            m_currentLevel.Add(script);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="nodeIDCount"></param>
        /// <returns></returns>
        private void ResolveStyle(ref int visitor, ref int nodeIDCount)
        {
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName && m_tokens[visitor].Content.Equals("style"));

            DHtmlStyle style = new DHtmlStyle();
            ++visitor;
            ResolveAttribute(ref visitor, style.Attributes);

            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd || m_tokens[visitor].Type == Token.TokenType.TagCloseEnd);
            if(m_tokens[visitor].Type == Token.TokenType.TagEnd)
            {
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.NoEscapingText);
                style.Style = m_tokens[visitor].Content;

                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagCloseBegin);
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName && m_tokens[visitor].Content.Equals("style"));
                ++visitor;
                System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd);

                ++visitor;
            }

            ++nodeIDCount;
            style.NodeID = nodeIDCount;
            if(StyleCreatedEvent != null) StyleCreatedEvent(style);

            m_currentLevel.Add(style);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="attributeCollection"></param>
        private void ResolveAttribute(ref int visitor, DHtmlAttributeCollection attributeCollection)
        {
            Token token = m_tokens[visitor];

            // read the attributes and values
            while(token.Type != Token.TokenType.TagEnd && token.Type != Token.TokenType.TagCloseEnd)
            {
                System.Diagnostics.Debug.Assert(token.Type == Token.TokenType.AttributeName);
                string attribute_name = token.Content;
                ++visitor;
                token = m_tokens[visitor];
                if(token.Type == Token.TokenType.AttributeValue)
                {
                    string attribute_value = DHtmlTextProcessor.TranHtmlTextToStr(token.Content);
                    DHtmlAttribute attribute = new DHtmlAttribute(attribute_name, attribute_value);
                    attributeCollection.Add(attribute);
                    ++visitor;
                }
                else
                {
                    // Null-value attribute
                    DHtmlAttribute attribute = new DHtmlAttribute(attribute_name);
                    attributeCollection.Add(attribute);
                }

                token = m_tokens[visitor];
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="nodeIDCount"></param>
        /// <returns></returns>
        private void ResolveEndTag(ref int visitor)
        {
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagCloseBegin);
            ++visitor;
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagName);

            string tagName = m_tokens[visitor].Content;
            DHtmlElement openElement = (DHtmlElement)m_lastOpenNodes[tagName];

            // "000<b>111<a>222</b>333</a>444" will be transformed into "000<b>111<a>222</a></b>333444".
            // The end tag "</a> will be ignored.

            if(openElement != null) // If open tag is not found, we ignore the end tag
            {
                openElement.TerminatedType = DHtmlElement.EndTagType.ExplicitlyTerminated;
                CloseElement(openElement);

                if(openElement.Parent != null)
                    m_currentLevel = openElement.Parent.Nodes;
                else m_currentLevel = m_result;
            }

            ++visitor;
            System.Diagnostics.Debug.Assert(m_tokens[visitor].Type == Token.TokenType.TagEnd);

            ++visitor;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="boundElements"></param>
        private void ResolveEndTagOptionalElement(string[] targetElements, string[] boundElements)
        {
            System.Diagnostics.Debug.Assert(targetElements != null);
            System.Diagnostics.Debug.Assert(boundElements != null);

            for(int targetIndex = 0, targetCount = targetElements.Length; targetIndex < targetCount; ++targetIndex)
            {
                // Find open element
                DHtmlElement openElement = (DHtmlElement)m_lastOpenNodes[targetElements[targetIndex]];
                if(openElement != null)
                {
                    bool fixNestedElement = true;
                    DHtmlElement boundElement = null;

                    // Find bound element
                    for(int index = 0, count = boundElements.Length; index < count; ++index)
                    {
                        boundElement = (DHtmlElement)m_lastOpenNodes[boundElements[index]];
                        if(boundElement != null &&
                           boundElement.m_close == false && boundElement.NodeID > openElement.NodeID)
                        {
                            // The bound element is found, and the "tagName" can cross the bound element to 
                            // match the open element
                            fixNestedElement = false;
                            break;
                        }
                    }

                    if(fixNestedElement == true)
                    {
                        DHtmlElement parent = openElement.Parent;

                        CloseElement(openElement);

                        if(openElement.Parent != null)
                            m_currentLevel = openElement.Parent.Nodes;
                        else m_currentLevel = m_result;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        private void CloseElement(DHtmlElement element)
        {
            System.Diagnostics.Debug.Assert(element != null);
            System.Diagnostics.Debug.Assert(element.m_close == false);
            
            /*
            for(int index = element.Nodes.Count - 1; index >= 0; --index)
            {
                DHtmlElement childElement = element.Nodes[index] as DHtmlElement;
                if(childElement != null && childElement.m_close == false)
                    CloseElement(childElement);
            }     */       

            if(m_lastOpenNodes[element.Name] == element)
                m_lastOpenNodes[element.Name] = element.m_previousWithSameNameNode;
            
            element.m_close = true; 
            element.m_previousWithSameNameNode = null;

        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 分割 Token

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        private void ParseTokens()
        {
            List<Token> subTokens = new List<Token>(64);

            int index = 0, newIndex = 0;
            while(index < m_input.Length)
            {
                subTokens.Clear();

                // End tag
                if(index + 1 < m_input.Length && m_input.IndexOf("</", index, 2) != -1)
                    newIndex = ParseEndTagTokens(index, subTokens);
                // Comment
                else if(index + 2 < m_input.Length && m_input.IndexOf("<!--", index, 4) != -1)
                    newIndex = ParseCommentTagTokens(index, subTokens);
                // SGML Comment
                else if(index + 1 < m_input.Length && m_input.IndexOf("<!", index, 2) != -1)
                    newIndex = ParseSGMLCommentTagTokens(index, subTokens);
                // Processing Instruction
                else if(index + 1 < m_input.Length && m_input.IndexOf("<?", index, 2) != -1)
                    newIndex = ParseProcessingInstructionTagTokens(index, subTokens);
                // Start tag
                else if(index < m_input.Length && m_input.IndexOf("<", index, 1) != -1)
                    newIndex = ParseStartTagTokens(index, subTokens);

                if(index == newIndex) // Text
                {
                    int textEndIndex = index;
                    if(m_input[index].Equals('<')) // 已知目前 '<' 為文字的並非 tag 開頭, 找尋其他 tag 的 '<' 開頭
                    {
                        if(index + 1 < m_input.Length)
                            textEndIndex = m_input.IndexOf("<", index + 1);
                        else textEndIndex = -1;
                    }
                    else textEndIndex = m_input.IndexOf("<", index); // 找尋 tag 的 '<' 開頭

                    string text = "";
                    if(textEndIndex != -1) // 找到其他 tag, 將 tag 前的所有內容是為 Text
                    {
                        text = m_input.Substring(index, textEndIndex - index);
                        index = textEndIndex;
                    }
                    else // 沒有找到其他 tag 將所有剩下的內視為 Text 也就是最後一個 text tag
                    {
                        text = m_input.Substring(index);
                        index = m_input.Length;
                    }

                    if(text.Length > 0) subTokens.Add(new Token(Token.TokenType.Text, text));
                }
                else index = newIndex;

                //lock(m_tokens)
                {
                    m_tokens.AddRange(subTokens);
                }

                
            }
            //lock(m_tokens)
            {
                m_tokens.Add(Token.EndOfHtml);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseStartTagTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "<"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(m_input[index].Equals('<') == false) throw new ParseExeception(); // 最前面必須是 '<'
                index = index + 1;
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                if(m_input[index].Equals('>')) // The tag is "<>" tag
                    index = index + 1;
                else
                {
                    // 取得 tag 名稱
                    if(DHtmlTextProcessor.EqualesOfAnyChar(m_input[index], DHtmlTextProcessor.WhiteSpaceChars + "\'\"=/<?")) throw new ParseExeception(); // 第一個字元不可以是泛型空白或是 '\'' '\"' '=' '/' '<' '?'
                    int tag_name_start = index++;
                    while(index < m_input.Length && !DHtmlTextProcessor.EqualesOfAnyChar(m_input[index], DHtmlTextProcessor.WhiteSpaceChars + "/>")) index = index + 1;  // 找到結尾 泛型空白字元 與 '/' '>'
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                    int tag_name_end = index - 1;
                    string tagName = m_input.Substring(tag_name_start, tag_name_end - tag_name_start + 1).ToLower();

                    while(index < m_input.Length && DHtmlTextProcessor.IsWhiteSpaceChar(m_input[index])) index = index + 1;  // 跳過泛型空白字元
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                    if(m_input[index].Equals('>')) // 是結尾 '>'
                    {
                        index = index + 1;
                        tokens.Add(Token.TagBegin);
                        tokens.Add(new Token(Token.TokenType.TagName, tagName));
                        tokens.Add(Token.TagEnd);
                    }
                    else if(index + 1 < m_input.Length && m_input.Substring(index, 2).Equals("/>")) // 是結尾 '/>'
                    {
                        index = index + 2;
                        tokens.Add(Token.TagBegin);
                        tokens.Add(new Token(Token.TokenType.TagName, tagName));
                        tokens.Add(Token.TagCloseEnd);                        
                    }
                    else
                    {
                        tokens.Add(Token.TagBegin);
                        tokens.Add(new Token(Token.TokenType.TagName, tagName));

                        int newIndex = ParseAttributeTokens(index, tokens);
                        if(newIndex == index) throw new ParseExeception();
                        index = newIndex;
                    }

                    // 解析 script 與 style
                    if((tagName.Equals("script") || tagName.Equals("style")) && tokens[tokens.Count - 1].Type == Token.TokenType.TagEnd)
                        index = ParseNoEscapingTagTokens(index, tokens, tagName);
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseStartTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private int ParseNoEscapingTagTokens(int index, List<Token> tokens, string tagName)
        {
            int startIndex = index;

            try
            {
                if(index < m_input.Length)
                {
                    int noEscapingEndIndex = m_input.IndexOf("</", index);
                    while(noEscapingEndIndex != -1) // No Escaping End Tag 邊界條件不同於一般的 Tag
                    {
                        // Read tag name
                        int tag_name_start = noEscapingEndIndex + 2;
                        int tag_name_end = tag_name_start;
                        while(tag_name_end < m_input.Length && !DHtmlTextProcessor.EqualesOfAnyChar(m_input[tag_name_end], DHtmlTextProcessor.WhiteSpaceChars + "/>")) tag_name_end = tag_name_end + 1;  // 找到結尾 泛型空白字元 與 '/' '>'
                        if(tag_name_end >= m_input.Length)
                        {
                            noEscapingEndIndex = -1;
                            break;
                        }

                        tag_name_end = tag_name_end - 1;
                        if(tag_name_end - tag_name_start + 1 > 0 &&
                           m_input.Substring(tag_name_start, tag_name_end - tag_name_start + 1).ToLower().Equals(tagName))
                            break; // 找到 邊界條件

                        noEscapingEndIndex = m_input.IndexOf("</", noEscapingEndIndex + 2);
                    }

                    if(noEscapingEndIndex == -1) // 沒有找到 End Tag 將所有剩下的內視為 no escaping tag 的一部份
                    {
                        tokens.Add(new Token(Token.TokenType.NoEscapingText, m_input.Substring(index)));
                        index = m_input.Length;

                        // 自動補齊
                        tokens.Add(Token.TagCloseBegin);
                        tokens.Add(new Token(Token.TokenType.TagName, tagName));
                        tokens.Add(Token.TagEnd);
                    }
                    else
                    {
                        tokens.Add(new Token(Token.TokenType.NoEscapingText, m_input.Substring(index, noEscapingEndIndex - index)));
                        index = noEscapingEndIndex;

                        int newIndex = ParseEndTagTokens(index, tokens);
                        if(newIndex == index) throw new ParseExeception();
                        index = newIndex;
                    }
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseNoEscapingTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseEndTagTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "</"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(index + 1 < m_input.Length && m_input.IndexOf("</", index, 2) == -1) throw new ParseExeception(); // 最前面必須是 '</'                
                index = index + 2;
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                if(m_input[index].Equals('>')) // The tag is "</>" tag    
                    index = index + 1;
                else
                {
                    // 取得 tag 名稱           
                    if(DHtmlTextProcessor.EqualesOfAnyChar(m_input[index], DHtmlTextProcessor.WhiteSpaceChars + "\'\"=/<?")) throw new ParseExeception(); // 第一個字元不可以是泛型空白或是 '\'' '\"' '=' '/' '<' '?'
                    int tag_name_start = index++;
                    while(index < m_input.Length && !DHtmlTextProcessor.EqualesOfAnyChar(m_input[index], DHtmlTextProcessor.WhiteSpaceChars + "/>")) index = index + 1;  // 找到結尾 泛型空白字元 與 '/' '>' 
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                    int tag_name_end = index - 1;
                    string tagName = m_input.Substring(tag_name_start, tag_name_end - tag_name_start + 1).ToLower();

                    // 邊界條件為 ">" 其他意外的資訊可以完全略過
                    while(index < m_input.Length && m_input[index].Equals('>') == false) index = index + 1; // 跳過所有非 ">" 的字元
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                    tokens.Add(Token.TagCloseBegin);
                    tokens.Add(new Token(Token.TokenType.TagName, tagName));
                    tokens.Add(Token.TagEnd);
                }

                index = index + 1;
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseEndTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseCommentTagTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "<!--"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(index + 3 < m_input.Length && m_input.IndexOf("<!--", index, 4) == -1) throw new ParseExeception(); // 最前面必須是 '<!--'
                index = index + 4;

                if(index < m_input.Length)
                {
                    // 邊界條件為 "-->" 
                    int comment_start = index;
                    index = m_input.IndexOf("-->", comment_start);
                    int comment_end = index - 1;
                    if(index == -1) // End comment tag "-->" is not found
                    {
                        string comment = m_input.Substring(comment_start);
                        tokens.Add(new Token(Token.TokenType.Comment, comment));
                        index = m_input.Length;
                    }
                    else 
                    {                        
                        string comment = m_input.Substring(comment_start, comment_end - comment_start + 1);
                        tokens.Add(new Token(Token.TokenType.Comment, comment));
                        index = index + 3;
                    }
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseCommentTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseSGMLCommentTagTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "<!"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(index + 1 < m_input.Length && m_input.IndexOf("<!", index, 2) == -1) throw new ParseExeception(); // 最前面必須是 '<!'
                index = index + 2;

                if(index < m_input.Length)
                {
                    // 邊界條件為 ">" 
                    int comment_start = index;
                    index = m_input.IndexOf(">", comment_start);
                    int comment_end = index - 1;
                    if(index == -1) // End comment tag ">" is not found
                    {
                        string comment = m_input.Substring(comment_start);
                        tokens.Add(new Token(Token.TokenType.SGMLComment, comment));
                        index = m_input.Length;
                    }
                    else 
                    {
                        string comment = m_input.Substring(comment_start, comment_end - comment_start + 1);
                        tokens.Add(new Token(Token.TokenType.SGMLComment, comment));
                        index = index + 1;
                    }
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseSGMLCommentTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseProcessingInstructionTagTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "<?"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(index + 1 < m_input.Length && m_input.IndexOf("<?", index, 2) == -1) throw new ParseExeception(); // 最前面必須是 '<'
                index = index + 2;

                if(index < m_input.Length)
                {
                    // 邊界條件為 "?>" 
                    int comment_start = index;
                    index = m_input.IndexOf("?>", comment_start);
                    int comment_end = index - 1;
                    if(index == -1) // End comment tag "?>" is not found
                    {
                        string pi = m_input.Substring(comment_start);
                        tokens.Add(new Token(Token.TokenType.ProcessingInstruction, pi));
                        index = m_input.Length;
                    }
                    else 
                    {
                        string pi = m_input.Substring(comment_start, comment_end - comment_start + 1);
                        tokens.Add(new Token(Token.TokenType.ProcessingInstruction, pi));
                        index = index + 2;
                    }
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseProcessingInstructionTagTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }        

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_input"></param>
        /// <param name="index"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private int ParseAttributeTokens(int index, List<Token> tokens)
        {
            int startIndex = index;

            try
            {
                for(; ; )
                {
                    // 邊界條件是 ">" 或是 "/>" 字串
                    while(index < m_input.Length && DHtmlTextProcessor.IsWhiteSpaceChar(m_input[index])) ++index; // 跳過泛型空白字元
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束                    

                    if(m_input[index].Equals('>')) // 是結尾 '>'
                    {
                        index = index + 1;
                        tokens.Add(Token.TagEnd);
                        break;
                    }
                    else if(index + 1 < m_input.Length && m_input.Substring(index, 2).Equals("/>")) // 是結尾 '/>'
                    {
                        index = index + 2;
                        tokens.Add(Token.TagCloseEnd);
                        break;
                    }
                    else if(DHtmlTextProcessor.IsVaildAttribueNameChar(m_input[index]) == false) // 意外出現 '\'' '\"' 字元 跳過
                    {
                        ++index;
                        continue;
                    }

                    // 讀取屬性名稱                    
                    int attribute_name_start = index ++;
                    while(index < m_input.Length && DHtmlTextProcessor.IsVaildAttribueNameChar(m_input[index])) index = index + 1;  // 找到結尾 泛型空白字元 與 '/' '<' '>' '=' '\'' '\"'
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束                    

                    int attribute_name_end = index - 1;
                    string attributeName = m_input.Substring(attribute_name_start, attribute_name_end - attribute_name_start + 1);
                    tokens.Add(new Token(Token.TokenType.AttributeName, attributeName));
                    while(index < m_input.Length && DHtmlTextProcessor.IsWhiteSpaceChar(m_input[index])) ++index; // 跳過泛型空白字元
                    if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                    // read the attribute value
                    if(m_input[index].Equals('=')) // if the attribute is not null-attribute
                    {
                        index = index + 1;
                        while(index < m_input.Length && DHtmlTextProcessor.IsWhiteSpaceChar(m_input[index])) ++index; // 跳過泛型空白字元
                        if(index >= m_input.Length) throw new ParseExeception(); // 意外結束 

                        int value_start = 0, value_end = 0;
                        if(m_input[index].Equals('\"') || m_input[index].Equals('\'')) // 以 "xxx" 'xxx' 包覆的屬性值
                        {
                            char checkpoint = m_input[index];
                            index = index + 1;
                            if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                            value_start = index;

                            index = m_input.IndexOf(checkpoint, index);
                            if(index == -1) throw new ParseExeception(); // 意外結束     

                            value_end = index - 1;
                            index = index + 1;
                        }
                        else
                        {
                            // get value
                            value_start = index;
                            while(index < m_input.Length && DHtmlTextProcessor.IsVaildAttribueValueChar(m_input[index])) ++index; // 找尋屬性值結尾
                            if(index >= m_input.Length) throw new ParseExeception();// 意外結束                                                            
                            value_end = index - 1;
                        }

                        if(value_end - value_start + 1 > 0) // the attribute value is not empty
                            tokens.Add(new Token(Token.TokenType.AttributeValue, m_input.Substring(value_start, value_end - value_start + 1)));
                    }
                }
            }
            catch(ParseExeception)
            {
                System.Diagnostics.Debug.Write("ParseAttributeTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();
            }

            return index;
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region	內部資料

        /// <summary>
        /// 
        /// </summary>
        private string m_input = null;
		/// <summary>
		/// 
		/// </summary>
        private List<Token> m_tokens = new List<Token>(16384);
        /// <summary>
        /// 
        /// </summary>
        private DHtmlNodeCollection m_result = null;
        /// <summary>
        /// 
        /// </summary>
        private Hashtable m_lastOpenNodes = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        private DHtmlNodeCollection m_currentLevel = null;

    #endregion	

	}
}


