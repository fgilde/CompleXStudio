using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CompleX_Library.Helper
{
    public static class XMLHelper
    {
        public static string GetAttribute(this XElement element, string attribute)
        {
            string result = String.Empty;

            try
            {
                if (element.Attribute(attribute) != null)
                    return element.Attribute(attribute).Value;

                if (element.Attribute(attribute.ToLower()) != null)
                    return element.Attribute(attribute.ToLower()).Value;

                if (element.Attribute(attribute.ToUpper()) != null)
                    return element.Attribute(attribute.ToUpper()).Value;
            }

            catch
            {
                return result;
            }
            return result;
        }


        public static string GetValue(this XElement element, string xname)
        {
            try
            {
                var tmp = element.Descendants(xname).FirstOrDefault();
                if (tmp != null)
                    return tmp.Value;
                return String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }

        public static bool GetValue(this XElement element, string xname, bool defaultvalue)
        {
            string value = element.GetValue(xname).ToLower();
            if (value == "true")
                return true;
            if (value == "false")
                return false;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static int GetValue(this XElement element, string xname, int defaultvalue)
        {
            string value = element.GetValue(xname).ToLower();
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultvalue;
            }
        }


    }
}