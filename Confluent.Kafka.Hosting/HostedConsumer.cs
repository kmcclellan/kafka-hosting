using Confluent.Kafka.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Confluent.Kafka.Hosting
{
    class HostedConsumer<THandler, TKey, TValue> : IHostedService
        where THandler : IMessageHandler<TKey, TValue>
    {
        private readonly IKafkaFactory factory;
        private readonly THandler handler;
        private readonly HostedConsumerOptions<THandler> options;
        private readonly IHostApplicationLifetime lifetime;
        private readonly ILogger<HostedConsumer<THandler, TKey, TValue>>? logger;

        private Task? task;

        public HostedConsumer(
            IKafkaFactory factory,
            THandler handler,
            HostedConsumerOptions<THandler> options,
            IHostApplicationLifetime lifetime,
            ILogger<HostedConsumer<THandler, TKey, TValue>>? logger = null)
        {
            this.factory = factory;
            this.handler = handler;
            this.options = options;
            this.lifetime = lifetime;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = factory.CreateConsumer<TKey, TValue>();

            await Task.Run(() => consumer.Subscribe(options.Topics), cancellationToken);
            task = Run(consumer, lifetime.ApplicationStopping);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Await twice to throw any exceptions.
            await await Task.WhenAny(task, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        private async Task Run(IConsumer<TKey, TValue> consumer, CancellationToken stoppingToken)
        {
            try
            {
                while (true)
                {
                    // First see if there is an available message before spinning up a thread.
                    var result = consumer.Consume(TimeSpan.Zero) ??
                        await Task.Run(() => consumer.Consume(stoppingToken), stoppingToken);

                    // TODO: Support EOF handling?
                    if (result.IsPartitionEOF) continue;

                    stoppingToken.ThrowIfCancellationRequested();
                    handler.OnMessage(result.Message);
                    consumer.StoreOffset(result);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { }
            catch (ConsumeException e)
            {
                // Consumer shouldn't emit transient errors.
                logger?.LogError(e, "Error consuming message.");
            }
            catch (Exception e)
            {
                logger?.LogCritical(e, "Unhandled exception in consumer. Stopping application...");
                lifetime.StopApplication();

                // Throw for host to set error exit code.
                throw;
            }
            finally
            {
                consumer.Close();
                consumer.Dispose();
            }
        }
    }
}
