using System.Linq;
using System.Threading.Tasks;
using Template.Application.SeedWork;
using Template.Domain.SeedWork;

namespace Template.Infrastructure
{
    public static class DispatcherExtensions
    {
        public static async Task DispatchDomainEvents(this IDomainEventDispatcher dispatcher, OrderDbContext context)
        {
            var entities = context.ChangeTracker.Entries<Entity>().Where(e => e.Entity.Events.Any()).Select(x => x.Entity);

            var events = entities.SelectMany(x => x.Events).ToList();

            foreach (var entity in entities)
            {
                entity.RemoveAllDomainEvents();
            }

            foreach (var @event in events)
            {
                await dispatcher.Dispatch(@event);
            }
        }
    }
}
