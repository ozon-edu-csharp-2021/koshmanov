using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public class OrderStateChangedToCancelledEvent:INotification
    {
        public OrderStateChangedToCancelledEvent(MerchOrder merchOrder)
        {
            MerchOrder = merchOrder;
        }
        public MerchOrder MerchOrder { get; }
    }
}