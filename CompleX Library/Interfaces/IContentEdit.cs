//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;

namespace CompleX_Library.Interfaces
{
    public interface IContentEdit:IHostedService
    {

        event EventHandler OnFocus;
        event EventHandler ContentChanged;
        event EventHandler SelectionChanged; 

        bool SaveToFile(string fileName);

        object SelectedContent { get; set; }
        object Content { get; set; }

        void Insert(object obj);

    }
}
