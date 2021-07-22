//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Net;
using CompleX_Types.Interfaces;

namespace CompleX_Types.Extensions
{
    public static class FtpExtension
    {

        public static FtpWebRequest ToFtpWebRequest(this IFtpSettings ftpSettings)
        {
            string uri = "ftp://" + ftpSettings.Server + "/";

            // Create FtpWebRequest object from the Uri provided
            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFtp.Credentials = new NetworkCredential(ftpSettings.UserName, ftpSettings.Password);

            reqFtp.EnableSsl = ftpSettings.EnableSsl;

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFtp.KeepAlive = ftpSettings.KeepAlive;
            return reqFtp;
        }

        public static DateTime? ToDateTime(this WINAPI.FILETIME time)
        {
            if (time.dwHighDateTime == 0 && time.dwLowDateTime == 0)
                return null;

            unchecked
            {
                var low = (uint)time.dwLowDateTime;
                var ft = (((long)time.dwHighDateTime) << 32 | low);
                return DateTime.FromFileTimeUtc(ft);
            }
        }


    }
}
