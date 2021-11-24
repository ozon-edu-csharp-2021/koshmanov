using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Domain.Events;


namespace OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate
{
    public interface IMerchOrderRepository:IRepository<MerchOrder>
    {
        Task<MerchOrder> CreateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default);
        Task<MerchOrder> UpdateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default);
        
        Task<MerchOrder> FindById(long orderId, CancellationToken cancellationToken = default);
        Task<OrderState> CheckOrderState(long orderId, CancellationToken cancellationToken = default);
        Task<bool> CheckEmployeeHaveMerch(long employeeId, long merchPackId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MerchOrder>> GetAllEmployeeCompleteOrders(long employeeId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MerchOrder>> GetAllEmployeeInProcessOrders(long employeeId, CancellationToken cancellationToken = default);
        Task<bool>CheckEmployeeHaveMerchOrders(long employeeId, long merchPackId,CancellationToken cancellationToken= default);
        Task<IReadOnlyList<MerchOrder>> GetAllEmployeeOrdersInSpecialStatus(long employeeId, List<int> statusList,
            CancellationToken cancellationToken = default);
    }
}