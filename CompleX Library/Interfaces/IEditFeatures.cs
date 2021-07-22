//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================

using System.Collections.Generic;

namespace CompleX_Library.Interfaces
{
    public interface IEditFeatures
    {

        /// <summary>
        /// List of custom actions. Return Emptylist if you dont have some
        /// </summary>
        /// <returns></returns>
        IEnumerable<ItemAction> GetCustomActions();

        /// <summary>
        /// Cut current selection to clipboard
        /// </summary>
        void CutToClipboard();

        /// <summary>
        /// Copy to Clipboard
        /// </summary>
        void CopyToClipboard();

        /// <summary>
        /// Paste from clipboard
        /// </summary>
        void PasteFromClipboard();

        /// <summary>
        /// Undo
        /// </summary>
        void Undo();

        /// <summary>
        /// Redo
        /// </summary>
        void Redo();

        /// <summary>
        /// Select All
        /// </summary>
        void SelectAll();

        /// <summary>
        /// Returns if editor can cut at this moment
        /// </summary>
        bool CanCut { get; }

        /// <summary>
        /// Can Copy at this moment
        /// </summary>
        bool CanCopy { get; }

        /// <summary>
        /// Can Paste
        /// </summary>
        bool CanPaste { get; }

        /// <summary>
        /// Can undo
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// Can Redo
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// Can Select
        /// </summary>
        bool CanSelect { get; } 
    }
}
