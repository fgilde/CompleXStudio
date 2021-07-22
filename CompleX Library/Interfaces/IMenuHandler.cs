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
using System.Windows.Forms;

namespace CompleX_Library.Interfaces
{
    public interface IMenuHandler:IContentEdit
    {
        /// <summary>
        /// should return the menu strip to merge.
        /// </summary>
        /// <value>The menu.</value>
        MenuStrip Menu { get; }
    }
}
