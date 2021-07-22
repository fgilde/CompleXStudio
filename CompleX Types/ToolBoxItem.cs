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
using System.Drawing;
using System.Linq;
using System.Text;
using CompleX_Library.Interfaces;

namespace CompleX_Types
{
    [Serializable]
    public class ToolBoxItem : IEquatable<ToolBoxItem>,ICloneable
    {

        public ToolBoxItem(string caption, object valueToInsert)
        {
            Id = Guid.NewGuid();
            Text = caption;
            Insert = valueToInsert;
            SupportedFileExtensions = new List<string>();
        }

        public ToolBoxItem(string caption, IInserter inserter, object parameter)
        {
            Id = Guid.NewGuid();
            Text = caption;
            Insert = parameter;
            InserterId = inserter.ID;
            SupportedFileExtensions = inserter.SupportedFileExtensions.ToList();
            Image = inserter.Image;
        }
        public ToolBoxItem(string caption, IInserter inserter):this(caption,inserter,null)
        {
        }

        public ToolBoxItem(IInserter inserter):this(inserter.ServiceName,inserter,null)
        {}

        /// <summary>
        /// Inserter if there is one
        /// </summary>     
        public Guid InserterId { get; set;}

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the supported file extensions.
        /// </summary>
        /// <value>The supported file extensions.</value>
        public List<string> SupportedFileExtensions { get; set; }

        /// <summary>
        /// Gets or sets the Object to insert.
        /// </summary>
        /// <value>The insert.</value>
        public object Insert { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public Image Image { get; set; }

        #region IEquatable

        public bool Equals(ToolBoxItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id) && Equals(other.SupportedFileExtensions, SupportedFileExtensions) && Equals(other.Insert, Insert) && Equals(other.Text, Text) && Equals(other.Category, Category) && Equals(other.Image, Image);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ToolBoxItem)) return false;
            return Equals((ToolBoxItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Id.GetHashCode();
                result = (result*397) ^ (SupportedFileExtensions != null ? SupportedFileExtensions.GetHashCode() : 0);
                result = (result*397) ^ (Insert != null ? Insert.GetHashCode() : 0);
                result = (result*397) ^ (Text != null ? Text.GetHashCode() : 0);
                result = (result*397) ^ (Category != null ? Category.GetHashCode() : 0);
                result = (result*397) ^ (Image != null ? Image.GetHashCode() : 0);
                return result;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static bool operator ==(ToolBoxItem left, ToolBoxItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ToolBoxItem left, ToolBoxItem right)
        {
            return !Equals(left, right);
        }

        #endregion

    }
}
