using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortUtility.Services
{
    public interface ISerialPortService
    {
        Encoding Encoding { get; set; }

        bool DtrEnable { get; set; }

        bool RtsEnable { get; set; }

        int DataBits { get; set; }

        int BaudRate { get; set; }

        Parity Parity { get; set; }

        StopBits StopBits { get; set; }

        Handshake Handshake { get; set; }
        int BytesToRead { get; }

        bool IsOpen { get; }

        string SerialPort { get; set; }

        void Write(string value);

        void Write(char ch);

        void Write(byte[] buffer, int offset, int count);

        void Read(byte[] buffer, int offset, int count);

        int ReadByte();

        void Open();

        void Close();

        event EventHandler DataReceived;

        IEnumerable<string> GetPortNames();
    }
}