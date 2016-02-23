using System.ComponentModel;
using System.Windows;
using SerialPortUtility.ViewModels;

namespace SerialPortUtility.Views
{
    /// <summary>
    ///     Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : WindowBase
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Loaded += SessionSetupWindow_Loaded;
        }

        public ISettingsViewModel ViewModel
        {
            get { return (ISettingsViewModel) DataContext; }
        }

        private void SessionSetupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void WindowBase_Closing(object sender, CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                if (ViewModel.HasErrors)
                {
                    e.Cancel = false;
                }
            }
        }
    }
}