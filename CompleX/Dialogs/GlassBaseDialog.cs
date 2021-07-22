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
using Microsoft.WindowsAPICodePack.Shell;

namespace CompleX.Dialogs
{
    public partial class GlassBaseDialog : GlassForm, IBaseDialog
    {

        private SystemMenu systemMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        public GlassBaseDialog()
        {
            InitializeComponent();

            AeroGlassCompositionChanged += MainFormAeroGlassCompositionChanged;
            UpdateGlassInfo(AeroGlassCompositionEnabled);

            ShowInTaskbar = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            AeroGlassCompositionEnabled = true;
        }

        private void UpdateGlassInfo(bool glassEnabled)
        {
            if (glassEnabled)
            {
                ExcludeControlFromAeroGlass(ContainerPanel);
                ExcludeControlFromAeroGlass(CancelBtn);
                ExcludeControlFromAeroGlass(OkBtn);
                ExcludeControlFromAeroGlass(NoBtn);
                ExcludeControlFromAeroGlass(CheckBox);
                Invalidate();
            }
            else
            {
                BackColor = ContainerPanel.BackColor;
            }
        }

        private void MainFormAeroGlassCompositionChanged(object sender, AeroGlassCompositionChangedEvenArgs e)
        {
            UpdateGlassInfo(e.GlassAvailable);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public GlassBaseDialog(Control control):this()
        {
            InitControl(control);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDialog"/> class.
        /// </summary>
        /// <param name="controlType">Type of the control.</param>
        public GlassBaseDialog(Type controlType)
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

        private void dlgOkBtn_Click(object sender, EventArgs e)
        {
            if (IsValid == null || IsValid().Result)
            {
                if (OnAccept != null)
                    OnAccept();
                DialogResult = DialogResult.OK;
            }else
            {
                DialogResult = DialogResult.Abort;
                ErrorProvider.SetError(OkBtn, IsValid().ErrorMessage);
            }
            if (!Modal)
                this.CheckInvoke(Close);
        }

        private void BaseDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult == DialogResult.Abort;
            if (!e.Cancel && DialogResult == DialogResult.Cancel && OnCancel != null)
            {
                OnCancel();   
            }
        }


        private void dlgCancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            if(!Modal)
                this.CheckInvoke(Close);
        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            if (OnNo != null)
                OnNo();
            DialogResult = DialogResult.No;

            if (!Modal)
                this.CheckInvoke(Close);
        }

        private void NoBtn_VisibleChanged(object sender, EventArgs e)
        {
            this.CheckInvoke(() => OkBtn.Left = NoBtn.Visible ? 153 : NoBtn.Left);
        }

        public void HideButtonRow()
        {
            ContainerPanel.Dock = DockStyle.Fill;
        }

        private void BaseDialog_Shown(object sender, EventArgs e)
        {
            if (!Modal)
            {
                systemMenu = SystemMenu.FromForm(this);
                systemMenu.InsertMenu(0,Const.ToToolWindowId, Resources.AsToolWindow);
                systemMenu.InsertSeparator(1);
            }
            UpdateGlassInfo(AeroGlassCompositionEnabled);
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
