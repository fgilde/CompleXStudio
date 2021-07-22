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
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CompleX;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using DOL.DHtml.DCssResolver;

namespace CompleX_ToolWindows
{
    [Export(typeof(IToolWindow))]
    public partial class CssExplorer : UserControl,IToolWindow
    {
        public CssExplorer()
        {
            InitializeComponent();
        }

        #region Implementation IHostedService

        public Guid ID
        {
            get { return new Guid("37916C41-7F4D-43CD-AB9B-2E28DA726B9F"); }
        }

        public string ServiceName
        {
            get { return "CSS Explorer"; }
        }

        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }
        }

        public bool Initialize()
        {
            ComplexEvents.Default.FileChanged += DefaultOnToolWindowChanged;
            return true;
        }

        private void DefaultOnToolWindowChanged(object sender, EventArgs e)
        {
            if(Visible)
               LoadCssTree();
        }

        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion

        #region Implementation IToolWindow

        public Icon Image
        {
            get { return null; }
        }

        #endregion


        private void LoadCssTree()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    cssTree.Nodes.Clear();
                    if (CompleX_Studio.CurrentContentEditor != null && CompleX_Studio.CurrentContentEditor.Content is string)
                    {
                        CompleX.Helper.HtmlHelper.CreateCssTree((string)CompleX_Studio.CurrentContentEditor.Content, cssTree);
                    }
                }));
            }
            catch (InvalidOperationException)
            {
            }            
        }

        private void CssExplorer_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                LoadCssTree();
        }

    }
}
