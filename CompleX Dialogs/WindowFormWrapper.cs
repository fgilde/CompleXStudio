using System;
using System.Windows;
using System.Windows.Forms;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls
{
    public class WindowFormWrapper<TForm> : Window, IStaticDialog, IProgressDialog
        where TForm : Form,IStaticDialog, new()
    {
        private TForm form;

        public WindowFormWrapper()
        {
            form = new TForm();
        }

        public void UpdateDialog(IDialogDescription description)
        {
            if (form != null)
                form.UpdateDialog(description);
        }

        public void DisplayDialog()
        {
            form.ShowDialog();
        }

        public void CloseDialog()
        {
            form.Close();
        }

        public string DescriptionText
        {
            get { 
                if (form is IProgressDialog) 
                    return ((IProgressDialog)form).DescriptionText;
                return String.Empty; 
            }
            set
            {
                if (form is IProgressDialog)
                    ((IProgressDialog)form).DescriptionText = value;              
            }
        }

        public double ProgressValue
        {
            get 
            {
                if (form is IProgressDialog)
                    return ((IProgressDialog)form).ProgressValue;
                return -1;
            }
            set
            {
                if (form is IProgressDialog)
                    ((IProgressDialog)form).ProgressValue = value;
            }
        }

        public double Maximum
        {
            get
            {
                if (form is IProgressDialog)
                    return ((IProgressDialog)form).Maximum;
                return -1;
            }
            set
            {
                if (form is IProgressDialog)
                    ((IProgressDialog)form).Maximum = value;
            }
        }

        public bool IsIndeterminate
        {
            get
            {
                if (form is IProgressDialog)
                    return ((IProgressDialog)form).IsIndeterminate;
                return false;
            }
            set
            {
                if (form is IProgressDialog)
                    ((IProgressDialog)form).IsIndeterminate = value;
            }
        }
    }
}
