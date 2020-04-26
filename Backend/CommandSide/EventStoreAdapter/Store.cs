using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Ports;

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

        public async Task SaveChanges<T>(T aggregate) where T : AggregateRoot
        {
            var result = await _connection.ConditionalAppendToStreamAsync(
                aggregate.ToStreamName(),
                -1,
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

    internal sealed partial class EventMetaData
    {
        public string AssemblyName { get; }

        public EventMetaData(string assemblyName)
        {
            AssemblyName = assemblyName;
        }
    }
}