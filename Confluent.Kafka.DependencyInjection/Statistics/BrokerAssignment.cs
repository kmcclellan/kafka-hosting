using System.Text.Json.Serialization;

namespace Confluent.Kafka.DependencyInjection.Statistics
{
    /// <summary>
    /// Topic partition assigned to broker.
    /// </summary>
    /// <remarks>
    /// See <see href="https://github.com/edenhill/librdkafka/blob/master/STATISTICS.md#brokerstoppars">librdkafka documentation</see> for more information.
    /// </remarks>
    public class BrokerAssignment
    {
        /// <summary>
        /// Topic name.
        /// </summary>
        [JsonPropertyName("topic")]
        public string? Topic { get; set; }

        /// <summary>
        /// Partition id.
        /// </summary>
        [JsonPropertyName("partition")]
        public long? Partition { get; set; }
    }
}
