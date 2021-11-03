using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;


namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class RegionValueObjectTests
    {
        [Fact]
        public void CreationRegionInstanceSuccess()
        {
            //Arrange
            var region = "Moscow";
            //Action
            var res = new Region("Moscow");
            //Assert
            Assert.Equal(region, res.Value);
        }
    }
}