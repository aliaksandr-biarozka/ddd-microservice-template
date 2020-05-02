using System;
using System.Collections.Generic;
using System.Linq;
using Template.Domain.SeedWork;
using static Template.Domain.SeedWork.Entity<int>;

namespace Template.Domain
{
    public class Order : IntEntity, IAggregateRoot
    {
        private readonly Guid _customerId;
        private readonly List<OrderItem> _orderItems;
        private OrderStatus _orderStatus;
        private Address _address;

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(Guid customerId) : this()
        {
            _customerId = customerId;
            _orderStatus = OrderStatus.Pending;
        }

        public void ChangeAddress(Address address)
        {
            Require.That(_orderStatus.IsOneOf(OrderStatus.Pending, OrderStatus.Submitted), $"can not change address of the {_orderStatus} order");
            _address = address;
        }

        public void AddProducts(int productId, string productName, int quantity, decimal price, Discount discount)
        {
            Require.That(_orderStatus == OrderStatus.Pending, $"can not change order items of the {_orderStatus} order");

            var existingOrderItem = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if(existingOrderItem != null)
            {
                existingOrderItem.AddItems(quantity);

                if((existingOrderItem.Discount?.ApplyTo(price) ?? 0) < (discount?.ApplyTo(price) ?? 0))
                {
                    existingOrderItem.SetNewDiscount(discount);
                }
            }
            else
            {
                _orderItems.Add(new OrderItem(productId, productName, quantity, price, discount));
            }
        }

        public void RemoveProducts(int productId, int quantity)
        {
            Require.That(_orderStatus == OrderStatus.Pending, $"can not change order items of the {_orderStatus} order");

            var existingOrderItem = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            Require.That(existingOrderItem != null, "can not remove products as they are not exist in an order");

            if(existingOrderItem.Quantity - quantity > 0)
            {
                existingOrderItem.RemoveItems(quantity);
            }
            else
            {
                _orderItems.Remove(existingOrderItem);
            }
        }

        public decimal GetTotalPrice() => _orderItems.Sum(x => x.GetTotalPrice());

        public void Submit()
        {
            Require.That(_orderStatus == OrderStatus.Pending, $"the {_orderStatus} order can not be submitted");
            _orderStatus = OrderStatus.Submitted;
        }

        public void AllowEdit()
        {
            Require.That(_orderStatus == OrderStatus.Submitted, $"the {_orderStatus} order can not be pending again");
            _orderStatus = OrderStatus.Pending;
        }

        public void Paying()
        {
            Require.That(_orderStatus == OrderStatus.Submitted, $"can not start payment of the {_orderStatus} order");
            _orderStatus = OrderStatus.PaymentPending;
        }

        public void DiscardPaying()
        {
            Require.That(_orderStatus == OrderStatus.PaymentPending, $"the {_orderStatus} order can not be discarded");
            _orderStatus = OrderStatus.Submitted;
        }

        public void Payed()
        {
            Require.That(_orderStatus == OrderStatus.PaymentPending, $"the {_orderStatus} order can not be payed");
            _orderStatus = OrderStatus.Payed;

            AddDomainEvent(new OrderPayed(Id));
        }

        public void AwaitingShipment()
        {
            Require.That(_orderStatus == OrderStatus.Payed, $"awaiting shipment is not applicable for the {_orderStatus} order");
            _orderStatus = OrderStatus.Payed;
        }

        public void Ship()
        {
            Require.That(_orderStatus == OrderStatus.AwaitingShipment, $"can not ship items of the {_orderStatus} order");
            _orderStatus = OrderStatus.Shipped;
        }

        public void Complete()
        {
            Require.That(_orderStatus == OrderStatus.Shipped, $"can not complete the {_orderStatus} order");
            _orderStatus = OrderStatus.Shipped;
        }

        public void Cancel()
        {
            Require.That(_orderStatus.IsOneOf(OrderStatus.Pending, OrderStatus.Submitted), $"can not cancel the {_orderStatus} order");
            _orderStatus = OrderStatus.Shipped;
        }

        public IReadOnlyList<OrderItem> OrderItems => _orderItems;

        public OrderStatus OrderStatus => _orderStatus;

        public Address Address => _address;
    }
}
