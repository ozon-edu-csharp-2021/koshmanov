using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Events;
using OzonEdu.Merchandise.Domain.Exceptions;
using OzonEdu.Merchandise.Domain.Models;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchOrder:Entity, IAggregateRoot
    {
        private MerchOrder(EmployeeId employeeId, PackId merchPackId, OrderState orderState, OrderDate orderDate)
        {
            EmployeeId = employeeId;
            MerchPackId = merchPackId;
            CurrentOrderState = orderState;
            OrderDate = orderDate;
            if(CurrentOrderState.Equals(OrderState.New))
                AddCreatedNewMerchOrderDomainEvent();
        }
        
        private MerchOrder(long id, EmployeeId employeeId, PackId merchPackId, OrderState orderState, OrderDate orderDate)
        {
            Id = id;
            EmployeeId = employeeId;
            MerchPackId = merchPackId;
            OrderDate = orderDate;
            CurrentOrderState = orderState;
            if(CurrentOrderState.Equals(OrderState.New))
                AddCreatedNewMerchOrderDomainEvent();
        }
        
        private MerchOrder( EmployeeId employeeId, PackId merchPackId, OrderDate orderDate)
        {
            EmployeeId = employeeId;
            MerchPackId = merchPackId;
            OrderDate = orderDate;
            CurrentOrderState = OrderState.New;
            AddCreatedNewMerchOrderDomainEvent();
        }

        public static MerchOrder Create(EmployeeId employeeId, PackId merchPackId, OrderDate orderDate)
        {
            var merchOrder = new MerchOrder(employeeId, merchPackId, orderDate);
            return merchOrder;
        }
        public static MerchOrder Create(EmployeeId employeeId, PackId merchPackId, OrderState orderState, OrderDate orderDate)
        {
            var merchOrder = new MerchOrder(employeeId, merchPackId, orderState, orderDate);
            return merchOrder;
        }
        public static MerchOrder Create(long id, EmployeeId employeeId, PackId merchPackId, OrderState orderState, OrderDate orderDate)
        {
            var merchOrder = new MerchOrder(id, employeeId, merchPackId, orderState, orderDate);
            return merchOrder;
        }
        public EmployeeId EmployeeId { get; }
        public PackId MerchPackId { get; }
        public OrderState CurrentOrderState { get; private set; }
        public OrderDate OrderDate { get; }

        public void SetInProgressStatus()
        {
            if (CurrentOrderState.Equals(OrderState.New)||
                CurrentOrderState.Equals(OrderState.Waiting))
            {
                CurrentOrderState = OrderState.InProgress;
                AddDomainEvent(new OrderStateChangedToInProgressDomainEvent(this));
            }
            else
            {
                ThrowWrongStateException(OrderState.InProgress);
            }
        }
        public void SetWaitingStatus()
        {
            if (CurrentOrderState.Equals(OrderState.New))
            {
                CurrentOrderState = OrderState.Waiting;
                AddDomainEvent(new OrderStateChangedToWaitingDomainEvent(this));
            }
            else
            {
                ThrowWrongStateException(OrderState.Waiting);
            }
        }
        public void SetGiveOutStatus()
        {
            if (CurrentOrderState.Equals(OrderState.InProgress))
            {
                CurrentOrderState = OrderState.GiveOut;
                AddDomainEvent(new OrderStateChangedToGiveOutEvent
                {
                    MerchPackId = MerchPackId,
                    NewState = CurrentOrderState,
                    EmployeeId = EmployeeId
                });
            }
            else
            {
                ThrowWrongStateException(OrderState.GiveOut);
            }
        }
        public void SetCancelledStatus()
        {
            if (CurrentOrderState.Equals(OrderState.New)||
                CurrentOrderState.Equals(OrderState.Waiting)||
                CurrentOrderState.Equals(OrderState.InProgress))
            {
                CurrentOrderState = OrderState.Cancelled;
                AddDomainEvent(new OrderStateChangedToCancelledEvent(this));
            }
            else
            {
                ThrowWrongStateException(OrderState.Cancelled);
            }
        }
        public void SetCompletedStatus()
        {
            if (CurrentOrderState.Equals(OrderState.GiveOut)||
                CurrentOrderState.Equals(OrderState.Cancelled))
            {
                CurrentOrderState = OrderState.Completed;
                AddDomainEvent(new OrderStateChangedToCompletedEvent(this));
            }
            else
            {
                ThrowWrongStateException(OrderState.Completed);
            }
        }
        private void ThrowWrongStateException(OrderState newState)
        {
            throw new WrongOrderStateValueException($"Incorrect new state value {newState.Name} " +
                                                    $"Current state {CurrentOrderState.Name}; ");
        }
        private void AddCreatedNewMerchOrderDomainEvent()
        {
            var orderCreatedDe = new CreatedNewMerchOrderDomainEvent(this);
            AddDomainEvent(orderCreatedDe);
        }
    }
}