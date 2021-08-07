using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
