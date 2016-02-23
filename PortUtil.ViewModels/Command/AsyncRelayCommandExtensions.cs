using System.Windows.Input;

namespace SerialPortUtility.Command
{
    public static class AsyncRelayCommandExtensions
    {
        public static AsyncRelayCommand AsAsyncRelayCommand(this ICommand command)
        {
            return (AsyncRelayCommand) command;
        }

        public static AsyncRelayCommand<T> AsAsyncRelayCommand<T>(this ICommand command)
        {
            return (AsyncRelayCommand<T>)command;
        }
    }
}