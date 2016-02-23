using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace SerialPortUtility.Services
{
    public sealed class DialogService : IDialogService
    {
        public async Task<bool> ShowYesOrNoDialogAsync(string content, string title)
        {
            return MessageBox
                .Show(content, title, MessageBoxButton.YesNo, MessageBoxImage.Question)
                   == MessageBoxResult.Yes;
        }

        public async Task ShowErrorDialogAsync(string content, string title)
        {
            MessageBox
                .Show(content, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowSaveFileDialog(string title, string extension, out Stream stream)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = title;
            saveFileDialog.Filter = extension;
            saveFileDialog.RestoreDirectory = true;

            if ((bool) saveFileDialog.ShowDialog())
            {
                stream = saveFileDialog.OpenFile();
                return true;
            }
            stream = null;
            return false;
        }
    }
}