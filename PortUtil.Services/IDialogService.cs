using System.IO;
using System.Threading.Tasks;

namespace SerialPortUtility.Services
{
    public interface IDialogService
    {
        Task<bool> ShowYesOrNoDialogAsync(string content, string title);
        Task ShowErrorDialogAsync(string content, string title);
        Task ShowWarningDialogAsync(string content, string title);
        Task ShowInfoDialogAsync(string content, string title);
        bool ShowSaveFileDialog(string title, string extension, out Stream stream);
        bool ShowOpenFileDialog(string title, string extension, out Stream stream);
    }
}