using System;
using System.Collections.Generic;

namespace DOL.DHtml.DCssResolver
{
    internal sealed class DCssSpecificity : IComparable<DCssSpecificity>
    {

    /////////////////////////////////////////////////////////////////////////////////
    #region 基本操作

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public DCssSpecificity()
        {
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public DCssSpecificity(DCssSpecificity other)
        {
            m_a = other.m_a;
            m_b = other.m_b;
            m_c = other.m_c;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public DCssSpecificity(int a, int b, int c)
        {
            m_a = a;
            m_b = b;
            m_c = c;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public void Add(DCssSpecificity other)
        {
            m_a += other.m_a;
            m_b += other.m_b;
            m_c += other.m_c;
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int A
        {
            get
            {
                return m_a;
            }
            set
            {
                m_a = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int B
        {
            get
            {
                return m_b;
            }
            set
            {
                m_b = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        public int C
        {
            get
            {
                return m_c;
            }
            set
            {
                m_c = value;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DCssSpecificity other)
        {
            if(m_a > other.m_a)
                return 1;
            else if(m_a < other.m_a)
                return -1;
            else if(m_b > other.m_b)
                return 1;
            else if(m_b < other.m_b)
                return -1;
            else if(m_c > other.m_c)
                return 1;
            else if(m_c < other.m_c)
                return -1;

            return 0;
        }

    #endregion

    /////////////////////////////////////////////////////////////////////////////////
    #region 內部資料

        /// <summary>
        /// 
        /// </summary>
        private int m_a = 0; // count the number of ID attributes in the selector (= a) 
        /// <summary>
        /// 
        /// </summary>
        private int m_b = 0; // count the number of other attributes and pseudo-classes in the selector (= b) 
        /// <summary>
        /// 
        /// </summary>
        private int m_c = 0; // count the number of element names in the selector (= c) 

    #endregion

    }
}
