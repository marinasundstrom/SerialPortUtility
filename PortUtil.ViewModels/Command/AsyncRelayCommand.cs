using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerialPortUtility.Command
{
    public sealed class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public AsyncRelayCommand(Func<Task> execute) 
            : this(execute, null)
        {

        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute();
        }

        public Task ExecuteAsync()
        {
            return _execute();
        }

        public bool CanExecute()
        {
            return _canExecute();
        }

        void ICommand.Execute(object parameter)
        {
           _execute();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, new EventArgs());
        }
    }

    public sealed class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<T, bool> _canExecute;

        public AsyncRelayCommand(Func<Task> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public AsyncRelayCommand(Func<Task> execute)
            : this(execute, null)
        {

        }

        bool ICommand.CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute((T)parameter);
        }

        public Task ExecuteAsync(T parameter)
        {
            return _execute();
        }

        public bool CanExecute(T parameter)
        {
            return _canExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, new EventArgs());
        }
    }
}
