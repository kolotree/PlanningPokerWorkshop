using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ports;
using WebSocketAdapter;

namespace WebSocketNotifier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var webSocketClientNotifier = new WebSocketClientNotifier();
                    webSocketClientNotifier.StartClientNotifier();

                    services.AddSingleton(typeof(IClientNotifier), webSocketClientNotifier);
                    services.AddHostedService<Worker>();
                });
    }
}