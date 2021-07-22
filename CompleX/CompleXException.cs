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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Services;
using CompleX_Library;

namespace CompleX
{
    public class CompleXException: Exception
    {

        public LogEntry ExceptionContent;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompleXException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CompleXException(string message):base(message)
        {
            if(ExceptionContent == null)
            {
                ExceptionContent = new LogEntry(DateTime.Now, LogType.Exception, message);
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="CompleXException"/> class.
        /// </summary>
        /// <param name="exceptionContent">Content of the exception.</param>
        /// <param name="exceptionCode">The exception code.</param>
        public CompleXException(LogEntry exceptionContent):this(exceptionContent.Message)
        {
            ExceptionContent = exceptionContent;
        }


        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void ShowException(Exception exception)
        {
            ShowException(exception, false);
        }

        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="asWarning"></param>
        public static void ShowException(Exception exception, bool asWarning )
        {
            ShowException(exception, asWarning, String.Empty);
        }

        /// <summary>
        /// Shows the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="asWarning"></param>
        /// <param name="infoText"></param>
        public static void ShowException(Exception exception, bool asWarning, string infoText)
        {
            var icontype = asWarning ? MessageIconType.Warning : MessageIconType.Error;
            using (var exceptionControl = new EditExceptionControl(exception, icontype, false))
            {
                exceptionControl.textLabel.Text = exception.Message;
                exceptionControl.contentEdit.Text += Environment.NewLine + infoText;

                var dlg = BaseDialogHelper.CreateBaseDialog(exceptionControl);
                dlg.TopMost = true;
                dlg.OkBtn.Visible = false;
                dlg.CancelBtn.Text = Properties.Resources.Close;
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Shows the log entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public static void ShowLogEntry(LogEntry entry)
        {
            if (entry.LogType == LogType.Error || entry.LogType == LogType.Exception)
            {
                var exception = new Exception(entry.Message);
                ShowException(exception, false, entry.ToString());
            }
            if(entry.LogType == LogType.Information)
               MessageService.ShowInformation(entry.Message,entry.Time.ToString());
            if(entry.LogType == LogType.Warning)
            {
                MessageService.ShowWarning(entry.Message,entry.Time.ToString());
            }
        }
    }


    public class VersionWrongException:Exception
    {
        public VersionWrongException()
        {
        }

        public VersionWrongException(string message) : base(message)
        {
        }

        public VersionWrongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VersionWrongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
