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
using System.Reflection;
using System.Windows.Forms;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;

namespace CompleX.Controls
{
    public partial class HostedControl : UserControl,IHostedService, IEquatable<HostedControl>
    {
        private readonly Guid id;
        public HostedControl()
        {
            InitializeComponent();
            id = Guid.NewGuid();
            ApplicationHost.Host.AddService(this);
        }

        public virtual Guid ID
        {
            get { return id; }
        }

        public virtual string ServiceName
        {
            get { return ToString(); }
        }

        public virtual IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] {"*.*"}; }
        }

        public virtual bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public virtual Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public bool Equals(HostedControl other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.id.Equals(id) && Equals(other.components, components);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (HostedControl)) return false;
            return Equals((HostedControl) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (id.GetHashCode()*397) ^ (components != null ? components.GetHashCode() : 0);
            }
        }

        public static bool operator ==(HostedControl left, HostedControl right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HostedControl left, HostedControl right)
        {
            return !Equals(left, right);
        }
    }
}
