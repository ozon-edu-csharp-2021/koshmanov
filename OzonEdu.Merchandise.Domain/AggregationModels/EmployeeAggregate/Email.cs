using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.Merchandise.Domain.Exceptions;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public class Email:ValueObject
    {
        private Email(string value)
        => Value = value;
        
        public string Value { get; }
        
        public static Email Create(string emailString)
        {
            if (IsValidEmail(emailString))
            {
                return new Email(emailString);
            }
            throw new InvalidEmailException($"Email is invalid: {emailString}");
        }
        private static bool IsValidEmail(string emailString)
            => Regex.IsMatch(emailString, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}