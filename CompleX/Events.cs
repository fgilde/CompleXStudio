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
using System.Threading;
using CompleX.Dialogs;
using CompleX.ServiceModel;
using CompleX.Helper;
using CompleX_Library;


namespace CompleX
{
    public class ComplexEvents
    {
        private static object syncObject = new object();
        private static ComplexEvents events;
        public delegate void ModeChangeHandler(SwitchedEditMode newModi);
        public delegate void EditorTypeChangeHandler(EditMode mode);
        
        internal ComplexEvents()
        {
            
        }

        public static ComplexEvents Default
        {
            get
            {
                lock (syncObject)
                {
                    if (events == null)
                        events = new ComplexEvents();
                    return events;
                }
            }
        }

        public event EventHandler ToolWindowChanged;
        public event EventHandler FileChanged;
        public event EventHandler SelectionChanged;
        public event EventHandler ClipboardChanged;
        public event ModeChangeHandler ActiveEditModeChanged;

        internal void InvokeActiveEditModeChanged(SwitchedEditMode newModi)
        {
            ModeChangeHandler Handler = ActiveEditModeChanged;
            if (Handler != null) Handler(newModi);
        }

        internal void InvokeClipboardChanged(EventArgs e)
        {
            EventHandler handler = ClipboardChanged;
            if (handler != null) handler(this, e);
        }


        internal void InvokeSelectionChanged(object sender, EventArgs e)
        {
            EventHandler handler = SelectionChanged;
            if (handler != null) handler(sender, e);
        }

        internal void InvokeFileChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
                                             {
                                                 EventHandler handler = FileChanged;
                                                 if (handler != null) handler(sender, e);
                                             });
        }

        internal void InvokeToolWindowChanged(object sender, EventArgs e)
        {
            EventHandler handler = ToolWindowChanged;
            if (handler != null) handler(sender, e);
        }
        


    }
}
