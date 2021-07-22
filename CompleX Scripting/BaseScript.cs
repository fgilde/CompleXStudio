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
    public abstract class BaseScript {

        public void Initialize(CompleXScriptContext context) {
            this.Context = context;
        }

        public CompleXScriptContext Context { get; protected set; }

        public abstract void Execute();
    }
}