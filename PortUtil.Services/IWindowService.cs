namespace SerialPortUtility.Services
{
    public interface IWindowService
    {
        void Show(string name);

        bool ShowDialog(string name);

        void CloseActive();
    }
}