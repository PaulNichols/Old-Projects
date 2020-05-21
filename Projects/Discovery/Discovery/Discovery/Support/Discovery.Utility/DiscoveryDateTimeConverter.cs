using System.ComponentModel;
using System.Globalization;

namespace Discovery.Utility
{
    public class DiscoveryDateTimeConverter : DateTimeConverter
    {
        public new object ConvertFromInvariantString(string text)
        {
            return ConvertFromString(null, CultureInfo.CurrentCulture, text);
        }
    }
}