using System;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls.DialogDescriptions
{
    public class WaitingDialogDescription : IDialogDescription, IEquatable<WaitingDialogDescription>
    {

        public static WaitingDialogDescription Default
        {
            get
            {
                return new WaitingDialogDescription(Resource.DefaultWaitingTitle,Resource.DefaultWaitingHeaderText,String.Empty,Resource.DefaultWaitingDescription,true,false);
            }
        }

        public static WaitingDialogDescription DefaultWithBorder
        {
            get
            {
                return new WaitingDialogDescription(Resource.DefaultWaitingTitle, Resource.DefaultWaitingHeaderText, String.Empty, Resource.DefaultWaitingDescription, false, false);
            }
        }

        public string TitleText { get; set; }
        public string HeaderText { get; set; }
        public string MainText { get; set; }
        public string DescriptionText { get; set; }
        public bool BorderLess { get; set; }
        public bool TopMost { get; set; }
        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="source"></param>
        public WaitingDialogDescription(WaitingDialogDescription source)
        {
            if (source!=null)
            {
                TitleText = source.TitleText;
                HeaderText = source.HeaderText;
                MainText = source.MainText;
                DescriptionText = source.DescriptionText;
                BorderLess = source.BorderLess;
                TopMost = source.TopMost;
            }
        }
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="headerText"></param>
        /// <param name="mainText"></param>
        /// <param name="description"></param>
        /// <param name="borderLess"></param>
        /// <param name="topMost"></param>
        public WaitingDialogDescription(string titleText, string headerText, string mainText, string description, bool borderLess, bool topMost)
        {
            TitleText = titleText;
            HeaderText = headerText;
            MainText = mainText;
            DescriptionText = description;
            BorderLess = borderLess;
            TopMost = topMost;
        }

        /// <summary>
        ///                     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        ///                     An object to compare with this object.
        ///                 </param>
        public bool Equals(WaitingDialogDescription other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.TitleText, TitleText) && Equals(other.HeaderText, HeaderText) && Equals(other.MainText, MainText) && Equals(other.DescriptionText, DescriptionText);
        }

        /// <summary>
        ///                     Determines whether the specified <see cref="TDialogWindow:System.Object" /> is equal to the current <see cref="TDialogWindow:System.Object" />.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="TDialogWindow:System.Object" /> is equal to the current <see cref="TDialogWindow:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">
        ///                     The <see cref="TDialogWindow:System.Object" /> to compare with the current <see cref="TDialogWindow:System.Object" />. 
        ///                 </param>
        /// <exception cref="TDialogWindow:System.NullReferenceException">
        ///                     The <paramref name="obj" /> parameter is null.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (WaitingDialogDescription)) return false;
            return Equals((WaitingDialogDescription) obj);
        }

        /// <summary>
        ///                     Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        ///                     A hash code for the current <see cref="TDialogWindow:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (TitleText != null ? TitleText.GetHashCode() : 0);
                result = (result*397) ^ (HeaderText != null ? HeaderText.GetHashCode() : 0);
                result = (result*397) ^ (MainText != null ? MainText.GetHashCode() : 0);
                result = (result*397) ^ (DescriptionText != null ? DescriptionText.GetHashCode() : 0);
                return result;
            }
        }

        /// <summary>
        /// == operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(WaitingDialogDescription left, WaitingDialogDescription right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// != operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(WaitingDialogDescription left, WaitingDialogDescription right)
        {
            return !Equals(left, right);
        }

        public bool IsValid
        {
            get { return true; }
        }
    }
}