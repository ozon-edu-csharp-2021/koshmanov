using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.Names;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchItem : Entity
    {
        public MerchItem(MerchItemName merchItemName, Sku sku)
        {
            MerchItemName = merchItemName;
            Sku = sku;
        }
        public MerchItemName MerchItemName { get; }
        
        public Sku Sku { get; }
        
    }
}