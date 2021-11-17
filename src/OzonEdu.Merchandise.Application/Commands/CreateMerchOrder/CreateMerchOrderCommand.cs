using MediatR;

namespace OzonEdu.Merchandise.Application.Commands.CreateMerchOrder
{
    public class CreateMerchOrderCommand:IRequest<int>
    {
        public int EmloyeeId { get; set; }
        public string EmloyeeEmail { get; set; }
        public int MerchPackType { get; set; }
    }
}