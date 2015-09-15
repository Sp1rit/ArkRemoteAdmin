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

        public static int GetValue(this XElement element, int defaultValue)
        {
            if (element != null)
            {
                int outVal;
                if (int.TryParse(element.Value, out outVal))
                    return outVal;
            }

            return defaultValue;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static string ToRcon(this string message)
        {
            return message
                .Replace(Environment.NewLine, "\n")
                .Replace("ä", "ae")
                .Replace("ü", "ue")
                .Replace("ö", "oe")
                .Replace("Ä", "Ae")
                .Replace("Ö", "Oe")
                .Replace("Ü", "Ue");
        }
    }

}
