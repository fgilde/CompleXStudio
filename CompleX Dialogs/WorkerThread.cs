using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Presentation.Controls.interfaces;
using CompleX.Presentation.Controls.WPFDialogs;

namespace CompleX.Presentation.Controls
{
    public class WorkerThread : WorkerThread<object>
    { }

    public class WorkerThread<T> : IWorkerThread
    {
        public string CompleteMessage { get; set; }
        public T Parameter { get; set; }


        #region PrivateFields

        private readonly BackgroundWorker worker;
        private readonly WaitingDialog waitingDialog;
        private Action<DoWorkEventArgs> doWorkEventHandler;
        private Action<RunWorkerCompletedEventArgs> workCompletedHandler;
        private readonly CultureInfo cultureUI;
        private DoWorkEventArgs eventArguments;

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public WorkerThread()
        {
            CancelNeedsConfirmation = false;
            CompleteMessage = null;
            worker = new BackgroundWorker {WorkerReportsProgress = true};
            
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            waitingDialog = new WaitingDialog(String.Empty, true);

            cultureUI = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            waitingDialog.Cancel += WaitOnCancel;
        }

        #region Properties

        /// <summary>
        /// Gibt an, ob es sich um eine unendliche ProgressBar handelt.
        /// </summary>
        public bool IsIndeterminate
        {
            get { return Execute(() => waitingDialog.IsIndeterminate); }
            set { Execute(() => waitingDialog.IsIndeterminate = value);}
        }

        /// <summary>
        /// Gibt an ob die Aktion abgebrochen werden kann
        /// </summary>
        public bool IsCancelable
        {
            get { return Execute(() => waitingDialog.IsCancelable); }
            set  {Execute(() => { waitingDialog.IsCancelable = waitingDialog.CancelOnClose = value; }); }
        }

        /// <summary>
        /// Haupttext
        /// </summary>
        public string TitleText
        {
            get { return Execute(() => waitingDialog.Title); }
            set { Execute(() => waitingDialog.Title = value); }
        }

        /// <summary>
        /// Haupttext
        /// </summary>
        public string HeaderText
        {
            get { return Execute(() => waitingDialog.HeaderText); }
            set { Execute(() => waitingDialog.HeaderText = value); }
        }

        /// <summary>
        /// Haupttext
        /// </summary>
        public string MainText
        {
            get { return Execute(() => waitingDialog.MainText); }
            set { Execute(() => waitingDialog.MainText = value); }
        }

        /// <summary>
        /// Beschreibung
        /// </summary>
        public string DescriptionText
        {
            get { return Execute(() => waitingDialog.DescriptionText); }
            set { Execute(() => waitingDialog.DescriptionText = value); }
        }

        public int Percentage
        {
            get
            {
                var actualProgress = (int)Execute(() => waitingDialog.ProgressValue);
                var maximum = (int)Execute(() => waitingDialog.Maximum);
                return ((100 * actualProgress) / maximum);
            }
        }

        public bool CancelNeedsConfirmation { get; set; }

        #endregion


        #region Public Methods

        /// <summary>
        /// Sets information by descriptions
        /// </summary>
        /// <param name="description"></param>
        public void SetDialogDescriptions(WaitingDialogDescription description)
        {
            Execute(() => waitingDialog.UpdateDialog(description));
        }

        /// <summary>
        /// Sets the maximum.
        /// </summary>
        /// <param name="max">The max.</param>
        public void SetMaximum(int max)
        {
            Execute(() => waitingDialog.Maximum = max);
        }

        /// <summary>
        /// Reports the progress.
        /// </summary>
        /// <param name="percentProgress">The percent progress.</param>
        /// <param name="state">The state.</param>
        public void ReportProgress(int percentProgress, object state)
        {
            worker.ReportProgress(percentProgress, state);
        }

        /// <summary>
        /// Increments the step.
        /// </summary>
        /// <param name="state">The state.</param>
        public void IncrementStep(object state)
        {
            var actualProgress = (int)Execute(() => waitingDialog.ProgressValue);
            worker.ReportProgress(actualProgress + 1, state);
        }


        public void RunWorkerAsync(Action<DoWorkEventArgs> doWorkHandler)
        {
            RunWorkerAsync(doWorkHandler, DefaultCallback);
        }

        public void RunWorkerAsync(Action<DoWorkEventArgs> doWorkHandler, Action<RunWorkerCompletedEventArgs> completedCallback)
        {
            doWorkEventHandler = doWorkHandler;
            if (completedCallback != null)
                workCompletedHandler = completedCallback;
            worker.RunWorkerAsync(Parameter);
        }

        #endregion



        private void Execute(Action action)
        {
            waitingDialog.CheckInvoke(action);
        }

        private TResult Execute<TResult>(Func<TResult> action)
        {
            return waitingDialog.CheckInvoke(action);
        }


        private bool CheckCancelConfirmation()
        {
            if (!CancelNeedsConfirmation)
                return true;
             

            if(!TaskDialog.IsSupported)
            {
                if (MessageBox.Show(Resource.CancelConfirmation, String.Empty, MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                    return true;
            }else
            {
                if (TaskDialog.Show(Resource.CancelConfirmation, Resource.CancelHeader, String.Empty, TaskDialogButtons.Yes | TaskDialogButtons.No, TaskDialogIcon.Warning) == TaskDialogResult.Yes)
                    return true;
            }
            

            return false;
        }
        private void WaitOnCancel(object sender)
        {
            if (IsCancelable)
            {
                if (CheckCancelConfirmation())
                    eventArguments.Cancel = true;
            }
        }


        void WorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = cultureUI;

            waitingDialog.Dispatcher.BeginInvoke(new MethodInvoker(() => waitingDialog.ShowDialog()));

            eventArguments = new DoWorkEventArgs(Parameter, this);
            doWorkEventHandler(eventArguments);
            e.Result = eventArguments.Result;
        }


        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            waitingDialog.Topmost = false;
            waitingDialog.CloseDialog();
            Application.DoEvents();
            var completedEventArgs = new RunWorkerCompletedEventArgs(e.Result,e.Error,eventArguments.Cancel);
            if (e.Error == null)
            {
                if (!string.IsNullOrEmpty(CompleteMessage))
                {
                    if(TaskDialog.IsSupported)
                    {
                        TaskDialog.Show(CompleteMessage);
                    }
                    else
                    {
                        MessageBox.Show(CompleteMessage);
                    }
                    
                }
            }
            if (workCompletedHandler != null)
                workCompletedHandler(completedEventArgs);
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            waitingDialog.ProgressValue = e.ProgressPercentage;
            waitingDialog.DescriptionText = (string)e.UserState;
        }


        private void DefaultCallback(RunWorkerCompletedEventArgs eventArgs)
        {
            if (eventArgs.Error != null)
                throw new TargetInvocationException(eventArgs.Error);
        }

        public class DoWorkEventArgs : CancelEventArgs
        {
            readonly T argument;
            readonly WorkerThread<T> worker;

            public DoWorkEventArgs(T argument, WorkerThread<T> worker)
            {
                this.argument = argument;
                this.worker = worker;
            }

            public T Argument
            {
                get { return argument; }
            }

            public WorkerThread<T> Worker
            {
                get { return worker; }
            }

            public object Result { get; set; }
        }
    }
}