using System.Threading.Tasks;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository
    {
        Task<bool> CheckPackByIdAsync(int id, out MerchPack merchPack);
    }
}