using System.Text.Json.Serialization;

namespace Confluent.Kafka.DependencyInjection.Statistics
{
    /// <summary>
    /// Statistics about EoS (idempotent and transactional) Kafka producers.
    /// </summary>
    /// <remarks>
    /// See <see href="https://github.com/edenhill/librdkafka/blob/master/STATISTICS.md#eos">librdkafka documentation</see> for more information.
    /// </remarks>
    public class ExactlyOnceSemanticsStatistics
    {
        /// <summary>
        /// Current idempotent producer id state.
        /// </summary>
        [JsonPropertyName("idemp_state")]
        public string? IdempotentState { get; set; }

        /// <summary>
        /// Time elapsed since last idemp_state change (milliseconds).
        /// </summary>
        [JsonPropertyName("idemp_stateage")]
        public long? IdempotentStateAge { get; set; }

        /// <summary>
        /// Current transactional producer state.
        /// </summary>
        [JsonPropertyName("txn_state")]
        public string? TransactionalState { get; set; }

        /// <summary>
        /// Time elapsed since last txn_state change (milliseconds).
        /// </summary>
        [JsonPropertyName("txn_stateage")]
        public long? TransactionalStateAge { get; set; }

        /// <summary>
        /// Transactional state allows enqueuing (producing) new messages.
        /// </summary>
        [JsonPropertyName("txn_may_enq")]
        public bool? TransactionalMayEnqueue { get; set; }

        /// <summary>
        /// The currently assigned Producer ID (or -1).
        /// </summary>
        [JsonPropertyName("producer_id")]
        public long? ProducerId { get; set; }

        /// <summary>
        /// The current epoch (or -1).
        /// </summary>
        [JsonPropertyName("producer_epoch")]
        public long? ProducerEpoch { get; set; }

        /// <summary>
        /// The number of Producer ID assignments since start.
        /// </summary>
        [JsonPropertyName("epoch_cnt")]
        public long? EpochCount { get; set; }
    }
}
