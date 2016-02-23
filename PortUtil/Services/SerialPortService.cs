using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace SerialPortUtility.Services
{
    public sealed class SerialPortService : ISerialPortService
    {
        private string _portName;
        private SerialPort _serialPort;
        public string BackgroundColor { get; set; }

        public event EventHandler DataReceived;

        public IEnumerable<string> GetPortNames()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }

        public Encoding Encoding
        {
            get { return _serialPort.Encoding; }

            set { _serialPort.Encoding = value; }
        }


        public bool DtrEnable
        {
            get { return _serialPort.DtrEnable; }

            set { _serialPort.DtrEnable = value; }
        }

        public bool RtsEnable
        {
            get { return _serialPort.RtsEnable; }

            set { _serialPort.RtsEnable = value; }
        }

        public int DataBits
        {
            get { return _serialPort.DataBits; }

            set { _serialPort.DataBits = value; }
        }

        public int BaudRate
        {
            get { return _serialPort.BaudRate; }

            set { _serialPort.BaudRate = value; }
        }

        public Parity Parity
        {
            get { return (Parity) _serialPort.Parity; }

            set { _serialPort.Parity = (System.IO.Ports.Parity) value; }
        }

        public StopBits StopBits
        {
            get { return (StopBits) _serialPort.StopBits; }

            set { _serialPort.StopBits = (System.IO.Ports.StopBits) value; }
        }

        public Handshake Handshake
        {
            get { return (Handshake) _serialPort.Handshake; }

            set { _serialPort.Handshake = (System.IO.Ports.Handshake) value; }
        }

        public bool IsOpen
        {
            get { return _serialPort.IsOpen; }
        }

        public string SerialPort
        {
            get { return _serialPort.PortName; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (_serialPort != null)
                {
                    if (IsOpen)
                        throw new InvalidOperationException("Cannot switch port when the current port is open.");
                }

                _serialPort = new SerialPort(value);
                _serialPort.DataReceived += _serialPort_DataReceived;
            }
        }

        //public string SerialPort
        //{
        //    get { return _portName; }
        //    set
        //    {
        //        _portName = value;
        //    }
        //}


        public void Write(string value)
        {
            _serialPort.Write(value);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            _serialPort.Read(buffer, offset, count);
        }

        public int ReadByte()
        {
            return _serialPort.ReadByte();
        }

        public int BytesToRead
        {
            get { return _serialPort.BytesToRead; }
        }

        public void Open()
        {
            if (_serialPort == null)
                throw new InvalidOperationException("No port has been assigned.");

            _serialPort.Open();
        }

        public void Close()
        {
            _serialPort.Close();
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }
    }
}