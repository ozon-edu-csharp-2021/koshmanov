using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Models;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces
{
    public interface IMerchandiseService
    {
        Task<GetMerchResponse> GetMerch(GetMerchRequest request,  CancellationToken token);
        Task<GetOrderStateResponse> GetMerchOrderState(GetOrderStateRequest request, CancellationToken token);
    }
}