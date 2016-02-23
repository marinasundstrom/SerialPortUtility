using System.Collections.Specialized;

namespace SerialPortUtility
{
    public static class AppSettingsExtensions
    {
        public static bool TryGetKey(this NameValueCollection collection, string name, out string value)
        {
            value = collection[name];
            return value != null;
        }
    }
}