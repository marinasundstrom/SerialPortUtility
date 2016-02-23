using System;
using System.Globalization;
using System.Windows.Data;

namespace SerialPortUtility.Converters
{
    public class BoolToConnectionIndicatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
            {
                return "Connected";
            }
            return "Not connected";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}