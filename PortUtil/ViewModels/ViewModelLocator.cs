using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SerialPortUtility.Services;
using SerialPortUtility.Services.Stubs;

namespace SerialPortUtility.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var nuioc = new SimpleIoc();

            ServiceLocator.SetLocatorProvider(() => nuioc);

            // Services

            nuioc.Register<IConsoleService, ConsoleService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                nuioc.Register<ISerialPortService>(() => new StubSerialPortService());
            }
            else
            {
                nuioc.Register<ISerialPortService>(() => new SerialPortService());
            }

            nuioc.Register<ISettingsService, SettingsService>();
            nuioc.Register<IClipboardService, ClipboardService>();
            nuioc.Register<IDialogService, DialogService>();
            nuioc.Register<IApplicationServices, ApplicationServices>();
            nuioc.Register<IWindowService, WindowService>();
            nuioc.Register<IPrinterService, PrinterService>();
            nuioc.Register<IConsoleFormatService, ConsoleFormatService>();
            nuioc.Register<ITaskHelpers, TaskHelpers>();

            // ViewModels

            nuioc.Register<ConsoleViewModel, ConsoleViewModel>();

            nuioc.Register<SessionSetupViewModel, SessionSetupViewModel>();
            nuioc.Register<SettingsViewModel, SettingsViewModel>();
        }

        public ISerialPortService SerialPortService
        {
            get { return ServiceLocator.Current.GetInstance<ISerialPortService>(); }
        }

        public IConsoleService ConsoleService
        {
            get { return ServiceLocator.Current.GetInstance<IConsoleService>(); }
        }

        public ISettingsService SettingsService
        {
            get { return ServiceLocator.Current.GetInstance<ISettingsService>(); }
        }

        public ConsoleViewModel ConsoleViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ConsoleViewModel>(); }
        }

        public SessionSetupViewModel SessionSetupViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SessionSetupViewModel>(); }
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }
    }
}