using System;
using System.Collections.Generic;

namespace Template.Domain.SeedWork
{
    public abstract class Entity<TId>
    {
        private List<DomainEvent> _events;

        public TId Id { get; protected set; }

        public IReadOnlyList<DomainEvent> Events => _events;

        private Entity() { }

        public void AddDomainEvent(DomainEvent @event)
        {
            _events ??= new List<DomainEvent>();
            _events.Add(@event);
        }

        public void RemoveDomainEvent(DomainEvent @event) => _events?.Remove(@event);

        public void RemoveAllDomainEvents() => _events?.Clear();
         

        public abstract class IntEntity : Entity<int> { }

        public abstract class GuidEntity : Entity<Guid> { }
    }
}
