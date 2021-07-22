using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CompleX_Types.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Match> ToArray(this MatchCollection collection)
        {
            var result = new Match[collection.Count];
            collection.CopyTo(result, 0);
            return result;
        }
    }
}
