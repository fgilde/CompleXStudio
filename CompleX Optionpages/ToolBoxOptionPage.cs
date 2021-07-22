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
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompleX;
using CompleX.Controls;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Types;
using DevExpress.XtraNavBar;

namespace CompleX_Optionpages
{
    [Export(typeof(ISettingsPage))]
    public partial class ToolBoxOptionPage : BaseOptionPage
    {
        private readonly string tempFile;
        public ToolBoxOptionPage()
        {
            InitializeComponent();
            tempFile = Path.GetTempFileName()+DateTime.Now.Millisecond+"cXsX.tmp";
        }

        public override void OnCancel()
        {
            if (File.Exists(tempFile))
            {
                CompleX_Studio.ToolBox.LoadToolBox(tempFile);
                File.Delete(tempFile);
            }
            base.OnCancel();
        }

        public override bool OnOk()
        {
            if(File.Exists(tempFile))
               File.Delete(tempFile);
            return base.OnOk();
        }

        public override void OnActivated(bool activated, bool asProjectOptions)
        {
            if(activated)
            {
                if (!File.Exists(tempFile))
                {
                    CompleX_Studio.ToolBox.SaveToolBox(tempFile);
                }
                listBoxCategories.BeginUpdate();
                listBoxCategories.Items.Clear();
                foreach (string group in CompleX_Studio.ToolBox.Groups)
                {
                    listBoxCategories.Items.Add(group);
                }
                listBoxCategories.EndUpdate();
            }
        }


        public override Image Image
        {
            get { return Images.ToolBoxImage; }
        }

        public override bool AllowedAsProjectOptions
        {
            get
            {
                return false;
            }
        }

        public override string PageID
        {
            get { return "ToolBoxOptions"; }
        }

        public override string ParentPageID
        {
            get
            {
                return "ToolsOptions";
            }
        }

        public override string PageTitle
        {
            get
            {
                return "ToolBox";
            }
        }

        private void listBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string group = listBoxCategories.SelectedItem.ToString();
            IEnumerable<NavBarItem> items = CompleX_Studio.ToolBox.GetItems(group);
            listBoxItems.BeginUpdate();
            listBoxItems.Items.Clear();
            foreach (NavBarItem item in items)
            {
                listBoxItems.Items.Add(item);
            }
            listBoxItems.EndUpdate();
        }

        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedItem is NavBarItem)
            {
                var item = (NavBarItem)listBoxItems.SelectedItem;
                Control control;
                if (item.Tag is ToolBoxItem)
                {
                    var tbitem = (ToolBoxItem)item.Tag;
                    control = new ToolBoxItemEditControl(tbitem, false);
                    panelEditHost.AutoScroll = true;
                    panelEditHost.AutoScrollMinSize = new Size(0, control.MinimumSize.Height);
                    ((ToolBoxItemEditControl)control).ToolBoxItemChanged += ((o, args) =>
                                                      {
                                                          tbitem =
                                                              ((ToolBoxItemEditControl)
                                                               control).ToolBoxItem.Clone() as ToolBoxItem;
                                                          item.Caption = tbitem.Text;
                                                          item.SmallImage = ImageFunctions.ResizeImage(tbitem.Image, 16,
                                                                                                       16, true,
                                                                                                       Color.Transparent, 2.0,
                                                                                                       2.0);
                                                          item.LargeImage = ImageFunctions.ResizeImage(tbitem.Image, 32, 32, true, Color.Transparent, 2.0, 2.0);
                                                          item.Tag = tbitem.Clone();
                                                          CompleX_Studio.ToolBox.SaveToolBox();
                                                      });
                }
                else
                {
                    control = new PropertyGrid();
                    ((PropertyGrid)control).SelectedObject = item;
                    ((PropertyGrid) control).HelpVisible = false;
                    ((PropertyGrid) control).CommandsVisibleIfAvailable = false;
                    ((PropertyGrid) control).ToolbarVisible = false;
                }
                panelEditHost.Controls.Clear();
                panelEditHost.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

    }
}
