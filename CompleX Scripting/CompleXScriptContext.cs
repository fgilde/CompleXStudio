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
    public class CompleXScriptContext {

        public string UserName { get; set; }

        public StringDelegate WriteLineDelegate { get; set; }

        public virtual void WriteLine(string text) {
            if(this.WriteLineDelegate != null) {
                this.WriteLineDelegate(text);
            }
        }
    }
}