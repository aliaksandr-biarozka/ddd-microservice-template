using System.Collections.Generic;
using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class Discount : ValueObject
    {
        public decimal Value { get; }

        public DiscountType DiscountType { get; }

        public Discount(decimal value, DiscountType discountType)
        {
            Require.That(discountType != null, "discount type is not provided");

            Value = value;
            DiscountType = discountType;
        }

        public decimal ApplyTo(decimal price)
        {
            return DiscountType == DiscountType.AbsoluteValue
                ? price - Value
                : price - price * Value;
        }

        protected override IEnumerable<object> EqualityCheckAttributes
        {
            get
            {
                yield return Value;
                yield return DiscountType;
            }
        }
    }

    public class DiscountType : Enumeration
    {
        private DiscountType(int id, string name): base(id, name) { }

        public static DiscountType AbsoluteValue = new DiscountType(1, "absolute value");

        public static DiscountType Percentage = new DiscountType(2, "percentage");
    }
}
