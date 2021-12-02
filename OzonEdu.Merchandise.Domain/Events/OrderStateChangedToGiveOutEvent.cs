using MediatR;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.Merchandise.Domain.Events
{
    public sealed record OrderStateChangedToGiveOutEvent:INotification
    {
        public PackId MerchPackId { get; init; }

        public OrderState NewState { get; init; }

        public EmployeeId EmployeeId { get; init; }
    }
}