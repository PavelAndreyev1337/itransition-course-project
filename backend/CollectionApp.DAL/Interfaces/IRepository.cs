using CollectionApp.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<EntityPageDTO<TEntity>> Paginate(int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null);
        Task<TEntity> Get(int id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
