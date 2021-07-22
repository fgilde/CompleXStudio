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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Types;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class MainEditForm : XtraForm
    {
        private IEnumerable<IDesignable> designers;
        private EditMode activeEditMode;

        private IEnumerable<ISourceEdit> possibleSourceEditors;
        internal IEnumerable<ISourceEdit> PossibleSourceEditors
        {
            get { return possibleSourceEditors; }
            set
            {
                possibleSourceEditors = value;
                UpdatePossibleEditorsMenu();
            }
        }


        public bool IsInSplitMode;
        public event EventHandler SelectionChanged;
        public event EventHandler ContentChanged;
        public FtpConnection FtpConnection { get; set; }

        internal event ComplexEvents.ModeChangeHandler ChangeMode;
        internal event ComplexEvents.EditorTypeChangeHandler ActiveEditorFocusChange;
        internal bool PromptSaveMessage;

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                UpdateTexts();
            }
        }

        private bool isSaved;

        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                CheckItemSaved.Checked = value;
                isSaved = value;
            }
        }

        public ISourceEdit EditControl { get; private set; }
        public IDesignable Designer { get; private set; }
        public IContentEdit ContentEditor
        {
            get
            {
                if (ActiveEditMode == EditMode.Designer && Designer != null)
                    return Designer;
                return EditControl;
            }
        }

        public EditMode ActiveEditMode
        {
           get
           {
               if(splitContainerMainHost.PanelVisibility == SplitPanelVisibility.Panel1)
                  return EditMode.Editor;
               if(splitContainerMainHost.PanelVisibility == SplitPanelVisibility.Panel2)
                  return EditMode.Designer;
               return activeEditMode;
           }   
        }

        public MainEditForm(ISourceEdit editControl, string fileName)
        {
            InitializeComponent();
            Tag = Guid.NewGuid();
            ((UserControl) editControl).Tag = Tag;
            IsSaved = true;
            PromptSaveMessage = true;
            splitContainerMainHost.PanelVisibility = SplitPanelVisibility.Panel1;

            EditControl = editControl;
            EditControl.OnFocus += ((sender, args) => { activeEditMode = EditMode.Editor;
                                                          InvokeActiveEditorChange(EditMode.Editor); });
            FileName = fileName;
            UpdateTexts();

            ((UserControl)EditControl).Dock = DockStyle.Fill;  
            if (File.Exists(fileName))
            {
                EditControl.LoadFromFile(fileName);
            }

            panelEditHost.Controls.Add((UserControl)EditControl);

            InitDesigners();
            EditControl.ContentChanged += EditControlOnTextChanged;
            EditControl.SelectionChanged += EditControlOnSelectionChanged;
        }

        private void EditControlOnSelectionChanged(object sender, EventArgs args)
        {
            EventHandler handler = SelectionChanged;
            if (handler != null) handler(sender, args);
        }

        internal void UpdateTexts()
        {
            Text = Path.GetFileName(FileName);
            labelFileName.Caption = FileName;
            var icon = File.Exists(FileName) ? ImageFunctions.GetFileIcon(FileName) : ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(FileName), false);
            if (icon != null)
                Icon = icon;  
        }

        private void InvokeActiveEditorChange(EditMode mode)
        {
            ComplexEvents.EditorTypeChangeHandler handler = ActiveEditorFocusChange;
            if (handler != null) handler(mode);
        }

        private void InvokeChangeMode(SwitchedEditMode newModi)
        {
            IsInSplitMode = newModi == SwitchedEditMode.Split;
            ComplexEvents.ModeChangeHandler handler = ChangeMode;
            if (handler != null) handler(newModi);
        }

        private void InitDesigners()
        {
            designers = ApplicationHost.GetModulesByFileName<IDesignable>(FileName);
            
            barButtonDesign.Visibility = designers.Count() > 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            barButtonSplit.Visibility = designers.Count() > 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
            if (designers.Count() == 1) // Wenn nur ein Designer möglich ist, kann dieser benutzt werden
            {
                barButtonSplit.Enabled = false;
                ActivateDesigner(designers.ToList()[0]);
            }else
            {
                foreach (var dsgn in designers)
                {
                    var item = new BarButtonItem(barManagerEditor, dsgn.ServiceName)
                                   {
                                       Name = "bi" + dsgn.ServiceName,
                                       Id = barManagerEditor.GetNewItemId()
                                   };
                    popupMenuDesigners.AddItem(item);
                    IDesignable designable = dsgn;
                    item.ItemClick += (sender, args) => { ActivateDesigner(designable); 
                        SwitchToDesign(); };
                }                  
            }
        }

        private void ActivateDesigner(IDesignable designer)
        {
            if (designer != null)
            {             
                popupMenuDesigners.ClearLinks();
                splitContainerMainHost.Panel2.Controls.Clear();
                if (Designer != null)
                {
                    Designer = null;
                    GC.Collect();
                }

                Designer = Activator.CreateInstance(designer.GetType()) as IDesignable;

                if (Designer is UserControl && Designer.Initialize()) // Da von dem designer eine neue instanz erstellt wird, muss initialize nochmal aufgerufen werden
                {
                    ((UserControl) Designer).Parent = splitContainerMainHost.Panel2;
                    ((UserControl) Designer).Dock = DockStyle.Fill;
                    Designer.Content = EditControl.Content;
                    Designer.ContentChanged += DesignerOnSourceCodeChanged;
                    Designer.SelectionChanged += EditControlOnSelectionChanged;
                    if (Designer.SupportSplitMode)
                    {
                        barButtonSplit.Enabled = true;
                    }
                    ((UserControl)Designer).Tag = Tag;
                    Designer.OnFocus += ((sender, args) => { activeEditMode = EditMode.Designer; InvokeActiveEditorChange(EditMode.Designer); });
                }
            }
        }

        private void ActivateSourceEditor(ISourceEdit sourceEdit)
        {
            if (sourceEdit != null)
            {
                var lastContent = EditControl.Content;

                splitContainerMainHost.Panel1.Controls.Clear();
                EditControl = null;
                GC.Collect();
                

                EditControl = Activator.CreateInstance(sourceEdit.GetType()) as ISourceEdit;

                if (EditControl is UserControl && EditControl.Initialize()) 
                {
                    ((UserControl)EditControl).Parent = splitContainerMainHost.Panel1;
                    ((UserControl)EditControl).Dock = DockStyle.Fill;
                    EditControl.Content = lastContent;
                    EditControl.ContentChanged += EditControlOnTextChanged;
                    EditControl.SelectionChanged += EditControlOnSelectionChanged;

                    ((UserControl)EditControl).Tag = Tag;
                    EditControl.OnFocus += ((sender, args) =>
                    {
                        activeEditMode = EditMode.Editor;
                        InvokeActiveEditorChange(EditMode.Editor);
                    });

                    if (!EditControl.ContextMenuIsHandled)
                        CompleX_Studio.Instance.barManager.SetPopupContextMenu((UserControl)EditControl, CompleX_Studio.Instance.defaultEditPopupMenu);
                    CompleX_Studio.Instance.UpdateDynamicMenuItems();
                    CompleX_Studio.Instance.EnableControls();
                    UpdatePossibleEditorsMenu();

                }
            }
        }

        private void EditControlOnTextChanged(object sender, EventArgs args)
        {
            IsSaved = false;
            if (!Text.StartsWith("*"))
                Text = @"* " + Text;

            if (ActiveEditMode == EditMode.Editor && Designer != null && Designer.SupportSplitMode)
            {
                Designer.Content = EditControl.Content;
            }
            if (ContentChanged != null)
                ContentChanged(EditControl, args);
        }

        private void DesignerOnSourceCodeChanged(object designable, EventArgs e)
        {
            if (designable is IDesignable)
            {
                if (activeEditMode == EditMode.Designer)
                {
                    if (((IDesignable)designable).SupportSplitMode)
                    {
                        EditControl.Content = ((IDesignable) designable).Content;
                    }

                    IsSaved = false;
                    if (!Text.StartsWith("*"))
                        Text = @"* " + Text;
                }
                if (ContentChanged != null)
                    ContentChanged(designable, e);
            }
        }

        public void SwitchToDesign()
        {
            if(Designer == null)
            {           
                if(popupMenuDesigners.ItemLinks.Count > 0)
                {
                    popupMenuDesigners.ShowPopup(Cursor.Position);
                }
                return;
            }
            ((UserControl) Designer).Focus();
            Designer.Content = EditControl.Content;
            splitContainerMainHost.PanelVisibility = SplitPanelVisibility.Panel2;
            barButtonDesign.Down = true;
            activeEditMode = EditMode.Designer;
            InvokeChangeMode(SwitchedEditMode.Designer);
        }


        public void SwitchToSplit()
        {
            if (Designer != null && Designer.SupportSplitMode)
            {
                if (activeEditMode == EditMode.Designer)
                    ((UserControl)Designer).Focus();
                else
                    ((UserControl)EditControl).Focus();

                splitContainerMainHost.PanelVisibility = SplitPanelVisibility.Both;
                barButtonSplit.Down = true;
                InvokeChangeMode(SwitchedEditMode.Split);
            }
        }

        public void SwitchToCode()
        {
            if(Designer != null)
                EditControl.Content = Designer.Content;

            ((UserControl)EditControl).Focus();
            splitContainerMainHost.PanelVisibility = SplitPanelVisibility.Panel1;
            barButtonCode.Down = true;
            activeEditMode = EditMode.Editor;
            InvokeChangeMode(SwitchedEditMode.Code);
        }



        private void BarButtonItem1ItemClick(object sender, ItemClickEventArgs e)
        {
            SwitchToCode();
        }


        private void BarButtonItem2ItemClick(object sender, ItemClickEventArgs e)
        {
            SwitchToSplit();
        }

        private void BarButtonDesignItemClick(object sender, ItemClickEventArgs e)
        {
            SwitchToDesign();
        }

        public static MainEditForm GetMainEditForm(IContentEdit edit)
        {
            var result = CompleX_Studio.Instance.MdiChildren.OfType<MainEditForm>().FirstOrDefault(
                form => form.Tag.Equals(((UserControl)edit).Tag) && 
                        (ReferenceEquals(form.EditControl, edit) || ReferenceEquals(form.Designer, edit)));
            return result;

        }

        private void MainEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved && PromptSaveMessage && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult answer =
                    MessageService.ShowConfirmation(
                        String.Format(Properties.Resources.ShowConfirmationCloseSave,
                                      Path.GetFileName(FileName)), Text,true);
                if (answer == DialogResult.Yes)
                    e.Cancel = !FileService.SaveFile(this);
                else if (answer == DialogResult.Cancel || answer == DialogResult.Abort)
                    e.Cancel = true;
            }
        }

        private void MainEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Wenn der MDI Manager nicht aktiviert ist, dann selber liste aktualisieren
            if (!CompleX_Studio.Instance.TabbedMdiActive)
                CompleX_Studio.Instance.UpdateOpenFileList();
        }

        private void UpdatePossibleEditorsMenu()
        {
            barViewMode.Visible = designers.Count() > 0 || possibleSourceEditors.Count() > 1;
            barSubItemChangeSourceEdit.Visibility = possibleSourceEditors.Count() > 1?BarItemVisibility.Always : BarItemVisibility.Never;
            barSubItemChangeSourceEdit.ClearLinks();
            int index = 0;
            foreach (ISourceEdit editor in possibleSourceEditors)
            {
                string caption = String.Format("{0} - {1}", editor.ServiceName, editor.GetVersion());
                var item = new BarButtonItem(barManagerEditor, caption)
                    {
                        Id = barManagerEditor.GetNewItemId(),
                        Name = "CS_SourceEdit" + index,
                    };

                ISourceEdit editor1 = editor;
                item.ButtonStyle = BarButtonStyle.Check;
                item.ItemClick += (sender, e) => ActivateSourceEditor(editor1);
                if (editor1.ID.Equals(EditControl.ID))
                    item.Down = true;

                barSubItemChangeSourceEdit.AddItem(item);
                index++;
             }
            
        }

    }
}