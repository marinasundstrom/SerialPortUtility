using System;
using System.Globalization;
using System.Windows.Data;
using SerialPortUtility.Services;

namespace SerialPortUtility.Converters
{
    public class FormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Format) value)
            {
                case Format.BytesDecimal:
                    return "Bytes (Decimal)";

                case Format.BytesHex:
                    return "Bytes (Hex)";

                case Format.Text:
                    return "Text";
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}