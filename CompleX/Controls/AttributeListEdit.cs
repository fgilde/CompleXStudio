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
using System.Windows.Forms;
using CompleX_Types;

namespace CompleX.Controls
{

    public partial class AttributeListEdit : UserControl,IAttributeEdit
    {
        private bool fromCombo;
        public AttributeListEdit()
        {
            InitializeComponent();
        }
        public TagAttribute Attribute { get; set; }
        public void Init()
        {
            comboBoxEdit1.Text = Attribute.AtrributeValue;
            if (Attribute.AttribOptions == null || Attribute.AttribOptions.Count <= 0) return;
            foreach (KeyValuePair<string, string> attribOption in Attribute.AttribOptions)
                comboBoxEdit1.Properties.Items.Add(new KeyValue<string,string>(attribOption.Key, attribOption.Value));
        }

        private void ComboBoxEdit1SelectedIndexChanged(object sender, EventArgs e)
        {
            fromCombo = true;
            if (comboBoxEdit1.SelectedItem is KeyValue<string, string>)
            {
                Attribute.AtrributeValue = ((KeyValue<string, string>) comboBoxEdit1.SelectedItem).Key;
                comboBoxEdit1.Text = Attribute.AtrributeValue;
            }
            fromCombo = false;
        }

        private void ComboBoxEdit1TextChanged(object sender, EventArgs e)
        {
            if (!fromCombo)
                Attribute.AtrributeValue = comboBoxEdit1.Text;
        }

    }

    public class KeyValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }

}
