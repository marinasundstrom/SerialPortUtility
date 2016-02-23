namespace Sundstrom.Mvvm.Messaging
{
    public sealed class TextMessage : Message
    {
        public TextMessage(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}