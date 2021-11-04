namespace OzonEdu.Merchandise.Domain.Contracts
{
    public interface IRepository<TAggregateRoot>  
    {
        IUnitOfWork UnitOfWork { get; }
    }
}