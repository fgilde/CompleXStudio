using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CompleX_Library.Interfaces
{
    public interface IToolWindow: IHostedService
    {
        Icon Image { get; }
    }
}
