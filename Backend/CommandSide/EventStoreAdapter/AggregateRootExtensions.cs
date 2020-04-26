using Domain;

namespace EventStoreAdapter
{
    public static class StreamNameExtensions
    {
        public static string ToStreamName<T>(this T aggregateRoot) where T : AggregateRoot
            => $"{typeof(T).Name}|{aggregateRoot.Id}";
        
        public static string ToStreamName<T>(this string aggregateId) where T: AggregateRoot
            =>$"{typeof(T).Name}|{aggregateId}";
    }
}