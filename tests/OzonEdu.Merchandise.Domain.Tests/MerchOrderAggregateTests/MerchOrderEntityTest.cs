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
            var employee = new Fixture().Build<Employee>().Create();
            MerchOrder merchOrder;

            var list = new List<OrderState>()
            {
                OrderState.Canceled,
                OrderState.Waiting,
                OrderState.InProgress
            };
            //Arrange
            
            list.ForEach(x =>
            {
                merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.New);
                //Action
                merchOrder.UpdateOrderState(x);
                
                //Assert
                Assert.Equal(x, merchOrder.CurrentOrderState);
            }); 
        }
        
        [Fact]
        public void UpdateNewOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.New);;

            var list = new List<OrderState>()
            {
                OrderState.New,
                OrderState.Completed,
                OrderState.GiveOut
            };
            
            list.ForEach(x =>
            {
                Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.UpdateOrderState(x));
            }); 
        }
        
        [Fact]
        public void UpdateInProgressOrderStateSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            MerchOrder merchOrder;

            var list = new List<OrderState>()
            {
                OrderState.Canceled,
                OrderState.GiveOut
            };
            list.ForEach(x =>
            {
                merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.InProgress);
                merchOrder.UpdateOrderState(x);
                Assert.Equal(x, merchOrder.CurrentOrderState);
            }); 
        }
        
        [Fact]
        public void UpdateInProgressOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.InProgress);;

            var list = new List<OrderState>()
            {
                OrderState.InProgress,
                OrderState.Completed,
                OrderState.Waiting,
                OrderState.New
            };
            
            list.ForEach(x =>
            {
                Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.UpdateOrderState(x));
            }); 
        }
        
        [Fact]
        public void UpdateWaitingOrderStateSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            MerchOrder merchOrder;

            var list = new List<OrderState>()
            {
                OrderState.Canceled,
                OrderState.InProgress
            };
            list.ForEach(x =>
            {
                merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.Waiting);
                merchOrder.UpdateOrderState(x);
                Assert.Equal(x, merchOrder.CurrentOrderState);
            }); 
        }
        
        [Fact]
        public void UpdateWaitingOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.Waiting);;

            var list = new List<OrderState>()
            {
                OrderState.Waiting,
                OrderState.Completed,
                OrderState.GiveOut,
                OrderState.New
            };
            
            list.ForEach(x =>
            {
                Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.UpdateOrderState(x));
            }); 
        }
        
        [Fact]
        public void UpdateGiveOutOrderStateSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            MerchOrder merchOrder;

            var list = new List<OrderState>()
            {
                OrderState.Completed
            };
            list.ForEach(x =>
            {
                merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.GiveOut);
                merchOrder.UpdateOrderState(x);
                Assert.Equal(x, merchOrder.CurrentOrderState);
            }); 
        }
        
        [Fact]
        public void UpdateGiveOutOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.GiveOut);
            var list = new List<OrderState>()
            {
                OrderState.Waiting,
                OrderState.InProgress,
                OrderState.GiveOut,
                OrderState.New,
                OrderState.Canceled
            };
            
            list.ForEach(x =>
            {
                Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.UpdateOrderState(x));
            }); 
        }
        
        [Fact]
        public void UpdateCanceledOrderStateSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            MerchOrder merchOrder;
            var list = new List<OrderState>()
            {
                OrderState.Completed
            };
            
            list.ForEach(x =>
            {
                merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.Canceled);
                merchOrder.UpdateOrderState(x);
                Assert.Equal(x, merchOrder.CurrentOrderState);
            }); 
        }
        
        [Fact]
        public void UpdateCanceledOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.Canceled);
            var list = new List<OrderState>()
            {
                OrderState.Waiting,
                OrderState.InProgress,
                OrderState.GiveOut,
                OrderState.New,
                OrderState.Canceled
            };
            list.ForEach(x =>
            {
                Assert.Throws<WrongOrderStateValueException>(()=>merchOrder.UpdateOrderState(x));
            }); 
        }
        
        [Theory]
        [MemberData(nameof(GetOrderStateList))]
        public void UpdateCompletedOrderStateSuccess(List<OrderState> list)
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.Completed);
            
            list.ForEach(x=> Assert.Throws<WrongOrderStateValueException>(() =>merchOrder.UpdateOrderState(x)));
        }

        [Fact]
        public void UpdateNullOrderStateExceptionSuccess()
        {
            var employee = new Fixture().Build<Employee>().Create();
            var merchOrder = new MerchOrder(employee, MerchPack.WelcomePack, OrderState.New);

            Assert.Throws<ArgumentNullException>(() => merchOrder.UpdateOrderState(null));
        }

        public static IEnumerable<object[]> GetOrderStateList()
        {
            yield return new object[]
            {
                new List<OrderState>()
                {
                    OrderState.Canceled, OrderState.New, 
                    OrderState.Waiting, OrderState.GiveOut,
                    OrderState.InProgress, OrderState.Completed
                }
            };
        }
        
        
        public static IEnumerable<object[]> GetOrderStateListWithoutCompleted()
        {
            yield return new object[]
            {
                new List<OrderState>()
                {
                    OrderState.Canceled, OrderState.New, OrderState.Waiting,
                    OrderState.GiveOut, OrderState.InProgress
                }
            };
        }
    }
}