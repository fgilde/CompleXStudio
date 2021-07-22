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
using System.Linq;
using System.Text;

namespace CompleX_Library
{
    public class InsertParameter
    {
        /// <summary>
        /// Parameter Data
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets the type of the data null secure.
        /// </summary>
        /// <value>The type of the data.</value>     
        public Type DataType
        {
            get
            {
                if (Data != null)
                    return Data.GetType();
                return typeof (object);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has some data.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has some data; otherwise, <c>false</c>.
        /// </value>
        public bool HasSomeData
        {
            get
            {
                return Data != null;
            }
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <value>The name of the data type.</value>
        public string DataTypeName
        {
            get
            {
                if (Data != null)
                    return Data.GetType().ToString();
                return String.Empty;    
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertParameter"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public InsertParameter(object data)
        {
            Data = data;
        }

        /// <summary>
        /// Creates a empty instance of <see cref="InsertParameter"/>.
        /// </summary>
        /// <returns></returns>
        public static InsertParameter CreateEmpty()
        {
           return new InsertParameter(null);
        }
        
    }
}
