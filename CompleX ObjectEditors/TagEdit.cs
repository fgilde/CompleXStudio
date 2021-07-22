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
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX_Library.Interfaces;
using CompleX_Types;

namespace CompleX_ObjectEditors
{
    [Export(typeof(IObjectEdit))]
    public class TagEdit:IObjectEdit
    {
        private Tag tag;

        #region Implementation of IEquatable<IHostedService>

        public bool Equals(IHostedService other)
        {
            return this.ID.Equals(other.ID);
        }

        #endregion

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("126585C8-3736-4508-A03A-A63A8C64BFA1"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "Tag Param Edit"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return Enumerable.Empty<string>(); }
        }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
        public bool Initialize()
        {
           return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;           
        }

        #endregion

        #region Implementation of IObjectEdit

        public event EventHandler ObjectEditingFinished;

        private void InvokeObjectEditingFinished(EventArgs e)
        {
            EventHandler finished = ObjectEditingFinished;
            if (finished != null) finished(this, e);
        }

        /// <summary>
        /// Control to edit object
        /// </summary>
        /// <value>The control.</value>
        public Control Control
        {
            get
            {
                var control = new TagSelector() {Tag = tag };
                control.TagChoosed += TagSelectFinished;
                return control;
            }
        }

        private void TagSelectFinished(Tag choosedTag)
        {
            tag = choosedTag;
            InvokeObjectEditingFinished(new EventArgs());
        }

        /// <summary>
        /// Calls the function to edit the object
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object Content
        {
            get { return tag; }
            set
            {
                if(value is Tag)
                    tag = value as Tag;
            }
        }

        /// <summary>
        /// Returns the type for the object you can edit
        /// </summary>
        /// <returns></returns>
        public Type GetPossibleObjectType()
        {
            return typeof (Tag);
        }

        #endregion
    }
}
