using System;
using System.Linq;
using System.Windows;

namespace SerialPortUtility.Services
{
    public class WindowService : IWindowService
    {
        private const string ViewNamespace = "SerialPortUtility.Views.";

        public void Show(string name, bool closeCurrent = false)
        {
            var previouslyActiveWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

            var type = Type.GetType($"{ViewNamespace}{name}");
            var instance = (Window)Activator.CreateInstance(type);

            if (previouslyActiveWindow != null && closeCurrent)
            {
                instance.Loaded += (s, e) =>
                {
                    previouslyActiveWindow.Close();
                };
            }

            instance.Show();
        }

        public bool? ShowDialog(string name)
        {
            var type = Type.GetType($"{ViewNamespace}{name}");
            var instance = (Window)Activator.CreateInstance(type);
            return instance.ShowDialog();
        }
    }
}