namespace OzonEdu.Merchandise.Domain.Contracts
{
    public interface IRepository<T> where T: IAggregateRoot 
    {
        IUnitOfWork UnitOfWork { get; }
    }
}