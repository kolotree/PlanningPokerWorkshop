using System;

namespace Ports
{
    public interface IClientNotifier
    {
        void NotifyAllClients(string sessionId, string message);
    }
}