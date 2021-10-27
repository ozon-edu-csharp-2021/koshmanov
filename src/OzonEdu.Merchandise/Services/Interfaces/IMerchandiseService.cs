using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Models;

namespace OzonEdu.Merchandise.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<GetMerchResponseModel> GetMerch( CancellationToken _);
        Task<GetMerchOrderStateResponseModel> GetMerchOrderState(long id, CancellationToken _);
    }
}