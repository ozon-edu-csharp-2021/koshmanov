using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class OrderState : Enumeration
    {
        public static readonly OrderState New = new OrderState(1, nameof(New));
        public static readonly OrderState InProgress = new OrderState(1, nameof(InProgress));
        public static readonly OrderState GiveOut = new OrderState(1, nameof(GiveOut));
        public static readonly OrderState Other = new OrderState(1, nameof(Other));
        public static readonly OrderState Completed = new OrderState(1, nameof(Completed));
        public static readonly OrderState Waiting = new OrderState(1, nameof(Waiting));
        public static readonly OrderState Canceled = new OrderState(1, nameof(Canceled));
        public string Description { get; }
        public OrderState(int id, string name) : base(id, name)
        {
            
        }
        public OrderState(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }
    }
}