using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Events;


namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public interface IMerchOrderRepository:IRepository<MerchOrder>
    {
        Task<MerchOrder> CreateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default);
        Task<MerchOrder> UpdateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default);
        Task<MerchOrder> FindById(long id, CancellationToken cancellationToken = default);
        Task<MerchOrder> CheckOrderState(MerchOrder order);
    }
}