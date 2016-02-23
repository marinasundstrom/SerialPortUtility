namespace SerialPortUtility.Services
{
    public interface IClipboardService
    {
        void SetText(string text);
        string GetText();
        void ClearText();
    }
}