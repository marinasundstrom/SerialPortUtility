using System;
using System.Configuration;
using System.Reflection;

namespace SerialPortUtility.Services
{
    public class SettingsService : ISettingsService
    {
        private Configuration _configuration;

        public SettingsService()
        {
            Open();
        }

        private KeyValueConfigurationCollection Settings
        {
            get { return _configuration.AppSettings.Settings; }
        }

        public void Open()
        {
            _configuration = ConfigurationManager.OpenExeConfiguration(
                Assembly.GetExecutingAssembly().Location);
        }

        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public void Save()
        {
            _configuration.Save(ConfigurationSaveMode.Modified);
        }

        public string Encoding
        {
            get
            {
                if (Settings["encoding"] == null)
                    return "ASCII";

                return Settings["encoding"].Value;
            }
            set { Settings.AddOrUpdate("encoding", value); }
        }

        public int DataBits
        {
            get
            {
                string value;

                if (Settings.TryGetKey("dataBits", out value))
                {
                    return int.Parse(value);
                }

                return 8;
            }
            set { Settings.AddOrUpdate("dataBits", value.ToString()); }
        }

        public int BaudRate
        {
            get
            {
                string value;

                if (Settings.TryGetKey("baudRate", out value))
                {
                    return int.Parse(value);
                }

                return 9600;
            }
            set { Settings.AddOrUpdate("baudRate", value.ToString()); }
        }

        public Parity Parity
        {
            get
            {
                string value;

                if (Settings.TryGetKey("parity", out value))
                {
                    return (Parity) Enum.Parse(typeof (Parity), value);
                }

                return Parity.None;
            }
            set { Settings.AddOrUpdate("parity", value.ToString()); }
        }

        public StopBits StopBits
        {
            get
            {
                string value;

                if (Settings.TryGetKey("stopBits", out value))
                {
                    return (StopBits) Enum.Parse(typeof (StopBits), value);
                }

                return StopBits.One;
            }
            set { Settings.AddOrUpdate("stopBits", value.ToString()); }
        }

        public Handshake Handshake
        {
            get
            {
                string value;

                if (Settings.TryGetKey("handshake", out value))
                {
                    return (Handshake) Enum.Parse(typeof (Handshake), value);
                }

                return Handshake.None;
            }
            set { Settings.AddOrUpdate("handshake", value.ToString()); }
        }

        public bool DtrEnable
        {
            get
            {
                string value;

                if (Settings.TryGetKey("dtrEnable", out value))
                {
                    return bool.Parse(value);
                }

                return false;
            }
            set { Settings.AddOrUpdate("dtrEnable", value.ToString()); }
        }

        public bool RtsEnable
        {
            get
            {
                string value;

                if (Settings.TryGetKey("rtsEnable", out value))
                {
                    return bool.Parse(value);
                }

                return false;
            }
            set { Settings.AddOrUpdate("rtsEnable", value.ToString()); }
        }

        public string ForegroundColor
        {
            get
            {
                string value;

                if (Settings.TryGetKey("foregroundColor", out value))
                {
                    return value;
                }

                return "White";
            }
            set { Settings.AddOrUpdate("foregroundColor", value); }
        }

        public string BackgroundColor
        {
            get
            {
                string value;

                if (Settings.TryGetKey("backgroundColor", out value))
                {
                    return value;
                }

                return "Black";
            }
            set { Settings.AddOrUpdate("backgroundColor", value); }
        }

        public string PortName
        {
            get
            {
                if (Settings["portName"] == null)
                    return null;

                return Settings["portName"].Value;
            }
            set { Settings.AddOrUpdate("portName", value); }
        }

        public Format InputFormat
        {
            get
            {
                if (Settings["inputFormat"] == null)
                    return Format.Text;

                return (Format) Enum.Parse(typeof (Format), Settings["inputFormat"].Value);
            }
            set { Settings.AddOrUpdate("inputFormat", value.ToString()); }
        }

        public Format OutputFormat
        {
            get
            {
                if (Settings["outputFormat"] == null)
                    return Format.Text;

                return (Format) Enum.Parse(typeof (Format), Settings["outputFormat"].Value);
            }
            set { Settings.AddOrUpdate("outputFormat", value.ToString()); }
        }

        public string NewLine
        {
            get
            {
                string value;

                if (Settings.TryGetKey("newLine", out value))
                {
                    return value;
                }

                return Environment.NewLine;
            }
            set { Settings.AddOrUpdate("newLine", value); }
        }

        public bool PrintInput
        {
            get
            {
                if (Settings["printInput"] == null)
                    return true;

                return bool.Parse(Settings["printInput"].Value);
            }
            set { Settings.AddOrUpdate("printInput", value.ToString()); }
        }

        public int PushDelay
        {
            get
            {
                if (Settings["pushDelay"] == null)
                    return 0;

                return int.Parse(Settings["pushDelay"].Value);
            }
            set { Settings.AddOrUpdate("pushDelay", value.ToString()); }
        }

        public string FontFamily
        {
            get
            {
                string value;

                if (Settings.TryGetKey("fontFamily", out value))
                {
                    return value;
                }

                return "Consolas";
            }
            set { Settings.AddOrUpdate("fontFamily", value); }
        }

        public int FontSize
        {
            get
            {
                string value;

                if (Settings.TryGetKey("fontSize", out value))
                {
                    return int.Parse(value);
                }

                return 12;
            }
            set { Settings.AddOrUpdate("fontSize", value.ToString()); }
        }
    }
}