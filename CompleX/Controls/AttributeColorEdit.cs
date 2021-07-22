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
    public partial class AttributeColorEdit : UserControl, IAttributeEdit
    {
        public AttributeColorEdit()
        {
            InitializeComponent();
        }

        public TagAttribute Attribute { get; set; }

        public void Init()
        {
            if (!String.IsNullOrEmpty(Attribute.AtrributeValue))
            {
                try
                {
                    colorEdit.Color = ColorTranslator.FromHtml(Attribute.AtrributeValue);
                }
                catch
                {
                    // eat, because possible but not important 
                }
            }
        }

        private void colorEdit_EditValueChanged(object sender, EventArgs e)
        {
            Attribute.AtrributeValue = ColorTranslator.ToHtml(colorEdit.Color);
            colorEdit.ToolTip = Attribute.AtrributeValue;
        }
    }
}
