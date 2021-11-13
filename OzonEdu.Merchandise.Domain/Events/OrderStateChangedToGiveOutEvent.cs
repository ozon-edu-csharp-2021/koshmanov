using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public class OrderStateChangedToGiveOutEvent:INotification
    {
        public OrderStateChangedToGiveOutEvent(MerchOrder merchOrder)
        {
            MerchOrder = merchOrder;
        }
        public MerchOrder MerchOrder { get; }
    }
}