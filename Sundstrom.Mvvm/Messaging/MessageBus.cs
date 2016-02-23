using System;
using System.Collections.Generic;
using System.Linq;

namespace Sundstrom.Mvvm.Messaging
{
    /// <summary>
    ///     A simple message bus for broadcasting messages across contexts.
    /// </summary>
    public sealed class MessageBus : IMessageBus
    {
        private static MessageBus _default;
        private readonly List<SubscriptionGroup> _subscriptions = new List<SubscriptionGroup>();
        private int _tokens = 1;

        /// <summary>
        ///     Gets the default MessageBus.
        /// </summary>
        public static MessageBus Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new MessageBus();
                }
                return _default;
            }
        }

        /// <summary>
        ///     Subscribes to bus.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="callback"></param>
        /// <returns></returns>
        public int Subscribe<TMessage>(Action<TMessage> callback)
            where TMessage : Message
        {
            SubscriptionGroup subscriptionGroup = _subscriptions.FirstOrDefault(x => x.MessageType == typeof (TMessage));
            if (subscriptionGroup == null)
            {
                subscriptionGroup = new SubscriptionGroup(typeof (TMessage));
                _subscriptions.Add(subscriptionGroup);
            }

            int token = _tokens++;
            subscriptionGroup.Subscriptions.Add(token, message => callback((TMessage) message));
            return token;
        }

        /// <summary>
        ///     Usubscribes from bus.
        /// </summary>
        /// <param name="token"></param>
        public void Unsubscribe(int token)
        {
            SubscriptionGroup subscription = _subscriptions
                .First(x => x.Subscriptions.Any(x2 => x2.Key == token));

            subscription.Subscriptions.Remove(token);
        }

        /// <summary>
        ///     Broadcasts a message.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void Send<TMessage>(object sender, TMessage message)
            where TMessage : Message
        {
            SubscriptionGroup subscriptionGroup = _subscriptions.First(x => x.MessageType == typeof (TMessage));
            foreach (var subscription in subscriptionGroup.Subscriptions)
            {
                message.Sender = sender;
                subscription.Value(message);
            }
        }

        /// <summary>
        ///     Broadcasts a message.
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        public void Send<TMessage>(TMessage message)
            where TMessage : Message
        {
            Send(null, message);
        }

        /// <summary>
        ///     Creates a new instance of MessageBuss.
        /// </summary>
        /// <returns></returns>
        public static MessageBus Create()
        {
            return new MessageBus();
        }

        internal class SubscriptionGroup
        {
            public SubscriptionGroup(Type messageType)
            {
                MessageType = messageType;
                Subscriptions = new Dictionary<int, Action<object>>();
            }

            public Type MessageType { get; private set; }
            public Dictionary<int, Action<object>> Subscriptions { get; private set; }
        }
    }
}