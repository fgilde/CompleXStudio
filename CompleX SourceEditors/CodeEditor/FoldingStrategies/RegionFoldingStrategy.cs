using System;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace CompleX_SourceEditors.CodeEditor.FoldingStrategies
{
    public class RegionFoldingStrategy:AbstractFoldingStrategy
    {
        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            return CreateNewFoldings(document);
        }

        public IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
        {
            var newFoldings = new List<NewFolding>();

            var startOffsets = new Stack<int>();
            int lastNewLineOffset = 0;
            for (int i = 0; i < document.TextLength; i++)
            {
                char c = document.GetCharAt(i);
                if (c == '#')
                {
                    var text = document.GetText(i + 1, 9);
                    if (text.StartsWith("region"))
                    {
                        startOffsets.Push(i);
                    }
                    else if (text == "endregion" && startOffsets.Count > 0)
                    {
                        int startOffset = startOffsets.Pop();
                        if (startOffset < lastNewLineOffset)
                        {
                            newFoldings.Add(new NewFolding(startOffset, i + 10));
                        }
                    }
                }
                else if (c == '\n' || c == '\r')
                {
                    lastNewLineOffset = i + 1;
                }
            }
            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }
    }
}