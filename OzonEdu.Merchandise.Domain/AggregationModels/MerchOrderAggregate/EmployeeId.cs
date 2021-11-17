using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class EmployeeId: ValueObject
    {
        public long Value { get; }

        public EmployeeId(long value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}