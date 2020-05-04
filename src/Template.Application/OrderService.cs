using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Application.SeedWork;
using Template.Domain;

namespace Template.Application
{
    public interface IOrderService
    {
        Task<long> Create(Guid customerId, IEnumerable<OrderItemDto> orderItems);
        Task AddProducts(int orderId, OrderItemDto orderItem);
        Task ChangeAddress(int orderId, string street, string city, string state, string country, string postalCode);
        Task RemoveProduct(int orderId, int productId, int quantity);
        Task Submit(int orderId);
        Task MakeEditable(int orderId);
        Task Cancel(int orderId);
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
    }

    public enum DiscountType
    {
        None,
        AbsoluteValue,
        Percentage
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Create(Guid customerId, IEnumerable<OrderItemDto> orderItems)
        {
            var order = new Order(customerId);

            foreach(var orderItem in orderItems)
            {
                order.AddProducts(orderItem.ProductId, orderItem.ProductName, orderItem.Quantity, orderItem.Price, ToDiscount(orderItem.DiscountType, orderItem.DiscountAmount));
            }

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();

            return order.Id;
        }

        public async Task AddProducts(int orderId, OrderItemDto orderItem)
        {
            var order = await _orderRepository.Get(orderId);

            order.AddProducts(orderItem.ProductId, orderItem.ProductName, orderItem.Quantity, orderItem.Price, ToDiscount(orderItem.DiscountType, orderItem.DiscountAmount));

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        public async Task RemoveProduct(int orderId, int productId, int quantity)
        {
            var order = await _orderRepository.Get(orderId);

            order.RemoveProducts(productId, quantity);

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        public async Task ChangeAddress(int orderId, string street, string city, string state, string country, string postalCode)
        {
            var order = await _orderRepository.Get(orderId);

            order.ChangeAddress(new Address(street, city, state, country, postalCode));

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        public async Task Submit(int orderId)
        {
            var order = await _orderRepository.Get(orderId);

            order.Submit();

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        public async Task MakeEditable(int orderId)
        {
            var order = await _orderRepository.Get(orderId);

            order.AllowEdditing();

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        public async Task Cancel(int orderId)
        {
            var order = await _orderRepository.Get(orderId);

            order.Cancel();

            await _orderRepository.Save(order);
            await _unitOfWork.Commit();
        }

        private static Discount ToDiscount(DiscountType discountType, decimal amount)
        {
            return discountType switch
            {
                DiscountType.Percentage => new Discount(amount, Domain.DiscountType.Percentage),
                DiscountType.AbsoluteValue => new Discount(amount, Domain.DiscountType.AbsoluteValue),
                _ => null
            };
        }
    }
}
