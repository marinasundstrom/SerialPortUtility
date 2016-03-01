using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SerialPortUtility.Services;

namespace SerialPortUtility.Views
{
    public abstract class WindowBase : Window, IWindow, INotifyPropertyChanged
    {
        public WindowBase()
        {
            StateChanged += WindowBase_StateChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsMaximized
        {
            get { return WindowState == WindowState.Maximized; }

            set { WindowState = value ? WindowState.Maximized : WindowState.Normal; }
        }

        public bool IsMinimized
        {
            get { return WindowState == WindowState.Minimized; }

            set { WindowState = value ? WindowState.Minimized : WindowState.Normal; }
        }

        public bool IsNormal
        {
            get { return WindowState == WindowState.Normal; }
        }

        private void WindowBase_StateChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsMaximized");
            OnPropertyChanged("IsMinimized");
            OnPropertyChanged("IsNormal");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}