using MediatR;

namespace OzonEdu.Merchandise.Application.Commands.CreateMerchOrder
{
    public class CreateMerchOrderCommand:IRequest<long>
    {
        public long EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public long MerchPackId { get; set; }
    }
}