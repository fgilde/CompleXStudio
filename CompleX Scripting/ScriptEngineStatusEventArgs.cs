//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;

namespace CompleX.Scripting
{
    public class ScriptEngineStatusEventArgs : EventArgs {

        public ScriptEngineStatusEventArgs(string status) {
            this.Status = status;
        }

        public string Status { get; protected set; }
    }
}