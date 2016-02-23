using System;
using System.Linq;
using System.Windows;

namespace SerialPortUtility.Services
{
    public class WindowService : IWindowService
    {
        private string ViewNamespace = "SerialPortUtility.Views.";

        private static Window ActiveWindow
        {
            get { return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive); }
        }

        public void Show(string name)
        {
            name = ViewNamespace + name;
            Type type = Type.GetType(name);
            var window = (Window) Activator.CreateInstance(type, null);
            window.Owner = ActiveWindow;
            window.Show();
        }

        public bool ShowDialog(string name)
        {
            name = ViewNamespace + name;
            Type type = Type.GetType(name);
            var window = (Window) Activator.CreateInstance(type, null);
            window.Owner = ActiveWindow;
            return (bool) window.ShowDialog();
        }

        public void CloseActive()
        {
            ActiveWindow.Close();
        }
    }
}