using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;


namespace OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Mock
{
    public class MerchOrderRepository:IMerchOrderRepository
    { 
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchOrder> CreateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
        public Task<MerchOrder> UpdateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
        public Task<MerchOrder> FindById(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
        public Task<MerchOrder> CheckOrderState(MerchOrder order)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckEmployeeMerch(long employeeId, MerchPack merchPack)
        {
            throw new System.NotImplementedException();
        }
    }
}