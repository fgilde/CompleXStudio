using System.Threading;
using System.Windows;
using System.Windows.Forms;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls
{
    public class DialogController <TDialogWindow>
        where TDialogWindow: Window, IStaticDialog, new()

    {
        private IDialogDescription dialogDescription;
        private readonly object syncObject = new object();
        private TDialogWindow dialog;

        public IDialogDescription DialogDescription
        {
            get {
                lock (syncObject)
                {
                    return dialogDescription; 
                }
            }
            set {
                lock (syncObject)
                {
                    dialogDescription = value;
                    UpdateControl();
                }
            }
        }

        public TDialogWindow DialogInstance
        {
            get
            {
                lock (syncObject)
                {
                    if (dialog != null)
                            return dialog;
                        return dialog;
                }
            }
        }

        public void UpdateControl()
        {
            if (dialog!=null)
            {
                dialog.Dispatcher.BeginInvoke(new MethodInvoker(UpdateDialogWithDescription));
            }
        }

        private void UpdateDialogWithDescription()
        {
            dialog.UpdateDialog(DialogDescription);
        }


        public DialogController(IDialogDescription description)
        {
            DialogDescription = description;
        }

        public void Run()
        {
            lock (syncObject)
            {
                if(dialog == null)
                   dialog = new TDialogWindow();
                UpdateDialogWithDescription();
            }
            dialog.DisplayDialog();
        }

        public void Stop()
        {
            lock (syncObject)
            {
                if (dialog != null)
                {
                    IStaticDialog currDialog = this.dialog;
                    dialog.Dispatcher.BeginInvoke(new MethodInvoker(currDialog.CloseDialog));
                    dialog = null;
                }
            }
        }

        /// <summary>
        /// creates and starts automatically an dialog controller
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static DialogController<TDialog> Start<TDialog>(IDialogDescription description) where TDialog : Window, IStaticDialog, new()
        {
            var controller = new DialogController<TDialog>(description);
            var thread = new Thread(controller.Run);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return controller;
        }
    }
}