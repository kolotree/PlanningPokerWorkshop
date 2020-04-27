﻿using System;

namespace Ports
{
    public interface IClientNotifier
    {
        void StartClientNotifier(Action<string> newClientCallback);
        void NotifyAllClients(string sessionId, string message);
    }
}