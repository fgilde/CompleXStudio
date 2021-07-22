//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using CompleX.Presentation.Controls.Extensions;
using CompleX.Properties;
using CompleX_Library;
using CompleX_Settings.Constants;
using DevExpress.XtraEditors;

namespace CompleX.Dialogs
{
    public partial class BaseDialog : XtraForm, IBaseDialog
    {

        private SystemMenu systemMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        public BaseDialog()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public BaseDialog(Control control):this()
        {
            InitControl(control);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        /// <param name="controlType">Type of the control.</param>
        public BaseDialog(Type controlType)
            : this()
        {
            InitControl(Activator.CreateInstance(controlType) as Control);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible in task bar].
        /// </summary>
        /// <value><c>true</c> if [visible in task bar]; otherwise, <c>false</c>.</value>
        public bool VisibleInTaskBar
        {
            get
            {
                return this.CheckInvoke(() => ShowInTaskbar);
            }
            set
            {
                this.CheckInvoke(() => ShowInTaskbar = value);
            }
        }

        /// <summary>
        /// Action for Ok 
        /// </summary>
        public Action OnAccept { get; set; }

        /// <summary>
        /// Action f
        /// </summary>
        public Action OnCancel { get; set; }

        /// <summary>
        /// Action f
        /// </summary>
        public Action OnNo { get; set; }

        /// <summary>
        /// Gets or sets the is valid.
        /// </summary>
        public Func<ValidationResult> IsValid { get; set; }

        CheckBox IBaseDialog.CheckBox
        {
            get { return CheckBox; }
            set { CheckBox = value; }
        }

        SimpleButton IBaseDialog.CancelBtn
        {
            get { return CancelBtn; }
            set { CancelBtn = value; }
        }

        SimpleButton IBaseDialog.OkBtn
        {
            get { return OkBtn; }
            set { OkBtn = value; }
        }

        SimpleButton IBaseDialog.NoBtn
        {
            get { return NoBtn; }
            set { NoBtn = value; }
        }

        private void InitControl(Control control)
        {
            if (control != null)
            {
                Width = control.Width+15;
                Height = control.Height + 80;
                if (control.MinimumSize.Height > 0 && control.MinimumSize.Width > 0)
                    MinimumSize = new Size(control.MinimumSize.Width+15, control.MinimumSize.Height + 80);

                ContainerPanel.BeginInit();
                ContainerPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                ContainerPanel.EndInit();
            }
        }

        private void DlgOkBtnClick(object sender, EventArgs e)
        {
            if (IsValid == null || IsValid().Result)
            {
                if (OnAccept != null)
                    OnAccept();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }else
            {
                DialogResult = System.Windows.Forms.DialogResult.Abort;
                ErrorProvider.SetError(OkBtn, IsValid().ErrorMessage);
            }
            if (!Modal)
                this.CheckInvoke(Close);
        }

        private void BaseDialogFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult == System.Windows.Forms.DialogResult.Abort;
            if (!e.Cancel && DialogResult == System.Windows.Forms.DialogResult.Cancel && OnCancel != null)
            {
                OnCancel();   
            }
        }


        private void DlgCancelBtnClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if(!Modal)
                this.CheckInvoke(Close);
        }

        private void NoBtnClick(object sender, EventArgs e)
        {
            if (OnNo != null)
                OnNo();
            DialogResult = System.Windows.Forms.DialogResult.No;

            if (!Modal)
                this.CheckInvoke(Close);
        }

        private void NoBtnVisibleChanged(object sender, EventArgs e)
        {
            this.CheckInvoke(() => OkBtn.Left = NoBtn.Visible ? 153 : NoBtn.Left);
        }

        public void HideButtonRow()
        {
            ContainerPanel.Dock = DockStyle.Fill;
        }

        private void BaseDialogShown(object sender, EventArgs e)
        {
            if (!Modal)
            {
                systemMenu = SystemMenu.FromForm(this);
                systemMenu.InsertMenu(0,Const.ToToolWindowId, Resources.AsToolWindow);
                systemMenu.InsertSeparator(1);
            }
        }

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == (int) WindowMessages.wmSysCommand)
            {
                switch (msg.WParam.ToInt32())
                {
                    case Const.ToToolWindowId: // ID of the reset item
                        {
                           CompleX_Studio.FormToToolWindow(this);
                        }
                    break;
                }
            }
            // Call base class function
            base.WndProc(ref msg);

        }

    }
}
