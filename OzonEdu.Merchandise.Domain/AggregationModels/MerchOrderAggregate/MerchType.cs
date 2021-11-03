using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchType : Enumeration
    {
        public static readonly MerchType Greeting = new MerchType(1, "Greeting");
        public static readonly MerchType Incentive = new MerchType(1, "Incentive");
        public static readonly MerchType Congratulatory = new MerchType(1, "Congratulatory");
        public MerchType(int id, string name) : base(id, name)
        {
            
        }
    }
}