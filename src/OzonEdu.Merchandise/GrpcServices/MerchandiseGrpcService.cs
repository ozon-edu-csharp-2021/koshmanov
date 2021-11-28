using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.Merchandise.Grpc;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.Services.Interfaces;
using Employee = OzonEdu.Merchandise.Models.Employee;

namespace OzonEdu.Merchandise.GrpcServices
{
    public class MerchandiseGrpcService: MerchandiseGrpc.MerchandiseGrpcBase
    {

        private readonly IMerchandiseService _merchandiseService;

        public MerchandiseGrpcService(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        public override async Task<GetMerchOrderStateResponseGrpc> GetMerchOrderState(GetMerchOrderStateRequestGrpc request, ServerCallContext context)
        {
            GetOrderStateRequest httpRequest = new GetOrderStateRequest(new MerchOrder
                (1, new List<MerchItem>() {new MerchItem("T-shirt")}));
            
            var merchOrderState = await _merchandiseService.GetMerchOrderState(httpRequest, context.CancellationToken);
            return new GetMerchOrderStateResponseGrpc()
            {
                State = merchOrderState.Status == MerchOrderStatus.New? OrderState.New: 
                        merchOrderState.Status == MerchOrderStatus.InProgress? OrderState.InProgress:
                        merchOrderState.Status == MerchOrderStatus.GiveOut? OrderState.GiveOut:
                        OrderState.Other
            };
        }
        public override async Task<GetMerchResponseGrpc> GetMerch(GetMerchRequestGrpc request, ServerCallContext context)
        {
            var httpRequest = new GetMerchRequest(
                new Employee(request.Employee.Id, request.Employee.Name),
                new MerchItem(request.Merch.Name)
            );
            var merch = await _merchandiseService.GetMerch(httpRequest , context.CancellationToken);
            return new GetMerchResponseGrpc()
            {
                Merch = new MerchOrderUnit
                {
                    MerchOrderId = merch.Order.Id,
                    Merch = new MerchUnit
                    {
                        Name = merch.Order.MerchItems.First().Name
                    },
                    State = merch.Order.Status == MerchOrderStatus.New? OrderState.New: 
                            merch.Order.Status == MerchOrderStatus.InProgress? OrderState.InProgress:
                            merch.Order.Status == MerchOrderStatus.GiveOut? OrderState.GiveOut:
                            OrderState.Other
                }
            };
        }
    }
}