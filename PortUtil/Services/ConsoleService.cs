using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SerialPortUtility.Services
{
    public sealed class ConsoleService : IConsoleService
    {
        private readonly SourceLocationTracker LocationTracker = new SourceLocationTracker();
        private readonly CharBuffer ReadBuffer = new CharBuffer();
        private bool IsKeyInputDisabled;

        public ConsoleService()
        {
            ReadBuffer = new CharBuffer();

            IsEnabled = true;
            PrintInput = true;

            NewLine = "\n";

            Encoding = Encoding.Default;
        }

        public TextBox TextBox { get; private set; }

        public Task<char> ReadCharAsync()
        {
            var source = new TaskCompletionSource<char>();
            EventHandler<CharBufferEventArgs> a = null;
            a = (sender, args) =>
            {
                if (!LocationTracker.HasInputStarted)
                    LocationTracker.StartInput();

                try
                {
                    if (source.Task.Status == TaskStatus.RanToCompletion
                        || source.Task.Status == TaskStatus.Canceled)
                    {
                        ReadBuffer.CharAdded -= a;
                        return;
                    }

                    string value = ReadBuffer.ToString();

                    ReadBuffer.Clear();

                    LocationTracker.EndInput();

                    ReadBuffer.CharAdded -= a;

                    source.SetResult(
                        value[0]);
                }
                catch (Exception e)
                {
                    source.SetException(e);
                }
            };
            ReadBuffer.CharAdded += a;
            return source.Task;
        }

        public string Background
        {
            get { return TextBox.Background.ToString(); }

            set
            {
                TextBox.Background = new SolidColorBrush(
                    (Color) ColorConverter.ConvertFromString(value));
            }
        }

        public Task<string> ReadLineAsync()
        {
            var s = new TaskCompletionSource<string>();
            EventHandler<CharBufferEventArgs> a = null;
            a = (sender, args) =>
            {
                if (!LocationTracker.HasInputStarted)
                    LocationTracker.StartInput();

                TaskCompletionSource<string> source = s;
                try
                {
                    if (args.Value == '\n')
                    {
                        if (source.Task.Status == TaskStatus.RanToCompletion
                            || source.Task.Status == TaskStatus.Canceled)
                        {
                            ReadBuffer.CharAdded -= a;
                            return;
                        }

                        string str = ReadBuffer.ToString().Trim('\n');

                        ReadBuffer.Clear();

                        LocationTracker.EndInput();

                        ReadBuffer.CharAdded -= a;

                        source.SetResult(
                            str);
                    }
                }
                catch (Exception e)
                {
                    source.SetException(e);
                }
            };
            ReadBuffer.CharAdded += a;
            return s.Task;
        }

        public void Write(string value)
        {
            Action action = () =>
            {
                IsKeyInputDisabled = true;

                TextBox.AppendText(value);

                LocationTracker.Increase(value.Length);

                Focus();

                IsKeyInputDisabled = false;
            };

            if (Application.Current != null)
                Application.Current.Dispatcher.Invoke(action);
            else
                action();
        }

        public void Write(char ch)
        {
            Write(ch.ToString());
        }

        public void WriteLine(string value)
        {
            Action action = () =>
            {
                IsKeyInputDisabled = true;

                TextBox.AppendText(
                    string.Format("{0}\r", value));

                LocationTracker.Increase(value.Length);
                LocationTracker.NewLine();

                Focus();

                IsKeyInputDisabled = false;
            };

            if (Application.Current != null)
                Application.Current.Dispatcher.Invoke(action);
            else
                action();
        }

        public void WriteLine()
        {
            WriteLine("");
        }

        public void Write(string format, params object[] args)
        {
            string value = string.Format(format, args);

            Action action = () =>
            {
                IsKeyInputDisabled = true;

                TextBox.AppendText(value);

                LocationTracker.Increase(value.Length);

                Focus();

                IsKeyInputDisabled = false;
            };

            if (Application.Current != null)
                Application.Current.Dispatcher.Invoke(action);
            else
                action();
        }

        public void InsertText(int index, string text)
        {
            if (text.Contains("\n") || text.Contains("\r"))
                throw new ArgumentException("Contains forbidden characters.", "text");

            if (PrintInput)
            {
                if (index < LocationTracker.Count)
                {
                    TextBox.Text = TextBox.Text.Insert(index, text);
                }
                else
                {
                    TextBox.AppendText(text);
                }
            }

            int lineIndex = LocationTracker.GetCharLineIndexFromCharStringIndex(index);
            if (LocationTracker.IsWithinInputArea(lineIndex))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    ReadBuffer.Insert(lineIndex + i, text[i]);
                }
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    ReadBuffer.Add(text[i]);
                }
            }

            LocationTracker.Increase(text.Length);

            Focus(index + text.Length);
        }

        public void Clear()
        {
            Action action = () =>
            {
                TextBox.Clear();

                LocationTracker.Reset();
            };
            Application.Current.Dispatcher.Invoke(action);
        }

        public bool IsEnabled { get; set; }

        public string SelectedText
        {
            get { return TextBox.SelectedText; }
        }

        public int SelectionStart
        {
            get { return TextBox.SelectionStart; }
        }

        public int SelectionLength
        {
            get { return TextBox.SelectionLength; }
        }

        public bool IsCaretVisible
        {
            get { return TextBox.IsReadOnlyCaretVisible; }
            set { TextBox.IsReadOnlyCaretVisible = value; }
        }

        public string GetText()
        {
            return TextBox.Text;
        }

        public void SelectAll()
        {
            TextBox.SelectAll();
        }

        public bool PrintInput { get; set; }
        public string NewLine { get; set; }

        public string FontFamily
        {
            get { return TextBox.FontFamily.Source; }

            set { TextBox.FontFamily = new FontFamily(value); }
        }

        public int FontSize
        {
            get { return (int) TextBox.FontSize; }

            set { TextBox.FontSize = value; }
        }

        public string Foreground
        {
            get { return TextBox.Foreground.ToString(); }

            set
            {
                TextBox.Foreground = new SolidColorBrush(
                    (Color) ColorConverter.ConvertFromString(value));
            }
        }

        public void Send(bool appendNewLine = true)
        {
            if (appendNewLine)
            {
                TextBox.AppendText("\r");
            }

            ReadBuffer.Add('\n');

            Focus();
        }

        public string CutSelectedInput()
        {
            int selectionStart = SelectionStart;

            var sb = new StringBuilder();
            if (LocationTracker.IsWithinInputArea(selectionStart))
            {
                int startLineIndex = LocationTracker.GetCharLineIndexFromCharStringIndex(SelectionStart);
                int endLineIndex = LocationTracker.GetCharLineIndexFromCharStringIndex(SelectionStart + SelectionLength);

                for (int i = startLineIndex; i < endLineIndex; i++)
                {
                    char ch = ReadBuffer[i];
                    sb.Append(ch);
                }

                for (int i = endLineIndex - 1; i >= startLineIndex; i--)
                {
                    ReadBuffer.RemoveAt(i);
                }

                TextBox.Text = TextBox.Text.Remove(SelectionStart, SelectionLength);

                LocationTracker.Decrease(sb.Length);

                Focus(selectionStart);

                return sb.ToString();
            }
            throw new InvalidOperationException("The selected area or parts of the area cannot be cut.");
        }

        public Encoding Encoding { get; set; }

        public void SetTextBox(TextBox textBox)
        {
            TextBox = textBox;
            TextBox.AcceptsReturn = true;
            TextBox.KeyUp += TextBox_KeyUp;
            TextBox.TextInput += (s, e) =>
            {
                if (IsKeyInputDisabled)
                    return;

                if (!IsEnabled)
                    return;

                if (e.Text == null)
                    return;

                if (KeyPressed != null)
                    KeyPressed(this, new KeyInputEventArgs(e.Text[0]));
            };
            KeyPressed += ConsoleService_KeyPressed;

            IsCaretVisible = true;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            int selectedStartIndex = TextBox.SelectionStart;

            if (TextBox.SelectionLength > 0)
                return;

            if (!IsEnabled)
                return;

            switch (e.Key)
            {
                case Key.Back:
                    int index = LocationTracker.GetCharLineIndexFromCharStringIndex(selectedStartIndex);

                    if (LocationTracker.IsWithinInputArea(selectedStartIndex - 1))
                    {
                        DeleteLast(selectedStartIndex);
                        MoveCaretBack(selectedStartIndex);

                        ReadBuffer.RemoveAt(index - 1);

                        LocationTracker.Decrease();
                    }
                    break;

                case Key.Space:
                    if (KeyPressed != null)
                        KeyPressed(this, new KeyInputEventArgs(' '));
                    break;
            }
        }

        private void DeleteLast(int selectedStartIndex)
        {
            TextBox.Text = TextBox.Text.Remove(selectedStartIndex - 1, 1);
        }

        private void MoveCaretBack(int selectedStartIndex)
        {
            TextBox.Select(selectedStartIndex - 1, 0);
        }

        public event EventHandler<KeyInputEventArgs> KeyPressed;


        private void ConsoleService_KeyPressed(object sender, KeyInputEventArgs e)
        {
            char ch = e.Char;

            HandleKeyPressed(ch);

            if (ch == '\r')
            {
                HandleKeyPressed('\n');
            }
        }

        private void HandleKeyPressed(char ch)
        {
            int selectedStartIndex = TextBox.SelectionStart;

            if (TextBox.SelectionLength > 0)
                return;

            if (PrintInput
                && LocationTracker.IsOutsideInputArea(selectedStartIndex))
                return;

            if (PrintInput)
            {
                if (LocationTracker.IsWithinInputArea(selectedStartIndex) && ch != '\r')
                {
                    TextBox.Text = TextBox.Text.Insert(selectedStartIndex, ch.ToString());
                }
                else
                {
                    TextBox.AppendText(
                        ch.ToString());
                }
            }

            switch (ch)
            {
                case '\r':
                    ReadBuffer.Add('\r');
                    if (ReadBuffer.ToString() == NewLine)
                    {
                        LocationTracker.NewLine();
                    }
                   
                    break;

                case '\n':
                    ReadBuffer.Add('\n');
                    if(ReadBuffer.ToString() == NewLine)
                    {
                        LocationTracker.NewLine();
                    }
                    break;

                default:
                    if (PrintInput
                        && LocationTracker.IsWithinInputArea(selectedStartIndex))
                    {
                        int index = LocationTracker
                            .GetCharLineIndexFromCharStringIndex(selectedStartIndex);

                        ReadBuffer.Insert(index, ch);

                        if (PrintInput)
                            Focus(selectedStartIndex + 1);
                    }
                    else
                    {
                        ReadBuffer.Add(ch);

                        if (PrintInput)
                            Focus();
                    }

                    LocationTracker.Increase();

                    break;
            }
        }

        private void Focus()
        {
            TextBox.Focus();
            TextBox.CaretIndex = TextBox.Text.Length;
        }

        private void Focus(int index)
        {
            TextBox.Focus();
            TextBox.CaretIndex = index;
        }
    }
}