using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SerialPortUtility.Services
{
    public class ConsoleFormatService : IConsoleFormatService
    {
        public IEnumerable<string> GetFontFamilies()
        {
            return Fonts.SystemFontFamilies.Select(x => x.ToString());
        }

        public IEnumerable<string> GetColors()
        {
            return Enum.GetNames(typeof (ConsoleColor));
        }
    }
}