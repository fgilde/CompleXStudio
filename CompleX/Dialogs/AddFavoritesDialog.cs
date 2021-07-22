//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Drawing;

namespace CompleX.Dialogs
{
    /// <summary>
    /// Summary description for AddFavoritesDialog.
    /// </summary>
    public partial class AddFavoritesDialog : DevExpress.XtraEditors.XtraForm {
        public string LocationName {
            get { return textBox1.Text; }
        }

        public string LocationURL {
            get { return textBox2.Text; }
        }

        public AddFavoritesDialog(string name, string URL, Image img) : this(name, URL, img, true) {}
        public AddFavoritesDialog(string name, string URL, Image img, bool isAdd) {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            textBox1.Text = name;
            textBox2.Text = URL;
            pictureBox1.Image = img;
            if(!isAdd) {
                Text = "Edit Favorite Item";
                label1.Text = "Use the following edit fields to change this Favorite item description or the URL.";
            }
        }

		

        #region Windows Form Designer generated code
		
        #endregion

    }
}