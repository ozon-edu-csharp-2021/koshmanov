﻿using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate
{
    public class Country:ValueObject
    {
        public Country(string value)
        {
            Value = value;
        }
        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}