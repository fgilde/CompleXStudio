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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using CompleX.Controls;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;

namespace CompleX.Executers
{
   
    public enum ExecutuionModes
    {
        Internal,
        External,
        OtherBrowser
    }

    [Export(typeof(IExecutable))]
    public class HtmlExecuter:IExecutable, IEquatable<HtmlExecuter>
    {
        private readonly List<LogEntry> errorList =  new List<LogEntry>();

        #region IHostedService
        public Guid ID
        {
            get { return new Guid("36C3B4E6-D8B9-4CA2-BCA4-3B1690CC8613");}
        }

        public string ServiceName
        {
            get { return "CompleX HTML Executer"; }
        }

        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }         
        }

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

        public bool Equals(IHostedService other)
        {
            return ID.Equals(other.ID);
        }
        #endregion

        #region IEquatable<HtmlExecuter>

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(HtmlExecuter left, HtmlExecuter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HtmlExecuter left, HtmlExecuter right)
        {
            return !Equals(left, right);
        }

        public bool Equals(HtmlExecuter other)
        {
            return ((IHostedService)other).ID.Equals(ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (HtmlExecuter)) return false;
            return Equals((HtmlExecuter) obj);
        }

        #endregion

        public Dictionary<int, string> ExcecutionModes
        {
            get
            {
                return new Dictionary<int, string>
                           {
                               {Convert.ToInt32(ExecutuionModes.Internal),Texts.InternalPreview},
                               {Convert.ToInt32(ExecutuionModes.External),Texts.ExternalDefaultPreview},
                               {Convert.ToInt32(ExecutuionModes.OtherBrowser),Texts.OtherBrowser}
                           };
            }
        }

        public bool Execute(int executionModeId, string file, IContentEdit editor, IEnumerable<string> projectFiles)
        {
            errorList.Clear();             
            string dir = Path.GetTempPath()+Guid.NewGuid()+Path.DirectorySeparatorChar;
            Directory.CreateDirectory(dir);
            OutputService.AddToOutput("Creating Directory " + dir);
            var editorInformation = editor.GetEditorInformation();
            string fileName = dir + Path.GetFileName(editorInformation.FileName);
            if (editorInformation.IsSaved && File.Exists(editorInformation.FileName))
                fileName = editorInformation.FileName;
            else
            {
                OutputService.AddToOutput("Save Temporary File " + Path.GetFileName(fileName));
                if (projectFiles != null && projectFiles.Count() > 0)
                {
                    var projectExplorer = ApplicationHost.Host.GetService<ProjectExplorer>();
                    if(projectExplorer != null && projectExplorer.IsProjectOpen)
                    {
                        ThreadPool.QueueUserWorkItem(state => projectExplorer.ExportProject(dir, false, false));
                    }
                    //TODO. Copy ProjectFiles with SameFile Structure to temp
                    //      to ensure all relative files existing
                }
                if(!editor.SaveToFile(fileName))
                {
                    errorList.Add(new LogEntry(DateTime.Now,LogType.Error, Texts.FileCouldNotSave,Path.GetFileName(fileName),0,String.Empty));
                    return false;
                }
            }

            if(executionModeId == Convert.ToInt32(ExecutuionModes.Internal))
                CompleX_Studio.ShowBrowserForm(fileName, Path.GetFileName(fileName) + " [Preview]");

            if (executionModeId == Convert.ToInt32(ExecutuionModes.External))
                System.Diagnostics.Process.Start(fileName);

            if (executionModeId == Convert.ToInt32(ExecutuionModes.OtherBrowser))
            {
                FileSystemUtils.OpenAs(fileName);
            }
            OutputService.AddToOutput("Finished!");
            return true;
        }

        public IEnumerable<LogEntry> ErrorList
        {
            get { return errorList; }
        }
    }
}