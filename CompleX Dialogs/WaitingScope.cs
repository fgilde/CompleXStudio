using System;
using System.Windows.Forms;
using System.Windows.Input;
using CompleX.Presentation.Controls.DialogDescriptions;
using CompleX.Presentation.Controls.WPFDialogs;
using Application = System.Windows.Application;
using Cursors = System.Windows.Input.Cursors;

namespace CompleX.Presentation.Controls
{
    public class WaitingScope : IDisposable
    {
        private static Application mainApp;
        private static Control ctrl;
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WaitingScope(Control control, WaitingDialogDescription description)
        {
            ctrl = control;
            if (control != null)
            {
                if (!control.InvokeRequired)
                    ctrl.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                else
                    control.Invoke((Action)(() => ctrl.Cursor = System.Windows.Forms.Cursors.WaitCursor));
            }
            ShowWaitDialog(description);
        }

        public WaitingScope(WaitingDialogDescription description)
            : this(Application.Current, description)
        {
          
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public WaitingScope(Application application, WaitingDialogDescription description)
        {     
            if (application == null)
                application = Application.Current;
            mainApp = application;
            if (application != null && application.Dispatcher != null)
            {
                if (application.Dispatcher.CheckAccess())
                    Mouse.OverrideCursor = Cursors.Wait;
                else
                    application.Dispatcher.Invoke((Action)(() => Mouse.OverrideCursor = Cursors.Wait));
            }
            ShowWaitDialog(description);
        }

        private static void ShowWaitDialog(WaitingDialogDescription description)
        {
            DialogContext<WaitingDialog>.Current.Description = (WaitingDialogDescription)description;
            DialogContext<WaitingDialog>.Current.ShowDialog();  
        }

        #region IDisposable Member

        /// <summary>
        /// Freigabe und Wiederherstellung des vorherigen Cursors
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DialogContext<WaitingDialog>.Current.CloseDialog(); 
                if (mainApp != null)
                {
                    if (mainApp.Dispatcher.CheckAccess())
                        Mouse.OverrideCursor = null;
                    else
                        mainApp.Dispatcher.Invoke((Action)(() => Mouse.OverrideCursor = null));
                }

                if (ctrl != null)
                {
                    if (!ctrl.InvokeRequired)
                        ctrl.Cursor = System.Windows.Forms.Cursors.Default;
                    else
                        ctrl.Invoke((Action)(() => ctrl.Cursor = System.Windows.Forms.Cursors.Default));
                }
            }
        }

        #endregion
    }
}