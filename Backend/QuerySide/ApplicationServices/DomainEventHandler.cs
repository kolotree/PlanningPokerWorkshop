using System;
using Newtonsoft.Json;
using Ports;
using Shared;

namespace ApplicationServices
{
    public sealed class DomainEventHandler : IEventStoreStreamMessageReceiver
    {
        private readonly IClientNotifier _clientNotifier;
        
        public DomainEventHandler(IClientNotifier clientNotifier)
        {
            _clientNotifier = clientNotifier;
        }

        public void Receive(IDomainEvent domainEvent)
        {
            var sessionId = domainEvent switch
            {
                DomainEvents.SessionCreated sessionCreated => sessionCreated.SessionId.ToString(),
                DomainEvents.MemberVoted memberVoted => memberVoted.SessionId.ToString(),
                DomainEvents.SessionCleared sessionCleared => sessionCleared.SessionId.ToString(),
                _ => throw new ArgumentException("Undefined event type")
            };

            var eventJson = JsonConvert.SerializeObject(new
            {
                Type = domainEvent.GetType().Name,
                Data = domainEvent
            });
            
            _clientNotifier.NotifyAllClients(sessionId, eventJson);
        }
    }
}