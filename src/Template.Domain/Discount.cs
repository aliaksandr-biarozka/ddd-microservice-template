using System.Collections.Generic;
using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class Discount : ValueObject
    {
        public decimal Amount { get; }

        public DiscountType DiscountType { get; }

        public Discount(decimal amount, DiscountType discountType)
        {
            Require.That(discountType != null, "discount type is not provided");

            Amount = amount;
            DiscountType = discountType;
        }

        public decimal ApplyTo(decimal price)
        {
            return DiscountType == DiscountType.AbsoluteValue
                ? price - Amount
                : price - price * Amount;
        }

        protected override IEnumerable<object> EqualityCheckAttributes
        {
            get
            {
                yield return Amount;
                yield return DiscountType;
            }
        }
    }

    public class DiscountType : Enumeration
    {
        private DiscountType(int id, string name): base(id, name) { }

        public static DiscountType AbsoluteValue = new DiscountType(1, nameof(AbsoluteValue));

        public static DiscountType Percentage = new DiscountType(2, nameof(Percentage));
    }
}
