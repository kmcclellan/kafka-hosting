using System.Collections.Generic;

namespace Confluent.Kafka.Hosting
{
    class HostedConsumerOptions<THandler>
    {
        public IEnumerable<string> Topics { get; }

        public HostedConsumerOptions(IEnumerable<string> topics)
        {
            Topics = topics;
        }
    }
}
