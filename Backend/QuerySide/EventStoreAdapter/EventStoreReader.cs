﻿using System;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Ports;
using Shared;

namespace EventStoreAdapter
{
    public sealed class EventStoreReader : IEventStoreReader
    {
        private readonly IEventStoreConnection _connection;
        private readonly IEventStoreStreamMessageReceiver _messageReceiver;

        public EventStoreReader(
            IEventStoreConnection connection, 
            IEventStoreStreamMessageReceiver messageReceiver)
        {
            _connection = connection;
            _messageReceiver = messageReceiver;
        }

        public IEventStoreSubscription SubscribeTo(string sourceStreamName, long startPosition = -1)
        {
            var catchUpSubscription = _connection.SubscribeToStreamFrom(
                sourceStreamName,
                startPosition == -1 ? null : (long?) startPosition,
                CatchUpSubscriptionSettings.Default,
                (_, x) => _messageReceiver.Receive(Convert(x)));

            return new EventStoreSubscription(catchUpSubscription);
        }

        private static IDomainEvent Convert(ResolvedEvent resolvedEvent)
        {
            var metadataAsString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata);
            var domainEventAsString = Encoding.UTF8.GetString(resolvedEvent.Event.Data);

            var metadata = JsonConvert.DeserializeObject<EventMetadata>(metadataAsString);
            var domainEventType = Type.GetType(metadata.AssemblyQualifiedName);
            var domainEvent = JsonConvert.DeserializeObject(domainEventAsString, domainEventType);

            return (dynamic) domainEvent;
        }
    }
}