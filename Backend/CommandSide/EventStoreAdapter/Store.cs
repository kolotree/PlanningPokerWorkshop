using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Ports;
using Shared;

namespace EventStoreAdapter
{
    public class Store : IStore
    {
        private readonly IEventStoreConnection _connection;

        private Store(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public static IStore BuildStoreByConnectingTo(Uri connectionString)
        {
            var connection = EventStoreConnection.Create(
                ConnectionSettings.Create().KeepReconnecting(),
                connectionString);
            connection.ConnectAsync().Wait();
            return new Store(connection);
        }

        public async Task<T> Load<T>(string id) where T : AggregateRoot, new()
        {
            var streamEventsSlice = await _connection.ReadStreamEventsForwardAsync(
                id.ToStreamName<T>(),
                0,
                1024,
                false);

            var domainEvents = streamEventsSlice.Events.Select(resolvedEvent =>
            {
                var metaData =JsonConvert.DeserializeObject<EventMetaData>(Encoding.UTF8.GetString(resolvedEvent.Event.Metadata));
                return (IDomainEvent) JsonConvert.DeserializeObject(
                    Encoding.UTF8.GetString(resolvedEvent.Event.Data),
                    Type.GetType(metaData.AssemblyQualifiedName));
            }).ToList();

            if (domainEvents.Count == 0)
            {
                throw new AggregateDoesntExistException();
            }

            var aggregate = new T();
            foreach (var domainEvent in domainEvents)
            {
                aggregate.Apply(domainEvent);
            }

            return aggregate;
        }

        public async Task SaveChanges<T>(T aggregate) where T : AggregateRoot
        {
            var result = await _connection.ConditionalAppendToStreamAsync(
                aggregate.ToStreamName(),
                aggregate.OriginalVersion,
                aggregate.UncommittedChanges.Select(domainEvent =>
                    new EventData(
                        Guid.NewGuid(), 
                        domainEvent.GetType().Name,
                        true,
                        Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(domainEvent)),
                        Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new EventMetaData(domainEvent.GetType().AssemblyQualifiedName))))));

            switch (result.Status)
            {
                case ConditionalWriteStatus.VersionMismatch:
                    throw new OptimisticConcurrencyEventStoreException();
                case ConditionalWriteStatus.StreamDeleted:
                    throw new StreamDeletedException();
            }
        }
    }
}