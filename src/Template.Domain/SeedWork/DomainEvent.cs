using System;
namespace Template.Domain.SeedWork
{
    public abstract class DomainEvent
    {
        public DateTime OccuredOn { get; }

        protected DomainEvent() => OccuredOn = DateTime.UtcNow;
    }
}
