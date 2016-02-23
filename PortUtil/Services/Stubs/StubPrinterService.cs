namespace SerialPortUtility.Services.Stubs
{
    public class StubPrinterService : IPrinterService
    {
        public bool PrintText(string text, string description = null)
        {
            return true;
        }
    }
}