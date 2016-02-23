namespace Sundstrom.Mvvm.Messaging
{
    public abstract class Message
    {
        public object Sender { get; internal set; }
    }
}