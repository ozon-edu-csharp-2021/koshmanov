using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class OrderState : Enumeration
    {
        /// <summary>
        /// New - начальное состояние всех заказов
        /// </summary>
        public static readonly OrderState New = new OrderState(1, nameof(New));
        /// <summary>
        /// InProgress - может наступить после New или после Waiting. Возможно только при наличии запрашиваемого мерча на складе.
        /// Используется в момент между подтверждением наличия мерча и получением мерча сотрудником.(?)
        /// </summary>
        public static readonly OrderState InProgress = new OrderState(1, nameof(InProgress));
        /// <summary>
        /// GiveOut - может наступить после InProgress. Возможен после отправки уведомления о выдаче мерча пользователю
        /// </summary>
        public static readonly OrderState GiveOut = new OrderState(1, nameof(GiveOut));
        /// <summary>
        /// Completed - может наступить после GiveOut. Последняя стадия заказа, после которой часть данных может быть удалена из бд.
        /// </summary>
        public static readonly OrderState Completed = new OrderState(1, nameof(Completed));
        /// <summary>
        /// Waiting - может наступить после InProgress и New. Наступает при отсутствии на складе запрашиваемого мерча и после его резервирования.
        /// </summary>
        public static readonly OrderState Waiting = new OrderState(1, nameof(Waiting));
        /// <summary>
        /// Canceled - может наступить после New, Waiting и InProgress. Наступает при отсутствии на складе запрашиваемого мерча и после его резервирования.
        /// </summary>
        public static readonly OrderState Canceled = new OrderState(1, nameof(Canceled));
        public string Description { get; }
        private OrderState(int id, string name) : base(id, name)
        {
            
        }
        private OrderState(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }
    }
}