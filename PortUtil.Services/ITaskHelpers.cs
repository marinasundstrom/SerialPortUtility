using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortUtility.Services
{
    public interface ITaskHelpers
    {

        Task Delay(int milliseconds);
        Task Delay(TimeSpan timeSpan);
    }
}
