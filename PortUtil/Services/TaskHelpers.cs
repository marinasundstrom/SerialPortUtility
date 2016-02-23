using System;
using System.Threading.Tasks;

namespace SerialPortUtility.Services
{
    public class TaskHelpers : ITaskHelpers
    {
        public async Task Delay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        public async Task Delay(TimeSpan timeSpan)
        {
            await Task.Delay(timeSpan);
        }
    }
}