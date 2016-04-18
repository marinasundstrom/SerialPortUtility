using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using SerialPortUtility.Services;
using SerialPortUtility.Validation;
using GalaSoft.MvvmLight.CommandWpf;
using System.Text.RegularExpressions;

namespace SerialPortUtility.ViewModels
{
    public class SessionSetupViewModel : ViewModelBase, ISessionSetupViewModel
    {
        private int _baudRate;
        private IEnumerable<int> _baudRateItems;
        private int _dataBits;
        private IEnumerable<int> _dataBitsItems;
        private bool _dtrEnabled;
        private string _encoding;
        private IEnumerable<string> _encodings;
        private Handshake _handshake;
        private IEnumerable<Handshake> _handshakesItems;
        private Format _inputFormat;
        private IEnumerable<Format> _intputFormatList;
        private Format _outputFormat;
        private IEnumerable<Format> _outputFormatList;
        private Parity _parity;
        private IEnumerable<Parity> _parityItems;
        private bool _printInputToScreen;
        private int _pushDelay;
        private bool _rtsEnabled;
        private string _serialPort;
        private IEnumerable<string> _serialPorts;
        private StopBits _stopBits;
        private IEnumerable<StopBits> _stopBitsItems;
        private string _newLine;

        public SessionSetupViewModel(
            IConsoleService consoleService,
            ISettingsService settingsService,
            ISerialPortService serialPortService,
            IConsoleFormatService consoleFormatService,
            IDialogService dialogService)
        {
            // Services

            ConsoleService = consoleService;
            SettingsService = settingsService;
            SerialPortService = serialPortService;
            ConsoleFormatService = consoleFormatService;
            DialogService = dialogService;

            // Commands

            StartCommand = new RelayCommand(StartCommandImpl);
            CancelCommand = new RelayCommand(CancelCommandImpl);
            MakeDefaultCommand = new RelayCommand(MakeDefaultCommandImpl);
        }

        public IDialogService DialogService { get; private set; }

        public IConsoleFormatService ConsoleFormatService { get; private set; }
        public ISerialPortService SerialPortService { get; private set; }
        public ISettingsService SettingsService { get; private set; }
        public IConsoleService ConsoleService { get; private set; }

        public ICommand MakeDefaultCommand { get; private set; }

        public void Initialize()
        {
            SerialPorts = SerialPortService.GetPortNames();
            if (SettingsService.PortName == null)
            {
                SerialPort = SerialPorts.FirstOrDefault();
            }
            else
            {
                SerialPort = SettingsService.PortName;
            }

            DataBitsItems = new[]
            {
                5, 6, 7, 8
            };
            DataBits = SettingsService.DataBits;

            BaudRateItems = new[]
            {
                9600,
                19200,
                38400,
                57600,
                115200,
                230400,
                460800,
                921600
            };
            BaudRate = SettingsService.BaudRate;

            ParityItems = (Parity[])Enum.GetValues(typeof(Parity));
            Parity = SettingsService.Parity;

            StopBitsItems = ((StopBits[])Enum.GetValues(typeof(StopBits))).Skip(1);
            StopBits = SettingsService.StopBits;

            HandshakesItems = (Handshake[])Enum.GetValues(typeof(Handshake));
            Handshake = SettingsService.Handshake;

            DtrEnabled = SettingsService.DtrEnable;
            RtsEnabled = SettingsService.RtsEnable;

            Encodings = new[]
            {
                "ASCII",
                "UTF-8",
                "Unicode",
                "BigEndianUnicode"
            };
            Encoding = SettingsService.Encoding;

            OutputFormatList = (Format[])Enum.GetValues(typeof(Format));
            OutputFormat = SettingsService.OutputFormat;

            IntputFormatList = (Format[])Enum.GetValues(typeof(Format));
            InputFormat = SettingsService.InputFormat;

            PrintInputToScreen = SettingsService.PrintInput;

            PushDelay = SettingsService.PushDelay;

            NewLine = Regex.Escape(SettingsService.NewLine);
        }

        [NotEmpty]
        public string SerialPort
        {
            get { return _serialPort; }
            set
            {
                    _serialPort = value;
                    RaisePropertyChanged();

                    ValidateProperty();
            }
        }

        public IEnumerable<string> SerialPorts
        {
            get { return _serialPorts; }
            set
            {
                _serialPorts = value;
                RaisePropertyChanged();
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Bit Rate must be at least 1.")] 
        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value;
                RaisePropertyChanged();
                ValidateProperty();
            }
        }

        public IEnumerable<int> BaudRateItems
        {
            get { return _baudRateItems; }
            set
            {
                _baudRateItems = value;
                RaisePropertyChanged();
            }
        }

        public Parity Parity
        {
            get { return _parity; }
            set
            {
                _parity = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<Parity> ParityItems
        {
            get { return _parityItems; }
            set
            {
                _parityItems = value;
                RaisePropertyChanged();
            }
        }

        public int DataBits
        {
            get { return _dataBits; }
            set
            {
                _dataBits = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<int> DataBitsItems
        {
            get { return _dataBitsItems; }
            set
            {
                _dataBitsItems = value;
                RaisePropertyChanged();
            }
        }

        public StopBits StopBits
        {
            get { return _stopBits; }
            set
            {
                _stopBits = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<StopBits> StopBitsItems
        {
            get { return _stopBitsItems; }
            set
            {
                _stopBitsItems = value;
                RaisePropertyChanged();
            }
        }

        public Handshake Handshake
        {
            get { return _handshake; }
            set
            {
                _handshake = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<Handshake> HandshakesItems
        {
            get { return _handshakesItems; }
            set
            {
                _handshakesItems = value;
                RaisePropertyChanged();
            }
        }

        public string Encoding
        {
            get { return _encoding; }
            set
            {
                _encoding = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<string> Encodings
        {
            get { return _encodings; }
            set
            {
                _encodings = value;
                RaisePropertyChanged();
            }
        }

        public bool DtrEnabled
        {
            get { return _dtrEnabled; }
            set
            {
                _dtrEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool RtsEnabled
        {
            get { return _rtsEnabled; }
            set
            {
                _rtsEnabled = value;
                RaisePropertyChanged();
            }
        }

        public ICommand StartCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public Format OutputFormat
        {
            get { return _outputFormat; }
            set
            {
                _outputFormat = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<Format> OutputFormatList
        {
            get { return _outputFormatList; }
            set
            {
                _outputFormatList = value;
                RaisePropertyChanged();
            }
        }

        public Format InputFormat
        {
            get { return _inputFormat; }
            set
            {
                _inputFormat = value;
                RaisePropertyChanged();
            }
        }

        public IEnumerable<Format> IntputFormatList
        {
            get { return _intputFormatList; }
            set
            {
                _intputFormatList = value;
                RaisePropertyChanged();
            }
        }

        [NotEmpty]
        public string NewLine
        {
            get { return _newLine; }
            set
            {
                _newLine = value;
                RaisePropertyChanged();
            }
        }

        public bool PrintInputToScreen
        {
            get { return _printInputToScreen; }
            set
            {
                _printInputToScreen = value;
                RaisePropertyChanged();
            }
        }

        [ExpectInt32]
        [Range(0, int.MaxValue, ErrorMessage = "Value is out of range.")]
        public int PushDelay
        {
            get { return _pushDelay; }
            set
            {
                _pushDelay = value;
                RaisePropertyChanged();
                ValidateProperty();
            }
        }

        private void CancelCommandImpl()
        {
        }

        private void StartCommandImpl()
        {
            if (IsValid)
            {
                SerialPortService.SerialPort = SerialPort;

                SerialPortService.DataBits = DataBits;
                SerialPortService.BaudRate = BaudRate;
                SerialPortService.DtrEnable = DtrEnabled;
                SerialPortService.RtsEnable = RtsEnabled;
                SerialPortService.Parity = Parity;
                SerialPortService.Handshake = Handshake;
                SerialPortService.StopBits = StopBits;

                SerialPortService.Encoding = System.Text.Encoding.GetEncoding(Encoding);
                ConsoleService.Encoding = System.Text.Encoding.GetEncoding(Encoding);

                ConsoleOutput.InputFormat = InputFormat;
                ConsoleOutput.OutputFormat = OutputFormat;

                ConsoleService.PrintInput = PrintInputToScreen;

                ConsoleService.NewLine = Regex.Unescape(NewLine);
            }
            else
            {
                DialogService.ShowErrorDialogAsync("Invalid parameters.", "Error");
            }
        }

        private void MakeDefaultCommandImpl()
        {
            SettingsService.PortName = SerialPort;
            SettingsService.BaudRate = BaudRate;
            SettingsService.DataBits = DataBits;
            SettingsService.DtrEnable = DtrEnabled;
            SettingsService.RtsEnable = RtsEnabled;
            SettingsService.Parity = Parity;
            SettingsService.Handshake = Handshake;
            SettingsService.StopBits = StopBits;
            SettingsService.Encoding = Encoding;
            SettingsService.InputFormat = InputFormat;
            SettingsService.OutputFormat = OutputFormat;
            SettingsService.PrintInput = PrintInputToScreen;
            SettingsService.PushDelay = PushDelay;
            SettingsService.NewLine = Regex.Unescape(NewLine);

            SettingsService.Save();
        }
    }
}