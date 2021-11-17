using System.Collections.Generic;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;

namespace OzonEdu.Merchandise.Application.Contracts
{
    public interface IStockItemService
    {
        Task<List<long>> GetStockItem(MerchPack merchPack);
        Task<bool> CheckMerchPackExist(MerchPack merchPack);
    }
}