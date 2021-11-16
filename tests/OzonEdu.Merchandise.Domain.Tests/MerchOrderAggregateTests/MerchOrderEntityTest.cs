using System;
using System.Collections.Generic;
using AutoFixture;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Exceptions;
using Xunit;

namespace OzonEdu.Merchandise.Domain.Tests.MerchOrderAggregateTests
{
    public class MerchOrderValueObjectTest
    {
        [Fact]
        public void UpdateNewOrderStateSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.New);
            merchOrder.SetInProgressStatus();
            Assert.Equal(OrderState.InProgress, merchOrder.CurrentOrderState); 
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.New);
            merchOrder.SetWaitingStatus();
            Assert.Equal(OrderState.Waiting, merchOrder.CurrentOrderState);
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.New);
            merchOrder.SetCancelledStatus();
            Assert.Equal(OrderState.Cancelled, merchOrder.CurrentOrderState);
        }
        
        [Fact]
        public void UpdateNewOrderStateExceptionSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.New);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetGiveOutStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.New);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCompletedStatus());
        }
        
        [Fact]
        public void UpdateInProgressOrderStateSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.InProgress);
            merchOrder.SetGiveOutStatus();
            Assert.Equal(OrderState.GiveOut, merchOrder.CurrentOrderState); 
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.InProgress);
            merchOrder.SetCancelledStatus();
            Assert.Equal(OrderState.Cancelled, merchOrder.CurrentOrderState);
        }
        
        [Fact]
        public void UpdateInProgressOrderStateExceptionSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.InProgress);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetInProgressStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.InProgress);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetWaitingStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.InProgress);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCompletedStatus());
        }
        
        [Fact]
        public void UpdateWaitingOrderStateSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Waiting);
            merchOrder.SetInProgressStatus();
            Assert.Equal(OrderState.InProgress, merchOrder.CurrentOrderState); 
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Waiting);
            merchOrder.SetCancelledStatus();
            Assert.Equal(OrderState.Cancelled, merchOrder.CurrentOrderState); 
        }
        
        [Fact]
        public void UpdateWaitingOrderStateExceptionSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Waiting);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetWaitingStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Waiting);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetGiveOutStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Waiting);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCompletedStatus());
        }
        
        [Fact]
        public void UpdateGiveOutOrderStateSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.GiveOut);
            merchOrder.SetCompletedStatus();
            Assert.Equal(OrderState.Completed, merchOrder.CurrentOrderState);
        }
        
        [Fact]
        public void UpdateGiveOutOrderStateExceptionSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.GiveOut);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetWaitingStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.GiveOut);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetGiveOutStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.GiveOut);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetInProgressStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.GiveOut);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCancelledStatus());
        }
        
        [Fact]
        public void UpdateCanceledOrderStateSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Cancelled);
            merchOrder.SetCompletedStatus();
            Assert.Equal(OrderState.Completed, merchOrder.CurrentOrderState);
        }
        
        [Fact]
        public void UpdateCanceledOrderStateExceptionSuccess()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Cancelled);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetWaitingStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Cancelled);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetGiveOutStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Cancelled);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetInProgressStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Cancelled);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCancelledStatus());
        }
        
        [Fact]
        public void UpdateCompletedOrderStateException()
        {
            var merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Completed);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetWaitingStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Completed);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetGiveOutStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Completed);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetInProgressStatus());
            
            merchOrder = MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Completed);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCancelledStatus());
            
            merchOrder =  MerchOrder.Create(new EmployeeId(1), MerchPack.WelcomePack, OrderState.Completed);
            Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.SetCompletedStatus());
        }
    }
}