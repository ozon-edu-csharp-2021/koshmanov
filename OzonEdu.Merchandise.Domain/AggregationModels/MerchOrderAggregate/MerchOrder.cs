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
        /// <param name="newState"></param>
        public void UpdateOrderState(OrderState newState)
        {
            if (CurrentOrderState!=newState)
            {
                if (CurrentOrderState==OrderState.Completed)
                {
                    ThrowWrongStateException(newState);
                }
                else if (CurrentOrderState==OrderState.New)
                {
                    if (newState==OrderState.InProgress||
                        newState==OrderState.Waiting||
                        newState==OrderState.Canceled)
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (CurrentOrderState==OrderState.InProgress)
                {
                    if (newState==OrderState.GiveOut||
                        newState==OrderState.Waiting||
                        newState==OrderState.Canceled)
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }
                else if (CurrentOrderState==OrderState.GiveOut)
                {
                    if (newState==OrderState.Completed)
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (CurrentOrderState==OrderState.Canceled)
                {
                    if (newState==OrderState.Completed)
                    {
                        CurrentOrderState = newState;
                        AddMerchOrderStatusChangedDomainEvent();
                    }
                    else
                    {
                        ThrowWrongStateException(newState);
                    }
                }else if (CurrentOrderState==OrderState.Waiting)
                {
                    if (newState==OrderState.InProgress||
                        newState==OrderState.Canceled)
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
            throw new WrongOrderStateValueException($"Incorrect new state value {newState.Name} "+
                                $"{(string.IsNullOrEmpty(newState.Description)?".":"Description "+newState.Description)}. "+
                                $"Current state {CurrentOrderState.Name}; "+
                                $"{(string.IsNullOrEmpty(CurrentOrderState.Description)?".":"Description "+CurrentOrderState.Description)}. ");
        }

        private void AddCreatedNewMerchOrderDomainEvent()
        {
            var orderCreatedDE = new CreatedNewMerchOrderDomainEvent(this);
            
            AddDomainEvent(orderCreatedDE);
        }
        
        private void AddMerchOrderStatusChangedDomainEvent()
        {
            var merchOrderStatusChangedDomainEvent = new MerchOrderStatusChangedDomainEvent(this);
            this.AddDomainEvent(merchOrderStatusChangedDomainEvent);
        }
    }
}