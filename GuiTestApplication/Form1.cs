using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CompleX.Presentation.Controls;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Presentation.Controls.interfaces;
using CompleX.Presentation.Controls.WPFDialogs;

namespace GuiTestApplication
{
    public partial class TestForm_Dialogs : Form
    {
        public TestForm_Dialogs()
        {
            InitializeComponent();
        }

        // TEST Viewing Static Dialogs (Dialogs runs in seperated thread)
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DialogContext<WaitingDialog>.Current.Description = new WaitingDialogDescription("Title", "Header " + DateTime.Now.Second, "Main", "description", true, true);
                DialogContext<WaitingDialog>.Current.ShowDialog(checkBox2.Checked);
                button2.Text = String.Format("Hide Wait ({0})", DialogContext<WaitingDialog>.Current.GetPendingCalls());
            }
            //else
            //{
            //    DialogContext<WindowFormWrapper<WaitingForm>>.Current.Description = new WaitingDialogDescription("Title", "Header", "Main", "description", true, true);
            //    DialogContext<WindowFormWrapper<WaitingForm>>.Current.ShowDialog(checkBox2.Checked);
            //    button2.Text = String.Format("Hide Wait ({0})", DialogContext<WindowFormWrapper<WaitingForm>>.Current.GetPendingCalls());
            //}
            checkBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DialogContext<WaitingDialog>.Current.CloseDialog();
                checkBox1.Enabled = !DialogContext<WaitingDialog>.Current.DialogIsVisible;
                button2.Text = String.Format("Hide Wait ({0})", DialogContext<WaitingDialog>.Current.GetPendingCalls());
            }
            //else
            //{
            //    DialogContext<WindowFormWrapper<WaitingForm>>.Current.CloseDialog();
            //    checkBox1.Enabled = !DialogContext<WindowFormWrapper<WaitingForm>>.Current.DialogIsVisible;
            //    button2.Text = String.Format("Hide Wait ({0})", DialogContext<WindowFormWrapper<WaitingForm>>.Current.GetPendingCalls());
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogContext<SplashDialog>.Current.Description = new SplashDialogDescription("CP-CONS", "3.5.0.14") { TopMost = true };
            DialogContext<SplashDialog>.Current.ShowDialog(checkBox2.Checked);
            button4.Text = String.Format("Hide Splash ({0})", DialogContext<SplashDialog>.Current.GetPendingCalls());
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (DialogContext<SplashDialog>.Current.Description is SplashDialogDescription)
            {
                ((SplashDialogDescription)DialogContext<SplashDialog>.Current.Description).Product = "CP-PlannerCS";
                ((SplashDialogDescription)DialogContext<SplashDialog>.Current.Description).Version = "2.4.5";
                ((SplashDialogDescription)DialogContext<SplashDialog>.Current.Description).ProgressValue = 89;

                DialogContext<SplashDialog>.Current.Update();
                button4.Text = String.Format("Hide Splash ({0})", DialogContext<SplashDialog>.Current.GetPendingCalls());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogContext<SplashDialog>.Current.CloseDialog();
            button4.Text = String.Format("Hide Splash ({0})", DialogContext<SplashDialog>.Current.GetPendingCalls());

            // DialogContext<SplashDialog>.Current.CloseImmediatly();
        }


        // TEST WORKERTHREAD
        private void button6_Click(object sender, EventArgs e)
        {
            using (new WaitingCursor(this))
            {
                var workerThread = new WorkerThread
                {
                    MainText = "Unwichtige Aktion wird vorbereitet",
                    DescriptionText = "Bereite nix vor...",
                    HeaderText = "Bitte haben Sie einen Moment Geduld",
                    CancelNeedsConfirmation = true
                };

                workerThread.RunWorkerAsync(TestWorkingAction, CompletedCallback);
            }
        }

        private void CompletedCallback(RunWorkerCompletedEventArgs obj)
        {
            if (!obj.Cancelled)
                MessageBox.Show("Successfully finished!");
            else
                MessageBox.Show("Action was cancelled!");
        }

        private void TestWorkingAction(WorkerThread<object>.DoWorkEventArgs obj)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            Thread.Sleep(rnd.Next(4500));
            obj.Worker.IsIndeterminate = false;
            obj.Worker.IsCancelable = true;

            int max = rnd.Next(50, 800);
            obj.Worker.SetMaximum(max);
            for (int i = 0; i < max; i++)
            {
                string descriptionText = String.Format("({0}/{1}) unwichtige Aktionen abgeschlossen!", i, max);
                descriptionText += "\n" + Path.GetFileName(Path.GetTempFileName());
                obj.Worker.MainText = "Unwichtige Aktion wird ausgeführt " + obj.Worker.Percentage + "%";

                obj.Worker.IncrementStep(descriptionText);
                if (!obj.Cancel)
                    Thread.Sleep(rnd.Next(200));
                else
                    return;
            }
        }



        ///TEST Work Action for static dialog (dialog runs in seperated thread)
        private void button7_Click(object sender, EventArgs e)
        {
            IProgressDialog dialog = DialogContext<WaitingDialog>.Current.DialogInstance;

            if (dialog == null) dialog = DialogContext<SplashDialog>.Current.DialogInstance;
            //if (dialog == null) dialog = DialogContext<WindowFormWrapper<WaitingForm>>.Current.DialogInstance;

            if (dialog != null)
            {
                int max = 400;
                dialog.TryInvoke(() => dialog.IsIndeterminate = false);
                dialog.TryInvoke(() => dialog.Maximum = max);
                for (int i = 0; i < max; i++)
                {
                    string descriptionText = String.Format("({0}/{1}) Test Aktion {2}%!", i, max, i.Percentage(max));

                    dialog.TryInvoke(() =>
                    {
                        dialog.ProgressValue = i;
                        dialog.DescriptionText = descriptionText;
                    });
                    Thread.Sleep(20);
                }

                dialog.TryInvoke(() =>
                {
                    dialog.ProgressValue = 0;
                    dialog.DescriptionText = String.Empty;
                });

            }
            else
            {
                MessageBox.Show("No possible Dialog visible");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogContext<WaitingDialog>.Current.CloseImmediatly();
            DialogContext<SplashDialog>.Current.CloseImmediatly();
            //DialogContext<WindowFormWrapper<WaitingForm>>.Current.CloseImmediatly();
        }
    }
}
