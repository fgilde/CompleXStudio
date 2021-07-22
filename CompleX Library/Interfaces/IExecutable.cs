using System.Collections.Generic;

namespace CompleX_Library.Interfaces
{
    public interface IExecutable:IHostedService
    {

        /// <summary>
        /// List with unique IDs of possible executions 
        /// (Example "Debug" and "Release") 
        /// return empty List to use only 1 executionmode
        /// </summary>
        Dictionary<int, string> ExcecutionModes { get; }

        /// <summary>
        /// Auführen des executers
        /// </summary>
        /// <param name="executionModeId">welcher modus gewählt wurde falls der executer mehrere modi erlaubt <see cref="ExcecutionModes"/></param>
        /// <param name="file">Die Datei, die ausgeführt weden soll (bei offenem projekt, die StartUpdatei dieses Projektes) </param>
        /// <param name="editor">Der Aktuelle editor (Hinweis: datei im editor kann eine andere sein als file, z.B kann in file die project satrtup datei stehen, aber im editor eine andere gerade offen sein</param>
        /// <param name="projectFiles">Alle datein im aktuellem projekt</param>
        /// <returns></returns>
        bool Execute(int executionModeId, string file, IContentEdit editor, IEnumerable<string> projectFiles);

        /// <summary>
        /// Falls 
        /// Fehler beim Execute aufgetreten sind hier die liste zurückgeben
        /// </summary>
        IEnumerable<LogEntry> ErrorList { get; }
    }
}
