using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompleX_Types;

namespace CompleX.Controls
{
    public partial class AttributeTextEdit : UserControl,IAttributeEdit
    {
        public AttributeTextEdit()
        {
            InitializeComponent();
        }
        public TagAttribute Attribute { get; set; }
        public void Init()
        {
             textEdit1.Text = Attribute.AtrributeValue;
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Attribute.AtrributeValue = textEdit1.Text;
        }
    }
}
