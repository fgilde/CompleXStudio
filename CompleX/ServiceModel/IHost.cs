using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompleX_Library.Interfaces;

namespace CompleX.ServiceModel
{
    /// <summary>
    /// defines a host
    /// </summary>
    public interface IHost 
    {
        /// <summary>
        /// Remove one Service by reference
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        bool RemoveService(object o);

        /// <summary>
        /// Remove alls Services with type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool RemoveServices<T>();

        /// <summary>
        /// Add services to container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        void AddServices<T>(IEnumerable<T> objs) where T:IHostedService;

        /// <summary>
        /// Add a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T AddService<T>(T obj) where T : IHostedService;

        /// <summary>
        /// returns a service
        /// </summary>
        /// <typeparam name="T">a hosted service</typeparam>
        /// <returns></returns>
        T GetService<T>() where T : IHostedService;

        /// <summary>
        /// returns a service with a condition
        /// </summary>
        /// <typeparam name="T">a hosted service</typeparam>
        /// <returns></returns>
        T GetService<T>(Func<T, bool> condition) where T : IHostedService;   
        
        /// <summary>
        /// returns a list of services
        /// </summary>
        /// <typeparam name="T">a hosted service</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetServices<T>() where T : IHostedService;
    }   
}
