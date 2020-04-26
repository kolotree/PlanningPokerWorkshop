using System;
using System.Collections.Generic;
using Shared;

namespace Domain
{
    public sealed class Session : AggregateRoot
    {
        private Guid _id = Guid.Empty;
        
        private Dictionary<string, int> _votes = new Dictionary<string, int>();

        public override string Id => _id.ToString();
        
        public static Session NewOf(Guid sessionId)
        {
            var session = new Session();
            session.ApplyChange(new DomainEvents.SessionCreated(sessionId));
            return session;
        }

        public void Vote(string member, int points)
        {
            if (!_votes.TryGetValue(member, out var currentPoints) ||
                currentPoints != points)
            {
                ApplyChange(new DomainEvents.MemberVoted(_id, member, points));
            }
        }

        public void Clear()
        {
            if (_votes.Count != 0)
            {
                ApplyChange(new DomainEvents.SessionCleared(_id));
            }
        }

        public override void When(IDomainEvent e)
        {
            switch (e)
            {
                case DomainEvents.MemberVoted memberVoted:
                    _votes[memberVoted.Member] = memberVoted.Points;
                    break;
                case DomainEvents.SessionCleared sessionCleared:
                    _votes.Clear();
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