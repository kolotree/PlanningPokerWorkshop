using System;
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

        private EventStoreReader(IEventStoreConnection connection)
        {
            _connection = connection;
            _connection.ConnectAsync().Wait();
        }

        public static EventStoreReader BuildUsing(Uri connectionString)
        {
            var connection = EventStoreConnection.Create(
                ConnectionSettings.Create().KeepReconnecting(),
                connectionString);

            return new EventStoreReader(connection);
        }

        public IEventStoreSubscription SubscribeTo(
            string sourceStreamName,
            long startPosition,
            IEventStoreStreamMessageReceiver receiver)
        {
            var catchUpSubscription = _connection.SubscribeToStreamFrom(
                sourceStreamName,
                startPosition == -1 ? null : (long?) startPosition,
                CatchUpSubscriptionSettings.Default,
                (_, x) => receiver.Receive(Convert(x)));

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