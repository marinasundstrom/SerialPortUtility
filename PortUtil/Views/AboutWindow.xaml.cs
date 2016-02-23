using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace SerialPortUtility.Views
{
    /// <summary>
    ///     Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            Loaded += AboutWindow_Loaded;
        }

        private void AboutWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VersionTextBlock.Text = string.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LinkTextBlock_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/robertsundstrom/SerialPortUtility/");
        }
    }
}