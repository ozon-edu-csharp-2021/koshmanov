using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public class CreatedNewMerchOrderDomainEvent : INotification
    {
        public CreatedNewMerchOrderDomainEvent(MerchOrder merchOrder)
        {
            MerchOrder = merchOrder;
        }
        public MerchOrder MerchOrder { get; }
    }
}