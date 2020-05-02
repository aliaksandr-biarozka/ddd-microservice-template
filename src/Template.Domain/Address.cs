using System.Collections.Generic;
using Template.Domain.SeedWork;

namespace Template.Domain
{
    public class Address : ValueObject
    {
        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string Country { get; }

        public string PostalCode { get; }

        public Address(string street, string city, string state, string country, string postalcode)
        {
            Require.ThatNotNullOrEmpty(street, "street and house number can not be empty");
            Require.ThatNotNullOrEmpty(country, "country can not be empty");
            Require.ThatNotNullOrEmpty(postalcode, "postal code can not be empty");

            Street = street;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalcode;
        }

        protected override IEnumerable<object> EqualityCheckAttributes
        {
            get
            {
                yield return Street;
                yield return City;
                yield return State;
                yield return Country;
                yield return PostalCode;
            }
        }
    }
}
