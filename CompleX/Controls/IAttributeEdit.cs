using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompleX_Types;

namespace CompleX.Controls
{
    interface IAttributeEdit
    {
        TagAttribute Attribute { get; set;}
        void Init();
    }
}
