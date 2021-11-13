using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee:Entity, IAggregateRoot
    {
        public Employee(FullName fullName, Contact contact, IsNotified isNotified)
        {
            FullName = fullName;
            Contact = contact;
            IsNotified = isNotified;
        }
        public FullName FullName { get; }
        public Contact Contact { get; }
        public IsNotified IsNotified {get;}  
        
    }
}