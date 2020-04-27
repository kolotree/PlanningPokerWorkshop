namespace EventStoreAdapter
{
    internal sealed class EventMetadata
    {
        public string AssemblyQualifiedName { get; set; }

        public EventMetadata(string assemblyQualifiedName)
        {
            AssemblyQualifiedName = assemblyQualifiedName;
        }
    }
}