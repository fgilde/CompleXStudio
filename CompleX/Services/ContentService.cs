using System.Collections.Generic;
using System.Linq;
using CompleX_Types.HTMLParser;

namespace CompleX.Services
{
    public static class ContentService
    {
        public static IEnumerable<string> GetUsedHtmlHrefs()
        {
            return CompleX_Studio.CurrentContentEditor == null ? Enumerable.Empty<string>() 
                                                               : HtmlParser.GetUsedHtmlHrefs(CompleX_Studio.CurrentContentEditor.Content as string);
        }
    }
}