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
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Controls;
using CompleX_Library.Interfaces;
using CompleX_Types;
using DevExpress.XtraWizard;

namespace CompleX.Dialogs
{
    public partial class WizardDialog : DevExpress.XtraEditors.XtraForm
    {
        private WizardParameters wizardParameters;
        private bool isInitializing;
        private Func<Dictionary<Type, object>, bool> finishAction;
        private Func<Dictionary<Type, object>, bool> validateWizardCompletionHandler;

        ///<summary>
        /// Constructor.
        ///</summary>
        public WizardDialog()
        {
            InitializeComponent();
            //wizardControl.Pages.Clear();
        }

        ///<summary>
        /// Executes the dialog.
        ///</summary>
        ///<param name="wizardStyle"></param>
        ///<param name="parameters">Parameters for the wizard execution.</param>
        ///<param name="validateWizardCompletionHandler">Optional validator for wizard completion.</param>
        ///<param name="resultData">The resulting data.</param>
        ///<returns>True on ok, false on cancel.</returns>
        public static bool Exec(
            WizardStyle wizardStyle,
            WizardParameters parameters,
            Func<Dictionary<Type, object>, bool> validateWizardCompletionHandler,
            out Dictionary<Type, object> resultData)
        {
            using (var dialog = new WizardDialog())
            {
                dialog.wizardControl.WizardStyle = wizardStyle;
                dialog.Init(parameters, validateWizardCompletionHandler);

                bool result = dialog.ShowDialog() == DialogResult.OK;
                resultData = dialog.GetData();

                return result;
            }
        }

        ///<summary>
        /// Executes the dialog .
        ///</summary>
        ///<param name="wizardStyle"></param>
        ///<param name="parameters">Parameters for the wizard execution.</param>
        ///<param name="validateWizardCompletionHandler">Optional validator for wizard completion.</param>
        ///<param name="finishAction"></param>
        public static void Exec(
            WizardStyle wizardStyle,
            WizardParameters parameters,
            Func<Dictionary<Type, object>, bool> validateWizardCompletionHandler,
            Func<Dictionary<Type, object>, bool> finishAction)
        {

            var dialog = new WizardDialog {wizardControl = {WizardStyle = wizardStyle}};

            dialog.Init(parameters, validateWizardCompletionHandler, finishAction);

            dialog.Show();
            
        }


        public IWizardPageControl GetPrevoiusPageControl()
        {
            IWizardPageControl result = null;
            if ((wizardControl.SelectedPage != null) && (wizardControl.SelectedPage.Controls.Count > 0))
            {
                if (wizardControl.SelectedPageIndex > 0)
                {
                    var wizardPage = wizardControl.Pages[wizardControl.SelectedPageIndex - 1];
                    if (wizardPage != null)
                        result = wizardPage.Controls[0] as IWizardPageControl;
                }
            }
            return result;
        }

        public IWizardPageControl GetNextPageControl()
        {
            IWizardPageControl result = null;
            if ((wizardControl.SelectedPage != null) && (wizardControl.SelectedPage.Controls.Count > 0))
            {
                if (wizardControl.SelectedPageIndex < wizardControl.Pages.Count - 1)
                {
                    var wizardPage = wizardControl.Pages[wizardControl.SelectedPageIndex + 1];
                    if (wizardPage != null)
                        result = wizardPage.Controls[0] as IWizardPageControl;
                }
            }
            return result;
        }

        public IWizardPageControl GetCurrentPageControl()
        {
            IWizardPageControl result = null;
            if ((wizardControl.SelectedPage != null) && (wizardControl.SelectedPage.Controls.Count > 0))
            {
                result = wizardControl.SelectedPage.Controls[0] as IWizardPageControl;
            }
            return result;
        }

        private void Init(
            WizardParameters parameters,
            Func<Dictionary<Type, object>, bool> validationCompletionHandler,
            Func<Dictionary<Type, object>, bool> wizardFinishAction)
        {
            finishAction = wizardFinishAction;
            Init(parameters, validationCompletionHandler);
        }


        private void Init(
            WizardParameters parameters,
            Func<Dictionary<Type, object>, bool> validationCompletionHandler)
        {
            isInitializing = true;
            try
            {
                wizardParameters = parameters;
                validateWizardCompletionHandler = validationCompletionHandler;
                Text = parameters.DialogCaption;

                // Welcome page.
                if (parameters.WelcomePageParams != null)
                {
                    var welcomeWizardPage = new WelcomeWizardPage
                    {
                        Text = parameters.WelcomePageParams.TitleText,
                        ProceedText = parameters.WelcomePageParams.ProceedText,
                        IntroductionText = parameters.WelcomePageParams.IntroductionText
                    };
                    wizardControl.Pages.Add(welcomeWizardPage);
                }

                // Interior pages.
                foreach (WizardInteriorPageParameters param in parameters.InteriorPagesParams)
                {
                    var interiorPageControl = Activator.CreateInstance(param.PageType) as UserControl;


                    if (interiorPageControl == null)
                        throw new ArgumentException(
                            "Controls for wizard pages must be derived from UserControl.");

                    var interiorPageControlInterface = interiorPageControl as IWizardPageControl;

                    if (interiorPageControlInterface == null)
                        throw new ArgumentException(
                            "Controls for wizard pages must implement IWizardPageControl.");

                    var page = new WizardPage { Text = param.TitleText, DescriptionText = param.SubTitleText };
                    page.Controls.Add(interiorPageControl);
                    wizardControl.Pages.Add(page);

                    interiorPageControlInterface.Init();
                    interiorPageControl.Dock = DockStyle.Fill;
                }

                // Completion page.
                if (parameters.CompletionPageParams != null)
                {
                    var completionWizardPage = new CompletionWizardPage
                    { 
                        Text = parameters.CompletionPageParams.TitleText,
                        FinishText = parameters.CompletionPageParams.FinishText,
                        ProceedText = parameters.CompletionPageParams.ProceedText
                    };
                    wizardControl.Pages.Add(completionWizardPage);
                }
            }
            finally
            {
                isInitializing = false;
            }
        }

        private Dictionary<Type, object> GetData()
        {
            var result = new Dictionary<Type, object>();
            foreach (BaseWizardPage page in wizardControl.Pages)
            {
                var interiorPage = page as WizardPage;
                if ((interiorPage != null) && (interiorPage.Controls.Count > 0))
                {
                    var pageControl = interiorPage.Controls[0] as IWizardPageControl;
                    if (pageControl != null)
                    {
                        result.Add(pageControl.GetType(), pageControl.GetData());
                    }
                }
            }
            return result;
        }

        private void wizardControl_NextClick(object sender, WizardCommandButtonClickEventArgs e)
        {
            var interiorPage = e.Page as WizardPage;
            if ((interiorPage != null) && (interiorPage.Controls.Count > 0))
            {
                var pageControl = interiorPage.Controls[0] as IWizardPageControl;
                if (pageControl != null)
                {
                    e.Handled = !pageControl.ValidatePageChange();
                }
            }
        }

        private void wizardControl_SelectedPageChanged(object sender, WizardPageChangedEventArgs e)
        {
            if (!isInitializing && (e.Direction == Direction.Forward))
            {
                InitCurrentPage();
            }
        }

        private void InitCurrentPage()
        {
            IWizardPageControl pageControl = GetCurrentPageControl();
            if (pageControl != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    object prevoiusPageData = null;
                    var prevoiusPageControl = GetPrevoiusPageControl();
                    if (prevoiusPageControl != null)
                    {
                        prevoiusPageData = prevoiusPageControl.GetData();
                    }
                    // Retrieve the content to work with
                    pageControl.InvalidateData(prevoiusPageData);

                    // Set the page data 
                    object pageData = GetPageData();
                    if (pageData != null)
                        pageControl.SetData(pageData);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private object GetPageData()
        {
            object result = null;

            var currentPageControl = GetCurrentPageControl();
            if (currentPageControl != null)
            {
                Type controlType = currentPageControl.GetType();
                WizardInteriorPageParameters pageParameters =
                    wizardParameters.InteriorPagesParams.Where(
                        parameters => parameters.PageType.Equals(controlType)).FirstOrDefault();

                if (pageParameters != null)
                {
                    result = pageParameters.Data;
                }
            }
            return result;
        }

        private void wizardControl_FinishClick(object sender, CancelEventArgs e)
        {
            // Validate entered data.
            if (validateWizardCompletionHandler != null)
            {
                Dictionary<Type, object> data = GetData();
                e.Cancel = !validateWizardCompletionHandler(data);
            }

            if (!e.Cancel)
            {
                // If the current page has a control -> validate the page change against it.
                IWizardPageControl currentPageControl = GetCurrentPageControl();
                if (currentPageControl != null)
                {
                    e.Cancel = !currentPageControl.ValidatePageChange();
                }

                if(finishAction != null)
                {
                    bool canClose = finishAction(GetData());
                    if(canClose)
                    {
                        Close();
                    }
                }
            }
        }


        private void WizardDialog_Shown(object sender, EventArgs e)
        {
            IWizardPageControl pageControl = GetCurrentPageControl();
            if (pageControl != null)
                pageControl.Init();
        }


        public static bool ShowTagWizard(out Tag tag)
        {
            tag = null;
            WizardParameters parameters = WizardParameterFactory.GetInsertTagParameters();
            Dictionary<Type, object> tagData;

            //WizardDialog.Exec(WizardStyle.Wizard97, parameters, dictionary => true, WizardFinish);

            if (Exec(WizardStyle.Wizard97, parameters, dictionary => true, out tagData))
            {
                tag = tagData[typeof(TagEditControl)] as Tag;
                return true;
            }
            return false;
        }

        public static bool ShowFirstStartWizard()
        {
            WizardParameters parameters = WizardParameterFactory.GetFirstStartSettingParameters();
            Dictionary<Type, object> result;
            bool executed = Exec(WizardStyle.Wizard97, parameters, dictionary => true, out result);

            foreach (KeyValuePair<Type, object> valuePair in result)
            {
                if(valuePair.Value is ISettingsPage)
                {
                    if (executed)
                        ((ISettingsPage) valuePair.Value).OnOk();
                    else
                        ((ISettingsPage)valuePair.Value).OnCancel();
                }
            }
            if(executed)
                CompleX_Settings.Settings.SaveSettings();
            return executed;

        }

    }
}