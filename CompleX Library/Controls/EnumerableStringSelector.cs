using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace CompleX_Library.Controls
{
    public class EnumerableStringSelector:UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            var service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            var prop = context.PropertyDescriptor as CustomClass.DynamicProperty;
            if(prop == null)
                throw new Exception("Only valid with custom class and CustomClass.DynamicProperty");
            if (service == null)
                return value;
            var control = new EnumerableSelectorControl<string>(
                service,
                value,
                prop.PossibleValues,
                s => s
                );
            service.DropDownControl(control);
            return control.Value;
        }
    }
}