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

namespace CompleX.Classes
{
    ///<summary>
    /// Holds information for the wizard dialog execution.
    ///</summary>
    public class WizardParameters
    {
        ///<summary>
        /// Constructor.
        ///</summary>
        public WizardParameters()
        {
            InteriorPagesParams = new List<WizardInteriorPageParameters>();
        }

        ///<summary>
        /// Caption text for the dialog.
        ///</summary>
        public string DialogCaption { get; set; }

        ///<summary>
        /// The params for the welcome page.
        ///</summary>
        public WizardWelcomePageParameters WelcomePageParams { get; set; }

        ///<summary>
        /// The params for the interior pages.
        ///</summary>
        public List<WizardInteriorPageParameters> InteriorPagesParams { get; set; }

        ///<summary>
        /// The params for the completion page.
        ///</summary>
        public WizardCompletionPageParameters CompletionPageParams { get; set; }
    }

    ///<summary>
    /// Holds information about the welcome page.
    ///</summary>
    public class WizardWelcomePageParameters
    {
        ///<summary>
        /// Title text to display.
        ///</summary>
        public string TitleText { get; set; }

        ///<summary>
        /// Proceed text to display.
        ///</summary>
        public string ProceedText { get; set; }

        ///<summary>
        /// Introduction text to display.
        ///</summary>
        public string IntroductionText { get; set; }
    }

    ///<summary>
    /// Holds information about one interior page.
    ///</summary>
    public class WizardInteriorPageParameters
    {
        ///<summary>
        /// Title text to display.
        ///</summary>
        public string TitleText { get; set; }

        ///<summary>
        /// Sub title text to display.
        ///</summary>
        public string SubTitleText { get; set; }

        ///<summary>
        /// The page object type that implements IWizardPageControl.
        ///</summary>
        public Type PageType { get; set; }

        /// <summary>
        /// The data to be set.
        /// </summary>
        public object Data { get; set; }
    }

    ///<summary>
    /// Holds information about the completion page.
    ///</summary>
    public class WizardCompletionPageParameters
    {
        ///<summary>
        /// The title text to display.
        ///</summary>
        public string TitleText { get; set; }

        ///<summary>
        /// The proceed text to display.
        ///</summary>
        public string ProceedText { get; set; }

        ///<summary>
        /// The finish text to display.
        ///</summary>
        public string FinishText { get; set; }
    }
}
