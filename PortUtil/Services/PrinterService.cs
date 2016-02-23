using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SerialPortUtility.Services
{
    public class PrinterService : IPrinterService
    {
        public bool PrintText(string text, string description = "")
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault())
            {
                var document = new FlowDocument();

                foreach (string line in text.Split('\r'))
                {
                    var myParagraph = new Paragraph();
                    myParagraph.Margin = new Thickness(0);
                    myParagraph.Inlines.Add(new Run(line));
                    document.Blocks.Add(myParagraph);
                }

                printDialog.PrintDocument(
                    ((IDocumentPaginatorSource) document).DocumentPaginator,
                    description);
                return true;
            }
            return false;
        }
    }
}