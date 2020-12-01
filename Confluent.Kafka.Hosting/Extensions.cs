using Confluent.Kafka.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Confluent.Kafka.Hosting
{
    /// <summary>
    /// Extensions to facilitate Kafka service hosting.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds a hosted consumer to the services.
        /// </summary>
        /// <typeparam name="THandler">The type of message handler.</typeparam>
        /// <typeparam name="TKey">The consumer key type.</typeparam>
        /// <typeparam name="TValue">The consumer value type.</typeparam>
        /// <param name="services">The extended services.</param>
        /// <param name="topics">The topic subscription.</param>
        /// <param name="clientConfig">Optional client configuration properties.</param>
        /// <returns>The same instance for chaining.</returns>
        public static IServiceCollection AddHostedConsumer<THandler, TKey, TValue>(
            this IServiceCollection services,
            IEnumerable<string> topics,
            IEnumerable<KeyValuePair<string, string>>? clientConfig = null)
            where THandler : class, IMessageHandler<TKey, TValue>
        {
            services.AddHostedService<HostedConsumer<THandler, TKey, TValue>>();
            services.AddSingleton<THandler>();
            services.AddSingleton(new HostedConsumerOptions<THandler>(topics));

            if (clientConfig != null)
            {
                services.AddKafkaClient<HostedConsumer<THandler, TKey, TValue>>(clientConfig);
            }

            return services;
        }

    }
}
