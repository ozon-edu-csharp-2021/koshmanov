using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;
using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class Address:ValueObject
    {
        public Address(Country country, Region region, District district, City city, Street street, PostalCode postalCode)
        {
            Country = country;
            Region = region;
            District = district;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }
        public Country Country { get; }
        public Region Region { get; }
        public District District { get; }
        public City City { get; }
        public Street Street { get; }
        public PostalCode PostalCode { get; }
            
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return Region;
            yield return District;
            yield return City;
            yield return Street;
            yield return PostalCode;
        }
    }
}