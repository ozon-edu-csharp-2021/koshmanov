using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee:Entity
    {
        public Employee(FullName fullName, Contact contact)
        {
            FullName = fullName;
            Contact = contact;
        }
        public FullName FullName { get; }
        public Contact Contact { get; }
        //public booll IsNotified {get;} ? 
        
    }
}