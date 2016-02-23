using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SerialPortUtility
{
    internal class Program
    {
        private static Format OutputFormat { get; set; }
        private static Encoding Encoding { get; set; }
        private static string PortName { get; set; }
        private static Dictionary<string, string> KeyMap { get; set; }

        private static SerialPort SerialPort { get; set; }

        private static NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        private static void Main(string[] args)
        {
            ShowHeader();

            Init();

            using (SerialPort = new SerialPort(PortName))
            {
                SetUp();

                try
                {
                    SerialPort.Open();
                }
                catch (Exception e)
                {
                    ShowExceptionDialofAndExit(e);
                }

                while (true)
                {
                    string line = Console.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line == Environment.NewLine)
                            break;

                        if (line.Trim(' ') == "cls")
                            Console.Clear();

                        line = ProcessLine(line);

                        SerialPort.Write(line);
                    }
                }
            }
        }

        private static void Init()
        {
            SetLanguage();

            PortName = AppSettings["portName"];

            if (string.IsNullOrEmpty(PortName))
            {
                MessageBox.Show("No port has been specified.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private static void SetLanguage()
        {
            var ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        private static void ShowHeader()
        {
            const string name = "Serial Port Utility";

            const string title = name + ". Version 0.0.1";
            const string copyright = "Copyright (c) Robert Sundström 2014-2016";

            Console.Title = name;

            SetColors();

            Console.WriteLine(title);
            Console.WriteLine(copyright);
            Console.WriteLine();

            Console.WriteLine("Check settings in SerialPortUtility.exe.config.");

            Console.WriteLine();
        }

        private static void SetColors()
        {
            string value = null;

            if (AppSettings.TryGetKey("backgroundColor", out value))
            {
                Console.BackgroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), value);
            }
            if (AppSettings.TryGetKey("foregroundColor", out value))
            {
                Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), value);
            }

            Console.Clear();
        }

        private static void SetUp()
        {
            LoadKeyMap();

            try
            {
                string value = null;

                if (AppSettings.TryGetKey("windowrHeight", out value))
                {
                    Console.WindowHeight = int.Parse(value);
                }
                if (AppSettings.TryGetKey("windowWidth", out value))
                {
                    Console.WindowWidth = int.Parse(value);
                }

                if (AppSettings.TryGetKey("bufferHeight", out value))
                {
                    Console.BufferHeight = int.Parse(value);
                }
                if (AppSettings.TryGetKey("bufferWidth", out value))
                {
                    Console.BufferWidth = int.Parse(value);
                }

                if (AppSettings.TryGetKey("encoding", out value))
                {
                    if (value == "Default")
                    {
                        Encoding = Encoding.Default;
                    }
                    else
                    {
                        Encoding = Encoding.GetEncoding(value);
                    }
                }
                if (AppSettings.TryGetKey("outputFormat", out value))
                {
                    OutputFormat = (Format) Enum.Parse(typeof (Format), value);
                }
                SerialPort.Encoding = Encoding;
                if (AppSettings.TryGetKey("dataBits", out value))
                {
                    SerialPort.DataBits = int.Parse(value);
                }
                if (AppSettings.TryGetKey("baudRate", out value))
                {
                    SerialPort.BaudRate = int.Parse(value);
                }
                if (AppSettings.TryGetKey("parity", out value))
                {
                    SerialPort.Parity = (Parity) Enum.Parse(typeof (Parity), value);
                }
                if (AppSettings.TryGetKey("stopBits", out value))
                {
                    SerialPort.StopBits = (StopBits) Enum.Parse(typeof (StopBits), value);
                }
                if (AppSettings.TryGetKey("handshake", out value))
                {
                    SerialPort.Handshake = (Handshake) Enum.Parse(typeof (Handshake), value);
                }
                if (AppSettings.TryGetKey("dtrEnable", out value))
                {
                    SerialPort.DtrEnable = bool.Parse(value);
                }
                if (AppSettings.TryGetKey("rtsEnable", out value))
                {
                    SerialPort.RtsEnable = bool.Parse(value);
                }
            }
            catch (Exception e)
            {
                ShowExceptionDialofAndExit(e);
            }

            SerialPort.DataReceived += serialPort_DataReceived;
        }

        private static void ShowExceptionDialofAndExit(Exception e)
        {
            DialogResult r = MessageBox.Show(e.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }

        private static string ProcessLine(string line)
        {
            line = line.TrimEnd('\n');

            foreach (var entry in KeyMap)
            {
                line = line.Replace(entry.Key, entry.Value);
            }
            return line;
        }

        private static void LoadKeyMap()
        {
            try
            {
                using (var streamReader = new StreamReader(
                    File.OpenRead("keymapping.json")))
                {
                    string str = streamReader.ReadToEnd();
                    KeyMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
                }
            }
            catch (Exception e)
            {
            }
        }

        private static void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            read();
        }

        private static void read()
        {
            var buffer = new byte[SerialPort.BytesToRead];
            SerialPort.Read(buffer, 0, SerialPort.BytesToRead);

            switch (OutputFormat)
            {
                case Format.Text:
                    string text = Encoding.GetString(buffer);
                    Console.Write(text);
                    break;

                case Format.BytesDecimal:
                    foreach (byte b in buffer)
                    {
                        Console.Write("{0} ", b);
                    }
                    break;

                case Format.BytesHex:
                    string str = BitConverter.ToString(buffer).Replace("-", " ");
                    Console.Write("{0} ", str);
                    break;
            }
        }

        private enum Format
        {
            Text,
            BytesDecimal,
            BytesHex
        }
    }
}