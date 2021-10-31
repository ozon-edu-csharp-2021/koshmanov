namespace OzonEdu.Merchandise.Models
{
  
    
    public class GetMerchResponse
    {
        public GetMerchResponse(MerchOrder order)
        {
            Order = order;
        }

        public MerchOrder Order { get; }
    }
    public class GetMerchRequest
    {
        public GetMerchRequest(Employee employee, MerchItem merchItem)
        {
            Employee = employee;
            MerchItem = merchItem;
        }

        public Employee Employee{ get; }
        public MerchItem MerchItem{ get; }
    }
    
}