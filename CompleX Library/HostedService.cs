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
using System.Reflection;
using CompleX_Library.Interfaces;

namespace CompleX_Library
{
    public class HostedService: IHostedService 
    {
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public virtual bool Equals(IHostedService other)
        {
            return ID.Equals(other.ID);
        }

        /// <summary>
        /// Guid
        /// </summary>
        /// <value></value>
        public virtual Guid ID
        {
            get { return Guid.NewGuid(); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        /// <value></value>
        public virtual string ServiceName
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        /// <value></value>
        public virtual IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { "*.*" }; }
        }

        /// <summary>
        /// Function call if service is added
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public virtual Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}