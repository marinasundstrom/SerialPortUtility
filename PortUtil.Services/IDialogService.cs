using System.IO;
using System.Threading.Tasks;

namespace SerialPortUtility.Services
{
    public interface IDialogService
    {
        Task<bool> ShowYesOrNoDialogAsync(string content, string title);
        Task ShowErrorDialogAsync(string content, string title);
        bool ShowSaveFileDialog(string title, string extension, out Stream stream);
    }
}