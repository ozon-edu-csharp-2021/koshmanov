using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Contact:ValueObject
    {
        public Contact(Phone phone, Email email)
        {
            Phone = phone;
            Email = email;
        }
        public Phone Phone {get;}
        public Email Email {get;}
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Phone;
            yield return Email;
        }
    }
}