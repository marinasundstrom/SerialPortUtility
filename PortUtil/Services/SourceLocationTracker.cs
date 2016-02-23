using System;

namespace SerialPortUtility.Services
{
    public class SourceLocationTracker
    {
        /// <summary>
        ///     The current index relative to line.
        /// </summary>
        public int LineIndex { get; private set; }

        /// <summary>
        ///     The index on which the current line starts.
        /// </summary>
        public int LineStartIndex { get; private set; }

        public int Count { get; private set; }

        /// <summary>
        ///     The index in the line in which input starts.
        /// </summary>
        public int LineInputStartIndex { get; private set; }

        public int InputAreaLength
        {
            get
            {
                try
                {
                    lock (this)
                    {
                        return Count - InputStartIndex;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        private int InputStartIndex
        {
            get
            {
                lock (this)
                {
                    return (LineStartIndex + LineInputStartIndex);
                }
            }
        }

        public bool HasInputStarted
        {
            get
            {
                lock (this)
                {
                    return LineInputStartIndex != -1;
                }
            }
        }

        public bool IsWithinInputArea(int selectedIndex)
        {
            lock (this)
            {
                return HasInputStarted
                       && selectedIndex >= InputStartIndex
                       && selectedIndex < Count;
            }
        }

        public void NewLine()
        {
            lock (this)
            {
                LineIndex = 0;

                Count++;
                LineStartIndex = Count;
            }
        }

        public void StartInput()
        {
            lock (this)
            {
                LineInputStartIndex = LineIndex;
            }
        }

        public void EndInput()
        {
            lock (this)
            {
                LineInputStartIndex = -1;
            }
        }

        public void Increase()
        {
            lock (this)
            {
                Count++;
                LineIndex++;
            }
        }

        public void Increase(int x)
        {
            lock (this)
            {
                Count += x;
                LineIndex += x;
            }
        }

        public int GetCharLineIndexFromCharStringIndex(int selectedStartIndex)
        {
            lock (this)
            {
                //var lastPartLength = Count - LineStartIndex;
                //var l = Count - selectedStartIndex;

                //return lastPartLength - l;

                int lastPartLength = Count - (LineStartIndex + LineInputStartIndex);
                int l = Count - selectedStartIndex;

                return lastPartLength - l;
            }
        }

        public void Decrease()
        {
            lock (this)
            {
                Count--;
            }
        }

        public void Reset()
        {
            lock (this)
            {
                LineIndex = 0;
                LineStartIndex = 0;
                LineInputStartIndex = 0;
                Count = 0;
            }
        }

        public bool IsOutsideInputArea(int selectedStartIndex)
        {
            return selectedStartIndex
                   < LineStartIndex + LineInputStartIndex;
        }

        public void Decrease(int length)
        {
            lock (this)
            {
                Count -= length;
            }
        }
    }
}