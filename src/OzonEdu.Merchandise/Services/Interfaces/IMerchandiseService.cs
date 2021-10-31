using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Models;

namespace OzonEdu.Merchandise.Services.Interfaces
{
    public interface IMerchandiseService
    {
        Task<GetMerchResponse> GetMerch(GetMerchRequest request,  CancellationToken token);
        Task<GetOrderStateResponse> GetMerchOrderState(GetOrderStateRequest request, CancellationToken token);
    }
}