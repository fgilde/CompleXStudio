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
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CompleX.Properties;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using DevExpress.XtraTreeList.Nodes;

namespace CompleX.Controls
{
    public partial class SettingsHostControl : UserControl
    {
        private bool inInit;

        private IEnumerable<ISettingsPage> allSettingsPages;

        /// <summary>
        /// Sets if it used as ProjectOptions
        /// </summary>
        public bool AreProjectOptions { get; private set; }

        /// <summary>
        /// List of all changed Pages
        /// </summary>
        public List<ISettingsPage> ChangedPages;

        /// <summary>
        /// Currently selected view
        /// </summary>
        public DataRowView SelectedNodeView
        {
            get
            {
                if (SelectedNode != null)
                    return treeListSettings.GetDataRecordByNode(SelectedNode) as DataRowView;
                return null;
            }
        }


        /// <summary>
        /// Currently selected node
        /// </summary>
        public TreeListNode SelectedNode
        {
            get
            {
                if (treeListSettings.Selection.Count > 0)
                    return treeListSettings.Selection[0];
                return null;
            }
        }

        /// <summary>
        /// Active Optionpage
        /// </summary>
        public ISettingsPage ActivePage { get; private set; }


        /// <summary>
        /// Applies all and ignores failed.
        /// </summary>
        public void ApplyAll()
        {
            foreach (ISettingsPage page in allSettingsPages)
            {
                page.OnOk();
            }
        }

        /// <summary>
        /// Tries to apply all changes.
        /// </summary>
        /// <returns></returns>
        public bool TryApplyAllChanges( )
        {
            IEnumerable<ISettingsPage> ns;
            return TryApplyAllChanges(out ns);
        }

        /// <summary>
        /// Applys changes of all changed pages and returns false if one of them not correctly saved
        /// </summary>
        /// <param name="notSaved">contains a list with all pages theyx could not saved</param>
        /// <returns>false if one of them not correctly saved</returns>
        public bool TryApplyAllChanges(out IEnumerable<ISettingsPage> notSaved )
        {
            bool result = true;
            notSaved = new List<ISettingsPage>();
            if (ChangedPages != null && ChangedPages.Count > 0)
            {
                foreach (ISettingsPage changedPage in ChangedPages)
                {
                    if(changedPage.ValidatePage().Result)
                    {
                        try
                        {
                            if(!changedPage.OnOk())
                            {
                                ((List<ISettingsPage>)notSaved).Add(changedPage);
                                result = false;
                            }
                        }
                        catch(Exception ex)
                        {
                            CompleX_Studio.MessageLog.LogException(ex);
                            ((List<ISettingsPage>)notSaved).Add(changedPage);
                            result = false;
                        }
                    }else
                    {
                        ((List<ISettingsPage>)notSaved).Add(changedPage);
                        result = false;
                    }
                } 
            }
            return result;
        }

        /// <summary>
        /// Cancel all
        /// </summary>
        /// <returns></returns>
        public void CancelAllChanges()
        {
            if (ChangedPages != null && ChangedPages.Count > 0)
            {
                foreach (ISettingsPage changedPage in ChangedPages)
                {
                    changedPage.OnCancel();
                }
            }
        }

        public ValidationResult ValidateActivePage()
        {
            var result = new ValidationResult(true, String.Empty);
            if (ActivePage != null)
                result = ActivePage.ValidatePage();
            return result;
        }

        public SettingsHostControl(bool asProjectOptions)
        {
            AreProjectOptions = asProjectOptions;
            InitializeComponent();
            inInit = true;
            ChangedPages = new List<ISettingsPage>();
            InitPages();
        }

        public SettingsHostControl():this(false)
        {}

        private void InitPages()
        {
            allSettingsPages = ApplicationHost.Host.GetServices<ISettingsPage>();

            if(AreProjectOptions)
                allSettingsPages = allSettingsPages.Where(page => page.AllowedAsProjectOptions);            

            if (allSettingsPages.Count() <= 0)
            {
                throw new Exception(Properties.Resources.Exception_EmptySettingPages);
            }
            foreach (ISettingsPage page in allSettingsPages)
            {
                if (page.Initialize())
                {
                    int imageIndex = 0;
                    if (page.Image != null)
                    {
                        imageListSettings.Images.Add(page.Image);
                        imageIndex = imageListSettings.Images.Count - 1;
                    }
                    dataSetSettingPages.DataTableSettingPages.AddDataTableSettingPagesRow(page.ID,
                                                                                          page.PageID,
                                                                                          page.ParentPageID,
                                                                                          page.PageTitle,
                                                                                          imageIndex,
                                                                                          page);
                }
            }

        }



        private void TreeListSettingsAfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            // Sete verlassen
            if (!inInit)
            {
                if (SelectedNode != null && SelectedNode["ISettingsPage"] != null &&
                    SelectedNode["ISettingsPage"] is ISettingsPage)
                {
                    var page = SelectedNode["ISettingsPage"] as ISettingsPage;
                    if (page != null)
                    {
                        page.OnActivated(false,AreProjectOptions);
                    }
                }
            }
            // Event wird leider nach anzeigen ausgelöst, darf aber bei der ersten aktiven page nicht deaktiviert werden
            inInit = false;
        }


        private void TreeListSettingsFocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //Seite betreten
            if (SelectedNode != null && SelectedNode["ISettingsPage"] != null && SelectedNode["ISettingsPage"] is ISettingsPage)
            {
                var page = SelectedNode["ISettingsPage"] as ISettingsPage;
                if (page != null)
                {
                    labelPageId.Text = Resources.PageId + ": " + page.PageID;
                    internalviewPanel.Text = page.PageTitle;
                    internalviewPanel.Controls.Clear();
                    internalviewPanel.Controls.Add(page.Control);
                    page.Control.Dock = DockStyle.Fill;
                    page.OnActivated(true, AreProjectOptions);
                    ActivePage = page;
                    if (!ChangedPages.Contains(page))
                        ChangedPages.Add(page);
                }
            }
        }

        private void PictureBox1MouseMove(object sender, MouseEventArgs e)
        {
            ShowLabel(labelExport);
        }

        private void PictureBox2MouseMove(object sender, MouseEventArgs e)
        {
            ShowLabel(labelImport);
        }

        private void PictureBox1Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog { DefaultExt = ".cssf", FileName = "CompleX Settings.csbin" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (Settings.Export(dlg.FileName))
                {
                    ShowLabel(labelSuccessFullyExported);
                    MessageService.ShowInformation(labelSuccessFullyExported.Text, labelExport.Text);
                }
                else
                {
                    ShowLabel(labelFailed);
                    MessageService.ShowError(labelFailed.Text, labelImport.Text);       
                }
            }
        }

        private void PictureBox2Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { DefaultExt = ".cssf", FileName = "CompleX Settings.csbin" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if(Settings.ImportSettings(dlg.FileName))
                {
                    ShowLabel(labelSuccesfullyImported);
                    MessageService.ShowInformation(labelSuccesfullyImported.Text,labelImport.Text);
                    if (ParentForm != null) ParentForm.Close();
                }else
                {
                    ShowLabel(labelFailed);
                    MessageService.ShowError(labelFailed.Text, labelImport.Text);                    
                }
            }
        }

        private void ShowLabel(Label label)
        {
            foreach (var control in panelHead.Controls)
            {
                if (control is Label)
                {
                    if (label != null)
                    {
                        var l = (Label) control;
                        l.Visible = label.Equals(l);
                    }
                    else
                        ((Label) control).Visible = false;
                }
            }
        }

        private void PictureBoxImportMouseLeave(object sender, EventArgs e)
        {
            ShowLabel(labelPageId);
        }

    }
}
