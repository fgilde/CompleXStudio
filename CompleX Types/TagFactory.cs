using System;
using System.Collections.Generic;

namespace CompleX_Types
{

    /// <summary>
    /// Static Factory to create Tag with cache
    /// </summary>
    public static class TagFactory
    {
        private static object lockObject = new object();
        private static Dictionary<Tuple<TagLanguage,string >,Tag> cache =  new Dictionary<Tuple<TagLanguage, string>, Tag>();

        /// <summary>
        /// Creates a new Tag
        /// </summary>
        public static Tag CreateTag(TagLanguage language, string tag, bool chacheTag)
        {

            var key = new Tuple<TagLanguage, string>(language, tag);
            if (cache.ContainsKey(key) && chacheTag)
                return cache[key];

            var result = new Tag(language, tag);
            if (chacheTag)
            {
                lock (lockObject)
                {
                    cache.Add(key, result);
                }
            }
            return result;
        }


    }

}