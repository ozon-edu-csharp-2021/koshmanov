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
        
        [Theory]
        [MemberData(nameof(GetOrderStateListWithoutCompleted))]
        public void UpdateGiveOutOrderStateSuccess(List<OrderState> list)
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchItem = new Fixture().Build<MerchItem>().Create();
            var merchManager = new Fixture().Build<MerchManager>().Create();
            var itemList = new List<MerchItem>(){merchItem};
            var merchOrder = new MerchOrder(employee, itemList, merchManager, OrderState.GiveOut, MerchPackType.Greeting); 
            
            //giveOutMerchOrder.Customize<MerchOrder>(x => x.FromFactory(() => merchOrder), .Build<MerchOrder>() );
        
            //Arrange
           
            //var newOrderState = OrderState.Completed;
            //Action

            //Assert
            list.ForEach(x=> Assert.Throws<Exception>(() =>merchOrder.UpdateOrderState(x)));
        }
        
        [Theory]
        [MemberData(nameof(GetOrderStateList))]
        public void UpdateCompletedOrderStateSuccess(List<OrderState> list)
        {
            //Arrange
            var employee = new Fixture().Build<Employee>().Create();
            var merchItem = new Fixture().Build<MerchItem>().Create();
            var merchManager = new Fixture().Build<MerchManager>().Create();
            var itemList = new List<MerchItem>(){merchItem};
            var merchOrder = new MerchOrder(employee, itemList, merchManager, OrderState.Completed, MerchPackType.Greeting); 

            //Action

            //Assert
            string it;
            foreach (var state in list)
            {
                if (state == OrderState.Completed)
                     it = "";
                Assert.Throws<Exception>(() => merchOrder.UpdateOrderState(state));
            }
           //list.ForEach(x=> Assert.Throws<Exception>(() =>merchOrder.UpdateOrderState(x)));
        }

        public static IEnumerable<object[]> GetOrderStateList()
        {
            yield return new object[]
            {
                new List<OrderState>()
                {
                    OrderState.Canceled, OrderState.New, 
                    OrderState.Other, OrderState.Waiting,
                    OrderState.GiveOut, OrderState.InProgress, OrderState.Completed
                }
            };
        }
        
        
        public static IEnumerable<object[]> GetOrderStateListWithoutCompleted()
        {
            yield return new object[]
            {
                new List<OrderState>()
                {
                    OrderState.Canceled, OrderState.New, 
                    OrderState.Other, OrderState.Waiting,
                    OrderState.GiveOut, OrderState.InProgress
                }
            };
        }
    }
}