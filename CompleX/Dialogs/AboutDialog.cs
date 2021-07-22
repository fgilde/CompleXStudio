//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.ServiceModel;
using CompleX_Library.Interfaces;
using CompleX_Settings.Constants;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    partial class AboutDialog : XtraForm
    {
        private SelectPluginControl<IHostedService>  plugincontrol;
        public AboutDialog()
        {
            InitializeComponent();
            Init();
            // this.textBoxDescription.Text = AssemblyDescription;
        }

        private void Init()
        {
            Text = String.Format("About {0}", AssemblyTitle);
            copyrightLabel.Text = String.Format(copyrightLabel.Text, DateTime.Today.Year, AssemblyCompany);
            labelName.Text = AssemblyProduct;
            labelProductName.Text = AssemblyProduct + " "+Const.CodeName+ " "+Const.State;
            labelVersion.Text = String.Format("Version {1} {0}", AssemblyVersion, Const.State);
            labelCopyright.Text = AssemblyCopyright;
            labelFileVersion.Text = AssemblyFileVersion;
            labelCompanyName.Text = AssemblyCompany;
            plugincontrol = new SelectPluginControl<IHostedService>();
            groupControl.Controls.Add(plugincontrol);
            plugincontrol.Dock = DockStyle.Fill;

            plugincontrol.ItemSelectionChanged += PlugincontrolOnItemSelectionChanged;

        }

        private void PlugincontrolOnItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs args)
        {
            IHostedService service = plugincontrol.SelectedService;
            buttonUpdate.Enabled = (service != null && service is IUpdateableService);    
            buttonAbout.Enabled = (service != null);    
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyFileVersion
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state => System.Diagnostics.Process.Start(linkLabel1.Text));
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            IHostedService service = plugincontrol.SelectedService;
            if(service != null)
            {
                if(service is IServiceInformation)
                    ((IServiceInformation)service).ShowInformation();
                else
                {
                    using(var control = new ServiceInfo(service))
                    {
                        var dlg = BaseDialogHelper.CreateBaseDialog(control);
                        dlg.OkBtn.Visible = false;
                        dlg.CancelBtn.Text = Properties.Resources.Close;
                        dlg.ShowDialog();
                    }
                }
            }
        }

        private void buttonSysInfo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("MSINFO32");
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            IHostedService service = plugincontrol.SelectedService;
            if(service != null && service is IUpdateableService)
            {
               ApplicationHost.UpdateService((IUpdateableService)service); 
            }  
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }




    }
}
