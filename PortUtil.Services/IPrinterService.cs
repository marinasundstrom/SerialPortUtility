namespace SerialPortUtility.Services
{
    public interface IPrinterService
    {
        bool PrintText(string text, string description = null);
    }
}