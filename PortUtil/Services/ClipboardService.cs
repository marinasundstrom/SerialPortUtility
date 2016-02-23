using System.Windows;

namespace SerialPortUtility.Services
{
    public sealed class ClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            Clipboard.SetText(text);
        }

        public string GetText()
        {
            return Clipboard.GetText();
        }

        public void ClearText()
        {
            Clipboard.Clear();
        }
    }
}