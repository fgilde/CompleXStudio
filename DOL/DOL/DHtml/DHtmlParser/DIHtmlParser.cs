/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DIHtmlParser Class
>
>	E-mail¡G	  nomad_libra.tw@yahoo.com.tw
>	E-mail¡G	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DIHtmlParser.cs: implementation of the DIHtmlParser interface.
//
///////////////////////////////////////////////////////////////////////////////

using DOL.DHtml.DHtmlParser.Node;

namespace DOL.DHtml.DHtmlParser
{
    public delegate void ElementEventHandler(DHtmlElement element);
    public delegate void StyleEventHandler(DHtmlStyle style);
    public delegate void ScriptEventHandler(DHtmlScript script);
    public delegate void CommentEventHandler(DHtmlComment comment);
    public delegate void ProcessingInstructionEventHandler(DHtmlProcessingInstruction pi);
    public delegate void TextEventHandler(DHtmlText text);

	/// <summary>
	/// This is the interface of HTML parser.
	/// </summary>
    public interface DIHtmlParser
	{
        /// <summary>
        /// Parse html text to DHtmlNodeCollection object
        /// </summary>
        /// <param name="html"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        void Parse(string html, DHtmlNodeCollection result);

        /// <summary>
        /// Parse html text to DHtmlNodeCollection object
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        DHtmlNodeCollection Parse(string html);

        /// <summary>
        /// 
        /// </summary>
        event ElementEventHandler ElementCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        event StyleEventHandler StyleCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        event ScriptEventHandler ScriptCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        event CommentEventHandler CommentCreatedEvent;        
        /// <summary>
        /// 
        /// </summary>
        event ProcessingInstructionEventHandler ProcessingInstructionCreatedEvent;
        /// <summary>
        /// 
        /// </summary>
        event TextEventHandler TextCreatedEvent;

	}
}
