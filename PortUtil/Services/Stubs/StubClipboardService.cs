namespace SerialPortUtility.Services.Stubs
{
    public class StubClipboardService : IClipboardService
    {
        private string _text;

        public void SetText(string text)
        {
            _text = text;
        }

        public string GetText()
        {
            return _text;
        }

        public void ClearText()
        {
            _text = null;
        }
    }
}