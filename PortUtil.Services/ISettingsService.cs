namespace SerialPortUtility.Services
{
    public interface ISettingsService
    {
        string Encoding { get; set; }

        int DataBits { get; set; }

        int BaudRate { get; set; }

        Parity Parity { get; set; }

        StopBits StopBits { get; set; }

        Handshake Handshake { get; set; }

        bool DtrEnable { get; set; }

        bool RtsEnable { get; set; }

        string ForegroundColor { get; set; }

        string BackgroundColor { get; set; }

        string PortName { get; set; }

        Format InputFormat { get; set; }

        Format OutputFormat { get; set; }

        string NewLine { get; set; }

        bool PrintInput { get; set; }

        int LinePushDelay { get; set; }
        string FontFamily { get; set; }
        int FontSize { get; set; }
        void Save();
        void Open();

        string Version { get; }
    }
}