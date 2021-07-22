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
    public interface IHostedService
    {        
        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        IEnumerable<string> SupportedFileExtensions { get; }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
        bool Initialize();

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        Version GetVersion();

    }
}
