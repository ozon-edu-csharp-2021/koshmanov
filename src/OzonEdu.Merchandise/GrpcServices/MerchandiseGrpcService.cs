using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OzonEdu.Merchandise.Grpc;
using OzonEdu.Merchandise.Models;
using OzonEdu.Merchandise.Services.Interfaces;
using Employee = OzonEdu.Merchandise.Models.Employee;
using ILogger = Serilog.ILogger;

namespace OzonEdu.Merchandise.GrpcServices
{
    public class MerchandiseGrpcService: MerchandiseGrpc.MerchandiseGrpcBase
    {

        private readonly IMerchandiseService _merchandiseService;
        private readonly ILogger<MerchandiseGrpcService> _logger;
        private readonly ITracer _tracer;

        public MerchandiseGrpcService(IMerchandiseService merchandiseService, ILogger<MerchandiseGrpcService> logger, ITracer tracer)
        {
            _merchandiseService = merchandiseService;
            _logger = logger;
            _tracer = tracer;
        }

        public override async Task<GetMerchOrderStateResponseGrpc> GetMerchOrderState(GetMerchOrderStateRequestGrpc request, ServerCallContext context)
        {
            using var span = _tracer.BuildSpan("GetMerchOrderState")
                .StartActive();
            
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