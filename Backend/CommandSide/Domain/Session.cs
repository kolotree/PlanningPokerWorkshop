using System;
using Shared;

namespace Domain
{
    public sealed class Session : AggregateRoot
    {
        private Guid _id = Guid.Empty;

        public override string Id => _id.ToString();
        
        public static Session NewOf(Guid sessionId)
        {
            var session = new Session();
            session.Apply(new DomainEvents.SessionCreated(sessionId));
            return session;
        }
        
        public override void When(IDomainEvent e)
        {
            switch (e)
            {
                case DomainEvents.MemberVoted memberVoted:
                    break;
                case DomainEvents.SessionCleared sessionCleared:
                    break;
                case DomainEvents.SessionCreated sessionCreated:
                    _id = sessionCreated.SessionId;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e));
            }
        }
    }
}