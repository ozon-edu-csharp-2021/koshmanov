using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Interfaces;

namespace OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Mock
{
    public class MerchOrderRepository:IMerchOrderRepository
    {
        public void SetStorageContext(IStorageContext storageContext)
        {
         
        }

        public IEnumerable<MerchOrder> All()
        {
            throw new System.NotImplementedException();
        }

        public MerchOrder GetMerchOrderById(long id)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveNewMerchOrderDomainEvent(MerchOrder order)
        {
            throw new System.NotImplementedException();
        }

        public OrderState CheckOrderState(MerchOrder order)
        {
            throw new System.NotImplementedException();
        }
    }
}