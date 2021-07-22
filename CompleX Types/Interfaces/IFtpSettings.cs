//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
namespace CompleX_Types.Interfaces
{
    /// <summary>
    /// FTP Einstellungen, um eine FTP Verbindung aufzubauen
    /// </summary>
    public interface IFtpSettings
    {
        /// <summary>
        /// Port
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// IP Adresse oder Servername
        /// </summary>
        string Server { get; set; }

        /// <summary>
        /// Benutzername der zum Einloggen benutzt werden soll
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Passwort für den Login
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Das Startverzeichnis, zu dem nach dem Login gewechselt werden soll
        /// (optional)
        /// </summary>
        /// <value>The default directory.</value>
        string DefaultDirectory { get; set; }

        /// <summary>
        /// Gibt an ob die Verbindung gehalten werden soll
        /// </summary>
        bool KeepAlive { get; set; }

        /// <summary>
        /// Gibt an ob SSL genutzt werden soll
        /// </summary>
        bool EnableSsl { get; set; }
    }
}