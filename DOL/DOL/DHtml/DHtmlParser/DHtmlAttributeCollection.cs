/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DHtmlAttributeCollection Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DHtmlAttributeCollection.cs: implementation of the DHtmlAttributeCollection class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DOL.DHtml.DHtmlParser
{    
	
	/// <summary>
	/// This is a collection of attributes. Typically, this is associated with a particular
	/// element. This collection is searchable by both the index and the name of the attribute.
	/// </summary>
    public sealed class DHtmlAttributeCollection : DOL.DBase.DIDiagnosisable, IEnumerable
	{

	/////////////////////////////////////////////////////////////////////////////////
	#region 基本操作	

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		public DHtmlAttributeCollection()
		{
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DHtmlAttributeCollection(DIHtmlNodeHasAttribute ownerNode)
        {
            m_ownerNode = ownerNode;
        }

        /////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 是否為有效物件
		/// </summary>
		public void AssertValid()
		{
            for(int index = 0, count = m_attributeList.Count; index < count; ++index)
                m_attributeList[index].AssertValid();
		}

        /////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 傾印物件
		/// </summary>
		public void Dump(StringBuilder buffer, string prefix)
		{
			AssertValid();
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
            return m_attributeList.GetEnumerator();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int Capacity
        {
            get
            {
                return m_attributeList.Capacity;
            }
            set
            {
                m_attributeList.Capacity = value;
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
                return m_attributeList.Count;
            }
        }

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
        /// This will add an attribute to the collection.
		/// </summary>
		/// <param name="attribute">The attribute to add.</param>
		/// <returns>The index at which it was added.</returns>
		public void Add(DHtmlAttribute attribute)
		{
			System.Diagnostics.Debug.Assert(attribute != null);

            if(m_ownerNode != null)
            {
                if(attribute.OwnerNode != null)
                    attribute.OwnerNode.Attributes.RemoveAt(attribute.OwnerNode.Attributes.IndexOf(attribute));

                attribute.OwnerNode = m_ownerNode;
            }

            // Update m_attributeHash
            if(m_attributeHash.ContainsKey(attribute.Name) == false)
                m_attributeHash[attribute.Name] = attribute;
            
            m_attributeList.Add(attribute);
		}

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will insert an attribute at the given position
        /// </summary>
        /// <param name="index">The position at which to insert the attribute.</param>
        /// <param name="node">The attribute to insert.</param>
        public void Insert(int index, DHtmlAttribute attribute)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index <= m_attributeList.Count);
            System.Diagnostics.Debug.Assert(attribute != null);

            // set attribute.OwnerNode
            if(m_ownerNode != null)
            {
                if(attribute.OwnerNode != null)
                    attribute.OwnerNode.Attributes.RemoveAt(attribute.OwnerNode.Attributes.IndexOf(attribute));

                attribute.OwnerNode = m_ownerNode;
            }

            // Update m_attributeHash
            if(m_attributeHash.ContainsKey(attribute.Name) == false)
                m_attributeHash[attribute.Name] = attribute;
            else
            {
                DHtmlAttribute temp = (DHtmlAttribute)m_attributeHash[attribute.Name];
                int tempIndex = m_attributeList.IndexOf(temp);
                if(index < tempIndex)
                    m_attributeHash[attribute.Name] = attribute;
            }

            m_attributeList.Insert(index, attribute);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            // set attribute.OwnerNode
            if(m_ownerNode != null)
            {
                for(int index = 0, count = m_attributeList.Count; index < count; ++index)
                {
                    DHtmlAttribute attribute = m_attributeList[index];
                    System.Diagnostics.Debug.Assert(m_ownerNode == attribute.OwnerNode);
                    attribute.OwnerNode = null;
                }                
            }

            m_attributeList.Clear();
            m_attributeHash.Clear();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < m_attributeList.Count);

            DHtmlAttribute attribute = m_attributeList[index];

            // set attribute.OwnerNode
            if(m_ownerNode != null)
            {
                System.Diagnostics.Debug.Assert(m_ownerNode == attribute.OwnerNode);
                attribute.OwnerNode = null;
            }  

            m_attributeList.RemoveAt(index);

            // Update m_attributeHash
            if(m_attributeHash[attribute.Name] == attribute)
            {
                m_attributeHash.Remove(attribute.Name);
                for(int count = m_attributeList.Count; index < count; ++index)
                {
                    DHtmlAttribute temp = m_attributeList[index];
                    if(attribute.Name.Equals(temp.Name))
                    {
                        m_attributeHash[attribute.Name] = temp;
                        break;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void RemoveRange(int index, int count)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < m_attributeList.Count);
            System.Diagnostics.Debug.Assert(count > 0);
            System.Diagnostics.Debug.Assert(index + count <= m_attributeList.Count);

            for(int scanIndex = index; scanIndex < index + count; ++scanIndex)
            {
                DHtmlAttribute attribute = m_attributeList[scanIndex];

                // set attribute.OwnerNode
                if(m_ownerNode != null)
                {
                    System.Diagnostics.Debug.Assert(m_ownerNode == attribute.OwnerNode);
                    attribute.OwnerNode = null;
                }

                // Update m_attributeHash
                if(m_attributeHash[attribute.Name] == attribute)
                {
                    m_attributeHash.Remove(attribute.Name);
                    for(int listIndex = index + count, listCount = m_attributeList.Count; listIndex < listCount; ++listIndex)
                    {
                        DHtmlAttribute temp = m_attributeList[listIndex];
                        if(attribute.Name.Equals(temp.Name))
                        {
                            m_attributeHash[attribute.Name] = temp;
                            break;
                        }
                    }
                }
            }

            m_attributeList.RemoveRange(index, count);
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
        public bool HasAttribute(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);
            return m_attributeHash.ContainsKey(name);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This provides direct access to an attribute in the collection by its index.
        /// </summary>
        public DHtmlAttribute this[int index]
        {
            get
            {
                System.Diagnostics.Debug.Assert(index >= 0 && index < m_attributeList.Count);
                return m_attributeList[index];
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This overload allows you to have direct access to an attribute by providing
        /// its name. If the attribute does not exist, null is returned.
        /// </summary>
        public DHtmlAttribute this[string name]
        {
            get
            {
                System.Diagnostics.Debug.Assert(name != null);
                return (DHtmlAttribute)m_attributeHash[name];
            }
        }
        
        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the index of the attribute with the specified name. If it is
        /// not found, this method will return -1.
        /// </summary>
        /// <param name="name">The name of the attribute to find.</param>
        /// <returns>The zero-based index, or -1.</returns>
        public int IndexOf(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);

            name = name.ToLower();

            int result = -1;
            for(int index = 0, count = m_attributeList.Count; index < count; ++index)
            {
                DHtmlAttribute attribute = m_attributeList[index];
                if(attribute.Name.Equals(name))
                {
                    result = index;
                    break;
                }
            }

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This is used to identify the index of this node as it appears in the collection.
        /// </summary>
        /// <param name="node">The attribute to test</param>
        /// <returns>The index of the attribute, or -1 if it is not in this collection</returns>
        public int IndexOf(DHtmlAttribute attribute)
        {
            System.Diagnostics.Debug.Assert(attribute != null);
            return m_attributeList.IndexOf(attribute);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will search the collection for the named attribute. If it is not found, this
        /// will return null.
        /// </summary>
        /// <param name="name">The name of the attribute to find.</param>
        /// <returns>The attribute, or null if it wasn't found.</returns>
        public DHtmlAttributeCollection FindByName(string name)
        {
            System.Diagnostics.Debug.Assert(name != null);

            name = name.ToLower();

            DHtmlAttributeCollection result = new DHtmlAttributeCollection();
            FindByName(name, result);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        public void FindByName(string name, DHtmlAttributeCollection result)
        {
            System.Diagnostics.Debug.Assert(name != null);
            System.Diagnostics.Debug.Assert(result != null);

            name = name.ToLower();

            for(int index = 0, count = m_attributeList.Count; index < count; ++index)
            {
                DHtmlAttribute attribute = m_attributeList[index];
                if(attribute.Name.Equals(name))
                    result.Add(attribute);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        public void FindByNameValue(string name, string value, DHtmlAttributeCollection result)
        {
            System.Diagnostics.Debug.Assert(name != null);
            System.Diagnostics.Debug.Assert(result != null);

            name = name.ToLower();
            for(int index = 0, count = m_attributeList.Count; index < count; ++index)
            {
                DHtmlAttribute attribute = m_attributeList[index];
                if(attribute.Name.Equals(name) && attribute.Value.Equals(value))
                    result.Add(attribute);
            }
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
	#region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private DIHtmlNodeHasAttribute m_ownerNode = null;
		/// <summary>
		/// 
		/// </summary>
        private Hashtable m_attributeHash = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        private List<DHtmlAttribute> m_attributeList = new List<DHtmlAttribute>();	

    #endregion 

	}
}
