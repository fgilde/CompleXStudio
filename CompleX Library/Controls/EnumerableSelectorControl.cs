using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CompleX_Library.Controls
{
    public partial class EnumerableSelectorControl<T> : UserControl
    {
        private readonly IWindowsFormsEditorService service;
        private object value;
        private readonly IEnumerable<T> values;
        private readonly Func<T, string> labeler;
        private readonly List<ValueEntry> entries = new List<ValueEntry>();

        public EnumerableSelectorControl(IWindowsFormsEditorService service, object value, IEnumerable<T> values, Func<T,string> labeler)
        {
            if (values == null)
                throw new ArgumentNullException(@"values");

            this.service = service;
            this.value = value;
            this.values = values;
            this.labeler = labeler ?? (entry => entry.ToString());

            InitializeComponent();
            InitializeListBox();
        }

        public object Value
        {
            [DebuggerStepThrough]
            get { return value; }
        }

        private void InitializeListBox()
        {
            int selectedIndex = -1;
            entries.Clear();

            foreach (var enumValue in values)
            {
                int index = listBox.Items.Add(new ValueEntry(enumValue,labeler));
                if (Equals(enumValue,value))
                    selectedIndex = index;
            }
            listBox.SelectedIndex = selectedIndex;
        }



        private void ListBoxDoubleClick(object sender, EventArgs args)
        {
            service.CloseDropDown();
        }

        private void ListBoxSelectedIndexChanged(object sender, EventArgs args)
        {
            var enumEntry = listBox.SelectedItem as ValueEntry;
            if (enumEntry != null)
                value = enumEntry.Value;
        }

        private class ValueEntry
        {
            private readonly Func<T, string> labeler;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            public ValueEntry(object value,Func<T,string> labeler)
            {
                this.labeler = labeler;
                Value = value;
            }

            public object Value { get; private set; }

            public override string ToString()
            {
                return labeler((T)Value);
            }

        }
    }
}