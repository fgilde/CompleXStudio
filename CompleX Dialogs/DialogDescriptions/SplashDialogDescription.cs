using System;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls.DialogDescriptions
{

    public class SplashDialogDescription : IDialogDescription, IEquatable<SplashDialogDescription>
    {
        public int ProgressValue { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public string Action  { get; set; }
        public string Info  { get; set; }
        public bool TopMost { get; set; }


        public SplashDialogDescription(string product, string version)
        {
            Product = product;
            Version = version;
        }

        public bool Equals(SplashDialogDescription other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Product, Product) && Equals(other.Version, Version);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != typeof (SplashDialogDescription)) return false;
            return Equals((SplashDialogDescription) other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Product != null ? Product.GetHashCode() : 0)*397) ^ (Version != null ? Version.GetHashCode() : 0);
            }
        }

        public static bool operator ==(SplashDialogDescription left, SplashDialogDescription right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SplashDialogDescription left, SplashDialogDescription right)
        {
            return !Equals(left, right);
        }

        public bool IsValid
        {
            get { return !String.IsNullOrEmpty(Product) && ! String.IsNullOrEmpty(Version); }
        }
    }
}