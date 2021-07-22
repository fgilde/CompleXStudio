//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using CompleX_Library;
using CompleX_Library.Helper;
using CompleX_Settings.Constants;

namespace CompleX.Controls
{
    public partial class RegexTextBox : UserControl
    {
        private TextBoxKind kind;
        private List<string> history;
        private string regexTester = Pathes.ToolsPath.AddDirectorySeparatorChar() + @"RegExTester.exe";

        /// <summary>
        /// Text
        /// </summary>
        public override string Text
        {
            get
            {
                return comboEdit.Text;
            }
            set
            {
                comboEdit.Text = value;
            }
        }

        /// <summary>
        /// Regex
        /// </summary>
        public Regex Regex
        {
            get{return new Regex(Text);}
            set {Text = value.ToString();}
        }

        /// <summary>
        /// Kind of this box
        /// </summary>
        [Browsable(true)]
        [DefaultValue(TextBoxKind.RegularExpression)]
        public TextBoxKind Kind
        {
            get { return kind; }
            set
            {
                kind = value;
                UpdateButtons();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RegexTextBox()
        {
            InitializeComponent();

            history = CompleX_Settings.Settings.Get(@"HISTORY_REGEXTEXTBOX" + Name, Enumerable.Empty<string>().ToList());
            if (history.Count() > 0)
                comboEdit.Properties.Items.AddRange(history.ToArray());

            contextMenuStripRegex.Renderer = new Office2007Renderer.Office2007Renderer();
        }

        /// <summary>
        /// Adds a text to history
        /// </summary>
        /// <param name="text"></param>
        public void AddTextToHistory(string text)
        {
            if (history != null && !history.Contains(text))
            {
                history.Add(text);
                CompleX_Settings.Settings.Set(@"HISTORY_REGEXTEXTBOX" + Name, history);
                CompleX_Settings.Settings.SaveSettings();
            }
        }

        private void UpdateButtons()
        {
            comboEdit.Properties.Buttons[1].Visible = kind == TextBoxKind.RegularExpression;
            comboEdit.Properties.Buttons[2].Visible = kind == TextBoxKind.RegularExpression || kind == TextBoxKind.Wildcard;
        }

        private void ComboEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                var editor = new RegexTypeEditor();
                var serviceProvider = new RuntimeServiceProvider();
                var res = editor.EditValue(serviceProvider, serviceProvider, Text);
                if (res != null) Text = res.ToString();
                
            }
            else if (e.Button.Index == 2)
            {
                if (kind == TextBoxKind.RegularExpression)
                    contextMenuStripRegex.Show(Cursor.Position);
                else if (kind == TextBoxKind.Wildcard)
                    MessageBox.Show("NOT FINISHED YET");
            }

        }

        private void ToolStripMenuItemInsertRegexClick(object sender, System.EventArgs e)
        {
            Text = Text.Insert(comboEdit.SelectionStart, ((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void OpenRegExTesterToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            if (File.Exists(regexTester))
                Process.Start(regexTester, Text);
        }

        private void ContextMenuStripRegexOpening(object sender, CancelEventArgs e)
        {
            openRegExTesterToolStripMenuItem.Visible = File.Exists(regexTester);
        }

    }

    public enum TextBoxKind
    {
        RegularExpression,
        Wildcard,
        None
    }

}
