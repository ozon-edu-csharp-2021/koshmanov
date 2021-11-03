using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.NamesAggregate;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchItem : ValueObject
    {
        public MerchItem(MerchType merchType, MerchItemName name)
        {
            MerchType = merchType;
            MerchItemName = name;
        }
        public MerchType MerchType { get; set; }
        public MerchItemName MerchItemName { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MerchType;
            yield return MerchItemName;
        }
    }
}