using System.Collections.Generic;
using System.Linq;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public class OrderState : Enumeration
    {
        /// <summary>
        /// New - начальное состояние всех заказов
        /// </summary>
        public  static readonly  OrderState New = new OrderState(1, nameof(New));
        /// <summary>
        /// InProgress - может наступить после New или после Waiting. Возможно только при наличии запрашиваемого мерча на складе.
        /// Используется в момент между подтверждением наличия мерча и получением мерча сотрудником.(?)
        /// </summary>
        public static readonly OrderState InProgress = new OrderState(2, nameof(InProgress));
        /// <summary>
        /// GiveOut - может наступить после InProgress. Возможен после отправки уведомления о выдаче мерча пользователю
        /// </summary>
        public static readonly OrderState GiveOut = new OrderState(3, nameof(GiveOut));
        /// <summary>
        /// Completed - может наступить после GiveOut и Сancelled. Последняя стадия заказа, после которой часть данных может быть удалена из бд.
        /// </summary>
        public static readonly OrderState Completed = new OrderState(4, nameof(Completed));
        /// <summary>
        /// Waiting - может наступить после New. Наступает при отсутствии на складе запрашиваемого мерча и после его резервирования.
        /// </summary>
        public static readonly OrderState Waiting = new OrderState(5, nameof(Waiting));
        /// <summary>
        /// Canceled - может наступить после New, Waiting и InProgress. Наступает при отсутствии на складе запрашиваемого мерча и после его резервирования.
        /// </summary>
        public static readonly OrderState Cancelled = new OrderState(6, nameof(Cancelled));

        private static readonly List<OrderState> SearchList = new List<OrderState>()
        {
            New, InProgress, Waiting, GiveOut, Cancelled, Completed
        };
        
        private OrderState(int id, string name) : base(id, name)
        {
            
        }

        public static bool TryGetOrderStateById(int id, out OrderState orderState)
        {
            orderState = SearchList.FirstOrDefault(x => x.Id.Equals(id));
            return orderState != null;
        }

        public static OrderState GetOrderStateById(int id)
        {
            return SearchList.FirstOrDefault(x => x.Id.Equals(id));
        }
        
        public static IReadOnlyCollection<int> GetActiveStateIdList()
        {
            return SearchList.Where(x =>x.Name!=Completed.Name && x.Name!= Cancelled.Name).Select(x =>x.Id).ToList();
        }

        public static IReadOnlyCollection<int> GetCompletedIdList()
        {
            return SearchList.Where(x =>x.Name==Completed.Name).Select(x =>x.Id).ToList();
        }
        public static bool TryGetOrderStateByName(string name, out OrderState orderState)
        {
            orderState = SearchList.FirstOrDefault(x => string.Equals(x.Name, name));
            return orderState != null;
        }
    }
}