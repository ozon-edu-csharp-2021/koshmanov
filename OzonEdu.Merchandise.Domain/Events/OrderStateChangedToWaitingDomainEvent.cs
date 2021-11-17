using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public class OrderStateChangedToWaitingDomainEvent:INotification
    {
        public OrderStateChangedToWaitingDomainEvent(MerchOrder merchOrder)
        {
            MerchOrder = merchOrder;
        }
        public MerchOrder MerchOrder { get; }
    }
}