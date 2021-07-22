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
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CompleX_Library;
using CompleX_Library.Interfaces;

namespace CompleX_Optionpages
{
    
    public partial class BaseOptionPage : UserControl,ISettingsPage
    {
        protected BaseOptionPage()
        {
            InitializeComponent();
        }

        public virtual void OnActivated(bool activated, bool asProjectOptions)
        {
            
        }

        public virtual bool OnOk()
        {
            return true;
        }

        public virtual void OnCancel()
        {
        }

        public virtual ValidationResult ValidatePage()
        {
            return new ValidationResult(true, String.Empty);
        }

        public virtual bool AllowedAsProjectOptions
        {
            get { return true; }
        }

        public virtual string PageID { get { return Name; } }

        public virtual string ParentPageID
        {
            get { return "GeneralOptions"; }
        }

        public virtual string PageTitle { get { return Name; } }

        public virtual Image Image
        {
            get { return null; }
        }

        public virtual Control Control
        {
            get { return this; }
        }

        public virtual bool Equals(IHostedService other)
        {
            return ID.Equals(other.ID);
        }

        public virtual Guid ID
        {
            get { return Guid.NewGuid(); }
        }

        public virtual string ServiceName
        {
            get { return PageTitle; }
        }

        public virtual IEnumerable<string> SupportedFileExtensions
        {
            get { return Enumerable.Empty<string>(); }
        }

        public virtual bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public virtual Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;          
        }
    }
}
