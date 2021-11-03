using System;
using System.Collections.Generic;
using AutoFixture;
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
        public void UpdateNewOrderStateSuccess()
        {
            var newMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.New)
                .Create();
            //Arrange
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => newMerchOrder.UpdateOrderState(newOrderState));
        }
        
        [Fact]
        public void UpdateInProgressOrderStateSuccess()
        {
            var inProgressMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.InProgress)
                .Create();
            //Arrange
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => inProgressMerchOrder.UpdateOrderState(newOrderState));
        }
        
        [Fact]
        public void UpdateCanceledOrderStateSuccess()
        {
            var canceledMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.Canceled)
                .Create(); 
            //Arrange
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => canceledMerchOrder.UpdateOrderState(newOrderState));
        }
        
        [Fact]
        public void UpdateWaitingOrderStateSuccess()
        {
            var waitingMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.Waiting)
                .Create();
            
            //Arrange
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => waitingMerchOrder.UpdateOrderState(newOrderState));
        }
        
        [Fact]
        public void UpdateGiveOutOrderStateSuccess()
        {
            var giveOutMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.GiveOut)
                .Create();
        
            //Arrange
           
            var newOrderState = OrderState.Completed;
            //Action

            //Assert
            Assert.Throws<Exception>(() => giveOutMerchOrder.UpdateOrderState(newOrderState));
        }
        
        [Theory]
        [MemberData(nameof(GetOrderStateList))]
        public void UpdateCompletedOrderStateSuccess(List<OrderState> list)
        {
            //Arrange
            var completedMerchOrder = new Fixture().Build<MerchOrder>()
                .With(x => x.CurrentOrderState, OrderState.Completed)
                .Create();
           
            //Action

            //Assert
           list.ForEach(x=> Assert.Throws<Exception>(() =>completedMerchOrder.UpdateOrderState(x)));
        }

        public static IEnumerable<object[]> GetOrderStateList()
        {
            yield return new object[]
            {
                new List<OrderState>()
                {
                    OrderState.Canceled, OrderState.Completed, OrderState.New, 
                    OrderState.Other, OrderState.Waiting,
                    OrderState.GiveOut, OrderState.InProgress
                }
            };
        }
    }
}