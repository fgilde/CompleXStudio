//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using CompleX.Presentation.Controls.Extensions;

namespace CompleX.Services
{
    public static class OutputService
    {
        /// <summary>
        /// Adds a string to CS Messageoutput window.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void AddToOutput(string message)
        {
            if (CompleX_Studio.Instance != null)
                CompleX_Studio.Instance.CheckInvoke(() => CompleX_Studio.Instance.AddOutput(message,true));
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteLine(string message)
        {
            AddToOutput(message);
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void WriteLine(string message,params object[] args)
        {
            AddToOutput(string.Format(message,args));
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Write(string message)
        {
            if (CompleX_Studio.Instance != null)
                CompleX_Studio.Instance.CheckInvoke(() => CompleX_Studio.Instance.AddOutput(message, false));
        }

    }
}
