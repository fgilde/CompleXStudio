//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================

using System;

namespace CompleX_Library
{
    public class ValidationResult
    {
        /// <summary>
        /// The Boolean result
        /// </summary>
        public bool Result{ get; set;}

        /// <summary>
        /// Errormessage
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationResult(){}

        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationResult(bool result):this(result,String.Empty)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public ValidationResult(bool result, string message):this()
        {
            Result = result;
            ErrorMessage = message;
        }

    }
}