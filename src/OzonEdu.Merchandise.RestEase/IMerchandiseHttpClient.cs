using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Models;
using Microsoft.AspNetCore.Mvc;

namespace OzonEdu.Merchandise.RestEase
{
    public interface IMerchandiseHttpClient
    {
        [HttpGet("{id:long}/{itemName}")]
        Task<GetMerchResponse> GetMerch([FromRoute] long employeeId, [FromRoute]string itemName, CancellationToken token);
        
        [HttpGet]
        Task<GetOrderStateResponse> GetMerchGetMerchOrderState([FromQuery]long id, CancellationToken token);
    }
}