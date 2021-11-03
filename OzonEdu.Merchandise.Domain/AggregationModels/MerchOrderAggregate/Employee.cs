using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class Employee:Entity
    {
        public Employee(FullName fullName, Position position, Department department, Address address, WorkFormat workFormat)
        {
            FullName = fullName;
            Position = position;
            Department = department;
            Address = address;
            WorkFormat = workFormat;
        }
        public FullName FullName { get; }
        public Position Position { get; }
        public Department Department { get; }
        public Address Address { get; }
        public WorkFormat WorkFormat { get; }
        
        //public booll IsNotified {get;} ? 
        
    }
}