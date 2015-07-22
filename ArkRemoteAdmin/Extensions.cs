using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ArkRemoteAdmin
{
    static class Extensions
    {
        public static bool GetValue(this XElement element, bool defaultValue)
        {
            if (element != null)
                return XmlConvert.ToBoolean(element.Value);

            return defaultValue;
        }

        public static DateTime GetValue(this XElement element, DateTime defaultValue)
        {
            if (element != null)
                return XmlConvert.ToDateTime(element.Value, XmlDateTimeSerializationMode.Local);

            return defaultValue;
        }

        public static string GetValue(this XElement element, string defaultValue)
        {
            if (element != null)
                return element.Value;

            return defaultValue;
        }
    }
}
