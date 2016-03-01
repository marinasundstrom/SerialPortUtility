using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SerialPortUtility.ViewModels
{
    public interface IViewModel : INotifyDataErrorInfo
    {
        bool IsValid { get; }
    }
}
