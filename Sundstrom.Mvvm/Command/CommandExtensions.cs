using System.Windows.Input;

namespace Sundstrom.Mvvm.Command
{
    public static class CommandExtensions
    {
        public static void Execute(this ICommand command)
        {
            command.Execute(null);
        }

        public static bool CanExecute(this ICommand command)
        {
            return command.CanExecute(null);
        }
    }
}