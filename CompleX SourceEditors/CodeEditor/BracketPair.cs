using System.Collections.Generic;

namespace CompleX_SourceEditors.CodeEditor
{
    class BracketPair:Dictionary<string,string>
    {
        public BracketPair()
        {
            Add("(",")");
            Add("[","]");
            Add("{","}");
            Add("\"","\"");
            Add("'","'");
        }

        public string GetClosingBracket(string openingBracket)
        {
            if(ContainsKey(openingBracket))
                return this[openingBracket];
            return string.Empty;
        }
    }
}
