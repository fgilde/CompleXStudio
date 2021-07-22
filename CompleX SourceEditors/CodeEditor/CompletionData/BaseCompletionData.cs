using System;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace CompleX_SourceEditors.CodeEditor.CompletionData
{
    public class BaseCompletionData : ICompletionData
    {

        public virtual Action<string> Completed { get; set; }

        public virtual System.Windows.Media.ImageSource Image { get; set; }

        public virtual string Text { get; set; }

        public virtual object Description { get; set; }

        public virtual object Content { get; set; }


        public virtual void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            if (Completed != null)
                Completed(Text);
        }
    }
}
