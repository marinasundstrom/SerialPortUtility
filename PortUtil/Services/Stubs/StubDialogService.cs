using System.IO;
using System.Threading.Tasks;

namespace SerialPortUtility.Services.Stubs
{
    public class StubDialogService : IDialogService
    {
        public bool StubDialogResult { get; set; }

        public bool StubDialogShown { get; set; }

        public async Task<bool> ShowYesOrNoDialogAsync(string content, string title)
        {
            StubDialogShown = true;
            return StubDialogResult;
        }

        public async Task ShowErrorDialogAsync(string content, string title)
        {
            StubDialogShown = true;
        }

        public bool ShowSaveFileDialog(string title, string extension, out Stream stream)
        {
            stream = null;
            StubDialogShown = true;
            return StubDialogResult;
        }
    }
}