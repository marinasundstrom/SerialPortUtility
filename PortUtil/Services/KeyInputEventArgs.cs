using System;

namespace SerialPortUtility.Services
{
    public class KeyInputEventArgs : EventArgs
    {
        public KeyInputEventArgs(char charValue)
        {
            Char = charValue;
        }

        public char Char { get; private set; }
    }
}