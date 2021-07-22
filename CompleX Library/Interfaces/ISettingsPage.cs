//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Drawing;
using System.Windows.Forms;

namespace CompleX_Library.Interfaces
{
    
    public interface ISettingsPage:IHostedService
    {

        /// <summary>
        ///  Called when page becomes active or inactive
        /// </summary>
        /// <param name="activated"></param>
        /// <param name="asProjectOptions"></param>
        void OnActivated(bool activated, bool asProjectOptions);

        /// <summary>
        ///  Options dialog is closed with OK button, apply changes
        /// </summary>
        /// <returns></returns>
        bool OnOk();

        /// <summary>
        ///  Options dialog is closed with Cancel button, check if you need to undo changes
        /// </summary>
        /// <returns></returns>
        void OnCancel();

        /// <summary>
        ///  Verifies if page has consistent state
        /// </summary>
        /// <returns></returns>
        ValidationResult ValidatePage();

        /// <summary>
        /// Gets if its allowed to use this page as project options
        /// </summary>
        /// <returns></returns>
        bool AllowedAsProjectOptions{ get;}

        /// <summary>
        /// PageID
        /// </summary>
        string PageID { get; }

        /// <summary>
        /// The PageId of the ParentPage
        /// </summary>
        string ParentPageID { get; }

        /// <summary>
        /// PageTitleName
        /// </summary>
        string PageTitle { get; }

        /// <summary>
        /// Image will be used as Icon
        /// </summary>
        Image Image { get; }

        /// <summary>
        /// The Usercontrol to display
        /// </summary>
        Control Control { get; }

       
    }
}
