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
using System.Windows.Forms;

namespace CompleX_Types
{
    public class ImageListBox : ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public ImageListBox()
        {
            // Set owner draw mode

            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            ImageListBoxItem item;
            Rectangle bounds = e.Bounds;
            Size imageSize = _myImageList.ImageSize;
            try
            {
                item = (ImageListBoxItem)Items[e.Index];
                if (item.ImageIndex != -1)
                {
                    ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                                          bounds.Left + imageSize.Width, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                                          bounds.Left, bounds.Top);
                }
            }
            catch
            {
                if (e.Index != -1)
                {
                    e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                                          new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor),
                                          bounds.Left, bounds.Top);
                }
            }
            base.OnDrawItem(e);
        }
    }


    public class ImageListBoxItem : IEquatable<ImageListBoxItem>
    {
        private string _myText;
        private int _myImageIndex;
        // properties 

        public string Text
        {
            get { return _myText; }
            set { _myText = value; }
        }
        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }

        public object Tag { get; set; }

        //constructor

        public ImageListBoxItem(string text, int index)
        {
            _myText = text;
            _myImageIndex = index;
        }
        public ImageListBoxItem(string text) : this(text, -1) { }
        public ImageListBoxItem() : this("") { }
        public override string ToString()
        {
            return _myText;
        }

        public bool Equals(ImageListBoxItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Tag, Tag) && Equals(other._myText, _myText);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ImageListBoxItem)) return false;
            return Equals((ImageListBoxItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Tag != null ? Tag.GetHashCode() : 0);
                result = (result*397) ^ (_myText != null ? _myText.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(ImageListBoxItem left, ImageListBoxItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ImageListBoxItem left, ImageListBoxItem right)
        {
            return !Equals(left, right);
        }
    }


}