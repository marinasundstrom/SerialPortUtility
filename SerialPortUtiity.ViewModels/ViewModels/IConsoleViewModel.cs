using System.Windows.Input;

namespace SerialPortUtility.ViewModels
{
    public interface IConsoleViewModel : IViewModel
    {
        bool IsSessionOpen { get; }

        ISession Session { get; }

        ICommand NewSessionCommand { get; }

        ICommand SaveCommand { get; }

        ICommand EndSessionCommand { get; }

        ICommand QuitCommand { get; }

        ICommand ClearCommand { get; }

        ICommand CopyCommand { get; }

        /// <summary>
        /// Pastes as string containing one line.
        /// </summary>
        /// <returns></returns>
        ICommand PasteCommand { get; }

        /// <summary>
        /// Pastes as string containing one or many lines and then sends.
        /// </summary>
        /// <returns></returns>
        ICommand PasteAndSendCommand { get; }

        ICommand CutCommand { get; }

        ICommand PrintCommand { get; }

        ICommand SelectAllCommand { get; }

        ICommand SettingsCommand { get; }

        void Initialize();
    }
}