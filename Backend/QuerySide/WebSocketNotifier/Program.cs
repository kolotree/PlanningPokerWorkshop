using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                    services.WireUpClientNotifier();
                    services.WireUpEventHandler();
                    var connectionString = hostContext.Configuration["AppSettings:EventStoreConnectionString"];
                    services.WireUpEventReader(connectionString);
                    services.AddHostedService<Worker>();
                });
    }
}