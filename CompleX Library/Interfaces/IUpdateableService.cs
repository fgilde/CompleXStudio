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

namespace CompleX_Library.Interfaces
{
    public interface IUpdateableService:IHostedService
    {

        /// <summary>
        /// Should return the Url to a zip file 
        /// <example>http://www.mysserver/myaddin.zip</example>
        /// </summary>
        /// <value>The URL.</value>
        string Url { get; }

        /// <summary>
        /// Should check if a new verison is available
        /// </summary>
        /// <returns></returns>
        bool NewVersionAvailable();
    }
}
