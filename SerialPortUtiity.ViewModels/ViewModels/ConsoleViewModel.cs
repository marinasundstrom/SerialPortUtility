using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SerialPortUtility.Command;
using SerialPortUtility.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SerialPortUtility.ViewModels
{
    public sealed class ConsoleViewModel : ViewModelBase
    {
        private bool _isSessionOpen;
        private Session _session;
        private string _title;

        public ConsoleViewModel(
            IConsoleService consoleService,
            ISerialPortService serialPortService,
            ISettingsService settingsService,
            IClipboardService clipboardService,
            IDialogService dialogService,
            IApplicationServices applicationServices,
            IWindowService windowService,
            IPrinterService printerService,
            ITaskHelpers taskHelpers)
        {
            // Services

            ConsoleService = consoleService;
            SerialPortService = serialPortService;
            SettingsService = settingsService;
            ClipboardService = clipboardService;
            DialogService = dialogService;
            ApplicationServices = applicationServices;
            WindowService = windowService;
            PrinterService = printerService;
            TaskHelpers = taskHelpers;


            // Commands

            NewSessionCommand = new AsyncRelayCommand(NewSessionCommandImpl);
            EndSessionCommand = new RelayCommand(EndSessionCommandImpl);
            SaveCommand = new AsyncRelayCommand(SaveCommandImpl);
            PrintCommand = new RelayCommand(PrintCommandImpl);
            ClearCommand = new RelayCommand(ClearCommandImpl);
            CopyCommand = new RelayCommand(CopyCommandImpl);
            PasteCommand = new AsyncRelayCommand(PasteCommandImpl);
            PasteAndSendCommand = new AsyncRelayCommand(PasteAndSendCommandImpl);
            PasteAndSendLinesCommand = new AsyncRelayCommand(PasteAndSendLinesCommandImpl);
            CutCommand = new AsyncRelayCommand(CutCommandImpl);
            SelectAllCommand = new RelayCommand(SelectAllCommandImpl);
            QuitCommand = new RelayCommand(QuitCommandImpl);
            SettingsCommand = new RelayCommand(SettingsCommandImpl);

            Title = "Serial Port Utility";
        }

        public ITaskHelpers TaskHelpers { get; private set; }

        public IPrinterService PrinterService { get; private set; }

        public IWindowService WindowService { get; private set; }

        public IApplicationServices ApplicationServices { get; private set; }

        public IDialogService DialogService { get; private set; }

        public IClipboardService ClipboardService { get; private set; }

        public ISettingsService SettingsService { get; private set; }
        public IConsoleService ConsoleService { get; private set; }
        public ISerialPortService SerialPortService { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public ICommand SettingsCommand { get; private set; }

        public void Initialize()
        {
            ApplyConsoleSettings();

            ConsoleService.IsEnabled = false;
        }

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSessionOpen
        {
            get { return _isSessionOpen; }
            private set
            {
                _isSessionOpen = value;
                RaisePropertyChanged();
            }
        }

        public Session Session
        {
            get { return _session; }
            private set
            {
                _session = value;
                RaisePropertyChanged("Session");
            }
        }

        public ICommand NewSessionCommand { get; private set; }
        public ICommand EndSessionCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public ICommand PasteAndSendCommand { get; private set; }
        public ICommand PasteAndSendLinesCommand { get; private set; }
        public ICommand CutCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand SelectAllCommand { get; private set; }

        private void SettingsCommandImpl()
        {
            WindowService.ShowDialog("SettingsWindow");
        }

        private void ApplyConsoleSettings()
        {
            ConsoleService.FontFamily = SettingsService.FontFamily;
            ConsoleService.FontSize = SettingsService.FontSize;

            ConsoleService.Foreground = SettingsService.ForegroundColor;
            ConsoleService.Background = SettingsService.BackgroundColor;
        }

        private async Task PasteAndSendCommandImpl()
        {
            try
            {
                if (ConsoleService.IsEnabled)
                {
                    string text = ClipboardService.GetText();

                    int index = ConsoleService.SelectionStart;
                    await ConsoleService.InsertText(index, text);

                    SerialPortService.Write(text);
                }
            }
            catch (ArgumentException e)
            {
                await DialogService.ShowErrorDialogAsync(e.Message, "Exception");
            }
        }

        private async Task PasteAndSendLinesCommandImpl()
        {
            try
            {
                if (ConsoleService.IsEnabled)
                {
                    string text = ClipboardService.GetText();

                    var lines = Regex.Split(text, $"(?={SettingsService.NewLine})");
                    if (lines.Length > 1)
                    {
                        foreach (var line in lines)
                        {
                            if (!Session.IsRunning)
                            {
                                // The session has ended break the loop.
                                break;
                            }

                            int index = ConsoleService.SelectionStart;
                            await ConsoleService.InsertText(index, line);

                            SerialPortService.Write(line);

                            if (SettingsService.LinePushDelay > 0)
                            {
                                await TaskHelpers.Delay(SettingsService.LinePushDelay);
                            }
                        }
                    }
                }
            }
            catch (ArgumentException e)
            {
                await DialogService.ShowErrorDialogAsync(e.Message, "Exception");
            }
        }

        private void SelectAllCommandImpl()
        {
            ConsoleService.SelectAll();
        }

        private async Task SaveCommandImpl()
        {
            Stream stream = null;
            if (DialogService.ShowSaveFileDialog("", "Text files (*.txt)|*.txt|All files (*.*)|*.*", out stream))
            {
                try
                {
                    using (var streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.AutoFlush = true;
                        string text = ConsoleService.GetText();
                        streamWriter.Write(text);
                    }
                }
                catch (Exception)
                {
                    await DialogService.ShowErrorDialogAsync("Failed to save to file.", "Error");
                }
            }
        }

        private void PrintCommandImpl()
        {
            PrinterService.PrintText(
                ConsoleService.GetText());
        }

        private void EndSessionCommandImpl()
        {
            if (Session != null)
            {
                Session.EndSessionCommand.Execute(null);

                IsSessionOpen = false;

                Session.Started -= ConsoleViewModel_SessionStarted;
                Session.Ended -= OnSessionEnded;

                Title = "Serial Port Utility";
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void QuitCommandImpl()
        {
            ApplicationServices.Exit();
        }

        private async Task CutCommandImpl()
        {
            if (ConsoleService.IsEnabled)
            {
                try
                {
                    var str = ConsoleService.CutSelectedInput();
                    ClipboardService.SetText(str);
                }
                catch (InvalidOperationException e)
                {
                    await DialogService.ShowErrorDialogAsync(e.Message, "Exception");
                }
            }
        }

        private async Task PasteCommandImpl()
        {
            if (ConsoleService.IsEnabled)
            {
                try
                {

                    string text = ClipboardService.GetText();
                    int index = ConsoleService.SelectionStart;
                    await ConsoleService.InsertText(index, text);

                }
                catch (ArgumentException e)
                {
                    await DialogService.ShowErrorDialogAsync(e.Message, "Exception");
                }
            }
        }

        private void CopyCommandImpl()
        {
            if (ConsoleService.IsEnabled)
            {
                string text = ConsoleService.SelectedText;
                ClipboardService.SetText(text);
            }
        }

        private void ClearCommandImpl()
        {
            ConsoleService.Clear();
        }

        public async Task NewSessionCommandImpl()
        {
            if (Session != null)
            {
                if (Session.IsRunning)
                {
                    bool result =
                        await DialogService.ShowYesOrNoDialogAsync(
                            "There is already an open session.\n\nDo you really want to end the current session?",
                            "End session");

                    if (result)
                    {
                        EndSessionCommand.Execute(null);
                    }
                    else
                        return;
                }
            }

            if (WindowService.ShowDialog("SessionSetupWindow").GetValueOrDefault())
            {
                Session = new Session(ConsoleService, SerialPortService, SettingsService, DialogService);

                Session.Started += ConsoleViewModel_SessionStarted;
                Session.Ended += OnSessionEnded;

                Session.StartSessionCommand.Execute(null);

                IsSessionOpen = true;

                Title = $"{SerialPortService.SerialPort} - Serial Port Utility";
            }
        }

        private void OnSessionEnded(object sender, EventArgs eventArgs)
        {
            ConsoleService.IsCaretVisible = false;
        }

        private void ConsoleViewModel_SessionStarted(object sender, EventArgs e)
        {
            ConsoleService.IsCaretVisible = true;
        }

        ~ConsoleViewModel()
        {
            //Session.EndSessionCommand.Execute(null);
        }
    }
}