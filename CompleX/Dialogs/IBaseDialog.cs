using System;
using System.Windows.Forms;
using CompleX_Library;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public interface IBaseDialog
    {
        /// <summary>
        /// Gets or sets a value indicating whether [visible in task bar].
        /// </summary>
        /// <value><c>true</c> if [visible in task bar]; otherwise, <c>false</c>.</value>
        bool VisibleInTaskBar { get; set; }

        /// <summary>
        /// Action for Ok 
        /// </summary>
        Action OnAccept { get; set; }

        /// <summary>
        /// Action f
        /// </summary>
        Action OnCancel { get; set; }

        /// <summary>
        /// Action f
        /// </summary>
        Action OnNo { get; set; }

        /// <summary>
        /// Gets or sets the is valid.
        /// </summary>
        Func<ValidationResult> IsValid { get; set; }

        /// <summary>
        /// CheckBox
        /// </summary>
        System.Windows.Forms.CheckBox CheckBox { get; set; }

        /// <summary>
        /// TopMod´st
        /// </summary>
        bool TopMost { get; set; }

        /// <summary>
        /// STartPos
        /// </summary>
        FormStartPosition StartPosition { get; set; }

        /// <summary>
        /// Caption
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// MAximizeBox
        /// </summary>
        bool MaximizeBox { get; set; }

        bool MinimizeBox { get; set; }
        SimpleButton CancelBtn { get; set; }
        SimpleButton OkBtn { get; set; }
        SimpleButton NoBtn { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        DialogResult DialogResult { get; set; }
        IButtonControl AcceptButton { get; set; }

        /// <summary>
        /// Show
        /// </summary>
        void Show();

        /// <summary>
        /// ShowModal
        /// </summary>
        /// <returns></returns>
        DialogResult ShowDialog();

        void HideButtonRow  ();
    }
}