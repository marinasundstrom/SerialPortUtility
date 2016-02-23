using System;
using System.Collections.Generic;

namespace SerialPortUtility.Services
{
    public class CharBuffer
    {
        private readonly List<char> _buffer = new List<char>();

        public int Length
        {
            get { return _buffer.Count; }
        }

        public char this[int i]
        {
            get { return _buffer[i]; }
        }

        public void Add(char value)
        {
            _buffer.Add(value);

            if (CharAdded != null)
                CharAdded(this, new CharBufferEventArgs(value));
        }

        public void Insert(int index, char value)
        {
            _buffer.Insert(index, value);

            if (CharAdded != null)
                CharAdded(this, new CharBufferEventArgs(value));
        }

        public void RemoveAt(int index)
        {
            _buffer.RemoveAt(index);
        }

        public void Clear()
        {
            _buffer.Clear();
        }

        public override string ToString()
        {
            return new string(_buffer.ToArray());
        }

        public event EventHandler<CharBufferEventArgs> CharAdded;
    }
}