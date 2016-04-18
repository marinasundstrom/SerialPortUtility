namespace SerialPortUtility.Services.Stubs
{
    public class StubSettingsService : ISettingsService
    {
        public StubSettingsService()
        {
            Encoding = "UTF-8";
            DataBits = 8;
            BaudRate = 9600;
            Parity = Parity.None;
            StopBits = StopBits.One;
            Handshake = Handshake.None;
            ForegroundColor = "White";
            BackgroundColor = "Black";
            PortName = "COM1";

            FontFamily = "Consolas";
            FontSize = 13;
        }

        public string Encoding { get; set; }
        public int DataBits { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public Handshake Handshake { get; set; }
        public bool DtrEnable { get; set; }
        public bool RtsEnable { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string PortName { get; set; }
        public Format InputFormat { get; set; }
        public Format OutputFormat { get; set; }
        public string NewLine { get; set; }
        public bool PrintInput { get; set; }
        public int PushDelay { get; set; }
        public string FontFamily { get; set; }
        public int FontSize { get; set; }

        public void Save()
        {
        }

        public void Open()
        {
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }
    }
}