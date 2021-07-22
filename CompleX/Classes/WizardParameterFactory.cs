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
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;

namespace CompleX.Classes
{
    public static class WizardParameterFactory
    {

        ///<summary>
        /// Gets the parameter object for the master data permission wizard.
        ///</summary>
        public static WizardParameters GetFirstStartSettingParameters()
        {
            WizardParameters result = GetDefaultWizardParams(Properties.Resources.Wizard);

            // Alle Settingseiten, die das Interface IWizardPageControl implementieren
            var settingPages = ApplicationHost.Host.GetServices<ISettingsPage>().OfType<IWizardPageControl>();
            GetInteriorParameters(settingPages);
            result.InteriorPagesParams.AddRange(GetInteriorParameters(settingPages));

            return result;
        }

        /// <summary>
        /// Gets the interior parameters.
        /// </summary>
        public static IEnumerable<WizardInteriorPageParameters> GetInteriorParameters<T>(IEnumerable<T> controls) where T : IWizardPageControl
        {
            var result = new List<WizardInteriorPageParameters>();
            foreach (IWizardPageControl control in controls)
            {
                var tmpControl = new WizardInteriorPageParameters
                                     {
                                         TitleText = ((UserControl)control).Text,
                                         SubTitleText = String.Empty,
                                         PageType = control.GetType(),
                                         Data = control.GetData()
                                     };

                if(control is ISettingsPage)
                {
                    tmpControl.TitleText = ((ISettingsPage) control).PageTitle;
                }
                result.Add(tmpControl);
            }
            return result;
        }


        ///<summary>
        /// Insert Tag Wizzard Params
        ///</summary>
        public static WizardParameters GetInsertTagParameters()
        {
            var result = GetDefaultWizardParams(Properties.Resources.InsertTag);

            result.InteriorPagesParams.Add(
                new WizardInteriorPageParameters
                {
                    TitleText = Properties.Resources.SelectTag,
                    SubTitleText = String.Empty,
                    PageType = typeof(TagSelector),
                    Data = null
                }
            );

            result.InteriorPagesParams.Add( 
                new WizardInteriorPageParameters
                {
                    TitleText = Properties.Resources.ConfigureTag,
                    SubTitleText = Properties.Resources.ConfigureTagDesc,
                    PageType = typeof(TagEditControl),
                    Data = null
                }
            );

            return result;
        }


        /// <summary>
        /// Gets the default empty wizard params.
        /// </summary>
        public static WizardParameters GetDefaultWizardParams(string caption)
        {
            return new WizardParameters
            {
                DialogCaption = caption,
                CompletionPageParams = new WizardCompletionPageParameters
                {
                    TitleText = Properties.Resources.WizardFinish,
                    FinishText = Properties.Resources.WizardFinishDetail,
                    ProceedText = Properties.Resources.WizardClickToFinish,
                }
            };
        }



    }
}
