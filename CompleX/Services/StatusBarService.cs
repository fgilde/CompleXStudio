//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Windows.Forms;
using CompleX.Presentation.Controls.Extensions;
using DevExpress.XtraBars;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace CompleX.Services
{
    public static class StatusBarService
    {

        public static string SimpleText
        {
            get { return CompleX_Studio.Instance.iStatus1.Caption; }
            set { CompleX_Studio.Instance.iStatus1.Caption = value; }
        }

        public static void StartIndeterminateProgress(string text)
        {
            SetProgress(text);
        }

        public static void StopIndeterminateProgress()
        {
            SetProgress(String.Empty, 100, 100);
        }
        public static void SetProgress(string text)
        {
            CompleX_Studio.Instance.CheckInvoke(() =>
            {
                if (TaskbarManager.IsPlatformSupported && TaskbarManager.Instance != null)
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate);
                CompleX_Studio.Instance.labelAction.Visibility = BarItemVisibility.Always;
                CompleX_Studio.Instance.progressBarMarquee.Visibility = BarItemVisibility.Always;
                CompleX_Studio.Instance.labelAction.Caption = text;

            });
            Application.DoEvents();
        }

        public static void SetProgress(string text, int progress, int maximum)
        {
            CompleX_Studio.Instance.CheckInvoke(() =>
            {

                if (TaskbarManager.IsPlatformSupported && TaskbarManager.Instance != null)
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                    TaskbarManager.Instance.SetProgressValue(progress, maximum);
                }
                if (progress <= 0 || progress >= maximum)
                {
                    CompleX_Studio.Instance.labelAction.Visibility = BarItemVisibility.Never;
                    CompleX_Studio.Instance.progressBar.Visibility = BarItemVisibility.Never;
                    CompleX_Studio.Instance.progressBarMarquee.Visibility = BarItemVisibility.Never;
                    if (TaskbarManager.IsPlatformSupported && TaskbarManager.Instance != null)
                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
                else
                {
                    CompleX_Studio.Instance.labelAction.Visibility = BarItemVisibility.Always;
                    CompleX_Studio.Instance.progressBar.Visibility = BarItemVisibility.Always;
                }
                CompleX_Studio.Instance.labelAction.Caption = text;
                CompleX_Studio.Instance.repositoryItemProgressBar1.Maximum = maximum;
                CompleX_Studio.Instance.progressBar.EditValue = progress;
            });
            Application.DoEvents();
        }
    }
}
