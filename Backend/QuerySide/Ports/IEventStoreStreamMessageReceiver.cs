using Shared;

namespace Ports
{
    public interface IEventStoreStreamMessageReceiver
    {
        void Receive(IDomainEvent domainEvent);
    }
}