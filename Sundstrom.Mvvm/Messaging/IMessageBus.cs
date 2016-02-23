using System;

namespace Sundstrom.Mvvm.Messaging
{
    /// <summary>
    ///     Interface for MessageBus.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        ///     Subscribes to bus.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="callback"></param>
        /// <returns></returns>
        int Subscribe<TMessage>(Action<TMessage> callback)
            where TMessage : Message;

        /// <summary>
        ///     Usubscribes from bus.
        /// </summary>
        /// <param name="token"></param>
        void Unsubscribe(int token);

        /// <summary>
        ///     Broadcasts a message.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void Send<TMessage>(object sender, TMessage message)
            where TMessage : Message;

        /// <summary>
        ///     Broadcasts a message.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        void Send<TMessage>(TMessage message)
            where TMessage : Message;
    }
}