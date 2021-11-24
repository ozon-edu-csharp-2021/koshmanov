using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee:Entity, IAggregateRoot
    {
        private Employee(long id, Email email)
        {
            Id = id;
            Email = email;
        }
        public Email Email {get; private set; }

        public static Employee Create(long id, Email email)
        {
            return new Employee(id, email);
        }
        
        public void UpdateEmail(Email email)
        {
            Email = email;
        }
    }
}