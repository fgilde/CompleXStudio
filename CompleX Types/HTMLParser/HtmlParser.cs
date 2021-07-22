//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using CompleX_Settings.Constants;
using CompleX_Types.Extensions;

namespace CompleX_Types.HTMLParser
{
    /// <summary>
    /// This is the main HTML parser class. I recommend you don't play around too much in here
    /// as it's a little fiddly, an dbuggy... ;-) 
    /// 
    /// Bascially, this class will build a tree containing HtmlNode elements.
    /// </summary>
    public class HtmlParser
    {

        // ReSharper disable InconsistentNaming
        private static readonly char[] WHITESPACE_CHARS = " \t\r\n".ToCharArray();
        // ReSharper restore InconsistentNaming

        private bool mRemoveEmptyElementText;

        /// <summary>
        /// Internal FSM to represent the state of the parser
        /// </summary>
        private enum ParseStatus
        {
            ReadText = 0,
            ReadEndTag = 1,
            ReadStartTag = 2,
            ReadAttributeName = 3,
            ReadAttributeValue = 4
        };

        public static IEnumerable<string> FindOpenHtmlTags(string input)
        {

            var openHtmlTags = new Regex(@"<(?<tag>\w+)[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var closedHtmlTags = new Regex(@"</(?<tag>\w+)[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            var alsoClosedHtmlTags = new Regex(@"<(?<tag>\w+)[^>]*/>", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            List<Match> openHtmlTagsCollection = openHtmlTags.Matches(input).ToArray().ToList();
            List<Match> closedHtmlTagsCollection = closedHtmlTags.Matches(input).ToArray().ToList();
            closedHtmlTagsCollection.AddRange(alsoClosedHtmlTags.Matches(input).ToArray().ToList());
            List<string> open = openHtmlTagsCollection.Select(match => match.Groups["tag"].Value).ToList();
            List<string> closed = closedHtmlTagsCollection.Select(match => match.Groups["tag"].Value).ToList();
            foreach (var tag in closed)
                open.Remove(tag);
            open.Reverse();
            return open;
            //return open.Except(closed).Reverse();
        }

        public static IEnumerable<string> GetUsedHtmlHrefs(string html)
        {
 
            var result = new List<string>();
            var content = html;
            //insert hrefs
            var linkPattern = new Regex(Const.RegexHref, RegexOptions.IgnoreCase);
            MatchCollection collection = linkPattern.Matches(content);
            foreach (var matchCollection in collection)
            {
                string link = matchCollection.ToString().ToLower().Replace("href", String.Empty).Replace("\"", String.Empty).Substring(1);
                if (!result.Contains(link) && !String.IsNullOrWhiteSpace(link))
                    result.Add(link);
            }

            // Insert functions
            var funcpattern = new Regex(Const.RegexFunction, RegexOptions.IgnoreCase);
            MatchCollection collection2 = funcpattern.Matches(content);
            foreach (var matchCollection in collection2)
            {
                string func = matchCollection.ToString().Substring(9);
                if (!result.Contains("javascript:" + func))
                    result.Add("javascript:" + func);
            }
            return result;
        }

        /// <summary>
        /// The default mechanism will extract a pure DOM tree, which will contain many text
        /// nodes containing just whitespace (carriage returns etc.) However, with normal
        /// parsing, these are useless and only serve to complicate matters. Therefore, this
        /// option exists to automatically remove those empty text nodes.
        /// </summary>
        public bool RemoveEmptyElementText
        {
            get{return mRemoveEmptyElementText;}
            set{ mRemoveEmptyElementText = value;}
        }

        #region The main parser

        /// <summary>
        /// This will parse a string containing HTML and will produce a domain tree.
        /// </summary>
        /// <param name="html">The HTML to be parsed</param>
        /// <returns>A tree representing the elements</returns>
        public HtmlNodeCollection Parse(string html)
        {
            var nodes = new HtmlNodeCollection(null);

            html = PreprocessScript( html ,"script" );
            html = PreprocessScript( html ,"style" );

            html = RemoveComments( html );
            html = RemoveSgmlComments( html );
            StringCollection tokens = GetTokens( html );

            int index = 0;
            HtmlElement element = null;
            while( index < tokens.Count )
            {
                if( "<".Equals( tokens[index] ) )
                {
                    // Read open tag

                    index++;
                    if( index >= tokens.Count ) break;
                    string tagName = tokens[index];
                    index++;
                    element = new HtmlElement( tagName );
                    // read the attributes and values

                    while( index < tokens.Count && ! ">".Equals( tokens[index] ) && ! "/>".Equals( tokens[index] ) )
                    {
                        string attributeName = tokens[ index ];
                        index++;
                        if( index < tokens.Count && "=".Equals( tokens[ index ] ) )
                        {
                            index++;
                            string attributeValue = index < tokens.Count ? tokens[ index ] : null;
                            index++;
                            var attribute = new HtmlAttribute( attributeName , HtmlEncoder.DecodeValue( attributeValue ) );
                            element.Attributes.Add( attribute );
                        }
                        else if( index < tokens.Count )
                        {
                            // Null-value attribute
                            var attribute = new HtmlAttribute( attributeName , null );
                            element.Attributes.Add( attribute );
                        }
                    }
                    nodes.Add( element );
                    if( index < tokens.Count && "/>".Equals( tokens[ index ] ) )
                    {
                        element.IsTerminated = true;
                        index++;
                        element = null;
                    }
                    else if( index < tokens.Count && ">".Equals( tokens[ index ] ) )
                    {
                        index++;
                    }
                }
                else if( ">".Equals( tokens[index] ) )
                {
                    index++;
                }
                else if( "</".Equals( tokens[index] ) )
                {
                    // Read close tag
                    index++;
                    if( index >= tokens.Count ) break;
                    string tagName = tokens[index];
                    index++;

                    int openIndex = FindTagOpenNodeIndex( nodes , tagName );
                    if( openIndex != -1 )
                    {
                        MoveNodesDown( ref nodes , openIndex + 1 , (HtmlElement)nodes[openIndex] );
                    }

                    // Skip to the end of this tag
                    while( index < tokens.Count && ! ">".Equals( tokens[ index ] ) )
                    {
                        index++;
                    }
                    if( index < tokens.Count && ">".Equals( tokens[ index ] ) )
                    {
                        index++;
                    }

                    element = null;
                }
                else
                {
                    // Read text
                    string value = tokens[ index ];
                    if( mRemoveEmptyElementText )
                    {
                        value = RemoveWhitespace( value );
                    }
                    value = DecodeScript( value );

                    if( mRemoveEmptyElementText && value.Length == 0 )
                    {
                        // We do nothing
                    }
                    else
                    {
                        if( ! ( element != null && element.NoEscaping ) )
                        {
                            value = HtmlEncoder.DecodeValue( value );
                        }
                        var node = new HtmlText( value );
                        nodes.Add( node );
                    }
                    index++;
                }
            }
            return nodes;
        }

        /// <summary>
        /// This will move all the nodes from the specified index to the new parent.
        /// </summary>
        /// <param name="nodes">The collection of nodes</param>
        /// <param name="nodeIndex">The index of the first node (in the above collection) to move</param>
        /// <param name="newParent">The node which will become the parent of the moved nodes</param>

        private static void MoveNodesDown(ref HtmlNodeCollection nodes,int nodeIndex,HtmlElement newParent)
        {
            for( int i = nodeIndex ; i < nodes.Count ; i++ )
            {
                newParent.Nodes.Add( nodes[i] );
                nodes[i].SetParent( newParent );
            }
            int c = nodes.Count;
            for( int i = nodeIndex ; i < c ; i++ )
            {
                nodes.RemoveAt( nodeIndex );
            }
            newParent.IsExplicitlyTerminated = true;
        }

        /// <summary>
        /// This will find the corresponding opening tag for the named one. This is identified as
        /// the most recently read node with the same name, but with no child nodes.
        /// </summary>
        /// <param name="nodes">The collection of nodes</param>
        /// <param name="name">The name of the tag</param>
        /// <returns>The index of the opening tag, or -1 if it was not found</returns>
        private static int FindTagOpenNodeIndex(HtmlNodeCollection nodes,string name)
        {
            for( int index = nodes.Count - 1 ; index >= 0 ; index-- )
            {
                if( nodes[index] is HtmlElement )
                {
                    if( ( (HtmlElement) nodes[index] ).Name.ToLower().Equals( name.ToLower() ) && ( (HtmlElement) nodes[index] ).Nodes.Count == 0 && ( (HtmlElement) nodes[index] ).IsTerminated == false )
                    {
                        return index;
                    }
                }
            }
            return -1;
        }

        #endregion

        #region HTML clean-up functions

        /// <summary>
        /// This will remove redundant whitespace from the string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string RemoveWhitespace(string input)
        {
            string output = input.Replace( "\r" , "" );
            output = output.Replace( "\n" , "" );
            output = output.Replace( "\t" , " " );
            output = output.Trim();
            return output;
        }

        /// <summary>
        /// This will remove all HTML comments from the input string. This will
        /// not remove comment markers from inside tag attribute values.
        /// </summary>
        /// <param name="input">Input HTML containing comments</param>
        /// <returns>HTML containing no comments</returns>

        private static string RemoveComments(string input)
        {
            var output = new StringBuilder();

            int i = 0;
            bool inTag = false;

            int startIndex = i;
            while( i < input.Length )
            {
                if( i + 4 < input.Length && input.Substring( i , 4 ).Equals( "<!--" ) )
                {
                    i += 4;
                    i = input.IndexOf( "-->" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i += 3;
                }
                else if( input.Substring( i , 1 ).Equals( "<" ) )
                {
                    inTag = true;
                    output.Append( "<" );
                    i++;
                }
                else if( input.Substring( i , 1 ).Equals( ">" ) )
                {
                    inTag = false;
                    output.Append( ">" );
                    i++;
                }
                else if( input.Substring( i , 1 ).Equals( "\"" ) && inTag )
                {
                    int stringStart = i;
                    i++;
                    i = input.IndexOf( "\"" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i++;
                    output.Append( input.Substring( stringStart , i - stringStart ) );
                }
                else if( input.Substring( i , 1 ).Equals( "\'" ) && inTag )
                {
                    i++;
                    i = input.IndexOf( "\'" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i++;
                    output.Append( input.Substring( startIndex , i - startIndex ) );
                }
                else
                {
                    output.Append( input.Substring( i , 1 ) );
                    i++;
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// This will remove all HTML comments from the input string. This will
        /// not remove comment markers from inside tag attribute values.
        /// </summary>
        /// <param name="input">Input HTML containing comments</param>
        /// <returns>HTML containing no comments</returns>

        private static string RemoveSgmlComments(string input)
        {
            var output = new StringBuilder();

            int i = 0;
            bool inTag = false;

            int startIndex = i;
            while( i < input.Length )
            {
                if( i + 2 < input.Length && input.Substring( i , 2 ).Equals( "<!" ) )
                {
                    i += 2;
                    i = input.IndexOf( ">" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i += 3;
                }
                else if( input.Substring( i , 1 ).Equals( "<" ) )
                {
                    inTag = true;
                    output.Append( "<" );
                    i++;
                }
                else if( input.Substring( i , 1 ).Equals( ">" ) )
                {
                    inTag = false;
                    output.Append( ">" );
                    i++;
                }
                else if( input.Substring( i , 1 ).Equals( "\"" ) && inTag )
                {
                    int stringStart = i;
                    i++;
                    i = input.IndexOf( "\"" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i++;
                    output.Append( input.Substring( stringStart , i - stringStart ) );
                }
                else if( input.Substring( i , 1 ).Equals( "\'" ) && inTag )
                {
                    i++;
                    i = input.IndexOf( "\'" , i );
                    if( i == -1 )
                    {
                        break;
                    }
                    i++;
                    output.Append( input.Substring( startIndex , i - startIndex ) );
                }
                else
                {
                    output.Append( input.Substring( i , 1 ) );
                    i++;
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// This will encode the scripts within the page so they get passed through the
        /// parser properly. This is due to some people using comments protect the script
        /// and others who don't. It also takes care of issues where the script itself has
        /// HTML comments in (in strings, for example).
        /// </summary>
        /// <param name="input">The HTML to examine</param>
        /// <param name="tagName"></param>
        /// <returns>The HTML with the scripts marked up differently</returns>
        private static string PreprocessScript(string input,string tagName)
        {
            var output = new StringBuilder();
            int index = 0;
            int tagNameLen = tagName.Length;
            while( index < input.Length )
            {
                bool omitBody = false;
                if( index + tagNameLen + 1 < input.Length && input.Substring( index , tagNameLen + 1 ).ToLower().Equals( "<" + tagName ) )
                {
                    // Look for the end of the tag (we pass the attributes through as normal)
                    do
                    {
                        if( index >= input.Length )
                        {
                            break;
                        }
                        if( input.Substring( index , 1 ).Equals( ">" ) ) 
                        {
                            output.Append( ">" );
                            index++;
                            break;
                        }
                        if( index + 1 < input.Length && input.Substring( index , 2 ).Equals( "/>" ) ) 
                        {
                            output.Append( "/>" );
                            index += 2;
                            omitBody = true;
                            break;
                        }
                        if( input.Substring( index , 1 ).Equals( "\"" ) ) 
                        {
                            output.Append( "\"" );
                            index++;
                            while( index < input.Length && ! input.Substring( index , 1 ).Equals( "\"" ) )
                            {
                                output.Append( input.Substring( index , 1 ) );
                                index++;
                            }
                            if( index < input.Length )
                            {
                                index++;
                                output.Append( "\"" );
                            }
                        }
                        else if( input.Substring( index , 1 ).Equals( "\'" ) ) 
                        {
                            output.Append( "\'" );
                            index++;
                            while( index < input.Length && ! input.Substring( index , 1 ).Equals( "\'" ) )
                            {
                                output.Append( input.Substring( index , 1 ) );
                                index++;
                            }
                            if( index < input.Length )
                            {
                                index++;
                                output.Append( "\'" );
                            }
                        }
                        else
                        {
                            output.Append( input.Substring( index , 1 ) );
                            index++;
                        }
                    } while( true );
                    if( index >= input.Length ) break;
                    // Phew! Ok now we are reading the script body

                    if( ! omitBody )
                    {
                        var scriptBody = new StringBuilder();
                        while( index + tagNameLen + 3 < input.Length && ! input.Substring( index , tagNameLen + 3 ).ToLower().Equals( "</" + tagName + ">" ) )
                        {
                            scriptBody.Append( input.Substring( index , 1 ) );
                            index++;
                        }
                        // Done - now encode the script
                        output.Append( EncodeScript( scriptBody.ToString() ) );
                        output.Append( "</" + tagName + ">" );
                        if( index + tagNameLen + 3 < input.Length )
                        {
                            index += tagNameLen + 3;
                        }
                    }
                }
                else
                {
                    output.Append( input.Substring( index , 1 ) );
                    index++;
                }
            }
            return output.ToString();
        }


        private static string EncodeScript(string script)
        {
            string output = script.Replace( "<" , "[MIL-SCRIPT-LT]" );
            output = output.Replace( ">" , "[MIL-SCRIPT-GT]" );
            output = output.Replace( "\r" , "[MIL-SCRIPT-CR]" );
            output = output.Replace( "\n" , "[MIL-SCRIPT-LF]" );
            return output;
        }

        private static string DecodeScript(string script)
        {
            string output = script.Replace( "[MIL-SCRIPT-LT]" , "<" );
            output = output.Replace( "[MIL-SCRIPT-GT]" , ">" );
            output = output.Replace( "[MIL-SCRIPT-CR]" , "\r" );
            output = output.Replace( "[MIL-SCRIPT-LF]" , "\n" );
            return output;
        }

        #endregion

        #region HTML tokeniser

        /// <summary>
        /// This will tokenise the HTML input string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static StringCollection GetTokens(string input)
        {
            var tokens = new StringCollection();

            int i = 0;
            ParseStatus status = ParseStatus.ReadText;

            int startIndex = i;
            while( i < input.Length )
            {
                int valueStartIndex = i;
                if( status == ParseStatus.ReadText )
                {
                    if( i+2 < input.Length && input.Substring( i , 2 ).Equals( "</" ) )
                    {
                        i += 2;
                        tokens.Add( "</" );
                        status = ParseStatus.ReadEndTag;
                    }
                    else if( input.Substring( i , 1 ).Equals( "<" ) )
                    {
                        i++;
                        tokens.Add( "<" );
                        status = ParseStatus.ReadStartTag;
                    }
                    else
                    {
                        int nextIndex = input.IndexOf( "<" , i );
                        switch (nextIndex)
                        {
                            case -1:
                                tokens.Add( input.Substring( i ) );
                                break;
                            default:
                                tokens.Add( input.Substring( i , nextIndex - i ) );
                                i = nextIndex;
                                break;
                        }
                    }
                }
                else if( status == ParseStatus.ReadStartTag )
                {
                    // Skip leading whitespace in tag
                    while( i<input.Length && input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    // Read tag name
                    int tagNameStart = i;
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( " \r\n\t/>".ToCharArray() ) == -1 )
                    {
                        i++;
                    }
                    tokens.Add( input.Substring( tagNameStart , i - tagNameStart ) );
                    // Skip trailing whitespace in tag
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    if(  i+1<input.Length && input.Substring( i , 1 ).Equals( "/>" ) )
                    {
                        tokens.Add( "/>" );
                        status = ParseStatus.ReadText;
                        i+=2;
                    }
                    else if(  i<input.Length && input.Substring( i , 1 ).Equals( ">" ) )
                    {
                        tokens.Add( ">" );
                        status = ParseStatus.ReadText;
                        i++;
                    }
                    else
                    {
                        status = ParseStatus.ReadAttributeName;
                    }
                }
                else if( status == ParseStatus.ReadEndTag )
                {
                    // Skip leading whitespace in tag
                    while( i<input.Length && input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    // Read tag name
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( " \r\n\t>".ToCharArray() ) == -1 )
                    {
                        i++;
                    }
                    tokens.Add( input.Substring( startIndex , i - startIndex ) );
                    // Skip trailing whitespace in tag
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    if(  i<input.Length && input.Substring( i , 1 ).Equals( ">" ) )
                    {
                        tokens.Add( ">" );
                        status = ParseStatus.ReadText;
                        i++;
                    }
                }
                else if( status == ParseStatus.ReadAttributeName )
                {
                    // Read attribute name
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    int attributeNameStart = i;
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( " \r\n\t/>=".ToCharArray() ) == -1 )
                    {
                        i++;
                    }
                    tokens.Add( input.Substring( attributeNameStart , i - attributeNameStart ) );
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    if(  i+1<input.Length && input.Substring( i , 2 ).Equals( "/>" ) )
                    {
                        tokens.Add( "/>" );
                        status = ParseStatus.ReadText;
                        i+=2;
                    }
                    else if(  i<input.Length && input.Substring( i , 1 ).Equals( ">" ) )
                    {
                        tokens.Add( ">" );
                        status = ParseStatus.ReadText;
                        i++;
                    }
                    else if( i<input.Length && input.Substring( i , 1 ).Equals( "=" ) )
                    {
                        tokens.Add( "=" );
                        i++;
                        status = ParseStatus.ReadAttributeValue;
                    }
                    else if( i<input.Length && input.Substring( i , 1 ).Equals( "/" ) )
                    {
                        i++;
                    }
                }
                else if( status == ParseStatus.ReadAttributeValue )
                {
                    // Read the attribute value
                    while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                    {
                        i++;
                    }
                    if(  i<input.Length && input.Substring( i , 1 ).Equals( "\"" ) )
                    {
                        i++;
                        while( i<input.Length &&  ! input.Substring( i , 1 ).Equals( "\"" ) )
                        {
                            i++;
                        }
                        if( i<input.Length && input.Substring( i , 1 ).Equals( "\"" ) )
                        {
                            i++;
                        }
                        tokens.Add( input.Substring( valueStartIndex + 1 , i - valueStartIndex - 2) );
                        status = ParseStatus.ReadAttributeName;
                    }
                    else if(  i<input.Length && input.Substring( i , 1 ).Equals( "\'" ) )
                    {
                        i++;
                        while( i<input.Length &&  ! input.Substring( i , 1 ).Equals( "\'" ) )
                        {
                            i++;
                        }
                        if( i<input.Length && input.Substring( i , 1 ).Equals( "\'" ) )
                        {
                            i++;
                        }
                        tokens.Add( input.Substring( valueStartIndex + 1, i - valueStartIndex - 2 ) );
                        status = ParseStatus.ReadAttributeName;
                    }
                    else
                    {
                        int valueStart = i;
                        while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( " \r\n\t/>".ToCharArray() ) == -1 )
                        {
                            i++;
                        }
                        tokens.Add( input.Substring( valueStart , i - valueStart ) );
                        while( i<input.Length &&  input.Substring( i , 1 ).IndexOfAny( WHITESPACE_CHARS ) != -1 )
                        {
                            i++;
                        }
                        status = ParseStatus.ReadAttributeName;
                    }
                    if(  i+1<input.Length && input.Substring( i , 2 ).Equals( "/>" ) )
                    {
                        tokens.Add( "/>" );
                        status = ParseStatus.ReadText;
                        i+=2;
                    }
                    else if( i<input.Length &&  input.Substring( i , 1 ).Equals( ">" ) )
                    {
                        tokens.Add( ">" );
                        i++;
                        status = ParseStatus.ReadText;
                    }
                    // ANDY
                }
            }

            return tokens;
        }

        #endregion
    }
}