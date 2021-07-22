//============================================================================================
// Projekt:			CompleX Studio 
//
// (C) Copyright Florian Gilde
// http://www.nksoft.de
//
// Alle Rechte vorbehalten. All rights reserved.
//============================================================================================
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CompleX_Types
{
    public class CustomCulture:CultureInfo 
    {
        #region Constructor

        public CustomCulture(string name) : base(name)
        {
        }

        public CustomCulture(string name, bool useUserOverride) : base(name, useUserOverride)
        {
        }

        public CustomCulture(int culture) : base(culture)
        {
        }

        public CustomCulture(int culture, bool useUserOverride) : base(culture, useUserOverride)
        {
        }

        public CustomCulture(CultureInfo culture):base(culture.Name)
        {

        }

        #endregion

        public override string ToString()
        {
            return DisplayName + " ("+IetfLanguageTag+")";}

    }
}
