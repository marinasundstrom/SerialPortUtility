using System.Text;
using System.Threading.Tasks;

namespace SerialPortUtility.Services.Stubs
{
    public class StubConsoleService : IConsoleService
    {
        public bool IsEnabled { get; set; }
        public string SelectedText { get; private set; }
        public int SelectionStart { get; private set; }
        public int SelectionLength { get; private set; }
        public bool IsCaretVisible { get; set; }
        public bool PrintInput { get; set; }
        public string NewLine { get; set; }
        public string FontFamily { get; set; }
        public int FontSize { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
        public Encoding Encoding { get; set; }

        public async Task<string> ReadLineAsync()
        {
            return "";
        }

        public async Task<char> ReadCharAsync()
        {
            return ' ';
        }

        public void Write(string value)
        {
        }

        public void WriteLine(string value)
        {
        }

        public void WriteLine()
        {
        }

        public void Write(string format, params object[] args)
        {
        }

        public void InsertText(int index, string text)
        {
        }

        public void Clear()
        {
        }

        public string GetText()
        {
            return "";
        }

        public void SelectAll()
        {
        }

        public void Send(bool appendNewLine = true)
        {
        }

        public string CutSelectedInput()
        {
            return "";
        }
    }
}