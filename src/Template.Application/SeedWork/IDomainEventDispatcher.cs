using System.Threading.Tasks;
using Template.Domain.SeedWork;

namespace Template.Application.SeedWork
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch<T>(T @event) where T : DomainEvent;
    }
}
