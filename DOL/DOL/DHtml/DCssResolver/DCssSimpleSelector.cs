/*****************************************************************************\
>	Copyright 2004 DOL for design studio.
>
>	DOLS DCssSimpleSelector  Class
>
>	E-mail：	  nomad_libra.tw@yahoo.com.tw
>	E-mail：	  jameshrsp@ms2.url.com.tw
>
\*****************************************************************************/

// DCssSimpleSelector .cs: implementation of the DCssSimpleSelector  class.
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections;
using DOL.DHtml.DHtmlParser;
using DOL.DHtml.DHtmlParser.Node;

namespace DOL.DHtml.DCssResolver
{
    public sealed class DCssSimpleSelector : ICloneable, IComparable<DCssSimpleSelector>, IEquatable<DCssSimpleSelector>, DOL.DBase.DIDiagnosisable
    {

    /////////////////////////////////////////////////////////////////////////////////
    #region 基本操作

        public enum Relation
        {            
            Descendant, // E   V  (Whitespace)
            Child,      // E > V
            Sibling,    // E + V
        }

        public enum AttributeMatching
        {
            None,
            ExactlyEqualName,       // E[foo]
            ExactlyEqualValue,      // E[foo="value"]
            IncludesValue,          // E[foo~="value"] includes 
            BeginValue              // E[foo|="value"] dashmatch 
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 相關操作

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DCssSimpleSelector()
        {            
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        public DCssSimpleSelector(DCssSimpleSelector selector)
        {
            m_combinator = selector.m_combinator;
            m_id = selector.m_id;
            m_class = selector.m_class;
            m_elementNames = selector.m_elementNames;
            m_attributeMatchingType = selector.m_attributeMatchingType;
            m_attributeName = selector.m_attributeName;
            m_attributeValue = selector.m_attributeValue;
            m_pseudo = selector.m_pseudo;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public DCssSimpleSelector(string id)
        {
            m_id = id.Trim().ToLower();
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementNames"></param>
        /// <param name="className"></param>
        public DCssSimpleSelector(string elementNames, string className)
        {
            m_class = className.Trim().ToLower();
            m_elementNames = elementNames.Trim().ToLower();
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementNames"></param>
        /// <param name="id"></param>
        /// <param name="className"></param>
        public DCssSimpleSelector(string elementNames, string id, string className)
        {
            m_id = id;
            m_class = className.Trim().ToLower();
            m_elementNames = elementNames.Trim().ToLower();
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementNames"></param>
        /// <param name="id"></param>
        /// <param name="className"></param>
        /// <param name="combinator"></param>
        public DCssSimpleSelector(string elementNames, string id, string className, Relation combinator)
        {
            m_combinator = combinator;
            m_id = id.Trim().ToLower();
            m_class = className.Trim().ToLower();
            m_elementNames = elementNames.Trim().ToLower();
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementNames"></param>
        /// <param name="id"></param>
        /// <param name="className"></param>
        /// <param name="attributeMatchingType"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="pseudoClass"></param>
        /// <param name="combinator"></param>
        public DCssSimpleSelector(string elementNames, string id, string className, 
            AttributeMatching attributeMatchingType, string attributeName, string attributeValue, string pseudo,
            Relation combinator)
        {
            m_combinator = combinator;
            m_id = id.Trim().ToLower();
            m_class = className.Trim().ToLower();
            m_elementNames = elementNames.Trim().ToLower();
            m_attributeMatchingType = attributeMatchingType;
            m_attributeName = attributeName.Trim().ToLower();
            m_attributeValue = attributeValue;
            m_pseudo = pseudo;
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
        /// <param name="buffer"></param>
        /// <param name="prefix"></param>        
        public void Dump(StringBuilder buffer, string prefix)
        {
            AssertValid();
            string old = prefix;
            buffer.Append(old + "├Object " + GetType().Name + " Dump : \n");

            prefix += "│　";
            buffer.Append(prefix + "CSS Value: " + this.CSS + "\n");	
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new DCssSimpleSelector(this);
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DCssSimpleSelector other)
        {
            return m_combinator.Equals(other.m_combinator) &&
                   m_id.Equals(other.m_id) &&
                   m_class.Equals(other.m_class) &&
                   m_elementNames.Equals(other.m_elementNames) &&
                   m_attributeMatchingType == other.m_attributeMatchingType &&
                   m_attributeName.Equals(other.m_attributeName) &&
                   m_attributeValue.Equals(other.m_attributeValue) &&
                   m_pseudo.Equals(other.m_pseudo);
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DCssSimpleSelector other)
        {
            return this.SpecificityValue.CompareTo(other.SpecificityValue);
        }

    #endregion	

    /////////////////////////////////////////////////////////////////////////////////
    #region 屬性設定

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        internal DCssSpecificity SpecificityValue
        {
            get
            {
                DCssSpecificity result = new DCssSpecificity();

                if(m_id.Equals("") == false)
                    ++result.A;
                if(m_class.Equals("") == false || m_attributeMatchingType != AttributeMatching.None)
                    ++result.B;
                if(m_elementNames.Equals("") == false && m_elementNames.Equals("*") == false)
                    ++result.C;

                return result;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsMatching(DHtmlElement element)
        {
            System.Diagnostics.Debug.Assert(element != null);

            // id            
            if(m_id.Length != 0)
            {
                DHtmlAttribute attribute = element.Attributes["id"];
                if(attribute == null || m_id.Equals(attribute.Value) == false) // match ID
                    return false;
            }

            // class            
            if(m_class.Length != 0)
            {
                DHtmlAttribute attribute = element.Attributes["class"];
                if(attribute == null || m_class.Equals(attribute.Value) == false) // match class
                    return false;
            }

            // element name
            if((m_elementNames.Length != 0 && m_elementNames.Equals("*") == false) &&
               m_elementNames.Equals(element.Name) == false) // match tag
                return false;

            // attribute
            switch(m_attributeMatchingType)
            {
                case AttributeMatching.None: break;

                case AttributeMatching.ExactlyEqualName: // E[foo]
                    if(m_attributeName.Equals("") == false)
                    {
                        int attributeIndex = element.Attributes.IndexOf(m_attributeName);
                        if(attributeIndex == -1) return false;
                    }
                    break;

                case AttributeMatching.ExactlyEqualValue: // E[foo="warning"]
                    if(m_attributeName.Equals("") == false)
                    {
                        int attributeIndex = element.Attributes.IndexOf(m_attributeName);
                        if(attributeIndex == -1 ||
                           element.Attributes[attributeIndex].Value.Equals(m_attributeValue) == false) 
                            return false;
                    }
                    break;

                case AttributeMatching.IncludesValue: // E[foo~="warning"]
                    if(m_attributeName.Equals("") == false)
                    {
                        int attributeIndex = element.Attributes.IndexOf(m_attributeName);
                        if(attributeIndex == -1 ||
                           element.Attributes[attributeIndex].Value.IndexOf(m_attributeValue) == -1)
                            return false;
                            
                    }
                    break;

                case AttributeMatching.BeginValue: // E[lang|="en"]
                    if(m_attributeName.Equals("") == false)
                    {
                        int attributeIndex = element.Attributes.IndexOf(m_attributeName);
                        if(attributeIndex == -1 || 
                           element.Attributes[attributeIndex].Value.IndexOf(m_attributeValue) != 0)
                            return false;
                    }                    
                    break;                

                default: System.Diagnostics.Debug.Assert(false); break;                
            }

            return true;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public Relation Combinator
        {
            get
            {
                return m_combinator;
            }
            set
            {
                m_combinator = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string ElementNames 
        {
            get
            {
                return m_elementNames;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_elementNames = value.Trim().ToLower();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            get
            {
                return m_id;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_id = value.Trim().ToLower();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string Class
        {
            get
            {
                return m_class;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_class = value.Trim().ToLower();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public AttributeMatching AttributeMatchingType
        {
            get
            {
                return m_attributeMatchingType;
            }
            set
            {
                m_attributeMatchingType = value;
            }

        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string AttributeName
        {
            get
            {
                return m_attributeName;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_attributeName = value.Trim().ToLower();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string AttributeValue
        {
            get
            {
                return m_attributeValue;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_attributeValue = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string Pseudo 
        {
            get
            {
                return m_pseudo;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                m_pseudo = value;
            }
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 匯出

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public string CSS
        {
            get
            {
                StringBuilder buffer = new StringBuilder();
                TransformCSS(buffer);
                return buffer.ToString();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This will return the full CSS to represent this property
        /// </summary>
        internal void TransformCSS(StringBuilder writer)
        {
            System.Diagnostics.Debug.Assert(writer != null);

            if(m_elementNames.Length != 0)
                writer.Append(m_elementNames);
            if(m_id.Length != 0)
                writer.Append("#" + m_id);
            if(m_class.Length != 0)
                writer.Append("." + m_class);

            switch(m_attributeMatchingType)
            {
                case AttributeMatching.ExactlyEqualName:
                    writer.Append("[" + m_attributeName + "]");
                    break;

                case AttributeMatching.ExactlyEqualValue:
                    writer.Append("[" + m_attributeName + "=" + m_attributeValue + "]");
                    break;

                case AttributeMatching.IncludesValue:
                    writer.Append("[" + m_attributeName + "~=" + m_attributeValue + "]");
                    break;

                case AttributeMatching.BeginValue:
                    writer.Append("[" + m_attributeName + "|=" + m_attributeValue + "]");
                    break;

                default: break;

            }

            if(m_pseudo.Length != 0)
                writer.Append(":" + m_pseudo);
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private Relation m_combinator = Relation.Descendant;
        /// <summary>
        /// 
        /// </summary>
        private string m_id = "";
        /// <summary>
        /// 
        /// </summary>
        private string m_class = "";
        /// <summary>
        /// 
        /// </summary>
        private string m_elementNames = "";
        /// <summary>
        /// 
        /// </summary>
        private AttributeMatching m_attributeMatchingType = AttributeMatching.None;
        /// <summary>
        /// 
        /// </summary>
        private string m_attributeName = "";
        /// <summary>
        /// 
        /// </summary>
        private string m_attributeValue = "";
        /// <summary>
        /// 
        /// </summary>
        private string m_pseudo = "";


    #endregion

    }
}