using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.AddressAggregateTests
{
    public class PostalCodeValueObjectTests
    {
        [Fact]
        public void CreationPostalCodeInstanceSuccess()
        {
            //Arrange
            var postalCode = "Moscow";
            //Action
            var res = new PostalCode("Moscow");
            //Assert
            Assert.Equal(postalCode, res.Value);
        }
    }
}