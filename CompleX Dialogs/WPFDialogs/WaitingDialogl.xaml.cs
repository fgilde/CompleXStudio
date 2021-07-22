using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Presentation.Controls.interfaces;
using Microsoft.WindowsAPICodePack.Taskbar;
using Application = System.Windows.Application;

namespace CompleX.Presentation.Controls.WPFDialogs
{
    /// <summary>
    /// Interaction logic for WaitingControl.xaml
    /// </summary>
    public partial class WaitingDialog : IStaticDialog, IProgressDialog
    {
        private readonly string name;
        private bool canClose;
        private bool externalClose;
        private readonly System.Timers.Timer timerElapsedTime;
        private readonly Stopwatch stopwatch; 

        public delegate void OnCancel(object sender);


        #region Properties and Dependencies

        /// <summary>
        /// Gibt an, ob es sich um eine unendliche ProgressBar handelt.
        /// </summary>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        /// <summary>
        /// Aktueller Fortschrittswert.
        /// </summary>
        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        /// <summary>
        /// Überschrift
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        /// <summary>
        /// Haupttext
        /// </summary>
        public string MainText
        {
            get { return (string)GetValue(MainTextProperty); }
            set { SetValue(MainTextProperty, value); }
        }

        /// <summary>
        /// Beschreibung
        /// </summary>
        public string DescriptionText
        {
            get { return (string)GetValue(DescriptionTextProperty); }
            set { SetValue(DescriptionTextProperty, value); }
        }

        /// <summary>
        /// Progress kann abgebrochen werden.
        /// </summary>
        public bool IsCancelable
        {
            get {
                return this.CheckInvoke(() => (bool)GetValue(IsCancelableProperty));
            }
            set
            {
                this.CheckInvoke(() =>
                {
                    SetValue(IsCancelableProperty, value);
                    buttonCancel.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                });
            }
        }

        /// <summary>
        /// Maximumwert.
        /// </summary>
        /// <value>The maximum.</value>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Videodatei
        /// </summary>
        public Uri VideoFile
        {
            get { return (Uri)GetValue(VideoFileProperty); }
            set { SetValue(VideoFileProperty, value); }
        }

        public string ElapsedTime
        {
            get { return (string)GetValue(ElapsedTimeProperty); }
            set { SetValue(ElapsedTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElapsedTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElapsedTimeProperty =
            DependencyProperty.Register("ElapsedTime", typeof(string), typeof(WaitingDialog), new UIPropertyMetadata(String.Empty));



        // Using a DependencyProperty as the backing store for CancelOnClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelOnCloseProperty =
            DependencyProperty.Register("CancelOnClose", typeof(bool), typeof(WaitingDialog), new UIPropertyMetadata(false));

        /// <summary>
        /// <see cref="HeaderText"/>
        /// </summary>
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText",
                                                                                                   typeof(string), typeof(WaitingDialog), new UIPropertyMetadata(String.Empty));

        /// <summary>
        /// <see cref="MainText"/>
        /// </summary>
        public static readonly DependencyProperty MainTextProperty =
            DependencyProperty.Register("MainText", typeof(string), typeof(WaitingDialog), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// <see cref="DescriptionText"/>
        /// </summary>
        public static readonly DependencyProperty DescriptionTextProperty =
            DependencyProperty.Register("DescriptionText", typeof(string), typeof(WaitingDialog), new UIPropertyMetadata(string.Empty));

        /// <summary>
        /// <see cref="IsIndeterminate"/>
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(WaitingDialog), new UIPropertyMetadata(true,OnIsIndeterminateChanged));

        /// <summary>
        /// <see cref="ProgressValue"/>
        /// </summary>
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(WaitingDialog), new UIPropertyMetadata(0d,OnProgressValueChanged));

        /// <summary>
        /// <see cref="IsCancelable"/>
        /// </summary>
        public static readonly DependencyProperty IsCancelableProperty =
            DependencyProperty.Register("IsCancelable", typeof(bool), typeof(WaitingDialog), new UIPropertyMetadata(false));

        /// <summary>
        /// <see cref="Maximum"/>
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(WaitingDialog), new UIPropertyMetadata(100d,OnMaximumChanged));

        /// <summary>
        /// <see cref="VideoFile"/>
        /// </summary>
        public static readonly DependencyProperty VideoFileProperty =
            DependencyProperty.Register("VideoFile", typeof(Uri), typeof(WaitingDialog), new UIPropertyMetadata(null));

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="WaitingDialog"/> class.
        /// </summary>
        public WaitingDialog()
        {
            InitializeComponent();

            DataContext = this;

            ElapsedTime = "00:00:00";
            canClose = false;
            externalClose = false;
           
            name = Path.GetTempFileName();
            name = Path.ChangeExtension(name, "wmv");
            File.WriteAllBytes(name, Resource.progress);
            VideoFile = new Uri(name);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            timerElapsedTime = new System.Timers.Timer(1000);
            timerElapsedTime.Elapsed += TimerElapsedTimeElapsed;
            timerElapsedTime.Enabled = true;

        }

        void TimerElapsedTimeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new MethodInvoker(() =>
                ElapsedTime =
                String.Format("{0}:{1}:{2}", stopwatch.Elapsed.Hours.Zeroize(), stopwatch.Elapsed.Minutes.Zeroize(),
                              stopwatch.Elapsed.Seconds.Zeroize())                
            ));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitingDialog"/> class.
        /// </summary>
        /// <param name="mainText">Haupttext</param>
        /// <param name="isIndeterminate">Gibt an, ob es sich um eine unendliche ProgressBar handelt.</param>
        public WaitingDialog(string mainText, bool isIndeterminate)
            : this()
        {
            MainText = mainText;
            IsIndeterminate = isIndeterminate;

            if (Application.Current != null)
                Owner = Application.Current.MainWindow;
        }




        public bool CancelOnClose
        {
            get { return this.CheckInvoke(() => (bool)GetValue(CancelOnCloseProperty)); }
            set
            {
                this.CheckInvoke(() =>
                {
                    SetValue(CancelOnCloseProperty, value);
                    if (value && IsCancelable)
                    {
                        canClose = true;
                    }
                    else
                    {
                        canClose = false;
                    }
                });

            }
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closed"/> event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            File.Delete(name);
            base.OnClosed(e);
        }



        public event OnCancel Cancel;

        private void InvokeCancel()
        {
            if (Cancel != null) Cancel(this);
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            if (!CancelOnClose)
                InvokeCancel();
            else
                base.Close();
        }

        public void DisplayDialog()
        {
            try
            {
                ShowDialog();
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Schließt den Dialog
        /// </summary>
        public void CloseDialog()
        {
            canClose = true;
            externalClose = true;
            base.Close();
            try
            {
                if (TaskbarManager.IsPlatformSupported)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Closes the Dialog
        /// </summary>
        public new void Close()
        {
            CloseDialog();
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing"/> event.
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (CancelOnClose && IsCancelable && !externalClose)
            {
                InvokeCancel();
                e.Cancel = true;
            }
            else
            {
                e.Cancel = !canClose;
            }
            base.OnClosing(e);
        }

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = d as WaitingDialog;
            if (dialog != null && TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressState(dialog.IsIndeterminate
                                                             ? TaskbarProgressBarState.Indeterminate
                                                             : TaskbarProgressBarState.Normal);
            }
        }

        private static void OnProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = d as WaitingDialog;
            if (dialog != null)
                dialog.UpdateProgress();
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dialog = d as WaitingDialog;
            if (dialog != null)
                dialog.UpdateProgress();
        }

        private void UpdateProgress()
        {
            if (TaskbarManager.IsPlatformSupported)
                TaskbarManager.Instance.SetProgressValue((int)ProgressValue, (int)Maximum);
        }

        public void UpdateDialog(IDialogDescription description)
        {
            if (description is WaitingDialogDescription)
            {
                IsCancelable = false;
                var dialogDescription = (WaitingDialogDescription) description;
                Title = dialogDescription.TitleText;
                HeaderText = dialogDescription.HeaderText;
                MainText = dialogDescription.MainText;
                DescriptionText = dialogDescription.DescriptionText;
                Topmost = dialogDescription.TopMost;
                if (dialogDescription.BorderLess)
                {
                    BorderBrush = Brushes.Black;
                    BorderThickness = new Thickness(1.5);
                    WindowStyle = WindowStyle.None;

                }
                else
                {
                    WindowStyle = WindowStyle.SingleBorderWindow;
                }
            }
        }
    }
}