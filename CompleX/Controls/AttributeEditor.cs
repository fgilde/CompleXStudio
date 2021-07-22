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
    public partial class AttributeEditor : UserControl, IAttributeEdit
    {
        public AttributeEditor()
        {
            InitializeComponent();
        }

        public TagAttribute Attribute { get; set; }
        public void Init()
        {

            labelName.Text = Attribute.AtrributeName;
            labelName.Text = Char.ToUpper(labelName.Text[0]) + labelName.Text.Substring(1);

            IAttributeEdit AttributeValueEditor;

            switch (Attribute.AttributeType)
            {
                case AttributeType.Text:
                    AttributeValueEditor = new AttributeTextEdit();
                    break;
                case AttributeType.Enumerated:
                    AttributeValueEditor = new AttributeListEdit();
                    break;
                case AttributeType.Color:
                    AttributeValueEditor = new AttributeColorEdit();
                    break;
                case AttributeType.Relativepath:
                    AttributeValueEditor = new AttributePathEdit();
                    break;
                case AttributeType.CssStyle:
                    AttributeValueEditor = new AttributeStyleEdit(false);
                    break;
                case AttributeType.CssID:
                    AttributeValueEditor = new AttributeStyleEdit(false);
                    break;
                case AttributeType.Style:
                    AttributeValueEditor = new AttributeStyleEdit(true);
                    break;
                case AttributeType.Flag:
                    AttributeValueEditor = new AttributeFlagEdit();
                    break;
                case AttributeType.Size:
                    AttributeValueEditor = new AttributeSizeEdit();
                    break;
                default:
                    AttributeValueEditor = new AttributeTextEdit();
                    break;
            }
            AttributeValueEditor.Attribute = Attribute;
            AttributeValueEditor.Init();
            ((UserControl) AttributeValueEditor).Parent = panel1;
            ((UserControl)AttributeValueEditor).Dock = DockStyle.Fill;
        }
    }
}
