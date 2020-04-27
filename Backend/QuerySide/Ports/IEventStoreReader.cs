﻿namespace Ports
{
    public interface IEventStoreReader
    {
        IEventStoreSubscription SubscribeTo(
            string sourceStreamName,
            long startPosition = -1);
    }
}