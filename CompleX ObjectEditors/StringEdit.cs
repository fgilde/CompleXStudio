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
using System.Windows.Forms;
using CompleX.Helper;
using CompleX_Library.Interfaces;

namespace CompleX_ObjectEditors
{
    [Export(typeof(IObjectEdit))]
    public partial class StringEdit : UserControl, IObjectEdit
    {
        public StringEdit()
        {
            InitializeComponent();
            textBox.ContextMenuStrip = PlaceHolder.GetPopUpMenu(s =>textBox.SelectedText=s, true);
        }

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("E25C085B-181C-4FA0-9AC4-8D20501BC9EF");}
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "String Edit"; }
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
            get { return this; }
        }

        /// <summary>
        /// Calls the function to edit the object
        /// </summary>
        /// <returns></returns>
        public object Content
        {
            get { return textBox.Text; }
            set { textBox.Text = value.ToString();}
        }

        /// <summary>
        /// Returns the type for the object you can edit
        /// </summary>
        /// <returns></returns>
        public Type GetPossibleObjectType()
        {
            return typeof(string);
        }

        #endregion

        private void buttonOk_Click(object sender, EventArgs e)
        {
            InvokeObjectEditingFinished(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox.SelectedText = PlaceHolder.SelectPlaceHolderDialog();
        }

    }
}
