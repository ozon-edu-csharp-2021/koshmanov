using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public class OrderStateChangedToInProgressDomainEvent:INotification
    {
        public OrderStateChangedToInProgressDomainEvent(MerchOrder merchOrder)
        {
            MerchOrder = merchOrder;
        }
        public MerchOrder MerchOrder { get; }
    }
}