using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class DistrictValueObjectTests
    {
        [Fact]
        public void CreationDistrictInstanceSuccess()
        {
            //Arrange
            var cityName = "Moscow";
            //Action
            var res = new District("Moscow");
            //Assert
            Assert.Equal(cityName, res.Value);
        }
    }
}