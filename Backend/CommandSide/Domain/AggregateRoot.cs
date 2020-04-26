using System.Collections.Generic;
using Shared;

namespace Domain
{
    public abstract class AggregateRoot
    {
        private long _version = -1;
        
        private readonly List<IDomainEvent> _uncommittedChanged = new List<IDomainEvent>();

        public IReadOnlyList<IDomainEvent> UncommittedChanges => _uncommittedChanged;

        public long OriginalVersion => _version - UncommittedChanges.Count;

        protected void ApplyChange(IDomainEvent domainEvent)
        {
            _uncommittedChanged.Add(domainEvent);
            Apply(domainEvent);
        }

        public void Apply(IDomainEvent domainEvent)
        {
            When(domainEvent);
            _version++;
        }
        
        public abstract string Id { get; }

        public abstract void When(IDomainEvent e);
    }
}