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
        private readonly GetOrderStateResponse _orderStateResponse = new GetOrderStateResponse(MerchOrderStatus.Other);

        public Task<GetMerchResponse> GetMerch(GetMerchRequest request, CancellationToken _)
        {
            var response = new GetMerchResponse(new MerchOrder(1, new List<MerchItem>(){new MerchItem( request.MerchItem.Name) }));
            return Task.FromResult( response);
        }
        public Task<GetOrderStateResponse> GetMerchOrderState(GetOrderStateRequest id, CancellationToken _)=>Task.FromResult( _orderStateResponse);
    }
}