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
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;

namespace CompleX.Executers
{
    [Export(typeof(IExecutable))]
    public class WebappExecuter:IExecutable
    {
        private readonly List<LogEntry> errorList = new List<LogEntry>();

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("E4468687-4F29-4CAC-937F-0EB0FF01F6B6"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "iPhone WebApp"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { ".webapp"}; }
        }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
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

        #region Implementation of IExecutable

        /// <summary>
        /// List with unique IDs of possible executions 
        /// (Example "Debug" and "Release") 
        /// return empty List to use only 1 executionmode
        /// </summary>
        public Dictionary<int, string> ExcecutionModes
        {
            get { return null; }
        }

        /// <summary>
        /// Auführen des executers
        /// </summary>
        /// <param name="executionModeId">welcher modus gewählt wurde falls der executer mehrere modi erlaubt <see cref="ExcecutionModes"/></param>
        /// <param name="file">Die Datei, die ausgeführt weden soll (bei offenem projekt, die StartUpdatei dieses Projektes) </param>
        /// <param name="editor">Der Aktuelle editor (Hinweis: datei im editor kann eine andere sein als file, z.B kann in file die project satrtup datei stehen, aber im editor eine andere gerade offen sein</param>
        /// <param name="projectFiles">Alle datein im aktuellem projekt</param>
        /// <returns></returns>
        public bool Execute(int executionModeId, string file, IContentEdit editor, IEnumerable<string> projectFiles)
        {
            errorList.Clear();
            OutputService.WriteLine("init iPhone Web Application");
            var editorInformation = editor.GetEditorInformation();
            string fileName = file;

            if (String.IsNullOrEmpty(fileName))
                fileName = editorInformation.FileName;

            // Fehler überprüfen
            if (!editorInformation.IsSaved || !File.Exists(editorInformation.FileName))
            {
                errorList.Add(new LogEntry(DateTime.Now, LogType.Error, "File is not saved, please save file first", Path.GetFileName(fileName),
                                           0, String.Empty));
                OutputService.WriteLine("Failed");
                return false;
            }

            if (projectFiles == null || projectFiles.Count() <= 0)
            {
                errorList.Add(new LogEntry(DateTime.Now, LogType.Error, "There is no correct Project open, or project files are missing", Path.GetFileName(fileName),
                                           0, String.Empty));
                OutputService.WriteLine("Failed");
                return false;
            }

            // Startup file ermitteln
            var startfile = IniFile.ReadValue(fileName, "Startup", "Index", "start.html");
            var startUp = projectFiles.FirstOrDefault(s => Path.GetFileName(s).Equals(startfile));
            if (startUp == null || !File.Exists(startUp))
            {
                errorList.Add(new LogEntry(DateTime.Now, LogType.Error, "Could not find Indexfile '" + startfile + "' ", Path.GetFileName(fileName),
                                           0, String.Empty));
                OutputService.WriteLine("Failed");
                return false; 
            }
            OutputService.WriteLine("Searching Safari");
            string programmfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programmfilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string defaultPath = Path.Combine(Directory.Exists(Path.Combine(programmfiles, "Safari")) ? programmfiles : programmfilesX86, "Safari", "Safari.exe");        
            string safari = Settings.Get("SAFARI_BROWSER", defaultPath);
            if (!File.Exists(safari))
            {
                OutputService.WriteLine("Safari not found!");
                var dlg = new OpenFileDialog {FileName = "Safari.exe", DefaultExt = ".exe", Filter = @"Safari.exe"};
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    safari = dlg.FileName;
                    Settings.Set("SAFARI_BROWSER",safari);
                }
                else
                {
                    return false;
                }
            }

            OutputService.WriteLine("Safari found!");
            bool useOnlyBrowser = Convert.ToBoolean(IniFile.ReadValue(fileName, "Startup", "UseBrowserOnly", "False"));
            // Wenn kein fehler aufgetreten, dann weiter
            string emu = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "iPhone4E", "iPhoneXMU.html");
            if (File.Exists(emu) && !useOnlyBrowser)
            {
                OutputService.WriteLine("Init CompleX iPhone Emulator");
                var streamReader = new StreamReader(emu);
                string contentText = streamReader.ReadToEnd();
                streamReader.Close();
                contentText =  contentText.Replace("STARTUPFILENAME", Path.GetFileName(startUp));
                string emufileName = Path.Combine(Path.GetDirectoryName(startUp), Guid.NewGuid() + ".html");
                var writer = new StreamWriter(new FileStream(emufileName, FileMode.Create));
                writer.Write(contentText);
                writer.Close();
                var startInfo = new ProcessStartInfo(safari, emufileName);
                var proc = new Process(){StartInfo = startInfo,EnableRaisingEvents = true};
                proc.Exited += (sender, args) => File.Delete(emufileName);
                OutputService.WriteLine("Starting....");
                proc.Start();
            }
            else
            {
                Process.Start(safari, startUp);
            }

            return true;
        }

        public IEnumerable<LogEntry> ErrorList
        {
            get { return errorList; }
        }

        #endregion
    }
}