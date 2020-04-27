using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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
            IEventStoreStreamMessageReceiver domainEventHandler,
            IConfiguration configuration)
        {
            _logger = logger;
            var sourceStreamName = configuration["AppSettings:AllSessionEventsProjectionName"];
            eventStoreReader.SubscribeTo(sourceStreamName, -1, domainEventHandler);
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