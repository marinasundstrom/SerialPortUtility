using System;
using System.Globalization;
using System.Windows.Data;
using SerialPortUtility.Services;

namespace SerialPortUtility.Converters
{
    public class HandshakeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Handshake) value)
            {
                case Handshake.None:
                    return "None";

                case Handshake.RequestToSend:
                    return "Request-to-Send (RTS)";

                case Handshake.XOnXOff:
                    return "XON/XOFF";

                case Handshake.RequestToSendXOnXOff:
                    return "Both";
            }

            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}