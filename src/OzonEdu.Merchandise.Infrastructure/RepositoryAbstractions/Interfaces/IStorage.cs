namespace OzonEdu.Merchandise.Infrastructure.RepositoryAbstractions.Interfaces
{
    /// <summary>
    /// Этот интерфейс описывает реализацию шаблона проектирования Единица работы. Объект класса, реализующего этот интерфейс,
    /// будет единственной точкой доступа к хранилищу и должен существовать в единственном экземпляре
    /// в рамках одного запроса к веб-приложению
    /// </summary>
    public interface IStorage
    {
        T GetRepository<T>() where T : IRepository;
        void Save();
    }
}