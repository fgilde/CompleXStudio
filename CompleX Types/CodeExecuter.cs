//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using CompleX_Library.Interfaces;

namespace CompleX_Types
{
    public class CodeExecuter
    {
        public IExecutable Executer { get; set; }
        public Guid Id { get; private set; }

        public CodeExecuter(IExecutable executable)
        {
            Id = Guid.NewGuid();
            Executer = executable;
        }
    }
}