using System;
using System.Collections.Generic;
using System.Linq;

namespace Template.Domain.SeedWork
{
    public abstract class ValueObject : IEquatable<ValueObject>
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

        public bool Equals(ValueObject other)
        {
            if (other == null) return false;

            var thisAttributes = EqualityCheckAttributes.GetEnumerator();
            var otherAttributes = EqualityCheckAttributes.GetEnumerator();
            while(thisAttributes.MoveNext() && otherAttributes.MoveNext())
            {
                if(thisAttributes.Current is null ^ otherAttributes.Current is null)
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

        public override bool Equals(object obj) => Equals(obj as ValueObject);

        public override int GetHashCode() => EqualityCheckAttributes.Aggregate(17,
                (current, attribute) => current * 31 + (attribute == null ? 0 : attribute.GetHashCode()));
    }
}
