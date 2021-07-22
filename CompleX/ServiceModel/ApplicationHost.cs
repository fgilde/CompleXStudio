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
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Threading;
using System.ComponentModel.Composition;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.WPFDialogs;
using CompleX.Properties;
using CompleX.Services;
using CompleX_Library.Helper;
using CompleX_Library.Interfaces;

namespace CompleX.ServiceModel
{
       
    public static class ApplicationHost
    {
        private static Host host;
        private static readonly object SyncObject = new object();

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>The host.</value>
        public static Host Host
        {
            get
            {
                lock (SyncObject)
                {
                    if (host == null)
                        host = new Host();
                    return host;
                }
            }
        }

        /// <summary>
        /// Gets the name of the modules by file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetModulesByFileName<T>(string fileName) where T:IHostedService
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                string currentFileExtension = Path.GetExtension(fileName).ToLower();
                return GetModulesByFileExtension<T>(currentFileExtension);
            }
            return default(IEnumerable<T>);
        }

        /// <summary>
        /// Gets the modules by file extension.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetModulesByFileExtension<T>(string fileExtension) where T:IHostedService
        {
            var modules = Host.GetServices<T>().
                Where(
                        designable => designable.SupportedFileExtensions.ToLower().Contains(fileExtension.ToLower()) ||
                                      designable.SupportedFileExtensions.Count() <= 0 ||
                                      designable.SupportedFileExtensions.ToArray()[0].IsForAll() 
                      );

            return modules;                
        }

        /// <summary>
        /// Creates the new instance of a service .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static  T CreateNewInstance<T>(Func<T,bool> condition) where T : class, IHostedService
        {
            var o = Host.GetService(condition);
            return Activator.CreateInstance(o.GetType()) as T;
        }

        /// <summary>
        /// Loads the plugins from a specefic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> LoadPlugins<T>() where T : IHostedService
        {
            try
            {
                new ComplexPlugins<T>(Host);
            }
            catch (VersionWrongException e)
            {
                CompleX_Studio.MessageLog.LogException(e);
            }
            catch (Exception e)
            {
                CompleX_Studio.MessageLog.LogException(e);
                CompleXException.ShowException(e);
                //throw e;
            }
            return Host.GetServices<T>();               
        }
        
        /// <summary>
        /// Loads the plugins from specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public static IEnumerable<T> LoadPlugins<T>(string directory) where T : IHostedService
        {
            try
            {
                new ComplexPlugins<T>(Host, directory);
            }
            catch (VersionWrongException e)
            {
                CompleX_Studio.MessageLog.LogException(e);
            }
            catch(Exception e)
            {
                CompleX_Studio.MessageLog.LogException(e);
                CompleXException.ShowException(e);
                //throw e;
            }
            return Host.GetServices<T>();        
        }

        /// <summary>
        /// Loads all plugins.
        /// </summary>
        public static void LoadAllPlugins()
        { 
            LoadPlugins<IDesignable>();
            LoadPlugins<IToolWindow>();
            LoadPlugins<IExecutable>();
            LoadPlugins<ISourceEdit>();
            LoadPlugins<ISettingsPage>();
            LoadPlugins<IInserter>();
            LoadPlugins<IObjectEdit>();
        }

        /// <summary>
        /// returns the directory for a service (e.g Modules\IDesignable).
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static string GetDirectory(IHostedService service)
        {
            var attr =
                    (ExportAttribute)Attribute.
                     GetCustomAttribute(service.GetType(),
                     typeof(ExportAttribute));
            if (attr != null)
            {
                return @"Modules\"+attr.ContractType.Name;
            }
            return String.Empty;
        }

        private static void InternalUpdateService(IUpdateableService service)
        {
            DialogContext<WaitingDialog>.Current.Description =
                new WaitingDialogDescription(Resources.PleaseWait, "Update " + service.ServiceName, Resources.PleaseWait, "Update " + service.ServiceName, false, false);

            DialogContext<WaitingDialog>.Current.ShowDialog(false);

            try
            {
                if (service.NewVersionAvailable())
                {
                    //TODO do the update here
                    string path = GetDirectory(service );
                    Thread.Sleep(9000);
                    MessageService.ShowInformation(Resources.UpdateFinished,service.ServiceName);
                }
                else
                    MessageService.ShowInformation(Resources.NoUpdateAvailable, service.ServiceName);              
            }
            finally
            {
                DialogContext<WaitingDialog>.Current.CloseDialog();
            }
        }

        public static void UpdateService(IUpdateableService service)
        {
            var thread = new Thread(() => InternalUpdateService(service));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();           
        }


    }


    /// <summary>
    /// Class for CompleX Plugin loading
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ComplexPlugins<T> where T : IHostedService
    {
        [ImportMany]
        private IEnumerable<T> Plugins { get; set; }

        private string _directory;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return Plugins.Count();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexPlugins&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        public ComplexPlugins(IHost host)
            : this(host, @"Modules\" + typeof(T).Name)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexPlugins&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="dir">The dir.</param>
        public ComplexPlugins(IHost host,string dir)
        {
            _directory = dir;
            Compose();
            host.AddServices(Plugins);
        }

        private void Compose()
        {
            //var catalog = new AssemblyCatalog(@"Modules\Designer\Complex HTMLDesigner.dll");       
            try
            {
                var catalog = new DirectoryCatalog(_directory);
                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (System.Reflection.ReflectionTypeLoadException le)
            {
                foreach (var loadException in le.LoaderExceptions)
                {
                    CompleX_Studio.MessageLog.LogException(loadException);
                }
            }
            catch (Exception e)
            {
                CompleX_Studio.MessageLog.LogError(e.Message);
            }
        }

    }

}