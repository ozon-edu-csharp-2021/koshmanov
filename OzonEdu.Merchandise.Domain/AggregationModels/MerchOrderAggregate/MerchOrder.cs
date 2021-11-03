using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchOrder:Entity
    {
        public MerchOrder(Employee employee, List<MerchItem> merchItem, MerchManager merchManager, OrderState orderState)
        {
            Employee = employee;
            MerchItems = merchItem;
            MerchManager = merchManager;
            OrderState = orderState;
        }
        public Employee Employee { get; }
        public List<MerchItem> MerchItems { get; }
        public MerchManager MerchManager { get; }
        public OrderState OrderState { get; }
        
        public MerchType MerchType { get; }
    }
}