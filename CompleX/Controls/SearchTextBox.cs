using System;
using System.Drawing;
using CompleX.Properties;
using DevExpress.XtraEditors;

namespace CompleX.Controls
{
    public partial class SearchTextBox : TextEdit
    {
        private bool senderIntern;
        public SearchTextBox()
        {
            InitializeComponent();
            Text = Resources.Search;
        }

        public override sealed string Text
        {
            get
            {
                return base.Text == Resources.Search ? String.Empty : base.Text;
            }
            set
            {
                base.Text = value;
                UpdateColor();
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            senderIntern = true;
            if (base.Text == Resources.Search || base.Text == @"Search" || base.Text == @"Suchen")
                base.Text = String.Empty;
            senderIntern = false;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            senderIntern = true;
            if (base.Text == String.Empty)
            {
                base.Text = Resources.Search;
                ForeColor = Color.LightGray;
            }
            senderIntern = false;
            UpdateColor();
            base.OnLeave(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!senderIntern)
            {
                if (base.Text == String.Empty)
                    base.Text = Resources.Search;
                base.OnTextChanged(e);
            }
            UpdateColor();
        }

        private void UpdateColor()
        {
            ForeColor = base.Text == Resources.Search ? Color.LightGray : Color.Black;
        }
    }
}
