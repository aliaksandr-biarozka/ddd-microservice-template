using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.SeedWork;
using Template.Domain.SeedWork;

namespace Template.Infrastructure
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task Dispatch<T>(T @event) where T : DomainEvent
        {
            var handlers = _serviceProvider.GetServices<IDomainEventHandler<T>>();

            foreach(var handler in handlers)
            {
                await handler.Handle(@event);
            }
        }
    }

    internal static class DispatcherExtensions
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
