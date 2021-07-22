using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class SimpleBrowserDialog : XtraForm
    {
        public SimpleBrowserDialog(string url)
        {
            InitializeComponent();
            webBrowser.Navigate(url);
        }
    }
}
