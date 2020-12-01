namespace Confluent.Kafka.Hosting
{
    /// <summary>
    /// A contract for individual handling of Kafka messages.
    /// </summary>
    /// <typeparam name="TKey">The message key type.</typeparam>
    /// <typeparam name="TValue">The message value type.</typeparam>
    public interface IMessageHandler<TKey, TValue>
    {
        /// <summary>
        /// Handles a consumed Kafka message.
        /// </summary>
        /// <param name="message">The Kafka message.</param>
        void OnMessage(Message<TKey, TValue> message);
    }
}
