/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DCssProperty Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DCssProperty.cs: implementation of the DCssProperty class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using DOL.DHtml.DHtmlParser;
using DOL.DHtml.DHtmlParser.Node;

namespace DOL.DHtml.DCssResolver
{
	/// <summary>
    /// 
	/// </summary>
    public sealed class DCssSelector : DOL.DBase.DIDiagnosisable, IEnumerable, ICloneable, IEquatable<DCssSelector>, IComparable<DCssSelector>
	{

    /////////////////////////////////////////////////////////////////////////////////
	#region 基本操作

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 建構子
		/// </summary>
        public DCssSelector()
		{        
		}

        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 建構子
        /// </summary>
        public DCssSelector(DCssSelector selector)
        {
            System.Diagnostics.Debug.Assert(selector != null);

            m_priority = selector.m_priority;

            int count = selector.m_simpleSelectorList.Count;
            m_simpleSelectorList.Capacity = count;
            for(int index = 0; index < count; ++index)
                m_simpleSelectorList.Add((DCssSimpleSelector)selector.m_simpleSelectorList[index].Clone());

            count = selector.m_properties.Count;
            m_properties.Capacity = count;
            for(int index = 0; index < count; ++index)
                m_properties.AddLast((DCssProperty)selector.m_properties[index].Clone());            
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DCssSelector(this);
        }

        /////////////////////////////////////////////////////////////////////////////        
        /// <summary>
        /// 是否為有效物件
        /// </summary>
        public void AssertValid()
        {
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
            buffer.Append(prefix + "CSS Value: " + this.CSS + "\n");
            DCssSpecificity specificity = this.SpecificityValue;
            buffer.Append(prefix + "Specificity Value: A" + specificity.A + ",B" + specificity.B + ",C" + specificity.C + "\n");
            buffer.Append(prefix + "Priority Value: " + m_priority + "\n");	


        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DCssSelector other)
        {
            bool result = true;

            if(m_priority == other.m_priority && m_simpleSelectorList.Count == other.m_simpleSelectorList.Count)
            {
                for(int index = 0; index < m_simpleSelectorList.Count; ++index)
                    if(m_simpleSelectorList[index].Equals(other.m_simpleSelectorList[index]) == false)
                    {
                        result = false;
                        break;
                    }
            }
            else result = false;

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DCssSelector other)
        {
            int result = this.SpecificityValue.CompareTo(other.SpecificityValue);
            if(result == 0)
            {
                if(m_priority > other.m_priority)
                    result = 1;
                else if(m_priority < other.m_priority)
                    result = -1;
            }

            return result;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return m_simpleSelectorList.GetEnumerator();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int Capacity
        {
            get
            {
                return m_simpleSelectorList.Capacity;
            }
            set
            {
                m_simpleSelectorList.Capacity = value;
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
                return m_simpleSelectorList.Count;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="simpleSelector"></param>
        public void Add(DCssSimpleSelector simpleSelector)
        {
            System.Diagnostics.Debug.Assert(simpleSelector != null);

            m_simpleSelectorList.Add(simpleSelector);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="simpleSelector"></param>
        public void Insert(int index, DCssSimpleSelector simpleSelector)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index <= m_simpleSelectorList.Count);
            System.Diagnostics.Debug.Assert(simpleSelector != null);

            m_simpleSelectorList.Insert(index, simpleSelector);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < m_simpleSelectorList.Count);
            m_simpleSelectorList.RemoveAt(index);
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            m_simpleSelectorList.Clear();
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DCssSimpleSelector this[int index]
        {
            get
            {
                return m_simpleSelectorList[index];
            }
            set
            {
                System.Diagnostics.Debug.Assert(index >= 0 && index < m_simpleSelectorList.Count);
                System.Diagnostics.Debug.Assert(value != null);
                m_simpleSelectorList[index] = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="simpleSelector"></param>
        /// <returns></returns>
        public int IndexOf(DCssSimpleSelector simpleSelector)
        {
            System.Diagnostics.Debug.Assert(simpleSelector != null);
            return m_simpleSelectorList.IndexOf(simpleSelector);
        }


    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 屬性設定

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DCssPropertyCollection Properties
        {
            get
            {
                return m_properties;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int Priority
        {
            get
            {
                return m_priority;
            }
            set
            {
                m_priority = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public bool HasPseudo
        {
            get
            {
                bool result = false;

                for(int index = 0, count = m_simpleSelectorList.Count; index < count; ++index)
                {
                    if(m_simpleSelectorList[index].Pseudo.Length != 0)
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        internal DCssSpecificity SpecificityValue
        {
            get
            {
                DCssSpecificity result = new DCssSpecificity();

                for(int index = 0, count = m_simpleSelectorList.Count; index < count; ++index)
                    result.Add(m_simpleSelectorList[index].SpecificityValue);

                return result;
            }
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 匯出

        ///////////////////////////////////////////////////////////////////////////////
		/// <summary>
        /// 匯出 Selector 成字串
		/// </summary>		
        public string Selector
        {
            get
            {
                StringBuilder writer = new StringBuilder();
                for(int index = 0, count = m_simpleSelectorList.Count; index < count; ++index)
                {
                    DCssSimpleSelector simpleSelector = m_simpleSelectorList[index];

                    writer.Append(simpleSelector.CSS);
                    switch(simpleSelector.Combinator)
                    {
                        case DCssSimpleSelector.Relation.Descendant:
                            writer.Append(" ");
                            break;
                        case DCssSimpleSelector.Relation.Child:
                            writer.Append(" > ");
                            break;
                        case DCssSimpleSelector.Relation.Sibling:
                            writer.Append(" + ");
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }

                return writer.ToString();
            }
        }

		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 匯出成 CSS 字串
		/// </summary>		
        public string CSS
		{
            get
            {
                StringBuilder writer = new StringBuilder();
                TransformCSS(writer);
                return writer.ToString();
            }
		}

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        internal void TransformCSS(StringBuilder writer)
        {
            for(int index = 0, count = m_simpleSelectorList.Count; index < count; ++index)
            {
                DCssSimpleSelector simpleSelector = m_simpleSelectorList[index];

                writer.Append(simpleSelector.CSS);
                switch(simpleSelector.Combinator)
                {
                    case DCssSimpleSelector.Relation.Descendant:
                        writer.Append(" ");
                        break;
                    case DCssSimpleSelector.Relation.Child:
                        writer.Append(" > ");
                        break;
                    case DCssSimpleSelector.Relation.Sibling:
                        writer.Append(" + ");
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false);
                        break;
                }
            }

            writer.Append("{ ");

            for(int index = 0, count = m_properties.Count; index < count; ++index)
                writer.Append(m_properties[index].CSS + " ");

            writer.Append("}");
        }

    #endregion	
        
    /////////////////////////////////////////////////////////////////////////////////
    #region 比對操作

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsMatching(DHtmlElement element)
        {            
            System.Diagnostics.Debug.Assert(element != null);
            System.Diagnostics.Debug.Assert(m_simpleSelectorList.Count > 0);

            bool result = false;

            DCssSimpleSelector.Relation combinator = DCssSimpleSelector.Relation.Descendant;

            int index = m_simpleSelectorList.Count - 1;
            DCssSimpleSelector simpleSelector = m_simpleSelectorList[index];
            if(simpleSelector.IsMatching(element) == true) // first matching
            {
                --index;

                if(index >= 0)
                {
                    simpleSelector = m_simpleSelectorList[index];
                    combinator = simpleSelector.Combinator;
                    switch(combinator)
                    {
                        case DCssSimpleSelector.Relation.Child:
                        case DCssSimpleSelector.Relation.Descendant:
                            element = element.Parent; // child to parent
                            break;

                        case DCssSimpleSelector.Relation.Sibling:
                            element = element.PreviousSibling as DHtmlElement; // brother
                            break;

                        default: System.Diagnostics.Debug.Assert(false); break;
                    }
                }

                while(index >= 0 && element != null)
                {
                    simpleSelector = m_simpleSelectorList[index];
                    if(simpleSelector.IsMatching(element) == true) // simple selector matching
                    {
                        --index; // match next simple selector

                        if(index >= 0)
                        {
                            combinator = simpleSelector.Combinator;
                            switch(combinator)
                            {
                                case DCssSimpleSelector.Relation.Child:
                                case DCssSimpleSelector.Relation.Descendant:
                                    element = element.Parent; // child to parent
                                    break;

                                case DCssSimpleSelector.Relation.Sibling:
                                    element = element.PreviousSibling as DHtmlElement; // brother
                                    break;

                                default: System.Diagnostics.Debug.Assert(false); break;
                            }
                        }
                    }
                    else
                    {
                        if(combinator == DCssSimpleSelector.Relation.Descendant)
                            element = element.Parent;// child to parent
                        else if(combinator == DCssSimpleSelector.Relation.Child ||
                           combinator == DCssSimpleSelector.Relation.Sibling)
                            break;
                        else System.Diagnostics.Debug.Assert(false);
                    }
                }

                if(index == -1) result = true;
            }
            
            return result;
        }     

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public DHtmlNodeCollection SearchMatching(DHtmlNodeCollection nodes)
        {
            System.Diagnostics.Debug.Assert(nodes != null);
            DHtmlNodeCollection result = new DHtmlNodeCollection();
            SearchMatching(nodes, result, false);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="searchChildren"></param>
        /// <returns></returns>
        public DHtmlNodeCollection SearchMatching(DHtmlNodeCollection nodes, bool searchChildren)
        {
            System.Diagnostics.Debug.Assert(nodes != null);
            DHtmlNodeCollection result = new DHtmlNodeCollection();
            SearchMatching(nodes, result, searchChildren);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="result"></param>
        /// <param name="searchChildren"></param>
        public void SearchMatching(DHtmlNodeCollection nodes, DHtmlNodeCollection result, bool searchChildren)
        {
            System.Diagnostics.Debug.Assert(nodes != null);
            System.Diagnostics.Debug.Assert(result != null);

            for(int index = 0, count = nodes.Count; index < count; ++index)
            {
                DHtmlElement element = nodes[index] as DHtmlElement;
                if(element != null)
                {
                    if(IsMatching(element) == true)
                        result.Add(element);
                    if(searchChildren == true && element.Nodes.Count != 0)
                        SearchMatching(element.Nodes, result, searchChildren);
                }
            }
        }

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private List<DCssSimpleSelector> m_simpleSelectorList = new List<DCssSimpleSelector>();
        /// <summary>
        /// 
        /// </summary>
        private DCssPropertyCollection m_properties = new DCssPropertyCollection();
        /// <summary>
        /// 
        /// </summary>
        private int m_priority = 0;

    #endregion	

    }

}
