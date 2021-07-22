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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Services;
using CompleX_Library.Interfaces;

namespace CompleX.ServiceModel
{
    public class Host : IHost
    {
        private List<object> container;

        public event OnAddService AddingService;

        private void InvokeAddingService(IHostedService svc)
        {
            OnAddService service = AddingService;
            if (service != null) service(this, svc);
        }

        public Host()
        {
            container = new List<object>();
        }

        public bool RemoveService(object o)
        {
           return container.Remove(o);
        }

        public bool RemoveServices<T>()
        {
          // return container.RemoveAll(o => o.GetType() == typeof (T)) > 0; // Not possible, because with gettype interfaces not possible
            List<T> toRemove = container.OfType<T>().ToList();
            for (int i = toRemove.Count()-1 ; i >= 0; i--)
                container.Remove(toRemove[i]);
            return toRemove.Count > 0;
        }

        public void AddServices<T>(IEnumerable<T> objs) where T:IHostedService
        {
            if (objs != null)
            {
                foreach (var obj in objs)
                    AddService(obj);
            }
        }

        public T AddService<T>(T obj) where T:IHostedService
        {
            if (!Equals(obj, default(T)))
            {
                InvokeAddingService(obj);
                var service = GetServices<T>().FirstOrDefault(arg => arg.ID.Equals(obj.ID));
                if (Equals(service, default(T)))
                {
                    if (CheckVersion(obj.GetVersion(), obj.ServiceName))
                    {
                        if (obj.Initialize())
                            container.Add(obj);
                    }else
                    {
                        throw new VersionWrongException(Properties.Resources.PlugInVersionWrongException +
                            ": " + obj.ServiceName + " Version=" + obj.GetVersion() + " "+
                            Assembly.GetExecutingAssembly().GetName().Name + " "+
                            Assembly.GetExecutingAssembly().GetName().Version);
                    }
                }
            }
            return obj;   
        }

        public bool CheckVersion(Version version,string name)
        {
           Version internalVersion =   Assembly.GetExecutingAssembly().GetName().Version;
           bool result = internalVersion.Major.Equals(version.Major) && internalVersion.Minor.Equals(version.Minor);
           if (!result && MessageService.ShowConfirmation(String.Format(Properties.Resources.PlugInVersionWrong,name), name,false,MessageIconType.Warning) == DialogResult.Yes)
               result = true;
           return result;
        }


        public T GetService<T>() where T:IHostedService
        {
            return container.OfType<T>().FirstOrDefault();
        }

        public T GetServiceById<T>(Guid id) where T : IHostedService
        {
            return GetService<T>(arg => arg.ID.Equals(id));
        }

        public T GetServiceByName<T>(string servicename) where T : IHostedService
        {
            return GetService<T>(arg => arg.ServiceName.Equals(servicename));
        }

        public T GetService<T>(Func<T, bool> condition) where T:IHostedService
        {
            return GetServices<T>().FirstOrDefault(condition);
        }

        public IEnumerable<T> GetServices<T>() where T:IHostedService
        {
            return container.OfType<T>();
        }


    }

    public delegate void OnAddService(object sender, IHostedService service);

}