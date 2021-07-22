using System;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace CompleX_SourceEditors.CodeEditor.CompletionData
{
    public class TagCompletionData : BaseCompletionData
    {
        public TagCompletionData(string text)
        {
            Text = text;
            InsertIfCompletedActionIsSet = true;
        }

        public bool InsertIfCompletedActionIsSet { get; set; }

        public bool AddApostrophe { get; set; }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        public override object Content
        {
            get { return Text; }
        }

       
        public override void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            if (Completed == null || InsertIfCompletedActionIsSet)
            {
                if (AddApostrophe)
                {
                    textArea.Document.Replace(completionSegment, Text + "=\"\"");
                    textArea.Caret.Column = textArea.Caret.Column - 1;
                }
                else
                    textArea.Document.Replace(completionSegment, Text);
            }

           base.Complete(textArea,completionSegment,insertionRequestEventArgs);
        }
    }

}
