namespace OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Interfaces
{
    /// <summary>
    /// Описывает классы репозиториев.
    /// </summary>
    public interface IRepository
    {
        void SetStorageContext(IStorageContext storageContext);
        
    }
}