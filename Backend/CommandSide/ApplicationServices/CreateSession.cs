using System;

namespace ApplicationServices
{
    public sealed class CreateSession
    {
        public Guid SessionId { get; }

        public CreateSession(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}