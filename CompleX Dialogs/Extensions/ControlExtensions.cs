//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;

namespace CompleX.Presentation.Controls.Extensions
{
    public static class ControlExtensions
    {
        public static void CheckInvoke(this System.Windows.Forms.Control control, Action action)
        {
            if (control == null || !control.InvokeRequired)
                action();
            else
            {
                try
                {
                    control.Invoke(action);
                }
                catch (ObjectDisposedException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        public static TResult CheckInvoke<TResult>(this System.Windows.Forms.Control control, Func<TResult> action)
        {
            if (control == null || !control.InvokeRequired)
                return action();

            TResult result = default(TResult);
            Action procAction = () => result = action();
            try
            {
                control.Invoke(procAction);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            return result;
        }

        public static void CheckInvoke(this System.Windows.Controls.Control ctrl, Action action)
        {
            if (ctrl == null || ctrl.Dispatcher.CheckAccess())
                action();
            else
                ctrl.Dispatcher.Invoke(action);
        }

        public static TResult CheckInvoke<TResult>(this System.Windows.Controls.Control ctrl, Func<TResult> action)
        {
            if (ctrl == null || ctrl.Dispatcher.CheckAccess())
                return action();

            TResult result = default(TResult);
            Action procAction = () => result = action();
            ctrl.Dispatcher.Invoke(procAction);
            return result;
        }

    }
}
