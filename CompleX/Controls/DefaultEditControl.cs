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
using System.IO;
using System.Linq;
using CompleX.Helper;
using CompleX.Presentation.Controls.Extensions;
using CompleX_Library;
using CompleX_Library.Interfaces;
using EventHandler=System.EventHandler;

namespace CompleX.Controls
{
    public partial class DefaultEditControl : BaseEditControl, IEditFeatures
    {
        private readonly Guid guid;

        public DefaultEditControl()
        {
            InitializeComponent();
            guid = Guid.NewGuid();
        }


        public override Guid ID
        {
            get { return guid; }
        }

        public override string ServiceName
        {
            get { return "CompleX default TextEditor"; }
        }


        public override event EventHandler OnFocus;

        private void InvokeOnFocus(EventArgs e)
        {
            EventHandler handler = OnFocus;
            if (handler != null) handler(this, e);
        }

        public override event EventHandler CursorPositionChanged;

        private void InvokeTextCursorPositionChanged(EventArgs e)
        {
            EventHandler changed = CursorPositionChanged;
            if (changed != null) changed(this, e);
        }

        public override event EventHandler ContentChanged
        {
            add { textBoxContent.TextChanged += value; }
            remove { textBoxContent.TextChanged -= value; }
        }

        public override object SelectedContent
        {
            get { return textBoxContent.SelectedText; }
            set
            {
                if (value != null)
                {
                    textBoxContent.SelectedText = value.ToString();
                }
            }
        }

        public override object Content
        {
            get { return textBoxContent.CheckInvoke(() => textBoxContent.Text); }
            set
            {
                if(value != null)
                    textBoxContent.Text = value.ToString();
            }
        }

        public override bool SaveToFile(string fileName)
        {
            try
            {
                var writer = new StreamWriter(new FileStream(fileName, FileMode.Create));
                writer.Write(textBoxContent.Text);
                writer.Close();
                return File.Exists(fileName);
            }
            catch
            {
                return false;
            }
        }

        public override bool LoadFromFile(string fileName)
        {
            var streamReader = new StreamReader(fileName);
            textBoxContent.Text = streamReader.ReadToEnd();
            streamReader.Close();
            return true;
        }

        public override void Insert(object obj)
        {
            if (obj != null)
            {
                string insert = PlaceHolder.ReplacePlaceHolder(obj.ToString());
                SelectedContent = insert;
            }
        }

        public IEnumerable<ItemAction> GetCustomActions()
        {
            return Enumerable.Empty<ItemAction>();
        }

        public override void CutToClipboard()
        {
            textBoxContent.Cut();
        }

        public override void CopyToClipboard()
        {
            textBoxContent.Copy();
        }

        public override void PasteFromClipboard()
        {
            textBoxContent.Paste();
        }

        public override void Undo()
        {
            textBoxContent.Undo();
        }

        public override void Redo()
        {
           //
        }

        public override void SelectAll()
        {
            textBoxContent.SelectAll();
        }

        public override bool CanCut
        {
            get { return SelectedContent.ToString() != String.Empty; }
        }

        public override bool CanCopy
        {
            get { return CanCut; }
        }

        public override bool CanPaste
        {
            get { return true; }
        }

        public override bool CanUndo
        {
            get { return textBoxContent.CanUndo; }
        }

        public override bool CanRedo
        {
            get { return false; }
        }

        public new bool CanSelect
        {
            get { return textBoxContent.CanSelect; }
        }

        private  void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            InvokeTextCursorPositionChanged(e);
        }

        private void BaseEditControl_Enter(object sender, EventArgs e)
        {
            InvokeOnFocus(e);
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            InvokeTextCursorPositionChanged(e);
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            InvokeOnFocus(e);
        }

        private void textBoxContent_CursorChanged(object sender, EventArgs e)
        {
            InvokeTextCursorPositionChanged(e);
        }

    }
}
