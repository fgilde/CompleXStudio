using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompleX_Library.Interfaces;
using CompleX.Helper;

namespace CompleX.Controls
{
    public partial class ServiceInfo : UserControl
    {      
        public ServiceInfo(IHostedService service)
        {
            InitializeComponent();
            labelName.Text += "  " + service.ServiceName;
            labelSupported.Text += " "+service.SupportedFileExtensions.AsString();
            labelId.Text += " " + service.ID;
            labelGetVersion.Text += " " + service.GetVersion();
        }
    }
}
