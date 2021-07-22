using System.Collections.Generic;

namespace CompleX_Library.Interfaces
{
    /// <summary>
    /// Provides access the Message log.
    /// </summary>
    public interface IMessageLog
    {
        /// <summary>
        /// Logs an error Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        void LogError(string message);

        /// <summary>
        /// Logs a warning Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs an information Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        void LogInformation(string message);

        ///<summary>
        /// Gets the Message history.
        ///</summary>
        List<LogEntry> History { get; }

        /// <summary>
        /// Cleat History.
        /// </summary>
        void ClearHistory();
    }
}