using System.Collections.Generic;
using System.Windows.Input;

namespace SerialPortUtility.ViewModels
{
    public interface ISettingsViewModel : IViewModel
    {
        IEnumerable<string> FontFamilyList { get; set; }

        string FontFamily { get; set; }

        int FontSize { get; set; }

        IEnumerable<string> ColorList { get; set; }

        string ForegroundColor { get; set; }

        string BackgroundColor { get; set; }

        ICommand OkCommand { get; }
        ICommand CancelCommand { get; }
        ICommand ApplyCommand { get; }

        void Initialize();
    }
}