using System.Configuration;

namespace SerialPortUtility.Services
{
    public static class SettingsExtensions
    {
        public static bool TryGetKey(this KeyValueConfigurationCollection collection, string key, out string value)
        {
            KeyValueConfigurationElement x = collection[key];
            if (x != null)
            {
                value = x.Value;
                return true;
            }
            value = null;
            return false;
        }

        public static void AddOrUpdate(this KeyValueConfigurationCollection collection, string key, string value)
        {
            KeyValueConfigurationElement x = collection[key];
            if (x != null)
            {
                x.Value = value;
            }
            else
            {
                collection.Add(key, value);
            }
        }
    }
}