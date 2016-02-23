using System.ComponentModel;
using System.Windows;
using SerialPortUtility.ViewModels;

namespace SerialPortUtility.Views
{
    /// <summary>
    ///     Interaction logic for SessionSetupWindow.xaml
    /// </summary>
    public partial class SessionSetupWindow : WindowBase
    {
        public SessionSetupWindow()
        {
            InitializeComponent();

            Loaded += SessionSetupWindow_Loaded;
        }

        public ISessionSetupViewModel ViewModel
        {
            get { return (ISessionSetupViewModel) DataContext; }
        }

        private void SessionSetupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}