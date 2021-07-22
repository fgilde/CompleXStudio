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
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using CompleX_Library.Interfaces;
//using CompleX_Settings;


namespace CompleX_Library
{
    /// <summary>
    /// Provides functionality for a visual log.
    /// </summary>
    public class MessageLog : IMessageLog 
    {
        private List<LogEntry> history = new List<LogEntry>();

        /// <summary>
        /// Delegate for OnAddEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="entry"></param>
        public delegate void MessageLogEventHandler(object sender, LogEntry entry);

        /// <summary>
        /// Event OnAddEntry
        /// </summary>
        public event MessageLogEventHandler AddEntry;
        private void OnAddEntry(LogEntry entry)
        {
            if (AddEntry != null) AddEntry(this, entry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AddEntryEventIsNull()
        {
            return AddEntry == null;
        }

        private static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public void LoadLog()
        {
            var filename = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + AssemblyTitle + @"\errorLog.log";
            LoadLog(filename);            
        }

        public void LoadLog(string fileName)
        {
            if (File.Exists(fileName))
            {
                var fileStream = new FileStream(fileName, FileMode.Open);
                try
                {
                    var serializer = new XmlSerializer(typeof (List<LogEntry>));
                    history = serializer.Deserialize(fileStream) as List<LogEntry>;
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        public bool SaveLog()
        {
            var filename = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + AssemblyTitle + @"\errorLog.log";
            return SaveLog(filename);
        }

        public bool SaveLog(string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            }
            var fileStream = new FileStream(filename, FileMode.Create);
            try
            {
                var serializer = new XmlSerializer(typeof(List<LogEntry>));
                serializer.Serialize(fileStream, history);
            }
            finally
            {
                fileStream.Close();
            }

            return File.Exists(filename);
        }

        /// <summary>
        /// Logs the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void LogEntry(LogEntry entry)
        {
            history.Add(entry);
            OnAddEntry(entry);            
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void LogException(Exception exception)
        {
            var entry = new LogEntry(DateTime.Now, LogType.Exception, exception.Message+Environment.NewLine+exception.StackTrace);
            history.Add(entry);
            OnAddEntry(entry);
        }

        /// <summary>
        /// Logs an error Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void LogError(string message)
        {
            var entry = new LogEntry(DateTime.Now, LogType.Error, message);
            history.Add(entry);
            OnAddEntry(entry);
        }

        /// <summary>
        /// Logs a warning Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void LogWarning(string message)
        {
            var entry = new LogEntry(DateTime.Now, LogType.Warning, message);
            history.Add(entry);
            OnAddEntry(entry);
        }

        /// <summary>
        /// Logs an information Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        public void LogInformation(string message)
        {
            var entry = new LogEntry(DateTime.Now, LogType.Information, message);
            history.Add(entry);
            OnAddEntry(entry);
        }

        ///<summary>
        /// Gets the Message history.
        ///</summary>
        public List<LogEntry> History
        {
            get
            {
                return new List<LogEntry>(history);
            }
        }

        /// <summary>
        /// Cleat History.
        /// </summary>
        public void ClearHistory()
        {
            history.Clear();
        }
    }

    ///<summary>
    /// Defines the type of message,
    ///</summary>
    public enum LogType
    {
        /// <summary>
        /// None.
        /// </summary>
        None,
        
        /// <summary>
        /// Information.
        /// </summary>
        Information,
        
        /// <summary>
        /// Warning.
        /// </summary>
        Warning,
        
        /// <summary>
        /// Error.
        /// </summary>
        Error,

        /// <summary>
        /// Exception
        /// </summary>
        Exception
    }

    /// <summary>
    /// Defines a log entry.
    /// </summary>
    public class LogEntry
    {
        private const string LogTypeFormat = "[{0}]";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time">The time when the message occured.</param>
        /// <param name="logType">The type of message.</param>
        /// <param name="message">The message.</param>
        public LogEntry(DateTime time, LogType logType, string message):this(time,logType,message,String.Empty,0,string.Empty)
        {

        }

        public override string ToString()
        {
            return LogType+" "+Time + " :" + Message;
        }

        public LogEntry(DateTime time, LogType logType, string message, string file, int line, string project)
        {
            Time = time;
            LogType = logType;
            Message = message;
            File = file;
            Line = line;
            Project = project;
        }

        public LogEntry()
        {
            
        }

        /// <summary>
        /// Line
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// The time when the message occured.
        /// </summary>
        public DateTime Time { get; set; }
        
        /// <summary>
        /// The type of message.
        /// </summary>
        public LogType LogType { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; set; }



        /// <summary>
        /// Gets the message box icon.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static MessageBoxIcon GetMessageBoxIcon(LogType type)
        {
            switch (type)
            {
                case LogType.Information:
                    return MessageBoxIcon.Information;
                case LogType.Warning:
                    return MessageBoxIcon.Warning;
                case LogType.Error:
                    return MessageBoxIcon.Error;
                case LogType.Exception:
                    return MessageBoxIcon.Error;
                default:
                    return MessageBoxIcon.None;
            }
        }

        public static string GetLogType(LogType type)
        {
            //TODO: Localize
            switch (type)
            {
                case LogType.Information:
                    return String.Format(LogTypeFormat, "Information");
                case LogType.Warning:
                    return String.Format(LogTypeFormat, "Warnung");
                case LogType.Error:
                    return String.Format(LogTypeFormat, "Fehler");
                case LogType.Exception:
                    return String.Format(LogTypeFormat, "Exception");
                default:
                    return string.Empty;
            }
        }

        public static LogType GetLogType(string type)
        {
            //TODO: Localize
            type = type.Replace("[", "");
            type = type.Replace("]", "");
            switch (type)
            {
                case "Information":
                    return LogType.Information;
                case "Exception":
                    return LogType.Exception;
                case "Warnung":
                    return LogType.Warning;
                case "Fehler":
                    return LogType.Error;
                default:
                    return LogType.None;
            }
        }

    }
}