using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository
    {
        Task<ICollection<MerchPack>> GetPackListByMerchTypeIdAsync(int merchPackTypeId, CancellationToken token);
        Task<MerchPack> GetPackByIdAsync(long id, CancellationToken token);
    }
}