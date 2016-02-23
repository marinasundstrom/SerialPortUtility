using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using SerialPortUtility.Services;
using SerialPortUtility.ViewModels;
using Sundstrom.Mvvm.Command;

namespace SerialPortUtility.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            InitializeComponent();

            MaximizeScreenCommand = new RelayCommand(() =>
            {
                if (IsMaximized)
                {
                    IsMaximized = false;
                }
                else
                {
                    IsMaximized = true;
                }
            });

            Loaded += MainWindow_Loaded;
        }

        public ICommand MaximizeScreenCommand { get; private set; }

        public ConsoleViewModel ViewModel
        {
            get { return (ConsoleViewModel) DataContext; }
        }

        public ConsoleService ConsoleService
        {
            get { return ((ConsoleService) ViewModel.ConsoleService); }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConsoleService.SetTextBox(ConsoleBox);

            ConsoleBox.Focus();
            ConsoleBox.IsReadOnlyCaretVisible = false;

            ViewModel.Initialize();
        }

        private void WindowBase_Closing(object sender, CancelEventArgs e)
        {
            if (ViewModel.IsSessionOpen)
            {
                MessageBoxResult results =
                    MessageBox.Show(
                        "Exiting the program will end the current session.\n\nDo you really want to end the session?",
                        "End session", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (results)
                {
                    case MessageBoxResult.Yes:
                        break;

                    case MessageBoxResult.No:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }
    }
}