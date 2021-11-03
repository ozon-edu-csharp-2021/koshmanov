using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class StreetValueObjectTests
    {
        [Fact]
        public void CreationStreetInstanceSuccess()
        {
            //Arrange
            var street = "Moscow";
            //Action
            var res = new Street("Moscow");
            //Assert
            Assert.Equal(street, res.Value);
        }
    }
}