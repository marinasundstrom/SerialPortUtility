using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SerialPortUtility.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace SerialPortUtility.ViewModels
{
    public class Session : ObservableObject, ISession
    {
        private bool _isRunning;
        private Task _taskThread;
        private CancellationToken _ct;
        private CancellationTokenSource _cts;

        public Session(
            IConsoleService consoleService,
            ISerialPortService serialPortService,
            ISettingsService settingsService,
            IDialogService dialogService)
        {
            // Services

            ConsoleService = consoleService;
            SerialPortService = serialPortService;
            SettingsService = settingsService;
            DialogService = dialogService;


            // Commands

            StartSessionCommand = new RelayCommand(StartSessionCommandImpl);
            EndSessionCommand = new RelayCommand(EndSessionCommandImpl);
        }

        public IDialogService DialogService { get; private set; }

        public ISettingsService SettingsService { get; private set; }

        public ISerialPortService SerialPortService { get; private set; }

        public IConsoleService ConsoleService { get; private set; }

        public Encoding Encoding
        {
            get { return ConsoleService.Encoding; }
        }

        public event EventHandler Started;
        public event EventHandler Ended;

        public ICommand StartSessionCommand { get; private set; }
        public ICommand EndSessionCommand { get; private set; }

        public bool IsRunning
        {
            get { return _isRunning; }
            private set
            {
                _isRunning = value;
                RaisePropertyChanged();
            }
        }

        private void EndSessionCommandImpl()
        {
            if (_cts != null)
                _cts.Cancel();

            ConsoleService.IsEnabled = false;

            SerialPortService.DataReceived -= SerialPortService_DataReceived;

            SerialPortService.Close();

            IsRunning = false;

            if (Ended != null)
                Ended(this, new EventArgs());
        }

        private void StartSessionCommandImpl()
        {
            ConsoleService.IsCaretVisible = true;

            ConsoleService.IsEnabled = true;
            ConsoleService.Clear();

            WriteHeaderToScreen();

            SerialPortService.DataReceived += SerialPortService_DataReceived;

            SerialPortService.Open();

            StartBackgroundTask();

            IsRunning = true;

            if (Started != null)
                Started(this, new EventArgs());
        }

        private void WriteHeaderToScreen()
        {
            ConsoleService.WriteLine(
                string.Format("Serial Port Utility. Version {0}", SettingsService.Version));
            ConsoleService.WriteLine();
        }

        private void SerialPortService_DataReceived(object sender, EventArgs e)
        {
            var buffer = new byte[SerialPortService.BytesToRead];
            SerialPortService.Read(buffer, 0, buffer.Length);


            switch (ConsoleOutput.OutputFormat)
            {
                case Format.Text:
                    string text = Encoding.GetString(buffer, 0, buffer.Length);
                    ConsoleService.Write(text);
                    break;

                case Format.BytesDecimal:
                    foreach (byte b in buffer)
                    {
                        ConsoleService.Write("{0} ", b);
                    }
                    break;

                case Format.BytesHex:
                    string str = BitConverter.ToString(buffer).Replace("-", " ");
                    ConsoleService.Write("{0} ", str);
                    break;
            }
        }

        private void StartBackgroundTask()
        {
            _cts = new CancellationTokenSource();
            _ct = _cts.Token;
            _taskThread = Task.Factory.StartNew(Thread, _ct);
        }

        private async void Thread()
        {
            bool flag = false;
            string message = string.Empty;
            try
            {
                while (true)
                {
                    string line = await ConsoleService.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line == Environment.NewLine)
                            break;

                        if (line.Trim(' ') == "cls")
                        {
                            ConsoleService.Clear();
                            continue;
                        }

                        byte[] bytes = ProcessLine(line);

                        SerialPortService.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception e)
            {
                flag = true;
                message = e.Message;
            }
            if (flag)
            {
                await DialogService.ShowErrorDialogAsync(message, "Exception");
            }
        }

        private byte[] ProcessLine(string line)
        {
            line = line.TrimEnd('\n');

            switch (ConsoleOutput.InputFormat)
            {
                case Format.Text:
                    return ProcessText(line);

                case Format.BytesDecimal:
                    return ProcessBytesDecimal(line);

                case Format.BytesHex:
                    return ProcessBytesHexString(line);
            }

            throw new Exception();
        }

        private byte[] ProcessText(string line)
        {
            //foreach (var entry in Ge)
            //{
            //    line = line.Replace(entry.Key, entry.Value);
            //}
            return Encoding.GetBytes(line);
        }

        private static byte[] ProcessBytesDecimal(string line)
        {
            string[] numberStrings = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<byte> numbers = numberStrings
                .Select(byte.Parse);
            return numbers.ToArray();
        }

        private static byte[] ProcessBytesHexString(string line)
        {
            string[] numberStrings2 = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<byte> numbers2 = numberStrings2
                .Select(x => (byte) Convert.ToInt32(x, 16));
            return numbers2.ToArray();
        }

        ~Session()
        {
        }
    }
}