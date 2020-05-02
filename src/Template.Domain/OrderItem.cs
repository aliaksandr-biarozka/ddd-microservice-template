using Template.Domain.SeedWork;
using static Template.Domain.SeedWork.Entity<int>;

namespace Template.Domain
{
    public class OrderItem : IntEntity
    {
        public int ProductId { get; }

        public string ProductName { get; }

        public int Quantity { get; private set; }

        public decimal Price { get; }

        public Discount Discount { get; private set; }

        public OrderItem(int productId, string productName, int quantity, decimal price, Discount discount)
        {
            Require.That(productId > 0, $"product with id = {productId} is not exist");
            Require.That(quantity > 0, "at least one product's unit should be provided");
            Require.That(price >= 0, $"provided price {price} is invalid");
            Require.That((discount?.ApplyTo(price) ?? 0) >= 0, "provided discount is impossible to apply");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }

        public decimal GetTotalPrice() => Discount?.ApplyTo(Quantity * Price) ?? Quantity * Price;

        internal void SetNewDiscount(Discount discount) => Discount = discount;

        public void AddItems(int count) => Quantity += count;

        public void RemoveItems(int count)
        {
            Require.That(Quantity - count > 0, "can not remove all items");
            Quantity -= count;
        }
    }
}
