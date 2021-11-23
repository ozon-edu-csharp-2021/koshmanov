using System.Collections.Generic;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.Merchandise.Application.Contracts
{
    public interface IStockItemService
    {
        Task<List<long>> GetStockItem(MerchPack merchPack);
        Task<bool> CheckMerchPackExist(MerchPack merchPack);
    }
}