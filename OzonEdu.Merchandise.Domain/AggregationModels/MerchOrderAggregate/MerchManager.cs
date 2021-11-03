using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchManager:Entity
    {
        public MerchManager(FullName fullName,  Department department)
        {
            FullName = fullName;
            Department = department; 
        }
        public FullName FullName { get; }
        public Department Department { get; }
    }
}