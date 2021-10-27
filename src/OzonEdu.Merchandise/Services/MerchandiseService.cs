using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.Services.Interfaces;
namespace OzonEdu.Merchandise.Services
{
    public class MerchandiseService: IMerchandiseService
    {
        private readonly GetMerchResponseModel response = new GetMerchResponseModel("Order");
        
        
        public Task<GetMerchResponseModel> GetMerch( CancellationToken _) =>Task.FromResult( response);
        
        
        public Task<GetMerchOrderStateResponseModel> GetMerchOrderState(long id, CancellationToken _)
        {
            GetMerchOrderStateResponseModel responseStatus = new GetMerchOrderStateResponseModel(  id, "OrderBegin");
            return Task.FromResult(responseStatus);
        }
    }
}