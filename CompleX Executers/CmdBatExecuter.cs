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
using System.Reflection;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;

namespace CompleX.Executers
{
    [Export(typeof(IExecutable))]
    public class CmdBatExecuter : IExecutable
    {
        private readonly List<LogEntry> errorList = new List<LogEntry>();
        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("9CB35CB1-7200-43DA-95B2-73C9EBDF0A04"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "CompleX Batch Executer"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { ".bat", ".cmd" }; } 
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
            get { return null;}
        }

        public bool Execute(int executionModeId,string file, IContentEdit editor, IEnumerable<string> projectFiles)
        {
            errorList.Clear(); 
            var editorInformation = editor.GetEditorInformation();
            string fileName;
            if (editorInformation.IsSaved && File.Exists(editorInformation.FileName))
                fileName = editorInformation.FileName;
            else
            {
                string dir = Path.GetTempPath() + Guid.NewGuid() + Path.DirectorySeparatorChar;
                OutputService.AddToOutput("Creating Temporary Directory");
                fileName = dir + Path.GetFileName(editorInformation.FileName);
                if (!editor.SaveToFile(fileName))
                {
                    errorList.Add(new LogEntry(DateTime.Now, LogType.Error, Texts.FileCouldNotSave, Path.GetFileName(fileName), 0, String.Empty));
                    return false;
                }
            }

            string output;
            string error;
            var startInfo = new ProcessStartInfo(fileName)
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = Process.Start(startInfo);
            if (process != null)
            {
                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                if (output.Length != 0)
                    OutputService.AddToOutput(output);
                else if (error.Length != 0)
                {
                    OutputService.AddToOutput(error);
                    errorList.Add(new LogEntry(DateTime.Now,LogType.Error, error,fileName,0,String.Empty));
                }
                return true;
            }
            return false;
        }

        public IEnumerable<LogEntry> ErrorList
        {
            get { return errorList; }
        }

        #endregion
    }
}
