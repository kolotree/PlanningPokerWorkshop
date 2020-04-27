using System;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ports;

namespace WebSocketNotifier
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = new Uri(hostContext.Configuration["AppSettings:EventStoreConnectionString"]);
                    
                    var connection = EventStoreConnection.Create(
                        ConnectionSettings.Create().KeepReconnecting(),
                        connectionString);
                    
                    connection.ConnectAsync().Wait();
                    
                    services.AddSingleton(connection);
                    services.WireUpEventStoreReader();
                    services.WireUpClientNotifier();
                    services.WireUpEventHandler();
                    services.AddHostedService<Worker>();

                    var serviceProvider = services.BuildServiceProvider();
                    
                    var eventReader = serviceProvider.GetService<IEventStoreReader>();
                    var webSocketClientNotifier = serviceProvider.GetService<IClientNotifier>();
                    
                    webSocketClientNotifier.StartClientNotifier(sessionId => eventReader.SubscribeTo($"Session|{sessionId}"));
                });
    }
}