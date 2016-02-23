using System.Windows.Controls;

namespace SerialPortUtility.Helpers
{
    public static class ComboBoxExtensions
    {
        public static TextBox GetTextBox(this ComboBox comboBox)
        {
            return (TextBox) comboBox.Template.FindName("PART_EditableTextBox", comboBox);
        }
    }
}