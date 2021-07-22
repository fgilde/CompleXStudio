//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using CompleX_Library.Interfaces;

namespace CompleX_Optionpages
{
    public partial class BaseWizardOptionPage : BaseOptionPage,IWizardPageControl
    {
        public BaseWizardOptionPage()
        {
            InitializeComponent();
        }

       #region Implementation of IWizardPageControl

        ///<summary>
        /// Inits the control.
        ///</summary>
        public virtual void Init()
        {
            Initialize();
        }

        ///<summary>
        /// Triggers the control to invalidate its data.
        ///</summary>
        public virtual void InvalidateData(object prevoiusPageData)
        {
            OnActivated(true, false);
        }

        ///<summary>
        /// Checks if page change is allowed.
        ///</summary>
        ///<returns>True if change is allowed, otherwise false.</returns>
        public virtual bool ValidatePageChange()
        {
            var result = ValidatePage();
            return result.Result;
        }

        ///<summary>
        /// Gets the controls data.
        ///</summary>
        ///<returns>The controls data.</returns>
        public virtual object GetData()
        {
            return this;
        }

        /// <summary>
        /// Sets the controls data.
        /// </summary>
        /// <param name="data">The data object.</param>
        public virtual void SetData(object data)
        {}


        #endregion

    }
}
