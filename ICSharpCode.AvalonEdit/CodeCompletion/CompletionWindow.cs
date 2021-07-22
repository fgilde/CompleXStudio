// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <author name="Daniel Grunwald"/>
//     <version>$Revision: 4282 $</version>
// </file>

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// The code completion window.
	/// </summary>
	public class CompletionWindow : CompletionWindowBase
	{
		readonly CompletionList completionList = new CompletionList();
		ToolTip toolTip = new ToolTip();
		
		/// <summary>
		/// Creates a new code completion window.
		/// </summary>
		public CompletionWindow(TextArea textArea) : base(textArea)
		{
			// keep height automatic
			CloseAutomatically = true;
			SizeToContent = SizeToContent.Height;
			Width = 175;
		    Height = 150;
			Content = completionList;
			// prevent user from resizing window to 0x0
			MinHeight = 15;
			MinWidth = 30;
			
			toolTip.PlacementTarget = this;
			toolTip.Placement = PlacementMode.Right;
			toolTip.Closed += ToolTipClosed;
			
			completionList.InsertionRequested += CompletionListInsertionRequested;
			completionList.SelectionChanged += CompletionListSelectionChanged;
		    
			AttachEvents();
		}
		
		#region ToolTip handling
		void ToolTipClosed(object sender, RoutedEventArgs e)
		{
			// Clear content after tooltip is closed.
			// We cannot clear is immediately when setting IsOpen=false
			// because the tooltip uses an animation for closing.
			if (toolTip != null)
				toolTip.Content = null;
		}
		
		void CompletionListSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = completionList.SelectedItem;
			if (item == null)
				return;
			object description = item.Description;
			if (description != null) {
				toolTip.Content = description;
				toolTip.IsOpen = true;
			} else {
				toolTip.IsOpen = false;
			}
		}
		#endregion
		
		void CompletionListInsertionRequested(object sender, EventArgs e)
		{
			var item = completionList.SelectedItem;
			if (item != null)
				item.Complete(TextArea, new AnchorSegment(TextArea.Document, StartOffset, EndOffset - StartOffset), e);
			Close();
		}
		
		/// <inheritdoc/>
		protected override void OnSourceInitialized(EventArgs e)
		{
			MaxHeight = 300;
			base.OnSourceInitialized(e);
		}
		
		InputHandler myInputHandler;
		
		void AttachEvents()
		{
			TextArea.Caret.PositionChanged += CaretPositionChanged;
			TextArea.MouseWheel += TextAreaMouseWheel;
			TextArea.PreviewTextInput += TextAreaPreviewTextInput;
			myInputHandler = new InputHandler(this);
			TextArea.ActiveInputHandler = myInputHandler;
		}
		
		/// <inheritdoc/>
		protected override void DetachEvents()
		{
			TextArea.Caret.PositionChanged -= CaretPositionChanged;
			TextArea.MouseWheel -= TextAreaMouseWheel;
			TextArea.PreviewTextInput -= TextAreaPreviewTextInput;
			base.DetachEvents();
			if (TextArea.ActiveInputHandler == myInputHandler)
				TextArea.ActiveInputHandler = TextArea.DefaultInputHandler;
		}
		
		#region InputHandler
		/// <summary>
		/// A dummy input handler (that justs invokes the default input handler).
		/// This is used to ensure the completion window closes when any other input handler
		/// becomes active.
		/// </summary>
		sealed class InputHandler : ITextAreaInputHandler
		{
			readonly CompletionWindow window;
			
			public InputHandler(CompletionWindow window)
			{
				Debug.Assert(window != null);
				this.window = window;
			}
			
			public TextArea TextArea {
				get { return window.TextArea; }
			}
			
			public void Attach()
			{
				TextArea.DefaultInputHandler.Attach();
			}
			
			public void Detach()
			{
				TextArea.DefaultInputHandler.Detach();
				// close with dispatcher so we don't get reentrance problems in input handler Detach/Attach
				window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(window.Close));
			}
		}
		#endregion
		
		/// <inheritdoc/>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			if (toolTip != null) {
				toolTip.IsOpen = false;
				toolTip = null;
			}
		}
		
		/// <inheritdoc/>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (!e.Handled) {
				completionList.HandleKey(e);
			}
		}
		
		void TextAreaPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = RaiseEventPair(this, PreviewTextInputEvent, TextInputEvent,
			                           new TextCompositionEventArgs(e.Device, e.TextComposition));
		}
		
		void TextAreaMouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = RaiseEventPair(GetScrollEventTarget(),
			                           PreviewMouseWheelEvent, MouseWheelEvent,
			                           new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta));
		}
		
		UIElement GetScrollEventTarget()
		{
			if (completionList == null)
				return this;
			return completionList.ScrollViewer ?? completionList.ListBox ?? (UIElement)completionList;
		}
		
		/// <summary>
		/// Gets/Sets whether the completion window should close automatically.
		/// The default value is true.
		/// </summary>
		public bool CloseAutomatically { get; set; }
		
		/// <inheritdoc/>
		protected override bool CloseOnFocusLost {
			get { return CloseAutomatically; }
		}
		
		/// <summary>
		/// When this flag is set, code completion closes if the caret moves to the
		/// beginning of the allowed range. This is useful in Ctrl+Space and "complete when typing",
		/// but not in dot-completion.
		/// Has no effect if CloseAutomatically is false.
		/// </summary>
		public bool CloseWhenCaretAtBeginning { get; set; }
		
		void CaretPositionChanged(object sender, EventArgs e)
		{
            completionList.FilterList(String.Empty);
			int offset = TextArea.Caret.Offset;
			if (offset == StartOffset) {
				if (CloseAutomatically && CloseWhenCaretAtBeginning)
					Close();
				return;
			}
			if (offset < StartOffset || offset > EndOffset) {
				if (CloseAutomatically) {
					Close();
				}
			} else {
				TextDocument document = TextArea.Document;
				if (document != null) {
				    var text = document.GetText(StartOffset, offset - StartOffset);
                    completionList.FilterList(text);
					completionList.SelectItemWithStart(text);
				}
			}
		}
		
		/// <summary>
		/// Gets the completion list used in this completion window.
		/// </summary>
		public CompletionList CompletionList {
			get { return completionList; }
		}
	}
}