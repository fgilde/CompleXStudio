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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Presentation.Controls.WPFDialogs;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using Microsoft.WindowsAPICodePack.Dialogs;
using TaskDialog=Microsoft.WindowsAPICodePack.Dialogs.TaskDialog;

namespace CompleX
{
    static class Program
    {
        private static Mutex mutex;
        private static int percentage;
        private static ErrorManager errorManager;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(new ComplexOutPutWriter()));

            if (Settings.Default.Language != "auto")
                LanguageService.SetCulture(new CultureInfo(Settings.Default.Language));
            
            errorManager = new ErrorManager();
            Application.EnableVisualStyles();
            UserLookAndFeel.Default.SkinName = Settings.Default.Theme;


            Application.SetCompatibleTextRenderingDefault(false);
            string infoText = Const.State;
            if (!String.IsNullOrEmpty(Const.CodeName))
                infoText += "  (" + Const.CodeName + ")";

            DialogContext<SplashDialog>.Current.Description =
                new SplashDialogDescription(Const.ApplicationName,
                                            Assembly.GetExecutingAssembly().GetName().Version.ToString()) { Info = infoText};

            DialogContext<SplashDialog>.Current.ShowDialog(false);

            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
                                       {
                                           DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = 5;
                                       } );


            #region Initializations
            
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();

            // Bei XP Form Skining sonst Aero
            if (Environment.OSVersion.Version.Major < 6) 
                SkinManager.EnableFormSkins();
            else
            {
                if (Settings.Get<bool>("EnableFormSkin"))
                    SkinManager.EnableFormSkins();
            }

            percentage = 10;

            // Load Services and Plugins

            #region PLUGIN LOADING 

           // MessageBox.Show(args[0].ToString());
            if (!args.Contains("-noplugin"))
            {
                
                DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
                {
                    DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = percentage;
                    DialogContext<SplashDialog>.Current.DialogInstance.StatusText = "Loading Plugins";
                });

                ApplicationHost.Host.AddingService += Host_AddingService;
                ApplicationHost.LoadAllPlugins();


            }
            #endregion
 

            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
            {
                DialogContext<SplashDialog>.Current.DialogInstance.StatusText = "Initializing ";
                DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = 70;
            });

            ThreadPool.QueueUserWorkItem(FillSplashDialogAsync);
            #endregion

            // Finished Loading Initializations (Close Splash etc) 
            ApplicationHost.Host.AddingService -= Host_AddingService;
           
            // Show MainForm
            //Application.Run(new MainForm(args));
            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
            {
                DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = 75;
            });

            bool canStart = true;
            int pluginCount = ApplicationHost.Host.GetServices<IHostedService>().Count();
            if (pluginCount != Settings.Default.LastPluginCount || !WindowsSecurityHelper.IsVistaOrHigher || WindowsSecurityHelper.IsAdmin)
            {
                // Neue Plugins geladen, oder entfernt
                canStart = SetVariables(pluginCount);
            }

            if (canStart) // CanStart is false, if the user wants to start with admin rights again
            {
                // Start Main Application
                Application.Run(new ComplexMainForm(args));
            }else
            {
                DialogContext<SplashDialog>.Current.CloseImmediatly();
            }

        }

        static void Host_AddingService(object sender, IHostedService service)
        {
            if(percentage <= 70)
                percentage += 5;
            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
            {
                DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = percentage;
                DialogContext<SplashDialog>.Current.DialogInstance.StatusText = "Loading: " +  service.ServiceName;
            });

        }

        private static void FillSplashDialogAsync(object state)
        {
            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() =>
            {
                while (DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue < 99)
                {
                    DialogContext<SplashDialog>.
                        Current.DialogInstance.
                        ProgressValue++;
                    //Application.DoEvents();
                }
                DialogContext<SplashDialog>.Current.DialogInstance.IsIndeterminate = true;
            });
        }


        /// <summary>
        /// Sets Global Variables and registry entries (only requered at first startup or after update).
        /// </summary>
        static bool SetVariables(int pluginCount)
        {
            if (WindowsSecurityHelper.IsVistaOrHigher && !WindowsSecurityHelper.IsAdmin)
            {
                bool result = !TryAdminRestart(pluginCount);
                return result;
            }

            try
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                Environment.SetEnvironmentVariable(Const.ShortName, Application.ExecutablePath,EnvironmentVariableTarget.Machine);
                
                // Setting file extensions for CompleX Specific files (settings, projects etc) 
                foreach (KeyValuePair<string, string> pair in Const.CompleXFileExtensions)
                {
                    KeyValuePair<string, string> valuePair = pair;
                    ThreadPool.QueueUserWorkItem(state => FileService.CreateComplexFileAssociation(valuePair.Key, valuePair.Value, true));
                }
                // Register shell menu (right mouse button in explorer) for every supported file with text "Editg with CompleX"
                foreach (var enumerable in FileService.GetPossibleFileDescription())
                {
                    string menuCommand = string.Format("\"{0}\" \"%L\"",
                                                       Application.ExecutablePath);
                    FileShellExtension.Register(enumerable.ExtensionName, Const.ApplicationName,
                                                                       String.Format(Resources.EditWith, Assembly.GetExecutingAssembly().GetName().Name + " " + version.Major+"."+version.Minor),
                                                                       menuCommand);
                }
                Settings.Default.LastPluginCount = pluginCount;
                Settings.SaveSettings();
            }
            catch (Exception e)
            {
               CompleX_Studio.MessageLog.LogException(e);
               return true;
            }
            return true;
        }

        private static bool TryAdminRestart(int pluginCount)
        {
            if (!TaskDialog.IsPlatformSupported)
                return false;
            bool result = false;
            // Show a dialog with elevation button
            var tdElevation = new TaskDialog
                                  {
                                      Cancelable = true,
                                      InstructionText = Resources.AdminRightsFirstStart
                                  };
            if (Settings.Default.LastPluginCount != -1) // Anwendung wurde nicht zu erstenmal gestartet, sondern neue plugins installiert oder entfernt
            {
                tdElevation.InstructionText = Resources.AdminRightsNewPluginsInstalled;
            }

            var adminTaskButton = new TaskDialogCommandLink("adminTaskButton", Resources.AdminRightsStartUp, Resources.AdminRightsStartUpDescription)
                                      {ShowElevationIcon = true};
            adminTaskButton.Click += ((sender,args) =>
                                          {
                                              WindowsSecurityHelper.RestartElevated();
                                              result = true;
                                          });


            adminTaskButton.Default = true;
            var noThankYouBtn = new TaskDialogCommandLink("noThankYouButton", Resources.NoThankYou);
            noThankYouBtn.Click += ((sender, args) => { result = false; tdElevation.Close(); });
            tdElevation.FooterCheckBoxChecked = true;
            tdElevation.FooterCheckBoxText = Resources.DontAskMeAgian;
            

            tdElevation.Controls.Add(adminTaskButton);
            tdElevation.Controls.Add(noThankYouBtn);

            tdElevation.Show();
            if (tdElevation.FooterCheckBoxChecked == true)
            {
                Settings.Default.LastPluginCount = pluginCount;
                Settings.SaveSettings();
            }
            return result;
        }


        public static bool IsApplicationStarted()
        {
            string mutexName = Application.ProductName;
            mutex = new Mutex(false, mutexName);
            if (mutex.WaitOne(0, true))
                return false;
            return true;
        } 


    }
}
