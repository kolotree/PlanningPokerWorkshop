using Domain;

namespace EventStoreAdapter
{
    public static class AggregateRootExtensions
    {
        public static string ToStreamName<T>(this T aggregateRoot) where T : AggregateRoot
            => $"{typeof(T).Name}|{aggregateRoot.Id}";
    }
}