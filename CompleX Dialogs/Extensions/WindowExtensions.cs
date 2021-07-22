using System;
using System.Windows;
using CompleX.Presentation.Controls.interfaces;

namespace CompleX.Presentation.Controls.Extensions
{
    public static class WindowExtensions
    {
        public static void CheckInvoke(this Window window, Action action)
        {
            if (window != null)
            {
                if (window.Dispatcher.CheckAccess())
                    action();
                else
                    window.Dispatcher.Invoke(action);
            }
        }

        public static TResult CheckInvoke<TResult>(this Window window, Func<TResult> action)
        {
            TResult result = default(TResult);
            if (window != null)
            {
                if (window.Dispatcher.CheckAccess())
                    return action();

                Action procAction = () => result = action();
                window.Dispatcher.Invoke(procAction);
            }
            return result;
        }

        public static void TryInvoke(this IProgressDialog window, Action action)
        {
            if (window is Window)
                ((Window)window).CheckInvoke(action);
            else if (window is System.Windows.Controls.Control)
                ((System.Windows.Controls.Control)window).CheckInvoke(action);
            else if (window is System.Windows.Forms.Control)
                ((System.Windows.Forms.Control)window).CheckInvoke(action);
            else
                throw new Exception("Wrong Type");
            
        }

        public static TResult TryInvoke<TResult>(this IProgressDialog window, Func<TResult> action)
        {
            if (window is Window)
                return ((Window)window).CheckInvoke(action);
            if (window is System.Windows.Controls.Control)
                return ((System.Windows.Controls.Control)window).CheckInvoke(action);
            if (window is System.Windows.Forms.Control)
                return ((System.Windows.Forms.Control)window).CheckInvoke(action);
            throw new Exception("Wrong Type");  
        }

    }
}
