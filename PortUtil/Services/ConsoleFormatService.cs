using System;
using System.Collections.Generic;
using System.Linq;

namespace SerialPortUtility.Services
{
    public class ConsoleFormatService : IConsoleFormatService
    {
        private readonly string[] array =
        {
            "Consolas",
            "Lucida Console"
        };

        public IEnumerable<string> GetFontFamilies()
        {
            return array.AsEnumerable();
        }

        public IEnumerable<string> GetColors()
        {
            return Enum.GetNames(typeof (ConsoleColor));
        }
    }
}