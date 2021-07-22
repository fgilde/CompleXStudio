//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ComboBox=System.Windows.Forms.ComboBox;

namespace CompleX.Helper
{
    internal static class Utilities
    {

        public static int AddImage(this ImageList imageList, Image image)
        {
            imageList.Images.Add(image);
            return imageList.Images.Count - 1;
        }

        public static int AddImage(this ImageList imageList, Icon image)
        {
            imageList.Images.Add(image);
            return imageList.Images.Count - 1;
        }

        /// <summary>
        /// Adds all Controls to a comboboxedit
        /// </summary>
        /// <param name="container"></param>
        /// <param name="cb"></param>
        internal static void AddControls(Control container, ComboBoxEdit cb)
        {
            foreach (object obj in container.Controls)
            {
                cb.Properties.Items.Add(obj);
                if (obj is Control) AddControls(obj as Control, cb);
            }
        }

        /// <summary>
        /// Ändert die Visibility der übergebenen controls
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="controls"></param>
        internal static void SetVisibility(bool visible, params Control[] controls)
        {
            foreach (var control in controls)
                control.Visible = visible;
        }

        /// <summary>
        /// Adds controls to combobox
        /// </summary>
        /// <param name="container"></param>
        /// <param name="cb"></param>
        internal static void AddControls(Control container, ComboBox cb)
        {
            foreach (object obj in container.Controls)
            {
                cb.Items.Add(obj);
                if (obj is Control) AddControls(obj as Control, cb);
            }
        }


    }
}
