using System.Windows;

namespace SerialPortUtility
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetCulture();

            base.OnStartup(e);
        }

        private static void SetCulture()
        {
            //var newCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentCulture = newCulture;
            //Thread.CurrentThread.CurrentUICulture = newCulture;

            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            //    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}