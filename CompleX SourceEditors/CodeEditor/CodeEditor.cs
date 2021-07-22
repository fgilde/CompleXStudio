#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using CompleX;
using CompleX.Helper;
using CompleX.ServiceModel;
using CompleX.Services;
using CompleX_Inserters;
using CompleX_Library;
using CompleX_Library.Interfaces;
using CompleX_Settings;
using CompleX_Settings.Constants;
using CompleX_SourceEditors.CodeEditor.CompletionData;
using CompleX_SourceEditors.CodeEditor.FoldingStrategies;
using CompleX_SourceEditors.Properties;
using CompleX_Types;
using CompleX_Types.Extensions;
using CompleX_Types.HTMLParser;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using Color = System.Windows.Media.Color;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;
using Size = System.Drawing.Size;
using UserControl = System.Windows.Forms.UserControl;

#endregion

namespace CompleX_SourceEditors.CodeEditor
{
    [Export(typeof(ISourceEdit))]
    public partial class CodeEditor : UserControl, ISourceEdit, IEditFeatures, IMenuHandler
    {
        private CompletionWindow completionWindow;

        public event EventHandler OnFocus;
        public event EventHandler CursorPositionChanged;
        public event EventHandler ContentChanged;
        public event EventHandler SelectionChanged;
        public string FileName { get { return CompleX_Studio.CurrentFile; } }
        public TextEditor TextEditor { get { return CodeEditorControl.TextEditor; } }
        public TextArea TextArea { get { return TextEditor.TextArea; } }
        public TextView TextView { get { return TextArea.TextView; } }
        public TextDocument TextDocument { get { return TextEditor.Document; } }
        public string CurrentLineText { get { return TextDocument.GetText(CurrentLine); } }
        public DocumentLine CurrentLine { get { return TextDocument.GetLineByNumber(TextArea.Caret.Line); } }

        public CodeEditor()
        {
            
            InitializeComponent();
            CodeEditorControl.TextEditor.GotFocus += TextEditorOnFocus;
            CodeEditorControl.KeyUp += CodeEditorControlOnKeyUp;
            CodeEditorControl.TextEditor.TextChanged += TextEditorOnTextChanged;
            CodeEditorControl.TextEditor.TextArea.SelectionChanged += TextEditorOnSelectionChanged;
            CodeEditorControl.TextEditor.TextArea.TextEntering += TextEditorTextAreaTextEntering;
            CodeEditorControl.TextEditor.TextArea.TextEntered += TextEditorTextAreaTextEntered;
            CodeEditorControl.TextEditor.TextArea.Caret.PositionChanged += CaretPositionChanged;
            CodeEditorControl.SyntaxHighlightingChanged += CodeEditorControlOnSyntaxHighlightingChanged;
            //CodeEditorControl.TextEditor.MouseHover += TextEditorOnMouseHover;
            CodeEditorControl.TextEditor.MouseMove += TextEditorOnMouseHover;

            var selectionBrush = new LinearGradientBrush { EndPoint = new Point(0.5, 1), StartPoint = new Point(0.5, 0) };
            selectionBrush.GradientStops.Add(new GradientStop(Color.FromArgb(100, 190, 222, 255), 0.078));
            selectionBrush.GradientStops.Add(new GradientStop(Color.FromArgb(100, 170, 200, 255), 0.98));

            CodeEditorControl.TextEditor.TextArea.SelectionBrush = selectionBrush;
            CodeEditorControl.TextEditor.TextArea.SelectionForeground = null;
            //TextEditor.Execute(AvalonEditCommands.InvertCase);
            //TextEditor.Select(5,10);
            FillSyntaxMenu();

        }

        public string GetWordAtMouse(MouseEventArgs mouseEventArgs)
        {
            var s = SelectionMouseHandler.GetWordAtMousePosition(mouseEventArgs, TextView);
            return TextDocument.GetText(s);
        }
    
        private void TextEditorOnMouseHover(object sender, MouseEventArgs mouseEventArgs)
        {
            try
            {
                 if((Keyboard.Modifiers & (System.Windows.Input.ModifierKeys.Alt )) == (System.Windows.Input.ModifierKeys.Alt ))
                    return;

                string text = GetWordAtMouse(mouseEventArgs);
                if (text.Length.Equals(6))
                {
                    var color = ColorTranslator.FromHtml("#" + text);
                    colorPanel.BackColor = color;
                    labelColorText.Text = @"#" + text;
                    colorPanel.Visible = true;
                    var position = PointToClient(System.Windows.Forms.Cursor.Position);
                    position.X += 10;
                    colorPanel.Location = position;
                }else
                {
                    colorPanel.Visible = false;
                }
            }
            catch
            {
                colorPanel.Visible = false;
            }
            
        }

        private void CodeEditorControlOnKeyUp(object sender, KeyEventArgs e)
        {
            // IntelliSense wenn Strg+Shift+Space gedrückt wurde (als Param Space)
            if (e.Key == Key.Space && 
                (Keyboard.Modifiers & (System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Shift)) == (System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Shift))
            {
                string sign = " " ;
                var charAtCaret = TextDocument.GetCharAt(TextArea.Caret.Offset-1);
                if (charAtCaret == '<')
                    sign = "<";
                var textComposition = new TextComposition(InputManager.Current, TextEditor, sign);
                IntelliSense(new TextCompositionEventArgs(Keyboard.PrimaryDevice, textComposition),true);
            }
            else if(e.Key == Key.Tab)
            {
                // Template replacing
                int startPos, endPos;
                string text = GetWordLeftFromCursor(out startPos, out endPos).Replace("\t",String.Empty);

                if (text == "nguid")
                    SetText(startPos, endPos, Guid.NewGuid());
                else if (text == "ntime")
                    SetText(startPos, endPos, DateTime.Now);


                //TODO: Allow to create and Check user Templates
            }
        }

 
        public string GetWordLeftFromCursor()
        {
            int a, b;
            return GetWordLeftFromCursor(out a, out b);
        }

        public string GetWordLeftFromCursor(out int startOffset, out int endOffset)
        {
            try
            {
                startOffset = FindPosition(TextArea.Caret.Offset, " ", true, NavigationDirection.Left);
                if (startOffset == -1)
                    startOffset = CurrentLine.Offset;
                else
                    startOffset = startOffset + 1;
                endOffset = TextArea.Caret.Offset;
                return TextDocument.GetText(new TextSegment { StartOffset = startOffset, EndOffset = endOffset });
            }
            catch
            {
                startOffset = -1;
                endOffset = -1;
                return String.Empty;
            }
        }


        public bool LanguageIsForWeb
        {
            get
            {
                return FilenameExtensions.ExtensionsWebFiles.Contains(Path.GetExtension(FileName)) ||
                       (CodeEditorControl.TextEditor.SyntaxHighlighting != null &&
                        CodeEditorControl.TextEditor.SyntaxHighlighting.Name.ToLower().Contains("html"));
            }
        }

        public bool LanguageIs(string name)
        {
            return Path.GetExtension(FileName).ToLower().Contains(name.ToLower()) ||
                       (CodeEditorControl.TextEditor.SyntaxHighlighting != null &&
                        CodeEditorControl.TextEditor.SyntaxHighlighting.Name.ToLower().Contains(name.ToLower()));
        }

        public void Goto(int line, int column)
        {
            CodeEditorControl.TextEditor.TextArea.Caret.Line = line;
            CodeEditorControl.TextEditor.TextArea.Caret.Column = column;
            CodeEditorControl.TextEditor.Focus();
        }

        #region Implementation of IHostedService

        /// <summary>
        /// The Unique ID of this service.
        /// </summary>
        public Guid ID
        {
            get { return new Guid("c55647fe-6a82-4981-8700-cff97078ea28"); }
        }

        /// <summary>
        /// returns the Name of the Service
        /// </summary>
        public string ServiceName
        {
            get { return "CompleX Default SourceCode Editor (BETA)"; }
        }

        /// <summary>
        /// List of supported file extensions .txt etc (use *.* for all)
        /// </summary>
        public IEnumerable<string> SupportedFileExtensions
        {
            get { return new[] { "*.*" }; }
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

        public bool SaveToFile(string fileName)
        {
            CodeEditorControl.TextEditor.Save(fileName);
            return File.Exists(fileName);
        }

        public object SelectedContent
        {
            get { return GetSelectedContent(); }
            set
            {
                string text = GetSourceToInsert(value);
                CodeEditorControl.TextEditor.SelectedText = text;
            }
        }

        private object GetSelectedContent()
        {

            string htmlText = CodeEditorControl.TextEditor.SelectedText;

            Tag tag = GetSelectedHtmlTag();
            if (tag != null)
                return tag.ToCustomClass();

            return htmlText;

        }

        public object Content
        {
            get { return CodeEditorControl.TextEditor.Text ?? String.Empty; }
            set { CodeEditorControl.TextEditor.Text = value != null ? value.ToString() : String.Empty; }
        }

        private static string GetSourceToInsert(object obj)
        {
            if (obj != null)
            {
                string source = obj.ToString();
                if (obj is CustomClass)
                {
                    var tag = CompleX_Types.Tag.CreateFromCustomClass((CustomClass) obj);
                    if (tag != null)
                        source = tag.ToString();
                }

                return PlaceHolder.ReplacePlaceHolder(source);
            }
            return string.Empty;
        }

        public void Insert(object obj)
        {
            string text = GetSourceToInsert(obj);
            InsertAt(TextArea.Caret.Offset,text);
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
            CodeEditorControl.TextEditor.Load(fileName);
            var ext = Path.GetExtension(fileName);
            if (ext.ToLower().Equals(".csms"))
            {
                CodeEditorControl.TextEditor.SyntaxHighlighting =
                    HighlightingManager.Instance.HighlightingDefinitions.FirstOrDefault(
                        definition => definition.Name.Equals("C#"));
            }
            else
                CodeEditorControl.TextEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(ext);
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
            CodeEditorControl.TextEditor.Cut();
        }

        /// <summary>
        /// Copy to Clipboard
        /// </summary>
        public void CopyToClipboard()
        {
            CodeEditorControl.TextEditor.Copy();
        }

        /// <summary>
        /// Paste from clipboard
        /// </summary>
        public void PasteFromClipboard()
        {
            CodeEditorControl.TextEditor.Paste();
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            CodeEditorControl.TextEditor.Undo();
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            CodeEditorControl.TextEditor.Redo();
        }

        /// <summary>
        /// Select All
        /// </summary>
        public void SelectAll()
        {
            CodeEditorControl.TextEditor.SelectAll();
        }

        /// <summary>
        /// Returns if editor can cut at this moment
        /// </summary>
        public bool CanCut
        {
            get { return !String.IsNullOrEmpty(CodeEditorControl.TextEditor.SelectedText); }
        }

        /// <summary>
        /// Can Copy at this moment
        /// </summary>
        public bool CanCopy
        {
            get { return CanCut; }
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
            get { return CodeEditorControl.TextEditor.CanUndo; }
        }

        /// <summary>
        /// Can Redo
        /// </summary>
        public bool CanRedo
        {
            get { return CodeEditorControl.TextEditor.CanRedo; }
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

        public string FindTagNameAtCursor()
        {
            try
            {
                int checkPos = FindPosition(TextArea.Caret.Offset - 1, ">", true, NavigationDirection.Left);
                int startPosition = FindPosition(TextArea.Caret.Offset - 1, "<", true, NavigationDirection.Left);
                if (checkPos == -1 || checkPos < startPosition)
                {
                    string tagName = String.Empty;
                    if (startPosition > -1)
                    {
                        int endPosition = FindPosition(startPosition, " ", true, NavigationDirection.Right);
                        if (endPosition > -1)
                            tagName = TextDocument.GetText(startPosition + 1, (endPosition - startPosition) - 1);
                        return tagName;
                    }
                }
                return String.Empty;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return String.Empty;
            }
        }

        public Tag FindTagAtCursor()
        {
            int i, x;
            return FindTagAtCursor(out i, out x);
        }

        public Tag FindTagAtCursor(out int startPosition, out int endPosition)
        {
            startPosition = -1;
            endPosition = -1;
            try
            {
                startPosition = FindPosition(TextArea.Caret.Offset-1, "<", true, NavigationDirection.Left);
                if (startPosition > -1)
                {
                    var regex = new Regex(Const.RegexMatchAllHtmlTags, RegexOptions.Multiline | RegexOptions.IgnoreCase|RegexOptions.Singleline | RegexOptions.CultureInvariant);
                    string textFromCursorToEnd = TextDocument.GetText(startPosition ,
                                                       TextDocument.TextLength - startPosition);

                    var match = regex.Match(textFromCursorToEnd);
                    endPosition = startPosition + match.Length-1;
                    string tag = match.ToString();
                    if (!String.IsNullOrEmpty(tag))
                        return tag.ToTag();
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
        }

        private int FindPosition(int startOffset, string text, bool currentLineOnly, NavigationDirection direction, bool allowOnlySpaces = false)
        {

            if (direction == NavigationDirection.Left) // Text von der cursor position gesehen aus nach links suchen
            {
                for (int i = 0; startOffset - i >= 0; i++)
                {
                    string sign = TextDocument.GetText(startOffset - i, text.Length);
                    if (currentLineOnly && CurrentLine.Offset > startOffset - i)
                        break;
                    if (text.Equals(sign))
                        return startOffset - i;
                    if (allowOnlySpaces && !String.IsNullOrWhiteSpace(sign))
                        return -1;
                }
            }
            
            else if (direction == NavigationDirection.Right) // Text von der cursor position gesehen aus nach rechts suchen
            {
                for (int i = 0; startOffset + i <= TextEditor.Text.Length; i++)
                {
                    string sign = TextDocument.GetText(startOffset + i, text.Length);
                    if (TextDocument.TextLength < startOffset + i ||
                        (currentLineOnly && CurrentLine.NextLine != null &&
                         CurrentLine.NextLine.Offset < startOffset + i))
                        break;

                    if (text.Equals(sign))
                        return startOffset + i;
                    if (allowOnlySpaces && !String.IsNullOrWhiteSpace(sign))
                        return -1;
                }
            }


            return -1;
        }

        private void CodeEditorControlOnSyntaxHighlightingChanged(object sender, EventArgs eventArgs)
        {
            foreach (ToolStripMenuItem item in syntaxHighlightingToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
                item.Checked = item.Text.Equals(CodeEditorControl.TextEditor.SyntaxHighlighting.Name);

            SetFolding();
        }

        private void SetFolding()
        {
            if (CodeEditorControl.TextEditor.SyntaxHighlighting == null)
            {
                CodeEditorControl.FoldingStrategy = null;
            }
            else
            {
                switch (CodeEditorControl.TextEditor.SyntaxHighlighting.Name)
                {
                    case "XML":
                    case "HTML":
                        CodeEditorControl.FoldingStrategy = new MultiFoldingStrategy(
                                    new XmlFoldingStrategy()
                                    //new SelectionFoldingStrategy(TextArea)
                            );
                        break;
                    case "C#":
                    case "C++":
                    case "PHP":
                    case "Java":
                        CodeEditorControl.FoldingStrategy = 
                            new MultiFoldingStrategy(
                                    new BraceFoldingStrategy(),
                                    new RegionFoldingStrategy()
                                    //new SelectionFoldingStrategy(TextArea)
                            );
                        break;
                    default:
                        CodeEditorControl.FoldingStrategy = null;
                        break;
                }
            }
            if (CodeEditorControl.FoldingStrategy != null)
            {
                if (CodeEditorControl.FoldingManager == null)
                    CodeEditorControl.FoldingManager = FoldingManager.Install(CodeEditorControl.TextEditor.TextArea);
                CodeEditorControl.FoldingStrategy.UpdateFoldings(CodeEditorControl.FoldingManager, CodeEditorControl.TextEditor.Document);
            }
            else
            {
                if (CodeEditorControl.FoldingManager != null)
                {
                    FoldingManager.Uninstall(CodeEditorControl.FoldingManager);
                    CodeEditorControl.FoldingManager = null;
                }
            }
        }

        private void EditSelectedTagToolStripMenuItemClick(object sender, EventArgs e)
        {
            Tag tag = GetSelectedHtmlTag();
            if (tag != null)
            {
                var inserter = ApplicationHost.Host.GetService<TagInserter>() ?? new TagInserter();
                var insert = inserter.GetObjectToInsert(new InsertParameter(tag));
                if (insert != null)
                {
                    SelectedContent = insert;
                }
            }
        }


        private Tag GetSelectedHtmlTag()
        {
            string htmlText = CodeEditorControl.TextEditor.SelectedText;
            return !String.IsNullOrWhiteSpace(htmlText) ? htmlText.ToTag() : null;
        }

        private void FillSyntaxMenu()
        {
            foreach (IHighlightingDefinition highlightingDefinition in HighlightingManager.Instance.HighlightingDefinitions)
            {
                ToolStripItem toolStripItem = new ToolStripMenuItem(highlightingDefinition.Name);
                IHighlightingDefinition definition = highlightingDefinition;
                toolStripItem.Click += (sender, args) => CodeEditorControl.TextEditor.SyntaxHighlighting = definition;
                syntaxHighlightingToolStripMenuItem.DropDownItems.Add(toolStripItem);
            }
        }

        private void TextEditorTextAreaTextEntered(object sender, TextCompositionEventArgs e)
        {
            IntelliSense(e,false); // Nicht manuell, da einfach beim Texteingeben geprüft wird
        }

        private void CloseBrackets(TextCompositionEventArgs e)
        {
            string bracket = new BracketPair().GetClosingBracket(e.Text);
            if (!String.IsNullOrEmpty(bracket))
            {
                if (!String.IsNullOrEmpty(TextEditor.SelectedText))
                {
                    TextEditor.SelectedText = e.Text + TextEditor.SelectedText + bracket;
                    e.Handled = true;
                }
                else
                {
                    TextArea.Document.Insert(TextArea.Caret.Offset, bracket);
                    TextArea.Caret.Column = TextArea.Caret.Column - 1;
                }
            }
        }

        private void IntelliSense(TextCompositionEventArgs e,bool manually)
        {
            if (LanguageIsForWeb)
            {
                IntelliSenseWeb(e,manually);
            }
        }


        private void ListOpenTags()
        {
            string textToCursorPosition = TextDocument.GetText(0, TextArea.Caret.Offset);
            var openTags = HtmlParser.FindOpenHtmlTags(textToCursorPosition).ToList();
            if (openTags.Count > 0)
            {
                completionWindow = new CompletionWindow(CodeEditorControl.TextEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

                foreach (var openTag in openTags)
                    data.Add(new TagCompletionData("</"+openTag+">"){Image = Resources.tag.ToImageSource()});

                completionWindow.Show();
                completionWindow.Closed += (sender, args) => completionWindow = null;
            }
        }

        private void CloseCurrentOpenTag()
        {
            string textToCursorPosition = TextDocument.GetText(0, TextArea.Caret.Offset);
            var openTags = HtmlParser.FindOpenHtmlTags(textToCursorPosition).ToList();
            if(openTags.Count>0)
                Insert(openTags[0]+">");
        }


        private void CloseTagAfterCompletionWindow(string tagName)
        {
            try
            {
                var tag = TagFactory.CreateTag(TagLanguage.HTML, tagName, true); 
                if (!tag.Endtag)
                {
                    TextArea.Document.Insert(TextArea.Caret.Offset, " />");
                    TextArea.Caret.Column = TextArea.Caret.Column - 3; // - " ","/" and ">"
                }
                else
                {
                    TextArea.Document.Insert(TextArea.Caret.Offset, "></" + tagName + ">");
                    TextArea.Caret.Column = TextArea.Caret.Column - (4 + tagName.Length);
                }

            }
            catch (Exception e)
            {
                CompleX_Studio.MessageLog.LogException(e);
            }
        }

        private void IntelliSenseWeb(TextCompositionEventArgs e, bool manually)
        {

            switch (e.Text)
            {
                case "/": // Offene Tags ggf Schließen
                    {
                        if (TextArea.Caret.Offset > 2 &&
                            TextDocument.GetText(TextArea.Caret.Offset - 2, 1).Equals("<"))
                        // Falls das eingebene Zeichen vor dem "/" dem Zeichen "<" entspricht, nach offenen Tags suchen 
                        {
                            CloseCurrentOpenTag();
                        }
                    }
                    break;

                case "<": // Mögliche Tags anzeigen
                    {
                        completionWindow = new CompletionWindow(CodeEditorControl.TextEditor.TextArea);
                        IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

                        // Add Asp Tags
                        if (Path.GetExtension(FileName).ToLower().Equals(".asp") || Path.GetExtension(FileName).ToLower().Equals(".aspx") || CodeEditorControl.TextEditor.SyntaxHighlighting.Name.ToLower().Contains("asp"))
                        {
                            IEnumerable<string> aspTags = CompleX_Types.Tag.GetAvailableTags(TagLanguage.ASPNet);
                            foreach (var tag in aspTags)
                                data.Add(new TagCompletionData(tag){Image = Resources.tag.ToImageSource()});
                        }

                        // Add html Tags
                        IEnumerable<string> htmlTags = CompleX_Types.Tag.GetAvailableTags(TagLanguage.HTML);
                        foreach (var htmlTag in htmlTags)
                        {
                            data.Add(new TagCompletionData(htmlTag)
                                         {
                                             Completed = CloseTagAfterCompletionWindow,
                                             Image = Resources.tag.ToImageSource()
                                         });
                        }

                        completionWindow.Show();
                        completionWindow.Closed += (sender, args) => completionWindow = null;
                    }
                    break;
                case " ": // Attribute oder mögliche werte für attribut zeigen
                    {
                        string tagName = FindTagNameAtCursor();
                        if (!String.IsNullOrEmpty(tagName))
                        {
                            try
                            {
                                var tag = TagFactory.CreateTag(TagLanguage.HTML, tagName, true); 

                                int posLeft = FindPosition(TextArea.Caret.Offset - 1, "\"", true, NavigationDirection.Left, true);
                                int posRight = FindPosition(TextArea.Caret.Offset - 1, "\"", true, NavigationDirection.Right, true);
                                if (posLeft > -1 && posRight > -1)
                                // Cursor ist im Tag und in einem Attribute
                                {
                                    int attributeStartPos = FindPosition(posLeft - 1, " ", true, NavigationDirection.Left);
                                    if (attributeStartPos > -1)
                                    {
                                        // AttributeNamen suchen
                                        string attributeName = TextDocument.GetText(attributeStartPos + 1, (posLeft - attributeStartPos - 2)); // +1 wegen dem Space und -2 weged dem = und dem addiertem Space
                                        ShowAttributeValues(tag, attributeName);
                                    }
                                }
                                else if (posRight == -1)
                                {
                                    // bei space attribute zeigen, wenn der cursor sich innerhalb eines Tags befindet aber nicht innerhalb eines Attributes befindet
                                    ShowTagAttributes(tag);
                                }

                            }
                            catch (Exception exception)
                            {
                                CompleX_Studio.MessageLog.LogException(exception);
                            }
                        }else
                        {
                            if (manually)
                                ListOpenTags();
                        }
                    }
                    break;
            }
        }

        private void ShowAttributeValues(Tag tag, string attributeName)
        {
            TagAttribute attribute = tag.Attributes.FirstOrDefault(tagAttribute => tagAttribute.AtrributeName.Equals(attributeName));
            if(attribute != null)
            {
                AttributeType attributeType = attribute.AttributeType;
                completionWindow = new CompletionWindow(CodeEditorControl.TextEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                switch (attributeType)
                {
                    case AttributeType.Enumerated: // z.B für target etc
                        foreach (var attribOption in attribute.AttribOptions)
                            data.Add(new TagCompletionData(attribOption.Value));
                        completionWindow.Show();
                        completionWindow.Closed += (sender, args) => completionWindow = null;
                        break;
                    case AttributeType.Color: // Farbe
                        var selectCompletionData = new TagCompletionData(Texts.Select)
                        {
                            InsertIfCompletedActionIsSet = false,
                            Image = Resources.color.ToImageSource(),
                            Completed = s =>
                            {
                                var dlg = new ColorDialog();
                                if (dlg.ShowDialog() == DialogResult.OK)
                                    Insert(ColorTranslator.ToHtml(dlg.Color));
                            }
                        };
                        data.Add(selectCompletionData);

                        foreach (var color in Enum.GetValues(typeof(KnownColor))) 
                            data.Add(new TagCompletionData(color.ToString()));
                        
                        completionWindow.Show();
                        completionWindow.Closed += (sender, args) => completionWindow = null;
                        break;
                    case AttributeType.Relativepath: // für links z.B href
                        var browseCompletionData = new TagCompletionData(Texts.Browse)
                                                       {
                                                           InsertIfCompletedActionIsSet = false,
                                                           Image = Resources.ImgOpenWindowsFolder.ToImageSource(),
                                                           Completed = s =>
                                                                           {
                                                                               var fdlg = new OpenFileDialog();
                                                                               if (fdlg.ShowDialog() == DialogResult.OK)
                                                                                   Insert(fdlg.FileName.ToRelativePathOrUri());
                                                                           }
                                                       };
                        data.Add(browseCompletionData);
                        var links = ContentService.GetUsedHtmlHrefs();
                        foreach (var link in links)
                        {
                            var completionData = new TagCompletionData(link);
                            if(link.StartsWith("http"))
                                completionData.Image = Resources.hyperlink_16.ToImageSource();
                            else if (link.StartsWith("javascript"))
                                completionData.Image = Resources.func.ToImageSource();
                            else
                            {
                                #region set file icon
                                Icon icon;
                                try
                                {
                                    icon = ImageFunctions.GetAssociatedIconByExtension(Path.GetExtension(link), true);
                                }
                                catch (Exception e)
                                {
                                    icon = null;
                                    Debug.WriteLine(e);
                                }
                                completionData.Image = icon != null
                                                           ? icon.ToBitmap().ToImageSource()
                                                           : Resources._new.ToImageSource();
                                #endregion
                            }
                            data.Add(completionData);
                        }
                        if(File.Exists(FileName) && Directory.Exists(Path.GetDirectoryName(FileName)))
                        {
                            string[] files = Directory.GetFiles(Path.GetDirectoryName(FileName));
                            foreach (var file in files)
                            {
                                string fileName = Path.GetFileName(file);
                                if (data.FirstOrDefault(data1 => data1.Text.Equals(fileName)) == null)
                                {
                                    var completionData = new TagCompletionData(fileName);
                                    Icon icon = ImageFunctions.GetFileIcon(file);
                                    completionData.Image = icon != null
                                                               ? icon.ToBitmap().ToImageSource()
                                                               : Resources._new.ToImageSource();
                                    data.Add(completionData);
                                }
                            }
                        }
                        completionWindow.Show();
                        completionWindow.Closed += (sender, args) => completionWindow = null;
                        break;
                    case AttributeType.CssStyle:
                        break;
                    case AttributeType.CssID:
                        break;
                    case AttributeType.Style:
                        break;
                    default:
                        completionWindow = null;
                        break;
                }
            }
        }

        private void ShowTagAttributes(Tag tag)
        {
            completionWindow = new CompletionWindow(CodeEditorControl.TextEditor.TextArea);
            IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

            foreach (var tagAttribute in tag.Attributes)
            {
                var completionData = new TagCompletionData(tagAttribute.AtrributeName)
                                         {
                                             Completed = s => ShowAttributeValues(tag, s),
                                             Image = Resources.property.ToImageSource(),
                                             AddApostrophe = true,
                                             Description =
                                                 tagAttribute.AttribCategoryGroup + " - " +
                                                 tagAttribute.AtrributeName + " (" +
                                                 tagAttribute.AttributeType + ") "
                                         };

                if (tagAttribute.IsEventOrAction)
                {
                    completionData.Description = "Event: " + completionData.Description;
                    completionData.Image = Resources._event.ToImageSource();
                }

                data.Add(completionData);

            }

            completionWindow.Show();
            completionWindow.Closed += (sender, args) => completionWindow = null;
        }


        private void TextEditorTextAreaTextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null && e.Text != ":" && e.Text != ">")
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            CloseBrackets(e);
        }

        private void RemoveText(int startPosition, int endPosition)
        {
            TextEditor.TextArea.Document.Remove(startPosition, (endPosition - startPosition));
        }

        private void InsertAt(int offset, object o)
        {
            string s = o.ToString();
            if (o is CustomClass)
            {
                var tag = CompleX_Types.Tag.CreateFromCustomClass((CustomClass)o);
                if (tag != null)
                    s = tag.ToString();
            }
            TextEditor.TextArea.Document.Insert(offset, s);
        }

        private void TagChanged(int start, int end, object tag)
        {
            if (start != -1 && end != -1)
            {
                SetText(start, end, tag);
                TextArea.Caret.Offset = start + 1;
            }
        }

        private void SetText(int start, int end, object insert, bool selectText = false)
        {
            if (!selectText)
            {
                RemoveText(start, end + 1);
                InsertAt(start, insert);
            }
            else
            {
                TextEditor.SelectionStart = start;
                TextEditor.SelectionLength = end-start+1;
                SelectedContent = insert;
            }
        }

        private void InspectPosition()
        {
            var p = new Position(CodeEditorControl.TextEditor.TextArea.Caret.Line,
                              CodeEditorControl.TextEditor.TextArea.Caret.Column);
            CompleX_Studio.InspectObject(p, o =>
                                               {
                                                   var pos = o as Position;
                                                   if (pos != null)
                                                       Goto(pos.Line, pos.Column);
                                               });
        }

        private void CaretPositionChanged(object sender, EventArgs eventArgs)
        {
            if (TextArea.Selection.Length <= 0)
            {
                if (LanguageIsForWeb)
                {
                    int start;
                    int end;
                    var tag = FindTagAtCursor(out start, out end);
                    if (tag != null)
                    {
                        var tagCustomClass = tag.ToCustomClass();
                        CompleX_Studio.InspectObject(tagCustomClass, o1 => TagChanged(start, end, o1));
                    }
                    else
                        InspectPosition();
                }
                else
                {
                    InspectPosition();
                }
            }

            var evt = CursorPositionChanged;
            if (evt != null)
                evt(this, new EventArgs());
        }

        private void TextEditorOnSelectionChanged(object sender, EventArgs e)
        {

            bool isWeb = LanguageIsForWeb;
            surroundObjectWithTagToolStripMenuItem.Visible = isWeb;
            editSelectedTagToolStripMenuItem.Visible = isWeb;

            closeAllOpenTagsToolStripMenuItem.Visible = isWeb || LanguageIs("xml");

            
            if (editSelectedTagToolStripMenuItem.Visible)
            {
                Tag tag = GetSelectedHtmlTag();
                editSelectedTagToolStripMenuItem.Enabled = (tag != null);
            }
            surroundObjectWithTagToolStripMenuItem.Enabled = !String.IsNullOrWhiteSpace(TextEditor.SelectedText);

            var evt = SelectionChanged;
            if (evt != null)
                evt(this, new EventArgs());
        }



        private void TextEditorOnTextChanged(object sender, EventArgs eventArgs)
        {
            var evt = ContentChanged;
            if (evt != null)
                evt(this, new EventArgs());
        }


        private void TextEditorOnFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            var evt = OnFocus;
            if (evt != null)
                evt(this, new EventArgs());
        }

        private void SurroundObjectWithTagToolStripMenuItemClick(object sender, EventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(TextEditor.SelectedText))
            {
                completionWindow = new CompletionWindow(CodeEditorControl.TextEditor.TextArea);
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

                // Add Asp Tags
                if (Path.GetExtension(FileName).ToLower().Equals(".asp") || Path.GetExtension(FileName).ToLower().Equals(".aspx") || CodeEditorControl.TextEditor.SyntaxHighlighting.Name.ToLower().Contains("asp"))
                {
                    IEnumerable<string> aspTags = CompleX_Types.Tag.GetAvailableTags(TagLanguage.ASPNet);
                    foreach (var tag in aspTags)
                        data.Add(new BaseCompletionData
                                     {
                                        Text = tag,
                                        Content = tag,
                                        Image = Resources.tag.ToImageSource(),
                                        Completed = s => TextEditor.SelectedText = "<"+s+">"+TextEditor.SelectedText+"</"+s+">"
                                    }
                                );
                }

                // Add html Tags
                IEnumerable<string> htmlTags = CompleX_Types.Tag.GetAvailableTags(TagLanguage.HTML);
                foreach (var htmlTag in htmlTags)
                {
                    data.Add(new BaseCompletionData
                                 {
                        Text = htmlTag,
                        Content = htmlTag,
                        Completed = s => TextEditor.SelectedText = "<" + s + ">" + TextEditor.SelectedText + "</" + s + ">",
                        Image = Resources.tag.ToImageSource()
                    });
                }
                
                completionWindow.Show();
                completionWindow.Closed += (sender1, args) => completionWindow = null;
            }
        }

        private void ButtonChangeColorClick(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog {Color = colorPanel.BackColor};
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                TextEditor.Text = TextEditor.Text.Replace(labelColorText.Text, ColorTranslator.ToHtml(colorDialog.Color));
                colorPanel.Visible = false;
            }
        }


        private void AsHtmlToolStripMenuItemClick1(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog() { DefaultExt = ".html", FileName = Path.GetFileName(FileName) + "Code Export.html" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TextEditor.SelectAll();
                string s = TextArea.Selection.CreateHtmlFragment(TextArea, new HtmlOptions());
                var writer = new StreamWriter(new FileStream(dlg.FileName, FileMode.Create));
                writer.Write(s);
                writer.Close();
                if (File.Exists(dlg.FileName))
                    FileService.OpenFile(dlg.FileName);
            }
        }

        private void CloseAllOpenTagsToolStripMenuItemClick(object sender, EventArgs e)
        {
            string textToCursorPosition = TextDocument.GetText(0, TextArea.Caret.Offset);
            var openTags = HtmlParser.FindOpenHtmlTags(textToCursorPosition).ToList();
            if (openTags.Count > 0)
            {
                foreach (var openTag in openTags)
                    Insert("</" + openTag + ">"+ Environment.NewLine);

            }
        }

    }

    class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }

    enum NavigationDirection
    {
        Left,
        Right
    }

}
