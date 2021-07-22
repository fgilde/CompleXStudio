using System;
using System.Windows.Forms;

namespace CompleX.Dialogs
{
    public static class BaseDialogHelper
    {
        public static IBaseDialog CreateBaseDialog()
        {
            return new BaseDialog();
        }

        public static IBaseDialog CreateBaseDialog(Control control)
        {
            return new BaseDialog(control);
        }

        public static IBaseDialog CreateBaseDialog(Type controlType)
        {
            return new BaseDialog(controlType);
        }

        public static DialogResult ShowDialog(Control control)
        {
            return ShowDialog(control, null);
        }

        public static DialogResult ShowDialog(Control control, Action onAccept)
        {
            var dlg = CreateBaseDialog(control);
            dlg.OnAccept = onAccept;
            return dlg.ShowDialog();
        }

        public static DialogResult ShowDialog(Type type)
        {
            return ShowDialog(type, null);
        }

        public static DialogResult ShowDialog(Type type, Action onAccept)
        {
            var dlg = new BaseDialog(type) { OnAccept = onAccept };
            return dlg.ShowDialog();
        }

    }
}