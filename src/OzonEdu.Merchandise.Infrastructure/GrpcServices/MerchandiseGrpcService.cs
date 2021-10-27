using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using OzonEdu.Merchandise.Grpc;

namespace OzonEdu.Merchandise.Infrastructure.GrpcServices
{
    public class MerchandiseGrpcService: MerchandiseGrpc.MerchandiseGrpcBase
    {
        public override async Task<GetMerchResponse> GetMerch(GetMerchRequest request, ServerCallContext context)
        {
            //return base.GetMerch(request, context);
            return new GetMerchResponse
            {
                Merch = {
                    new GetMerchResponseUnit
                    {
                        MerchId = 1,
                        MerchName = "bag"
                    }
                }
            };
        }
    }
}