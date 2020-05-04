using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class OrderPayed : DomainEvent
    {
        public long OrderId { get; }

        public OrderPayed(long orderId) => OrderId = orderId;
    }
}
