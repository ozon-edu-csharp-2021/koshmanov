using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class CountryValueObjectTests
    {
        [Fact]
        public void CreationCountryInstanceSuccess()
        {
            //Arrange
            var cityName = "Moscow";
            //Action
            var res = new Country("Moscow");
            //Assert
            Assert.Equal(cityName, res.Value);
        }
    }
}