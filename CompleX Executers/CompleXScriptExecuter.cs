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
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using CompleX.Scripting;
using CompleX_Library;
using CompleX_Library.Interfaces;

namespace CompleX.Executers
{
    [Export(typeof(IExecutable))]
    public class CompleXScriptExecuter:IExecutable
    {
        #region IHostedService
        public Guid ID
        {
            get { return new Guid("BD7F5F07-C04A-4E46-B040-73B8080C433B"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        /// <value></value>
        public string ServiceName
        {
            get { return "CompleX Macro Script"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        /// <value></value>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { ".csms" }; }
        }

        /// <summary>
        /// Function is called if service is added
        /// return true if everything ok otherwise service would not loaded
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(IHostedService other)
        {
            return ID.Equals(other.ID);
        }
        #endregion

        private ScriptLanguage scriptLanguage;
        private string codeToExecute;

        public Dictionary<int, string> ExcecutionModes
        {
            get { 
                return new Dictionary<int, string> {{1, "CompleX Macro Script CS"}, {2, "CompleX Macro Script VB"}};
            }
        }

        public bool Execute(int executionModeId,string file, IContentEdit editor, IEnumerable<string> projectFiles)
        {
            if (editor != null && editor.Content != null)
            {
                codeToExecute = editor.Content.ToString();
                scriptLanguage = (executionModeId == 1 ? ScriptLanguage.CSharp : ScriptLanguage.VbNet);
                var thread = new Thread(GenerateAndRunScript);
                thread.Start();
                return true;
            }
            return false;
        }

        public IEnumerable<LogEntry> ErrorList
        {
            get { return Enumerable.Empty<LogEntry>(); }
        }


        private void GenerateAndRunScript()
        {
            var context = new CompleXScriptContext
            {
                UserName = WindowsIdentity.GetCurrent().Name,
            };
            context.WriteLineDelegate = parameter => AddInfoText(parameter);

            var engine = new ScriptEngine(scriptLanguage, codeToExecute, context);
            engine.StatusChanged += this.ScriptEngine_StatusChanged;
            engine.Execute();
        }

        private void ScriptEngine_StatusChanged(object sender, ScriptEngineStatusEventArgs e)
        {
           AddInfoText(e.Status);
        }

        private void AddInfoText(string text)
        {
            CompleX.Services.OutputService.AddToOutput(text);
        }


    }
}
