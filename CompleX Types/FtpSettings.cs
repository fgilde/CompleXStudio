//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================

using System;
using System.ComponentModel;
using CompleX_Types.Interfaces;

namespace CompleX_Types
{
    /// <summary>
    /// FTP Einstellungen, um eine FTP Verbindung aufzubauen
    /// </summary>
    [Serializable]
    public class FtpSettings : IFtpSettings, ICloneable, IEditableObject, INotifyPropertyChanging, INotifyPropertyChanged
    {

        private string name;
        private string serverName;
        private string userName;
        private string password;
        private string defaultDirectory;
        private bool keepAlive;
        private bool enableSsl;
        private int port;

        /// <summary>
        /// Constructor
        /// </summary>
        public FtpSettings()
        {
            Port = WININET.INTERNET_DEFAULT_FTP_PORT;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public FtpSettings(string name):this()
        {
            this.name = name;
        }

        /// <summary>
        /// Ovveride ToString from object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name+" <"+Server+">";
        }

        /// <summary>
        /// IP Adresse oder Servername
        /// </summary>
        /// <value></value>
        public int Port
        {
            get { return port; }
            set
            {
                OnPropertyChanging("Port");
                port = value;
                OnPropertyChanged("Port");
            }
        }

        /// <summary>
        /// IP Adresse oder Servername
        /// </summary>
        /// <value></value>
        public string Name
        {
            get { return name; }
            set
            {
                OnPropertyChanging("Name");
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// IP Adresse oder Servername
        /// </summary>
        /// <value></value>
        public string Server
        {
            get { return serverName; }
            set
            {
                OnPropertyChanging("Server");
                serverName = value;
                OnPropertyChanged("Server");
            }
        }

        /// <summary>
        /// Benutzername der zum Einloggen benutzt werden soll
        /// </summary>
        /// <value></value>
        public string UserName
        {
            get { return userName; }
            set
            {
                OnPropertyChanging("UserName");
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// Passwort für den Login
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                OnPropertyChanging("Password");
                password = value;
                OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Das Startverzeichnis, zu dem nach dem Login gewechselt werden soll
        /// </summary>
        public string DefaultDirectory
        {
            get { return defaultDirectory; }
            set
            {
                OnPropertyChanging("DefaultDirectory");
                defaultDirectory = value;
                OnPropertyChanged("DefaultDirectory");
            }
        }

        /// <summary>
        /// Gibt an ob die Verbindung gehalten werden soll
        /// </summary>
        public bool KeepAlive
        {
            get { return keepAlive; }
            set
            {
                OnPropertyChanging("KeepAlive");
                keepAlive = value;
                OnPropertyChanged("KeepAlive");
            }
        }   

        /// <summary>
        /// Gibt an ob SSL genutzt werden soll
        /// </summary>
        public bool EnableSsl
        {
            get { return enableSsl; }
            set
            {
                OnPropertyChanging("EnableSsl");
                enableSsl = value;
                OnPropertyChanged("EnableSsl");
            }
        }

        /// <summary>
        /// Sets the values with same content of the source.
        /// </summary>
        public void SetValues(FtpSettings source)
        {
            Server = source.Server;
            UserName = source.UserName;
            Password = source.Password;
            DefaultDirectory = source.DefaultDirectory;
            KeepAlive = source.KeepAlive;
            EnableSsl = source.EnableSsl;
        }

        #region Implementation of IEditableObject

        private FtpSettings clone;

        public void BeginEdit()
        {
            clone = Clone() as FtpSettings;
        }

        public void EndEdit()
        {
            clone = null;
        }

        public void CancelEdit()
        {
            if (clone != null)
            {
                SetValues(clone);
                clone = null;
            }
        }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of INotifyPropertyChanging

        [field: NonSerialized]
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(string propertyName)
        {
            if(PropertyChanging != null)
               PropertyChanging(this,new PropertyChangingEventArgs(propertyName));
        }

        #endregion
    }
}
