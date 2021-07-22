/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DCssPropertyCollection Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DCssPropertyCollection.cs: implementation of the DCssPropertyCollection class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DOL.DHtml.DCssResolver
{

    /// <summary>
    /// This is a collection of CSS property, and the name and value of property is one-to-one mapping.
    /// Typically, this is associated with a particular element. This collection is searchable by both 
    /// the index and the name of the attribute.
    /// </summary>
    public sealed class DCssPropertyCollection : DOL.DBase.DIDiagnosisable, IEnumerable
    {

    /////////////////////////////////////////////////////////////////////////////////
    #region 基本操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DCssPropertyCollection() : base()
        {
        }

        /////////////////////////////////////////////////////////////////////////////        
        /// <summary>
        /// 是否為有效物件
        /// </summary>
        public void AssertValid()
        {
            for(int index = 0, count = m_propertyList.Count; index < count; ++index)
                m_propertyList[index].AssertValid();
        }

        /////////////////////////////////////////////////////////////////////////////       
        /// <summary>
        /// 傾印物件
        /// </summary>
        public void Dump(StringBuilder buffer, string prefix)
        {
            AssertValid();
            string old = prefix;
            buffer.Append(old + "├Object " + GetType().Name + " Dump : \n");

            prefix += "│　";
            buffer.Append(prefix + "DCssProperty number: " + m_propertyList.Count + "\n");

            if(m_propertyList.Count != 0)
            {
                buffer.Append(prefix + "Deep dump in the following:\n");

                for(int index = 0, count = m_propertyList.Count; index < count; ++index) // 所有物件尋訪一次呼叫 Dump
                {
                    buffer.Append(prefix + "│\n");
                    m_propertyList[index].Dump(buffer, prefix);
                }
            }

        }

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 相關操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return m_propertyList.GetEnumerator();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int Capacity
        {
            get
            {
                return m_propertyList.Capacity;
            }
            set
            {
                m_propertyList.Capacity = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return m_propertyList.Count;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public int AddFirst(DCssProperty property)
        {
            System.Diagnostics.Debug.Assert(property != null);

            object result = m_propertyHash[property.Name];
            if(result == null) // Find the property name whether the it exists in collection.
            {
                result = 0;
                m_propertyHash[property.Name] = 0;
                m_propertyList.Insert(0, property);

                // Update index of m_propertyHash after 0.
                for(int index = 1, count = m_propertyList.Count; index < count; ++index)
                {
                    string name = m_propertyList[index].Name;
                    m_propertyHash[name] = (int)m_propertyHash[name] + 1;
                }
            }
            else m_propertyList[(int)result] = property;

            return (int)result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public int AddLast(DCssProperty property)
        {
            System.Diagnostics.Debug.Assert(property != null);

            object result = m_propertyHash[property.Name];
            if(result == null) // Find the property name whether the it exists in collection.
            {
                result = m_propertyList.Count;
                m_propertyHash[property.Name] = m_propertyList.Count;
                m_propertyList.Add(property);                
            }
            else m_propertyList[(int)result] = property;

            return (int)result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public int Insert(int index, DCssProperty property)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index <= m_propertyList.Count);
            System.Diagnostics.Debug.Assert(property != null);

            object result = m_propertyHash[property.Name];
            if(result == null) // Find the property name whether the it exists in collection.
            {
                result = index;
                m_propertyHash[property.Name] = index;
                m_propertyList.Insert(index, property);

                // Update index of m_propertyHash after "index"
                for(int scan = index + 1, count = m_propertyList.Count; scan < count; ++scan)
                {
                    string name = m_propertyList[scan].Name;
                    m_propertyHash[name] = (int)m_propertyHash[name] + 1;
                }
            }
            else m_propertyList[(int)result] = property;

            return (int)result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < m_propertyList.Count);

            DCssProperty property = m_propertyList[index];

            // Update index of m_propertyHash after "index"            
            for(int scan = index + 1, count = m_propertyList.Count; scan < count; ++scan)
            {
                string name = m_propertyList[scan].Name;
                m_propertyHash[name] = (int)m_propertyHash[name] - 1;
            }

            m_propertyList.RemoveAt(index);
            m_propertyHash.Remove(property.Name);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public bool Remove(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);

            object index = m_propertyHash[name];

            if(index != null)
            {
                DCssProperty property = m_propertyList[(int)index];

                // Update index of m_propertyHash after "index"            
                for(int scan = (int)index + 1, count = m_propertyList.Count; scan < count; ++scan)
                {
                    string scanName = m_propertyList[scan].Name;
                    m_propertyHash[scanName] = (int)m_propertyHash[scanName] - 1;
                }

                m_propertyList.RemoveAt((int)index);
                m_propertyHash.Remove(property.Name);
            }

            return index != null;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            m_propertyList.Clear();
            m_propertyHash.Clear();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverlet"></param>
        /// <returns></returns>
        public void Merge(DCssPropertyCollection merged)
        {
            System.Diagnostics.Debug.Assert(merged != null);

            for(int index = 0, count = merged.m_propertyList.Count; index < count; ++index)
                AddFirst(merged.m_propertyList[index]);
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 搜尋操作

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasProperty(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);

            name = name.ToLower();
            return m_propertyHash.ContainsKey(name);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This provides direct access to an property in the collection by its index.
        /// </summary>
        public DCssProperty this[int index]
        {
            get
            {
                return m_propertyList[index];
            }
            set
            {
                System.Diagnostics.Debug.Assert(index >= 0 && index < m_propertyList.Count);
                System.Diagnostics.Debug.Assert(value != null);
                m_propertyList[index] = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This overload allows you to have direct access to an property by providing
        /// its name. If the property does not exist, null is returned.
        /// </summary>
        public DCssProperty this[string name]
        {
            get
            {
                System.Diagnostics.Debug.Assert(name != null);

                name = name.ToLower();

                DCssProperty result = null;
                object index = m_propertyHash[name];
                if(index != null)
                    result = m_propertyList[(int)index];

                return result;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the index of the property with the specified name. If it is
        /// not found, this method will return -1.
        /// </summary>
        /// <param name="name">The name of the property to find.</param>
        /// <returns>The zero-based index, or -1.</returns>
        public int IndexOf(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);

            name = name.ToLower();

            object result = m_propertyHash[name];
            if(result == null)
                result = -1;

            return (int)result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is used to identify the index of this node as it appears in the collection.
        /// </summary>
        /// <param name="node">The property to test</param>
        /// <returns>The index of the property, or -1 if it is not in this collection</returns>
        public int IndexOf(DCssProperty property)
        {
            System.Diagnostics.Debug.Assert(property != null);
            return m_propertyList.IndexOf(property);
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 匯出

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 匯出成 CSS 字串
		/// </summary>		
        public string CSS
		{
            get
            {
                StringBuilder writer = new StringBuilder();
                for(int index = 0, count = m_propertyList.Count; index < count; ++index)
                    m_propertyList[index].TransformCSS(writer);

                return writer.ToString();
            }
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full CSS to represent this property
        /// </summary>
        internal void TransformCSS(StringBuilder writer)
        {
            System.Diagnostics.Debug.Assert(writer != null);

            for(int index = 0, count = m_propertyList.Count; index < count; ++index)
            {
                m_propertyList[index].TransformCSS(writer);
                writer.Append(" ");
            }
        }
	
    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private Hashtable m_propertyHash = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        private List<DCssProperty> m_propertyList = new List<DCssProperty>();


    #endregion	

    }
}
