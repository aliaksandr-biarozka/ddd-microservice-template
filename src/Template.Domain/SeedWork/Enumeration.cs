using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Template.Domain.SeedWork
{
    public abstract class Enumeration : IComparable, IEquatable<Enumeration>
    {
        private int _id;

        private string _name;

        protected Enumeration(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string ToString() => _name;

        public int CompareTo(object obj) => _id.CompareTo(((Enumeration)obj)._id);

        public bool Equals(Enumeration other)
        {
            if (other == null) return false;

            return GetType() == other.GetType() && _id == other._id && _name == other._name;
        }

        public override bool Equals(object obj) => Equals(obj as Enumeration);

        public override int GetHashCode() => (_id, _name).GetHashCode();

        public IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }
    }
}
