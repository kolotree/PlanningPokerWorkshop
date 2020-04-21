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

            public MemberVoted(Guid sessionId, string member)
            {
                SessionId = sessionId;
                Member = member;
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