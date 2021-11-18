namespace OzonEdu.Merchandise.Infrastructure.Repositories.Models
{
    public class MerchOrderDto
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int MerchPackId { get; set; }
        public int StatusId { get; set; }
    }
}