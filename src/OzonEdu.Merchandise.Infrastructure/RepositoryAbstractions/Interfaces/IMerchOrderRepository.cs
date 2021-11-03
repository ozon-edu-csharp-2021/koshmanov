using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Events;

namespace OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Interfaces
{
    public interface IMerchOrderRepository:IRepository
    {
        IEnumerable<MerchOrder> All();
        MerchOrder GetMerchOrderById(long id);
        bool SaveNewMerchOrderDomainEvent(MerchOrder order);
        OrderState CheckOrderState(MerchOrder order);
    }
}