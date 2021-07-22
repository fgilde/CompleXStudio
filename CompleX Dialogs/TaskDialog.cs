﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CompleX.Presentation.Controls
{
    /// <summary>
    /// Example call TaskDialog.Show(this, "HELLO", "Can I ask you a question?", "My application", TaskDialogButtons.Yes | TaskDialogButtons.No | TaskDialogButtons.Cancel, TaskDialogIcon.Warning);
    /// </summary>
    public class TaskDialog
    {
        [DllImport("comctl32.dll", CharSet = CharSet.Unicode, EntryPoint = "TaskDialog")]
        static extern int _TaskDialog(IntPtr hWndParent, IntPtr hInstance, String pszWindowTitle, String pszMainInstruction, String pszContent, int dwCommonButtons, IntPtr pszIcon, out int pnButton);

        public static bool IsSupported
        {
            get
            {
                return Environment.OSVersion.Version.Major >= 6;
            }
        }

        #region Modal

        public static TaskDialogResult Show(IWin32Window owner, string text)
        {
            return Show(owner, text, null, null, TaskDialogButtons.Ok);
        }

        public static TaskDialogResult Show(IWin32Window owner, string text, string instruction)
        {
            return Show(owner, text, instruction, null, TaskDialogButtons.Ok, 0);
        }

        public static TaskDialogResult Show(IWin32Window owner, string text, string instruction, string caption)
        {
            return Show(owner, text, instruction, caption, TaskDialogButtons.Ok, 0);
        }

        public static TaskDialogResult Show(IWin32Window owner, string text, string instruction, string caption, TaskDialogButtons buttons)
        {
            return Show(owner, text, instruction, caption, buttons, 0);
        }

        public static TaskDialogResult Show(IWin32Window owner, string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
        {
            return ShowInternal(owner.Handle, text, instruction, caption, buttons, icon);
        }

        #endregion

        #region Non-modal

        public static TaskDialogResult Show(string text)
        {
            return Show(text, null, null, TaskDialogButtons.Ok);
        }

        public static TaskDialogResult Show(string text, string instruction)
        {
            return Show(text, instruction, null, TaskDialogButtons.Ok, 0);
        }

        public static TaskDialogResult Show(string text, string instruction, string caption)
        {
            return Show(text, instruction, caption, TaskDialogButtons.Ok, 0);
        }

        public static TaskDialogResult Show(string text, string instruction, string caption, TaskDialogButtons buttons)
        {
            return Show(text, instruction, caption, buttons, 0);
        }

        public static TaskDialogResult Show(string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
        {
            return ShowInternal(IntPtr.Zero, text, instruction, caption, buttons, icon);
        }

        #endregion

        #region Core implementation

        private static TaskDialogResult ShowInternal(IntPtr owner, string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
        {
            int p;
            if (_TaskDialog(owner, IntPtr.Zero, caption, instruction, text, (int)buttons, new IntPtr((int)icon), out p) != 0)
                throw new InvalidOperationException("Something weird has happened.");

            switch (p)
            {
                case 1:
                    return TaskDialogResult.Ok;
                case 2:
                    return TaskDialogResult.Cancel;
                case 4:
                    return TaskDialogResult.Retry;
                case 6:
                    return TaskDialogResult.Yes;
                case 7:
                    return TaskDialogResult.No;
                case 8:
                    return TaskDialogResult.Close;
                default:
                    return TaskDialogResult.None;
            }
        }

        #endregion
    }

    [Flags]
    public enum TaskDialogButtons
    {
        Ok = 0x0001,
        Cancel = 0x0008,
        Yes = 0x0002,
        No = 0x0004,
        Retry = 0x0010,
        Close = 0x0020
    }

    public enum TaskDialogIcon
    {
        Information = UInt16.MaxValue - 2,
        Warning = UInt16.MaxValue,
        Stop = UInt16.MaxValue - 1,
        Question = 0, //?
        SecurityWarning = UInt16.MaxValue - 5,
        SecurityError = UInt16.MaxValue - 6,
        SecuritySuccess = UInt16.MaxValue - 7,
        SecurityShield = UInt16.MaxValue - 3,
        SecurityShieldBlue = UInt16.MaxValue - 4,
        SecurityShieldGray = UInt16.MaxValue - 8
    }

    public enum TaskDialogResult
    {
        None,
        Ok,
        Cancel,
        Yes,
        No,
        Retry,
        Close
    }
}
