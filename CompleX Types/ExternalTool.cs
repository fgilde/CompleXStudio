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
using System.Windows.Forms;

namespace CompleX_Types
{
    [Serializable]
    public class ExternalTool: ICloneable
    {
        /// <summary>
        /// Tool Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Command 
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Argument
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// Directory
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// Gets or sets the shortcut.
        /// </summary>
        /// <value>The shortcut.</value>
        public Shortcut Shortcut { get; set; }

        /// <summary>
        /// ShowModal
        /// </summary>
        public bool ShowModal { get; set; }

        /// <summary>
        /// ReloadFileAfterClose
        /// </summary>
        public bool ReloadFileAfterClose { get; set; }

        /// <summary>
        /// If it is set, this tool only visible if FileExtensions contains current extension 
        /// </summary>
        public IEnumerable<string> FileExtensions { get; set; }

        #region Implementation of ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
