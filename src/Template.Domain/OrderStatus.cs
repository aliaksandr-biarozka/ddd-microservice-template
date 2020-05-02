using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class OrderStatus : Enumeration
    {
        private OrderStatus(int id, string name) : base(id, name) { }

        public OrderStatus Pending = new OrderStatus(1, nameof(Pending));

        public OrderStatus Submitted = new OrderStatus(2, nameof(Submitted));

        public OrderStatus PaymentPending = new OrderStatus(3, nameof(PaymentPending));

        public OrderStatus Payed = new OrderStatus(4, nameof(Payed));

        public OrderStatus AwaitingShipment = new OrderStatus(5, nameof(AwaitingShipment));
        
        public OrderStatus Shipped = new OrderStatus(5, nameof(Shipped));

        public OrderStatus Completed = new OrderStatus(6, nameof(Completed));

        public OrderStatus Cancelled = new OrderStatus(7, nameof(Cancelled));
    }
}