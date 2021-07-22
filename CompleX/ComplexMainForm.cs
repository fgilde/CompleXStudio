//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================

#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Helper;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Presentation.Controls.WPFDialogs;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_Types;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraVerticalGrid.Rows;
using Cursor = System.Windows.Forms.Cursor;
using Cursors = System.Windows.Forms.Cursors;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

#endregion

namespace CompleX
{
    /// <summary>
    /// ComplexMainForm
    /// </summary>
    internal partial class ComplexMainForm : XtraForm, IHostedService
    {

        private readonly Dictionary<DockPanel, BarButtonItem> dockPanelList;
        private Cursor currentCursor;
        private bool firstOutput = true;
        public List<string> RecentFiles = new List<string>();
        private readonly List<Bar> hiddenToolBars = new List<Bar>();
        private Action<object> propertyGridValueChanged;

        #region Interface IHostedService


        /// <summary>
        /// Guid
        /// </summary>
        /// <value></value>
        public Guid ID
        {
            get { return Guid.NewGuid(); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        /// <value></value>
        public string ServiceName
        {
            get { return Text; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        /// <value></value>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { "*.*" }; }
        }

        /// <summary>
        /// Function call if service is added
        /// </summary>
        /// <returns></returns>
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

        internal bool TabbedMdiActive
        {
            get
            {
                return biTabbedMDI.Down;
            }
            set
            {
                biTabbedMDI.Down = value;
                InitTabbedMdi();
            }
        }

        /// <summary>
        /// Updates the executers.
        /// </summary>
        internal void UpdateExecuters()
        {
            ComboBoxExecuters.Items.Clear();
            if (CurrentContentEditor == null && !projectExplorer.IsProjectOpen)
                return;

            var executables = new List<IExecutable>();
            // Wenn ein Projekt offen ist, und eine Startupfile hat, und die Aktuell offene datei zu diesem Projekt gehört, dann auch nach executern für die Startup datei gucken
            if (projectExplorer.IsProjectOpen && !String.IsNullOrEmpty(projectExplorer.StartUpFileName) && ActiveMainEditForm != null && !String.IsNullOrEmpty(ActiveMainEditForm.FileName) && projectExplorer.ProjectFiles.Contains(ActiveMainEditForm.FileName))
                executables = ApplicationHost.GetModulesByFileName<IExecutable>(projectExplorer.StartUpFileName).ToList();

            if (ActiveMainEditForm != null)
                executables.AddRange(ApplicationHost.GetModulesByFileName<IExecutable>(ActiveMainEditForm.FileName).Except(executables));

            foreach (IExecutable executer in executables)
            {
                if (executer.ExcecutionModes == null || executer.ExcecutionModes.Count <= 0)
                {
                    ComboBoxExecuters.Items.Add(
                        new ImageComboBoxItem(executer.ServiceName, new CodeExecuter(executer), -1)
                    );
                }
                else
                {
                    foreach (KeyValuePair<int, string> excecutionMode in executer.ExcecutionModes)
                    {
                        ComboBoxExecuters.Items.Add(
                            new ImageComboBoxItem(excecutionMode.Value, new CodeExecuter(executer), excecutionMode.Key)
                        );
                    }
                }
            }
            if (ComboBoxExecuters.Items.Count > 0)
                eExecuter.EditValue = ComboBoxExecuters.Items[0].Value;
        }

        internal void UpdateOpenFileList()
        {
            ThreadPool.QueueUserWorkItem(state => openFilesControl.UpdateOpenFiles());
        }



        internal void CreateNewFileFromTemplate(string templateFileName, string newFileName)
        {
            if (File.Exists(templateFileName))
            {
                CreateContentEditor(templateFileName, false, true);
                ActiveMainEditForm.FileName = newFileName;
            }
            else
            {
                CreateContentEditor(newFileName, false, false);
            }
        }

        internal void CreateNewFileFromTemplate(string templateFileName)
        {
            if (File.Exists(templateFileName))
            {
                CreateNewFileFromTemplate(templateFileName, Path.GetFileName(templateFileName));
            }
        }


        internal void CreateContentEditor(string fileName, bool askForEditor)
        {
            CreateContentEditor(fileName, true, askForEditor);
        }

        internal void CreateContentEditor(string fileName)
        {
            CreateContentEditor(fileName, true, true);
        }

        /// <summary>
        /// Creates the content and load the fileName if it exists editor.
        /// </summary>
        internal void CreateContentEditor(string fileName, bool addToRecentList, bool askForEditor)
        {
            if (fileName.EndsWith(Path.DirectorySeparatorChar.ToString()))
                fileName = fileName.Remove(fileName.Length - 1, 1);

            // If project then load project
            if ((Path.GetExtension(fileName).ToLower().Equals(".cspr") || Path.GetExtension(fileName).ToLower().Equals(".cslpr")) && File.Exists(fileName))
            {
                projectExplorer.LoadProject(fileName);
                GetTopDockPanel(dockPanelSolutionExplorer).Show();
                return;
            }

            this.CheckInvoke(() =>
            {
                if (FileService.ActivateFile(fileName)) // Wenn die Datei schon geöffnet ist, nur auswählen und nicht neu öffnen
                    return;

                ISourceEdit editControl = default(ISourceEdit);
                var possibleSourceEditors = ApplicationHost.GetModulesByFileName<ISourceEdit>(fileName);
                if (possibleSourceEditors.Count() <= 0)
                {
                    // Kein möglicher editor vorhanden
                    editControl = new DefaultEditControl();
                }
                else
                {
                    if (possibleSourceEditors.Count() > 1)
                    {
                        // Wenn nach einem editor gefragt werden soll, gucken ob der user die letzte antwort gespeichert hat
                        var editId = Settings.Get<Guid>(MessageService.FillDsaKey("DEFAULTEDIT_FOR_" + Path.GetExtension(fileName.ToLower())));
                        if (editId != Guid.Empty)
                        {
                            var savedEdit = ApplicationHost.GetModulesByFileName<ISourceEdit>(fileName).FirstOrDefault(
                                edit => edit.ID.Equals(editId));
                            if (savedEdit != null)
                            {
                                editControl = Activator.CreateInstance(savedEdit.GetType()) as ISourceEdit;
                                askForEditor = false;
                            }
                        }
                    }
                    if (possibleSourceEditors.Count() > 1 && askForEditor && editControl == null)
                    {
                        var ctrl = new SelectPluginControl<ISourceEdit>(possibleSourceEditors);
                        var dlg = BaseDialogHelper.CreateBaseDialog(ctrl);
                        dlg.Text = Resources.PleaseChooseEditor;
                        dlg.IsValid = () => ctrl.SelectedService == null ? new ValidationResult(false, Resources.PleaseSelectEditor) : new ValidationResult(true);
                        dlg.CancelBtn.Visible = false;
                        dlg.CheckBox.Visible = true;
                        dlg.CheckBox.Text = Resources.DontAskMeAgian;
                        dlg.OkBtn.Left = dlg.CancelBtn.Left;
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            editControl = Activator.CreateInstance(ctrl.SelectedService.GetType()) as ISourceEdit;
                            if (dlg.CheckBox.Checked && editControl != null) // User will nicht mehr gefragt werden, sondern immer diesen editor für diese datei art wählen
                                Settings.Set(MessageService.FillDsaKey("DEFAULTEDIT_FOR_" + Path.GetExtension(fileName.ToLower())), editControl.ID);

                        }
                        else
                        {
                            // Es waren meherer editoren verfügbar, doch der User wollte patou keinen auswählen, daher abbrechen
                            return;
                        }
                    }
                    else if (editControl == null)
                    {
                        editControl = Activator.CreateInstance(possibleSourceEditors.ToList()[0].GetType()) as ISourceEdit;
                    }
                }
                try
                {

                    var newForm = new MainEditForm(editControl, fileName) { MdiParent = this, PossibleSourceEditors = possibleSourceEditors };
                    newForm.SelectionChanged += (EditorSelectionChanged);
                    newForm.ContentChanged += CurrentContentChanged;
                    newForm.ChangeMode += (ActiveMainEditFormModeChanged);
                    newForm.ActiveEditorFocusChange += (ActiveEditorFocusChanged);

                    if (newForm.EditControl != null && !newForm.EditControl.ContextMenuIsHandled)
                    {
                        barManager.SetPopupContextMenu((UserControl)newForm.EditControl, defaultEditPopupMenu);
                    }
                    newForm.Show();
                    if (Settings.Get("ShowFileIconOnTabPage", true))
                        DevExpressHelper.SetFormIconOnTabPage(xtraTabbedMdiManager.SelectedPage);

                }
                catch (Exception e)
                {
                    CompleX_Studio.MessageLog.LogException(e);
                    var ctrl = new EditExceptionControl(e, true);
                    var newForm = new MainEditForm(ctrl, fileName) { MdiParent = this };
                    newForm.Show();
                    throw;
                }
            });

            if (addToRecentList)
                UpdateRecentList(fileName);

            // Wenn der MDI Manager nicht aktiviert ist, dann selber liste aktualisieren
            if (!TabbedMdiActive)
                UpdateOpenFileList();
        }


        private void CurrentContentChanged(object sender, EventArgs args)
        {
            EnableControls();
        }


        /// <summary>
        /// Updates the plug in visibility.
        /// </summary>
        internal void UpdatePlugInVisibility()
        {
            foreach (KeyValuePair<DockPanel, BarButtonItem> pair in dockPanelList)
            {
                DockPanel toolWindow = pair.Key;
                BarButtonItem item = pair.Value;
                var supportedFiles = (string[])toolWindow.Tag;
                if (supportedFiles == null || supportedFiles.Count() <= 0 ||
                   (supportedFiles.Count() == 1 && supportedFiles[0].IsForAll()))
                {
                    EnableToolWindowPlugin(toolWindow, item, true);
                }
                else
                {
                    if (CurrentContentEditor != null)
                    {
                        string ext = Path.GetExtension(ActiveMainEditForm.FileName).ToLower();
                        bool enabled = supportedFiles.ToLower().Contains(ext);
                        EnableToolWindowPlugin(toolWindow, item, enabled);
                    }
                    else
                    {
                        EnableToolWindowPlugin(toolWindow, item, false);
                    }
                }
            }
        }


        /// <summary>
        /// Executes the specified code executer.
        /// </summary>
        /// <param name="codeExecuter">The code executer.</param>
        /// <param name="executionMode">The execution mode.</param>
        /// <returns></returns>
        internal bool Execute(CodeExecuter codeExecuter, int executionMode)
        {
            textBoxOutPut.Text = String.Empty;
            string file = String.Empty;
            if (projectExplorer.IsProjectOpen && !String.IsNullOrEmpty(projectExplorer.StartUpFileName))
                file = projectExplorer.StartUpFileName;
            else if (ActiveMainEditForm != null)
                file = ActiveMainEditForm.FileName;
            bool result = codeExecuter.Executer.Execute(executionMode, file, CurrentContentEditor, projectExplorer.ProjectFiles);
            foreach (LogEntry entry in codeExecuter.Executer.ErrorList)
            {
                CompleX_Studio.MessageLog.LogEntry(entry);
            }
            return result;
        }

        /// <summary>
        /// Displays the OptionsDialog
        /// </summary>
        internal void ShowOptions()
        {
            var control = new SettingsHostControl();

            IEnumerable<ISettingsPage> notSaved = Enumerable.Empty<ISettingsPage>();
            var optionsDialog = BaseDialogHelper.CreateBaseDialog(control);
            optionsDialog.OnAccept = () => control.TryApplyAllChanges(out notSaved);
            optionsDialog.OnCancel = control.CancelAllChanges;
            optionsDialog.IsValid = control.ValidateActivePage;

            if (optionsDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (notSaved != null && notSaved.Count() > 0)
                {
                    var sb = new StringBuilder();
                    foreach (ISettingsPage page in notSaved)
                    {
                        page.OnCancel();
                        sb.AppendLine(Resources.Option + ": " + page.PageTitle);
                        sb.AppendLine(Resources.ErrorMessage + ": " + page.ValidatePage().ErrorMessage);
                        sb.AppendLine(new string('-', 79));
                    }

                    CompleXException.ShowException(new Exception(Resources.WariningOptionPages), true, sb.ToString());
                }
                Settings.SaveSettings();
            }
            UpdateFtpConnectionsCombo();
        }

        /// <summary>
        /// Inspects a object in ObjectInspector.
        /// </summary>
        internal void InspectObject(object value)
        {
            InspectObject(value, null);
        }
        /// <summary>
        /// Inspects a object in ObjectInspector.
        /// </summary>
        internal void InspectObject(object value, Action<object> onValueChanged)
        {
            propertyGridValueChanged = null;
            propertyGrid.CheckInvoke(() =>
            {
                propertyGrid.Tag = 0;
                propertyGrid.PropertyGrid.SelectedObject = value;
            });
            propertyGridValueChanged = onValueChanged;
        }

        /// <summary>
        /// Executes a dos command and loads the result as a file.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="dir">The dir.</param>
        /// <param name="showConsole">if set to <c>true</c> [show console].</param>
        internal void ExecuteDosCommandAndLoadResultAsFile(string command, string dir, bool showConsole)
        {
            Settings.Set(Const.SettingShowDosConsole, showConsole);
            Settings.Set(Const.SettingLastDosCommand, command);
            Settings.Set(Const.SettingLastDosDir, dir);

            string result, error;
            ShellControl.GetCommandResult(command, dir, showConsole, out result, out error);
            string cmd = Path.GetFileNameWithoutExtension(command);
            string fileName = Path.GetTempPath().AddDirectorySeparatorChar() + "result_" + cmd + " " + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".txt";
            var writer = new StreamWriter(new FileStream(fileName, FileMode.Create));
            writer.Write(result);
            writer.Write(error);
            writer.Close();
            CreateContentEditor(fileName);
        }

        /// <summary>
        /// Adds a message to complex output window
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="newLine"></param>
        internal void AddOutput(string message, bool newLine)
        {
            if (firstOutput)
                GetTopDockPanel(dockPanelOutPut).Show();
            firstOutput = false;
            if (newLine)
                textBoxOutPut.Text += Environment.NewLine;
            textBoxOutPut.Text += message;
            textBoxOutPut.SelectionStart = textBoxOutPut.Text.Length;
            textBoxOutPut.ScrollToCaret();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexMainForm"/> class. Ctor
        /// </summary>
        /// <param name="args">The param arguments.</param>
        internal ComplexMainForm(IEnumerable<string> args)
        {
            InitializeComponent();
            barButtonShowFileSystemMenu.ShortcutKeyDisplayString = Resources.CtrlRight;
            barButtonShowFileSystemMenu.Caption = Resources.ShellContextMenu;
            UserLookAndFeel.Default.SkinName = Settings.Default.Theme;
            shellControl.Prompt = Path.GetDirectoryName(Application.ExecutablePath) + ">";
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            SetCaptionText();

            ClipboardController.Current.AddForm(this);

            dockPanelList = new Dictionary<DockPanel, BarButtonItem>();
            propertyGrid.PropertyGrid.AutoGenerateRows = true;
            propertyGrid.PropertyGrid.CellValueChanged += PropertyGridCellValueChanged;
            ApplicationHost.Host.AddService(this);

            if (args.Count() <= 0)
                ShowWelcomePage();

            foreach (string s in args)
            {
                if (File.Exists(s))
                    CreateContentEditor(s);
            }

        }

        private void SetCaptionText()
        {
            Text = Assembly.GetExecutingAssembly().GetName().Name + Const.CodeName + @" " + GetVersion() + Const.State;
        }

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrmMainLoad(object sender, EventArgs e)
        {
            try
            {
                shellControl.ShellTextBackColor = Settings.Get("ShellTextBackColor", Color.Silver);
                shellControl.ShellTextForeColor = Settings.Get("ShellTextForeColor", Color.White);
                Init();
                BringToFront();
                WindowState = FormWindowState.Maximized;
                DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() => { DialogContext<SplashDialog>.Current.DialogInstance.Topmost = true; });
                ThreadPool.QueueUserWorkItem(CloseSplashDialogAsync);
                Activate();
                CheckFirstStartUp();
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message);
            }
        }

        private void CheckFirstStartUp()
        {
            try
            {
                if (!Settings.Get(Const.SettingsWasEverStarted, false) && (!WindowsSecurityHelper.IsVistaOrHigher || WindowsSecurityHelper.IsAdmin))
                {
                    if (MessageService.Ask(Resources.FirstStartWizard, Text))
                        WizardDialog.ShowFirstStartWizard();
                }
            }
            catch (Exception)
            {}
            Settings.Set(Const.SettingsWasEverStarted, true);
        }

        private void CloseSplashDialogAsync(object state)
        {
            DialogContext<SplashDialog>.Current.DialogInstance.CheckInvoke(() => DialogContext<SplashDialog>.Current.DialogInstance.ProgressValue = 100);
            Thread.Sleep(3500);
            DialogContext<SplashDialog>.Current.CloseDialog();
            this.CheckInvoke(Activate);
        }

        void PropertyGridCellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (propertyGrid.Tag is int && (int)propertyGrid.Tag == 1 && CurrentContentEditor != null)
            {
                // Wenn CompleX den wert beim Selektierten gesetzt hat, dann bei änderungen übernehmen
                CurrentContentEditor.SelectedContent = propertyGrid.PropertyGrid.SelectedObject;
            }
            else
            {
                // Falls der aufrufer eine Action übergeben hatte, dieser Aufrufen
                if (propertyGridValueChanged != null)
                    propertyGridValueChanged(propertyGrid.PropertyGrid.SelectedObject);
            }
        }

        /// <summary>
        /// Gets the current content editor.
        /// </summary>
        internal IContentEdit CurrentContentEditor
        {
            get
            {
                if (ActiveMdiChild != null && ActiveMdiChild is MainEditForm)
                {
                    if (CompleX_Studio.ActiveEditMode == EditMode.Designer)
                        return CurrentDesigner;
                    return CurrentEditor;
                }
                return null;
            }
        }

        /// <summary>
        /// ActiveMdiChild as MainEditForm
        /// </summary>
        internal MainEditForm ActiveMainEditForm
        {
            get
            {
                return this.CheckInvoke(() =>
                {
                    if (ActiveMdiChild != null && ActiveMdiChild is MainEditForm)
                    {
                        return (MainEditForm)ActiveMdiChild;
                    }
                    return null;
                });
            }
        }

        /// <summary>
        /// Gets the current Sourceeditor.
        /// </summary>
        internal ISourceEdit CurrentEditor
        {
            get
            {
                if (ActiveMdiChild != null && ActiveMdiChild is MainEditForm)
                {
                    return ((MainEditForm)ActiveMdiChild).EditControl;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the current designer.
        /// </summary>
        internal IDesignable CurrentDesigner
        {
            get
            {
                if (ActiveMdiChild != null && ActiveMdiChild is MainEditForm)
                {
                    return ((MainEditForm)ActiveMdiChild).Designer;
                }
                return null;
            }
        }

        internal void LoadFiles(IList<string> fileList)
        {
            using (new WaitingCursor(this))
            {
                if (fileList.Count > 3) // Wenn mehr als 5 Dateien, dann in einem ExtraThread mit wartedialog
                {
                    var workerThread = new WorkerThread { HeaderText = Resources.PleaseWait, IsCancelable = true, IsIndeterminate = false };
                    workerThread.RunWorkerAsync(args =>
                    {
                        args.Worker.SetMaximum(fileList.Count());
                        for (int i = 0; i < fileList.Count(); i++)
                        {
                            args.Worker.MainText = Resources.OpeningFiles + " " +
                                                   args.Worker.Percentage + "%";
                            args.Worker.IncrementStep(fileList[i]);
                            CreateContentEditor(fileList[i], false);
                            if (args.Cancel)
                                return;
                        }
                    }, null);
                }
                else // Wenn weniger als 5 Dateien, dann einfach öffnen
                {
                    foreach (string s in fileList)
                        CreateContentEditor(s, fileList.Count == 1); // bei mehreren datein nicht nachfragen welcher editor genommen werden soll
                }
            }
        }


        internal void FormToToolWindow(Form frm)
        {
            var newDockPanel = dockManager.AddPanel(new Point(0, 0));
            newDockPanel.Text = frm.Text;
            if (!frm.Visible)
                frm.Show();
            try
            {
                if (frm.MdiParent != null)
                    frm.MdiParent = this;
                newDockPanel.Controls.Add(frm);
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.ShowInTaskbar = false;
                frm.ShowIcon = false;
                frm.TextChanged += (sender, args) => newDockPanel.Text = frm.Text;
                frm.Dock = DockStyle.Fill;
            }
            catch (Exception)
            {
                newDockPanel.Close();
                dockManager.RemovePanel(newDockPanel);
                throw;
            }
        }

        /// <summary>
        /// Executes the file open dialog.
        /// </summary>
        internal void ExecuteFileOpenDialog()
        {
            string dir = String.Empty;
            if (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile))
                dir = Path.GetDirectoryName(CompleX_Studio.CurrentFile);
            var dl = FileService.GetOpenFileDialog(dir);
            dl.Multiselect = true;

            if (dl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFiles(dl.FileNames);
            }
        }

        /// <summary>
        /// Gets the dock panel by id.
        /// </summary>
        /// <param name="id">The id.</param>
        private DockPanel GetDockPanelById(Guid id)
        {
            return dockManager.Panels.Cast<DockPanel>().FirstOrDefault(panel => panel.ID.Equals(id));
        }

        private void Init()
        {
            InitQuickTemplates();
            Task.Factory.StartNew(LoadRecentList);
            fileExplorer.Init();
            if (!String.IsNullOrEmpty(Settings.Default.LastFileExplorerPath))
                Task.Factory.StartNew(() => fileExplorer.CheckInvoke(() => fileExplorer.SelectPath(Settings.Default.LastFileExplorerPath)));

            InitTabbedMdi();
            barManager.ForceInitialize();

            LoadLayouts();
            UpdateFtpConnectionsCombo();

#if !DEBUG
                LoadDockLayout(Settings.Default.LastDock,true);
#endif

            UpdateViewMenu(true);
            MenuService.UpdateToolsMenu();
            MenuService.UpdateInsertMenu();
            messageLogControl.Initialize(CompleX_Studio.MessageLog);
            CompleX_Studio.MessageLog.AddEntry += (sender, entry) => messageLogControl.CheckInvoke(() => messageLogControl.Initialize(CompleX_Studio.MessageLog));

            textBoxFindResult.ContextMenu = textBoxOutPut.ContextMenu = new ContextMenu();

            ClassViewer.AddClassInfo(treeView1, GetType(), new object[] { this, new ProjectExplorer() });
        }

        private void InitQuickTemplates()
        {
            var files = Directory.GetFiles(Pathes.TemplatePath);
            int index = 0;
            foreach (var s in files)
            {
                var newFromTemplateItem = new BarButtonItem(barManager, Path.GetFileName(s))
                {
                    Id = barManager.GetNewItemId(),
                    ImageIndex = imageListMain.AddImage(ImageFunctions.GetFileIcon(s, false))
                };

                siNew.AddItem(newFromTemplateItem);
                siNew.ItemLinks[siNew.ItemLinks.Count - 1].BeginGroup = index == 0;
                string name = s;
                newFromTemplateItem.ItemClick += (sender, args) => CreateNewFileFromTemplate(name);
                index++;
            }

        }

        private void LoadRecentList()
        {
            try
            {
                if (File.Exists(Pathes.RecentFileList))
                {
                    if (SerializationHelper.TryXmlDeserialize(Pathes.RecentFileList, out RecentFiles))
                    {
                        RecentFiles = RecentFiles.Where(File.Exists).ToList();
                        recentFileListControl.SetFiles(RecentFiles);
                        var recentList = RecentFiles.Where(s => Path.GetExtension(s).ToLower() != ".cspr" && Path.GetExtension(s).ToLower() != ".cslpr").Top(Settings.Default.MaxRecentMenuCount).ToList();
                        // Die letzten dateien öffnen, wenn es so eingestellt ist)
                        if (Settings.Default.OpenLastRecentFiles)
                        {
                            LoadFiles(recentList);
                        }
                        // Dateien im Datei menü anzeigen
                        UpdateRecentListMenu();
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }

        internal void UpdateRecentList(string fileName)
        {
            if (File.Exists(fileName))
            {
                if (!RecentFiles.Contains(fileName))
                {
                    RecentFiles.Add(fileName);
                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        RecentFiles.TryXmlSerialize(Pathes.RecentFileList);
                        recentFileListControl.SetFiles(RecentFiles);
                        UpdateRecentListMenu();
                    });
                }
            }
        }

        private void UpdateRecentListMenu()
        {
            this.CheckInvoke(() =>
            {
                var recentFiles = RecentFiles.ToList();
                recentFiles.Reverse();
                var recentFileList = recentFiles.Where(s => Path.GetExtension(s).ToLower() != ".cspr" && Path.GetExtension(s).ToLower() != ".cslpr").Top(Settings.Default.MaxRecentMenuCount).ToList();
                var recentProjectList = recentFiles.Where(s => Path.GetExtension(s).ToLower() == ".cspr" || Path.GetExtension(s).ToLower() == ".cslpr").Top(Settings.Default.MaxRecentMenuCount).ToList();

                if (recentFileList.Count > 0)
                {
                    int index = 1;
                    iRecentFiles.ClearLinks();
                    foreach (string file in recentFileList)
                    {
                        var item = new BarButtonItem(barManager, file.ToShortPath())
                        {
                            Id = barManager.GetNewItemId(),
                            Name = "CS_RecentFile_" + file.GetHashCode(),
                        };

                        item.Caption = @"&" + index + @" " + item.Caption;
                        string name = file;
                        item.ItemClick += (sender, e) => CreateContentEditor(name);
                        item.ImageIndex = imageListMain.AddImage(ImageFunctions.GetFileIcon(file, false));
                        iRecentFiles.AddItem(item);
                        index++;
                    }
                    var clearitem = new BarButtonItem(barManager, Resources.Clear) { Id = barManager.GetNewItemId(), Name = "CS_ClearRecentItem", };
                    clearitem.ItemClick += (sender, e) => ClearRecentList(true);
                    clearitem.ImageIndex = 40;
                    iRecentFiles.AddItem(clearitem);
                    iRecentFiles.ItemLinks[iRecentFiles.ItemLinks.Count - 1].BeginGroup = true;
                    iRecentFiles.Visibility = BarItemVisibility.Always;

                }
                else
                {
                    iRecentFiles.Visibility = BarItemVisibility.Never;
                }

                // Recent projects
                if (recentProjectList.Count > 0)
                {
                    int index = 1;
                    iRecentProjects.ClearLinks();
                    foreach (string file in recentProjectList)
                    {
                        var item = new BarButtonItem(barManager, file.ToShortPath())
                        {
                            Id = barManager.GetNewItemId(),
                            Name = "CS_RecentProject_" + file.GetHashCode(),
                        };

                        item.Caption = @"&" + index + @" " + item.Caption;
                        string name = file;
                        item.ItemClick += (sender, e) => CreateContentEditor(name);
                        item.ImageIndex = imageListMain.AddImage(ImageFunctions.GetFileIcon(file, false));
                        iRecentProjects.AddItem(item);
                        index++;
                    }
                    var clearitem = new BarButtonItem(barManager, Resources.Clear) { Id = barManager.GetNewItemId(), Name = "CS_ClearRecentItem", };
                    clearitem.ItemClick += (sender, e) => ClearRecentList(true);
                    clearitem.ImageIndex = 40;
                    iRecentProjects.AddItem(clearitem);
                    iRecentProjects.ItemLinks[iRecentProjects.ItemLinks.Count - 1].BeginGroup = true;
                    iRecentProjects.Visibility = BarItemVisibility.Always;

                }
                else
                {
                    iRecentProjects.Visibility = BarItemVisibility.Never;
                }


            });

        }

        private void ClearRecentList(bool ask)
        {
            if (!ask || MessageService.AskDsa(Resources.ShowConfirmation, Resources.Clear, "ClearRecentFilesAndProjects"))
            {
                RecentFiles.Clear();
                iRecentFiles.ClearLinks();
                recentFileListControl.Clear();
                iRecentFiles.Visibility = BarItemVisibility.Never;
                RecentFiles.TryXmlSerialize(Pathes.RecentFileList);
            }
        }


        internal void UpdateDynamicMenuItems()
        {
            MenuService.UpdateEditMenu();
            MainMenuBar.UnMergeMenuStrip(Const.DefaultContentEditMergeKey);
            if (CurrentContentEditor != null && CurrentContentEditor is IMenuHandler)
            {
                var menuHanlder = (IMenuHandler)CurrentContentEditor;
                if (menuHanlder.Menu != null)
                {
                    MainMenuBar.Merge(menuHanlder.Menu, Const.DefaultContentEditMergeKey);
                    MenuService.MoveMenuItemLastPosition(iWindow);
                    MenuService.MoveMenuItemLastPosition(iHelp);
                    MenuService.MoveMenuItemLastPosition(CompleX_Studio.Instance.iFile.ItemLinks, CompleX_Studio.Instance.iExit);
                }
            }
            HideEmptyToolBars();
        }

        private void UpdateViewMenu(bool clearfirst)
        {

            dockPanelList.Clear();

            if (clearfirst)
                iView.ClearLinks();

            // Alle PlugIns die ToolWindows sind als DockPanel laden
            LoadToolWindows();

            // Alle vorhandenen Dockpanel dem Menü "Ansicht" hinzufügen
            bool groupstarted = false;
            int index = 0;
            foreach (DockPanel panel in dockManager.Panels)
            {
                DockPanel dockPanel = panel;
                if (!panel.Text.Contains("panelContainer"))
                {
                    var item = new BarButtonItem(barManager, panel.Text)
                    {
                        Name = "dockpanel_" + panel.Name,
                        ButtonStyle = BarButtonStyle.Check,
                        Id = barManager.GetNewItemId()
                    };

                    iView.AddItem(item);
                    item.Down = dockPanel.Visible;
                    item.Tag = dockPanel.Tag;
                    // Tag wird für die SupportedFileExtensions benutzt, um Menü und fenster ggf auszublenden
                    item.ItemClick += (sender, args) =>
                    {
                        dockPanel.Visible = !dockPanel.Visible;
                        item.Down = dockPanel.Visible;
                        if (dockPanel.Visible)
                        {
                            GetTopDockPanel(dockPanel).Show();
                        }
                    };
                    item.ImageIndex = dockPanel.ImageIndex;
                    index++;
                    if ((panel.Hint == Const.NewGroupTag && !groupstarted) || (index == 1))
                    {
                        iView.ItemLinks[iView.ItemLinks.Count - 1].BeginGroup = true;
                        groupstarted = index != 1;
                        if (groupstarted)
                            panel.Hint = String.Empty;
                    }
                    dockPanelList.Add(dockPanel, item);
                }
            }


            // Alle ToolBars dem Menü "Ansicht" hinzufügen
            var tbitem = new BarSubItem(barManager, Resources.Toolbars)
            {
                Id = barManager.GetNewItemId()
            };
            iView.AddItem(tbitem);
            iView.ItemLinks[iView.ItemLinks.Count - 1].BeginGroup = true;

            var toolbarsListItem = new BarToolbarsListItem();
            tbitem.AddItem(toolbarsListItem);

            #region show hide all toolbars

            var tbShowAllItem = new BarButtonItem(barManager, Resources.ShowAllToolBars)
            {
                Name = "dockpanel_ShowAll",
                Id = barManager.GetNewItemId(),
                ItemShortcut = new BarShortcut(Keys.Control | Keys.T, Keys.Control | Keys.S),
            };

            // Tag wird für die SupportedFileExtensions benutzt, um Menü und fenster ggf auszublenden
            tbShowAllItem.ItemClick += (sender, args) =>
            {
                foreach (Bar bar in barManager.Bars)
                    if (!bar.IsMainMenu)
                        bar.Visible = true;
            };

            tbitem.AddItem(tbShowAllItem);
            tbitem.ItemLinks[tbitem.ItemLinks.Count - 1].BeginGroup = true;
            var tbHideAllItem = new BarButtonItem(barManager, Resources.HideAllToolBars)
            {
                Name = "dockpanel_HideAll",
                Id = barManager.GetNewItemId(),
                ItemShortcut = new BarShortcut(Keys.Control | Keys.T, Keys.Control | Keys.H),

            };

            // Tag wird für die SupportedFileExtensions benutzt, um Menü und fenster ggf auszublenden
            tbHideAllItem.ItemClick += (sender, args) =>
            {
                foreach (Bar bar in barManager.Bars)
                    if (!bar.IsMainMenu)
                        bar.Visible = false;
            };
            tbitem.AddItem(tbHideAllItem);

            #endregion

            // BarButtonItem Customize hinzufügen
            var customizeItem = new BarButtonItem(barManager, Resources.Customize + "...")
            {
                Id = barManager.GetNewItemId()
            };
            iView.AddItem(customizeItem);
            iView.ItemLinks[iView.ItemLinks.Count - 1].BeginGroup = true;
            customizeItem.ItemClick += (sender, args) => barManager.Customize();

            // Einträge für Layout Speichern hizufügen
            iView.AddItem(iSaveLayout);
            iView.ItemLinks[iView.ItemLinks.Count - 1].BeginGroup = true;

            var layoutSubItem = new BarSubItem(barManager, Resources.LoadLayout)
            {
                Id = barManager.GetNewItemId()
            };
            iView.AddItem(layoutSubItem);

            layoutSubItem.AddItem(beiDockMode);
            iView.AddItem(iToggleFullscreen);
            iView.ItemLinks[iView.ItemLinks.Count - 1].BeginGroup = true;
            UpdatePlugInVisibility();
        }

        private void LoadToolWindows()
        {
            IEnumerable<IToolWindow> toolWindows = ApplicationHost.Host.GetServices<IToolWindow>();
            if (toolWindows.Count() > 0)
            {
                foreach (IToolWindow window in toolWindows)
                {
                    if (window is UserControl)
                    {
                        bool isNew = false;
                        // Prüfen ob bereits ein dockpanel für das Plugin vorhanden ist
                        // z.B nach dem Laden eines Layouts
                        DockPanel newDockPanel = GetDockPanelById(window.ID);
                        if (newDockPanel == null)
                        {
                            // Ansonsten neues Panel anlegen
                            newDockPanel = dockManager.AddPanel(new Point(0, 0));
                            isNew = true;
                        }

                        if (newDockPanel != null)
                        {
                            newDockPanel.Hint = Const.NewGroupTag;
                            // Falls das PlugIn in image besitzt, dieses zur ImageList hinzufügen und verwenden
                            if (window.Image != null)
                            {
                                newDockPanel.ImageIndex = imageListMain.AddImage(window.Image);

                            }
                            // ID zuweisen, damit das Panel nach einem Layout laden gefunden werden kann
                            newDockPanel.ID = window.ID;
                            newDockPanel.Tag = window.SupportedFileExtensions;
                            // Tag wird für die SupportedFileExtensions benutzt, um Menü und fenster ggf auszublenden
                            newDockPanel.Controls.Add((UserControl)window);
                            ((UserControl)window).Dock = DockStyle.Fill;
                            newDockPanel.Text = window.ServiceName;
                            if (isNew) // Wenn es neu angelegt wurde erstmal nicht anzeigen
                                newDockPanel.Visible = false;
                        }
                    }
                }
            }
        }


        private static void ShowWelcomePage()
        {
            CompleX_Studio.ShowBrowserForm("about:Welcome%20to%20CompleX%20Studio", Resources.Welcome);
        }

        private void LoadLayouts()
        {
            // Alle verfügbaren Layouts laden und in die ComboBox packen
            dockingComboBox.Items.Clear();
            string[] dockings = Directory.GetFiles(Pathes.LayoutFolder, "*.dock");
            foreach (string docking in dockings)
            {
                dockingComboBox.Items.Add(new ImageComboBoxItem(Path.GetFileNameWithoutExtension(docking), docking));
            }
        }

        private void UpdateFtpConnectionsCombo()
        {
            // Alle verfügbaren Ftp Connections laden und in die ComboBox packen
            repositoryItemComboBoxConnections.Items.Clear();
            var tmpCollection = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
            if (tmpCollection.Count() > 0)
            {
                foreach (var ftpSetting in tmpCollection)
                    repositoryItemComboBoxConnections.Items.Add(ftpSetting);
            }
        }

        private void BeiDockModeEditValueChanged(object sender, EventArgs e)
        {
            // Layout wurde gewechselt
            string dockfile = beiDockMode.EditValue.ToString();
            LoadDockLayout(dockfile, false);
        }

        private void LoadDockLayout(string dockfile, bool init)
        {
            // Layout aus Datei laden
            iDeleteLayout.Enabled = File.Exists(dockfile);
            if (File.Exists(dockfile))
            {
                beiDockMode.EditValue = dockfile;
                barManager.RestoreFromXml(dockfile);
                Settings.Default.LastDock = dockfile;
                Settings.Default.Save();
                UpdatePlugInVisibility();
            }
            else
            {
                if (init && File.Exists(Pathes.LayoutFolder.AddDirectorySeparatorChar() + GetVersion() + Const.TemporaryDockFile))
                    barManager.RestoreFromXml(Pathes.LayoutFolder.AddDirectorySeparatorChar() + GetVersion() + Const.TemporaryDockFile);
            }
        }

        internal void UpdateProjectSpecificItems(object sender, EventArgs e)
        {
            iCloseProject.Enabled = projectExplorer.IsProjectOpen;
            // iOpenProject.Enabled = !projectExplorer.IsProjectOpen;
            iAddProject.Enabled = projectExplorer.IsProjectOpen;
            // iCreateNewProject.Enabled = !projectExplorer.IsProjectOpen;
            iAddExistingDirectory.Enabled = projectExplorer.IsProjectOpen;
            iAddNewItem.Enabled = projectExplorer.IsProjectOpen;
            iAddExistingItem.Enabled = projectExplorer.IsProjectOpen;
            iAddProject.Enabled = projectExplorer.IsProjectOpen;
            iExportProject.Enabled = projectExplorer.IsProjectOpen;
            iUploadProject.Enabled = projectExplorer.IsProjectOpen;
            barButtonItemAddFileToProject.Visibility = projectExplorer.IsProjectOpen ? BarItemVisibility.Always : BarItemVisibility.Never;

            if (projectExplorer.IsProjectOpen)
                taskListControl.LoadTasks(projectExplorer.ProjectFileName + Const.TasksAdd);
            else
                taskListControl.LoadTasks();


        }

        internal void EnableControls()
        {
            this.CheckInvoke(() =>
            {
                iCut.Enabled =
                iSelectAll.Enabled =
                iCycle.Enabled =
                iClipboard.Enabled =
                iCopy.Enabled =
                iPaste.Enabled =
                iUndo.Enabled =
                iRedo.Enabled = false;

                IEditFeatures editFeatures = null;

                if (CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor != null && CurrentEditor is IEditFeatures)
                    editFeatures = CurrentEditor as IEditFeatures;
                else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                    editFeatures = CurrentDesigner as IEditFeatures;


                if (editFeatures != null)
                {
                    iCut.Enabled = editFeatures.CanCut;
                    iCopy.Enabled = editFeatures.CanCopy;
                    iPaste.Enabled = iCycle.Enabled = iClipboard.Enabled = editFeatures.CanPaste;
                    iUndo.Enabled = editFeatures.CanUndo;
                    iRedo.Enabled = editFeatures.CanRedo;
                    iSelectAll.Enabled = true;
                }

                iSaveAs.Enabled = CurrentContentEditor != null;
                iSaveToFtp.Enabled = CurrentContentEditor != null;
                iSave.Enabled = CurrentContentEditor != null;
                iSave.Caption = Resources.Save;
                if (CurrentContentEditor != null)
                {
                    iSave.Enabled = !ActiveMainEditForm.IsSaved;
                    iSave.Caption = Resources.Save + @" " + Path.GetFileName(ActiveMainEditForm.FileName);
                }

                iEditWith.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));
                iFileInformation.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));
                barButtonShowFileSystemMenu.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));

                iRename.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));
                iCopyFullPath.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));
                iOpenContainingFolder.Enabled = (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile));

                iStart.Enabled = ComboBoxExecuters.Items.Count > 0;

            });
        }

        private void EditorSelectionChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                EnableControls();
                propertyGrid.Tag = 1;
                if (CurrentContentEditor != null)
                {
                    propertyGrid.CheckInvoke(() => propertyGrid.PropertyGrid.SelectedObject = CurrentContentEditor.SelectedContent);
                    ComplexEvents.Default.InvokeSelectionChanged(sender, e);
                }
            });

        }

        private void RepositoryItemComboBox1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && eFind.EditValue != null)
                searchItemComboBox1.Items.Add(eFind.EditValue.ToString());
        }

        private void TextBox2KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void ItemNewProjectClick(object sender, ItemClickEventArgs e)
        {
            New(ProjectControlMode.Project);
        }

        private void ItemNewFileClick(object sender, ItemClickEventArgs e)
        {
            New(ProjectControlMode.FileNew);
        }

        private void ItemNewBlankProjectClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new SaveFileDialog { DefaultExt = ".cspr", Filter = Resources.ProjectFilter };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (projectExplorer.IsProjectOpen)
                    projectExplorer.CloseProject(false);
                projectExplorer.CreateNewProject(dlg.FileName);
                GetTopDockPanel(dockPanelSolutionExplorer).Show();
            }
        }

        internal void New(ProjectControlMode mode)
        {
            using (var control = new NewProjectControl(mode))
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(control);

                dlg.CheckBox.Visible = (mode == ProjectControlMode.Project);
                dlg.CheckBox.Text = Resources.AsLinkedProject;
                dlg.IsValid = control.IsValid;

                dlg.OnAccept = () => CreateFromProjectControl(control, dlg.CheckBox.Checked);
                dlg.ShowDialog();
            }
        }

        internal void CreateFromProjectControl(NewProjectControl control, bool asLinkedProject)
        {
            // New file
            if (control.ControlMode == ProjectControlMode.FileNew)
            {
                CreateNewFileFromTemplate(control.TemplateFile, control.ItemName);
            }
            // Add New File to current project
            else if (control.ControlMode == ProjectControlMode.FileAdd)
            {
                if (projectExplorer.IsProjectOpen)
                {
                    if (!projectExplorer.FolderOrProjectSelected)
                        projectExplorer.SelectRootNode();
                    string path = Path.GetTempPath().AddDirectorySeparatorChar();
                    File.Copy(control.TemplateFile, path + Path.GetFileName(control.TemplateFile), true);
                    if (File.Exists(path + Path.GetFileName(control.TemplateFile)))
                    {
                        projectExplorer.AddExistingItems(path + Path.GetFileName(control.TemplateFile));
                    }
                }
            }
            // Create a new Project
            else if (control.ControlMode == ProjectControlMode.Project)
            {
                string projectdir = control.ProjectLocation;
                if (projectExplorer.IsProjectOpen)
                    projectExplorer.CloseProject(false);

                if (!Directory.Exists(projectdir))
                    Directory.CreateDirectory(projectdir);
                if (control.CreateDirectory)
                {
                    projectdir += control.ProjectName;
                    if (!Directory.Exists(projectdir))
                        Directory.CreateDirectory(projectdir);
                }

                string ext = asLinkedProject ? ".cslpr" : ".cspr";
                string projectfile = projectdir.AddDirectorySeparatorChar() + control.ProjectName + ext;

                projectExplorer.CreateNewProject(projectfile);
                File.Copy(control.TemplateFile, projectdir.AddDirectorySeparatorChar() + control.ItemName);
                var filesToAdd = new List<string> { projectdir.AddDirectorySeparatorChar() + control.ItemName };

                // Prüfen wenn content.zip existiert, dem Projekt alle Datein aus dem zip hinzufügen);
                if (File.Exists(control.ProjectContentFile))
                {
                    using (new WaitingScope(projectExplorer, WaitingDialogDescription.Default))
                    {
                        var zip = ZipStorer.Open(control.ProjectContentFile, FileAccess.Read);
                        string localTmpPath = Path.GetTempPath().AddDirectorySeparatorChar() + "CS_TMP_PROJECT_" + Guid.NewGuid().ToString().AddDirectorySeparatorChar();
                        if (!Directory.Exists(localTmpPath))
                            Directory.CreateDirectory(localTmpPath);

                        // Read the central directory collection
                        List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();

                        // Look for the desired file
                        foreach (ZipStorer.ZipFileEntry entry in dir)
                            zip.ExtractFile(entry, localTmpPath + entry.FilenameInZip);

                        projectExplorer.AddExistingDirectory(localTmpPath, true, String.Empty, false);
                        zip.Close();
                    }
                }

                projectExplorer.SelectRootNode();
                projectExplorer.AddExistingItems(filesToAdd.ToArray());
                projectExplorer.ExpandFirst();
                projectExplorer.StartUpFileName = projectdir.AddDirectorySeparatorChar() + control.ItemName;
                // Alles soweit fertig, hauptdatei öffnen, und den Projektexplorer aktivieren

                CreateContentEditor(projectdir.AddDirectorySeparatorChar() + control.ItemName);
                GetTopDockPanel(dockPanelSolutionExplorer).Show();
            }
        }


        private void CascadeItemClick(object sender, ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void HorizontalItemClick(object sender, ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void VerticalItemClick(object sender, ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void ActiveEditorFocusChanged(EditMode mode)
        {
            // Wenn sich der Fokus des Editors von design zu code oder andersrum ändert
            // Nur bei split mode
            if (ActiveMainEditForm != null && (ActiveMainEditForm.IsInSplitMode || mode != ActiveMainEditForm.ActiveEditMode))
            {
                ThreadPool.QueueUserWorkItem(state => this.CheckInvoke(UpdateDynamicMenuItems));
            }
        }

        private void FrmMainMdiChildActivate(object sender, EventArgs e)
        {
            // Wechseln der Fenster / Dateien
            ComplexEvents.Default.InvokeFileChanged(sender, e);


            if (ActiveMdiChild != null)
            {
                barButtonItemAsToolWindow.Caption = String.Format(Resources.CurrentWindowAsToolWindow,
                                                                  ActiveMdiChild.Text);
                barButtonItemAsToolWindow.Visibility = ActiveMdiChild is MainEditForm
                                               ? BarItemVisibility.Never
                                               : BarItemVisibility.Always;
            }
            else
                barButtonItemAsToolWindow.Visibility = BarItemVisibility.Never;


            ThreadPool.QueueUserWorkItem(state => this.CheckInvoke(() =>
            {
                UpdateExecuters();
                UpdatePlugInVisibility();
                MenuService.UpdateToolsMenu();
                MenuService.UpdateInsertMenu();
                UpdateDynamicMenuItems();
                EnableControls();
                toolBox.DisableNotSupportedItems(CompleX_Studio.CurrentFile);
            }));


        }

        private void ActiveMainEditFormModeChanged(SwitchedEditMode newModi)
        {
            // Wechsel zwischen code, design und split
            ThreadPool.QueueUserWorkItem(state => this.CheckInvoke(() =>
            {
                EnableControls();
                UpdateDynamicMenuItems();
            }));

            ComplexEvents.Default.InvokeActiveEditModeChanged(newModi);
        }

        private static void EnableToolWindowPlugin(DockPanel dockPanel, BarItem barItem, bool enabled)
        {
            //der barItemTag wird benutzt, um zu gucken, ob das dockPanel
            //automatisch unsichtbar gemacht wurde (not supportedfile ext) um es ggf wieder sichtbar zumachen
            if (!enabled && dockPanel.Visible)
            {
                dockPanel.Visible = false;
                barItem.Tag = Const.AutoVisibleChanged;
            }
            else if (enabled && barItem.Tag != null && barItem.Tag is int && (int)barItem.Tag == Const.AutoVisibleChanged)
            {
                dockPanel.Visible = true;
                barItem.Tag = 0;
            }

            //barItem.Enabled = enabled; 
            barItem.Visibility = !enabled ? BarItemVisibility.Never : BarItemVisibility.Always;
        }


        private void CutItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).CutToClipboard();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).CutToClipboard();
        }

        private void CopyItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).CopyToClipboard();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).CopyToClipboard();
        }

        private void PasteItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).PasteFromClipboard();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).PasteFromClipboard();
        }

        private void SelectAllItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).SelectAll();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).SelectAll();
        }

        private void UndoItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).Undo();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).Undo();
            EnableControls();
        }

        private void RedoItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentEditor != null && CompleX_Studio.ActiveEditMode == EditMode.Editor && CurrentEditor is IEditFeatures)
                ((IEditFeatures)CurrentEditor).Redo();
            else if (CompleX_Studio.ActiveEditMode == EditMode.Designer && CurrentDesigner != null && CurrentDesigner is IEditFeatures)
                ((IEditFeatures)CurrentDesigner).Redo();
            EnableControls();
        }

        private DockPanel GetTopDockPanelCore(DockPanel panel)
        {
            if (panel.ParentPanel != null)
                return GetTopDockPanel(panel.ParentPanel);
            return panel;
        }

        internal DockPanel GetTopDockPanel(DockPanel panel)
        {
            DockPanel floatPanelCandidate = GetTopDockPanelCore(panel);
            if (floatPanelCandidate.Dock == DockingStyle.Float)
                return floatPanelCandidate;
            return panel;
        }

        private void SolutionExplorerItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelSolutionExplorer).Show();
        }

        private void PropertiesItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelProperties).Show();
        }

        private void TaskListItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelTaskList).Show();
        }

        private void FindResultsItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelFindResults).Show();
        }

        private void OutputItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelOutPut).Show();
        }

        private void ToolboxItemClick(object sender, ItemClickEventArgs e)
        {
            GetTopDockPanel(dockPanelToolbox).Show();
        }

        private void SaveLayoutItemClick(object sender, ItemClickEventArgs e)
        {
            SaveDockingLayout();
        }

        private void SaveDockingLayout()
        {
            string def = beiDockMode.EditValue != null
                             ? Path.GetFileNameWithoutExtension(beiDockMode.EditValue.ToString())
                             : String.Empty;
            string dockName = InputDlg.Execute(Resources.LayoutHeader, Resources.Name, def);
            if (!String.IsNullOrEmpty(dockName))
            {
                string filename = Pathes.LayoutFolder.AddDirectorySeparatorChar() + dockName + ".dock";
                if (File.Exists(filename))
                {
                    if (MessageService.ShowConfirmation(String.Format(Resources.LayoutExistsReplaceShowConfirmation, dockName),
                                        Resources.ShowConfirmation) != System.Windows.Forms.DialogResult.Yes)
                    {
                        return;
                    }
                }
                else
                {
                    dockingComboBox.Items.Add(new ImageComboBoxItem(dockName, filename));
                }
                Refresh(true);
                barManager.SaveToXml(filename);
                Refresh(false);

                beiDockMode.EditValue = filename;
                Settings.Default.LastDock = filename;
                Settings.Default.Save();
            }
        }

        private void Refresh(bool isWait)
        {
            if (isWait)
            {
                currentCursor = System.Windows.Forms.Cursor.Current;
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            }
            else
                System.Windows.Forms.Cursor.Current = currentCursor;
            Refresh();
        }

        private void ExitItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void BarManager1ItemClick(object sender, ItemClickEventArgs e)
        {
            actionListControl.Add(new NamedAction(e.Item.Name, e.Item.Caption, e.Item.PerformClick));
        }


        private void InitTabbedMdi()
        {
            xtraTabbedMdiManager.MdiParent = biTabbedMDI.Down ? this : null;
            iCascade.Enabled = iHorizontal.Enabled = iVertical.Enabled = !biTabbedMDI.Down;
            if (biTabbedMDI.Down)
            {
                if (Settings.Get("ShowFileIconOnTabPage", true))
                {
                    foreach (XtraMdiTabPage page in xtraTabbedMdiManager.Pages)
                        DevExpressHelper.SetFormIconOnTabPage(page);
                }
            }
        }

        private void BiTabbedMdiItemClick(object sender, ItemClickEventArgs e)
        {
            InitTabbedMdi();
        }

        private void DeleteLayoutItemClick(object sender, ItemClickEventArgs e)
        {
            string filename = beiDockMode.EditValue != null ? beiDockMode.EditValue.ToString() : String.Empty;
            if (File.Exists(filename))
            {
                if (MessageService.ShowConfirmation(String.Format(Resources.DeleteLayoutShowConfirmation, Path.GetFileNameWithoutExtension(filename)),
                        Resources.ShowConfirmation) == System.Windows.Forms.DialogResult.Yes)
                {
                    File.Delete(filename);
                    iDeleteLayout.Enabled = false;
                    LoadLayouts();
                }
            }
        }


        private void OpenItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteFileOpenDialog();
        }


        private void ProjectExplorerPropertiesItemClick(object sender, EventArgs e)
        {
            string fileName = projectExplorer.SelectedFileNames[0];
            if (!String.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                var fileInfo = new FileInfo(fileName);
                InspectObject(fileInfo);
            }
        }

        private void StartItemClick(object sender, ItemClickEventArgs e)
        {
            if (eExecuter.EditValue is CodeExecuter && ComboBoxExecuters.Items.Count > 0)
            {
                int mode = -1;
                ImageComboBoxItem item = ComboBoxExecuters.Items.GetItem(eExecuter.EditValue);
                if (item != null)
                    mode = item.ImageIndex;
                Execute(eExecuter.EditValue as CodeExecuter, mode);
            }
        }


        private void DockManagerVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (e.Panel != null)
            {
                var dockPanel = e.Panel;
                if (dockPanelList.ContainsKey(dockPanel))
                {
                    BarButtonItem item = dockPanelList[dockPanel];
                    item.Down = dockPanel.Visible;
                }
            }
        }

        private void DockManagerActivePanelChanged(object sender, ActivePanelChangedEventArgs e)
        {
            ComplexEvents.Default.InvokeToolWindowChanged(this, e);
        }

        private void ClipboardItemClick(object sender, ItemClickEventArgs e)
        {
            var selectStringDialog = new SelectStringDialog(ClipboardController.Current.ClipboardStringList);
            if (selectStringDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (CurrentContentEditor != null)
                    CurrentContentEditor.Insert(selectStringDialog.SelectedText);
            }
        }


        private void XtraTabbedMdiManagerMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (ModifierKeys == Keys.Control && File.Exists(CompleX_Studio.CurrentFile))
                {
                    var arrFi = new FileInfo[1];
                    arrFi[0] = new FileInfo(CompleX_Studio.CurrentFile);
                    var ctxMnu = new ShellContextMenu();
                    ctxMnu.ShowContextMenu(arrFi, System.Windows.Forms.Cursor.Position);
                }
                else
                {
                    if (ActiveMdiChild != null)
                    {
                        iEditWith.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                        iFileInformation.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                        barButtonShowFileSystemMenu.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                        iRename.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                        iCopyFullPath.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;
                        iOpenContainingFolder.Visibility = ActiveMainEditForm != null ? BarItemVisibility.Always : BarItemVisibility.Never;

                        popupMenuTab.ShowPopup(System.Windows.Forms.Cursor.Position);
                    }
                }
            }
        }

        private void XtraTabbedMdiManagerSelectedPageChanged(object sender, EventArgs e)
        {
            iStatus1.Caption = CompleX_Studio.CurrentFile;
        }

        private void AboutItemClick(object sender, ItemClickEventArgs e)
        {
            new AboutDialog().ShowDialog();
        }

        private void ISaveItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.SaveCurrentFile();
        }


        private void BarButtonItem2ItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.SaveCurrentFileAs();
        }

        private void ISaveAllItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.SaveAll();
        }

        private void ICloseItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.CloseCurrentForm();
        }

        private void ComplexMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            const bool useSaveFileDialog = true;
            if (ActiveMainEditForm != null)
                ActiveMainEditForm.PromptSaveMessage = !useSaveFileDialog;
            e.Cancel = !FileService.CloseAllForms(useSaveFileDialog);
            if (e.Cancel)
            {
                if (ActiveMainEditForm != null)
                    ActiveMainEditForm.PromptSaveMessage = useSaveFileDialog;
            }
            else
            {
                CleanUp();
            }
        }

        private void CleanUp()
        {
            taskListControl.Save();
            string file = Pathes.LayoutFolder.AddDirectorySeparatorChar() + "TEMP".AddDirectorySeparatorChar() + "_tmpFullToggle.xml";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            else
            {
                barManager.SaveToXml(Pathes.LayoutFolder.AddDirectorySeparatorChar() + GetVersion() +
                                     Const.TemporaryDockFile);
            }
        }

        private void ICloseAllItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.CloseAllForms(true);
        }

        private void ComplexMainForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Move : DragDropEffects.None;
        }


        private void ComplexMainFormDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var dragList = (string[])e.Data.GetData(DataFormats.FileDrop);
                var fileList = new List<string>();
                foreach (var s in dragList)
                {
                    if (File.Exists(s))
                        fileList.Add(s);
                    else
                    {
                        if (Directory.Exists(s))
                            fileList.AddRange(Directory.GetFiles(s));
                    }

                }
                LoadFiles(fileList);
            }
        }

        private void CloseAllButThisItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                var formsToCLose = FileService.OpenForms.Where(form => !ReferenceEquals(form, ActiveMdiChild));
                FileService.CloseForms(true, formsToCLose.ToArray());
            }
        }

        private void CopyFullPathItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveMainEditForm != null)
            {
                Clipboard.SetText(ActiveMainEditForm.FileName);
            }
        }

        private void OpenContainingFolderItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveMainEditForm != null && Directory.Exists(Path.GetDirectoryName(ActiveMainEditForm.FileName)))
            {
                Process.Start(Path.GetDirectoryName(ActiveMainEditForm.FileName));
            }
        }

        private void ToggleFullscreenItemClick(object sender, ItemClickEventArgs e)
        {
            ToggleFullScreen();
        }


        internal void ToggleFullScreen()
        {
            string file = Pathes.LayoutFolder.AddDirectorySeparatorChar() + "TEMP".AddDirectorySeparatorChar() + "_tmpFullToggle.xml";
            if (!Directory.Exists(Path.GetDirectoryName(file)))
                Directory.CreateDirectory(Path.GetDirectoryName(file));
            if (File.Exists(file))
            {
                barManager.RestoreFromXml(file);
                UpdatePlugInVisibility();
                File.Delete(file);
            }
            else
            {
                barManager.SaveToXml(file);
                bool keepMainMenu = Settings.Get(Const.SettingKeepMainMenuOnFullScreen, false);
                try
                {
                    foreach (Bar bar in barManager.Bars)
                    {
                        if (!bar.IsMainMenu || !keepMainMenu)
                            bar.Visible = false;
                    }
                    foreach (DockPanel panel in dockManager.Panels)
                        panel.Visible = false;
                }
                catch (Exception x)
                {
                    CompleX_Studio.MessageLog.History.Add(new LogEntry(DateTime.Now, LogType.Warning, x.Message));
                }
            }
        }

        private void XtraTabbedMdiManagerPageAdded(object sender, MdiTabPageEventArgs e)
        {
            UpdateOpenFileList();
        }

        private void XtraTabbedMdiManagerPageRemoved(object sender, MdiTabPageEventArgs e)
        {
            UpdateOpenFileList();
        }

        private void BarButtonItem2ItemClick1(object sender, ItemClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile))
                if (FileService.RenameFile(CompleX_Studio.CurrentFile))
                {
                    RecentFiles = recentFileListControl.GetFiles().ToList();
                    UpdateRecentListMenu();
                    RecentFiles.TryXmlSerialize(Pathes.RecentFileList);
                }
        }

        private void BarButtonItem3ItemClick(object sender, ItemClickEventArgs e)
        {
            string dir = Directory.Exists(shellControl.Dir) ? shellControl.Dir : DriveInfo.GetDrives()[0].RootDirectory.FullName;
            if (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile))
                dir = Path.GetDirectoryName(CompleX_Studio.CurrentFile);
            QuickOpenForm.Execute(dir, CreateContentEditor);
            // LoadFiles(fileList.ToList());
        }

        private void IFileInformationItemClick(object sender, ItemClickEventArgs e)
        {
            if (!String.IsNullOrEmpty(CompleX_Studio.CurrentFile))
                FileSystemUtils.ShowProperties(CompleX_Studio.CurrentFile);
        }

        private void ICloseAllFilesItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.CloseForms(true, FileService.OpenForms.OfType<MainEditForm>());
        }

        private void ISaveAllExistingFilesItemClick(object sender, ItemClickEventArgs e)
        {
            var filesToSave = FileService.OpenForms.OfType<MainEditForm>().Where(form => File.Exists(form.FileName));
            FileService.Save(filesToSave);
        }

        private void ICloseAllExistingFilesItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.CloseForms(true, FileService.OpenForms.OfType<MainEditForm>().Where(form => File.Exists(form.FileName)));
        }

        private void CloseToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (string file in openFilesControl.SelectedFiles)
                FileService.CloseFile(file);
        }

        private void IStartNewInstanceItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void BrowserItemClick(object sender, ItemClickEventArgs e)
        {
            CompleX_Studio.ShowBrowserForm("about:blank", iBrowser.Caption);
        }

        private void DosCommandItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new QuickCommandForm { Directory = Settings.Get(Const.SettingLastDosDir, String.Empty), Command = Settings.Get(Const.SettingLastDosCommand, String.Empty), ShowConsole = Settings.Get(Const.SettingShowDosConsole, false) };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string command = dlg.Command;
                string dir = dlg.Directory;
                bool showConsole = dlg.ShowConsole;

                ExecuteDosCommandAndLoadResultAsFile(command, dir, showConsole);
            }
        }


        private void LastDosCommandItemClick(object sender, ItemClickEventArgs e)
        {
            var cmd = Settings.Get<string>(Const.SettingLastDosCommand);
            if (!String.IsNullOrEmpty(cmd))
            {
                bool showConsole = Settings.Get(Const.SettingShowDosConsole, false);
                var dir = Settings.Get<string>(Const.SettingLastDosDir);
                ExecuteDosCommandAndLoadResultAsFile(cmd, dir, showConsole);
            }
            else
            {
                iDOSCommand.PerformClick();
            }
        }

        private void OpenProjectItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new OpenFileDialog { Multiselect = false, DefaultExt = ".cspr", Filter = Resources.ProjectFilter };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                projectExplorer.LoadProject(dlg.FileName);
                GetTopDockPanel(dockPanelSolutionExplorer).Show();
            }
        }

        private void CloseProjectItemClick(object sender, ItemClickEventArgs e)
        {
            projectExplorer.CloseProject(true);
        }

        private void AddExistingDirectoryItemClick(object sender, ItemClickEventArgs e)
        {
            projectExplorer.AddExistingDirectory();
        }

        private void AddNewItemItemClick(object sender, ItemClickEventArgs e)
        {
            projectExplorer.AddNewItem();
        }

        private void AddExistingItemItemClick(object sender, ItemClickEventArgs e)
        {
            projectExplorer.AddExistingItems();
        }

        private void BarButtonItemAddFileToProjectItemClick(object sender, ItemClickEventArgs e)
        {
            if (projectExplorer.IsProjectOpen && !String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile) && !projectExplorer.ProjectFiles.Contains(CompleX_Studio.CurrentFile))
            {
                projectExplorer.AddExistingItems(CompleX_Studio.CurrentFile);
                var addedFile = projectExplorer.ProjectFiles.FirstOrDefault(
                    s => Path.GetFileName(s).Equals(Path.GetFileName(CompleX_Studio.CurrentFile)));

                if (addedFile != null)
                    CreateContentEditor(addedFile);

            }
        }

        private void PopupMenuTabBeforePopup(object sender, CancelEventArgs e)
        {
            if (barButtonItemAddFileToProject.Visibility == BarItemVisibility.Always)
                barButtonItemAddFileToProject.Enabled = (projectExplorer.IsProjectOpen && !String.IsNullOrEmpty(CompleX_Studio.CurrentFile) && File.Exists(CompleX_Studio.CurrentFile) && !projectExplorer.ProjectFiles.Contains(CompleX_Studio.CurrentFile));
        }

        private void ExportProjectItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var dlg = new ExportProjectDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    projectExplorer.ExportProject(dlg.Directory, dlg.ExportProject, dlg.StoreInZip);
                    MessageService.ShowInformation(Resources.ExportDone, Const.ApplicationName);
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError(Resources.ExportFailed);
                CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private void BarButtonFtpSitesClick(object sender, ItemClickEventArgs e)
        {
            ManageFtpAccounts();
        }

        internal void ManageFtpAccounts()
        {
            var page =
                ApplicationHost.Host.GetServiceById<ISettingsPage>(new Guid("557DE147-5DBB-4942-AB92-182625C90BD4"));

            if (page != null)
            {
                CompleX_Studio.ShowOptionPage(page);
                UpdateFtpConnectionsCombo();
            }
        }

        private void FtpExplorer1ConnectionChanged(FtpConnection connection)
        {
            dockPanelFtpExplorer.Text = Resources.FtpExplorer;
            if (connection != null)
                dockPanelFtpExplorer.Text += @" " + connection.FtpSettings;
        }

        private void OpenFromFtpItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.OpenFileFromFtp();
        }

        private void BarButtonItemConnectToItemClick(object sender, ItemClickEventArgs e)
        {
            var settings = barEditItemConnections.EditValue as FtpSettings;
            if (settings != null)
                FileService.OpenFtp(settings);
            else if (repositoryItemComboBoxConnections.Items.Count > 0)
                FileService.OpenFtp();
            else
                ManageFtpAccounts();
        }

        private void SaveToFtpItemClick(object sender, ItemClickEventArgs e)
        {
            FileService.SaveFileToFtp();
        }

        private void BarButtonItemAsToolWindowItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveMdiChild != null)
                FormToToolWindow(ActiveMdiChild);
        }

        private void UploadProjectItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var dlg = new ExportProjectDialog(true);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    projectExplorer.UploadProjectToFtp(dlg.FtpSettings, dlg.Directory, dlg.ExportProject, dlg.StoreInZip);
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError(Resources.ExportFailed);
                CompleX_Studio.MessageLog.LogException(ex);
            }
        }

        private void SaveOutputButtonItemItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new SaveFileDialog { DefaultExt = ".txt", FileName = "Log.txt" };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var writer = new StreamWriter(dlg.FileName);
                writer.Write(textBoxOutPut.Text);
                writer.Close();
            }
        }

        private void OpenOutputButtonItemItemClick(object sender, ItemClickEventArgs e)
        {
            string fileName = FileService.GetTempUniqueDir() + "log.txt";
            var writer = new StreamWriter(fileName);
            writer.Write(textBoxOutPut.Text);
            writer.Close();
            FileService.OpenFile(fileName);
        }

        private void ClearOutPutButtonItemItemClick(object sender, ItemClickEventArgs e)
        {
            textBoxOutPut.Text = String.Empty;
        }

        private void TextBoxOutPutMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                popupMenuOutput.ShowPopup(System.Windows.Forms.Cursor.Position);
        }

        private void HideEmptyToolBars()
        {
            foreach (Bar bar in barManager.Bars)
            {
                if (bar.VisibleLinks.Count <= 0 && bar.Visible)
                {
                    bar.Visible = false;
                    hiddenToolBars.Add(bar);
                }
                else if (hiddenToolBars.Contains(bar) && !bar.Visible)
                {
                    bar.Visible = true;
                    hiddenToolBars.Remove(bar);
                }
            }
        }


        private void TextEditSearchPropertiesEditValueChanged(object sender, EventArgs e)
        {
            foreach (BaseRow row in propertyGrid.PropertyGrid.Rows)
                DevExpressHelper.FilterBaseRow(row, textEditSearchProperties.Text);
        }

        private void iFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchService.ShowSearchDialog();
        }

        private void barButtonShowFileSystemMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (File.Exists(CompleX_Studio.CurrentFile))
            {
                MenuService.ShowShellContextMenu(CompleX_Studio.CurrentFile);
            }
        }

        private void barButtonItemSendError_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EMail.CanWork)
            {
                    var mail = new EMail();
                    mail.AddRecipientTo("feedback@nksoft.de");
                    mail.SendMailPopup("Feedback | Error | Feature CS3", "...");
            }
        }

    }
}