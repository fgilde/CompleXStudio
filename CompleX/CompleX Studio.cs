//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Helper;
using CompleX.Presentation.Controls.Extensions;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_Types;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraNavBar;

namespace CompleX
{

    public enum SwitchedEditMode
    {
        Code,
        Split,
        Designer,
    }

    public static class CompleX_Studio
    {
        private static readonly object SyncObject = new object();
        private static MessageLog messageLog;
        /// <summary>
        /// Gets the MainForm instance.
        /// </summary>
        /// <value>The instance.</value>
        internal static ComplexMainForm Instance
        {
            get
            {
                lock (SyncObject)
                {
                    return ApplicationHost.Host.GetService<ComplexMainForm>();
                }
            }
        }

        public static ToolBox ToolBox
        {
            get
            {
                return ApplicationHost.Host.GetService<ToolBox>();                 
            }
        }

        /// <summary>
        /// returns the version.
        /// </summary>
        public static Version Version
        {
            get
            {
                return Instance.GetVersion();
            }
        }

        /// <summary>
        /// Gets the message log.
        /// </summary>
        /// <value>The message log.</value>
        public static MessageLog MessageLog
        {
            get
            {
                lock (SyncObject)
                {
                    if (messageLog == null)
                    {
                        messageLog = new MessageLog();
                        if (Settings.Get<bool>(Const.SettingSaveMessageLog))
                            messageLog.LoadLog();
                        messageLog.AddEntry += MessageLogOnAddEntry;
                    }
                    return messageLog;
                }
            }
            set
            {
                lock (SyncObject)
                {
                    messageLog = value;
                }
            }
        }


        /// <summary>
        /// Makes a form to a CompleX toolwindow
        /// </summary>
        public static void FormToToolWindow(Form frm)
        {
            Instance.CheckInvoke(() => Instance.FormToToolWindow(frm));
        }

        private static void MessageLogOnAddEntry(object sender, LogEntry entry)
        {
            if (Instance != null && Instance.dockPanelMessageLog != null && Settings.Get(Const.SettingShowLogToolWindow,true))
                Instance.CheckInvoke(() =>  Instance.GetTopDockPanel(Instance.dockPanelMessageLog).Show());
        
            if(Settings.Get(Const.SettingShowLogAlert,true))
            {
                var alerter = new AlertControl {AutoFormDelay = 2500};
                alerter.AlertClick += (o, args) => CompleXException.ShowLogEntry(entry);
                alerter.Show(Instance, entry.LogType.ToString(), entry.Message, String.Empty, GetLogTypeImage(entry.LogType));
            }
            if (Settings.Get<bool>(Const.SettingSaveMessageLog))
                ThreadPool.QueueUserWorkItem(state => messageLog.SaveLog());
        }

        public static Image GetLogTypeImage(LogType type)
        {
            switch (type)
            {
                case LogType.Information:
                    return Properties.Resources.information;
                case LogType.Warning:
                    return Properties.Resources.Warning;
                case LogType.Error:
                    return Properties.Resources.error;
                case LogType.Exception:
                    return Properties.Resources.error;
                default:
                    return Properties.Resources.information;
            }
        }


        /// <summary>
        /// Shows the browser form.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="title">The title.</param>
        public static void ShowBrowserForm(string url, string title)
        {           
            var newForm = new BrowserDialog(url){Text = title};
            ShowForm(newForm);
        }


        public static void ShowForm(IBaseDialog frm)
        {
            if (frm is Form)
                ShowForm(frm as Form);
        }

        /// <summary>
        /// Shows a form as a CompleX MDI Form.
        /// </summary>
        public static void ShowForm(Form frm)
        {
                Instance.CheckInvoke(() =>
                                         {
                                             frm.MdiParent = Instance;
                                             frm.Show();
                                         });
        }

        /// <summary>
        /// Gets the mode of the active edit.
        /// </summary>
        /// <value>The type of the active edit.</value>
        public static EditMode ActiveEditMode
        {
            get
            {
               return Instance.CheckInvoke(() =>
                                         {
                                             if (Instance.ActiveMdiChild is MainEditForm)
                                             {
                                                 var frm = (MainEditForm)Instance.ActiveMdiChild;
                                                 return frm.ActiveEditMode;
                                             }
                                             return EditMode.Unknown;
                                         });

            }
        }

        /// <summary>
        /// Gets the current editor.
        /// </summary>
        /// <value>The current editor.</value>
        public static IContentEdit CurrentContentEditor
        {
            get
            {
                return Instance.CheckInvoke(() => Instance.CurrentContentEditor);
            }
        }

        /// <summary>
        /// Gets the current editor.
        /// </summary>
        /// <value>The current editor.</value>
        public static ISourceEdit CurrentEditor
        {
            get
            {
                return Instance.CheckInvoke(() => Instance.CurrentEditor);
            }
        }

        /// <summary>
        /// Gets the current designer.
        /// </summary>
        /// <value>The current designer.</value>
        public static IDesignable CurrentDesigner
        {
            get
            {
                return Instance.CheckInvoke(() => Instance.CurrentDesigner);
            }
        }

        /// <summary>
        /// Gets the current open filename.
        /// </summary>
        /// <value>The current file.</value>
        public static string CurrentFile
        {
            get
            {
                if (Instance?.ActiveMainEditForm != null)
                    return Instance.ActiveMainEditForm.FileName;
                return String.Empty;
            }
        }


        /// <summary>
        /// Switch current Editor to designmode
        /// </summary>
        public static void SwitchToDesign()
        {
            Instance.CheckInvoke(() =>
            {
                if(Instance.ActiveMainEditForm != null)
                   Instance.ActiveMainEditForm.SwitchToDesign();
            });  
        }

        /// <summary>
        /// Switch current Editor to codemode
        /// </summary>
        public static void SwitchToCode()
        {
            Instance.CheckInvoke(() =>
            {
                if (Instance.ActiveMainEditForm != null)
                    Instance.ActiveMainEditForm.SwitchToCode();
            });
        }
        /// <summary>
        /// Switch current Editor to splitmode
        /// </summary>
        public static void SwitchToSplit()
        {
            Instance.CheckInvoke(() =>
            {
                if (Instance.ActiveMainEditForm != null)
                    Instance.ActiveMainEditForm.SwitchToSplit();
            });
        }

        /// <summary>
        /// Displays the Options  page
        /// </summary>
        public static void ShowOptions()
        {
            Instance.CheckInvoke(() => Instance.ShowOptions());  
        }

        /// <summary>
        /// Displays a specific Option Page
        /// </summary>
        /// <param name="page"></param>
        public static DialogResult ShowOptionPage(ISettingsPage page)
        {
            var dialog = BaseDialogHelper.CreateBaseDialog(page.Control);
            dialog.OnAccept = () => page.OnOk();
            dialog.OnCancel = page.OnCancel;
            dialog.IsValid = page.ValidatePage;
            page.OnActivated(true,false);
            return dialog.ShowDialog();
        }

        /// <summary>
        /// Inspects a object in the inspector.
        /// </summary>
        public static void InspectObject(object value)
        {
            InspectObject(value,null);
        }

        /// <summary>
        /// Inspects a object in the inspector.
        /// </summary>
        public static void InspectObject(object value,Action<object> onValueChanged)
        {
            Instance.InspectObject(value, onValueChanged);
        }

        /// <summary>
        /// Inserts a tool box entry from ToolBox to CurrentEditor.
        /// </summary>
        /// <param name="baritem">The baritem.</param>
        internal static void InsertToolBoxEntry(NavBarItem baritem)
        {
            if(CurrentContentEditor != null)
            {
                object toInsert;
                if (baritem.Tag is ToolBoxItem)
                {
                    var item = (ToolBoxItem) baritem.Tag;
                    toInsert = item.Insert;
                    if (item.InserterId != Guid.Empty)
                    {
                        var inserter = ApplicationHost.Host.GetServiceById<IInserter>(item.InserterId);
                        if (inserter != null)
                            toInsert = inserter.GetObjectToInsert(new InsertParameter(item.Insert));
                    }

                }
                else
                {
                    toInsert = !String.IsNullOrEmpty(baritem.Hint) ? baritem.Hint : baritem.Caption;
                }
                if (toInsert != null)
                {
                    if (toInsert is string)
                        toInsert = PlaceHolder.ReplacePlaceHolder((string)toInsert);
                       
                    CurrentContentEditor.Insert(toInsert);
                }
            }       
        }

        private static bool asElevated;
        public static void Restart(bool elevated)
        {
            if (FileService.CloseAllForms(true))
            {
                asElevated = elevated;
                var timer = new System.Timers.Timer(1500);
                timer.Start();
                timer.Elapsed += TimerFinished; 
            }

        }

        private static void TimerFinished(object sender, ElapsedEventArgs e)
        {
            if (!asElevated)
                Application.Restart();
            else
            {
                var timer = sender as System.Timers.Timer;
                if (timer != null) timer.Enabled = false;
                WindowsSecurityHelper.RestartElevated();
                Application.Exit();
            }
        }

        public static void Insert(IInserter inserter)
        {
            object insert = inserter.GetObjectToInsert(new InsertParameter(null));
            if (insert != null)
            {
                CurrentContentEditor.Insert(insert);
            }
        }



        public static void StartExternalTool(ExternalTool externalTool)
        {
            var startInfo = new ProcessStartInfo(externalTool.Command)
            {
                Arguments = PlaceHolder.ReplacePlaceHolder(externalTool.Argument)
            };
            if (Directory.Exists(externalTool.InitialDirectory))
                startInfo.WorkingDirectory = externalTool.InitialDirectory;

            var p = new Process();
            // Process p = Process.Start(startInfo);

            if (Instance.ActiveMainEditForm != null && externalTool.ReloadFileAfterClose && File.Exists(Instance.ActiveMainEditForm.FileName))
            {
                MainEditForm editForm = Instance.ActiveMainEditForm;
                p.EnableRaisingEvents = true;
                p.Exited += ((sender, args) => Instance.CheckInvoke(() =>
                {
                    editForm.EditControl.LoadFromFile(editForm.FileName);
                    editForm.IsSaved = true;
                    editForm.UpdateTexts();
                }));
            }
            p.StartInfo = startInfo;
            p.Start();
            if (externalTool.ShowModal)
                p.WaitForExit();

        }

    }
}
