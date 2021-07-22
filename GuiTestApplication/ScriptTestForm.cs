using System;
using System.Threading;
using System.Security.Principal;
using System.Windows.Forms;
using CompleX.Scripting;

namespace GuiTestApplication
{
    public partial class ScriptTestForm : Form {

        public ScriptTestForm() {
            InitializeComponent();
        }

        private void BT_Execute_Click(object sender, EventArgs e) {
            this.ClearInfoText();

            var thread = new Thread(this.GenerateAndRunScript);
            thread.Start();
        }

        private void GenerateAndRunScript() {
            var language = (this.RB_Language_CS.Checked ? ScriptLanguage.CSharp : ScriptLanguage.VbNet);
            var context = new CompleXScriptContext {
                                                       UserName = WindowsIdentity.GetCurrent().Name,
                                                   };
            context.WriteLineDelegate = s => this.Invoke(new StringDelegate(this.AddInfoText), s);

            var engine = new ScriptEngine(language, this.TB_Script.Text, context);
            engine.StatusChanged += this.ScriptEngine_StatusChanged;
            engine.Execute();
        }

        private void ScriptEngine_StatusChanged(object sender, ScriptEngineStatusEventArgs e) {
            this.Invoke(new StringDelegate(this.AddInfoText), e.Status);
        }

        private void ClearInfoText() {
            this.TB_Info.Text = null;
        }

        private void AddInfoText(string text) {
            this.TB_Info.Text += string.Concat(text, "\r\n");
        }
    }
}