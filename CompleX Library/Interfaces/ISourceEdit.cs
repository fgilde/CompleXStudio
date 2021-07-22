//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
namespace CompleX_Library.Interfaces
{

    public interface ISourceEdit : IContentEdit
    {
        /// <summary>
        /// Load file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool LoadFromFile(string fileName);

        /// <summary>
        /// Set if contextmenu is handled manual
        /// </summary>
        bool ContextMenuIsHandled { get; }

    }
}
