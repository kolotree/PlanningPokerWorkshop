using ApplicationServices;
using EventStoreAdapter;
using Microsoft.Extensions.DependencyInjection;
using Ports;
using WebSocketAdapter;

namespace WebSocketNotifier
{
    public static class ServicesCollectionExtensions
    {
        public static void WireUpClientNotifier(this IServiceCollection services) => 
            services.AddSingleton<IClientNotifier, WebSocketClientNotifier>();

        public static void WireUpEventHandler(this IServiceCollection services) =>
            services.AddSingleton<IEventStoreStreamMessageReceiver, DomainEventHandler>();
        
        public static void WireUpEventStoreReader(this IServiceCollection services) => 
            services.AddSingleton<IEventStoreReader, EventStoreReader>();
    }
}