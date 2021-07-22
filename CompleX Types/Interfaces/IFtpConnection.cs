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
    public interface IFtpConnection
    {
        /// <summary>
        /// FTP Einstellungen, um eine FTP Verbindung aufzubauen
        /// </summary>
        IFtpSettings FtpSettings { get; set; }

        /// <summary>
        /// Gibt an ob diese Connection, mit den gesetzten FtpSettings Einstellungen sich verbinden kann
        /// </summary>
        bool CanConnect{ get;}
    }
}