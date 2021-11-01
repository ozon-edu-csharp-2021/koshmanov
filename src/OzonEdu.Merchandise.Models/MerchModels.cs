using System;
using System.Collections.Generic;
using System.Linq;

namespace OzonEdu.Merchandise.Models
{
    public sealed record Employee
    {
        public long Id { get; init; }
        public string Name { get; init; }

        public Employee(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
    public sealed record MerchOrder
    {
        public long Id { get; init; }
        public MerchOrderStatus Status{ get; init; }
        public List<MerchItem> MerchItems { get; init; }

        public MerchOrder(long id, List<MerchItem> items)
        {
            Id = id;
            MerchItems = items;
            Status = MerchOrderStatus.New;
        }
    }

    public sealed class MerchItem
    {
        public string Name { get; }

        public MerchItem(string name)
        {
            Name = name;
        }
    }

    public enum MerchOrderStatus
    {
        New,
        InProgress, 
        GiveOut,
        Other
    }
}