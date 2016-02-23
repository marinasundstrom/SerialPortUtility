using System.Windows;

namespace SerialPortUtility.Services
{
    public sealed class ApplicationServices : IApplicationServices
    {
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}