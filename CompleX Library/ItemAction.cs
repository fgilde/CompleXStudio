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

namespace CompleX_Library
{
    public class ItemAction
    {
        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        public ItemAction(string text, Action action)
        {
            Action = action;
            Caption = text;
        }

        /// <summary>
        /// Action what this Item should do on click
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Icon for Menu and Toolbarbutton
        /// </summary>
        public Icon Image { get; set; }

        /// <summary>
        /// Text/Caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Begin new Itemgroup
        /// </summary>
        public bool BeginGroup { get; set; }

        /// <summary>
        /// ShortCut
        /// </summary>
        public Shortcut ShortCut { get; set; }


    }
}
