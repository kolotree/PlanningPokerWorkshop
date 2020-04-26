using System;

namespace Shared
{
    public static class DomainEvents
    {
        public sealed class SessionCreated
        {
            public Guid SessionId { get; }

            public SessionCreated(Guid sessionId)
            {
                SessionId = sessionId;
            }
        }

        public sealed class MemberVoted
        {
            public Guid SessionId { get; }
            public string Member { get; }
            public int Points { get; }

            public MemberVoted(Guid sessionId, string member, int points)
            {
                SessionId = sessionId;
                Member = member;
                Points = points;
            }
        }

        public sealed class SessionCleared
        {
            public Guid SessionId { get; }

            public SessionCleared(Guid sessionId)
            {
                SessionId = sessionId;
            }
        }
    }
}