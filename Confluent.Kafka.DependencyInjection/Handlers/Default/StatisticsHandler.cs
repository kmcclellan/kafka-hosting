using System.Text.Json;
using Confluent.Kafka.DependencyInjection.Statistics;

namespace Confluent.Kafka.DependencyInjection.Handlers.Default
{
    /// <summary>
    /// A DI-friendly contract for deserialized Kafka statistics handling.
    /// </summary>
    /// <seealso cref="ProducerBuilder{TKey, TValue}.SetStatisticsHandler"/>
    /// <seealso cref="ConsumerBuilder{TKey, TValue}.SetStatisticsHandler"/>
    public abstract class StatisticsHandler : IStatisticsHandler
    {
        /// <inheritdoc />
        public void OnStatistics(IClient client, string statistics) =>
            OnStatistics(client, JsonSerializer.Deserialize<KafkaStatistics>(statistics));

        /// <summary>
        /// Handles deserialized, periodic Kafka statistics.
        /// </summary>
        /// <remarks>
        /// Statistics are enabled by configuring <c>StatisticsIntervalMs</c> for the client.
        /// Refer to <see href="https://github.com/edenhill/librdkafka/blob/master/STATISTICS.md">librdkafka documentation</see> for format.
        /// </remarks>
        /// <param name="client">The Kafka producer or consumer.</param>
        /// <param name="statistics">The deserialized statistics.</param>
        public abstract void OnStatistics(IClient client, KafkaStatistics statistics);
    }
}
