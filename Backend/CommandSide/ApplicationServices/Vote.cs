using System;

namespace ApplicationServices
{
    public sealed class Vote
    {
        public Guid SessionId { get; }
        public string Member { get; }
        public int Points { get; }

        public Vote(Guid sessionId, string member, int points)
        {
            SessionId = sessionId;
            Member = member;
            Points = points;
        }
    }
}