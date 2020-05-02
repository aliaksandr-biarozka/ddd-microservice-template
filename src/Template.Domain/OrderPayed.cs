using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class OrderPayed : DomainEvent
    {
        public int OrderId { get; }

        public OrderPayed(int orderId) => OrderId = orderId;
    }
}
