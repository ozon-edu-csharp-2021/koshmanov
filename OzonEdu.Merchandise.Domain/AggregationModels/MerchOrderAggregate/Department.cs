using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.NamesAggregate;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class Department:ValueObject
    {
        public Department(Address address, DepName depName)
        {
            Address = address;
            DepName = depName;
        }
        public Address Address { get; }
        public DepName DepName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
            yield return DepName;
        }
    }
}