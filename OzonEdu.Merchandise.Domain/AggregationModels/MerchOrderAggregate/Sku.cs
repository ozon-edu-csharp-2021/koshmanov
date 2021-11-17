using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class Sku : ValueObject
    {
        public long Value { get; }

        public Sku(long value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}