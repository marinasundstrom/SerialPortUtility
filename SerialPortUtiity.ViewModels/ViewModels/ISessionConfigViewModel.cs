using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SerialPortUtility.Services;

namespace SerialPortUtility.ViewModels
{
    public interface ISessionSetupViewModel : IViewModel
    {    
        string SerialPort { get; set; }
        IEnumerable<string> SerialPorts { get; set; }
        int BaudRate { get; set; }
        IEnumerable<int> BaudRateItems { get; set; }
        Parity Parity { get; set; }
        IEnumerable<Parity> ParityItems { get; set; }
        int DataBits { get; set; }
        IEnumerable<int> DataBitsItems { get; set; }
        StopBits StopBits { get; set; }
        IEnumerable<StopBits> StopBitsItems { get; set; }
        Handshake Handshake { get; set; }
        IEnumerable<Handshake> HandshakesItems { get; set; }
        string Encoding { get; set; }
        IEnumerable<string> Encodings { get; set; }
        bool DtrEnabled { get; set; }
        bool RtsEnabled { get; set; }
        int PushDelay { get; set; }

        ICommand StartCommand { get; }
        ICommand CancelCommand { get; }

        Format OutputFormat { get; set; }
        IEnumerable<Format> OutputFormatList { get; set; }

        Format InputFormat { get; set; }
        IEnumerable<Format> IntputFormatList { get; set; }

        string NewLine { get; set; }

        bool PrintInputToScreen { get; set; }
        ICommand MakeDefaultCommand { get; }
        void Initialize();
    }
}