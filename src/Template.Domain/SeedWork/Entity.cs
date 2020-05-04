using System.Collections.Generic;

namespace Template.Domain.SeedWork
{
    public abstract class Entity
    {
        private List<DomainEvent> _events;

        public long Id { get; protected set; }

        public IReadOnlyList<DomainEvent> Events => _events;

        protected Entity() { }

        public void AddDomainEvent(DomainEvent @event)
        {
            _events ??= new List<DomainEvent>();
            _events.Add(@event);
        }

        public void RemoveDomainEvent(DomainEvent @event) => _events?.Remove(@event);

        public void RemoveAllDomainEvents() => _events?.Clear();
    }
}
