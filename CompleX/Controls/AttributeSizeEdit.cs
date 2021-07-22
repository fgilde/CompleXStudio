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
    public partial class AttributeSizeEdit : UserControl,IAttributeEdit
    {
        public AttributeSizeEdit()
        {
            InitializeComponent();
        }
        public TagAttribute Attribute { get; set; }
        public void Init()
        {
            string s = String.Empty;
            if(!String.IsNullOrEmpty( Attribute.AtrributeValue))
               s = Attribute.AtrributeValue.Replace("%", "").Replace("px", "");
            if(String.IsNullOrEmpty(s))
            {
                spinEdit1.Value = 0;
            }
            try
            {
                spinEdit1.Value = Convert.ToInt32(s);
            }
            catch
            {
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            Attribute.AtrributeValue = spinEdit1.Value + comboBoxEdit1.SelectedText;
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            UpdateValue();
        }
    }
}
