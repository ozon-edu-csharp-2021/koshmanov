using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;
using OzonEdu.Merchandise.Domain.AggregationModels.Names;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class FullName:ValueObject
    {
        public FullName(FirstName firstName, SecondName secondName, Patronymic patronymic)
        {
            FirstName = firstName;
            SecondName = secondName;
            Patronymic = patronymic;
        }
        public FirstName FirstName { get; set; }
        public SecondName SecondName { get; set; }
        public Patronymic Patronymic { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
            yield return Patronymic;
        }
    }
}