//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;

namespace CompleX_Types
{

    [Serializable]
    public class CloneableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ICloneable where TValue : ICloneable
    {
        public CloneableDictionary<TKey, TValue> CloneDictionary()
        {
            return Clone() as CloneableDictionary<TKey, TValue>;
        }

        #region Implementation of ICloneable

        public object Clone()
        {
            var result = (CloneableDictionary<TKey, TValue>)MemberwiseClone();
            foreach (TKey key in Keys)
            {
                result[key] = (TValue) this[key].Clone();
            }

            return result;
        }

        #endregion
    }

    [Serializable]
    public class CloneableList<T> : List<T>,ICloneable where T : ICloneable
    {
        public CloneableList<T> CloneList()
        {
            return Clone() as CloneableList<T>;
        }

        #region Implementation of ICloneable
        
        public object Clone()
        {
            var result = (CloneableList <T>) MemberwiseClone();
            for (int i = 0; i < result.Count; i++)
            {
                if (!Equals(this[i], default(T)))
                    result[i] = (T) this[i].Clone();
            }

            return result;
        }

        #endregion
    }
}
