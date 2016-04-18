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

        public void Add(char value, bool raise = true)
        {
            _buffer.Add(value);

            if (raise && CharAdded != null)
                CharAdded(this, new CharBufferEventArgs(value));
        }

        public void Add(string value, bool raise = true) 
        {
            _buffer.AddRange(value);

            foreach (var ch in value)
            {
                if (raise && CharAdded != null)
                    CharAdded(this, new CharBufferEventArgs(ch));
            }
        }

        public void Insert(int index, char value, bool raise = true)
        {
            _buffer.Insert(index, value);

            if (raise && CharAdded != null)
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

        public char Dequeue()
        {
            var ch = _buffer[0];
            _buffer.RemoveAt(0);
            return ch;
        }

        public void Enqueue(char value, bool raise = true)
        {
            Add(value, raise);
        }

        public void RaiseForAll()
        {
            var buffer = this._buffer.ToArray();
            foreach(var ch in buffer)
            {
                CharAdded(this, new CharBufferEventArgs(ch));
            }
        }

        public override string ToString()
        {
            return new string(_buffer.ToArray());
        }

        public event EventHandler<CharBufferEventArgs> CharAdded;
    }
}