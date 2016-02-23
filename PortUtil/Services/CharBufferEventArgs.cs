using System;

namespace SerialPortUtility.Services
{
    public class CharBufferEventArgs : EventArgs
    {
        public CharBufferEventArgs(char value)
        {
            Value = value;
        }

        public char Value { get; private set; }
    }
}