//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================

#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using CompleX;
using CompleX.Services;
using CompleX_Library;
using CompleX_Library.Interfaces;

#endregion

namespace CompleX_SourceEditors.XSLEditor
{
    [Export(typeof(ISourceEdit))]
    public partial class XSLEditor : UserControl, ISourceEdit,IEditFeatures,IMenuHandler
    {
        #region Declarations
        private bool showToolStrip = true;
        private int lastCharPrintedPos;
        #endregion

        public event ActionExecuteHandler ActionBeforeExecute;
        public event ActionExecuteHandler ActionAfterExecute;
        
        #region Constructors
        public XSLEditor()
        {
            InitializeComponent();

            xslTextBox.EnableIntellisense = true;
            
            xslTextBox.ContentChanged += XslTextBoxContentChanged;
            xslTextBox.KeyDown += XslTextBoxKeyDown;
            xslTextBox.MouseUp += XslTextBoxMouseUp;
            xslTextBox.SelectionChanged += XslTextBoxSelectionChanged;
            xslTextBox.Enter += xslTextBox_Enter;
            
            // toolbar and menu handlers

            enableISToolStripMenuItem.Click += OnToggleEnableIntellisense;
            findToolStripButton.Click += OnFindReplace;
            pageSetupToolStripButton.Click += OnPageSetup;
            printToolStripButton.Click += OnPrint;
            printPreviewToolStripButton.Click += OnPrintPreview;
            wordWrapToolStripMenuItem.Click += OnToggleWordwrap;            
            validateToolStripMenuItem.Click += OnValidateXsl;

        }        
        #endregion

        //*********************************************************************
        //* PROPERTIES
        //*********************************************************************

        #region Public Appearance Properties
        [Browsable(true),
         Category("Appearance"),
         Description("Gets or sets whether default toolstrip will be shown")]
        public bool ShowToolStrip
        {
            get { return showToolStrip; }
            set
            {
                
                showToolStrip = value;
                xslToolStrip.Visible = value;
            }
        }

        #endregion

        #region Public Properties


        [Browsable(false)]
        public string Filename
        {
            get { return xslTextBox.Filename; }
        }

        public override string Text
        {
            get { return xslTextBox.Text; }
            set { xslTextBox.Text = value; }
        }

        public XSLRichTextBox XslTextBox
        {
            get { return xslTextBox; }
        }        
                      
        
        /// <summary>
        /// True to generate transformation debug information; otherwise false. Setting this to 
        /// true enables you to debug the style sheet with the Microsoft Visual Studio Debugger
        /// </summary>
        [
            Browsable(true),
            Category("Behavior"),
            DefaultValue(true),
            Description("True to generate transformation debug information; otherwise false. Setting this to "
                        + "true enables you to debug the style sheet with the Microsoft Visual Studio Debugger")
        ]
     
        #endregion

        //*********************************************************************
        //* METHODS
        //*********************************************************************

        #region Virtual Methods
        protected virtual void OnCopy(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.Copy)) return;
            xslTextBox.Copy();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.Copy);
        }

        protected virtual void OnCut(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.Cut)) return;
            xslTextBox.Cut();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.Cut);
        }

        protected virtual void OnFindReplace(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.FindReplace)) return;

            var findForm = new FindReplaceDialog(xslTextBox);
            findForm.Show();

            OnActionAfterExecute(ActionExecuteEventArgs.Actions.FindReplace);
        }

        protected virtual void OnNew(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.New)) return;
            xslTextBox.NewFile();
            xslTextBox.Text = Properties.Resources.NewXSLTemplate;
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.New);
        }

        protected virtual void OnPageSetup(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.PageSetup)) return;
            PageSetup();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.PageSetup);
        }

        protected virtual void OnPaste(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.Paste)) return;
            xslTextBox.Paste();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.Paste);
        }

        protected virtual void OnPrint(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.Print)) return;
            Print();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.Print);
        }

        protected virtual void OnPrintPreview(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.PrintPreview)) return;
            PrintPreview();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.PrintPreview);
        }



        protected virtual void OnToggleEnableIntellisense(object sender, EventArgs e)
        {
            enableISToolStripMenuItem.Checked = !enableISToolStripMenuItem.Checked;
            xslTextBox.EnableIntellisense = enableISToolStripMenuItem.Checked;
        }

        protected virtual void OnToggleToolStrip(object sender, EventArgs e)
        {
            ShowToolStrip = !ShowToolStrip;
        }
            

        protected virtual void OnValidateXsl(object sender, EventArgs e)
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.ValidateXsl)) return;
            ValidateXsl();
            OnActionAfterExecute(ActionExecuteEventArgs.Actions.ValidateXsl);
        }

        protected virtual void OnToggleWordwrap(object sender, EventArgs e)
        {
            xslTextBox.WordWrap = !xslTextBox.WordWrap;
            wordWrapToolStripMenuItem.Checked = xslTextBox.WordWrap;
            wordWrapToolStripMenuItem.Checked = xslTextBox.WordWrap;
        }

        protected virtual void TransformDebugClick()
        {
            if (!OnActionBeforeExecute(ActionExecuteEventArgs.Actions.TransformDebug)) return;

            OnActionAfterExecute(ActionExecuteEventArgs.Actions.TransformDebug);
        }
        #endregion

        #region Public Virtual Methods

        public virtual void PageSetup()
        {
            pageSetupDialog.ShowDialog();
        }

        public virtual void Print()
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        public virtual void PrintPreview()
        {
            printPreviewDialog.ShowDialog();
        }


        public virtual void Transform()
        {
            xslTextBox.ShowTransformDialog();
        }

        public virtual void ValidateXsl()
        {
            try
            {
                new XPathDocument(new StringReader(xslTextBox.Text));
                OutputService.WriteLine("Syntax valid");
            }
            catch (XmlException xmlEx)
            {
                CompleX_Studio.MessageLog.LogEntry(new LogEntry(DateTime.Now, LogType.Error, xmlEx.Message, Filename, xmlEx.LineNumber, string.Empty));
            }
        }
        #endregion Public Virtual Methods
        

        #region Private Methods
        private bool OnActionBeforeExecute(ActionExecuteEventArgs.Actions action)
        {
            var e = new ActionExecuteEventArgs(action);

            if (ActionBeforeExecute != null)
            {
                ActionBeforeExecute(this, e);
            }

            return (!e.Cancel);
        }

        private void OnActionAfterExecute(ActionExecuteEventArgs.Actions action)
        {
            var e = new ActionExecuteEventArgs(action);

            if (ActionAfterExecute != null)
            {
                ActionAfterExecute(this, e);
            }
        }

        private void UpdateCursorPos()
        {
            lineToolStripStatusLabel.Text = "Line: " + xslTextBox.LineNumber;
            columnToolStripStatusLabel.Text = "Col: " + xslTextBox.ColumnNumber;
        }
        #endregion

        //*********************************************************************
        //* EVENTS
        //*********************************************************************

        #region ToolStrip Events
        private void xslDebugToolStripButton_Click(object sender, EventArgs e)
        {
            TransformDebugClick();
        }            
        #endregion

        #region xslTextBox events
        private void XslTextBoxContentChanged()
        {
            if (ContentChanged != null)
                ContentChanged(xslTextBox, new EventArgs());
            UpdateCursorPos();        
        }

        private void XslTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            // shortcut keys for context menu strip will only fire when context menu is shown
            if (e.KeyCode == Keys.F && e.Control)
            {
                OnFindReplace(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.N && e.Control)
            {
                OnNew(this, new EventArgs());
            }          
            else if (e.KeyCode == Keys.P && e.Control)
            {
                OnPrint(this, new EventArgs());
            }
            if (CursorPositionChanged != null)
                CursorPositionChanged(sender, e);
        }
       

        private void XslTextBoxMouseUp(object sender, MouseEventArgs e)
        {
            UpdateCursorPos();
        }

        private void XslTextBoxSelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(sender, e);
            UpdateCursorPos();
            
        }
        #endregion xslTextBox events
    
                      
        #region Printing Events
        private void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            lastCharPrintedPos = 0;
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            lastCharPrintedPos = xslTextBox.Print(lastCharPrintedPos, xslTextBox.TextLength, e);

            // Check for more pages
            e.HasMorePages = lastCharPrintedPos < xslTextBox.TextLength;
        }

        private void printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            //xslTextBox.WordWrap = false;
        }
        #endregion

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("AE0637ED-FB08-4500-8492-652B7F44A19C");}
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "CompleX XSL Editor"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] {".xsl", ".xslt"}; }
        }

        /// <summary>
        /// Function is called if service is added 
        /// return true if everything ok otherwise service would not loaded
        /// </summary>
        public bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Function is called before service will added 
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion

        #region Implementation of IContentEdit

        public event EventHandler OnFocus;
        public event EventHandler CursorPositionChanged;
        public event EventHandler ContentChanged;
        public event EventHandler SelectionChanged;
        
        public bool SaveToFile(string fileName)
        {
            xslTextBox.SaveFile(fileName, RichTextBoxStreamType.PlainText);
            xslTextBox.Refresh();
            return true;
        }

        public object SelectedContent
        {
            get { return xslTextBox.SelectedText; }
            set { xslTextBox.SelectedText = value.ToString(); }
        }

        public object Content
        {
            get { return xslTextBox.Text; }
            set { xslTextBox.Text = value.ToString(); }
        }

        public void Insert(object obj)
        {
            xslTextBox.SelectedText = obj.ToString();
        }

        #endregion

        #region Implementation of ISourceEdit

        /// <summary>
        /// Load file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool LoadFromFile(string fileName)
        {
            xslTextBox.LoadFile(fileName,RichTextBoxStreamType.PlainText);
            return true;
        }

        /// <summary>
        /// Set if contextmenu is handled manual
        /// </summary>
        public bool ContextMenuIsHandled
        {
            get { return false; }
        }

        #endregion

        private void xslTextBox_Enter(object sender, EventArgs e)
        {
            if (OnFocus != null)
                OnFocus(sender, e);
        }

        #region Implementation of IEditFeatures

        /// <summary>
        /// List of custom actions. Return Emptylist if you dont have some
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ItemAction> GetCustomActions()
        {
            return Enumerable.Empty<ItemAction>();
        }

        /// <summary>
        /// Cut current selection to clipboard
        /// </summary>
        public void CutToClipboard()
        {
            xslTextBox.Cut();
        }

        /// <summary>
        /// Copy to Clipboard
        /// </summary>
        public void CopyToClipboard()
        {
            xslTextBox.Copy();
        }

        /// <summary>
        /// Paste from clipboard
        /// </summary>
        public void PasteFromClipboard()
        {
           xslTextBox.Paste();
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            xslTextBox.Undo();
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            xslTextBox.Redo();
        }

        /// <summary>
        /// Select All
        /// </summary>
        public void SelectAll()
        {
            xslTextBox.SelectAll();
        }

        /// <summary>
        /// Returns if editor can cut at this moment
        /// </summary>
        public bool CanCut
        {
            get { return !String.IsNullOrEmpty(xslTextBox.SelectedText); }
        }

        /// <summary>
        /// Can Copy at this moment
        /// </summary>
        public bool CanCopy
        {
            get { return !String.IsNullOrEmpty(xslTextBox.SelectedText); }
        }

        /// <summary>
        /// Can Paste
        /// </summary>
        public bool CanPaste
        {
            get { return true; }
        }

        /// <summary>
        /// Can undo
        /// </summary>
        public bool CanUndo
        {
            get { return xslTextBox.CanUndo; }
        }

        /// <summary>
        /// Can Redo
        /// </summary>
        public bool CanRedo
        {
            get { return xslTextBox.CanRedo; }
        }

        #endregion


        #region Implementation of IMenuHandler

        /// <summary>
        /// should return the menu strip to merge.
        /// </summary>
        /// <value>The menu.</value>
        public MenuStrip Menu
        {
            get { return menuStrip1; }
        }

        #endregion

        private void transformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transform();
        }
    }

    public delegate void ActionExecuteHandler(object sender, ActionExecuteEventArgs e);

    public class ActionExecuteEventArgs : EventArgs
    {
        public enum Actions
        {
            Copy, Cut, FindReplace, New, Open, PageSetup, Paste, Print, PrintPreview, 
            Save, SaveAs, Transform, TransformDebug, ValidateXsl
        }

        public bool Cancel { get; set; }

        public Actions Action { get; set; }

        public ActionExecuteEventArgs(Actions action)
        {
            Action = action;
        }
    }
}