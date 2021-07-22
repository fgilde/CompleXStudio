using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using CompleX_Library.Interfaces;

namespace CompleX.Controls
{
    public partial class BaseEditControl : UserControl, ISourceEdit, IDesignable
    {
        public BaseEditControl()
        {
            InitializeComponent();
        }

       public bool Equals(IHostedService other)
        {
            return this.ID.Equals(other.ID);
        }

        public virtual Guid ID
        {
            get { return Guid.NewGuid();}
        }

        public virtual string ServiceName
        {
            get { return String.Empty; }
        }

        public virtual IEnumerable<string> SupportedFileExtensions
        {
            get { return new string[]{}; }
        }

        public virtual bool Initialize()
        {
            return true;
        }


        /// <summary>
        /// Function is called before service will added
        /// Major and Minor shoud be the same of CompleX Studio version otherwise service wasnt loaded
        /// </summary>
        /// <returns></returns>
        public virtual Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public virtual event EventHandler OnFocus;

        public virtual event EventHandler CursorPositionChanged;

        public virtual event EventHandler ContentChanged;

        public virtual event EventHandler SelectionChanged;

        public virtual string FileName { get; set; }

        public virtual bool IsSaved { get;set; }

        public virtual bool SaveToFile(string fileName)
        {
            return false;
        }

        public virtual object SelectedContent
        {
            get { return null; }
            set { return; }
        }

        public virtual object Content { get; set; }

        public virtual void Insert(object obj)
        {
        }

        public virtual bool LoadFromFile(string fileName)
        {
            return false;
        }

        public virtual void CutToClipboard()
        {
        }

        public virtual void CopyToClipboard()
        {
        }

        public virtual void PasteFromClipboard()
        {
        }

        public virtual void Undo()
        {
        }

        public virtual void Redo()
        {
        }

        public virtual void SelectAll()
        {
        }

        public virtual bool ContextMenuIsHandled
        {
            get { return false; }
        }

        public virtual bool CanCut
        {
            get { return false; }
        }

        public virtual bool CanCopy
        {
            get { return false; }
        }

        public virtual bool CanPaste
        {
            get { return false; }
        }

        public virtual bool CanUndo
        {
            get { return false; }
        }

        public virtual bool CanRedo
        {
            get { return false; }
        }

        public virtual event OnException ExceptionOccurred;

        public virtual string Author
        {
            get { return String.Empty; }
        }

        public virtual bool SupportSplitMode
        {
            get { return false; }
        }
    }

}
