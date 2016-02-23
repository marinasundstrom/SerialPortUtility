using Microsoft.Practices.ServiceLocation;
using SerialPortUtility.Services;
using SerialPortUtility.Services.Stubs;
using Sundstrom.NuIoc;

namespace SerialPortUtility.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var nuioc = new NuContainer();

            ServiceLocator.SetLocatorProvider(() => nuioc);

            // Services


            nuioc.Register<IConsoleService, ConsoleService>();

#if DEBUG
            nuioc.Register<ISerialPortService>(() => new StubSerialPortService());
#else
            nuioc.Register<ISerialPortService>(() => new SerialPortService());
#endif

            nuioc.Register<ISettingsService, SettingsService>();
            nuioc.Register<IClipboardService, ClipboardService>();
            nuioc.Register<IDialogService, DialogService>();
            nuioc.Register<IApplicationServices, ApplicationServices>();
            nuioc.Register<IWindowService, WindowService>();
            nuioc.Register<IPrinterService, PrinterService>();
            nuioc.Register<IConsoleFormatService, ConsoleFormatService>();
            nuioc.Register<ITaskHelpers, TaskHelpers>();

            // ViewModels

            nuioc.Register<IConsoleViewModel, ConsoleViewModel>();
            nuioc.RegisterTransient<ISessionSetupViewModel, SessionSetupViewModel>();
            nuioc.RegisterTransient<ISettingsViewModel, SettingsViewModel>();
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

        public IConsoleViewModel ConsoleViewModel
        {
            get { return ServiceLocator.Current.GetInstance<IConsoleViewModel>(); }
        }

        public ISessionSetupViewModel SessionSetupViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ISessionSetupViewModel>(); }
        }

        public ISettingsViewModel SettingsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<ISettingsViewModel>(); }
        }
    }
}