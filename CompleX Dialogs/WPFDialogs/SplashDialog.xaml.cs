using System;
using System.Windows;
using System.Windows.Media.Animation;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls.WPFDialogs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>	
    public partial class SplashDialog : Window, IStaticDialog, IProgressDialog
    {

        #region Dependency Properties


        public string Info
        {
            get { return (string)GetValue(InfoProperty); }
            set { SetValue(InfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InfoProperty =
            DependencyProperty.Register("Info", typeof(string), typeof(SplashDialog), new UIPropertyMetadata(String.Empty));




        public string Version
        {
            get { return (string)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register("Version", typeof(string), typeof(SplashDialog), new UIPropertyMetadata(String.Empty));




        public string Product
        {
            get { return (string)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(string), typeof(SplashDialog), new UIPropertyMetadata(String.Empty));



        public string StatusText
        {
            get { return (string)GetValue(StatusTextProperty); }
            set { SetValue(StatusTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register("StatusText", typeof(string), typeof(SplashDialog), new UIPropertyMetadata(String.Empty));

        /// <summary>
        /// Gibt an, ob es sich um eine unendliche ProgressBar handelt.
        /// </summary>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public string DescriptionText
        {
            get { return StatusText; }
            set { StatusText = value;}
        }

        /// <summary>
        /// Aktueller Fortschrittswert.
        /// </summary>
        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set
            {
                if (value <= 0 || value >= Maximum)
                    progressBar.Visibility = Visibility.Hidden;
                else
                    progressBar.Visibility = Visibility.Visible;

                SetValue(ProgressValueProperty, value);
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
        /// <see cref="IsIndeterminate"/>
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(SplashDialog), new UIPropertyMetadata(false));

        /// <summary>
        /// <see cref="ProgressValue"/>
        /// </summary>
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(SplashDialog), new UIPropertyMetadata(0d));
       
        /// <summary>
        /// <see cref="Maximum"/>
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(SplashDialog), new UIPropertyMetadata(100d));



        #endregion

        public SplashDialog()
        {
            DataContext = this;
            InitializeComponent();
        }

        private static string GetImageUri()
        {
            return "../Resources/Images/splash.png";
        }


        public void UpdateDialog(IDialogDescription description)
        {
            if (description is SplashDialogDescription)
            {
                var dialogDescription = (SplashDialogDescription)description;

                Info = dialogDescription.Info;
                Version = dialogDescription.Version;
                Product = dialogDescription.Product;
                StatusText = dialogDescription.Action;
                Topmost = dialogDescription.TopMost;
                ProgressValue = dialogDescription.ProgressValue;
                Copyright.Content = String.Format(Resource.Copyright, DateTime.Now.Year);
            }
        }

        public void DisplayDialog()
        {
            ShowDialog();
        }
        public void FadeOut()
        {
            var fadeOut = (Storyboard)FindResource("FadeOut");
            if (fadeOut != null)
            {
                fadeOut.Completed += (sender, args) => Close();
                fadeOut.Begin(this);
            }
            else
            {
                Close();
            }
        }
        public void CloseDialog()
        {
            FadeOut();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}