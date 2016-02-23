using System;
using System.Threading.Tasks;

namespace SerialPortUtility.Services.Stubs
{
    public class StubTaskHelpers : ITaskHelpers
    {
        public async Task Delay(int milliseconds)
        {
        }

        public async Task Delay(TimeSpan timeSpan)
        {
        }
    }
}