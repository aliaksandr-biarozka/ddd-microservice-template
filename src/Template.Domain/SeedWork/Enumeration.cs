using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Template.Domain.SeedWork
{
    public abstract class Enumeration : IComparable, IEquatable<Enumeration>
    {
        public int Id { get; }

        public string Name { get; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        public bool Equals(Enumeration other)
        {
            if (other == null) return false;

            return GetType() == other.GetType() && Id == other.Id && Name == other.Name;
        }

        public override bool Equals(object obj) => Equals(obj as Enumeration);

        public override int GetHashCode() => (Id, Name).GetHashCode();

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }
    }

    public static class EnumerationExtensions
    {
        public static bool IsOneOf<T>(this T item, params T[] items) where T: Enumeration
        {
            return items.Contains(item);
        }
    }
}
