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
    public interface IObjectEdit:IHostedService
    {
        /// <summary>
        /// Occurs when [object editing finished].
        /// </summary>
        event EventHandler ObjectEditingFinished; 

        /// <summary>
        /// Control to edit object
        /// </summary>
        /// <value>The control.</value>
        Control Control { get; }

        /// <summary>
        /// Calls the function to edit the object
        /// </summary>
        /// <returns></returns>
        object Content { get; set; }

        /// <summary>
        /// Returns the type for the object you can edit
        /// </summary>
        /// <returns></returns>
        Type GetPossibleObjectType();
    }
}
