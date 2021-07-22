//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using CompleX.Controls;
using CompleX.Dialogs;
using CompleX.Properties;
using CompleX_Settings;
using CompleX_Settings.Constants;

namespace CompleX.Services
{
    public class MessageService
    {
        /// <summary>
        /// Asks the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static bool Ask(string message, string caption)
        {
            return ShowConfirmation(message, caption) == DialogResult.Yes;
        }


        public static bool AskDsa(string message, string caption, string dsaKey)
        {
            return ShowConfirmation(message, caption, false, MessageIconType.Question,dsaKey,Resources.DsaDefaultText) == DialogResult.Yes;
        }

        public static bool AskDsa(string message, string caption, string dsaKey, MessageIconType icon)
        {
            return ShowConfirmation(message, caption, false, icon, dsaKey, Resources.DsaDefaultText) == DialogResult.Yes;
        }

        public static bool AskDsaOnYes(string message, string caption, string dsaKey)
        {
            bool result = AskDsa(message, caption, dsaKey);
            if(!result)
                DeleteDsaKey(dsaKey);
            return result;
        }

        public static bool AskDsaOnNo(string message, string caption, string dsaKey)
        {
            bool result = AskDsa(message, caption, dsaKey);
            if (result)
                DeleteDsaKey(dsaKey);
            return result;
        }

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        public static void ShowError(string message, string caption)
        {
            ShowMessage(message, caption, MessageIconType.Error);
        }

        public static void ShowError(string message)
        {
            ShowError(message,Resources.Exception);
        }

        /// <summary>
        /// Shows the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        public static void ShowWarning(string message, string caption)
        {
            ShowMessage(message, caption, MessageIconType.Warning);
        }

        /// <summary>
        /// Shows the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        public static void ShowInformation(string message, string caption)
        {
            ShowMessage(message, caption, MessageIconType.Information);
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="icon">The icon.</param>
        public static void ShowMessage(string message, string caption, MessageIconType icon)
        {
           ShowMessage( message, caption,  icon, String.Empty, String.Empty);     
        }

        /// <summary>
        /// Shows the confirmation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmation(string message, string caption)
        {
            return ShowConfirmation(message, caption, false, MessageIconType.Question);
        }

        /// <summary>
        /// Shows the confirmation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="allowCancel">if set to <c>true</c> [allow cancel].</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmation(string message, string caption, bool allowCancel)
        {
            return ShowConfirmation(message, caption, allowCancel,MessageIconType.Question);
        }

        /// <summary>
        /// Shows the confirmation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="allowCancel">if set to <c>true</c> [allow cancel].</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmation(string message, string caption, bool allowCancel, MessageIconType icon)
        {
            return ShowConfirmation(message, caption, allowCancel, icon,String.Empty,String.Empty); 
        }

        /// <summary>
        /// Shows the confirmation.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="allowCancel">if set to <c>true</c> [allow cancel].</param>
        /// <param name="icon">The icon.</param>
        /// <param name="dsaKey">Key for dont show again</param>
        /// <param name="dsaText">Text example (Dont Show this message again)</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmation(string message,string caption, bool allowCancel, MessageIconType icon, string dsaKey, string dsaText )
        {
            DialogResult result = DialogResult.No;
            bool checkBoxChecked;
            dsaKey = FillDsaKey(dsaKey);
            // Prüfen ob Message überhaupt angezeigt werden muss.
            if(!String.IsNullOrEmpty(dsaKey))
            {
                result = Settings.Get(dsaKey, DialogResult.Ignore);
                if (result != DialogResult.Ignore)
                    return result;
            }

            using (var exceptionControl = new EditExceptionControl(message, icon, false))
            {
                if (!allowCancel)
                {
                    var dlg = BaseDialogHelper.CreateBaseDialog(exceptionControl);
                    dlg.MaximizeBox = false;
                    dlg.TopMost = true;
                    dlg.Text = caption;
                    dlg.OnAccept = () => result = DialogResult.Yes;
                    dlg.OnCancel = () => result = DialogResult.No;
                    dlg.OkBtn.Text = Resources.Yes;
                    dlg.CancelBtn.Text = Resources.No;
                    dlg.CheckBox.Visible = !String.IsNullOrEmpty(dsaKey) && !String.IsNullOrEmpty(dsaText);
                    dlg.Text = dsaText;
                    dlg.ShowDialog();
                    checkBoxChecked = dlg.CheckBox.Checked;
                }else
                {
                    var dlg = BaseDialogHelper.CreateBaseDialog(exceptionControl);
                    dlg.MaximizeBox = false;
                    dlg.TopMost = true;
                    dlg.Text = caption;
                    dlg.OnAccept = () => result = DialogResult.Yes;
                    dlg.OnCancel = () => result = DialogResult.Cancel;
                    dlg.OnNo = () => result = DialogResult.No;
                    dlg.OkBtn.Text = Resources.Yes;
                    dlg.NoBtn.Visible = true;
                    dlg.CheckBox.Visible = !String.IsNullOrEmpty(dsaKey) && !String.IsNullOrEmpty(dsaText);
                    dlg.CheckBox.Text = dsaText;
                                 
                    dlg.ShowDialog();
                    checkBoxChecked = dlg.CheckBox.Checked;
                }
            }
            if (!String.IsNullOrEmpty(dsaKey) && checkBoxChecked)
            {
                Settings.Set(dsaKey, result);  
            }
            return result;
        }

        public static void DeleteDsaKey(string dsakey)
        {
            dsakey = FillDsaKey(dsakey);
            Settings.Remove(dsakey);
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="dsaKey"></param>
        /// <param name="dsaText"></param>
        public static void ShowMessage(string message, string caption, MessageIconType icon, string dsaKey, string dsaText)
        {
            dsaKey = FillDsaKey(dsaKey);
            // Prüfen ob Message überhaupt angezeigt werden muss.
            if (!String.IsNullOrEmpty(dsaKey))
            {
                DialogResult result = Settings.Get(dsaKey, DialogResult.Ignore);
                if (result != DialogResult.Ignore)
                    return;
            }

            using (var exceptionControl = new EditExceptionControl(message, icon, false))
            {
                var dlg = BaseDialogHelper.CreateBaseDialog(exceptionControl);
                dlg.MaximizeBox = false;
                dlg.TopMost = true;
                dlg.Text = caption;
                dlg.OkBtn.Visible = false;
                dlg.CancelBtn.Text = Resources.Ok;
                dlg.CheckBox.Visible = !String.IsNullOrEmpty(dsaKey) && !String.IsNullOrEmpty(dsaText);
                dlg.CheckBox.Text = dsaText;
            
                dlg.ShowDialog();
                if (!String.IsNullOrEmpty(dsaKey) && dlg.CheckBox.Checked)
                {
                    Settings.Set(dsaKey, DialogResult.OK);
                }
            }

        }

        public static IEnumerable<string> GetAllUsedDsaKeys()
        {
            return Settings.GetAllKeys().Where(
                s => s.StartsWith(Const.DsaDefault) && s.EndsWith(CompleX_Studio.Version.ToString()));
        }

        public static void DeleteAllUsedDsaKeys()
        {
            var unusedKeys = GetAllUsedDsaKeys().ToList();
            foreach (string key in unusedKeys)
                Settings.Remove(key);
        }

        public static string FillDsaKey(string s)
        {
            if(!String.IsNullOrEmpty(s))
                return Const.DsaDefault + s + CompleX_Studio.Version;
            return String.Empty;
        }


    }
}