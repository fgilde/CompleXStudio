using System;
using System.Collections.Generic;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace CompleX_SourceEditors.CodeEditor.FoldingStrategies
{
    public class MultiFoldingStrategy:AbstractFoldingStrategy
    {
        private AbstractFoldingStrategy[] strategies;

        public MultiFoldingStrategy(params AbstractFoldingStrategy[] foldingStrategies)
        {
            strategies = foldingStrategies;
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            var newFoldings = new List<NewFolding>();
            foreach (var abstractFoldingStrategy in strategies)
                newFoldings.AddRange(abstractFoldingStrategy.CreateNewFoldings(document, out firstErrorOffset));
            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }
    }
}