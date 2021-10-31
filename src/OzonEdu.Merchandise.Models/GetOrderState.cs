namespace OzonEdu.Merchandise.Models
{
    public class GetOrderStateRequest
    {
        public GetOrderStateRequest(MerchOrder order)
        {
            Order = order;
        }

        public MerchOrder Order { get; }
    }
    public class GetOrderStateResponse
    {
        public GetOrderStateResponse(MerchOrderStatus status)
        {
            Status = status;
        }

        public MerchOrderStatus Status { get; }
        
    }
}