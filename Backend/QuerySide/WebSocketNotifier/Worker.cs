using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ports;

namespace WebSocketNotifier
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(
            ILogger<Worker> logger, 
            IEventStoreReader eventStoreReader, 
            IEventStoreStreamMessageReceiver domainEventHandler)
        {
            _logger = logger;
            eventStoreReader.SubscribeTo("SomeAggregate", -1, domainEventHandler);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}