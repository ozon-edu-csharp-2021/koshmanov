using System;
using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class CityValueObjectTest
    {
        [Fact]
        public void CreationCityInstanceSuccess()
        {
            //Arrange
            var cityName = "Moscow";
            //Action
            var res = new City("Moscow");
            //Assert
            Assert.Equal(cityName, res.Value);

        }
    }
}