using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.Merchandise.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken token);
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}