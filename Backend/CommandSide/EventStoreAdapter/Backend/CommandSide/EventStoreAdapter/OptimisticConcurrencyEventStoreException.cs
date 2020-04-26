using System;

namespace EventStoreAdapter
{
    public sealed class OptimisticConcurrencyEventStoreException : Exception
    {
        public OptimisticConcurrencyEventStoreException()
        {
            
        }
    }

    public sealed class StreamDeletedException : Exception
    {
        public StreamDeletedException()
        {
            
        }
    }
}