using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace CompleX_SourceEditors.CodeEditor
{
    /// <summary>
    /// Interaction logic for CodeEditorControl.xaml
    /// </summary>
    public partial class CodeEditorControl : UserControl
    {

        public event EventHandler SyntaxHighlightingChanged;
        public FoldingManager FoldingManager;
        public AbstractFoldingStrategy FoldingStrategy;


        public CodeEditorControl()
        {
            InitializeComponent();
            TextEditor.TextArea.Caret.PositionChanged += CaretOnPositionChanged;

            var foldingUpdateTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(2)};
            foldingUpdateTimer.Tick += FoldingUpdateTimerTick;
            foldingUpdateTimer.Start();
        }



        private void CaretOnPositionChanged(object sender, EventArgs eventArgs)
        {
            if (Line != null && Column != null && TextEditor != null && TextEditor.TextArea != null && TextEditor.TextArea.Caret != null)
            {
                Line.Text = TextEditor.TextArea.Caret.Line.ToString();
                Column.Text = TextEditor.TextArea.Caret.Column.ToString();
                Offset.Text = TextEditor.TextArea.Caret.Offset.ToString();
            }
        }


        private void InvokeSyntaxHighlightingChanged(EventArgs e)
        {
            EventHandler handler = SyntaxHighlightingChanged;
            if (handler != null) handler(this, e);
        }

        void HighlightingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InvokeSyntaxHighlightingChanged(e);
        }

        void FoldingUpdateTimerTick(object sender, EventArgs e)
        {
            if (FoldingStrategy != null && TextEditor.Document != null && !String.IsNullOrEmpty(TextEditor.Document.Text) && TextEditor.Document.LineCount > 1)
            {
                try
                {
                    FoldingStrategy.UpdateFoldings(FoldingManager, TextEditor.Document);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                }
            }
        } 


    }

}
