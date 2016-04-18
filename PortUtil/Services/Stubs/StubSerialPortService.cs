using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortUtility.Services.Stubs
{
    public class StubSerialPortService : ISerialPortService
    {
        private readonly Random _rand = new Random((int) DateTime.Now.Ticks);
        private byte[] bytes;

        public Encoding Encoding { get; set; }
        public bool DtrEnable { get; set; }
        public bool RtsEnable { get; set; }
        public int DataBits { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public Handshake Handshake { get; set; }

        public void Write(string value)
        {
            if (DataReceived != null)
            {
                byte[] bytes = Encoding.Default.GetBytes("Test");
                BytesToRead = bytes.Length;
                DataReceived(this, new EventArgs());
            }
        }

        public void Write(char value)
        {
            if (DataReceived != null)
            {
                byte[] bytes = Encoding.Default.GetBytes("Test");
                BytesToRead = bytes.Length;
                DataReceived(this, new EventArgs());
            }
        }

        public async void Write(byte[] buffer, int offset, int count)
        {
            if (DataReceived != null)
            {
                string str = Encoding.ASCII.GetString(buffer, offset, count);

                //var str = buffer.Sum(z => z).ToString();

                bytes = Encoding.Default.GetBytes(str);

                //bytes = BitConverter.GetBytes(
                //    buffer.Sum(z => z));

                int x = _rand.Next()%1500 + 0;
                await Task.Delay(x);

                BytesToRead = bytes.Length;
                DataReceived(this, new EventArgs());
            }
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            Array.Copy(bytes, 0, buffer, offset, count);

            BytesToRead = 0;
        }

        public int ReadByte()
        {
            return -1;
        }

        public int BytesToRead { get; private set; }
        public bool IsOpen { get; private set; }
        public string SerialPort { get; set; }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

        public event EventHandler DataReceived;

        public IEnumerable<string> GetPortNames()
        {
            return new[] {"COM1", "COM2"};
        }
    }
}