using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using CompleX.Dialogs;

namespace CompleX
{
    public class RuntimeServiceProvider : IServiceProvider, ITypeDescriptorContext
    {
        object IServiceProvider.GetService(Type serviceType)
        {
            return serviceType == typeof(IWindowsFormsEditorService) ? new EditorService() : null;
        }

        public void OnComponentChanged() { }
        public IContainer Container { get { return null; } }
        public bool OnComponentChanging()
        {
            return true; //um Änderungen zu behalten ..
        }
        public object Instance { get { return null; } }
        public PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }
    }

    public class EditorService : IWindowsFormsEditorService
    {
        public void DropDownControl(Control control)
        {
            control.MinimumSize = control.Size;
            var dialog = BaseDialogHelper.CreateBaseDialog(control);
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;
            dialog.CancelBtn.Visible = false;
            dialog.OkBtn.Left = dialog.CancelBtn.Left;
            dialog.ShowDialog();
        }

        public void CloseDropDown() { }

        public DialogResult ShowDialog(Form dialog)
        {
            dialog.BackColor = Color.Blue;
            return dialog.ShowDialog();
        }
    }


}