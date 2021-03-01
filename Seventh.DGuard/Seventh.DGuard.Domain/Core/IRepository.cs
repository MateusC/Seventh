using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seventh.DGuard.Domain.Core
{
    public interface IRepository<T> where T : Entity
    {
        T Get(Guid id);
        
        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        bool Delete(Guid id);

        bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}