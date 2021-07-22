//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
namespace CompleX_Library.Interfaces
{
    public interface IWizardPageControl
    {
        ///<summary>
        /// Inits the control.
        ///</summary>
        void Init();

        ///<summary>
        /// Triggers the control to invalidate its data.
        ///</summary>
        void InvalidateData(object prevoiusPageData);

        ///<summary>
        /// Checks if page change is allowed.
        ///</summary>
        ///<returns>True if change is allowed, otherwise false.</returns>
        bool ValidatePageChange();

        ///<summary>
        /// Gets the controls data.
        ///</summary>
        ///<returns>The controls data.</returns>
        object GetData();

        /// <summary>
        /// Sets the controls data.
        /// </summary>
        /// <param name="data">The data object.</param>
        void SetData(object data);
    }
}