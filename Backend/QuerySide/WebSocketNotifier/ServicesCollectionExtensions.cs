﻿using System;
using ApplicationServices;
using EventStoreAdapter;
using Microsoft.Extensions.DependencyInjection;
using Ports;
using WebSocketAdapter;

namespace WebSocketNotifier
{
    public static class ServicesCollectionExtensions
    {
        public static void WireUpClientNotifier(this IServiceCollection services)
        {
            var webSocketClientNotifier = new WebSocketClientNotifier();
            webSocketClientNotifier.StartClientNotifier();

            services.AddSingleton(typeof(IClientNotifier), webSocketClientNotifier);
        }
        
        public static void WireUpEventReader(this IServiceCollection services, string connectionString)
        {
            var eventReader = EventStoreReader.BuildUsing(new Uri(connectionString));
            services.AddSingleton(typeof(IEventStoreReader), eventReader);
        }

        public static void WireUpEventHandler(this IServiceCollection services) =>
            services.AddSingleton<IEventStoreStreamMessageReceiver, DomainEventHandler>();
    }
}