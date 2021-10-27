using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.Merchandise.Grpc;
using OzonEdu.Merchandise.Services.Interfaces;

namespace OzonEdu.Merchandise.GrpcServices
{
    public class MerchandiseGrpcService: MerchandiseGrpc.MerchandiseGrpcBase
    {

        private readonly IMerchandiseService _merchandiseService;

        public MerchandiseGrpcService(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }

        public override async Task<GetMerchOrderStateResponse> GetMerchOrderState(GetMerchOrderStateRequest request, ServerCallContext context)
        {
            var merchOrderState = await _merchandiseService.GetMerchOrderState(request.Id, context.CancellationToken);
            return new GetMerchOrderStateResponse
            {
                Id = merchOrderState.Id,
                State = merchOrderState.Status
            };
        }

        public override async Task<GetMerchResponse> GetMerchV2(Empty request, ServerCallContext context)
        {
            var merch = await _merchandiseService.GetMerch(context.CancellationToken);
            return new GetMerchResponse
            {
                Merch = {
                    new GetMerchResponseUnit
                    {
                        MerchId = 1,
                        MerchName = merch.Order
                    }
                }
            };
        }

        public override async Task<GetMerchResponse> GetMerch(GetMerchRequest request, ServerCallContext context)
        {
            var merch = await _merchandiseService.GetMerch(context.CancellationToken);
            return new GetMerchResponse
            {
                Merch = {
                    new GetMerchResponseUnit
                    {
                        MerchId = 1,
                        MerchName = merch.Order
                    }
                }
            };
        }

        public override async Task<GetMerchResponseWithNulls> GetMerchWithNulls(Empty request, ServerCallContext context)
        {
            var merch = await _merchandiseService.GetMerch(context.CancellationToken);
            return new GetMerchResponseWithNulls
            {
                Merch =
                {
                    new List<GetMerchResponseUnitWithNulls>
                    {
                        new GetMerchResponseUnitWithNulls
                        {
                            MerchId = 1,
                            MerchName = merch.Order
                        },
                        new GetMerchResponseUnitWithNulls
                        {
                            MerchId = 2,
                            MerchName = merch.Order
                        }
                    }
                }
            };
              
        }
    }
}