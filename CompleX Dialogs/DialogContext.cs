using System.Timers;
using System.Windows;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls
{
    public class DialogContext<TDialog> where TDialog : Window , IStaticDialog, new()
    {
        private static readonly object SyncObject = new object();
        private static DialogContext<TDialog> current;
        private readonly Timer timer;
        private int pendingCalls;
        private IDialogDescription description;
        private DialogController<TDialog> dialogController;

        private DialogContext()
        {
            timer = new Timer(1900);
            timer.Elapsed+=ShowDialog;
        }

        public TDialog DialogInstance
        {
            get
            {
                if (DialogIsVisible)
                {
                    if (dialogController != null && dialogController.DialogInstance != null)
                        return dialogController.DialogInstance;
                }
                return null;   
            }
        }

        public void Update()
        {
            lock (SyncObject)
            {
                dialogController.UpdateControl();
            }
        }

        public static DialogContext<TDialog> Current
        {
            get
            {
                lock (SyncObject)
                {
                    return current ?? (current = new DialogContext<TDialog>());
                }
            }
        }

        public IDialogDescription Description
        {
            get {
                lock (SyncObject)
                {
                    return description;
                }
            }
            set 
            {
                lock(SyncObject)
                {
                    if (description!=value)
                    {
                        //description = new WaitingDialogDescription(value);
                        description = value;
                        if (dialogController!=null)
                            dialogController.DialogDescription = description;
                    }
                }
            }
        }

        public void ShowDialog(bool useTimer)
        {
            lock (SyncObject)
            {
                pendingCalls++;
                if (pendingCalls == 1)
                {
                    timer.Enabled = useTimer;
                    if(!useTimer)
                        ShowDialog(this,null); 
                }
            }
        }

        public void ShowDialog()
        {
            lock (SyncObject)
            {
                ShowDialog(true);
            }                        
        }

        public bool DialogIsVisible
        {
            get
            {
                return GetPendingCalls() > 0;
            }
        }

        public int GetPendingCalls()
        {
            lock (SyncObject)
            {
                return pendingCalls;
            }
        }

        public void CloseDialog()
        {
            lock (SyncObject)
            {
                pendingCalls--;
                if (pendingCalls <= 0)
                {
                    StopAll();
                }
                
            }
        }

        public void CloseImmediatly()
        {
            lock (SyncObject)
            {
                StopAll();
            }
        }

        private void StopAll()
        {
            pendingCalls = 0;
            timer.Enabled = false;
            if (dialogController!=null)
                dialogController.Stop();
        }

        private void ShowDialog(object sender, ElapsedEventArgs e)
        {
            lock (SyncObject)
            {
                if (description.IsValid)
                {
                    timer.Enabled = false;
                    dialogController = DialogController<TDialog>.Start<TDialog>(description);
                }
            }
        }

    }
}