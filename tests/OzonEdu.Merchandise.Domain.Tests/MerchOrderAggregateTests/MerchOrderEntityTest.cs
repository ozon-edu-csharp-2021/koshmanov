using System;
using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.AddressAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.NamesAggregate;
using OzonEdu.Merchandise.Domain.Exceptions;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.MerchOrderAggregateTests
{
    public class CityValueObjectTest
    {
        [Fact]
        public void CreationCityInstanceSuccess()
        {
            //Arrange

            //Action

            //Assert

        }
        
        [Fact]
        public void UpdateOrderStateSuccess()
        {
            //Arrange
            var merchOrder = new MerchOrder(new Employee(
                    new FullName(new FirstName("Игорь"), new SecondName(""), new Patronymic("")),
                    Position.Developer, new Department(new Address(new Country(""), new Region(""), new District(""),
                        new City(""),
                        new Street(""), new PostalCode("")), new DepName("")), new Address(new Country(""),
                        new Region(""),
                        new District(""), new City(""),
                        new Street(""), new PostalCode("")), WorkFormat.Local,
                    new Contact(new Phone(""), new Email(""))),
                new List<MerchItem>(), new MerchManager(
                    new FullName(new FirstName(""), new SecondName(""), new Patronymic("")),
                    new Department(new Address(new Country(""), new Region(""), new District(""), new City(""),
                        new Street(""), new PostalCode("")), new DepName(""))), MerchType.Greeting);
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => merchOrder.UpdateOrderState(newOrderState));
        }
    }
}