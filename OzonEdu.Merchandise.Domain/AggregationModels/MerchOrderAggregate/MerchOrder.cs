using System;
using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.Events;
using OzonEdu.Merchandise.Domain.Exceptions;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchOrder:Entity
    {
        public MerchOrder(Employee employee, MerchPack merchPack, OrderState orderState)
        {
            Employee = employee;
            MerchPack = merchPack;
            if(orderState.Equals(OrderState.New))
                AddCreatedNewMerchOrderDomainEvent();
            CurrentOrderState = orderState;
        }
        public Employee Employee { get; }
        public MerchPack MerchPack { get; }
        public OrderState CurrentOrderState { get; private set; }
     
        /// <summary>
        /// При каждом состоянии своё событие и бизнес логика. 
        /// </summary>
        public void UpdateOrderState(OrderState newState)
        {
            if (newState == null)
                throw new ArgumentNullException();
            if (!Equals(CurrentOrderState, newState))
            {
                if (Equals(CurrentOrderState, OrderState.Completed))
                {
                    ThrowWrongStateException(newState);
                }
                else if (Equals(CurrentOrderState, OrderState.New))
                {
                    if (Equals(newState, OrderState.InProgress)||
                        Equals(newState, OrderState.Waiting)||
                        Equals(newState, OrderState.Canceled))
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (Equals(CurrentOrderState, OrderState.InProgress))
                {
                    if (Equals(newState, OrderState.GiveOut)||
                        Equals(newState, OrderState.Canceled))
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }
                else if (Equals(CurrentOrderState, OrderState.GiveOut))
                {
                    if (Equals(newState, OrderState.Completed))
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (Equals(CurrentOrderState, OrderState.Canceled))
                {
                    if (Equals(newState, OrderState.Completed))
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (Equals(CurrentOrderState, OrderState.Waiting))
                {
                    if (Equals(newState, OrderState.InProgress)||
                        Equals(newState, OrderState.Canceled))
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }
            }
            else
                ThrowWrongStateException(newState);
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
        
        private void AddMerchOrderStatusChangedDomainEvent()
        {
            var merchOrderStatusChangedDomainEvent = new MerchOrderStatusChangedDomainEvent(this);
            AddDomainEvent(merchOrderStatusChangedDomainEvent);
        }
    }
}