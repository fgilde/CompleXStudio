//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Windows.Forms;
using Application = System.Windows.Application;
using WinFormsApplication = System.Windows.Forms.Application; 


namespace CompleX
{
    /// <summary>
    /// Fehlerbehandlung
    /// </summary>
    public class ErrorManager
    {

        /// <summary>
        /// Initialisiert eine neue Instanz von der <see cref="ErrorManager"/> class Klasse
        /// </summary>
        public ErrorManager()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            if (Application.Current != null) // WPF 
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            WinFormsApplication.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            WinFormsApplication.ThreadException += WinFormsApplication_ThreadException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            if (exception != null)
                HandleException(exception);
        }

        void WinFormsApplication_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            HandleException(exception);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            HandleException(exception);
            e.Handled = true;
        }

        private void HandleException(Exception exception)
        {
            CompleX_Studio.MessageLog.LogException(exception);
            CompleXException.ShowException(exception);
            //UnhandledErrorViewModel em = new UnhandledErrorViewModel(exception);
            //em.ExecuteShowError(this);
        }

    }
}