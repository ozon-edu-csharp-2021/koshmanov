using System;
using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.Events;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class MerchOrder:Entity
    {
        public MerchOrder(Employee employee, List<MerchItem> merchItem, MerchManager merchManager, MerchType merchType)
        {
            Employee = employee;
            MerchItems = merchItem;
            MerchManager = merchManager;
            CurrentOrderState =  OrderState.New;
            PrevOrderState = CurrentOrderState;
            MerchType = merchType;
            AddCreatedNewMerchOrderDomainEvent();
        }
        public Employee Employee { get; }
        public List<MerchItem> MerchItems { get; }
        public MerchManager MerchManager { get; }
        public OrderState CurrentOrderState { get; private set; }
        public OrderState PrevOrderState { get; private set;}
        public MerchType MerchType { get; }

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
                }else if (CurrentOrderState==OrderState.Other)
                {
                    if (newState==OrderState.Completed||
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
        }

        private void ThrowWrongStateException(OrderState newState)
        {
            throw new Exception($"Incorrect new state value {newState.Name} "+
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
            
        }
    }
}