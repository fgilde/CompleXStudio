
/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DCssResolver Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DCssResolver.cs: implementation of the DCssResolver class.
//
///////////////////////////////////////////////////////////////////////////////using System;
using System.Text;
using System.Collections.Generic;

namespace DOL.DHtml.DCssResolver
{
    /// <summary>
    /// 
    /// </summary>
    public class DCssResolver
    {

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部結構

        // Tokenization
        //IDENT         {ident} 
        //ATKEYWORD     @{ident} 
        //STRING        {string} 
        //INVALID       {invalid} 
        //HASH          #{name} 
        //NUMBER        {num} 
        //PERCENTAGE    {num}% 
        //DIMENSION     {num}{ident} 
        //URI           url\({w}{string}{w}\)
        //              |url\({w}([!#$%&*-~]|{nonascii}|{escape})*{w}\) 
        //UNICODE-RANGE U\+[0-9a-f?]{1,6}(-[0-9a-f]{1,6})? 
        //CDO           <!-- 
        //CDC           --> 
        //;             ; 
        //{             \{ 
        //}             \} 
        //(             \( 
        //)             \) 
        //[             \[ 
        //]             \] 
        //S             [ \t\r\n\f]+ 
        //COMMENT       \/\*[^*]*\*+([^/*][^*]*\*+)*\/ 
        //FUNCTION      {ident}\( 
        //INCLUDES      ~= 
        //DASHMATCH     |= 
        //DELIM         any other character not matched by the above rules, and neither a single nor a double quote  

        // Macro  Definition          
        //
        //ident     [-]?{nmstart}{nmchar}* 
        //name      {nmchar}+ 
        //nmstart   [_a-z]|{nonascii}|{escape} 
        //nonascii  [^\0-\177] 
        //unicode   \\[0-9a-f]{1,6}(\r\n|[ \n\r\t\f])? 
        //escape    {unicode}|\\[^\n\r\f0-9a-f] 
        //nmchar    [_a-z0-9-]|{nonascii}|{escape} 
        //num       [0-9]+|[0-9]*\.[0-9]+ 
        //string    {string1}|{string2} 
        //string1   \"([^\n\r\f\\"]|\\{nl}|{escape})*\" 
        //string2   \'([^\n\r\f\\']|\\{nl}|{escape})*\' 
        //invalid   {invalid1}|{invalid2} 
        //invalid1  \"([^\n\r\f\\"]|\\{nl}|{escape})* 
        //invalid2  \'([^\n\r\f\\']|\\{nl}|{escape})* 
        //nl        \n|\r\n|\r|\f 
        //w         [ \t\r\n\f]* 

        //stylesheet  : [ CDO | CDC | S | statement ]*;
        //statement   : ruleset | at-rule;
        //at-rule     : ATKEYWORD S* any* [ block | ';' S* ];
        //block       : '{' S* [ any | block | ATKEYWORD S* | ';' S* ]* '}' S*;
        //ruleset     : selector? '{' S* declaration? [ ';' S* declaration? ]* '}' S*;
        //selector    : any+;
        //declaration : DELIM? property S* ':' S* value;
        //property    : IDENT;
        //value       : [ any | block | ATKEYWORD S* ]+;
        //any         : [ IDENT | NUMBER | PERCENTAGE | DIMENSION | STRING
        //              | DELIM | URI | HASH | UNICODE-RANGE | INCLUDES
        //              | DASHMATCH | FUNCTION S* any* ')' 
        //              | '(' S* any* ')' | '[' S* any* ']' ] S*;


        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        protected class StyleSheetToken
        {
            public enum TokenType
            {
                None,
                Selector,                    // any+;
                PropertyList,                // S* declaration? [ ';' S* declaration? ]*;
                AtRule,                      // ATKEYWORD S* any* [ block | ';' S* ];
            }

            public StyleSheetToken(TokenType type, string value)
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
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        protected enum PropertyListTokenType
        {
            Name,                      
            Value,
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        protected class ParseExeception : System.Exception
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
        public DCssResolver()
        {
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 解析操作
        
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Resolve(string styleSheet, List<DCssSelector> result)
        {
            System.Diagnostics.Debug.Assert(styleSheet != null);
            System.Diagnostics.Debug.Assert(result != null);

            m_input = styleSheet;
            m_result = result;

            ParseTokens();
            ResolveTokens();

            m_result = null;
            m_input = null;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<DCssSelector> Resolve(string styleSheet)
        {
            System.Diagnostics.Debug.Assert(styleSheet != null);

            List<DCssSelector> result = new List<DCssSelector>();
            Resolve(styleSheet, result);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DCssSelector ResolveSelector(string input)
        {
            System.Diagnostics.Debug.Assert(input != null);

            DCssSelector selector = new DCssSelector();
            string[] selectorList = input.Split(DHtmlTextProcessor.WhiteSpaceCharsArray);

            StringBuilder builder = new StringBuilder();
            bool combinatorExist = false;

            for(int tokenIndex = 0, count = selectorList.Length; tokenIndex < count; ++tokenIndex)
            {
                string token = selectorList[tokenIndex];

                if(token.Equals('+')) // combinator +
                {
                    if(selector.Count != 0 && combinatorExist == false)
                    {
                        selector[selector.Count - 1].Combinator = DCssSimpleSelector.Relation.Sibling;
                        combinatorExist = true;
                    }
                    else throw new ParseExeception();
                }
                else if(token.Equals('>')) // combinator >
                {
                    if(selector.Count != 0 && combinatorExist == false)
                    {
                        selector[selector.Count - 1].Combinator = DCssSimpleSelector.Relation.Child;
                        combinatorExist = true;
                    }
                    else throw new ParseExeception();
                }
                else
                {
                    DCssSimpleSelector simpleSelector = new DCssSimpleSelector();

                    builder.Length = 0;
                    int index = 0;
                    while(index < token.Length)
                    {
                        if(token[index].Equals('#')) // id
                        {
                            index += 1;
                            if(index >= token.Length) throw new ParseExeception();

                            int nextIndex = token.IndexOfAny("#.:[".ToCharArray(), index);
                            if(nextIndex != -1)
                            {
                                simpleSelector.ID = token.Substring(index, nextIndex - index);
                                index = nextIndex;
                            }
                            else
                            {
                                simpleSelector.ID = token.Substring(index);
                                index = token.Length;
                            }
                        }

                        else if(token[index].Equals('.')) // class
                        {
                            index += 1;
                            if(index >= token.Length) throw new ParseExeception();

                            int nextIndex = token.IndexOfAny("#.:[".ToCharArray(), index);
                            if(nextIndex != -1)
                            {
                                simpleSelector.Class = token.Substring(index, nextIndex - index);
                                index = nextIndex;
                            }
                            else
                            {
                                simpleSelector.Class = token.Substring(index);
                                index = token.Length;
                            }
                        }
                        else if(token[index].Equals('[')) // attribute
                        {
                            System.Diagnostics.Debug.Assert(false);
                        }
                        else if(token[index].Equals(':')) // pseudo-x
                        {
                            index += 1;
                            if(index >= token.Length) throw new ParseExeception();

                            int nextIndex = token.IndexOfAny("#.:[".ToCharArray(), index);
                            if(nextIndex != -1)
                            {
                                simpleSelector.Pseudo = token.Substring(index, nextIndex - index);
                                index = nextIndex;
                            }
                            else
                            {
                                simpleSelector.Pseudo = token.Substring(index);
                                index = token.Length;
                            }
                        }
                        else // element name
                        {
                            int nextIndex = token.IndexOfAny("#.[:".ToCharArray(), index);
                            if(nextIndex != -1)
                            {
                                simpleSelector.ElementNames = token.Substring(index, nextIndex - index);
                                index = nextIndex;
                            }
                            else
                            {
                                simpleSelector.ElementNames = token.Substring(index);
                                index = token.Length;
                            }
                        }
                    }

                    selector.Add(simpleSelector);
                    combinatorExist = false;
                }
            }

            return selector;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void ResolvePropertyList(string input, DCssPropertyCollection result)
        {
            System.Diagnostics.Debug.Assert(input != null);
            System.Diagnostics.Debug.Assert(result != null);

            PropertyListTokenType type = PropertyListTokenType.Name;
            int index = 0;

            string name = "", value = "";
            while(index < input.Length) // PropertyList:  S* declaration? [ ';' S* declaration? ]*;
            {
                // declaration:  DELIM? property S* ':' S* value;
                int startIndex = 0;
                if(type == PropertyListTokenType.Name)
                    startIndex = input.IndexOfAny(":=".ToCharArray(), index);
                else if(type == PropertyListTokenType.Value)
                    startIndex = input.IndexOf(';', index);

                if(type == PropertyListTokenType.Name &&
                    startIndex != -1 && startIndex - index > 0 &&
                    (input[startIndex].Equals(':') || input[startIndex].Equals('=')))
                {
                    name = input.Substring(index, startIndex - index);
                    name = name.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);

                    if(name.Length > 0 &&
                       DHtmlTextProcessor.ExistWhiteSpaceChar(name) == false)
                        type = PropertyListTokenType.Value;
                }
                else if(type == PropertyListTokenType.Value &&
                        startIndex != -1 && startIndex - index > 0 &&
                        input[startIndex].Equals(';'))
                {
                    value = input.Substring(index, startIndex - index);
                    value = value.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);

                    if(value.Length > 0)
                    {
                        DCssProperty property = new DCssProperty(name, value);
                        result.AddLast(property);
                    }

                    type = PropertyListTokenType.Name;
                }
                else if(type == PropertyListTokenType.Value &&
                         startIndex == -1)
                {
                    value = input.Substring(index);
                    value = value.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);

                    if(value.Length > 0)
                    {
                        DCssProperty property = new DCssProperty(name, value);
                        result.AddLast(property);
                    }

                    type = PropertyListTokenType.Name;
                }
                else type = PropertyListTokenType.Name;

                if(startIndex == -1) index = input.Length;
                else index = startIndex + 1;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DCssPropertyCollection ResolvePropertyList(string input)
        {
            System.Diagnostics.Debug.Assert(input != null);

            DCssPropertyCollection result = new DCssPropertyCollection();
            ResolvePropertyList(input, result);

            return result;
        }
        
    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 解析 Token

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        protected void ResolveTokens()
        {
            System.Diagnostics.Debug.Assert(m_result != null);

            int visitor = 0;
            List<DCssSelector> selectorList = new List<DCssSelector>();
            while(visitor < m_tokens.Count)
            {
                StyleSheetToken token = m_tokens[visitor];
                //=====================================================================================//
                if(token.Type == StyleSheetToken.TokenType.Selector) 
                {
                    selectorList.Add(ResolveSelector(m_tokens[visitor].Content));
                    ++visitor;
                }
                //=====================================================================================//
                else if(token.Type == StyleSheetToken.TokenType.PropertyList)
                {
                    System.Diagnostics.Debug.Assert(selectorList.Count > 0);
                    DCssSelector selector = selectorList[0];
                    ResolvePropertyList( m_tokens[visitor].Content, selector.Properties);
                    ++visitor;

                    for(int index = 1; index < selectorList.Count; ++index)
                    {
                        DCssSelector cloneSelector = selectorList[index];
                        int propertyCount = selector.Properties.Count;
                        for(int propertyIndex = 0; propertyIndex < propertyCount; ++propertyIndex)
                            cloneSelector.Properties.AddLast((DCssProperty)selector.Properties[propertyIndex].Clone());
                    }

                    m_result.AddRange(selectorList);
                    selectorList.Clear();
                }
                else System.Diagnostics.Debug.Assert(false);
            }

            m_tokens.Clear();
        }
        
    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 分割 Token

        /////////////////////////////////////////////////////////////////////////////////
        protected void ParseTokens()
        {
            List<StyleSheetToken> subTokens = new List<StyleSheetToken>(8);

            int index = 0;
            while(index < m_input.Length)
            {
                try
                {
                    // While Space
                    if(index < m_input.Length && DHtmlTextProcessor.IsWhiteSpaceChar(m_input[index]) == true)
                        index += 1;
                    // CDO
                    else if(index + 3 < m_input.Length && m_input.IndexOf("<!--", index, 4) != -1)
                        index += 4;
                    // CDC
                    else if(index + 2 < m_input.Length && m_input.IndexOf("-->", index, 3) != -1)
                        index += 3;
                    // Comment
                    else if(index + 1 < m_input.Length && m_input.IndexOf("/*", index, 2) != -1)
                        index = ParseComment(index, subTokens);
                    // At-Rule
                    else if(index < m_input.Length && m_input[index].Equals('@') == true)
                        index = ParseAtRule(index, subTokens);                        
                    // RuleSet     
                    else index = ParseRuleSet(index, subTokens);

                    m_tokens.AddRange(subTokens);
                }
                catch(ParseExeception)
                {
                    System.Diagnostics.Debug.Write("ParseTokens parse exeception\n");
                    break;
                }                
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        protected int ParseAtRule(int index, List<StyleSheetToken> tokens)
        {
            int startIndex = index;

            try
            {
                // 4.1.5, 4.1.6
                ++index;
                int blockLevel = 0;
                for(;;)
                {
                    if(index >= m_input.Length)
                        throw new ParseExeception();
                    if(index < m_input.Length && m_input[index].Equals(';') == true)
                    {
                        index += 1;
                        if(blockLevel == 0) break;
                    }
                    if(index < m_input.Length && m_input[index].Equals('{') == true)
                    {
                        index += 1;
                        ++blockLevel;
                    }
                    else if(index < m_input.Length && m_input[index].Equals('}') == true)
                    {
                        index += 1;
                        --blockLevel;
                        if(blockLevel == 0) break;
                    }
                    else if(index + 1 < m_input.Length && m_input.IndexOf("/*", index, 2) != -1)
                        index = ParseComment(index, tokens);
                    else index += 1;
                }    

            }
            catch(ParseExeception exeception)
            {
                System.Diagnostics.Debug.Write("ParseAtRule parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();

                throw exeception;
            }

            return index;
        }
        /////////////////////////////////////////////////////////////////////////////////
        protected int ParseRuleSet(int index, List<StyleSheetToken> tokens)
        {
            int startIndex = index;

            try
            {
                // Selector
                StringBuilder builder = new StringBuilder();
                for(;;)
                {
                    if(index >= m_input.Length)
                        throw new ParseExeception();
                    if(index < m_input.Length && m_input[index].Equals('{') == true)
                    {
                        index += 1;

                        string selector = builder.ToString();
                        selector = selector.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);
                        if(selector.Length == 0) throw new ParseExeception();
                        m_tokens.Add(new StyleSheetToken(StyleSheetToken.TokenType.Selector, selector));
                        builder.Length = 0;

                        break;
                    }
                    else if(index < m_input.Length && m_input[index].Equals(',') == true)
                    {
                        index += 1;

                        string selector = builder.ToString();
                        selector = selector.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);
                        if(selector.Length == 0) throw new ParseExeception();
                        m_tokens.Add(new StyleSheetToken(StyleSheetToken.TokenType.Selector, selector));
                        builder.Length = 0;
                    }
                    else if(index + 1 < m_input.Length && m_input.IndexOf("/*", index, 2) != -1)
                    {
                        index = ParseComment(index, tokens);
                        builder.Append(" ");
                    }
                    else
                    {
                        builder.Append(m_input[index]);
                        index += 1;
                    }
                }                

                // PropertyList              
                for(;;)
                {
                    if(index >= m_input.Length)
                        throw new ParseExeception();
                    if(index < m_input.Length && m_input[index].Equals('{') == true)
                        throw new ParseExeception();
                    if(index < m_input.Length && m_input[index].Equals('}') == true)
                    {
                        index += 1;
                        break;
                    }
                    else if(index + 1 < m_input.Length && m_input.IndexOf("/*", index, 2) != -1)
                    {
                        index = ParseComment(index, tokens);
                        builder.Append(" ");
                    }
                    else
                    {
                        builder.Append(m_input[index]);
                        index += 1;
                    }
                }

                string propertyList = builder.ToString();
                propertyList = propertyList.Trim(DHtmlTextProcessor.WhiteSpaceCharsArray);
                m_tokens.Add(new StyleSheetToken(StyleSheetToken.TokenType.PropertyList, propertyList));
            }
            catch(ParseExeception exeception)
            {
                System.Diagnostics.Debug.Write("ParseRuleSet parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");

                index = startIndex;
                tokens.Clear();

                throw exeception;
            }

            return index;
        }

        /////////////////////////////////////////////////////////////////////////////////
        protected int ParseComment(int index, List<StyleSheetToken> tokens)
        {
            int startIndex = index;

            try
            {
                // 起始條件為 "/*"
                if(index >= m_input.Length) throw new ParseExeception(); // 意外結束
                if(index + 1 < m_input.Length && m_input.IndexOf("/*", index, 2) == -1) throw new ParseExeception(); // 最前面必須是 '/*'
                index = index + 2;

                if(index < m_input.Length)
                {
                    // 邊界條件為 "*/" 
                    int comment_start = index;
                    index = m_input.IndexOf("*/", comment_start);
                    int comment_end = index - 1;
                    if(index == -1) // End comment tag "*/" is not found
                        index = m_input.Length;
                    else index = index + 2;
                }
            }
            catch(ParseExeception exeception)
            {
                System.Diagnostics.Debug.Write("ParseCommentTokens parse exeception: \"");
                if(index < m_input.Length) System.Diagnostics.Debug.Write(m_input.Substring(startIndex, index - startIndex + 1));
                else System.Diagnostics.Debug.Write(m_input.Substring(startIndex));
                System.Diagnostics.Debug.Write("\"\n");
                
                index = startIndex;
                tokens.Clear();

                throw exeception;
            }

            return index;
        }

    #endregion

	/////////////////////////////////////////////////////////////////////////////////
	#region	內部資料

        /// <summary>
        /// 
        /// </summary>
        protected string m_input = null;
		/// <summary>
		/// 
		/// </summary>
        protected List<StyleSheetToken> m_tokens = new List<StyleSheetToken>(512);
        /// <summary>
        /// 
        /// </summary>
        protected List<DCssSelector> m_result = null;

    #endregion	

    }
}
