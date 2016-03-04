namespace SerialPortUtility.Services
{
    public interface IWindowService
    {
        void Show(string name, bool closeCurrent = false);

        bool? ShowDialog(string name);
    }
}