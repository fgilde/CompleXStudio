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
    public partial class AttributeFlagEdit : UserControl,IAttributeEdit
    {
        public AttributeFlagEdit()
        {
            InitializeComponent();
        }

        public TagAttribute Attribute { get; set; }

        public void Init()
        {
            checkEdit1.Checked = !String.IsNullOrEmpty(Attribute.AtrributeValue);
        }

        private void CheckEdit1CheckedChanged(object sender, EventArgs e)
        {
            Attribute.AtrributeValue = checkEdit1.Checked ? "True" : String.Empty;
        }
    }
}
