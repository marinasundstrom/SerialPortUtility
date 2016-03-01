using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using SerialPortUtility.Services;
using GalaSoft.MvvmLight.CommandWpf;

namespace SerialPortUtility.ViewModels
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private string _backgroundColor;
        private IEnumerable<string> _colorList;
        private string _fontFamily;
        private IEnumerable<string> _fontFamilyList;
        private int _fontSize;
        private string _foregroundColor;

        public SettingsViewModel(
            IConsoleService consoleService,
            ISettingsService settingsService,
            ISerialPortService serialPortService,
            IConsoleFormatService consoleFormatService)
        {
            ConsoleService = consoleService;
            SettingsService = settingsService;
            SerialPortService = serialPortService;
            ConsoleFormatService = consoleFormatService;

            OkCommand = new RelayCommand(OkCommandImpl);
            CancelCommand = new RelayCommand(CancelCommandImpl);
            ApplyCommand = new RelayCommand(ApplyCommandImpl);
        }

        public IConsoleFormatService ConsoleFormatService { get; private set; }
        public ISerialPortService SerialPortService { get; private set; }
        public ISettingsService SettingsService { get; private set; }
        public IConsoleService ConsoleService { get; private set; }

        public void Initialize()
        {
            ColorList = ConsoleFormatService.GetColors();
            if (SettingsService.ForegroundColor == null)
            {
                ForegroundColor = "White";
            }
            else
            {
                ForegroundColor = SettingsService.ForegroundColor;
            }

            if (SettingsService.BackgroundColor == null)
            {
                BackgroundColor = "Black";
            }
            else
            {
                BackgroundColor = SettingsService.BackgroundColor;
            }

            FontFamilyList = ConsoleFormatService.GetFontFamilies();
            if (SettingsService.FontFamily == null)
            {
                FontFamily = FontFamilyList.FirstOrDefault();
            }
            else
            {
                FontFamily = SettingsService.FontFamily;
            }

            FontSize = SettingsService.FontSize;
        }

        public IEnumerable<string> FontFamilyList
        {
            get { return _fontFamilyList; }
            set
            {
                _fontFamilyList = value;
                RaisePropertyChanged();
            }
        }

        public string FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Font size must be at least 1.")] 
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();

                ValidateProperty();
            }
        }

        public IEnumerable<string> ColorList
        {
            get { return _colorList; }
            set
            {
                _colorList = value;
                RaisePropertyChanged();
            }
        }

        public string ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                _foregroundColor = value;
                RaisePropertyChanged();
            }
        }

        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        private void ApplyCommandImpl()
        {
            ApplySettings();
        }

        private void OkCommandImpl()
        {
            SaveSettings();
            ApplySettings();
        }

        private void ApplySettings()
        {
            ConsoleService.FontFamily = FontFamily;
            ConsoleService.FontSize = FontSize;

            ConsoleService.Foreground = ForegroundColor;
            ConsoleService.Background = BackgroundColor;
        }

        private void SaveSettings()
        {
            SettingsService.FontFamily = FontFamily;
            SettingsService.FontSize = FontSize;

            SettingsService.ForegroundColor = ForegroundColor;
            SettingsService.BackgroundColor = BackgroundColor;

            SettingsService.Save();
        }

        private void CancelCommandImpl()
        {
        }
    }
}