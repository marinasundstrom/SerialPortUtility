using System.Collections.Generic;

namespace SerialPortUtility.Services
{
    public interface IConsoleFormatService
    {
        IEnumerable<string> GetFontFamilies();
        IEnumerable<string> GetColors();
    }
}