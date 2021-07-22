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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompleX.Classes;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_Types;
using Microsoft.VisualBasic.FileIO;

namespace CompleX.Services
{
    public static class FileService
    {

        /// <summary>
        /// Gets the open file dialog with dynamic filter.
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog GetOpenFileDialog(string directory)
        {
            var dl = new OpenFileDialog();
            if (Directory.Exists(directory))
                dl.InitialDirectory = directory;
            try
            {
                string filterstring = GetPossibleFileDescription().GetDialogFilter();

                var allSupportedExtensions = GetPossibleFileExtensions();
                var builder = new StringBuilder();
                foreach (string supportedExtension in allSupportedExtensions)
                    builder.Append("*").Append(supportedExtension).Append(";");
                string mask = builder.ToString().Remove(builder.ToString().Length - 1, 1);

                filterstring += "|" + Resources.AllSupportedFiles + " (" + mask + ")|" + mask;

                dl.Filter = Resources.DefaultFilter + @"|" + filterstring;
            }
            catch
            {
                try
                {
                    dl.Filter = Resources.DefaultFilter;
                }
                catch (Exception)
                {
                    dl.Filter = String.Empty;
                }
            }
            return dl;
        }

        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static bool RenameFile(string file)
        {
            if (!String.IsNullOrEmpty(file))
            {
                string newFileName = InputDlg.Execute(Resources.Rename, Resources.Filename, Path.GetFileName(file));
                if (!String.IsNullOrEmpty(newFileName) && newFileName != Path.GetFileName(file))
                {
                    newFileName = Path.GetDirectoryName(file).AddDirectorySeparatorChar() +
                                  newFileName;
                    if (!newFileName.Equals(file))
                    {
                        return RenameFile(file, newFileName);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="newFileName">New name of the file.</param>
        public static bool RenameFile(string file, string newFileName)
        {
            if (File.Exists(file))
            {
     
                newFileName = Path.GetDirectoryName(file).AddDirectorySeparatorChar() +
                             Path.GetFileName(newFileName);

                File.Move(file, newFileName);
                var formToSelect = OpenForms.OfType<MainEditForm>().FirstOrDefault(form => form.FileName.Equals(file));
                if (formToSelect != null)
                    formToSelect.FileName = newFileName;
                var fileListControls = ApplicationHost.Host.GetServices<FileListControl>();
                
                foreach (FileListControl control in fileListControls)
                    control.RenameFile(file, newFileName);
                
                
                return File.Exists(newFileName);
            }
            return false;
        }

        /// <summary>
        /// Returns all possible file extensions.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetPossibleFileExtensions()
        {
            var services = ApplicationHost.Host.GetServices<IContentEdit>();
            if (services != null && services.Count() > 0)
            {
                var result = new List<string>();
                return services.Aggregate(result, (current, contentEdit) => contentEdit.SupportedFileExtensions.Union(current).ToList());
            }
            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Returns all possible filetypes as description List with name, mimetype and extensions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FileDescription> GetPossibleFileDescription()
        {
            var extensions = GetPossibleFileExtensions();
            var result = new List<FileDescription>();
            foreach (string s in extensions)
            {
                var desc = new FileDescription(s);
                var tmpDesc = result.FirstOrDefault(description => description.Name.Equals(desc.Name));
                if (tmpDesc != null)
                    tmpDesc.Extensions.Add(s);
                else
                    result.Add(desc);
            }
            return result;
        }


        /// <summary>
        /// Opens a file.
        /// </summary>
        /// <param name="file">The file.</param>
        public static void OpenFile(string file)
        {
            CompleX_Studio.Instance.CreateContentEditor(file);
        }    
        
        /// <summary>
        /// Opens files.
        /// </summary>
        public static void LoadFiles(IEnumerable<string> files)
        {
            CompleX_Studio.Instance.LoadFiles(files.ToList());
        }

        public static void OpenFileFromFtp()
        {
            var control = new FtpFileListExplorer();

            var dlg = BaseDialogHelper.CreateBaseDialog(control);
            dlg.Text = Resources.OpenFile;
            dlg.IsValid = control.IsValid;
            dlg.OkBtn.Text = Resources.Open;
            dlg.OnAccept = () =>
                               {
                                   using (new WaitingScope(WaitingDialogDescription.Default))
                                   {
                                       control.Enabled = false;
                                       foreach (string item in control.SelectedItems)
                                           OpenFileFromFtp(control.FtpConnection, item);
                                   }
                               };
            dlg.ShowDialog();
        }

        public static bool SaveFileToFtp()
        {
           return SaveFileToFtp(CompleX_Studio.CurrentContentEditor);
        }

        public static bool SaveFileToFtp(IContentEdit editor)
        {
            MainEditForm editForm = MainEditForm.GetMainEditForm(editor);
            if(editForm == null)
                throw new Exception("Could not find Editwindow");
            var control = new FtpFileListExplorer { MultiSelect = false, FileName = Path.GetFileName(editForm.FileName) };
            bool result = false;
            var dlg = BaseDialogHelper.CreateBaseDialog(control);

            dlg.Text = Resources.Save;
            dlg.IsValid = () =>
                              {
                                  if (control.FtpConnection == null || !control.FtpConnection.CanConnect)
                                      return new ValidationResult(false, Resources.PleaseConnectToServer);
                                  if (String.IsNullOrEmpty(control.FileName))
                                      return new ValidationResult(false, Resources.PleaseEnterName);
                                  return new ValidationResult(true, String.Empty);

                              };
            dlg.OkBtn.Text = Resources.Save;
            dlg.OnAccept = () =>
                               {
                                   control.Enabled = false;
                                   result = SaveFileToFtp(control.FtpConnection, editor,
                                                          control.SelectedPath + @"/" + control.FileName);
                               };
            
            dlg.ShowDialog();
            return result;
        }

        public static string GetTempUniqueDir()
        {
            string tempDir = Path.GetTempPath().AddDirectorySeparatorChar() +
                             Guid.NewGuid().ToString().AddDirectorySeparatorChar();
            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);
            return tempDir;
        }

        public static bool SaveFileToFtp(FtpConnection connection, IContentEdit contentEdit, string ftpFileName)
        {
            using (new WaitingScope(new WaitingDialogDescription(Resources.Upload, Resources.Upload, Resources.Wait, Resources.PleaseWaitWhileUpdate, true, true)))
            {
                var connectionClone = connection.Clone() as FtpConnection;
                if (connectionClone != null)
                {
                    if (ftpFileName.StartsWith("ftp:"))
                        ftpFileName = ftpFileName.Substring(4);
                    while (!ftpFileName.StartsWith("//"))
                        ftpFileName = "/" + ftpFileName;

                    string tempDir = GetTempUniqueDir();

                    string tempFile = tempDir + Path.GetFileName(ftpFileName);
                    if (File.Exists(tempFile))
                        File.Delete(tempFile);

                    if (contentEdit.SaveToFile(tempFile))
                    {
                        connectionClone.Open();
                        connectionClone.Login();

                        connectionClone.PutFile(tempFile, ftpFileName);

                        MainEditForm editForm = MainEditForm.GetMainEditForm(contentEdit);
                        editForm.IsSaved = true;
                        if (editForm.FtpConnection == null)
                            editForm.FtpConnection = connection;
                        editForm.FileName = "ftp:" + ftpFileName;
                        CompleX_Studio.Instance.EnableControls();
                        editForm.UpdateTexts();

                        //DELETE TEMP DIR
                        if (Directory.Exists(tempDir))
                            Directory.Delete(tempDir, true);

                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Opens a file from a ftp connection
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="path">The Path to the file you want to open (example /www/htdocs/file.txt)</param>
        public static void OpenFileFromFtp(FtpConnection connection, string path )
        {
            var connectionClone = connection.Clone() as FtpConnection;
            var openForm = OpenForms.OfType<MainEditForm>().FirstOrDefault(form => form.FileName.Equals("ftp:" + path) && form.FtpConnection.FtpSettings.Server.Equals(connection.FtpSettings.Server) );
            if (openForm != null)
            {
                ActivateForm(openForm);
                return;
            }
            if (connectionClone != null)
            {
                connectionClone.Open();
                connectionClone.Login();
                var info = new FtpFileInfo(connectionClone, path);
                if (!info.Exists)
                    throw new FileNotFoundException();

                string tempDir = GetTempUniqueDir();
                string tempFile = tempDir + info.Name;
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
                connectionClone.GetFile(info.FullName, tempFile, false);
                if (File.Exists(tempFile))
                {
                    CompleX_Studio.Instance.CreateContentEditor(tempFile);
                    CompleX_Studio.Instance.ActiveMainEditForm.FileName = "ftp:"+path;
                    CompleX_Studio.Instance.ActiveMainEditForm.FtpConnection = connectionClone;
                }
                //DELETE TEMP DIR
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir,true);
            }
        }

        /// <summary>
        /// Opens a file from a ftp connection
        /// </summary>
        /// <param name="ftpSetting"></param>
        /// <param name="path">The Path to the file you want to open (example /www/htdocs/file.txt)</param>
        public static void OpenFileFromFtp(FtpSettings ftpSetting, string path)
        {
            OpenFileFromFtp(new FtpConnection(ftpSetting) { OutputWriter = new ComplexOutPutWriter() }, path);
        }

        /// <summary>
        /// Activates a file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static bool ActivateFile(string fileName)
        {
            var formToSelect = OpenForms.OfType<MainEditForm>().FirstOrDefault(form => form.FileName.Equals(fileName));
            if (formToSelect != null)
            {
                formToSelect.CheckInvoke(() =>
                {
                    formToSelect.Activate();
                    formToSelect.Focus();
                });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Activates a form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static bool ActivateForm(Form form)
        {
            Form formToSelect = OpenForms.FirstOrDefault(form1 => form1.Equals(form));
            if (formToSelect != null)
            {
                formToSelect.CheckInvoke(() =>
                {
                    formToSelect.Activate();
                    formToSelect.Focus();
                });
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all open forms
        /// </summary>
        public static IEnumerable<Form> OpenForms
        {
            get { return CompleX_Studio.Instance.MdiChildren; }
        }

        /// <summary>
        /// Returns a List of all open files
        /// </summary>
        public static IEnumerable<IContentEdit> OpenFiles
        {
            get
            {
                foreach (Form mdiChild in CompleX_Studio.Instance.MdiChildren)
                {
                    if (mdiChild != null && mdiChild is MainEditForm)
                    {
                        var editFrm = ((MainEditForm)mdiChild);
                        if (editFrm.ActiveEditMode == EditMode.Editor || editFrm.ActiveEditMode == EditMode.Unknown)
                            yield return editFrm.EditControl;
                        else if (editFrm.ActiveEditMode == EditMode.Designer && editFrm.Designer != null)
                            yield return editFrm.Designer;
                    }
                }
            }
        }

        /// <summary>
        /// Saves the specified edits.
        /// </summary>
        public static void Save(IEnumerable<MainEditForm> edits)
        {
            foreach (var edit in edits)
                SaveFile(edit);
        }

        /// <summary>
        /// Saves the specified edits.
        /// </summary>
        public static void Save(params IContentEdit[] edits)
        {
            foreach (var edit in edits)
                SaveFile(edit);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="edit">The edit.</param>
        public static bool SaveFile(IContentEdit edit)
        {
            if (edit != null)
            {
                MainEditForm editForm = MainEditForm.GetMainEditForm(edit);
                if (editForm != null)
                    return SaveFile(editForm);
            }
            return false;
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="editForm">The edit form.</param>
        /// <returns></returns>
        public static bool SaveFile(MainEditForm editForm)
        {
            IContentEdit edit = editForm.ContentEditor;

            if (editForm.FtpConnection != null && editForm.FileName.StartsWith("ftp:"))
                return SaveFileToFtp(editForm.FtpConnection, edit, editForm.FileName);

            if (Directory.Exists(Path.GetDirectoryName(editForm.FileName)) && File.Exists(editForm.FileName))
            {
                bool result = edit.SaveToFile(editForm.FileName);
                editForm.IsSaved = result;
                CompleX_Studio.Instance.EnableControls();
                editForm.UpdateTexts();
                return result;
            }
            return SaveFileAs(edit);

        }


        /// <summary>
        /// Saves the file as.
        /// </summary>
        /// <param name="edit">The edit.</param>
        public static bool SaveFileAs(IContentEdit edit)
        {
            if (edit != null)
            {
                MainEditForm editForm = MainEditForm.GetMainEditForm(edit);
                if (editForm != null)
                {
                    var saveDialog = new SaveFileDialog { FileName = editForm.FileName };
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                        return SaveFileAs(edit, saveDialog.FileName);
                }
            }
            return false;
        }


        /// <summary>
        /// Saves the file as.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <param name="fileName">Name of the file.</param>
        public static bool SaveFileAs(IContentEdit edit, string fileName)
        {
            if (edit.SaveToFile(fileName))
            {
                MainEditForm editForm = MainEditForm.GetMainEditForm(edit);
                if (editForm != null)
                {
                    editForm.CheckInvoke(() =>
                    {
                        editForm.FileName = fileName;
                        editForm.IsSaved = true;
                    });
                    CompleX_Studio.Instance.CheckInvoke(() => CompleX_Studio.Instance.EnableControls());
                }
                CompleX_Studio.Instance.UpdateOpenFileList();
                CompleX_Studio.Instance.UpdateRecentList(fileName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Saves the current file.
        /// </summary>
        public static bool SaveCurrentFile()
        {
            return SaveFile(CompleX_Studio.CurrentContentEditor);
        }

        /// <summary>
        /// Saves the current file as.
        /// </summary>
        public static bool SaveCurrentFileAs()
        {
            return SaveFileAs(CompleX_Studio.CurrentContentEditor);
        }

        /// <summary>
        /// Saves the current file as.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static bool SaveCurrentFileAs(string fileName)
        {
            return SaveFileAs(CompleX_Studio.CurrentContentEditor, fileName);
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        public static void SaveAll()
        {
            Save(OpenFiles.ToArray());
        }

        /// <summary>
        /// Closes the current form.
        /// </summary>
        public static void CloseCurrentForm()
        {
            if (CompleX_Studio.Instance.ActiveMdiChild != null)
                CloseForm(CompleX_Studio.Instance.ActiveMdiChild, true);
        }

        /// <summary>
        /// Closes the current file.
        /// </summary>
        public static void CloseCurrentFile()
        {
            if (CompleX_Studio.Instance.ActiveMainEditForm != null)
                CloseForm(CompleX_Studio.Instance.ActiveMainEditForm, true);
        }

        /// <summary>
        /// Closes the forms.
        /// </summary>
        /// <param name="promptSaveMessage">if set to <c>true</c> [prompt save message].</param>
        /// <param name="forms">The forms.</param>
        /// <returns></returns>
        public static bool CloseForms(bool promptSaveMessage, IEnumerable<Form> forms)
        {
            IEnumerable<MainEditForm> notSaved = OpenForms.OfType<MainEditForm>().Where(form => !form.IsSaved);
            if (promptSaveMessage && notSaved.Count() > 1)
            {

                DialogResult result = new PromptSaveDialog(notSaved).ShowDialog();
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {
                if (notSaved.Count() == 1 && promptSaveMessage)
                    promptSaveMessage = false;
            }
            return forms.All(form => CloseForm(form, !promptSaveMessage));
        }

        /// <summary>
        /// Closes some files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        public static bool CloseFiles(IEnumerable<string> files)
        {
            return CloseForms(true, OpenForms.OfType<MainEditForm>().Where(form => files.Contains(form.FileName)).Cast<Form>());            
        }

        /// <summary>
        /// Closes the forms.
        /// </summary>
        /// <param name="promptSaveMessage">if set to <c>true</c> [prompt save message].</param>
        /// <param name="forms">The forms.</param>
        /// <returns></returns>
        public static bool CloseForms(bool promptSaveMessage, params Form[] forms)
        {
            IEnumerable<MainEditForm> notSaved = OpenForms.OfType<MainEditForm>().Where(form => !form.IsSaved);
            if (promptSaveMessage && notSaved.Count() > 1)
            {

                DialogResult result = new PromptSaveDialog(notSaved).ShowDialog();
                if (result == DialogResult.Cancel)
                    return false;
            }
            else
            {
                if (notSaved.Count() == 1 && promptSaveMessage)
                    promptSaveMessage = false;
            }
            return forms.All(form => CloseForm(form, !promptSaveMessage));
        }

        /// <summary>
        /// Closes all files.
        /// </summary>
        /// <param name="promptSaveMessage">if set to <c>true</c> [prompt save message].</param>
        /// <returns></returns>
        public static bool CloseAllFiles(bool promptSaveMessage)
        {
            return CloseForms(promptSaveMessage, OpenForms.OfType<MainEditForm>().ToArray());
        }

        /// <summary>
        /// Closes all forms.
        /// </summary>
        /// <param name="promptSaveMessage">if set to <c>true</c> [prompt save message].</param>
        /// <returns></returns>
        public static bool CloseAllForms(bool promptSaveMessage)
        {
            return CloseForms(promptSaveMessage, OpenForms.ToArray());
        }


        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="promptSaveMessage">if set to <c>true</c> [prompt save message].</param>
        /// <returns></returns>
        public static bool CloseForm(Form form, bool promptSaveMessage)
        {

            return CompleX_Studio.Instance.CheckInvoke(() =>
            {
                if (form is MainEditForm)
                {
                    var editForm = form as MainEditForm;
                    editForm.PromptSaveMessage = promptSaveMessage;
                }
                form.Close();
                if (form == null)
                    return true;
                return !form.Visible;
            });
        }


        /// <summary>
        /// Determines whether the specified edit is saved.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns>
        /// 	<c>true</c> if the specified edit is saved; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSaved(this IContentEdit edit)
        {
            MainEditForm editForm = MainEditForm.GetMainEditForm(edit);
            if (editForm != null)
                return editForm.IsSaved;
            return false;
        }

        /// <summary>
        /// Gets the name of the file for given editor.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns></returns>
        public static string GetFileName(this IContentEdit edit)
        {
            MainEditForm editForm = MainEditForm.GetMainEditForm(edit);
            if (editForm != null)
                return editForm.FileName;
            return String.Empty;
        }

        /// <summary>
        /// returns the information for file in given editor.
        /// </summary>
        /// <param name="edit">The edit.</param>
        /// <returns></returns>
        public static EditorInformation GetEditorInformation(this IContentEdit edit)
        {
            var form = MainEditForm.GetMainEditForm(edit);
            if (form != null)
            {
                return new EditorInformation
                {
                    Designer = form.Designer,
                    EditControl = form.EditControl,
                    FileName = form.FileName,
                    IsSaved = form.IsSaved,
                    EditMode = form.ActiveEditMode,
                };
            }
            return null;
        }

        public static void CloseFile(string fileName)
        {
            if(ActivateFile(fileName))
               CloseCurrentFile();
        }



        internal static void CreateComplexFileAssociation(string extension, string fileDescription, bool setComplexStudioIcon)
        {
            string progId = Const.ApplicationName + " " + extension;
            var fai = new FileAssociationInfo(extension);
            if (!fai.Exists)
                fai.Create(progId);
            else
                fai.ProgID = progId;

            var pai = new ProgramAssociationInfo(fai.ProgID);
            if (pai.Exists)
                pai.Delete();

            pai.Create(fileDescription, new ProgramVerb("Open", Application.ExecutablePath + " \"%1\" "));
            if (setComplexStudioIcon)
                pai.DefaultIcon = new ProgramIcon(GetOwnFileExtensionIcon(extension));

        }

        public static bool IsAssociated(string extension)
        {
            var fai = new FileAssociationInfo(extension);
            bool isAssociated = false;
            if (fai.Exists)
            {
                var pai = new ProgramAssociationInfo(fai.ProgID);
                var csprogramVerbs = Enumerable.Empty<ProgramVerb>();
                if (pai.Exists)
                    csprogramVerbs = pai.Verbs.Where(verb => verb.Name.ToLower().Equals("open") && verb.Command.Contains(Application.ExecutablePath));

                isAssociated = (fai.Exists && csprogramVerbs.Count() > 0);
            }
            return isAssociated;
        }

        private static string GetOwnFileExtensionIcon(string extension)
        {
            string defaultIcon = Path.GetDirectoryName(Application.ExecutablePath).AddDirectorySeparatorChar() + "cs.ico";
            string csExtensionIcon = Path.GetDirectoryName(Application.ExecutablePath).AddDirectorySeparatorChar() + "cs" + extension + ".ico";
            return File.Exists(csExtensionIcon) ? csExtensionIcon : defaultIcon;
        }

        public static void RemoveFileAssociation(string extension)
        {
            var fai = new FileAssociationInfo(extension);
            if (fai.Exists)
            {
                var pai = new ProgramAssociationInfo(fai.ProgID);
                if (pai.Exists)
                {
                    var programVerb = pai.Verbs.FirstOrDefault(verb => verb.Name.ToLower().Equals("open"));
                    if (programVerb != null)
                    {
                        pai.RemoveVerb(programVerb);
                    }
                }
            }
        }

        public static void CreateFileAssociation(string extension, string fileDescription)
        {
            var fai = new FileAssociationInfo(extension);
            if (!fai.Exists)
                fai.Create(fileDescription);
            var pai = new ProgramAssociationInfo(fai.ProgID);
            if (!pai.Exists)
                pai.Create(fileDescription, new ProgramVerb("Open", Application.ExecutablePath + " \"%1\" "));
            else
            {
                var programVerb = pai.Verbs.FirstOrDefault(verb => verb.Name.ToLower().Equals("open"));
                if (programVerb != null)
                {
                    pai.RemoveVerb(programVerb);
                }
                pai.AddVerb(new ProgramVerb("Open", Application.ExecutablePath + " \"%1\" "));
            }
        }

        public static void DeleteFileWithConfirmation(string fileName)
        {
            DeleteFileWithConfirmation(fileName,@"DSA_DELETE_FILE_MS",false); 
        }

        public static void DeleteFileWithConfirmation(string fileName, string dsaKey, bool dsaOnCancel)
        {
            if(!File.Exists(fileName))
                throw new FileNotFoundException();

            string key = Const.DsaDefault + dsaKey + CompleX_Studio.Version;
            if (!String.IsNullOrEmpty(key))
            {
                string lastAction = Settings.Get(key, "none");
                if (!String.IsNullOrEmpty(lastAction) && lastAction != "node")
                {
                    if (lastAction == "PAPERBASKET")
                    {
                        MoveFileToPaperBasket(fileName);
                        return;
                    }
                    if (lastAction == "DELETE")
                    {
                        File.Delete(fileName);
                        return;
                    }
                    if (lastAction == "CANCEL" && dsaOnCancel)
                        return;
                }
            }
            bool checkBoxChecked;
            string action = "none";
            using (var exceptionControl = new EditExceptionControl(String.Format(Resources.DeleteFileQuestion,fileName), MessageIconType.Question, false))
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(exceptionControl);

                dlg.MaximizeBox = false;
                dlg.Text = Resources.Delete + " " + Path.GetFileName(fileName);
                dlg.OnCancel = () => action = "CANCEL";
                dlg.OnAccept = () =>
                                   {
                                       MoveFileToPaperBasket(fileName);
                                       action = "PAPERBASKET";
                                   };
                dlg.OnNo = () =>
                               {
                                   File.Delete(fileName);
                                   action = "DELETE";
                               };
                dlg.OkBtn.Text = Resources.Paperbasket;
                dlg.CancelBtn.Text = Resources.No;
                dlg.NoBtn.Text = Resources.Delete;
                dlg.NoBtn.Visible = true ;
                dlg.CheckBox.Visible = !String.IsNullOrEmpty(key);
                dlg.CheckBox.Text = Resources.DontAskMeAgian;
                    
                dlg.ShowDialog();
                checkBoxChecked = dlg.CheckBox.Checked;
                
            }
            if (!String.IsNullOrEmpty(key) && checkBoxChecked)
                Settings.Set(key, action);
        }

        public static void MoveFileToPaperBasket(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();
            FileSystem.DeleteFile(fileName, UIOption.OnlyErrorDialogs,RecycleOption.SendToRecycleBin);

        }

        public static void OpenFtp()
        {
            var lb = new ListBox();
            var tmpCollection = Settings.Get("FtpCollection", Enumerable.Empty<FtpSettings>());
            if (tmpCollection.Count() > 0)
            {
                foreach (var ftpSetting in tmpCollection)
                    lb.Items.Add(ftpSetting);
            }
            lb.MinimumSize = new Size(250,150);
            lb.DoubleClick += (o, args) =>
                                  {
                                      var setting = lb.SelectedItem as FtpSettings;
                                      if (setting != null)
                                          OpenFtp(setting);
                                  };
            var dlg = BaseDialogHelper.CreateBaseDialog(lb);
            dlg.MaximizeBox = false;
            dlg.TopMost = true;
            dlg.OnAccept = () =>
                               {
                                   var setting = lb.SelectedItem as FtpSettings;
                                   if (setting != null)
                                       OpenFtp(setting);
                               };
            dlg.Show();  
        }

        public static void OpenFtp(FtpSettings settings)
        {
            if (settings != null)
            {
                var control = new FtpFileListExplorer(settings);

                var dlg = BaseDialogHelper.CreateBaseDialog(control);

                dlg.Text = "FTP: " + settings;
                dlg.AcceptButton = null;
                
                dlg.HideButtonRow();
                control.ConnectionChanged += ftpConnection => { dlg.Text = "FTP: "; if (ftpConnection != null) dlg.Text += ftpConnection.FtpSettings; };
                CompleX_Studio.ShowForm(dlg);
            }
        }

        public static void OpenFtp(FtpConnection connection)
        {
            if (connection != null)
            {
                var control = new FtpFileListExplorer(connection);

                var dlg = BaseDialogHelper.CreateBaseDialog(control);
                dlg.Text = "FTP: " + connection.FtpSettings;
                dlg.AcceptButton = null;
                
                dlg.HideButtonRow();
                control.ConnectionChanged += ftpConnection => { dlg.Text = "FTP: "; if (ftpConnection != null) dlg.Text += ftpConnection.FtpSettings; };
                CompleX_Studio.ShowForm(dlg);
            }
        }

    }
}
