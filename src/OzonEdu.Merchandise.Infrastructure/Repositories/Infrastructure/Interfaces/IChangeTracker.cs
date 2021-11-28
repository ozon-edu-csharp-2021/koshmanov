using System.Collections.Generic;
using OzonEdu.Merchandise.Domain.Models;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces
{
    public interface IChangeTracker
    {
        /// <summary>
        /// Коллекция всех сущностей, которые так или иначе были использованы в репозитории.
        /// </summary>
        IEnumerable<Entity> TrackedEntities { get; }
        
        public void Track(Entity entity);
    }   
    
}