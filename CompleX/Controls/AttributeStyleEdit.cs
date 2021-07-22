//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Windows.Forms;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class AttributeStyleEdit : UserControl,IAttributeEdit
    {
        private readonly bool allowNewStyles;
        public AttributeStyleEdit(bool newStylesAllowed)
        {
            InitializeComponent();
            allowNewStyles = newStylesAllowed;
        }
        public TagAttribute Attribute { get; set; }
        public void Init()
        {
            textEdit1.Text = Attribute.AtrributeValue;
            pictureBox1.Image = pictureBox1.InitialImage;
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Attribute.AtrributeValue = textEdit1.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (allowNewStyles)
            {
                MessageBox.Show("STYLE");
            }
            // TODO Show StyleSelect Dialog
            MessageBox.Show("NOT FINISHED YET");
        }
    }
}
