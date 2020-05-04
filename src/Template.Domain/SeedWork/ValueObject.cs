using System.Collections.Generic;
using System.Linq;

namespace Template.Domain.SeedWork
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if(left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

        protected abstract IEnumerable<object> EqualityCheckAttributes { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ValueObject valueObject)) return false;

            var thisAttributes = EqualityCheckAttributes.GetEnumerator();
            var otherAttributes = valueObject.EqualityCheckAttributes.GetEnumerator();
            while (thisAttributes.MoveNext() && otherAttributes.MoveNext())
            {
                if (thisAttributes.Current is null ^ otherAttributes.Current is null)
                {
                    return false;
                }

                if (thisAttributes.Current != null && !thisAttributes.Current.Equals(otherAttributes.Current))
                {
                    return false;
                }
            }

            return !thisAttributes.MoveNext() && !otherAttributes.MoveNext();
        }

        public override int GetHashCode() => EqualityCheckAttributes.Aggregate(17,
                (current, attribute) => current * 31 + (attribute == null ? 0 : attribute.GetHashCode()));
    }
}
