using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Folding;

namespace CompleX_SourceEditors.CodeEditor.FoldingStrategies
{
    public class SelectionFoldingStrategy:AbstractFoldingStrategy
    {
        private readonly TextArea textArea;
        private readonly List<NewFolding> newFoldings;
        public SelectionFoldingStrategy(TextArea textArea)
        {
            this.textArea = textArea;
            newFoldings = new List<NewFolding>();
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            if (textArea.Selection.Length > 0)
            {
                newFoldings.Clear();
                //textArea.Selection.StartSelectionOrSetEndpoint()
                
                var startOffset = textArea.Selection.SurroundingSegment.Offset;
                var endOffset = textArea.Selection.SurroundingSegment.EndOffset;

                string name = document.GetText(startOffset, Math.Min(6, endOffset))+"...";
                var folding = new NewFolding(startOffset, endOffset) {Name = name};
                newFoldings.Add(folding);
            }
            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }
    }
}