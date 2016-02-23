using System;
using System.Windows.Input;

namespace SerialPortUtility.ViewModels
{
    public interface ISession
    {
        ICommand StartSessionCommand { get; }
        ICommand EndSessionCommand { get; }

        bool IsRunning { get; }

        event EventHandler Started;
        event EventHandler Ended;
    }
}