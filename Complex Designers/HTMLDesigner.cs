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
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Helper;
using CompleX.ServiceModel;
using CompleX_Inserters;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Types;
using CompleX_Types.Extensions;
using mshtml;

namespace Complex_Designers
{
    [Export(typeof(IDesignable))]
    public partial class HTMLDesigner : UserControl, IDesignable, IEditFeatures, IUpdateableService, IMenuHandler
    {

        private IHTMLDocument2 htmlEditDocument;
        private readonly Guid id;

        public HTMLDesigner()
        {
            InitializeComponent();
            id = Guid.NewGuid();
        }

        #region Public Methods

        public void ExecCommand(string action)
        {
            ExecCommand(action, false, null);
        }

        public void ExecCommand(string action, bool showUi)
        {
            ExecCommand(action, showUi, null);
        }

        public void ExecCommand(string action, bool showUi, object value)
        {
            if (HtmlEditor.Document != null)
            {
                HtmlEditor.Document.ExecCommand(action, showUi, value);
            }
            else
            {
                htmlEditDocument.execCommand(action, showUi, value);
            }
            InvokeContentCodeChanged(this);

        }

        public string StripHtml()
        {
            return StripHtml(Content.ToString());
        }

        public string StripHtml(string source)
        {
            try
            {
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                string result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                                                      @"<( )*head([^>])*>", "<head>",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"(<( )*(/)( )*head( )*>)", "</head>",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      "(<head>).*(</head>)", string.Empty,
                                                                      RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                                                      @"<( )*script([^>])*>", "<script>",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"(<( )*(/)( )*script( )*>)", "</script>",
                                                                      RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"(<script>).*(</script>)", string.Empty,
                                                                      RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                                                      @"<( )*style([^>])*>", "<style>",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"(<( )*(/)( )*style( )*>)", "</style>",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      "(<style>).*(</style>)", string.Empty,
                                                                      RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                                                                      @"<( )*td([^>])*>", "\t",
                                                                      RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                                                                      @"<( )*br( )*>", "\r",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"<( )*li( )*>", "\r",
                                                                      RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                                                                      @"<( )*div([^>])*>", "\r\r",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"<( )*tr([^>])*>", "\r\r",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"<( )*p([^>])*>", "\r\r",
                                                                      RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                                                                      @"<[^>]*>", string.Empty,
                                                                      RegexOptions.IgnoreCase);

                // replace special characters:
                result = Regex.Replace(result,
                                                                      @" ", " ",
                                                                      RegexOptions.IgnoreCase);

                result = Regex.Replace(result,
                                                                      @"&bull;", " * ",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&lsaquo;", "<",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&rsaquo;", ">",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&trade;", "(tm)",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&frasl;", "/",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&lt;", "<",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&gt;", ">",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&copy;", "(c)",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      @"&reg;", "(r)",
                                                                      RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                                                                      @"&(.{2,6});", string.Empty,
                                                                      RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                                                                      "(\r)( )+(\r)", "\r\r",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      "(\t)( )+(\t)", "\t\t",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      "(\t)( )+(\r)", "\t\r",
                                                                      RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                                                      "(\r)( )+(\t)", "\r\t",
                                                                      RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                                                                      "(\r)(\t)+(\r)", "\r\r",
                                                                      RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                                                                      "(\r)(\t)+", "\r\t",
                                                                      RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                MessageBox.Show(@"Error");
                return source;
            }
        }

        #endregion

        #region UserControl Delegating Events

        public event OnException ExceptionOccurred;

        private void InvokeExceptionOccurred(Exception e)
        {
            OnException exception = ExceptionOccurred;
            if (exception != null) exception(this, e);
        }


        public event EventHandler ContentChanged;

        private void InvokeContentCodeChanged(IDesignable designer)
        {
            EventHandler changed = ContentChanged;
            if (changed != null) changed(designer, new EventArgs());
        }

        public event EventHandler CursorPositionChanged;

        private void InvokeCursorPositionChanged(EventArgs e)
        {
            EventHandler changed = CursorPositionChanged;
            if (changed != null) changed(this, e);
        }
       


        public event EventHandler OnFocus;

        private void InvokeOnFocus(EventArgs e)
        {
            EventHandler handler = OnFocus;
            if (handler != null) handler(this, e);
        }
        
        
        
        public event EventHandler SelectionChanged;

        private void InvokeSelectionChanged(EventArgs e)
        {
            string htmlText = GetSelectedHtmlText();
            Tag tag = htmlText.ToTag();
            editTagToolStripMenuItem.Enabled = (tag != null);

            EventHandler handler = SelectionChanged;
            if (handler != null) handler(this, e);
            popupEditTag.Text = GetSelectedHtmlText();
        }

        #endregion

        #region Interface IDesignable


        public string Author
        {
            get { return "nksoft"; }
        }

        public string PluginName
        {
            get { return "CompleX HTML Designer 1.1"; }
        }

        private object GetSelectedContent()
        {
            string htmlText = GetSelectedHtmlText();

            Tag tag = htmlText.ToTag();
            if (tag != null)
                return tag.ToCustomClass();

            return htmlText; 
        }

        private string GetSelectedHtmlText()
        {
            string htmlText = String.Empty;
            if(htmlEditDocument.selection.type == "Text" || htmlEditDocument.selection.type == "None")
            {
                var range = (IHTMLTxtRange) htmlEditDocument.selection.createRange();
                htmlText = range.htmlText;
            }
            else if (htmlEditDocument.selection.type == "Control")
            {
                var range = (IHTMLControlRange)htmlEditDocument.selection.createRange();
                htmlText = range.item(0).outerHTML;
            }
            return htmlText;
        }

        public object SelectedContent
        {    
            get
            {
                return GetSelectedContent();             
            }
            set
            {
                Insert(value);
            }
        }


        public object Content
        {
            get
            {
                var res = String.Empty;
                if (htmlEditDocument.body != null)
                    res = htmlEditDocument.body.innerHTML;
                return String.Format(Resource.HtmlBody,res);
                
            }
            set
            {
                if (htmlEditDocument.body != null && value != null)
                {
                    htmlEditDocument.body.innerHTML = value.ToString();
                }
            }
        }


        public void Insert(object obj)
        {
            if (obj != null)
            {
                string source = obj.ToString();

                if(obj is CustomClass)
                {
                    var tag = CompleX_Types.Tag.CreateFromCustomClass((CustomClass)obj);
                    if (tag != null)
                        source = tag.ToString();
                }

                htmlEditDocument.selection.clear();
                var range = htmlEditDocument.selection.createRange() as IHTMLTxtRange;
                if (range != null)
                {
                    source = PlaceHolder.ReplacePlaceHolder(source);
                    range.pasteHTML(source);                    
                    //TODO: Select (or focus) the inserted code
                }
                InvokeContentCodeChanged(this);
            }
        }


        public bool SaveToFile(string filename)
        {
            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var streamWriter = new StreamWriter(stream);
            streamWriter.Write(Content);
            streamWriter.Close();
            return File.Exists(filename);
        }

        public bool SupportSplitMode
        {
            get { return true; }
        }

        public IEnumerable<string> SupportedFileExtensions
        {
            get { return FilenameExtensions.ExtensionsWebFiles; }
        }


        #endregion

        #region Interface IHostedService

        public Guid ID
        {
            get { return id; }
        }

        public Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public string ServiceName
        {
            get { return PluginName; }
        }

        public bool Initialize()
        {

            try
            {
                if (HtmlEditor.Document != null)
                {
                    htmlEditDocument = (IHTMLDocument2) HtmlEditor.Document.DomDocument;
                    htmlEditDocument.designMode = "On";

                    HtmlEditor.Document.Write("-");

                    if (HtmlEditor.Document.Body != null)
                    {
                        HtmlEditor.Document.Body.MouseUp += BodyMouseUp;
                        HtmlEditor.Document.Body.KeyPress += BodyKeyPress;
                        HtmlEditor.Document.Body.KeyUp += BodyKeyPress;
                        HtmlEditor.Document.Body.Click += BodyClick;
                        HtmlEditor.Document.Body.Focusing += BodyOnFocusing;                          
                    }

                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                InvokeExceptionOccurred(e);
                return false;
            }
        }

        void BodyMouseUp(object sender, HtmlElementEventArgs e)
        {
            InvokeSelectionChanged(e);
        }

        private void BodyOnFocusing(object sender, HtmlElementEventArgs args)
        {
            InvokeOnFocus(args);
        }

        private void BodyClick(object sender, HtmlElementEventArgs e)
        {
            InvokeCursorPositionChanged(e);
            InvokeSelectionChanged(e);
        }

        private void BodyKeyPress(object sender, HtmlElementEventArgs e)
        {
            InvokeContentCodeChanged(this);
            InvokeSelectionChanged(e);
        }

        #endregion

        #region Interface IEquatable

        public bool Equals(IHostedService other)
        {
            return ServiceName.Equals(other.ServiceName);
        }

        #endregion

        #region Interface IEditFeatures

        public IEnumerable<ItemAction> GetCustomActions()
        {
            var result = new List<ItemAction>
                             {
                                 new ItemAction("Bold", () => ExecCommand("Bold"))
                                     {BeginGroup = true, ShortCut = Shortcut.CtrlB, Image = null},

                                 new ItemAction("InsertUnorderedList", () => ExecCommand("InsertUnorderedList"))
                                     {BeginGroup = false, ShortCut = Shortcut.None, Image = null},

                                 new ItemAction("InsertOrderedList", () => ExecCommand("InsertOrderedList"))
                                     {BeginGroup = false, ShortCut = Shortcut.None, Image = null},

                             };
            return result;
        }

        public void CutToClipboard()
        {
            ExecCommand("Cut");
        }

        public void CopyToClipboard()
        {
            ExecCommand("Copy");
        }

        public void PasteFromClipboard()
        {
            ExecCommand("Paste");
        }

        public void Undo()
        {
            ExecCommand("Undo");
        }

        public void Redo()
        {
            ExecCommand("Redo");
        }

        public void SelectAll()
        {
            ExecCommand("SelectAll");
        }

        public bool CanCut
        {
            get { return true; }
        }

        public bool CanCopy
        {
            get { return true; }
        }

        public bool CanPaste
        {
            get { return true; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public bool CanRedo
        {
            get { return true; }
        }

        #endregion

        #region Implementation of IUpdateableService

        /// <summary>
        /// Should return the Url to a zip file 
        /// <example>http://www.mysserver/myaddin.zip</example>
        /// </summary>
        /// <value>The URL.</value>
        public string Url
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Should check if a new verison is available
        /// </summary>
        /// <returns></returns>
        public bool NewVersionAvailable()
        {
            return true;
        }

        #endregion

        #region Implementation of IMenuHandler

        /// <summary>
        /// should return the menu strip to merge.
        /// </summary>
        /// <value>The menu.</value>
        public MenuStrip Menu
        {
            get { return menuStrip; }
        }

        #endregion



        private void EditSelectedTagClick(object sender, EventArgs e)
        {
            string htmlText = GetSelectedHtmlText();
            Tag tag = htmlText.ToTag();
            if (tag != null)
            {
                var inserter = ApplicationHost.Host.GetService<TagInserter>() ?? new TagInserter();
                var insert = inserter.GetObjectToInsert(new InsertParameter(tag));
                if(insert != null)
                {
                    SelectedContent = insert;
                }
            }
        }


        private void urlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecCommand("CreateLink",true);
        }

        private void popupOptions_Properties_Click(object sender, EventArgs e)
        {
            string htmlText = GetSelectedHtmlText();
            Tag tag = htmlText.ToTag();
            popupContainerEditTag.Controls.Clear();
            if (tag != null)
            {
                var editControl = new TagEditControl(tag);
                editControl.Init();
                popupContainerEditTag.Controls.Add(editControl);
                editControl.Dock = DockStyle.Fill;
                popupContainerEditTag.Width = popupEditTag.Width;
            }else
             popupEditTag.CancelPopup();
        }

        private void popupEditTag_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (popupContainerEditTag.Controls.Count > 0)
            {
                var control = popupContainerEditTag.Controls[0];
                if (control != null && control is TagEditControl)
                {
                    SelectedContent = ((TagEditControl) control).Tag;
                }
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new FontDialog();
            if(dlg.ShowDialog() == DialogResult.OK)
            {
                float size = dlg.Font.Size - 7;
                if (size < 1) size = 1;

                ExecCommand("FontSize",false,size);
                ExecCommand("FontName",false,dlg.Font.Name);
                if (dlg.Font.Bold)
                    ExecCommand("Bold");
                if (dlg.Font.Italic)
                    ExecCommand("Italic");
                if (dlg.Font.Underline)
                    ExecCommand("Underline");
                if (dlg.Font.Strikeout)
                    ExecCommand("StrikeThrough");


            }
        }
    }
}