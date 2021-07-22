//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
//<TAG\b[^>]*>(.*?)</TAG>

// Tag Start and End Match: (<([\/@!?#]?[^\W_]+)(?:\s|(?:\s(?:[^'">\s]|'[^']*'|"[^"]*")*))*>)|(<\!--[^-]*-->)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CompleX_Settings.Constants;

namespace CompleX_Types.Extensions
{
    public static class TagExtension
    {
        /// <summary>
        ///  Converts a string like <a href="www.google.de" target="_blank" > go to google </a> 
        ///  To a CompleX_Types.Tag
        /// </summary>
        /// <param name="s">The s.</param>
        public static Tag ToTag(this string s)
        {
            var attributepattern = new Regex(@"(?<key>[\w-]+)=(?<value>("".*?"")|('.*?')|(\w*))");
                //new Regex(@"(\S+)=[""']?((?:.(?![""']?\s+(?:\S+)=|[>""']))+.)[""']?");
            var ultraRegex = new Regex(Const.RegexMatchAllHtmlTags,RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

            if (!String.IsNullOrEmpty(s))
            {
                    var match = ultraRegex.Match(s);
                    try
                    {
                        //var result = new Tag(TagLanguage.HTML, match.Groups["tag"].Value);
                        var result = TagFactory.CreateTag(TagLanguage.HTML, match.Groups["tag"].Value, true);
                        foreach (var attribute in attributepattern.Matches(match.Groups["params"].Value))
                        {
                            string[] keyvalue = attribute.ToString().Split('=');
                            if (keyvalue.Count() >= 2)
                            { 
                                var key = keyvalue[0];
                                var value = keyvalue.AsString(1);
                            
                                if(value.StartsWith("'") || value.StartsWith("\""))
                                    value = value.Substring(1);
                                if (value.EndsWith("'") || value.EndsWith("\""))
                                    value = value.Substring(0,value.Length-1);

                                result.SetAttributeValue(key, value, true);
                            }
                        }
                        if (result.Endtag)
                            result.TagValue = GetTagValue(s);

                        return result;
                    }
                    catch 
                    {
                        return null;
                    }
  
                
            }
            return null;
        }

        public static string GetTagValue(string tag)
        {
            var match = new Regex(Const.RegexMatchAllHtmlTags,RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
            return match.Match(tag).Groups["content"].Value;
        }

        public static string AsString(this string[] c, int startindex)
        {
            string r = String.Empty;
            for (int i = startindex; i < c.Count(); i++)
                r += c[i];
                           
            return r;
        }
        public static string AsString(this string[] c)
        {
            return c.AsString(0);
        }

    }
}