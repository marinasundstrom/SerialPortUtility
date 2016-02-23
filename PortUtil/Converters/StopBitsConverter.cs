using System;
using System.Globalization;
using System.Windows.Data;
using SerialPortUtility.Services;

namespace SerialPortUtility.Converters
{
    public class StopBitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((StopBits) value)
            {
                case StopBits.None:
                    return "None";

                case StopBits.One:
                    return "1";

                case StopBits.OnePointFive:
                    return "1.5";

                case StopBits.Two:
                    return "2";
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}