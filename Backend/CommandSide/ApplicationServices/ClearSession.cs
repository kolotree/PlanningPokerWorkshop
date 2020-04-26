using System;

namespace ApplicationServices
{
    public sealed class ClearSession
    {
        public Guid SessionId { get; }

        public ClearSession(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}