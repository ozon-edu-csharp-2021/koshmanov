using System.Threading;
using System.Threading.Tasks;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;

namespace OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        Task<Employee> CreateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}