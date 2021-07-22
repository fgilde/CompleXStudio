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
using CompleX_Library.Interfaces;

namespace CompleX_Library
{
    public class EditorInformation
    {

        /// <summary>
        /// Gets or sets the edit mode.
        /// </summary>
        /// <value>The edit mode.</value>
        public EditMode EditMode { get; set; }


        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is saved.
        /// </summary>
        /// <value><c>true</c> if this instance is saved; otherwise, <c>false</c>.</value>
        public bool IsSaved { get; set; }

        /// <summary>
        /// Gets or sets the edit control.
        /// </summary>
        /// <value>The edit control.</value>
        public ISourceEdit EditControl { get;  set; }

        /// <summary>
        /// Gets or sets the designer.
        /// </summary>
        /// <value>The designer.</value>
        public IDesignable Designer { get;  set; }
    }

    public enum EditMode
    {
        Editor,
        Designer,
        Unknown,
    }

}
