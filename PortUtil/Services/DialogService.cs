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

        public async Task ShowWarningDialogAsync(string content, string title)
        {
            MessageBox
                .Show(content, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public async Task ShowInfoDialogAsync(string content, string title)
        {
            MessageBox
                .Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information);
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

        public bool ShowOpenFileDialog(string title, string extension, out Stream stream)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = extension;
            openFileDialog.RestoreDirectory = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                stream = openFileDialog.OpenFile();
                return true;
            }
            stream = null;
            return false;
        }
    }
}