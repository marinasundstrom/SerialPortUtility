using System.Text;
using System.Threading.Tasks;

namespace SerialPortUtility.Services
{
    public interface IConsoleService
    {
        bool IsEnabled { get; set; }

        string SelectedText { get; }

        int SelectionStart { get; }

        int SelectionLength { get; }

        bool IsCaretVisible { get; set; }

        bool PrintInput { get; set; }

        string NewLine { get; set; }

        string FontFamily { get; set; }
        int FontSize { get; set; }
        string Foreground { get; set; }
        string Background { get; set; }
        Encoding Encoding { get; set; }

        Task<string> ReadLineAsync();
        Task<char> ReadCharAsync();

        void Write(string value);
        void WriteLine(string value);
        void WriteLine();
        void Write(string format, params object[] args);

        Task InsertText(int index, string text);

        void Clear();

        string GetText();
        void SelectAll();

        void Send(bool appendNewLine = true);
        string CutSelectedInput();
    }
}