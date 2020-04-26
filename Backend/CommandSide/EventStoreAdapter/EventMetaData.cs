namespace EventStoreAdapter
{
    internal sealed partial class EventMetaData
    {
        public string AssemblyQualifiedName { get; }

        public EventMetaData(string assemblyQualifiedName)
        {
            AssemblyQualifiedName = assemblyQualifiedName;
        }
    }
}