using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee:Entity
    {
        public Employee(FullName fullName, Position position, Department department, Address address, WorkFormat workFormat, Contact contact)
        {
            FullName = fullName;
            Position = position;
            Department = department;
            Address = address;
            WorkFormat = workFormat;
            Contact = contact;
        }
        public FullName FullName { get; }
        public Position Position { get; }
        public Department Department { get; }
        public Address Address { get; }
        public WorkFormat WorkFormat { get; }
        public Contact Contact { get; }
        //public booll IsNotified {get;} ? 
        
    }
}