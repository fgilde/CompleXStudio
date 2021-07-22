//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CompleX_Library.Interfaces
{
    public interface IInserter:IHostedService
    {
        /// <summary>
        /// Name of object (example "Image")
        /// Dont forget to return a localized string 
        /// </summary>
        string NameOfObjectToInsert { get; }

        /// <summary>
        /// Gets the object to insert.
        /// </summary>
        /// <returns></returns>
        object GetObjectToInsert(InsertParameter param);

        /// <summary>
        /// Should return the type of requested parameter
        /// </summary>
        /// <returns></returns>
        Type GetParameterType();

        /// <summary>
        /// Image for MenuIcon and ToolBox entry
        /// </summary>
        /// <value>The image.</value>
        Image Image{ get;}

        /// <summary>
        /// ShortCut1 for Access this directly 
        /// </summary>
        /// <example>
        /// return Keys.Control | Keys.T as ShortCut1 and
        ///        Keys.Control | Keys.S as ShortCut2
        /// the real shortcut shuld be Ctrl+T,Ctrl+S
        /// use Keys.None if you dont want to have a shortcut
        /// </example>
        Keys ShortCutKey1 { get; }

        /// <summary>
        /// ShortCut2
        /// </summary>
        Keys ShortCutKey2 { get; }

    }
}
