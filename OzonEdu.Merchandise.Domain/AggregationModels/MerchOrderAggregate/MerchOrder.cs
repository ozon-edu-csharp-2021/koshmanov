using System;
using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Events;
using OzonEdu.Merchandise.Domain.Exceptions;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchOrder:Entity, IAggregateRoot
    {
        private MerchOrder(EmployeeId employeeId, MerchPack merchPack, OrderState orderState)
        {
            EmployeeId = employeeId;
            MerchPack = merchPack;
            CurrentOrderState = orderState;
            if(CurrentOrderState.Equals(OrderState.New))
                AddCreatedNewMerchOrderDomainEvent();
        }
        private MerchOrder(EmployeeId employeeId, MerchPack merchPack)
        {
            EmployeeId = employeeId;
            MerchPack = merchPack;
            CurrentOrderState = OrderState.New;
            AddCreatedNewMerchOrderDomainEvent();
        }

        public static MerchOrder Create(EmployeeId employeeId, MerchPack merchPack)
        {
            var merchOrder = new MerchOrder(employeeId, merchPack);
            return merchOrder;
        }
        public static MerchOrder Create(EmployeeId employeeId, MerchPack merchPack, OrderState orderState)
        {
            var merchOrder = new MerchOrder(employeeId, merchPack, orderState);
            return merchOrder;
        }
        public EmployeeId EmployeeId { get; }
        public MerchPack MerchPack { get; }
        public OrderState CurrentOrderState { get; private set; }
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
                AddDomainEvent(new OrderStateChangedToGiveOutEvent(this));
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